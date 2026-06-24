# -*- coding: utf-8 -*-
"""Generate the source-aligned full DCGO card asset manifest.

The generator is intentionally inventory-only. It does not implement effects,
does not collapse variants by CardId, and reports missing source bodies as
explicit anomalies instead of mapping them to NoEffect.
"""

from __future__ import annotations

import argparse
import hashlib
import json
import re
from pathlib import Path
from typing import Any


CARD_COLOR_NAMES = ["Red", "Blue", "Yellow", "Green", "White", "Black", "Purple", "None"]
CARD_KIND_NAMES = ["Digimon", "Tamer", "Option", "DigiEgg"]
RARITY_NAMES = ["C", "U", "R", "SR", "UR", "SEC", "P", "None"]
KNOWN_KEYWORDS = [
    "Blocker",
    "Security Attack",
    "Piercing",
    "Jamming",
    "Rush",
    "Reboot",
    "Retaliation",
    "Decoy",
    "Collision",
    "Alliance",
    "Blast Digivolve",
    "Overflow",
    "Delay",
    "DigiXros",
    "Link",
]


def unix_path(path: Path, root: Path) -> str:
    return path.resolve().relative_to(root.resolve()).as_posix()


def file_sha256(path: Path) -> str:
    digest = hashlib.sha256()
    with path.open("rb") as handle:
        for chunk in iter(lambda: handle.read(1024 * 1024), b""):
            digest.update(chunk)
    return digest.hexdigest()


def read_json(path: Path) -> Any:
    with path.open("r", encoding="utf-8-sig") as handle:
        return json.load(handle)


def write_json(path: Path, value: Any) -> None:
    path.write_text(
        json.dumps(value, ensure_ascii=False, indent=2, sort_keys=False) + "\n",
        encoding="utf-8",
    )


def source_checkout_candidates(workspace: Path) -> list[Path]:
    candidates = [workspace]
    external_source_parent = Path("E:/headlessDCGO")
    if external_source_parent.exists():
        candidates.append(external_source_parent)
    return candidates


def resolve_locked_source_path(workspace: Path, relative_path: str) -> tuple[Path, Path]:
    for root in source_checkout_candidates(workspace):
        candidate = root / relative_path
        if candidate.exists():
            return candidate, root
    return workspace / relative_path, workspace


def assert_source_lock(workspace: Path) -> dict[str, Any]:
    lock_path = workspace / "docs/source/dcgo-source-lock.json"
    if not lock_path.exists():
        raise RuntimeError("docs/source/dcgo-source-lock.json 파일이 없습니다.")

    lock = read_json(lock_path)
    if not lock.get("approved"):
        raise RuntimeError("dcgo-source-lock.json이 승인 상태가 아닙니다.")

    selected = lock.get("selectedSnapshot", {})
    if selected.get("kind") != "local-manifest":
        raise RuntimeError(f"현재 생성기는 approved local-manifest snapshot만 처리합니다: {selected.get('kind')}")

    manifest_path = workspace / selected["fileManifest"]
    if not manifest_path.exists():
        raise RuntimeError(f"선택된 source manifest가 없습니다: {selected['fileManifest']}")

    actual_hash = file_sha256(manifest_path)
    expected_hash = str(selected["fileManifestSha256"]).lower()
    if actual_hash.lower() != expected_hash:
        raise RuntimeError(f"source manifest hash mismatch: expected={expected_hash} actual={actual_hash}")

    return lock


