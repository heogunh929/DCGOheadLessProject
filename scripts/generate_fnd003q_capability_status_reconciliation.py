# -*- coding: utf-8 -*-
"""Generate FND-003-Q capability status reconciliation artifacts.

This generator records a source-aligned reconciliation only. It does not
implement card effect bodies, run card-porting batches, promote generated card
statuses, or change Foundation Gate values by hand.
"""

from __future__ import annotations

import argparse
import hashlib
import json
from collections import Counter
from datetime import datetime, timezone
from pathlib import Path
from typing import Any


CAPABILITY_REGISTRY_PATH = Path("docs/generated/capability-truth-audit/capability-registry.json")
SOURCE_REQUIRED_PATH = Path("docs/generated/capability-truth-audit/source-required-capabilities.json")
FULL_MECHANIC_INVENTORY_PATH = Path("docs/generated/full-mechanic-inventory.json")
FOUNDATION_GATE_PATH = Path("docs/generated/foundation-completion-gate.json")
FND003A_QUEUE_PATH = Path("docs/generated/as-is-restart/fnd-003-a-foundation-goal-queue.json")

OUTPUT_JSON = Path("docs/generated/as-is-restart/fnd-003-q-capability-status-reconciliation.json")
OUTPUT_QUEUE_JSON = Path("docs/generated/as-is-restart/fnd-003-q-next-action-queue.json")
OUTPUT_DOC = Path("docs/as-is-restart/FND_003_Q_CAPABILITY_STATUS_RECONCILIATION.md")
OUTPUT_SUMMARY = Path("docs/as-is-restart/fnd-003-q-capability-status-reconciliation-summary.md")


TARGETS = [
    {
        "capabilityId": "AfterPayCost",
        "fndGoalId": "FND-003-C",
        "scopeDoc": "docs/rl-engine/after-pay-cost-foundation-scope-fnd003c.md",
        "completedScope": "standard play, option play, and digivolution post-cost trigger runtime scope",
        "latestVerification": "All 613 tests passed at FND-003-C; latest later full regression before this evidence-only reconciliation was All 640 tests passed.",
        "remainingBoundary": "full-card parity remains NotRun and card-specific cost/body variants are not promoted.",
    },
    {
        "capabilityId": "BeforePayCost",
        "fndGoalId": "FND-003-N",
        "scopeDoc": "docs/rl-engine/before-pay-cost-foundation-scope-fnd003n.md",
        "completedScope": "pre-cost play, option, and digivolution cost payment runtime scope",
        "latestVerification": "All 629 tests passed at FND-003-N; latest later full regression before this evidence-only reconciliation was All 640 tests passed.",
        "remainingBoundary": "full-card parity remains NotRun and replacement/cost-policy variants remain follow-up work.",
    },
    {
        "capabilityId": "OnDeclaration",
        "fndGoalId": "FND-003-P",
        "scopeDoc": "docs/rl-engine/on-declaration-foundation-scope-fnd003p.md",
        "completedScope": "legal action generation and selected declaration execution runtime scope",
        "latestVerification": "All 633 tests passed at FND-003-P; latest later full regression before this evidence-only reconciliation was All 640 tests passed.",
        "remainingBoundary": "full-card parity remains NotRun and declaration continuation/card-body variants remain follow-up work.",
    },
    {
        "capabilityId": "OnEndBattle",
        "fndGoalId": "FND-003-L",
        "scopeDoc": "docs/rl-engine/on-end-battle-foundation-scope-fnd003l.md",
        "completedScope": "battle-result runtime scope with selection pause/resume handling",
        "latestVerification": "All 623 tests passed at FND-003-L; latest later full regression before this evidence-only reconciliation was All 640 tests passed.",
        "remainingBoundary": "full-card parity remains NotRun and all battle lifecycle/card-body variants remain follow-up work.",
    },
    {
        "capabilityId": "OnMove",
        "fndGoalId": "FND-003-I",
        "scopeDoc": "docs/rl-engine/on-move-foundation-scope-fnd003i.md",
        "completedScope": "primitive permanent-move event runtime scope",
        "latestVerification": "All 619 tests passed at FND-003-I; latest later full regression before this evidence-only reconciliation was All 640 tests passed.",
        "remainingBoundary": "full-card parity remains NotRun and source-zone edge variants remain follow-up work.",
    },
]


def load_json(workspace: Path, path: Path) -> Any:
    return json.loads((workspace / path).read_text(encoding="utf-8-sig"))


