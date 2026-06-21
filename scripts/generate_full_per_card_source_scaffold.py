# -*- coding: utf-8 -*-
"""Generate the full per-card/source-effect scaffold for queue 64.

This generator is mapping-only. It creates traceable scaffold records from the
approved source snapshot, but it does not implement card effects and does not
promote source-bearing effects to NoEffect/Implemented/Verified.
"""

from __future__ import annotations

import argparse
import json
import sys
from collections import Counter, defaultdict
from pathlib import Path
from typing import Any

SCRIPT_DIR = Path(__file__).resolve().parent
if str(SCRIPT_DIR) not in sys.path:
    sys.path.insert(0, str(SCRIPT_DIR))

from generate_full_card_pool_manifest import (  # noqa: E402
    assert_current_source_fingerprint,
    assert_source_lock,
    file_sha256,
)


STATUS_UNSUPPORTED = "Unsupported"
STATUS_NO_EFFECT = "NoEffect"
STATUS_NEEDS_REVIEW = "NeedsSourceReview"

CARD_EFFECT_ROOT = "DCGO/Assets/Scripts/CardEffect/"
SCAFFOLD_ROOT = "docs/generated/full-card-source-scaffold"
SCHEMA_VERSION = 1


def read_json(path: Path) -> Any:
    with path.open("r", encoding="utf-8-sig") as handle:
        return json.load(handle)


def write_json(path: Path, value: Any) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(
        json.dumps(value, ensure_ascii=False, indent=2, sort_keys=False) + "\n",
        encoding="utf-8",
    )


def safe_segment(value: str | None) -> str:
    text = (value or "Unknown").strip() or "Unknown"
    return "".join(ch if ch.isalnum() or ch in {"_", "-"} else "_" for ch in text)


def source_set_id(source_path: str | None) -> str:
    if not source_path:
        return "MissingSource"
    if not source_path.startswith(CARD_EFFECT_ROOT):
        return "Script"
    rest = source_path[len(CARD_EFFECT_ROOT) :]
    return safe_segment(rest.split("/", 1)[0])


def planned_rl_source_file(source_path: str) -> str:
    if source_path.startswith(CARD_EFFECT_ROOT):
        rest = source_path[len(CARD_EFFECT_ROOT) :]
    else:
        rest = source_path.replace("DCGO/Assets/Scripts/", "")
    return f"src/DCGO.RL.Engine/CardEffects/FullCardPool/SourceEffects/{rest}"


def source_scaffold_id(effect_class_name: str, source_path: str) -> str:
    return f"{effect_class_name}@{source_path}"


def card_status(record: dict[str, Any]) -> tuple[str, str]:
    effect_class = str(record.get("cardEffectClassName") or "").strip()
    paths = record.get("sourceEffectPaths") or []
    if record.get("importError"):
        return STATUS_NEEDS_REVIEW, f"asset import/parsing issue: {record['importError']}"
    if not effect_class:
        return STATUS_NO_EFFECT, "CardEffectClassName is empty in the source asset; this is an explicit no-effect marker only."
    if not record.get("sourceBodyExists"):
        return STATUS_NEEDS_REVIEW, "CardEffectClassName exists but no matching source body was found."
    if len(paths) != 1:
        return STATUS_NEEDS_REVIEW, "CardEffectClassName resolves to multiple source bodies; manual source review is required."
    return STATUS_UNSUPPORTED, "Source body exists, but queue 64 only creates scaffold and does not implement the effect body."


def compact_card_reference(record: dict[str, Any]) -> dict[str, Any]:
    return {
        "definitionStableId": record["definitionStableId"],
        "cardId": record["cardId"],
        "cardIndex": record["cardIndex"],
        "variantKey": record["variantKey"],
        "assetPath": record["assetPath"],
    }


