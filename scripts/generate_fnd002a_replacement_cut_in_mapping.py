# -*- coding: utf-8 -*-
"""Generate FND-002-A replacement/cut-in source mapping artifacts.

This is an evidence generator only. It does not implement replacement effects,
does not modify the original DCGO source, and does not promote generated card
status.
"""

from __future__ import annotations

import argparse
import hashlib
import json
from collections import Counter
from datetime import datetime, timezone
from pathlib import Path
from typing import Any


INVENTORY_PATH = Path("docs/generated/full-mechanic-inventory.json")
GATE_PATH = Path("docs/generated/foundation-completion-gate.json")
FND002_RERUN_PATH = Path("docs/generated/as-is-restart/fnd-002-rerun-unknown-common-api-mapping.json")
ORIGINAL_FND002_PATH = Path("docs/generated/as-is-restart/fnd-002-unknown-common-api-mapping.json")

OUTPUT_JSON = Path("docs/generated/as-is-restart/fnd-002-a-replacement-cut-in-source-mapping.json")
OUTPUT_QUEUE_JSON = Path("docs/generated/as-is-restart/fnd-002-a-next-action-queue.json")
OUTPUT_DOC = Path("docs/as-is-restart/FND_002_A_REPLACEMENT_CUT_IN_SOURCE_MAPPING.md")
OUTPUT_SUMMARY = Path("docs/as-is-restart/fnd-002-a-replacement-cut-in-source-mapping-summary.md")