def assert_current_source_fingerprint(workspace: Path, lock: dict[str, Any]) -> None:
    selected = lock["selectedSnapshot"]
    manifest = read_json(workspace / selected["fileManifest"])
    expected_paths: dict[str, dict[str, Any]] = {entry["path"]: entry for entry in manifest["files"]}

    for relative_path, entry in expected_paths.items():
        full_path, _ = resolve_locked_source_path(workspace, relative_path)
        if not full_path.exists():
            raise RuntimeError(f"source fingerprint mismatch: missing file {relative_path}")
        if full_path.stat().st_size != int(entry["size"]):
            raise RuntimeError(f"source fingerprint mismatch: size changed {relative_path}")
        actual_hash = file_sha256(full_path)
        if actual_hash.lower() != str(entry["sha256"]).lower():
            raise RuntimeError(f"source fingerprint mismatch: hash changed {relative_path}")

    for root in manifest["roots"]:
        root_path, checkout_root = resolve_locked_source_path(workspace, root)
        if not root_path.exists():
            raise RuntimeError(f"source fingerprint mismatch: missing root {root}")
        for current_file in sorted(path for path in root_path.rglob("*") if path.is_file()):
            relative_path = unix_path(current_file, checkout_root)
            if relative_path not in expected_paths:
                raise RuntimeError(f"source fingerprint mismatch: unexpected file {relative_path}")


def parse_asset_sections(text: str) -> dict[str, tuple[str, list[str]]]:
    lines = text.splitlines()
    sections: dict[str, tuple[str, list[str]]] = {}
    i = 0
    while i < len(lines):
        match = re.match(r"^\s{2}([A-Za-z_][A-Za-z0-9_]*):(.*)$", lines[i])
        if not match:
            i += 1
            continue
        key = match.group(1)
        value = match.group(2)
        block: list[str] = []
        i += 1
        while i < len(lines) and not re.match(r"^\s{2}[A-Za-z_][A-Za-z0-9_]*:", lines[i]):
            block.append(lines[i])
            i += 1
        sections[key] = (value, block)
    return sections


def normalize_scalar(value: str | None) -> str | None:
    if value is None:
        return None
    text = value.strip()
    if not text:
        return None
    if text == "[]":
        return ""
    if len(text) >= 2 and ((text[0] == '"' and text[-1] == '"') or (text[0] == "'" and text[-1] == "'")):
        text = text[1:-1]
    return (
        text.replace("\\n", " ")
        .replace("\\_", "_")
        .replace("\\uFF1C", "<")
        .replace("\\uFF1E", ">")
        .replace("\\uFF0D", "-")
        .replace("\\u0027", "'")
        .strip()
    )


def scalar(sections: dict[str, tuple[str, list[str]]], key: str) -> str | None:
    if key not in sections:
        return None
    value, block = sections[key]
    parts = [value]
    for line in block:
        if re.match(r"^\s{2}-\s", line):
            continue
        parts.append(line.strip())
    return normalize_scalar(" ".join(part for part in parts if part is not None))


def array_value(sections: dict[str, tuple[str, list[str]]], key: str) -> list[str]:
    if key not in sections:
        return []
    value, block = sections[key]
    if value.strip() == "[]":
        return []
    result: list[str] = []
    for line in block:
        match = re.match(r"^\s{2}-\s*(.*)$", line)
        if match:
            item = normalize_scalar(match.group(1))
            if item:
                result.append(item)
    return result


def nullable_int(value: str | None) -> int | None:
    if value is None or str(value).strip() == "":
        return None
    try:
        return int(str(value).strip())
    except ValueError:
        return None


def hex_int32_values(value: str | None) -> list[int]:
    if not value:
        return []
    hex_text = re.sub(r"[^0-9A-Fa-f]", "", value)
    values: list[int] = []
    for index in range(0, len(hex_text) - 7, 8):
        chunk = hex_text[index : index + 8]
        bytes_le = [int(chunk[i : i + 2], 16) for i in range(0, 8, 2)]
        values.append(bytes_le[0] + (bytes_le[1] << 8) + (bytes_le[2] << 16) + (bytes_le[3] << 24))
    return values


def enum_values(values: list[int], names: list[str]) -> list[dict[str, Any]]:
    result: list[dict[str, Any]] = []
    for value in values:
        if 0 <= value < len(names):
            name = names[value]
        else:
            name = f"Unknown({value})"
        result.append({"value": value, "name": name})
    return result