def build_scaffold(workspace: Path) -> dict[str, Any]:
    lock = assert_source_lock(workspace)
    assert_current_source_fingerprint(workspace, lock)

    manifest_path = workspace / "docs/generated/full-card-pool-manifest.json"
    inventory_path = workspace / "docs/generated/full-mechanic-inventory.json"
    if not manifest_path.exists():
        raise RuntimeError("docs/generated/full-card-pool-manifest.json 파일이 없습니다.")
    if not inventory_path.exists():
        raise RuntimeError("docs/generated/full-mechanic-inventory.json 파일이 없습니다.")

    manifest = read_json(manifest_path)
    inventory = read_json(inventory_path)
    records: list[dict[str, Any]] = manifest["records"]
    effect_source_records: list[dict[str, Any]] = inventory["effectSourceRecords"]

    source_details: dict[str, dict[str, Any]] = {}
    for source in effect_source_records:
        key = source_scaffold_id(source["effectClassName"], source["sourcePath"])
        source_details[key] = source

    card_mapping_records: list[dict[str, Any]] = []
    referenced_source_keys: set[str] = set()
    for record in records:
        status, reason = card_status(record)
        source_refs: list[str] = []
        for source_path in record.get("sourceEffectPaths") or []:
            key = source_scaffold_id(record["cardEffectClassName"], source_path)
            source_refs.append(key)
            referenced_source_keys.add(key)

        set_id = safe_segment(record.get("setId"))
        card_mapping_records.append(
            {
                "definitionStableId": record["definitionStableId"],
                "cardId": record["cardId"],
                "cardIndex": record["cardIndex"],
                "variantKey": record["variantKey"],
                "assetName": record["assetName"],
                "assetPath": record["assetPath"],
                "setId": record["setId"],
                "assetColorPath": record["assetColorPath"],
                "assetKindPath": record["assetKindPath"],
                "cardEffectClassName": record.get("cardEffectClassName"),
                "sourceEffectPaths": record.get("sourceEffectPaths") or [],
                "sourceBodyExists": bool(record.get("sourceBodyExists")),
                "sourceEffectScaffoldRefs": source_refs,
                "mappingStatus": status,
                "statusReason": reason,
                "cardMappingFile": f"{SCAFFOLD_ROOT}/cards/{set_id}.json",
                "registryCatalogFile": f"{SCAFFOLD_ROOT}/catalogs/{set_id}.json",
            }
        )

    source_scaffold_records: list[dict[str, Any]] = []
    for source in effect_source_records:
        set_id = source_set_id(source["sourcePath"])
        key = source_scaffold_id(source["effectClassName"], source["sourcePath"])
        source_scaffold_records.append(
            {
                "sourceScaffoldId": key,
                "sourceEffectClassName": source["effectClassName"],
                "sourcePath": source["sourcePath"],
                "sourceSetId": set_id,
                "sourceScaffoldFile": f"{SCAFFOLD_ROOT}/sources/{set_id}.json",
                "plannedRlSourceFile": planned_rl_source_file(source["sourcePath"]),
                "scaffoldStatus": STATUS_UNSUPPORTED,
                "statusReason": "Source body exists, but queue 64 creates a marker scaffold only.",
                "timings": source.get("timings") or [],
                "flags": source.get("flags") or {},
                "selectionKinds": source.get("selectionKinds") or [],
                "rootZones": source.get("rootZones") or [],
                "factoryApi": source.get("factoryApi") or [],
                "commonsApi": source.get("commonsApi") or [],
                "classApi": source.get("classApi") or [],
                "setMethods": source.get("setMethods") or [],
                "affectedCards": source.get("affectedCards") or [],
                "affectedCardCount": source.get("affectedCardCount") or 0,
            }
        )

    missing_source_markers = [
        record
        for record in card_mapping_records
        if record["mappingStatus"] == STATUS_NEEDS_REVIEW
    ]
    no_effect_markers = [
        record
        for record in card_mapping_records
        if record["mappingStatus"] == STATUS_NO_EFFECT
    ]

    cards_by_set: dict[str, list[dict[str, Any]]] = defaultdict(list)
    for record in card_mapping_records:
        cards_by_set[safe_segment(record["setId"])].append(record)

    sources_by_set: dict[str, list[dict[str, Any]]] = defaultdict(list)
    for record in source_scaffold_records:
        sources_by_set[safe_segment(record["sourceSetId"])].append(record)

    set_ids = sorted(set(cards_by_set) | set(sources_by_set), key=str)
    set_catalogs: list[dict[str, Any]] = []
    generated_files = {
        "index": f"{SCAFFOLD_ROOT}/index.json",
        "statusRegistry": f"{SCAFFOLD_ROOT}/status-registry.json",
        "cardMappings": [],
        "sourceScaffolds": [],
        "setCatalogs": [],
    }

    for set_id in set_ids:
        card_file = f"{SCAFFOLD_ROOT}/cards/{set_id}.json"
        source_file = f"{SCAFFOLD_ROOT}/sources/{set_id}.json"
        catalog_file = f"{SCAFFOLD_ROOT}/catalogs/{set_id}.json"
        generated_files["cardMappings"].append(card_file)
        generated_files["sourceScaffolds"].append(source_file)
        generated_files["setCatalogs"].append(catalog_file)
        set_catalogs.append(
            {
                "setId": set_id,
                "registryOnly": True,
                "cardMappingFile": card_file,
                "sourceScaffoldFile": source_file,
                "catalogFile": catalog_file,
                "cardMappingRecordCount": len(cards_by_set.get(set_id, [])),
                "sourceScaffoldRecordCount": len(sources_by_set.get(set_id, [])),
                "definitionStableIds": [record["definitionStableId"] for record in cards_by_set.get(set_id, [])],
                "sourceScaffoldIds": [record["sourceScaffoldId"] for record in sources_by_set.get(set_id, [])],
            }
        )

    card_status_counts = Counter(record["mappingStatus"] for record in card_mapping_records)
    source_status_counts = Counter(record["scaffoldStatus"] for record in source_scaffold_records)
    duplicate_card_ids = sum(1 for group in manifest.get("duplicateCardIdGroups", []) if group.get("count", 0) > 1)

    status_registry = {
        "schemaVersion": SCHEMA_VERSION,
        "statusPolicy": {
            "sourceBearingDefault": STATUS_UNSUPPORTED,
            "emptyCardEffectClassName": STATUS_NO_EFFECT,
            "missingOrAmbiguousSourceBody": STATUS_NEEDS_REVIEW,
            "implementedOrVerifiedAutoPromotion": False,
            "effectBodiesChanged": False,
        },
        "cardMappingStatusCounts": dict(sorted(card_status_counts.items())),
        "sourceScaffoldStatusCounts": dict(sorted(source_status_counts.items())),
        "implementedOrVerifiedCount": 0,
        "needsSourceReviewDefinitionStableIds": [record["definitionStableId"] for record in missing_source_markers],
        "noEffectDefinitionStableIds": [record["definitionStableId"] for record in no_effect_markers],
    }

    summary = {
        "manifestRecordCount": len(records),
        "cardMappingRecordCount": len(card_mapping_records),
        "sourceInventoryRecordCount": len(effect_source_records),
        "sourceScaffoldRecordCount": len(source_scaffold_records),
        "setCatalogCount": len(set_catalogs),
        "duplicateCardIdGroupCount": duplicate_card_ids,
        "duplicateCardIdGroupsPreserved": duplicate_card_ids == manifest["validation"]["duplicateCardIdGroupCount"],
        "definitionStableIdDuplicateGroupCount": manifest["validation"]["definitionStableIdDuplicateGroupCount"],
        "missingSourceBodyCount": manifest["validation"]["missingSourceBodyCount"],
        "needsSourceReviewMarkerCount": len(missing_source_markers),
        "noEffectMarkerCount": len(no_effect_markers),
        "referencedSourceScaffoldCount": len(referenced_source_keys),
        "sourceScaffoldWithoutAssetReferenceCount": len(source_scaffold_records) - len(referenced_source_keys),
    }

    index = {
        "schemaVersion": SCHEMA_VERSION,
        "sourceSnapshot": manifest["sourceSnapshot"],
        "inputManifests": {
            "fullCardPoolManifest": {
                "path": "docs/generated/full-card-pool-manifest.json",
                "sha256": file_sha256(manifest_path),
            },
            "fullMechanicInventory": {
                "path": "docs/generated/full-mechanic-inventory.json",
                "sha256": file_sha256(inventory_path),
            },
        },
        "identityPolicy": {
            "definitionStableId": "CardId#CardIndex@VariantKey",
            "variantFlattening": "forbidden",
            "cardIdOnlyRegistry": "forbidden",
        },
        "scaffoldPolicy": {
            "catalogRole": "registry-only",
            "effectBodyImplementation": "not-in-queue-64",
            "sourceBearingDefault": STATUS_UNSUPPORTED,
            "missingSourceBody": STATUS_NEEDS_REVIEW,
            "noEffectRequiresEmptyCardEffectClassName": True,
        },
        "summary": summary,
        "generatedFiles": generated_files,
        "setCatalogs": set_catalogs,
    }

    return {
        "index": index,
        "statusRegistry": status_registry,
        "cardsBySet": dict(cards_by_set),
        "sourcesBySet": dict(sources_by_set),
        "setCatalogs": set_catalogs,
    }


