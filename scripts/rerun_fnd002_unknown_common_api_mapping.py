# -*- coding: utf-8 -*-
"""Refresh FND-002 unknown common API mapping from current generated evidence.

This script does not implement card effects and does not promote generated card
status. It documents how the current full-mechanic inventory maps the original
FND-002 NeedsSourceReview timing set after foundation evidence has been added.
"""

from __future__ import annotations

import argparse
import hashlib
import json
from collections import Counter
from datetime import datetime, timezone
from pathlib import Path
from typing import Any


ORIGINAL_FND002_PATH = Path("docs/generated/as-is-restart/fnd-002-unknown-common-api-mapping.json")
INVENTORY_PATH = Path("docs/generated/full-mechanic-inventory.json")
GATE_PATH = Path("docs/generated/foundation-completion-gate.json")

OUTPUT_JSON = Path("docs/generated/as-is-restart/fnd-002-rerun-unknown-common-api-mapping.json")
OUTPUT_QUEUE_JSON = Path("docs/generated/as-is-restart/fnd-002-rerun-next-action-queue.json")
OUTPUT_DOC = Path("docs/as-is-restart/FND_002_RERUN_UNKNOWN_COMMON_API_SOURCE_MAPPING.md")
OUTPUT_SUMMARY = Path("docs/as-is-restart/fnd-002-rerun-unknown-common-api-mapping-summary.md")

FND003_PARTIAL_TASKS = {
    "OnRemovedField": "FND-003-B",
    "AfterPayCost": "FND-003-C",
    "OnDiscardSecurity": "FND-003-D",
    "OnAddSecurity": "FND-003-E",
    "OnDiscardLibrary": "FND-003-F",
    "OnUseOption": "FND-003-G",
    "OnUnTappedAnyone": "FND-003-H",
    "OnMove": "FND-003-I",
    "OnAddDigivolutionCards": "FND-003-J",
    "OnDigivolutionCardDiscarded": "FND-003-K",
    "OnEndBattle": "FND-003-L",
    "OnDetermineDoSecurityCheck": "FND-003-M",
    "BeforePayCost": "FND-003-N",
    "OnTappedAnyone": "FND-003-O",
    "OnDeclaration": "FND-003-P",
}

FND002_DATA_POLICY_PARTIAL_TASKS = {
    "OnFaceUpSecurityIncreased": "FND-002-D",
}

FND002_ZERO_CARD_REFERENCE_TASKS = {
    "OnStartBattle": "FND-002-E",
}

REMAINING_CLASSIFICATION = {
    "WhenPermanentWouldBeDeleted": "NeedsCapabilityRemediation",
    "WhenRemoveField": "NeedsCapabilityRemediation",
    "WhenLinked": "NeedsSourceMapping",
    "WhenReturntoLibraryAnyone": "NeedsCapabilityRemediation",
    "WhenDigisorption": "NeedsCapabilityRemediation",
    "OnLinkCardDiscarded": "NeedsSourceMapping",
    "WhenWouldLink": "NeedsSourceMapping",
    "OnFaceUpSecurityIncreased": "NeedsDataPolicy",
    "OnUseDigiburst": "CardEffectBodySpecific",
    "WhenUntapAnyone": "NeedsCapabilityRemediation",
    "WhenWouldDigivolutionCardDiscarded": "NeedsCapabilityRemediation",
    "OnStartBattle": "SourceKnownZeroCardTiming",
}

