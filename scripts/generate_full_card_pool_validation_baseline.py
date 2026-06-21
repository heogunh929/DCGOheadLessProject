# -*- coding: utf-8 -*-
"""Generate queue 65 full-card-pool validation baseline reports.

The expected result is blocked. This script turns the queue 64 scaffold into a
machine-readable blocker report without implementing or guessing card effects.
"""

from __future__ import annotations

import argparse
import json
import sys
from collections import Counter
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


SCHEMA_VERSION = "dcgo.full-card-pool-validation-baseline.65.v1"
INDEX_PATH = "docs/generated/full-card-source-scaffold/index.json"
OUTPUT_JSON = "docs/generated/full-card-pool-validation-baseline-65.json"
OUTPUT_MD = "docs/rl-engine/full-card-pool-validation-baseline-65.md"

ALLOWED_STATUSES = {
    "NoEffect",
    "Implemented",
    "Verified",
    "Unsupported",
    "PartiallyImplemented",
    "StubbedForValidation",
    "NeedsSourceReview",
    "UnknownVariant",
}

BLOCKING_STATUSES = {
    "Unsupported",
    "PartiallyImplemented",
    "StubbedForValidation",
    "NeedsSourceReview",
    "UnknownVariant",
}


def read_json(path: Path) -> Any:
    with path.open("r", encoding="utf-8-sig") as handle:
        return json.load(handle)


def write_json(path: Path, value: Any) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(
        json.dumps(value, ensure_ascii=False, indent=2, sort_keys=False) + "\n",
        encoding="utf-8",
    )


def count(values: list[str]) -> dict[str, int]:
    return dict(sorted(Counter(value for value in values if value).items()))


def load_records(workspace: Path, relative_paths: list[str]) -> list[dict[str, Any]]:
    records: list[dict[str, Any]] = []
    for relative_path in relative_paths:
        document = read_json(workspace / relative_path)
        records.extend(document["records"])
    return records


def issue(
    category: str,
    code: str,
    message: str,
    *,
    definition_stable_id: str | None = None,
    card_id: str | None = None,
    set_id: str | None = None,
    status: str | None = None,
    evidence: list[str] | None = None,
) -> dict[str, Any]:
    return {
        "category": category,
        "code": code,
        "severity": "Blocking",
        "message": message,
        "definitionStableId": definition_stable_id,
        "cardId": card_id,
        "setId": set_id,
        "status": status,
        "evidence": evidence or [],
    }


def card_issue(category: str, code: str, card: dict[str, Any], message: str) -> dict[str, Any]:
    return issue(
        category,
        code,
        message,
        definition_stable_id=card["definitionStableId"],
        card_id=card["cardId"],
        set_id=card["setId"],
        status=card["mappingStatus"],
        evidence=[card.get("statusReason", "")],
    )


def build_card_issues(cards: list[dict[str, Any]], source_ids: set[str]) -> list[dict[str, Any]]:
    issues: list[dict[str, Any]] = []
    seen: set[str] = set()
    for card in cards:
        definition_id = card["definitionStableId"]
        status = card["mappingStatus"]
        effect_class = card.get("cardEffectClassName") or ""
        source_paths = card.get("sourceEffectPaths") or []

        if definition_id in seen:
            issues.append(card_issue("StructureViolation", "DuplicateDefinitionStableId", card, f"Duplicate definitionStableId: {definition_id}."))
        seen.add(definition_id)

        if "#" not in definition_id or "@" not in definition_id:
            issues.append(card_issue("UnknownVariant", "UnknownVariantIdentity", card, f"Card mapping identity is not variant-aware: {definition_id}."))

        if status not in ALLOWED_STATUSES:
            issues.append(card_issue("StructureViolation", "UnknownStatus", card, f"Unknown card mapping status: {status}."))

        if status == "NoEffect" and effect_class:
            issues.append(card_issue("FalseNoEffect", "FalseNoEffect", card, "NoEffect is only allowed for empty CardEffectClassName."))

        if effect_class and not card.get("sourceBodyExists"):
            issues.append(card_issue("MissingSourceBody", "MissingSourceBody", card, "CardEffectClassName exists but no matching source body was found."))

        if effect_class and card.get("sourceBodyExists") and len(source_paths) != 1:
            issues.append(card_issue("SourceMappingMissing", "AmbiguousSourceBody", card, "CardEffectClassName must resolve to exactly one source body."))

        for source_ref in card.get("sourceEffectScaffoldRefs") or []:
            if source_ref not in source_ids:
                issues.append(card_issue("SourceMappingMissing", "MissingSourceScaffoldRef", card, f"Missing source scaffold ref: {source_ref}."))

        if status in BLOCKING_STATUSES:
            issues.append(card_issue("UnsupportedOrPartial", f"Status{status}", card, f"Card is blocked by baseline status: {status}."))
    return issues