def write_outputs(workspace: Path, scaffold: dict[str, Any]) -> None:
    root = workspace / SCAFFOLD_ROOT
    write_json(root / "index.json", scaffold["index"])
    write_json(root / "status-registry.json", scaffold["statusRegistry"])

    catalogs_by_set = {catalog["setId"]: catalog for catalog in scaffold["setCatalogs"]}
    for set_id, records in scaffold["cardsBySet"].items():
        write_json(
            root / "cards" / f"{set_id}.json",
            {
                "schemaVersion": SCHEMA_VERSION,
                "setId": set_id,
                "recordKind": "card-variant-mapping",
                "records": sorted(records, key=lambda record: (record["cardId"], record["cardIndex"], record["variantKey"])),
            },
        )

    for set_id, records in scaffold["sourcesBySet"].items():
        write_json(
            root / "sources" / f"{set_id}.json",
            {
                "schemaVersion": SCHEMA_VERSION,
                "setId": set_id,
                "recordKind": "source-effect-scaffold",
                "records": sorted(records, key=lambda record: (record["sourceEffectClassName"], record["sourcePath"])),
            },
        )

    for set_id, catalog in catalogs_by_set.items():
        write_json(
            root / "catalogs" / f"{set_id}.json",
            {
                "schemaVersion": SCHEMA_VERSION,
                "setId": set_id,
                "recordKind": "set-registry-catalog",
                "registryOnly": True,
                "cardMappingFile": catalog["cardMappingFile"],
                "sourceScaffoldFile": catalog["sourceScaffoldFile"],
                "cardMappingRecordCount": catalog["cardMappingRecordCount"],
                "sourceScaffoldRecordCount": catalog["sourceScaffoldRecordCount"],
                "definitionStableIds": catalog["definitionStableIds"],
                "sourceScaffoldIds": catalog["sourceScaffoldIds"],
            },
        )

    write_markdown(workspace, scaffold)