def evo_costs(sections: dict[str, tuple[str, list[str]]]) -> list[dict[str, Any]]:
    if "EvoCosts" not in sections:
        return []
    value, block = sections["EvoCosts"]
    if value.strip() == "[]":
        return []
    items: list[dict[str, Any]] = []
    current: dict[str, Any] | None = None
    for line in block:
        start = re.match(r"^\s{2}-\s*CardColor:\s*(.*)$", line)
        if start:
            if current is not None:
                items.append(current)
            current = {"colorRaw": nullable_int(normalize_scalar(start.group(1))), "level": None, "memoryCost": None}
            continue
        nested = re.match(r"^\s{4}(Level|MemoryCost):\s*(.*)$", line)
        if current is not None and nested:
            if nested.group(1) == "Level":
                current["level"] = nullable_int(normalize_scalar(nested.group(2)))
            else:
                current["memoryCost"] = nullable_int(normalize_scalar(nested.group(2)))
    if current is not None:
        items.append(current)

    result: list[dict[str, Any]] = []
    for item in items:
        color_raw = item["colorRaw"]
        color = CARD_COLOR_NAMES[color_raw] if isinstance(color_raw, int) and 0 <= color_raw < len(CARD_COLOR_NAMES) else None
        if color is None and color_raw is not None:
            color = f"Unknown({color_raw})"
        result.append(
            {
                "colorRaw": color_raw,
                "color": color,
                "level": item["level"],
                "memoryCost": item["memoryCost"],
            }
        )
    return result


def variant_key(asset_name: str | None) -> str:
    if asset_name:
        match = re.search(r"_(P[0-9]+)$", asset_name)
        if match:
            return match.group(1)
    return "base"


def set_id(card_id: str | None, asset_path: str) -> str | None:
    if card_id and "-" in card_id:
        return card_id.split("-", 1)[0]
    prefix = "DCGO/Assets/CardBaseEntity/"
    if asset_path.startswith(prefix):
        rest = asset_path[len(prefix) :]
        return rest.split("/", 1)[0]
    return None


def path_part(asset_path: str, index: int) -> str | None:
    prefix = "DCGO/Assets/CardBaseEntity/"
    if not asset_path.startswith(prefix):
        return None
    parts = asset_path[len(prefix) :].split("/")
    return parts[index] if 0 <= index < len(parts) else None


def keyword_candidates(texts: list[str | None]) -> list[str]:
    candidates: set[str] = set()
    for text in texts:
        if not text:
            continue
        normalized = text.replace("＜", "<").replace("＞", ">")
        for match in re.finditer(r"<([^<>]+)>", normalized):
            candidate = match.group(1).strip()
            if candidate:
                candidates.add(candidate)
        for keyword in KNOWN_KEYWORDS:
            if keyword in normalized:
                candidates.add(keyword)
    return sorted(candidates)


def add_anomaly(
    anomalies: list[dict[str, Any]],
    code: str,
    severity: str,
    asset_path: str,
    message: str,
    details: dict[str, Any] | None = None,
) -> None:
    anomalies.append(
        {
            "code": code,
            "severity": severity,
            "assetPath": asset_path,
            "message": message,
            "details": details,
        }
    )


def build_effect_source_map(workspace: Path) -> tuple[dict[str, list[str]], list[dict[str, Any]]]:
    source_map: dict[str, list[str]] = {}
    for source_file in sorted((workspace / "DCGO/Assets/Scripts/CardEffect").rglob("*.cs")):
        source_map.setdefault(source_file.stem, []).append(unix_path(source_file, workspace))
    duplicates = [
        {"effectClassName": name, "paths": paths}
        for name, paths in sorted(source_map.items())
        if len(paths) > 1
    ]
    return source_map, duplicates


