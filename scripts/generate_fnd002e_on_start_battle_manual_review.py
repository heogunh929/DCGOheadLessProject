# -*- coding: utf-8 -*-
"""Generate FND-002-E OnStartBattle manual review artifacts.

This is an evidence/policy generator only. It does not implement battle-start
runtime behavior, card effect bodies, card-porting work, or RL components.
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

OUTPUT_JSON = Path("docs/generated/as-is-restart/fnd-002-e-on-start-battle-manual-review.json")
OUTPUT_QUEUE_JSON = Path("docs/generated/as-is-restart/fnd-002-e-next-action-queue.json")
OUTPUT_DOC = Path("docs/as-is-restart/FND_002_E_ON_START_BATTLE_MANUAL_REVIEW.md")
OUTPUT_SUMMARY = Path("docs/as-is-restart/fnd-002-e-on-start-battle-manual-review-summary.md")

TARGET = {
    "apiName": "OnStartBattle",
    "sourceClassification": "BattleStartRuntimeDispatch",
    "inventoryClassification": "SourceKnownZeroCardTiming",
    "sourceFiles": [
        "DCGO/Assets/Scripts/Script/ICardEffect.cs:1011",
        "DCGO/Assets/Scripts/Script/CardController.cs:4527",
        "DCGO/Assets/Scripts/Script/CardController.cs:4542",
        "DCGO/Assets/Scripts/Script/CardController.cs:4549",
        "DCGO/Assets/Scripts/Script/CardController.cs:4553",
        "DCGO/Assets/Scripts/Script/CardController.cs:4554",
        "DCGO/Assets/Scripts/Script/CardController.cs:4555",
        "DCGO/Assets/Scripts/Script/CardController.cs:4557",
    ],
    "payloadKeys": [
        "AttackingPermanent",
        "DefendingPermanent",
        "DefendingCard",
    ],
    "sourceMeaning": (
        "The source dispatch occurs in CardController.Battle() after attacker/defender existence checks and before "
        "the battle auto-process/body comparison flow. The source stores snapshot Permanent copies for attacking and "
        "defending permanents, plus the defending security/card target when present."
    ),
    "policyDecision": (
        "Treat OnStartBattle as source-known NotReferenced for the current AS-IS card pool. The runtime dispatch and "
        "payload are real, but the current CardEffect corpus has zero EffectTiming.OnStartBattle references and zero "
        "affected card records. It should not keep the common API mapping gate open for the current source snapshot."
    ),
    "reopenCondition": (
        "If a future source snapshot contains any CardEffect file referencing EffectTiming.OnStartBattle, the "
        "conditional inventory override no longer applies and the timing must return as a battle-start payload "
        "foundation task with Unity/RL fixture coverage."
    ),
    "nextFoundationTask": "FND-002-E1",
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

    api_name = TARGET["apiName"]
    timings = {item["name"]: item for item in inventory["timings"]}
    current = timings[api_name]
    evidence = current["evidence"]
    rerun_entries = {item["apiName"]: item for item in fnd002_rerun["entries"]}
    original_entries = {item["apiName"]: item for item in original_fnd002["entries"]}
    previous = rerun_entries.get(api_name, {})
    original = original_entries.get(api_name, {})
    generated_at = datetime.now(timezone.utc).isoformat()

    entry = {
        "apiName": api_name,
        "originalFnd002MappingStatus": original.get("originalMappingStatus", "NeedsSourceReview"),
        "previousRerunMappingStatus": previous.get("currentMappingStatus"),
        "previousRerunClassification": previous.get("rerunClassification"),
        "currentMappingStatus": current["mappingStatus"],
        "currentCapabilityStatus": capability_status(capability_registry, api_name),
        "fnd002EClassification": TARGET["inventoryClassification"],
        "sourceClassification": TARGET["sourceClassification"],
        "affectedCardCount": current["affectedCardCount"],
        "affectedCards": current["affectedCards"],
        "cardEffectSourceFileCount": current["cardEffectSourceFileCount"],
        "cardEffectOccurrenceCount": current["cardEffectOccurrenceCount"],
        "sourceFileCount": current["sourceScopeFileCount"],
        "sourceOccurrenceCount": current["sourceScopeOccurrenceCount"],
        "engineSourceReferenceCount": evidence["engineSourceReferenceCount"],
        "engineTestOrDocReferenceCount": evidence["engineTestReferenceCount"],
        "sampleSourcePaths": evidence["sampleSourcePaths"],
        "sourceFiles": TARGET["sourceFiles"],
        "payloadKeys": TARGET["payloadKeys"],
        "sourceMeaning": TARGET["sourceMeaning"],
        "policyDecision": TARGET["policyDecision"],
        "reopenCondition": TARGET["reopenCondition"],
        "nextFoundationTask": TARGET["nextFoundationTask"],
        "evidenceNotes": evidence.get("notes", []),
        "implementationPerformed": False,
        "generatedStatusPromoted": False,
    }

    counts = Counter([entry["currentMappingStatus"]])
    classification_counts = Counter([entry["fnd002EClassification"]])
    gate_summary = gate["summary"]
    summary = {
        "targetCount": 1,
        "currentStatusCounts": dict(sorted(counts.items())),
        "classificationCounts": dict(sorted(classification_counts.items())),
        "movedFromNeedsSourceReviewToNotReferencedCount": 1
        if entry["originalFnd002MappingStatus"] == "NeedsSourceReview"
        and entry["currentMappingStatus"] == "NotReferenced"
        else 0,
        "currentCardEffectReferenceCount": entry["cardEffectOccurrenceCount"],
        "currentAffectedCardCount": entry["affectedCardCount"],
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
        "schemaVersion": "dcgo.as-is-restart.fnd002e-on-start-battle-manual-review.v1",
        "generatedAt": generated_at,
        "goal": "FND-002-E",
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
        "schemaVersion": "dcgo.as-is-restart.fnd002e-next-action-queue.v1",
        "generatedAt": generated_at,
        "goal": "FND-002-E",
        "sourceMapping": OUTPUT_JSON.as_posix(),
        "summary": {
            "closedCurrentNeedsSourceReviewCount": summary["movedFromNeedsSourceReviewToNotReferencedCount"],
            "sourceKnownZeroCardTimingCount": 1,
            "remainingImmediateFoundationTaskCount": 0,
            "reopenCondition": TARGET["reopenCondition"],
        },
        "closedItems": [entry],
        "futureReopenTasks": [
            {
                "taskId": TARGET["nextFoundationTask"],
                "status": "DeferredUntilSourceReferenceExists",
                "apiName": api_name,
                "reason": TARGET["reopenCondition"],
                "requiredEvidence": [
                    "CardEffect source reference to EffectTiming.OnStartBattle",
                    "battle-start Unity fixture with attacker/defender snapshot payload",
                    "headless battle-start payload/replay comparison",
                ],
            }
        ],
        "recommendedNextQueueGroups": [
            "FND-003 unsupported capability remediation continuation",
            "FND-001 ContinuousOrStaticEffect partial closure continuation",
            "PARITY-001-A Unity full-card fixture exporter scenario contract",
        ],
    }

    return result, queue, render_doc(result, queue), render_summary(result, queue)


def render_doc(result: dict[str, Any], queue: dict[str, Any]) -> str:
    summary = result["summary"]
    entry = result["entries"][0]
    rows = [
        [
            entry["apiName"],
            entry["originalFnd002MappingStatus"],
            entry["currentMappingStatus"],
            entry["fnd002EClassification"],
            entry["cardEffectOccurrenceCount"],
            entry["affectedCardCount"],
            entry["sourceOccurrenceCount"],
            entry["engineSourceReferenceCount"],
            entry["engineTestOrDocReferenceCount"],
        ]
    ]
    lines = [
        "# FND-002-E OnStartBattle Manual Review",
        "",
        "이번 문서는 `OnStartBattle`이 `NeedsSourceReview`로 남아 있던 이유를 원본 source 근거로 닫는다. 구현은 수행하지 않는다.",
        "",
        "## 결론",
        "",
        f"- AS-IS root: `{result['sourceRoot']}`",
        "- 분류: `SourceKnownZeroCardTiming`",
        "- 현재 mapping status: `NotReferenced`",
        "- 현재 CardEffect source reference: 0",
        "- 현재 affected card: 0",
        "- 정책: 현재 AS-IS 카드풀의 full-card completion blocker에서 제외한다.",
        "- 재개 조건: 미래 source snapshot에서 `EffectTiming.OnStartBattle`을 참조하는 CardEffect가 발견되면 battle-start payload foundation task로 다시 연다.",
        "",
        "## Source Evidence",
        "",
        *[f"- `{item}`" for item in entry["sourceFiles"]],
        "",
        "## Payload",
        "",
        *[f"- `{item}`" for item in entry["payloadKeys"]],
        "",
        "## Mapping Audit",
        "",
        markdown_table(
            [
                "API",
                "Original",
                "Current",
                "Classification",
                "CardEffect refs",
                "Affected",
                "Source refs",
                "Engine refs",
                "Test/doc refs",
            ],
            rows,
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
        "## Reopen Condition",
        "",
        entry["reopenCondition"],
        "",
        "## Gate Snapshot",
        "",
        f"- OpenCodeReady: {str(summary['foundationGateOpenCodeReady']).lower()}",
        f"- Unknown common API count: {summary['foundationGateUnknownCommonApiCount']}",
        f"- Unsupported capability count: {summary['foundationGateUnsupportedCapabilityCount']}",
        f"- PartiallyImplemented capability count: {summary['foundationGatePartiallyImplementedCapabilityCount']}",
        "",
        "## Guardrails",
        "",
        "- `src/DCGO.RL.Engine` implementation code was not modified.",
        "- Original `DCGO/Assets` was not modified.",
        "- No individual `CardEffect` body was implemented.",
        "- No C0039+ card-porting was run.",
        "- No RL Environment/Observation/Reward/Dataset/Trainer was implemented.",
        "- Generated card status was not promoted.",
        "- Foundation Gate numbers were recalculated from generated inventory, not edited by hand.",
        "- No commit/push was performed.",
    ]
    return "\n".join(lines) + "\n"


def render_summary(result: dict[str, Any], queue: dict[str, Any]) -> str:
    summary = result["summary"]
    lines = [
        "# FND-002-E OnStartBattle Manual Review Summary",
        "",
        f"- AS-IS root: `{result['sourceRoot']}`",
        "- Target: `OnStartBattle`",
        "- Classification: `SourceKnownZeroCardTiming`",
        f"- Current status: `{next(iter(summary['currentStatusCounts'].keys()))}`",
        f"- CardEffect references: {summary['currentCardEffectReferenceCount']}",
        f"- Affected cards: {summary['currentAffectedCardCount']}",
        f"- Unknown common API count: {summary['foundationGateUnknownCommonApiCount']}",
        f"- OpenCodeReady: {str(summary['foundationGateOpenCodeReady']).lower()}",
        "",
        "## Generated Files",
        "",
        f"- `{OUTPUT_DOC.as_posix()}`",
        f"- `{OUTPUT_SUMMARY.as_posix()}`",
        f"- `{OUTPUT_JSON.as_posix()}`",
        f"- `{OUTPUT_QUEUE_JSON.as_posix()}`",
        "",
        "추천 commit message: `docs: close FND-002 OnStartBattle zero-card review`",
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
