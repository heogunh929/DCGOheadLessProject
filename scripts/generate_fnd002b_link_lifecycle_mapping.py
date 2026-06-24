# -*- coding: utf-8 -*-
"""Generate FND-002-B link lifecycle source mapping artifacts.

This is an evidence generator only. It does not implement link lifecycle
runtime behavior, does not modify the original DCGO source, and does not
promote generated card status.
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

OUTPUT_JSON = Path("docs/generated/as-is-restart/fnd-002-b-link-lifecycle-source-mapping.json")
OUTPUT_QUEUE_JSON = Path("docs/generated/as-is-restart/fnd-002-b-next-action-queue.json")
OUTPUT_DOC = Path("docs/as-is-restart/FND_002_B_LINK_LIFECYCLE_SOURCE_MAPPING.md")
OUTPUT_SUMMARY = Path("docs/as-is-restart/fnd-002-b-link-lifecycle-source-mapping-summary.md")

TARGETS: dict[str, dict[str, Any]] = {
    "WhenWouldLink": {
        "sourceClassification": "PreLinkCutIn",
        "inventoryClassification": "SourceMappedUnsupportedFoundation",
        "sourceFiles": [
            "DCGO/Assets/Scripts/Script/ICardEffect.cs:1024",
            "DCGO/Assets/Scripts/Script/CardController.cs:3439",
            "DCGO/Assets/Scripts/Script/CardController.cs:3456",
            "DCGO/Assets/Scripts/Script/CardController.cs:3476",
            "DCGO/Assets/Scripts/Script/CardController.cs:3477",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/HashtableSetting.cs:198",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenWouldLink.cs:11",
        ],
        "payloadKeys": ["Card", "Root", "CardEffect", "Permanent"],
        "sourceMeaning": "link action pre-cut-in before link cost payment and AddLinkCard/IPlacePermanentToLinkCards continuation",
        "missingHeadlessBoundary": "pre-link cut-in queue, payload preservation, root-aware continuation, and post-cut-in target revalidation",
        "nextFoundationTask": "FND-002-B1",
    },
    "WhenLinked": {
        "sourceClassification": "PostLinkTrigger",
        "inventoryClassification": "SourceMappedUnsupportedFoundation",
        "sourceFiles": [
            "DCGO/Assets/Scripts/Script/ICardEffect.cs:1025",
            "DCGO/Assets/Scripts/Script/Permanent.cs:1237",
            "DCGO/Assets/Scripts/Script/Permanent.cs:1263",
            "DCGO/Assets/Scripts/Script/Permanent.cs:1285",
            "DCGO/Assets/Scripts/Script/Permanent.cs:1290",
            "DCGO/Assets/Scripts/Script/CardEffectFactory.cs:874",
            "DCGO/Assets/Scripts/Script/CardEffectFactory.cs:1111",
            "DCGO/Assets/Scripts/Script/CardEffectFactory.cs:1131",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenLinked.cs:8",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenLinked.cs:45",
        ],
        "payloadKeys": ["Permanent", "CardEffect", "Card", "isFromDigimon"],
        "sourceMeaning": "post-link trigger emitted after a card is inserted into LinkedCards and LinkedDP/cardSources are updated",
        "missingHeadlessBoundary": "post-link rule event queue, linked-source role snapshot, isFromDigimon payload, and WhenLinking factory wrapper parity",
        "nextFoundationTask": "FND-002-B2",
    },
    "OnLinkCardDiscarded": {
        "sourceClassification": "LinkedCardDiscardEvent",
        "inventoryClassification": "SourceMappedUnsupportedFoundation",
        "sourceFiles": [
            "DCGO/Assets/Scripts/Script/ICardEffect.cs:1029",
            "DCGO/Assets/Scripts/Script/CardController.cs:5242",
            "DCGO/Assets/Scripts/Script/CardController.cs:5263",
            "DCGO/Assets/Scripts/Script/CardController.cs:5310",
            "DCGO/Assets/Scripts/Script/CardController.cs:5322",
            "DCGO/Assets/Scripts/Script/CardController.cs:5327",
            "DCGO/Assets/Scripts/Script/CardController.cs:5339",
            "DCGO/Assets/Scripts/Script/CardEffectCommons.cs:567",
        ],
        "payloadKeys": ["CardEffect", "Permanent", "DiscardedCards"],
        "sourceMeaning": "linked-card discard event emitted for fixed linked cards before RemoveLinkedCard actually removes/trashes them",
        "missingHeadlessBoundary": "linked-card discard primitive, willBeRemoveSources equivalent, OnLinkCardDiscarded trigger queue, and Ace overflow ordering",
        "nextFoundationTask": "FND-002-B3",
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
                "fnd002BClassification": target["inventoryClassification"],
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
    classification_counts = Counter(entry["fnd002BClassification"] for entry in entries)
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
        "schemaVersion": "dcgo.as-is-restart.fnd002b-link-lifecycle-source-mapping.v1",
        "generatedAt": generated_at,
        "goal": "FND-002-B",
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
        "schemaVersion": "dcgo.as-is-restart.fnd002b-next-action-queue.v1",
        "generatedAt": generated_at,
        "goal": "FND-002-B",
        "sourceMapping": OUTPUT_JSON.as_posix(),
        "summary": {
            "nextFoundationTaskCount": len(entries),
            "note": "These tasks are source-known Unsupported link lifecycle blockers, not CardEffect body tasks.",
        },
        "nextFoundationTasks": [
            {
                "taskId": entry["nextFoundationTask"],
                "apiName": entry["apiName"],
                "classification": entry["fnd002BClassification"],
                "sourceMeaning": entry["sourceMeaning"],
                "missingHeadlessBoundary": entry["missingHeadlessBoundary"],
                "candidateTestScope": [
                    "source payload key preservation",
                    "link lifecycle ordering",
                    "source/target revalidation",
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

    return result, queue, render_doc(result, queue), render_summary(result)


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
        "# FND-002-B Link Lifecycle Source Mapping",
        "",
        "이 문서는 FND-002-A 이후 남은 unknown common API 중 link lifecycle timing 3개를 원본 source에 맞춰 분해한 evidence이다. 구현은 수행하지 않았다.",
        "",
        "## Scope",
        "",
        f"- AS-IS root: `{result['sourceRoot']}`",
        "- Source of Truth: `E:/headlessDCGO/DCGO/Assets`",
        "- Target timings: `WhenWouldLink`, `WhenLinked`, `OnLinkCardDiscarded`",
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
        "## Remaining FND-002 Range After FND-002-B",
        "",
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


def render_summary(result: dict[str, Any]) -> str:
    summary = result["summary"]
    entries = result["entries"]
    lines = [
        "# FND-002-B Link Lifecycle Source Mapping Summary",
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
        "추천 commit message: `docs: map FND-002 link lifecycle timings`",
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