REMAINING_NEXT_ACTION = {
    "WhenPermanentWouldBeDeleted": "replacement/delete-prevention cut-in payload foundation 작업으로 분리",
    "WhenRemoveField": "would-remove cut-in과 actual removal event의 source ordering 재분해",
    "WhenLinked": "link lifecycle source mapping과 CanTriggerWhenLinked 조건 범위 확정",
    "WhenReturntoLibraryAnyone": "return-to-library would-remove/replacement payload foundation 작업으로 분리",
    "WhenDigisorption": "BeforePayCost cost-reduction body와 WhenDigisorption 후속 trigger를 별도 cost mechanic 작업으로 분리",
    "OnLinkCardDiscarded": "link source discard lifecycle와 payload source mapping 확정",
    "WhenWouldLink": "would-link cut-in/replacement boundary source mapping 확정",
    "OnFaceUpSecurityIncreased": "face-up security data/state policy와 affected-card fixture 범위 확정",
    "OnUseDigiburst": "DigiBurst mechanic body와 common timing boundary를 분리할 수 있는지 재검토",
    "WhenUntapAnyone": "would-untap cut-in, 대상 재고정, actual unsuspend ordering을 별도 foundation 작업으로 분리",
    "WhenWouldDigivolutionCardDiscarded": "would-source-trash replacement/cut-in payload foundation 작업으로 분리",
    "OnStartBattle": "Current AS-IS CardEffect references are zero; exclude this source-known battle-start timing from current full-card-pool blockers and reopen payload fixture work only if a future source card references it.",
}


def load_json(workspace: Path, path: Path) -> Any:
    return json.loads((workspace / path).read_text(encoding="utf-8-sig"))