def build_source_issues(sources: list[dict[str, Any]]) -> list[dict[str, Any]]:
    issues: list[dict[str, Any]] = []
    seen: set[str] = set()
    for source in sources:
        source_id = source["sourceScaffoldId"]
        status = source["scaffoldStatus"]
        if source_id in seen:
            issues.append(issue("StructureViolation", "DuplicateSourceScaffoldId", f"Duplicate source scaffold id: {source_id}.", status=status, evidence=[source_id]))
        seen.add(source_id)
        if status not in ALLOWED_STATUSES:
            issues.append(issue("StructureViolation", "UnknownSourceStatus", f"Unknown source scaffold status: {status}.", status=status, evidence=[source_id]))
        if status in BLOCKING_STATUSES:
            issues.append(issue("UnsupportedOrPartial", f"SourceStatus{status}", f"Source scaffold is blocked by baseline status: {status}.", status=status, evidence=[source_id, source["sourcePath"]]))
    return issues


def build_mechanic_issues(inventory: dict[str, Any]) -> list[dict[str, Any]]:
    issues: list[dict[str, Any]] = []
    for section in ["timings", "features", "selections", "specialMechanics", "keywords"]:
        for item in inventory.get(section, []):
            status = item.get("mappingStatus")
            name = item.get("name")
            if status not in ALLOWED_STATUSES and status != "NotReferenced":
                issues.append(issue("StructureViolation", "UnknownMechanicStatus", f"Unknown mechanic status: {section}/{name}={status}.", status=status, evidence=[section, name]))
            if status in {"Unsupported", "PartiallyImplemented", "NeedsSourceReview"}:
                issues.append(issue("UnsupportedMechanic", f"Mechanic{status}", f"Mechanic inventory item blocks full-card-pool completion: {section}/{name}={status}.", status=status, evidence=[section, name]))
    return issues


def validate_deck(name: str, ids: list[str], card_by_id: dict[str, dict[str, Any]]) -> dict[str, Any]:
    issues: list[dict[str, Any]] = []
    known: list[dict[str, Any]] = []
    for definition_id in ids:
        card = card_by_id.get(definition_id)
        if card is None:
            issues.append(issue("DeckCardMissing", "DeckCardMissing", f"Deck references unknown definitionStableId: {definition_id}.", definition_stable_id=definition_id, evidence=[name]))
            continue
        known.append(card)
        if card["mappingStatus"] in BLOCKING_STATUSES:
            issues.append(card_issue("DeckCardBlocked", "DeckCardBlocked", card, f"Deck card is blocked by baseline status: {card['mappingStatus']}."))
    return {
        "name": name,
        "requestedCardCount": len(ids),
        "knownCardCount": len(known),
        "decision": "Passed" if not issues else "Blocked",
        "statusCounts": count([card["mappingStatus"] for card in known]),
        "issues": issues,
    }


