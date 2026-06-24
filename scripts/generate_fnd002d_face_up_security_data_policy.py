# -*- coding: utf-8 -*-
"""Generate FND-002-D face-up security data policy artifacts.

This is an evidence/policy generator only. It does not implement the
IFlipSecurity primitive, EX11-004 body behavior, or card-porting work.
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
CAPABILITY_REGISTRY_PATH = Path("docs/generated/capability-truth-audit/capability-registry.json")
FND002_RERUN_PATH = Path("docs/generated/as-is-restart/fnd-002-rerun-unknown-common-api-mapping.json")
ORIGINAL_FND002_PATH = Path("docs/generated/as-is-restart/fnd-002-unknown-common-api-mapping.json")

OUTPUT_JSON = Path("docs/generated/as-is-restart/fnd-002-d-face-up-security-data-policy.json")
OUTPUT_QUEUE_JSON = Path("docs/generated/as-is-restart/fnd-002-d-next-action-queue.json")
OUTPUT_DOC = Path("docs/as-is-restart/FND_002_D_FACE_UP_SECURITY_DATA_POLICY.md")
OUTPUT_SUMMARY = Path("docs/as-is-restart/fnd-002-d-face-up-security-data-policy-summary.md")

TARGET = {
    "apiName": "OnFaceUpSecurityIncreased",
    "sourceClassification": "FaceUpSecurityDataPolicy",
    "inventoryClassification": "SourceMappedDataPolicyPartial",
    "sourceFiles": [
        "DCGO/Assets/Scripts/Script/ICardEffect.cs:1030",
        "DCGO/Assets/Scripts/Script/CardController.cs:5494",
        "DCGO/Assets/Scripts/Script/CardController.cs:5506",
        "DCGO/Assets/Scripts/Script/CardController.cs:5516",
        "DCGO/Assets/Scripts/Script/CardController.cs:5529",
        "DCGO/Assets/Scripts/Script/CardController.cs:5532",
        "DCGO/Assets/Scripts/Script/CardController.cs:5548",
        "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnFaceUpSecurityIncrease.cs:11",
        "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnFaceUpSecurityIncrease.cs:13",
        "DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnFaceUpSecurityIncrease.cs:17",
        "DCGO/Assets/Scripts/Script/CardSource.cs:56",
        "DCGO/Assets/Scripts/Script/CardSource.cs:76",
        "DCGO/Assets/Scripts/Script/CardSource.cs:91",
        "DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_004.cs:15",
        "DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_004.cs:30",
    ],
    "headlessEvidence": [
        "src/DCGO.RL.Engine/Effects/EffectTiming.cs:66",
        "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:705",
        "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:821",
        "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:855",
        "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:858",
        "src/DCGO.RL.Engine.Tests/Program.cs:14968",
        "src/DCGO.RL.Engine.Tests/Program.cs:15043",
        "docs/rl-engine/on-add-security-foundation-scope-fnd003e.md",
    ],
    "payloadKeys": [
        "Player",
        "CardSources",
        "AddedSecurityCards",
        "SecurityCards",
        "SecurityCard",
        "SourceZone",
        "DestinationZone",
        "CardEffect",
        "SourceCard",
        "SourcePermanent",
        "ToTop",
        "FaceUp",
        "MoveReason",
    ],
    "sourceMeaning": (
        "The source event is raised when a card is added to security already face-up, or when an existing face-down "
        "security card is flipped face-up. The source hashtable carries Player and CardSources, and "
        "CanTriggerOnFaceUpSecurityIncreases checks player match plus at least one matching face-up security card."
    ),
    "policyDecision": (
        "Treat the timing as source-known PartiallyImplemented data-policy foundation. Headless AddSecurity already "
        "queues OnAddSecurity followed by OnFaceUpSecurityIncreased for face-up additions, but source-aligned "
        "IFlipSecurity conversion and EX11-004 full-card parity are not closed."
    ),
    "missingHeadlessBoundary": (
        "source-aligned FlipSecurity primitive/event, explicit face-up security increase fixture for flip-not-add, "
        "EX11-004 inherited draw body parity, and Unity/RL full-card replay comparison"
    ),
    "nextFoundationTask": "FND-002-D1",
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


def capability_status(registry: dict[str, Any], capability_id: str) -> str:
    for item in registry.get("capabilities", []):
        if item.get("capabilityId") == capability_id:
            return str(item.get("status", "Unknown"))
    return "Unknown"


def build(workspace: Path) -> tuple[dict[str, Any], dict[str, Any], str, str]:
    inventory = load_json(workspace, INVENTORY_PATH)
    gate = load_json(workspace, GATE_PATH)
    capability_registry = load_json(workspace, CAPABILITY_REGISTRY_PATH)
    fnd002_rerun = load_json(workspace, FND002_RERUN_PATH)
    original_fnd002 = load_json(workspace, ORIGINAL_FND002_PATH)

    timings = {item["name"]: item for item in inventory["timings"]}
    previous_entries = {item["apiName"]: item for item in fnd002_rerun["entries"]}
    original_entries = {item["apiName"]: item for item in original_fnd002["entries"]}
    api_name = TARGET["apiName"]
    current = timings[api_name]
    previous = previous_entries.get(api_name, {})
    original = original_entries.get(api_name, {})
    evidence = current["evidence"]
    generated_at = datetime.now(timezone.utc).isoformat()

    entry = {
        "apiName": api_name,
        "originalFnd002MappingStatus": original.get("originalMappingStatus", "NeedsSourceReview"),
        "currentRerunMappingStatus": previous.get("currentMappingStatus", "NeedsSourceReview"),
        "currentRerunClassification": previous.get("rerunClassification"),
        "currentMappingStatus": current["mappingStatus"],
        "currentCapabilityStatus": capability_status(capability_registry, api_name),
        "fnd002DClassification": TARGET["inventoryClassification"],
        "sourceClassification": TARGET["sourceClassification"],
        "affectedCardCount": current["affectedCardCount"],
        "affectedCards": current["affectedCards"],
        "sourceFileCount": current["sourceScopeFileCount"],
        "engineSourceReferenceCount": evidence["engineSourceReferenceCount"],
        "engineTestOrDocReferenceCount": evidence["engineTestReferenceCount"],
        "sourceFiles": TARGET["sourceFiles"],
        "headlessEvidence": TARGET["headlessEvidence"],
        "sampleSourcePaths": evidence["sampleSourcePaths"],
        "payloadKeys": TARGET["payloadKeys"],
        "sourceMeaning": TARGET["sourceMeaning"],
        "policyDecision": TARGET["policyDecision"],
        "missingHeadlessBoundary": TARGET["missingHeadlessBoundary"],
        "nextFoundationTask": TARGET["nextFoundationTask"],
        "evidenceNotes": evidence.get("notes", []),
        "implementationPerformed": False,
        "generatedStatusPromoted": False,
    }

    counts = Counter([entry["currentMappingStatus"]])
    classification_counts = Counter([entry["fnd002DClassification"]])
    gate_summary = gate["summary"]
    summary = {
        "targetCount": 1,
        "currentStatusCounts": dict(sorted(counts.items())),
        "classificationCounts": dict(sorted(classification_counts.items())),
        "movedFromNeedsSourceReviewToPartialCount": 1
        if entry["originalFnd002MappingStatus"] == "NeedsSourceReview"
        and entry["currentMappingStatus"] == "PartiallyImplemented"
        else 0,
        "foundationGateOpenCodeReady": gate_summary["openCodeReady"],
        "foundationGateUnknownCommonApiCount": gate_summary["unknownCommonApiCount"],
        "foundationGateUnsupportedCapabilityCount": gate_summary["unsupportedCapabilityCount"],
        "foundationGatePartiallyImplementedCapabilityCount": gate_summary["partiallyImplementedCapabilityCount"],
        "inventorySha256": file_sha256(workspace / INVENTORY_PATH),
        "capabilityRegistrySha256": file_sha256(workspace / CAPABILITY_REGISTRY_PATH),
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
        "schemaVersion": "dcgo.as-is-restart.fnd002d-face-up-security-data-policy.v1",
        "generatedAt": generated_at,
        "goal": "FND-002-D",
        "sourceRoot": "E:/headlessDCGO/DCGO/Assets",
        "inputs": {
            "fullMechanicInventory": INVENTORY_PATH.as_posix(),
            "foundationGate": GATE_PATH.as_posix(),
            "capabilityRegistry": CAPABILITY_REGISTRY_PATH.as_posix(),
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
        "entries": [entry],
    }

    queue = {
        "schemaVersion": "dcgo.as-is-restart.fnd002d-next-action-queue.v1",
        "generatedAt": generated_at,
        "goal": "FND-002-D",
        "sourceMapping": OUTPUT_JSON.as_posix(),
        "summary": {
            "nextFoundationTaskCount": 3,
            "note": "These tasks are source-known data-policy/foundation blockers, not card-porting tasks.",
        },
        "nextFoundationTasks": [
            {
                "taskId": "FND-002-D1",
                "apiName": api_name,
                "classification": "CloseableFoundationTask",
                "title": "Define source-aligned FlipSecurity primitive and event payload",
                "candidateTestScope": [
                    "face-down security flip queues OnFaceUpSecurityIncreased once",
                    "non-security or already face-up card does not queue the event",
                    "payload preserves Player/CardSources/FaceUp and source metadata",
                ],
                "prohibited": ["EX11-004 body implementation", "C0039+ card-porting", "generated status promotion"],
            },
            {
                "taskId": "FND-002-D2",
                "apiName": api_name,
                "classification": "NeedsParityFixture",
                "title": "Add source-locked Unity/RL fixture contract for EX11-004",
                "candidateTestScope": [
                    "opponent face-up security increase triggers owner-turn inherited draw candidate",
                    "face-down recovery does not trigger EX11-004",
                    "full-card parity remains NotRun until Unity fixture exists",
                ],
                "prohibited": ["CardEffect body implementation before gate", "synthetic parity pass"],
            },
            {
                "taskId": "FND-002-E",
                "apiName": "OnStartBattle",
                "classification": "BlockedNeedsManualReview",
                "title": "Manual review of remaining zero-card OnStartBattle timing",
                "candidateTestScope": ["source enum/call presence", "affected-card count 0", "gate blocker policy"],
                "prohibited": ["removing the enum without source review", "silent no-op"],
            },
        ],
    }

    return result, queue, render_doc(result, queue), render_summary(result)


def render_doc(result: dict[str, Any], queue: dict[str, Any]) -> str:
    summary = result["summary"]
    entry = result["entries"][0]
    affected_rows = [
        [
            card["definitionStableId"],
            card["cardId"],
            card["variantKey"],
            card["assetPath"],
        ]
        for card in entry["affectedCards"]
    ]
    evidence_rows = [
        [
            entry["apiName"],
            entry["originalFnd002MappingStatus"],
            entry["currentRerunMappingStatus"],
            entry["currentMappingStatus"],
            entry["currentCapabilityStatus"],
            entry["fnd002DClassification"],
        ]
    ]
    task_rows = [
        [task["taskId"], task["apiName"], task["classification"], task["title"]]
        for task in queue["nextFoundationTasks"]
    ]
    lines = [
        "# FND-002-D Face-Up Security Data Policy",
        "",
        "이 문서는 `OnFaceUpSecurityIncreased`를 원본 근거와 현재 headless 데이터 정책 기준으로 다시 분류한 evidence 산출물이다. 구현은 수행하지 않았다.",
        "",
        "## Scope",
        "",
        f"- AS-IS root: `{result['sourceRoot']}`",
        "- Target timing: `OnFaceUpSecurityIncreased`",
        "- Implementation performed: no",
        "- Generated status promoted: no",
        "",
        "## Count Audit",
        "",
        f"- Target count: {summary['targetCount']}",
        f"- Current status counts: {summary['currentStatusCounts']}",
        f"- Classification counts: {summary['classificationCounts']}",
        f"- Moved from NeedsSourceReview to PartiallyImplemented: {summary['movedFromNeedsSourceReviewToPartialCount']}",
        f"- Foundation Gate unknownCommonApiCount: {summary['foundationGateUnknownCommonApiCount']}",
        f"- Foundation Gate OpenCodeReady: {str(summary['foundationGateOpenCodeReady']).lower()}",
        "",
        "## Mapping Table",
        "",
        markdown_table(
            ["API", "Original", "Current rerun", "Current", "Capability", "Classification"],
            evidence_rows,
        ),
        "",
        "## Source Meaning",
        "",
        entry["sourceMeaning"],
        "",
        "## Policy Decision",
        "",
        entry["policyDecision"],
        "",
        "## Payload Keys",
        "",
        ", ".join(f"`{key}`" for key in entry["payloadKeys"]),
        "",
        "## Source Evidence",
        "",
        "\n".join(f"- `{path}`" for path in entry["sourceFiles"]),
        "",
        "## Current Headless Evidence",
        "",
        "\n".join(f"- `{path}`" for path in entry["headlessEvidence"]),
        "",
        "## Affected Card Data",
        "",
        markdown_table(["Definition", "Card", "Variant", "Asset"], affected_rows),
        "",
        "## Missing Boundary",
        "",
        entry["missingHeadlessBoundary"],
        "",
        "## Next Foundation Tasks",
        "",
        markdown_table(["Task", "API", "Classification", "Title"], task_rows),
        "",
        "## Remaining FND-002 Range After FND-002-D",
        "",
        "- `FND-002-E`: `OnStartBattle` manual review",
        "- `FND-003`: unsupported capability remediation continuation",
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
    entry = result["entries"][0]
    lines = [
        "# FND-002-D Face-Up Security Data Policy Summary",
        "",
        f"- AS-IS root: `{result['sourceRoot']}`",
        f"- Target API: `{entry['apiName']}`",
        f"- Current mapping status: `{entry['currentMappingStatus']}`",
        f"- Capability status: `{entry['currentCapabilityStatus']}`",
        f"- Classification: `{entry['fnd002DClassification']}`",
        f"- Affected card data count: {entry['affectedCardCount']}",
        f"- Foundation Gate unknownCommonApiCount: {summary['foundationGateUnknownCommonApiCount']}",
        f"- OpenCodeReady: {str(summary['foundationGateOpenCodeReady']).lower()}",
        "",
        "## Generated Files",
        "",
        f"- `{OUTPUT_DOC.as_posix()}`",
        f"- `{OUTPUT_SUMMARY.as_posix()}`",
        f"- `{OUTPUT_JSON.as_posix()}`",
        f"- `{OUTPUT_QUEUE_JSON.as_posix()}`",
        "",
        "추천 commit message: `docs: map FND-002 face-up security data policy`",
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
