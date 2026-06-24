# -*- coding: utf-8 -*-
"""Generate FND-002-C Digisorption/DigiBurst timing policy artifacts.

This is an evidence/policy generator only. It does not implement Digisorption
or DigiBurst runtime behavior, does not modify the original DCGO source, and
does not promote generated card status.
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

OUTPUT_JSON = Path("docs/generated/as-is-restart/fnd-002-c-digisorption-digiburst-timing-policy.json")
OUTPUT_QUEUE_JSON = Path("docs/generated/as-is-restart/fnd-002-c-next-action-queue.json")
OUTPUT_DOC = Path("docs/as-is-restart/FND_002_C_DIGISORPTION_DIGIBURST_TIMING_POLICY.md")
OUTPUT_SUMMARY = Path("docs/as-is-restart/fnd-002-c-digisorption-digiburst-timing-policy-summary.md")

TARGETS: dict[str, dict[str, Any]] = {
    "WhenDigisorption": {
        "sourceClassification": "DigisorptionCostWindow",
        "inventoryClassification": "SourceMappedUnsupportedFoundation",
        "sourceFiles": [
            "DCGO/Assets/Scripts/Script/ICardEffect.cs:978",
            "DCGO/Assets/Scripts/Script/CardSource.cs:2640",
            "DCGO/Assets/Scripts/Script/CardSource.cs:2641",
            "DCGO/Assets/Scripts/Script/CardSource.cs:2645",
            "DCGO/Assets/Scripts/Script/Player.cs:1180",
            "DCGO/Assets/Scripts/Script/Player.cs:1198",
            "DCGO/Assets/Scripts/Script/Player.cs:1204",
            "DCGO/Assets/Scripts/Script/Player.cs:1250",
            "DCGO/Assets/Scripts/Script/Player.cs:1268",
            "DCGO/Assets/Scripts/Script/Player.cs:1274",
            "DCGO/Assets/Scripts/Script/CardEffectInterfaces.cs:366",
            "DCGO/Assets/Scripts/Script/CardEffectInterfaces.cs:368",
            "DCGO/Assets/Scripts/Script/CardEffects/CanSuspendByDigisorptionClass.cs:6",
            "DCGO/Assets/Scripts/Script/CardEffects/CanSuspendByDigisorptionClass.cs:8",
            "DCGO/Assets/Scripts/Script/CardEffects/CanSuspendByDigisorptionClass.cs:19",
            "DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:15",
            "DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:18",
            "DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:60",
            "DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:130",
            "DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:136",
            "DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:144",
            "DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:150",
            "DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:165",
            "DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:193",
            "DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:196",
            "DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs:279",
            "DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs:282",
            "DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs:374",
            "DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs:377",
            "DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs:411",
            "DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs:425",
        ],
        "payloadKeys": ["CardEffect"],
        "derivedContext": [
            "Card",
            "isEvolution",
            "Permanents",
            "selected suspended permanent",
            "cost reduction amount",
            "UntilCalculateFixedCostEffect",
        ],
        "sourceMeaning": (
            "Digisorption is a BeforePayCost card-effect body pattern that selects a suspend source, installs a "
            "temporary cost-reduction provider, then opens a WhenDigisorption cut-in/trigger window for effects that "
            "modify which permanent may be suspended."
        ),
        "policyDecision": (
            "Do not port individual Digisorption card bodies until a shared cost-window state machine exists. The "
            "timing is source-known and should be tracked as a known Unsupported foundation blocker, not unknown."
        ),
        "missingHeadlessBoundary": (
            "source-aligned Digisorption cost transaction, selected suspend-source snapshot, reduction amount, "
            "UntilCalculateFixedCostEffect equivalent, and post-selection WhenDigisorption trigger queue"
        ),
        "nextFoundationTask": "FND-002-C1",
        "cardEffectBodyBoundaryRetained": False,
    },
    "OnUseDigiburst": {
        "sourceClassification": "DigiBurstLifecycleBoundary",
        "inventoryClassification": "SourceMappedUnsupportedFoundation",
        "sourceFiles": [
            "DCGO/Assets/Scripts/Script/ICardEffect.cs:992",
            "DCGO/Assets/Scripts/Script/CardSource.cs:2544",
            "DCGO/Assets/Scripts/Script/CardSource.cs:2552",
            "DCGO/Assets/Scripts/Script/CardController.cs:2114",
            "DCGO/Assets/Scripts/Script/CardController.cs:2135",
            "DCGO/Assets/Scripts/Script/CardController.cs:2163",
            "DCGO/Assets/Scripts/Script/CardController.cs:2173",
            "DCGO/Assets/Scripts/Script/CardController.cs:2220",
            "DCGO/Assets/Scripts/Script/CardController.cs:2222",
            "DCGO/Assets/Scripts/Script/CardController.cs:2223",
            "DCGO/Assets/Scripts/Script/CardController.cs:2228",
            "DCGO/Assets/Scripts/Script/CardController.cs:2233",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenUseDigiBurst.cs:11",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenUseDigiBurst.cs:13",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenUseDigiBurst.cs:21",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnTrashBySelfDigiBurst.cs:10",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnTrashBySelfDigiBurst.cs:18",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnTrashBySelfDigiBurst.cs:24",
            "DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_056.cs:59",
            "DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_056.cs:89",
            "DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_004.cs:37",
        ],
        "payloadKeys": ["Permanent", "CardEffect"],
        "derivedContext": [
            "selected digivolution cards",
            "DigiBurst count",
            "up-to-max selection flag",
            "self source-trash follow-up via CanTriggerOnTrashBySelfDigiBurst",
        ],
        "sourceMeaning": (
            "IDigiBurst selects digivolution cards, emits OnUseDigiburst with Permanent/CardEffect before actual "
            "ITrashDigivolutionCards, then self source-trash triggers use the same activating DigiBurst effect as "
            "follow-up context."
        ),
        "policyDecision": (
            "Treat the common DigiBurst lifecycle as a known Unsupported foundation blocker. Individual DigiBurst card "
            "effect bodies remain excluded until the common selection/trash lifecycle exists."
        ),
        "missingHeadlessBoundary": (
            "source-aligned DigiBurst primitive, selected source snapshot, OnUseDigiburst trigger ordering before "
            "source trash, and self-source-trash follow-up payload"
        ),
        "nextFoundationTask": "FND-002-C2",
        "cardEffectBodyBoundaryRetained": True,
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
                "previousMappingStatus": original.get("originalMappingStatus", "NeedsSourceReview"),
                "currentRerunMappingStatus": previous.get("currentMappingStatus", "NeedsSourceReview"),
                "previousRerunClassification": previous.get("rerunClassification"),
                "currentMappingStatus": current["mappingStatus"],
                "fnd002CClassification": target["inventoryClassification"],
                "sourceClassification": target["sourceClassification"],
                "affectedCardCount": current["affectedCardCount"],
                "sourceFileCount": current["sourceScopeFileCount"],
                "engineSourceReferenceCount": evidence["engineSourceReferenceCount"],
                "engineTestOrDocReferenceCount": evidence["engineTestReferenceCount"],
                "sourceFiles": target["sourceFiles"],
                "sampleSourcePaths": evidence["sampleSourcePaths"],
                "payloadKeys": target["payloadKeys"],
                "derivedContext": target["derivedContext"],
                "sourceMeaning": target["sourceMeaning"],
                "policyDecision": target["policyDecision"],
                "missingHeadlessBoundary": target["missingHeadlessBoundary"],
                "nextFoundationTask": target["nextFoundationTask"],
                "cardEffectBodyBoundaryRetained": target["cardEffectBodyBoundaryRetained"],
                "evidenceNotes": evidence.get("notes", []),
                "implementationPerformed": False,
                "generatedStatusPromoted": False,
            }
        )

    counts = Counter(entry["currentMappingStatus"] for entry in entries)
    classification_counts = Counter(entry["fnd002CClassification"] for entry in entries)
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
        "schemaVersion": "dcgo.as-is-restart.fnd002c-digisorption-digiburst-timing-policy.v1",
        "generatedAt": generated_at,
        "goal": "FND-002-C",
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
        "schemaVersion": "dcgo.as-is-restart.fnd002c-next-action-queue.v1",
        "generatedAt": generated_at,
        "goal": "FND-002-C",
        "sourceMapping": OUTPUT_JSON.as_posix(),
        "summary": {
            "nextFoundationTaskCount": len(entries),
            "bodySpecificBoundaryRetainedCount": sum(1 for entry in entries if entry["cardEffectBodyBoundaryRetained"]),
            "note": "These tasks are source-known Unsupported common-mechanic blockers, not card-porting tasks.",
        },
        "nextFoundationTasks": [
            {
                "taskId": entry["nextFoundationTask"],
                "apiName": entry["apiName"],
                "classification": entry["fnd002CClassification"],
                "sourceMeaning": entry["sourceMeaning"],
                "policyDecision": entry["policyDecision"],
                "missingHeadlessBoundary": entry["missingHeadlessBoundary"],
                "candidateTestScope": [
                    "source payload key preservation",
                    "selection snapshot ordering",
                    "cost/source-trash lifecycle ordering",
                    "explicit unsupported until common runtime exists",
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
            entry["policyDecision"],
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
        "# FND-002-C Digisorption / DigiBurst Timing Policy",
        "",
        "이 문서는 FND-002-B 이후 남은 unknown common API 중 Digisorption/DigiBurst timing 2개를 원본 source에 맞춰 정책 분해한 evidence이다. 구현은 수행하지 않았다.",
        "",
        "## Scope",
        "",
        f"- AS-IS root: `{result['sourceRoot']}`",
        "- Source of Truth: `E:/headlessDCGO/DCGO/Assets`",
        "- Target timings: `WhenDigisorption`, `OnUseDigiburst`",
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
                "Original",
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
        "## Source Evidence And Policy",
        "",
        markdown_table(["API", "Source meaning", "Policy decision", "Missing headless boundary", "Source files"], detail_rows),
        "",
        "## Next Foundation Tasks",
        "",
        markdown_table(["Task", "API", "Boundary"], task_rows),
        "",
        "## Remaining FND-002 Range After FND-002-C",
        "",
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
        "# FND-002-C Digisorption / DigiBurst Timing Policy Summary",
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
        "추천 commit message: `docs: map FND-002 digisorption digiburst timings`",
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