def write_json(path: Path, value: Any) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(value, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


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


def classify_entry(name: str, current_status: str) -> str:
    if name in FND003_PARTIAL_TASKS and current_status == "PartiallyImplemented":
        return "SourceMappedPartialFoundation"
    if name in FND002_DATA_POLICY_PARTIAL_TASKS and current_status == "PartiallyImplemented":
        return "SourceMappedDataPolicyPartial"
    if name in FND002_ZERO_CARD_REFERENCE_TASKS and current_status == "NotReferenced":
        return "SourceKnownZeroCardTiming"
    if current_status == "Unsupported":
        return "SourceMappedUnsupportedFoundation"
    return REMAINING_CLASSIFICATION.get(name, "NeedsSourceMapping")


def build(workspace: Path) -> tuple[dict[str, Any], dict[str, Any], str, str]:
    original = load_json(workspace, ORIGINAL_FND002_PATH)
    inventory = load_json(workspace, INVENTORY_PATH)
    gate = load_json(workspace, GATE_PATH)

    original_entries = original["entries"]
    timing_by_name = {item["name"]: item for item in inventory["timings"]}
    generated_at = datetime.now(timezone.utc).isoformat()
    inventory_hash = file_sha256(workspace / INVENTORY_PATH)

    entries: list[dict[str, Any]] = []
    for previous in original_entries:
        name = previous["apiName"]
        current = timing_by_name[name]
        current_status = current["mappingStatus"]
        classification = classify_entry(name, current_status)
        fnd003_task = FND003_PARTIAL_TASKS.get(name)
        evidence = current["evidence"]
        entries.append(
            {
                "apiName": name,
                "category": previous["category"],
                "previousMappingStatus": previous["originalMappingStatus"],
                "currentMappingStatus": current_status,
                "rerunClassification": classification,
                "fnd003Task": fnd003_task,
                "affectedCardCount": current["affectedCardCount"],
                "sourceFileCount": current["sourceScopeFileCount"],
                "engineSourceReferenceCount": evidence["engineSourceReferenceCount"],
                "engineTestOrDocReferenceCount": evidence["engineTestReferenceCount"],
                "sourceRuntimeCallEvidence": previous.get("sourceRuntimeCallEvidence"),
                "sourceClassOrMethodArea": previous.get("sourceClassOrMethodArea"),
                "sampleSourcePaths": evidence["sampleSourcePaths"],
                "evidenceNotes": evidence.get("notes", []),
                "nextAction": REMAINING_NEXT_ACTION.get(
                    name,
                    "full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적",
                ),
                "generatedStatusPromoted": False,
                "implementationPerformed": False,
            }
        )

    classification_counts = Counter(entry["rerunClassification"] for entry in entries)
    previous_unknown = sum(1 for entry in entries if entry["previousMappingStatus"] == "NeedsSourceReview")
    current_unknown = sum(1 for entry in entries if entry["currentMappingStatus"] == "NeedsSourceReview")
    current_partial = sum(1 for entry in entries if entry["currentMappingStatus"] == "PartiallyImplemented")
    source_mapped_partial = [entry for entry in entries if entry["rerunClassification"] == "SourceMappedPartialFoundation"]
    source_mapped_data_policy_partial = [
        entry for entry in entries if entry["rerunClassification"] == "SourceMappedDataPolicyPartial"
    ]
    source_known_zero_card_timing = [
        entry for entry in entries if entry["rerunClassification"] == "SourceKnownZeroCardTiming"
    ]
    remaining_needs_review = [entry for entry in entries if entry["currentMappingStatus"] == "NeedsSourceReview"]

    gate_summary = gate["summary"]
    summary = {
        "previousNeedsSourceReviewCount": previous_unknown,
        "currentNeedsSourceReviewCount": current_unknown,
        "needsSourceReviewReductionCount": previous_unknown - current_unknown,
        "currentPartiallyImplementedFromOriginalFnd002Count": current_partial,
        "sourceMappedPartialFoundationCount": len(source_mapped_partial),
        "sourceMappedDataPolicyPartialCount": len(source_mapped_data_policy_partial),
        "sourceKnownZeroCardTimingCount": len(source_known_zero_card_timing),
        "remainingNeedsSourceReviewCount": len(remaining_needs_review),
        "classificationCounts": dict(sorted(classification_counts.items())),
        "foundationGateOpenCodeReady": gate_summary["openCodeReady"],
        "foundationGateFailedGateCount": gate_summary["failedGateCount"],
        "foundationGateUnknownCommonApiCount": gate_summary["unknownCommonApiCount"],
        "foundationGateUnsupportedCapabilityCount": gate_summary["unsupportedCapabilityCount"],
        "foundationGatePartiallyImplementedCapabilityCount": gate_summary["partiallyImplementedCapabilityCount"],
        "selectedNextFoundationCapability": gate_summary["selectedNextFoundationCapability"],
        "selectedNextFoundationStatus": gate_summary["selectedNextFoundationStatus"],
        "inventorySha256": inventory_hash,
        "generatedStatusPromoted": False,
        "foundationGateDirectlyManipulated": False,
        "implementationPerformed": False,
        "cardEffectBodyImplemented": False,
        "c0039OrLaterCardPortingRun": False,
        "rlComponentsImplemented": False,
    }

    result = {
        "schemaVersion": "dcgo.as-is-restart.fnd002-rerun-unknown-common-api-source-mapping.v1",
        "generatedAt": generated_at,
        "goal": "FND-002-RERUN",
        "sourceRoot": original["sourceRoot"],
        "inputs": {
            "previousFnd002Mapping": str(ORIGINAL_FND002_PATH).replace("\\", "/"),
            "fullMechanicInventory": str(INVENTORY_PATH).replace("\\", "/"),
            "foundationGate": str(GATE_PATH).replace("\\", "/"),
        },
        "policy": {
            "implementationPerformed": False,
            "srcImplementationModified": False,
            "dcgoOriginalModified": False,
            "cardEffectBodyImplemented": False,
            "generatedStatusPromoted": False,
            "foundationGateDirectlyManipulated": False,
            "c0039OrLaterCardPortingRun": False,
            "rlComponentsImplemented": False,
            "commitOrPushPerformed": False,
            "rawJsonOver100MiBAdded": False,
        },
        "summary": summary,
        "entries": entries,
    }

    queue = {
        "schemaVersion": "dcgo.as-is-restart.fnd002-rerun-next-action-queue.v1",
        "generatedAt": generated_at,
        "goal": "FND-002-RERUN",
        "sourceMapping": str(OUTPUT_JSON).replace("\\", "/"),
        "summary": {
            "sourceMappedPartialFoundationCount": len(source_mapped_partial),
            "sourceMappedDataPolicyPartialCount": len(source_mapped_data_policy_partial),
            "sourceKnownZeroCardTimingCount": len(source_known_zero_card_timing),
            "remainingNeedsSourceReviewCount": len(remaining_needs_review),
            "recommendedNextQueueGroups": [
                "FND-002-A replacement/cut-in timing source mapping (closed)",
                "FND-002-B link lifecycle timing source mapping (closed)",
                "FND-002-C digisorption/digiburst timing policy (closed)",
                "FND-002-D face-up security data policy (closed when generated)",
                "FND-002-E OnStartBattle zero-card timing review (closed when generated)",
            ],
        },
        "sourceMappedPartialFoundationItems": source_mapped_partial,
        "sourceMappedDataPolicyPartialItems": source_mapped_data_policy_partial,
        "sourceKnownZeroCardTimingItems": source_known_zero_card_timing,
        "remainingNeedsSourceReviewItems": remaining_needs_review,
        "recommendedNextQueueGroups": [
            {
                "queueId": "FND-002-A",
                "title": "replacement/cut-in timing source mapping",
                "items": [
                    "WhenPermanentWouldBeDeleted",
                    "WhenRemoveField",
                    "WhenReturntoLibraryAnyone",
                    "WhenUntapAnyone",
                    "WhenWouldDigivolutionCardDiscarded",
                ],
            },
            {
                "queueId": "FND-002-B",
                "title": "link lifecycle timing source mapping",
                "items": ["WhenLinked", "WhenWouldLink", "OnLinkCardDiscarded"],
            },
            {
                "queueId": "FND-002-C",
                "title": "digisorption/digiburst timing policy",
                "items": ["WhenDigisorption", "OnUseDigiburst"],
            },
            {
                "queueId": "FND-002-D",
                "title": "face-up security data policy",
                "status": "closed-by-source-mapped-partial-data-policy",
                "items": ["OnFaceUpSecurityIncreased"],
            },
            {
                "queueId": "FND-002-E",
                "title": "OnStartBattle zero-card timing review",
                "status": "closed-by-source-known-zero-card-timing",
                "items": ["OnStartBattle"],
            },
        ],
    }

    doc = render_doc(result, queue)
    summary_doc = render_summary(result, queue)
    return result, queue, doc, summary_doc


def render_doc(result: dict[str, Any], queue: dict[str, Any]) -> str:
    summary = result["summary"]
    entries = result["entries"]
    rows = [
        [
            entry["apiName"],
            entry["previousMappingStatus"],
            entry["currentMappingStatus"],
            entry["rerunClassification"],
            entry.get("fnd003Task") or "",
            entry["affectedCardCount"],
            entry["sourceFileCount"],
            entry["engineSourceReferenceCount"],
            entry["engineTestOrDocReferenceCount"],
            entry["nextAction"],
        ]
        for entry in entries
    ]
    remaining_rows = [
        [
            entry["apiName"],
            entry["rerunClassification"],
            entry["affectedCardCount"],
            entry["sourceRuntimeCallEvidence"],
            entry["nextAction"],
        ]
        for entry in queue["remainingNeedsSourceReviewItems"]
    ]
    partial_rows = [
        [
            entry["apiName"],
            entry["fnd003Task"],
            entry["affectedCardCount"],
            entry["engineSourceReferenceCount"],
            entry["engineTestOrDocReferenceCount"],
        ]
        for entry in queue["sourceMappedPartialFoundationItems"]
    ]
    data_policy_rows = [
        [
            entry["apiName"],
            entry["rerunClassification"],
            entry["affectedCardCount"],
            entry["engineSourceReferenceCount"],
            entry["engineTestOrDocReferenceCount"],
            entry["nextAction"],
        ]
        for entry in queue.get("sourceMappedDataPolicyPartialItems", [])
    ]
    zero_card_rows = [
        [
            entry["apiName"],
            entry["currentMappingStatus"],
            entry["rerunClassification"],
            entry["affectedCardCount"],
            entry["sourceRuntimeCallEvidence"],
            entry["nextAction"],
        ]
        for entry in queue.get("sourceKnownZeroCardTimingItems", [])
    ]

    lines = [
        "# FND-002-RERUN Unknown Common API Source Mapping",
        "",
        "이번 문서는 기존 FND-002 결과를 historical input으로만 사용하고, 현재 `full-mechanic-inventory.json`과 Foundation Gate 재계산 결과를 기준으로 unknown common API 상태를 다시 정리한다.",
        "",
        "## Scope",
        "",
        f"- AS-IS root: `{result['sourceRoot']}`",
        "- Source of Truth: `E:/headlessDCGO/DCGO/Assets`",
        "- Implementation performed: no",
        "- Generated status promoted: no",
        "- Foundation Gate direct manipulation: no",
        "",
        "## Count Audit",
        "",
        f"- Previous FND-002 NeedsSourceReview count: {summary['previousNeedsSourceReviewCount']}",
        f"- Current NeedsSourceReview count for original FND-002 set: {summary['currentNeedsSourceReviewCount']}",
        f"- Reduced NeedsSourceReview count: {summary['needsSourceReviewReductionCount']}",
        f"- Source-mapped partial foundation count: {summary['sourceMappedPartialFoundationCount']}",
        f"- Source-mapped data-policy partial count: {summary['sourceMappedDataPolicyPartialCount']}",
        f"- Source-known zero-card timing count: {summary['sourceKnownZeroCardTimingCount']}",
        f"- Remaining NeedsSourceReview count: {summary['remainingNeedsSourceReviewCount']}",
        f"- Foundation Gate unknownCommonApiCount: {summary['foundationGateUnknownCommonApiCount']}",
        f"- Foundation Gate OpenCodeReady: {str(summary['foundationGateOpenCodeReady']).lower()}",
        "",
        "## Classification Counts",
        "",
    ]
    for key, value in summary["classificationCounts"].items():
        lines.append(f"- {key}: {value}")

    lines.extend(
        [
            "",
            "## Source-Mapped Partial Foundation Items",
            "",
            markdown_table(["API", "FND task", "Affected", "Engine refs", "Test/doc refs"], partial_rows),
            "",
            "## Source-Mapped Data-Policy Partial Items",
            "",
            markdown_table(["API", "Classification", "Affected", "Engine refs", "Test/doc refs", "Next action"], data_policy_rows),
            "",
            "## Source-Known Zero-Card Timing Items",
            "",
            markdown_table(["API", "Current", "Classification", "Affected", "Source call evidence", "Next action"], zero_card_rows),
            "",
            "## Remaining NeedsSourceReview Items",
            "",
            markdown_table(["API", "Classification", "Affected", "Source call evidence", "Next action"], remaining_rows),
            "",
            "## Full Mapping Table",
            "",
            markdown_table(
                [
                    "API",
                    "Previous",
                    "Current",
                    "Rerun classification",
                    "FND task",
                    "Affected",
                    "Source files",
                    "Engine refs",
                    "Test/doc refs",
                    "Next action",
                ],
                rows,
            ),
            "",
            "## Next Goal Candidate Range",
            "",
            "- `FND-002-A`: replacement/cut-in timing source mapping",
            "- `FND-002-B`: link lifecycle timing source mapping",
            "- `FND-002-C`: digisorption/digiburst timing policy",
            "- `FND-002-D`: face-up security data policy (closed as source-mapped partial data-policy blocker when present in the data-policy table)",
            "- `FND-002-E`: `OnStartBattle` zero-card timing review (closed when present in the zero-card table)",
            f"- `FND-003`: unsupported capability {summary['foundationGateUnsupportedCapabilityCount']}개는 별도 remediation queue로 계속 유지",
            f"- `FND-001`: `ContinuousOrStaticEffect` partial {summary['foundationGatePartiallyImplementedCapabilityCount']}개는 별도 partial closure queue로 계속 유지",
            "",
            "## Guardrails",
            "",
            "- `src/DCGO.RL.Engine` implementation code was not modified by this rerun.",
            "- Original `DCGO/Assets` was not modified.",
            "- No individual `CardEffect` body was implemented.",
            "- No C0039+ card-porting was run.",
            "- No RL Environment/Observation/Reward/Dataset/Trainer was implemented.",
            "- Generated card status was not promoted.",
            "- Foundation Gate numbers were recalculated from generated inventory, not edited by hand.",
            "- No commit/push was performed.",
        ]
    )
    return "\n".join(lines) + "\n"


def render_summary(result: dict[str, Any], queue: dict[str, Any]) -> str:
    summary = result["summary"]
    remaining = ", ".join(entry["apiName"] for entry in queue["remainingNeedsSourceReviewItems"])
    partial = ", ".join(entry["apiName"] for entry in queue["sourceMappedPartialFoundationItems"])
    data_policy_partial = ", ".join(entry["apiName"] for entry in queue.get("sourceMappedDataPolicyPartialItems", []))
    zero_card = ", ".join(entry["apiName"] for entry in queue.get("sourceKnownZeroCardTimingItems", []))
    lines = [
        "# FND-002-RERUN Unknown Common API Mapping Summary",
        "",
        f"- AS-IS root: `{result['sourceRoot']}`",
        f"- Previous NeedsSourceReview: {summary['previousNeedsSourceReviewCount']}",
        f"- Current NeedsSourceReview: {summary['currentNeedsSourceReviewCount']}",
        f"- Reduction: {summary['needsSourceReviewReductionCount']}",
        f"- SourceMappedPartialFoundation: {summary['sourceMappedPartialFoundationCount']}",
        f"- SourceMappedDataPolicyPartial: {summary['sourceMappedDataPolicyPartialCount']}",
        f"- SourceKnownZeroCardTiming: {summary['sourceKnownZeroCardTimingCount']}",
        f"- Foundation Gate unknownCommonApiCount: {summary['foundationGateUnknownCommonApiCount']}",
        f"- OpenCodeReady: {str(summary['foundationGateOpenCodeReady']).lower()}",
        "",
        "## Source-Mapped Partial Foundation",
        "",
        partial or "- none",
        "",
        "## Source-Mapped Data Policy Partial",
        "",
        data_policy_partial or "- none",
        "",
        "## Source-Known Zero-Card Timing",
        "",
        zero_card or "- none",
        "",
        "## Remaining NeedsSourceReview",
        "",
        remaining or "- none",
        "",
        "## Generated Files",
        "",
        f"- `{OUTPUT_DOC.as_posix()}`",
        f"- `{OUTPUT_SUMMARY.as_posix()}`",
        f"- `{OUTPUT_JSON.as_posix()}`",
        f"- `{OUTPUT_QUEUE_JSON.as_posix()}`",
        "",
        "추천 commit message: `docs: refresh FND-002 common API mapping`",
    ]
    return "\n".join(lines) + "\n"


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--workspace", default=".")
    args = parser.parse_args()
    workspace = Path(args.workspace).resolve()

    result, queue, doc, summary_doc = build(workspace)
    write_json(workspace / OUTPUT_JSON, result)
    write_json(workspace / OUTPUT_QUEUE_JSON, queue)
    (workspace / OUTPUT_DOC).parent.mkdir(parents=True, exist_ok=True)
    (workspace / OUTPUT_DOC).write_text(doc, encoding="utf-8")
    (workspace / OUTPUT_SUMMARY).write_text(summary_doc, encoding="utf-8")

    print(
        json.dumps(
            {
                "output": str(OUTPUT_JSON).replace("\\", "/"),
                "queue": str(OUTPUT_QUEUE_JSON).replace("\\", "/"),
                "summary": result["summary"],
            },
            ensure_ascii=False,
            indent=2,
        )
    )


if __name__ == "__main__":
    main()