def write_markdown(workspace: Path, scaffold: dict[str, Any]) -> None:
    index = scaffold["index"]
    summary = index["summary"]
    status = scaffold["statusRegistry"]
    lines = [
        "# Full Per-Card / Source-Effect Scaffold",
        "",
        "이 문서는 64번 queue의 생성 결과다. 효과 body를 구현하지 않고, 전체 asset/source identity를 추적 가능한 generated scaffold와 registry-only catalog로 고정한다.",
        "",
        "## 입력",
        "",
        f"- Source snapshot: `{index['sourceSnapshot']['kind']}` / `{index['sourceSnapshot']['fileManifest']}`",
        f"- Full card pool manifest: `{index['inputManifests']['fullCardPoolManifest']['path']}` / `{index['inputManifests']['fullCardPoolManifest']['sha256']}`",
        f"- Full mechanic inventory: `{index['inputManifests']['fullMechanicInventory']['path']}` / `{index['inputManifests']['fullMechanicInventory']['sha256']}`",
        "",
        "## Coverage",
        "",
        f"- Card mapping records: {summary['cardMappingRecordCount']} / {summary['manifestRecordCount']}",
        f"- Source effect scaffold records: {summary['sourceScaffoldRecordCount']} / {summary['sourceInventoryRecordCount']}",
        f"- Set registry catalogs: {summary['setCatalogCount']}",
        f"- Duplicate CardId groups preserved: {summary['duplicateCardIdGroupsPreserved']} ({summary['duplicateCardIdGroupCount']})",
        f"- DefinitionStableId duplicate groups: {summary['definitionStableIdDuplicateGroupCount']}",
        f"- Source body missing records: {summary['missingSourceBodyCount']}",
        f"- NeedsSourceReview markers: {summary['needsSourceReviewMarkerCount']}",
        "",
        "## Status Policy",
        "",
        f"- Source-bearing scaffold default: `{status['statusPolicy']['sourceBearingDefault']}`",
        f"- Empty CardEffectClassName marker: `{status['statusPolicy']['emptyCardEffectClassName']}`",
        f"- Missing/ambiguous source body marker: `{status['statusPolicy']['missingOrAmbiguousSourceBody']}`",
        f"- Implemented/Verified auto-promotion: `{status['statusPolicy']['implementedOrVerifiedAutoPromotion']}`",
        f"- Effect bodies changed by this queue: `{status['statusPolicy']['effectBodiesChanged']}`",
        "",
        "카드별 source-bearing scaffold는 이 단계에서 `Unsupported`이며, 원본 source body가 없거나 모호하면 `NeedsSourceReview`다. `NoEffect`는 원본 asset의 `CardEffectClassName`이 비어 있는 경우에만 사용한다.",
        "",
        "## Generated Files",
        "",
        f"- Index: `{index['generatedFiles']['index']}`",
        f"- Status registry: `{index['generatedFiles']['statusRegistry']}`",
        f"- Card mapping files: {len(index['generatedFiles']['cardMappings'])}",
        f"- Source scaffold files: {len(index['generatedFiles']['sourceScaffolds'])}",
        f"- Set catalog files: {len(index['generatedFiles']['setCatalogs'])}",
        "",
        "Catalog 파일은 `registryOnly=true`이며 effect body, 특정 CardId 분기, zone mutation logic을 포함하지 않는다.",
    ]
    (workspace / "docs/rl-engine/full-per-card-source-scaffold.md").write_text("\n".join(lines) + "\n", encoding="utf-8")


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--workspace", default=".", help="Workspace root")
    args = parser.parse_args()

    workspace = Path(args.workspace).resolve()
    scaffold = build_scaffold(workspace)
    write_outputs(workspace, scaffold)
    print(json.dumps(scaffold["index"]["summary"], ensure_ascii=False, indent=2))


if __name__ == "__main__":
    main()