def build_report(workspace: Path) -> dict[str, Any]:
    lock = assert_source_lock(workspace)
    assert_current_source_fingerprint(workspace, lock)

    index = read_json(workspace / INDEX_PATH)
    generated = index["generatedFiles"]
    cards = load_records(workspace, generated["cardMappings"])
    sources = load_records(workspace, generated["sourceScaffolds"])
    catalogs = [read_json(workspace / path) for path in generated["setCatalogs"]]
    inventory = read_json(workspace / index["inputManifests"]["fullMechanicInventory"]["path"])

    source_ids = {source["sourceScaffoldId"] for source in sources}
    issues = []
    issues.extend(build_card_issues(cards, source_ids))
    issues.extend(build_source_issues(sources))
    issues.extend(build_mechanic_issues(inventory))

    for catalog in catalogs:
        if not catalog.get("registryOnly"):
            issues.append(issue("RegistryMissing", "CatalogNotRegistryOnly", f"Set catalog is not registry-only: {catalog.get('setId')}.", set_id=catalog.get("setId")))
        if catalog.get("cardMappingRecordCount", 0) == 0 and catalog.get("sourceScaffoldRecordCount", 0) == 0:
            issues.append(issue("RegistryMissing", "EmptySetCatalog", f"Set catalog has no card/source records: {catalog.get('setId')}.", set_id=catalog.get("setId")))

    card_by_id = {card["definitionStableId"]: card for card in cards}
    deck_validations = [
        validate_deck("no-effect subset", ["BT1-009#143@base"], card_by_id),
        validate_deck("blocked subset", ["AD1-001#7817@base", "ST3-02#4977@P2"], card_by_id),
        validate_deck("missing subset", ["NOPE-000#0@base"], card_by_id),
    ]

    mechanics = {
        "timings": count([timing for source in sources for timing in source.get("timings", [])]),
        "flags": count([name for source in sources for name, enabled in source.get("flags", {}).items() if enabled]),
        "selectionKinds": count([kind for source in sources for kind in source.get("selectionKinds", [])]),
        "rootZones": count([zone for source in sources for zone in source.get("rootZones", [])]),
    }

    report = {
        "schemaVersion": SCHEMA_VERSION,
        "targetName": "Full DCGO card pool validation baseline",
        "decision": "Blocked" if issues else "Passed",
        "scope": "Queue 65 establishes a full-card-pool validation baseline. Blocked is expected until every source-bearing card is ported or reviewed.",
        "inputManifests": index["inputManifests"],
        "manifestRecordCount": index["summary"]["manifestRecordCount"],
        "cardMappingRecordCount": len(cards),
        "sourceScaffoldRecordCount": len(sources),
        "setCatalogCount": len(catalogs),
        "statusCounts": count([card["mappingStatus"] for card in cards]),
        "setCounts": count([card["setId"] for card in cards]),
        "colorCounts": count([card["assetColorPath"] for card in cards]),
        "mechanicCounts": mechanics,
        "blockingIssueCounts": count([item["category"] for item in issues]),
        "blockingIssueTotal": len(issues),
        "blockingIssues": issues,
        "deckValidations": deck_validations,
    }
    return report


def write_markdown(workspace: Path, report: dict[str, Any]) -> None:
    lines = [
        "# Full Card Pool Validation Baseline - Queue 65",
        "",
        "이 문서는 64번 scaffold를 전체 카드풀 completion gate의 입력으로 검증한 baseline이다. 현재 결과는 `Blocked`가 정상이며, 이는 미구현 카드 효과를 silent no-op으로 숨기지 않는다는 뜻이다.",
        "",
        "## Summary",
        "",
        f"- Decision: `{report['decision']}`",
        f"- Card mapping records: {report['cardMappingRecordCount']} / {report['manifestRecordCount']}",
        f"- Source scaffold records: {report['sourceScaffoldRecordCount']}",
        f"- Set catalogs: {report['setCatalogCount']}",
        f"- Blocking issues: {report['blockingIssueTotal']}",
        "",
        "## Status Counts",
        "",
    ]
    for status, value in report["statusCounts"].items():
        lines.append(f"- `{status}`: {value}")
    lines.extend([
        "",
        "## Blocking Issue Counts",
        "",
    ])
    for category, value in report["blockingIssueCounts"].items():
        lines.append(f"- `{category}`: {value}")
    lines.extend([
        "",
        "## Deck Validation API Smoke",
        "",
    ])
    for deck in report["deckValidations"]:
        lines.append(f"- `{deck['name']}`: `{deck['decision']}` ({deck['knownCardCount']}/{deck['requestedCardCount']})")
    lines.extend([
        "",
        "## Policy",
        "",
        "- `NoEffect`는 원본 asset의 `CardEffectClassName`이 비어 있는 경우에만 허용한다.",
        "- `Unsupported`, `PartiallyImplemented`, `StubbedForValidation`, `NeedsSourceReview`, `UnknownVariant`는 full snapshot completion blocker다.",
        "- mechanic inventory의 `Unsupported`, `PartiallyImplemented`, `NeedsSourceReview`도 blocker다.",
        "- 이 단계는 effect body를 구현하지 않으며, RL Environment/Observation/Reward/Dataset/Trainer를 만들지 않는다.",
        "",
        f"Machine-readable report: `{OUTPUT_JSON}`",
    ])
    (workspace / OUTPUT_MD).write_text("\n".join(lines) + "\n", encoding="utf-8")


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--workspace", default=".", help="Workspace root")
    args = parser.parse_args()

    workspace = Path(args.workspace).resolve()
    report = build_report(workspace)
    write_json(workspace / OUTPUT_JSON, report)
    write_markdown(workspace, report)
    print(json.dumps({
        "decision": report["decision"],
        "cardMappingRecordCount": report["cardMappingRecordCount"],
        "sourceScaffoldRecordCount": report["sourceScaffoldRecordCount"],
        "blockingIssueTotal": report["blockingIssueTotal"],
        "statusCounts": report["statusCounts"],
        "blockingIssueCounts": report["blockingIssueCounts"],
    }, ensure_ascii=False, indent=2))


if __name__ == "__main__":
    main()