def generate(workspace: Path) -> dict[str, Any]:
    lock = assert_source_lock(workspace)
    assert_current_source_fingerprint(workspace, lock)
    output_dir = workspace / "docs/generated"
    output_dir.mkdir(parents=True, exist_ok=True)

    effect_source_map, effect_source_duplicates = build_effect_source_map(workspace)
    anomalies: list[dict[str, Any]] = []
    records: list[dict[str, Any]] = []
    asset_files = sorted((workspace / "DCGO/Assets/CardBaseEntity").rglob("*.asset"))
    processed_asset_count = 0
    non_gameplay_asset_count = 0

    for asset_file in asset_files:
        processed_asset_count += 1
        asset_path = unix_path(asset_file, workspace)
        try:
            sections = parse_asset_sections(asset_file.read_text(encoding="utf-8-sig"))
            asset_name = scalar(sections, "m_Name")
            card_id = scalar(sections, "CardID")
            card_index = nullable_int(scalar(sections, "CardIndex"))

            if not card_id:
                non_gameplay_asset_count += 1
                add_anomaly(
                    anomalies,
                    "NonGameplayAssetNoCardId",
                    "info",
                    asset_path,
                    "CardID가 없어 gameplay card asset manifest record에서는 제외했습니다.",
                    {"assetName": asset_name, "cardIndex": card_index},
                )
                continue

            current_variant = variant_key(asset_name)
            card_effect_class_name = scalar(sections, "CardEffectClassName")
            source_paths = effect_source_map.get(card_effect_class_name or "", []) if card_effect_class_name else []

            if card_effect_class_name and not source_paths:
                add_anomaly(
                    anomalies,
                    "MissingCardEffectSourceBody",
                    "needs-review",
                    asset_path,
                    "CardEffectClassName에 대응하는 CardEffect source 파일을 찾지 못했습니다.",
                    {
                        "cardId": card_id,
                        "cardIndex": card_index,
                        "variantKey": current_variant,
                        "cardEffectClassName": card_effect_class_name,
                    },
                )

            if len(source_paths) > 1:
                add_anomaly(
                    anomalies,
                    "DuplicateCardEffectSourcePath",
                    "needs-review",
                    asset_path,
                    "CardEffectClassName에 대응하는 source 파일이 여러 개입니다.",
                    {
                        "cardId": card_id,
                        "cardIndex": card_index,
                        "variantKey": current_variant,
                        "cardEffectClassName": card_effect_class_name,
                        "sourcePaths": source_paths,
                    },
                )

            if card_index is None:
                add_anomaly(
                    anomalies,
                    "MissingOrInvalidCardIndex",
                    "error",
                    asset_path,
                    "CardIndex를 정수로 읽지 못했습니다.",
                    {"cardId": card_id, "assetName": asset_name},
                )

            forms = array_value(sections, "Form_ENG")
            attributes = array_value(sections, "Attribute_ENG")
            types = array_value(sections, "Type_ENG")
            traits = sorted({item for item in forms + attributes + types if item and item != "-"})
            effect_text = scalar(sections, "EffectDiscription_ENG")
            inherited_text = scalar(sections, "InheritedEffectDiscription_ENG")
            security_text = scalar(sections, "SecurityEffectDiscription_ENG")
            link_effect_text = scalar(sections, "LinkEffect")
            option_effect_text = scalar(sections, "OptionEffect")
            color_values = hex_int32_values(scalar(sections, "cardColors"))
            kind_values = hex_int32_values(scalar(sections, "cardKind"))
            rarity_raw = nullable_int(scalar(sections, "rarity"))
            rarity = RARITY_NAMES[rarity_raw] if isinstance(rarity_raw, int) and 0 <= rarity_raw < len(RARITY_NAMES) else None
            if rarity is None and rarity_raw is not None:
                rarity = f"Unknown({rarity_raw})"

            records.append(
                {
                    "definitionStableId": f"{card_id}#{card_index}@{current_variant}",
                    "cardId": card_id,
                    "cardIndex": card_index,
                    "variantKey": current_variant,
                    "assetName": asset_name,
                    "assetPath": asset_path,
                    "setId": set_id(card_id, asset_path),
                    "assetSetPath": path_part(asset_path, 0),
                    "assetColorPath": path_part(asset_path, 1),
                    "assetKindPath": path_part(asset_path, 2),
                    "colors": enum_values(color_values, CARD_COLOR_NAMES),
                    "rawCardColors": scalar(sections, "cardColors"),
                    "cardKinds": enum_values(kind_values, CARD_KIND_NAMES),
                    "rawCardKind": scalar(sections, "cardKind"),
                    "level": nullable_int(scalar(sections, "Level")),
                    "playCost": nullable_int(scalar(sections, "PlayCost")),
                    "dp": nullable_int(scalar(sections, "DP")),
                    "rarity": rarity,
                    "rarityRaw": rarity_raw,
                    "maxCountInDeck": nullable_int(scalar(sections, "MaxCountInDeck")),
                    "overflowMemory": nullable_int(scalar(sections, "OverflowMemory")),
                    "linkDP": nullable_int(scalar(sections, "LinkDP")),
                    "digivolutionRequirements": evo_costs(sections),
                    "forms": forms,
                    "attributes": attributes,
                    "types": types,
                    "traits": traits,
                    "keywords": keyword_candidates([effect_text, inherited_text, security_text, link_effect_text, option_effect_text]),
                    "cardEffectClassName": card_effect_class_name,
                    "sourceEffectPath": source_paths[0] if len(source_paths) == 1 else None,
                    "sourceEffectPaths": source_paths,
                    "sourceBodyExists": bool(source_paths),
                    "duplicateCardIdGroup": None,
                    "importError": None,
                }
            )
        except Exception as exc:  # noqa: BLE001 - inventory must record source parse failures.
            add_anomaly(
                anomalies,
                "AssetReadOrParseFailure",
                "error",
                asset_path,
                "asset import/read 중 예외가 발생했습니다.",
                {"exception": str(exc)},
            )

    records.sort(key=lambda item: (item["cardId"] or "", item["cardIndex"] if item["cardIndex"] is not None else -1, item["variantKey"], item["assetPath"]))

    duplicate_card_id_groups: list[dict[str, Any]] = []
    by_card_id: dict[str, list[dict[str, Any]]] = {}
    for record in records:
        by_card_id.setdefault(record["cardId"], []).append(record)
    for card_id, group_records in sorted(by_card_id.items()):
        if len(group_records) <= 1:
            continue
        group_records.sort(key=lambda item: (item["cardIndex"] if item["cardIndex"] is not None else -1, item["variantKey"], item["assetPath"]))
        group = {
            "cardId": card_id,
            "count": len(group_records),
            "cardIndices": [item["cardIndex"] for item in group_records],
            "variantKeys": [item["variantKey"] for item in group_records],
            "assetPaths": [item["assetPath"] for item in group_records],
        }
        duplicate_card_id_groups.append(group)
        for record in group_records:
            record["duplicateCardIdGroup"] = {
                "cardId": group["cardId"],
                "count": group["count"],
                "cardIndices": group["cardIndices"],
                "variantKeys": group["variantKeys"],
            }

    duplicate_card_index_groups: list[dict[str, Any]] = []
    by_card_index: dict[int, list[dict[str, Any]]] = {}
    for record in records:
        if record["cardIndex"] is not None:
            by_card_index.setdefault(record["cardIndex"], []).append(record)
    for card_index, group_records in sorted(by_card_index.items()):
        if len(group_records) <= 1:
            continue
        group_records.sort(key=lambda item: (item["cardId"], item["variantKey"], item["assetPath"]))
        group = {
            "cardIndex": card_index,
            "count": len(group_records),
            "cardIds": [item["cardId"] for item in group_records],
            "variantKeys": [item["variantKey"] for item in group_records],
            "assetPaths": [item["assetPath"] for item in group_records],
        }
        duplicate_card_index_groups.append(group)
        add_anomaly(anomalies, "DuplicateCardIndex", "error", group["assetPaths"][0], "CardIndex가 여러 gameplay card asset에서 중복됩니다.", group)

    definition_stable_id_duplicates: list[dict[str, Any]] = []
    by_stable_id: dict[str, list[dict[str, Any]]] = {}
    for record in records:
        by_stable_id.setdefault(record["definitionStableId"], []).append(record)
    for stable_id, group_records in sorted(by_stable_id.items()):
        if len(group_records) <= 1:
            continue
        group = {
            "definitionStableId": stable_id,
            "count": len(group_records),
            "assetPaths": sorted(item["assetPath"] for item in group_records),
        }
        definition_stable_id_duplicates.append(group)
        add_anomaly(anomalies, "DuplicateDefinitionStableId", "error", group["assetPaths"][0], "DefinitionStableId가 중복됩니다.", group)

    for duplicate in effect_source_duplicates:
        add_anomaly(
            anomalies,
            "DuplicateEffectSourceClassName",
            "needs-review",
            duplicate["paths"][0],
            "동일 CardEffect source class/file name이 여러 source path에서 발견됐습니다.",
            duplicate,
        )

    anomalies.sort(key=lambda item: (item["code"], item["severity"], item["assetPath"]))
    missing_source_count = sum(1 for item in anomalies if item["code"] == "MissingCardEffectSourceBody")
    parse_failure_count = sum(1 for item in anomalies if item["code"] == "AssetReadOrParseFailure")

    source_snapshot = {
        "kind": lock["selectedSnapshot"]["kind"],
        "path": lock["selectedSnapshot"]["path"],
        "fileManifest": lock["selectedSnapshot"]["fileManifest"],
        "fileManifestSha256": lock["selectedSnapshot"]["fileManifestSha256"],
        "fileCount": lock["selectedSnapshot"]["fileCount"],
        "approvedAt": lock.get("approvedAt"),
    }
    manifest = {
        "schemaVersion": 1,
        "sourceSnapshot": source_snapshot,
        "identityPolicy": {
            "definitionStableId": "CardId#CardIndex@VariantKey",
            "variantFlattening": "forbidden",
            "duplicateCardIdHandling": "preserve-variants-and-report-groups",
        },
        "sourceCoverage": {
            "cardBaseEntityAssetCount": len(asset_files),
            "processedAssetCount": processed_asset_count,
            "gameplayCardRecordCount": len(records),
            "nonGameplayAssetCount": non_gameplay_asset_count,
            "cardEffectSourceClassCount": len(effect_source_map),
        },
        "validation": {
            "allCardBaseEntityAssetsProcessed": len(asset_files) == processed_asset_count,
            "cardIndexDuplicateGroupCount": len(duplicate_card_index_groups),
            "definitionStableIdDuplicateGroupCount": len(definition_stable_id_duplicates),
            "duplicateCardIdGroupCount": len(duplicate_card_id_groups),
            "missingSourceBodyCount": missing_source_count,
            "parseFailureCount": parse_failure_count,
            "sourceBodyMissingIsSilentNoOp": False,
        },
        "duplicateCardIdGroups": duplicate_card_id_groups,
        "duplicateCardIndexGroups": duplicate_card_index_groups,
        "definitionStableIdDuplicateGroups": definition_stable_id_duplicates,
        "records": records,
    }

    manifest_path = output_dir / "full-card-pool-manifest.json"
    anomalies_path = output_dir / "full-card-pool-anomalies.json"
    summary_path = output_dir / "full-card-pool-summary.md"
    write_json(manifest_path, manifest)
    manifest_hash = file_sha256(manifest_path)

    anomaly_document = {
        "schemaVersion": 1,
        "sourceSnapshot": source_snapshot,
        "manifestPath": "docs/generated/full-card-pool-manifest.json",
        "manifestSha256": manifest_hash,
        "counts": {
            "total": len(anomalies),
            "missingSourceBody": missing_source_count,
            "parseFailure": parse_failure_count,
            "duplicateCardIndex": len(duplicate_card_index_groups),
            "duplicateDefinitionStableId": len(definition_stable_id_duplicates),
            "nonGameplayAsset": non_gameplay_asset_count,
        },
        "anomalies": anomalies,
    }
    write_json(anomalies_path, anomaly_document)
    anomalies_hash = file_sha256(anomalies_path)

    set_summary: list[tuple[str, int]] = []
    set_counts: dict[str, int] = {}
    for record in records:
        set_counts[record["setId"] or "Unknown"] = set_counts.get(record["setId"] or "Unknown", 0) + 1
    set_summary = sorted(set_counts.items(), key=lambda item: (-item[1], item[0]))[:20]

    kind_counts: dict[str, int] = {}
    color_counts: dict[str, int] = {}
    for record in records:
        for kind in record["cardKinds"]:
            kind_counts[kind["name"]] = kind_counts.get(kind["name"], 0) + 1
        for color in record["colors"]:
            color_counts[color["name"]] = color_counts.get(color["name"], 0) + 1
    kind_summary = sorted(kind_counts.items(), key=lambda item: (-item[1], item[0]))
    color_summary = sorted(color_counts.items(), key=lambda item: (-item[1], item[0]))

    summary_lines = [
        "# 전체 카드풀 Manifest 요약",
        "",
        f"- Source snapshot: `{source_snapshot['kind']}` / `{source_snapshot['fileManifest']}`",
        f"- Source manifest SHA-256: `{source_snapshot['fileManifestSha256']}`",
        f"- Manifest SHA-256: `{manifest_hash}`",
        f"- Anomalies SHA-256: `{anomalies_hash}`",
        f"- CardBaseEntity asset files processed: {processed_asset_count} / {len(asset_files)}",
        f"- Gameplay card records: {len(records)}",
        f"- Non-gameplay asset files excluded with anomaly: {non_gameplay_asset_count}",
        f"- Unique CardId count: {len(by_card_id)}",
        f"- Duplicate CardId groups: {len(duplicate_card_id_groups)}",
        f"- Duplicate CardIndex groups: {len(duplicate_card_index_groups)}",
        f"- Duplicate DefinitionStableId groups: {len(definition_stable_id_duplicates)}",
        f"- Missing CardEffect source body records: {missing_source_count}",
        f"- Parse/read failures: {parse_failure_count}",
        "",
        "## Identity Policy",
        "",
        "`DefinitionStableId`는 `CardId#CardIndex@VariantKey` 형식이다. 같은 `CardId`의 base/P1/P2 등 variant는 평탄화하지 않고 별도 record로 유지한다.",
        "",
        "## Top Sets",
        "",
    ]
    summary_lines.extend(f"- {name}: {count}" for name, count in set_summary)
    summary_lines.extend(["", "## Card Kinds", ""])
    summary_lines.extend(f"- {name}: {count}" for name, count in kind_summary)
    summary_lines.extend(["", "## Colors", ""])
    summary_lines.extend(f"- {name}: {count}" for name, count in color_summary)
    summary_lines.extend(
        [
            "",
            "## Validation Notes",
            "",
            "- 모든 `DCGO/Assets/CardBaseEntity/**/*.asset` 파일을 읽고 처리 수를 기록했다.",
            "- `CardEffectClassName`에 대응하는 source body가 없으면 silent no-op으로 숨기지 않고 anomalies에 `MissingCardEffectSourceBody`로 남긴다.",
            "- `CardId` 중복은 variant-aware identity 보존 대상으로 보고하며, `CardIndex`/`DefinitionStableId` 중복은 error anomaly로 보고한다.",
            "- manifest에는 효과 구현 또는 NoEffect 자동 대체가 포함되지 않는다.",
        ]
    )
    summary_path.write_text("\n".join(summary_lines) + "\n", encoding="utf-8")

    return {
        "manifestPath": "docs/generated/full-card-pool-manifest.json",
        "manifestSha256": manifest_hash,
        "anomaliesPath": "docs/generated/full-card-pool-anomalies.json",
        "anomaliesSha256": anomalies_hash,
        "summaryPath": "docs/generated/full-card-pool-summary.md",
        "gameplayCardRecords": len(records),
        "anomalies": len(anomalies),
    }


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--workspace", default=".")
    args = parser.parse_args()
    result = generate(Path(args.workspace).resolve())
    print(json.dumps(result, ensure_ascii=False, indent=2))


if __name__ == "__main__":
    main()