TARGETS: dict[str, dict[str, Any]] = {
    "WhenPermanentWouldBeDeleted": {
        "sourceClassification": "ReplacementDeletePrevention",
        "inventoryClassification": "SourceMappedUnsupportedFoundation",
        "sourceFiles": [
            "DCGO/Assets/Scripts/Script/ICardEffect.cs:980",
            "DCGO/Assets/Scripts/Script/Permanent.cs:2898",
            "DCGO/Assets/Scripts/Script/Permanent.cs:2954",
            "DCGO/Assets/Scripts/Script/Permanent.cs:3117",
            "DCGO/Assets/Scripts/Script/Permanent.cs:3138",
            "DCGO/Assets/Scripts/Script/CardController.cs:3696",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/KeyWordEffects/Barrier.cs:99",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/KeyWordEffects/Evade.cs:77",
        ],
        "payloadKeys": ["Permanents", "battle", "CardEffect"],
        "sourceMeaning": "deletion/destruction replacement window for Evade, Barrier, Partition, Scapegoat, and related prevention effects",
        "missingHeadlessBoundary": "delete-prevention replacement queue before actual field removal",
        "nextFoundationTask": "FND-002-A1",
    },
    "WhenRemoveField": {
        "sourceClassification": "WouldRemoveFieldCutIn",
        "inventoryClassification": "SourceMappedUnsupportedFoundation",
        "sourceFiles": [
            "DCGO/Assets/Scripts/Script/ICardEffect.cs:979",
            "DCGO/Assets/Scripts/Script/CardController.cs:2333",
            "DCGO/Assets/Scripts/Script/CardController.cs:2499",
            "DCGO/Assets/Scripts/Script/CardController.cs:2932",
            "DCGO/Assets/Scripts/Script/CardController.cs:2948",
            "DCGO/Assets/Scripts/Script/CardController.cs:3228",
            "DCGO/Assets/Scripts/Script/CardController.cs:3244",
            "DCGO/Assets/Scripts/Script/CardController.cs:3538",
            "DCGO/Assets/Scripts/Script/CardController.cs:3705",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/HashtableSetting.cs:70",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenRemoveField.cs:11",
            "DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/ArmorPurge.cs:21",
            "DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Decode.cs:54",
            "DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Fragment.cs:52",
            "DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/MaterialSave.cs:47",
        ],
        "payloadKeys": ["Permanents", "battle", "CardEffect", "digixros"],
        "sourceMeaning": "generic would-leave-battle-area cut-in before bounce, placement under cards, link placement, and removal paths",
        "missingHeadlessBoundary": "willBeRemoveField target marking, cut-in ordering, target re-fix, and actual movement continuation",
        "nextFoundationTask": "FND-002-A2",
    },
    "WhenReturntoLibraryAnyone": {
        "sourceClassification": "WouldReturnToLibraryCutIn",
        "inventoryClassification": "SourceMappedUnsupportedFoundation",
        "sourceFiles": [
            "DCGO/Assets/Scripts/Script/ICardEffect.cs:981",
            "DCGO/Assets/Scripts/Script/CardController.cs:2323",
            "DCGO/Assets/Scripts/Script/CardController.cs:2489",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/HashtableSetting.cs:70",
        ],
        "payloadKeys": ["Permanents", "CardEffect", "battle", "digixros"],
        "sourceMeaning": "specific deck-return replacement window that precedes the generic WhenRemoveField window",
        "missingHeadlessBoundary": "return-to-library pre-move cut-in before deck bottom/top mutation",
        "nextFoundationTask": "FND-002-A3",
    },
    "WhenUntapAnyone": {
        "sourceClassification": "WouldUnsuspendCutIn",
        "inventoryClassification": "SourceMappedUnsupportedFoundation",
        "sourceFiles": [
            "DCGO/Assets/Scripts/Script/ICardEffect.cs:983",
            "DCGO/Assets/Scripts/Script/CardController.cs:5694",
        ],
        "payloadKeys": ["Permanents", "CardEffect"],
        "sourceMeaning": "would-unsuspend cut-in before actual IsSuspended=false mutation and OnUnTappedAnyone trigger",
        "missingHeadlessBoundary": "pre-unsuspend replacement/selection window and target re-fix before primitive unsuspend",
        "nextFoundationTask": "FND-002-A4",
    },
    "WhenWouldDigivolutionCardDiscarded": {
        "sourceClassification": "WouldSourceTrashCutIn",
        "inventoryClassification": "SourceMappedUnsupportedFoundation",
        "sourceFiles": [
            "DCGO/Assets/Scripts/Script/ICardEffect.cs:1023",
            "DCGO/Assets/Scripts/Script/CardController.cs:5181",
            "DCGO/Assets/Scripts/Script/CardController.cs:5293",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/HashtableSetting.cs:321",
        ],
        "payloadKeys": ["Permanent", "DiscardedCards", "CardEffect"],
        "sourceMeaning": "would-trash-digivolution-cards cut-in before source cards are removed and OnDigivolutionCardDiscarded fires",
        "missingHeadlessBoundary": "source-card willBeRemoveSources marking, cut-in processing, and post-cut-in fixed source trash continuation",
        "nextFoundationTask": "FND-002-A5",
    },
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


def build(workspace: Path) -> tuple[dict[str, Any], dict[str, Any], str, str]:
    inventory = load_json(workspace, INVENTORY_PATH)
    gate = load_json(workspace, GATE_PATH)
    fnd002_rerun = load_json(workspace, FND002_RERUN_PATH)
    original_fnd002 = load_json(workspace, ORIGINAL_FND002_PATH)

    timings = {item["name"]: item for item in inventory["timings"]}
    previous_entries = {item["apiName"]: item for item in fnd002_rerun["entries"]}
    original_entries = {item["apiName"]: item for item in original_fnd002["entries"]}
    generated_at = datetime.now(timezone.utc).isoformat()

    entries: list[dict[str, Any]] = []
    for api_name, target in TARGETS.items():
        current = timings[api_name]
        previous = previous_entries.get(api_name, {})
        original = original_entries.get(api_name, {})
        evidence = current["evidence"]
        entries.append(
            {
                "apiName": api_name,
                "originalFnd002MappingStatus": original.get("originalMappingStatus", "NeedsSourceReview"),
                "previousMappingStatus": previous.get("currentMappingStatus", "NeedsSourceReview"),
                "currentMappingStatus": current["mappingStatus"],
                "fnd002AClassification": target["inventoryClassification"],
                "sourceClassification": target["sourceClassification"],
                "affectedCardCount": current["affectedCardCount"],
                "sourceFileCount": current["sourceScopeFileCount"],
                "engineSourceReferenceCount": evidence["engineSourceReferenceCount"],
                "engineTestOrDocReferenceCount": evidence["engineTestReferenceCount"],
                "sourceFiles": target["sourceFiles"],
                "sampleSourcePaths": evidence["sampleSourcePaths"],
                "payloadKeys": target["payloadKeys"],
                "sourceMeaning": target["sourceMeaning"],
                "missingHeadlessBoundary": target["missingHeadlessBoundary"],
                "nextFoundationTask": target["nextFoundationTask"],
                "evidenceNotes": evidence.get("notes", []),
                "implementationPerformed": False,
                "generatedStatusPromoted": False,
            }
        )

    counts = Counter(entry["currentMappingStatus"] for entry in entries)
    classification_counts = Counter(entry["fnd002AClassification"] for entry in entries)
    gate_summary = gate["summary"]
    summary = {
        "targetCount": len(entries),
        "currentStatusCounts": dict(sorted(counts.items())),
        "classificationCounts": dict(sorted(classification_counts.items())),
        "movedFromNeedsSourceReviewToUnsupportedCount": sum(
            1
            for entry in entries
            if entry["originalFnd002MappingStatus"] == "NeedsSourceReview"
            and entry["currentMappingStatus"] == "Unsupported"
        ),
        "foundationGateOpenCodeReady": gate_summary["openCodeReady"],
        "foundationGateUnknownCommonApiCount": gate_summary["unknownCommonApiCount"],
        "foundationGateUnsupportedCapabilityCount": gate_summary["unsupportedCapabilityCount"],
        "foundationGatePartiallyImplementedCapabilityCount": gate_summary["partiallyImplementedCapabilityCount"],
        "inventorySha256": file_sha256(workspace / INVENTORY_PATH),
        "implementationPerformed": False,
        "srcImplementationModified": False,
        "dcgoOriginalModified": False,
        "cardEffectBodyImplemented": False,
        "generatedStatusPromoted": False,
        "foundationGateDirectlyManipulated": False,
        "c0039OrLaterCardPortingRun": False,
        "rlComponentsImplemented": False,
    }

    result = {
        "schemaVersion": "dcgo.as-is-restart.fnd002a-replacement-cut-in-source-mapping.v1",
        "generatedAt": generated_at,
        "goal": "FND-002-A",
        "sourceRoot": "E:/headlessDCGO/DCGO",
        "inputs": {
            "fullMechanicInventory": INVENTORY_PATH.as_posix(),
            "foundationGate": GATE_PATH.as_posix(),
            "fnd002Rerun": FND002_RERUN_PATH.as_posix(),
            "originalFnd002Mapping": ORIGINAL_FND002_PATH.as_posix(),
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
        "schemaVersion": "dcgo.as-is-restart.fnd002a-next-action-queue.v1",
        "generatedAt": generated_at,
        "goal": "FND-002-A",
        "sourceMapping": OUTPUT_JSON.as_posix(),
        "summary": {
            "nextFoundationTaskCount": len(entries),
            "note": "These tasks are source-known Unsupported blockers, not CardEffect body tasks.",
        },
        "nextFoundationTasks": [
            {
                "taskId": entry["nextFoundationTask"],
                "apiName": entry["apiName"],
                "classification": entry["fnd002AClassification"],
                "sourceMeaning": entry["sourceMeaning"],
                "missingHeadlessBoundary": entry["missingHeadlessBoundary"],
                "candidateTestScope": [
                    "source payload key preservation",
                    "pre-mutation cut-in ordering",
                    "target re-fix after cut-in",
                    "explicit unsupported until implementation exists",
                ],
                "prohibited": [
                    "CardEffect body implementation",
                    "C0039+ card-porting",
                    "generated status promotion",
                    "silent no-op",
                ],
            }
            for entry in entries
        ],
    }

    return result, queue, render_doc(result, queue), render_summary(result, queue)


def render_doc(result: dict[str, Any], queue: dict[str, Any]) -> str:
    summary = result["summary"]
    entries = result["entries"]
    rows = [
        [
            entry["apiName"],
            entry["previousMappingStatus"],
            entry["currentMappingStatus"],
            entry["sourceClassification"],
            entry["affectedCardCount"],
            entry["sourceFileCount"],
            ", ".join(entry["payloadKeys"]),
            entry["nextFoundationTask"],
        ]
        for entry in entries
    ]
    detail_rows = [
        [
            entry["apiName"],
            entry["sourceMeaning"],
            entry["missingHeadlessBoundary"],
            "<br>".join(entry["sourceFiles"]),
        ]
        for entry in entries
    ]
    task_rows = [
        [
            task["taskId"],
            task["apiName"],
            task["missingHeadlessBoundary"],
        ]
        for task in queue["nextFoundationTasks"]
    ]
    lines = [
        "# FND-002-A Replacement/Cut-In Source Mapping",
        "",
        "이번 문서는 FND-002-RERUN 이후 남은 unknown common API 중 replacement/cut-in timing 5개를 원본 source에 맞춰 분해한다. 구현은 수행하지 않는다.",
        "",
        "## Scope",
        "",
        f"- AS-IS root: `{result['sourceRoot']}`",
        "- Source of Truth: `E:/headlessDCGO/DCGO/Assets`",
        "- Target timings: `WhenPermanentWouldBeDeleted`, `WhenRemoveField`, `WhenReturntoLibraryAnyone`, `WhenUntapAnyone`, `WhenWouldDigivolutionCardDiscarded`",
        "- Implementation performed: no",
        "- Generated status promoted: no",
        "",
        "## Count Audit",
        "",
        f"- Target count: {summary['targetCount']}",
        f"- Current status counts: {summary['currentStatusCounts']}",
        f"- Moved from NeedsSourceReview to Unsupported: {summary['movedFromNeedsSourceReviewToUnsupportedCount']}",
        f"- Foundation Gate unknownCommonApiCount: {summary['foundationGateUnknownCommonApiCount']}",
        f"- Foundation Gate OpenCodeReady: {str(summary['foundationGateOpenCodeReady']).lower()}",
        "",
        "## Mapping Table",
        "",
        markdown_table(
            [
                "API",
                "Previous",
                "Current",
                "Source classification",
                "Affected",
                "Source files",
                "Payload keys",
                "Next task",
            ],
            rows,
        ),
        "",
        "## Source Evidence",
        "",
        markdown_table(["API", "Source meaning", "Missing headless boundary", "Source files"], detail_rows),
        "",
        "## Next Foundation Tasks",
        "",
        markdown_table(["Task", "API", "Boundary"], task_rows),
        "",
        "## Remaining FND-002 Range After FND-002-A",
        "",
        "- `FND-002-B`: link lifecycle source mapping (`WhenLinked`, `WhenWouldLink`, `OnLinkCardDiscarded`)",
        "- `FND-002-C`: digisorption/digiburst timing policy (`WhenDigisorption`, `OnUseDigiburst`)",
        "- `FND-002-D`: face-up security data policy (`OnFaceUpSecurityIncreased`)",
        "- `FND-002-E`: `OnStartBattle` manual review",
        "",
        "## Guardrails",
        "",
        "- `src/DCGO.RL.Engine` implementation code was not modified.",
        "- Original `DCGO/Assets` was not modified.",
        "- No individual `CardEffect` body was implemented.",
        "- No C0039+ card-porting was run.",
        "- No RL component was implemented.",
        "- Generated card status was not promoted.",
        "- Foundation Gate values were recalculated from generated inventory, not edited by hand.",
        "- No commit/push was performed.",
    ]
    return "\n".join(lines) + "\n"


def render_summary(result: dict[str, Any], queue: dict[str, Any]) -> str:
    summary = result["summary"]
    entries = result["entries"]
    lines = [
        "# FND-002-A Replacement/Cut-In Source Mapping Summary",
        "",
        f"- AS-IS root: `{result['sourceRoot']}`",
        f"- Target count: {summary['targetCount']}",
        f"- Current status counts: {summary['currentStatusCounts']}",
        f"- Moved from NeedsSourceReview to Unsupported: {summary['movedFromNeedsSourceReviewToUnsupportedCount']}",
        f"- Foundation Gate unknownCommonApiCount: {summary['foundationGateUnknownCommonApiCount']}",
        f"- OpenCodeReady: {str(summary['foundationGateOpenCodeReady']).lower()}",
        "",
        "## Target APIs",
        "",
        ", ".join(entry["apiName"] for entry in entries),
        "",
        "## Generated Files",
        "",
        f"- `{OUTPUT_DOC.as_posix()}`",
        f"- `{OUTPUT_SUMMARY.as_posix()}`",
        f"- `{OUTPUT_JSON.as_posix()}`",
        f"- `{OUTPUT_QUEUE_JSON.as_posix()}`",
        "",
        "추천 commit message: `docs: map FND-002 replacement cut-in timings`",
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
                "output": OUTPUT_JSON.as_posix(),
                "queue": OUTPUT_QUEUE_JSON.as_posix(),
                "summary": result["summary"],
            },
            ensure_ascii=False,
            indent=2,
        )
    )


if __name__ == "__main__":
    main()