def write_json(path: Path, value: Any) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(value, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def write_text(path: Path, text: str) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(text, encoding="utf-8")


def file_sha256(path: Path) -> str:
    digest = hashlib.sha256()
    with path.open("rb") as handle:
        for chunk in iter(lambda: handle.read(1024 * 1024), b""):
            digest.update(chunk)
    return digest.hexdigest()


def markdown_table(headers: list[str], rows: list[list[Any]]) -> str:
    lines = [
        "| " + " | ".join(headers) + " |",
        "| " + " | ".join("---" for _ in headers) + " |",
    ]
    for row in rows:
        lines.append("| " + " | ".join(str(value).replace("\n", " ") for value in row) + " |")
    return "\n".join(lines)


def capability_entry(registry: dict[str, Any], capability_id: str) -> dict[str, Any]:
    for entry in registry.get("capabilities", []):
        if entry.get("capabilityId") == capability_id:
            return entry
    return {}


def inventory_entry(inventory: dict[str, Any], capability_id: str) -> tuple[str, dict[str, Any]]:
    for section in ["timings", "features", "selections", "rootZones", "specialMechanics", "keywords"]:
        for entry in inventory.get(section, []):
            name = entry.get("name") or entry.get("id")
            if name == capability_id:
                return section, entry
    return "missing", {}


def source_required_stats(source_required: dict[str, Any], capability_id: str) -> dict[str, Any]:
    matches = [
        entry
        for entry in source_required.get("sourceEffects", [])
        if capability_id in entry.get("requiredCapabilities", [])
    ]
    affected_cards = {
        card.get("definitionStableId") or f"{card.get('cardId')}#{card.get('cardIndex')}"
        for entry in matches
        for card in entry.get("affectedCards", [])
    }
    source_paths = {entry.get("sourcePath") for entry in matches if entry.get("sourcePath")}
    samples = []
    for entry in matches[:3]:
        samples.append(
            {
                "sourceEffectClassName": entry.get("sourceEffectClassName"),
                "sourcePath": entry.get("sourcePath"),
                "affectedCardCount": entry.get("affectedCardCount", 0),
                "sampleCardData": entry.get("affectedCards", [])[:3],
            }
        )

    return {
        "sourceEffectCount": len(matches),
        "sourceFileCount": len(source_paths),
        "affectedCardCount": len(affected_cards),
        "sourceEffectSamples": samples,
    }


def completed_goal_ids(queue: dict[str, Any]) -> set[str]:
    progress = queue.get("progress", {})
    return set(progress.get("completedParentGoalIds", []))


def gate_capability_ids(gate: dict[str, Any], sample_key: str) -> list[str]:
    return [
        item.get("capabilityId")
        for item in gate.get("samples", {}).get(sample_key, [])
        if item.get("capabilityId")
    ]


def build(workspace: Path) -> tuple[dict[str, Any], dict[str, Any], str, str]:
    registry = load_json(workspace, CAPABILITY_REGISTRY_PATH)
    source_required = load_json(workspace, SOURCE_REQUIRED_PATH)
    inventory = load_json(workspace, FULL_MECHANIC_INVENTORY_PATH)
    gate = load_json(workspace, FOUNDATION_GATE_PATH)
    fnd003a_queue = load_json(workspace, FND003A_QUEUE_PATH)
    completed = completed_goal_ids(fnd003a_queue)
    generated_at = datetime.now(timezone.utc).isoformat()

    entries = []
    for target in TARGETS:
        capability_id = target["capabilityId"]
        registry_item = capability_entry(registry, capability_id)
        section, inventory_item = inventory_entry(inventory, capability_id)
        evidence = inventory_item.get("evidence", {})
        source_stats = source_required_stats(source_required, capability_id)
        fnd_goal_id = target["fndGoalId"]
        reconciled_status = registry_item.get("status", "Unknown")
        entries.append(
            {
                "capabilityId": capability_id,
                "fndGoalId": fnd_goal_id,
                "fndGoalCompleted": fnd_goal_id in completed,
                "previousAuditStatus": "Unsupported",
                "inventorySection": section,
                "inventoryStatus": registry_item.get("inventoryStatus") or inventory_item.get("mappingStatus"),
                "reconciledCapabilityStatus": reconciled_status,
                "reconciliationClassification": (
                    "SourceAlignedPartialFoundation"
                    if reconciled_status == "PartiallyImplemented"
                    else "ManualReview"
                ),
                "affectedCardCount": registry_item.get("affectedCardCount", source_stats["affectedCardCount"]),
                "sourceFileCount": registry_item.get("sourceFileCount", source_stats["sourceFileCount"]),
                "sourceRequired": source_stats,
                "inventoryEvidence": {
                    "mappingStatus": inventory_item.get("mappingStatus"),
                    "cardEffectSourceFileCount": inventory_item.get("cardEffectSourceFileCount"),
                    "cardEffectOccurrenceCount": inventory_item.get("cardEffectOccurrenceCount"),
                    "sourceScopeFileCount": inventory_item.get("sourceScopeFileCount"),
                    "sourceScopeOccurrenceCount": inventory_item.get("sourceScopeOccurrenceCount"),
                    "engineSourceReferenceCount": evidence.get("engineSourceReferenceCount", 0),
                    "engineTestReferenceCount": evidence.get("engineTestReferenceCount", 0),
                    "sampleSourcePaths": evidence.get("sampleSourcePaths", []),
                    "notes": evidence.get("notes", []),
                },
                "capabilityRegistryReason": registry_item.get("reason", ""),
                "implementationEvidence": registry_item.get("implementationEvidence", []),
                "testEvidence": registry_item.get("testEvidence", []),
                "scopeDoc": target["scopeDoc"],
                "completedScope": target["completedScope"],
                "latestVerification": target["latestVerification"],
                "remainingBoundary": target["remainingBoundary"],
                "fullCardParityStatus": "NotRun",
                "verifiedPromotionAllowed": False,
                "generatedCardStatusPromotionAllowed": False,
            }
        )

    classification_counts = Counter(entry["reconciliationClassification"] for entry in entries)
    status_counts = Counter(entry["reconciledCapabilityStatus"] for entry in entries)
    gate_summary = gate["summary"]
    result = {
        "schemaVersion": "dcgo.as-is-restart.fnd003q-capability-status-reconciliation.v1",
        "generatedAt": generated_at,
        "goal": "FND-003-Q",
        "sourceRoot": "E:/headlessDCGO/DCGO/Assets",
        "inputs": {
            "capabilityRegistry": CAPABILITY_REGISTRY_PATH.as_posix(),
            "sourceRequiredCapabilities": SOURCE_REQUIRED_PATH.as_posix(),
            "fullMechanicInventory": FULL_MECHANIC_INVENTORY_PATH.as_posix(),
            "foundationGate": FOUNDATION_GATE_PATH.as_posix(),
            "fnd003AQueue": FND003A_QUEUE_PATH.as_posix(),
        },
        "policy": {
            "implementationPerformed": False,
            "srcImplementationModified": False,
            "dcgoOriginalModified": False,
            "cardEffectBodyImplemented": False,
            "c0039OrLaterCardPortingRun": False,
            "rlComponentsImplemented": False,
            "generatedCardStatusPromoted": False,
            "verifiedPromotionPerformed": False,
            "foundationGateDirectlyManipulated": False,
            "commitOrPushPerformed": False,
            "rawJsonOver100MiBAdded": False,
        },
        "summary": {
            "targetCount": len(entries),
            "classificationCounts": dict(sorted(classification_counts.items())),
            "reconciledStatusCounts": dict(sorted(status_counts.items())),
            "previousUnsupportedCount": len(entries),
            "currentUnsupportedAmongTargets": sum(1 for entry in entries if entry["reconciledCapabilityStatus"] == "Unsupported"),
            "currentPartiallyImplementedAmongTargets": sum(
                1 for entry in entries if entry["reconciledCapabilityStatus"] == "PartiallyImplemented"
            ),
            "allFndGoalsCompleted": all(entry["fndGoalCompleted"] for entry in entries),
            "foundationGateOpenCodeReady": gate_summary["openCodeReady"],
            "foundationGateUnsupportedCapabilityCount": gate_summary["unsupportedCapabilityCount"],
            "foundationGatePartiallyImplementedCapabilityCount": gate_summary["partiallyImplementedCapabilityCount"],
            "foundationGateFailedGateCount": gate_summary["failedGateCount"],
            "inventorySha256": file_sha256(workspace / FULL_MECHANIC_INVENTORY_PATH),
            "capabilityRegistrySha256": file_sha256(workspace / CAPABILITY_REGISTRY_PATH),
            "sourceRequiredCapabilitiesSha256": file_sha256(workspace / SOURCE_REQUIRED_PATH),
        },
        "entries": entries,
    }

    remaining_unsupported = gate.get("samples", {}).get("unsupportedCapabilities", [])
    queue = {
        "schemaVersion": "dcgo.as-is-restart.fnd003q-next-action-queue.v1",
        "generatedAt": generated_at,
        "goal": "FND-003-Q",
        "reconciliation": OUTPUT_JSON.as_posix(),
        "summary": {
            "closedUnsupportedByReconciliationCount": result["summary"]["currentPartiallyImplementedAmongTargets"],
            "remainingUnsupportedCapabilityCount": gate_summary["unsupportedCapabilityCount"],
            "remainingPartiallyImplementedCapabilityCount": gate_summary["partiallyImplementedCapabilityCount"],
            "openCodeReady": gate_summary["openCodeReady"],
        },
        "reconciledItems": entries,
        "remainingUnsupportedCapabilityIds": gate_capability_ids(gate, "unsupportedCapabilities"),
        "remainingUnsupportedCapabilities": remaining_unsupported,
        "recommendedNextQueueGroups": [
            {
                "taskId": "FND-003-R",
                "status": "Pending",
                "capabilityIds": [
                    "WhenPermanentWouldBeDeleted",
                    "WhenRemoveField",
                    "WhenReturntoLibraryAnyone",
                    "WhenUntapAnyone",
                    "WhenWouldDigivolutionCardDiscarded",
                ],
                "reason": "Replacement/cut-in timings are still source-mapped Unsupported and require runtime foundation decomposition.",
            },
            {
                "taskId": "FND-003-S",
                "status": "Pending",
                "capabilityIds": ["WhenWouldLink", "WhenLinked", "OnLinkCardDiscarded"],
                "reason": "Link lifecycle timings are still source-mapped Unsupported and require runtime foundation decomposition.",
            },
            {
                "taskId": "FND-003-T",
                "status": "Pending",
                "capabilityIds": ["WhenDigisorption", "OnUseDigiburst"],
                "reason": "Digisorption/DigiBurst lifecycle timings are still source-mapped Unsupported and require data/runtime policy work.",
            },
            {
                "taskId": "FND-001-CONTINUE",
                "status": "Pending",
                "capabilityIds": ["ContinuousOrStaticEffect"],
                "reason": "Partial capability count remains non-zero and the Gate still selects ContinuousOrStaticEffect as next foundation capability.",
            },
            {
                "taskId": "PARITY-001-A",
                "status": "Pending",
                "capabilityIds": [],
                "reason": "Full-card parity remains NotRun and is still required before any Verified/generated status promotion.",
            },
        ],
    }

    return result, queue, render_doc(result, queue), render_summary(result, queue)


def render_doc(result: dict[str, Any], queue: dict[str, Any]) -> str:
    summary = result["summary"]
    rows = []
    for entry in result["entries"]:
        rows.append(
            [
                entry["capabilityId"],
                entry["fndGoalId"],
                entry["previousAuditStatus"],
                entry["inventoryStatus"],
                entry["reconciledCapabilityStatus"],
                entry["sourceRequired"]["affectedCardCount"],
                entry["sourceRequired"]["sourceFileCount"],
                entry["inventoryEvidence"]["engineSourceReferenceCount"],
                entry["inventoryEvidence"]["engineTestReferenceCount"],
            ]
        )

    boundary_rows = [
        [entry["capabilityId"], entry["remainingBoundary"]]
        for entry in result["entries"]
    ]
    sample_rows = []
    for entry in result["entries"]:
        for sample in entry["sourceRequired"]["sourceEffectSamples"]:
            sample_rows.append(
                [
                    entry["capabilityId"],
                    sample.get("sourceEffectClassName"),
                    sample.get("sourcePath"),
                    sample.get("affectedCardCount"),
                ]
            )

    lines = [
        "# FND-003-Q Capability Status Reconciliation",
        "",
        "이번 문서는 FND-003에서 이미 runtime foundation과 테스트 근거가 있는 capability가 capability truth audit에서 계속 `Unsupported`로 남아 있던 상태를 source-aligned 기준으로 재조정한다. 구현은 수행하지 않는다.",
        "",
        "## 기준선",
        "",
        f"- Source root: `{result['sourceRoot']}`",
        f"- Foundation Gate OpenCodeReady: `{summary['foundationGateOpenCodeReady']}`",
        f"- Gate unsupported capability count: `{summary['foundationGateUnsupportedCapabilityCount']}`",
        f"- Gate partially implemented capability count: `{summary['foundationGatePartiallyImplementedCapabilityCount']}`",
        f"- Reconciled target count: `{summary['targetCount']}`",
        f"- Classification counts: `{summary['classificationCounts']}`",
        "",
        "## Reconciled Capability",
        "",
        markdown_table(
            [
                "Capability",
                "FND goal",
                "Previous audit",
                "Inventory",
                "Reconciled",
                "Affected cards",
                "Source files",
                "Engine refs",
                "Test refs",
            ],
            rows,
        ),
        "",
        "## Source Samples",
        "",
        markdown_table(
            ["Capability", "Source class", "Source path", "Affected cards"],
            sample_rows,
        ),
        "",
        "## Remaining Boundary",
        "",
        markdown_table(["Capability", "Boundary"], boundary_rows),
        "",
        "## Next Queue",
        "",
        f"- Remaining unsupported capabilities: `{queue['summary']['remainingUnsupportedCapabilityCount']}`",
        f"- Remaining partially implemented capabilities: `{queue['summary']['remainingPartiallyImplementedCapabilityCount']}`",
        "- Recommended next groups:",
    ]
    for item in queue["recommendedNextQueueGroups"]:
        lines.append(f"  - `{item['taskId']}`: {', '.join(item['capabilityIds']) or 'non-capability parity work'}")

    lines.extend(
        [
            "",
            "## 금지 사항 준수",
            "",
            "- `src/DCGO.RL.Engine` 구현 코드 수정 없음.",
            "- 원본 `DCGO/Assets` 수정 없음.",
            "- 개별 `CardEffect` body 구현 없음.",
            "- C0039 이후 card-porting 실행 없음.",
            "- RL Environment/Observation/Reward/Dataset/Trainer 구현 없음.",
            "- generated card status 또는 Verified 승격 없음.",
            "- Foundation Gate 수치 수동 조작 없음.",
            "- commit/push 없음.",
            "",
        ]
    )
    return "\n".join(lines)


def render_summary(result: dict[str, Any], queue: dict[str, Any]) -> str:
    summary = result["summary"]
    lines = [
        "# FND-003-Q Capability Status Reconciliation Summary",
        "",
        f"- Reconciled targets: `{summary['targetCount']}`",
        f"- Previous Unsupported among targets: `{summary['previousUnsupportedCount']}`",
        f"- Current PartiallyImplemented among targets: `{summary['currentPartiallyImplementedAmongTargets']}`",
        f"- Current Unsupported among targets: `{summary['currentUnsupportedAmongTargets']}`",
        f"- Remaining Gate Unsupported capability count: `{summary['foundationGateUnsupportedCapabilityCount']}`",
        f"- Remaining Gate PartiallyImplemented capability count: `{summary['foundationGatePartiallyImplementedCapabilityCount']}`",
        f"- OpenCodeReady: `{summary['foundationGateOpenCodeReady']}`",
        "",
        "## Targets",
        "",
    ]
    for entry in result["entries"]:
        lines.append(
            f"- `{entry['capabilityId']}` -> `{entry['reconciledCapabilityStatus']}` via `{entry['fndGoalId']}`; boundary: {entry['remainingBoundary']}"
        )
    lines.extend(
        [
            "",
            "## Next",
            "",
            "- Continue FND-003 for remaining replacement/cut-in, link lifecycle, and Digisorption/DigiBurst unsupported capabilities.",
            "- Continue FND-001 partial closure and PARITY-001-A fixture work before any card-porting or generated status promotion.",
            "",
        ]
    )
    return "\n".join(lines)


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace", default=".", help="Repository/workspace root.")
    args = parser.parse_args()

    workspace = Path(args.workspace).resolve()
    result, queue, doc, summary = build(workspace)
    write_json(workspace / OUTPUT_JSON, result)
    write_json(workspace / OUTPUT_QUEUE_JSON, queue)
    write_text(workspace / OUTPUT_DOC, doc)
    write_text(workspace / OUTPUT_SUMMARY, summary)
    print(f"Wrote {OUTPUT_JSON.as_posix()}")
    print(f"Wrote {OUTPUT_QUEUE_JSON.as_posix()}")
    print(f"Wrote {OUTPUT_DOC.as_posix()}")
    print(f"Wrote {OUTPUT_SUMMARY.as_posix()}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
