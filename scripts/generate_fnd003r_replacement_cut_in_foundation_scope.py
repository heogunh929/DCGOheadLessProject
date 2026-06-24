# -*- coding: utf-8 -*-
"""Generate FND-003-R replacement/cut-in foundation scope artifacts.

This generator records the source-aligned foundation boundary only. It does not
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
FND002A_MAPPING_PATH = Path("docs/generated/as-is-restart/fnd-002-a-replacement-cut-in-source-mapping.json")

OUTPUT_JSON = Path("docs/generated/as-is-restart/fnd-003-r-replacement-cut-in-foundation-scope.json")
OUTPUT_QUEUE_JSON = Path("docs/generated/as-is-restart/fnd-003-r-next-action-queue.json")
OUTPUT_DOC = Path("docs/as-is-restart/FND_003_R_REPLACEMENT_CUT_IN_FOUNDATION_SCOPE.md")
OUTPUT_SUMMARY = Path("docs/as-is-restart/fnd-003-r-replacement-cut-in-foundation-scope-summary.md")


TARGETS = [
    {
        "apiName": "WhenPermanentWouldBeDeleted",
        "methodEvidence": [
            "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:PermanentRemovalReplacementWindowRequest",
            "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:QueuePermanentRemovalReplacementWindow",
        ],
        "testEvidence": [
            "FND-003-R permanent removal replacement windows queue before mutation",
        ],
        "closedFoundationBoundary": "delete replacement window request and payload are queued before the target permanent leaves the field.",
        "remainingBoundary": "replacement body execution, cut-in continuation, target re-fix after selection, and actual destroy continuation remain follow-up work.",
    },
    {
        "apiName": "WhenRemoveField",
        "methodEvidence": [
            "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:PermanentRemovalReplacementWindowRequest",
            "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:QueuePermanentRemovalReplacementWindow",
        ],
        "testEvidence": [
            "FND-003-R permanent removal replacement windows queue before mutation",
        ],
        "closedFoundationBoundary": "generic would-remove-field replacement window request and payload are queued before field mutation.",
        "remainingBoundary": "willBeRemoveField target marking parity, replacement body execution, target re-fix, and actual movement continuation remain follow-up work.",
    },
    {
        "apiName": "WhenReturntoLibraryAnyone",
        "methodEvidence": [
            "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:PermanentRemovalReplacementWindowRequest",
            "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:QueuePermanentRemovalReplacementWindow",
        ],
        "testEvidence": [
            "FND-003-R permanent removal replacement windows queue before mutation",
        ],
        "closedFoundationBoundary": "return-to-library replacement window request and payload are queued before deck mutation.",
        "remainingBoundary": "deck top/bottom continuation, generic WhenRemoveField continuation ordering, target re-fix, and full-card parity remain follow-up work.",
    },
    {
        "apiName": "WhenUntapAnyone",
        "methodEvidence": [
            "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:UnsuspendReplacementWindowRequest",
            "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:QueueUnsuspendReplacementWindow",
        ],
        "testEvidence": [
            "FND-003-R unsuspend replacement window queues before mutation",
        ],
        "closedFoundationBoundary": "pre-unsuspend replacement window request and payload are queued while the target remains suspended.",
        "remainingBoundary": "replacement body execution, cannot-unsuspend policy, target re-fix, and actual unsuspend continuation remain follow-up work.",
    },
    {
        "apiName": "WhenWouldDigivolutionCardDiscarded",
        "methodEvidence": [
            "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:DigivolutionCardDiscardReplacementWindowRequest",
            "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:QueueDigivolutionCardDiscardReplacementWindow",
        ],
        "testEvidence": [
            "FND-003-R digivolution source discard replacement window queues before mutation",
        ],
        "closedFoundationBoundary": "pre-source-trash replacement window request and payload are queued before digivolution cards leave the source stack.",
        "remainingBoundary": "replacement body execution, source trash continuation, target re-fix, and full-card parity remain follow-up work.",
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


def fnd002a_entry(mapping: dict[str, Any], api_name: str) -> dict[str, Any]:
    for entry in mapping.get("entries", []):
        if entry.get("apiName") == api_name:
            return entry
    return {}


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
    return {
        "sourceEffectCount": len(matches),
        "sourceFileCount": len(source_paths),
        "affectedCardCount": len(affected_cards),
        "sourceEffectSamples": [
            {
                "sourceEffectClassName": entry.get("sourceEffectClassName"),
                "sourcePath": entry.get("sourcePath"),
                "affectedCardCount": entry.get("affectedCardCount", 0),
                "sampleCardData": entry.get("affectedCards", [])[:3],
            }
            for entry in matches[:3]
        ],
    }


def build(workspace: Path) -> tuple[dict[str, Any], dict[str, Any], str, str]:
    registry = load_json(workspace, CAPABILITY_REGISTRY_PATH)
    source_required = load_json(workspace, SOURCE_REQUIRED_PATH)
    inventory = load_json(workspace, FULL_MECHANIC_INVENTORY_PATH)
    gate = load_json(workspace, FOUNDATION_GATE_PATH)
    fnd002a = load_json(workspace, FND002A_MAPPING_PATH)
    generated_at = datetime.now(timezone.utc).isoformat()

    entries = []
    for target in TARGETS:
        api_name = target["apiName"]
        registry_item = capability_entry(registry, api_name)
        section, inventory_item = inventory_entry(inventory, api_name)
        source_mapping = fnd002a_entry(fnd002a, api_name)
        source_stats = source_required_stats(source_required, api_name)
        reconciled_status = registry_item.get("status", "Unknown")
        inventory_status = registry_item.get("inventoryStatus") or inventory_item.get("mappingStatus")
        entries.append(
            {
                "apiName": api_name,
                "sourceMappingInput": FND002A_MAPPING_PATH.as_posix(),
                "previousFnd002AStatus": source_mapping.get("currentMappingStatus", "Unsupported"),
                "currentInventoryStatus": inventory_status,
                "currentCapabilityStatus": reconciled_status,
                "fnd003RClassification": (
                    "SourceAlignedReplacementWindowPartial"
                    if reconciled_status == "PartiallyImplemented"
                    else "ManualReview"
                ),
                "affectedCardCount": registry_item.get("affectedCardCount", source_stats["affectedCardCount"]),
                "sourceFileCount": registry_item.get("sourceFileCount", source_stats["sourceFileCount"]),
                "sourceRequired": source_stats,
                "sourceEvidence": {
                    "sourceClassification": source_mapping.get("sourceClassification"),
                    "sourceMeaning": source_mapping.get("sourceMeaning"),
                    "sourceFiles": source_mapping.get("sourceFiles", []),
                    "payloadKeys": source_mapping.get("payloadKeys", []),
                    "sampleSourcePaths": source_mapping.get("sampleSourcePaths", []),
                    "missingHeadlessBoundaryBeforeFnd003R": source_mapping.get("missingHeadlessBoundary"),
                },
                "headlessEvidence": {
                    "methodEvidence": target["methodEvidence"],
                    "testEvidence": target["testEvidence"],
                    "closedFoundationBoundary": target["closedFoundationBoundary"],
                    "capabilityRegistryReason": registry_item.get("reason", ""),
                    "implementationEvidence": registry_item.get("implementationEvidence", []),
                    "testInventoryEvidence": registry_item.get("testEvidence", []),
                },
                "remainingBoundary": target["remainingBoundary"],
                "fullCardParityStatus": "NotRun",
                "verifiedPromotionAllowed": False,
                "generatedCardStatusPromotionAllowed": False,
            }
        )

    classification_counts = Counter(entry["fnd003RClassification"] for entry in entries)
    status_counts = Counter(entry["currentCapabilityStatus"] for entry in entries)
    gate_summary = gate["summary"]
    result = {
        "schemaVersion": "dcgo.as-is-restart.fnd003r-replacement-cut-in-foundation-scope.v1",
        "generatedAt": generated_at,
        "goal": "FND-003-R",
        "sourceRoot": "E:/headlessDCGO/DCGO/Assets",
        "inputs": {
            "capabilityRegistry": CAPABILITY_REGISTRY_PATH.as_posix(),
            "sourceRequiredCapabilities": SOURCE_REQUIRED_PATH.as_posix(),
            "fullMechanicInventory": FULL_MECHANIC_INVENTORY_PATH.as_posix(),
            "foundationGate": FOUNDATION_GATE_PATH.as_posix(),
            "fnd002AReplacementCutInSourceMapping": FND002A_MAPPING_PATH.as_posix(),
        },
        "policy": {
            "implementationPerformed": True,
            "srcImplementationModified": True,
            "dcgoOriginalModified": False,
            "cardEffectBodyImplemented": False,
            "generatedStatusPromoted": False,
            "foundationGateDirectlyManipulated": False,
            "c0039OrLaterCardPortingRun": False,
            "rlComponentsImplemented": False,
            "commitOrPushPerformed": False,
            "rawJsonOver100MiBAdded": False,
        },
        "summary": {
            "targetCount": len(entries),
            "classificationCounts": dict(sorted(classification_counts.items())),
            "currentCapabilityStatusCounts": dict(sorted(status_counts.items())),
            "movedFromUnsupportedToPartiallyImplementedCount": sum(
                1
                for entry in entries
                if entry["previousFnd002AStatus"] == "Unsupported"
                and entry["currentCapabilityStatus"] == "PartiallyImplemented"
            ),
            "closedBoundary": "replacement/cut-in request events are source-aligned and queued before state mutation.",
            "remainingBoundary": "replacement body execution, cut-in continuation, target re-fix, and full-card parity remain incomplete.",
            "foundationGateOpenCodeReady": gate_summary.get("openCodeReady"),
            "foundationGateUnknownCommonApiCount": gate_summary.get("unknownCommonApiCount"),
            "foundationGateUnsupportedCapabilityCount": gate_summary.get("unsupportedCapabilityCount"),
            "foundationGatePartiallyImplementedCapabilityCount": gate_summary.get("partiallyImplementedCapabilityCount"),
            "foundationGateFailedGateCount": gate_summary.get("failedGateCount"),
            "fullCardParityStatus": "NotRun",
            "generatedStatusCounts": gate_summary.get("generatedStatusCounts", {}),
            "inventorySha256": file_sha256(workspace / FULL_MECHANIC_INVENTORY_PATH),
            "capabilityRegistrySha256": file_sha256(workspace / CAPABILITY_REGISTRY_PATH),
            "implementationPerformed": True,
            "srcImplementationModified": True,
            "dcgoOriginalModified": False,
            "cardEffectBodyImplemented": False,
            "generatedStatusPromoted": False,
            "foundationGateDirectlyManipulated": False,
            "c0039OrLaterCardPortingRun": False,
            "rlComponentsImplemented": False,
        },
        "entries": entries,
    }

    next_queue = {
        "schemaVersion": "dcgo.as-is-restart.fnd003r-next-action-queue.v1",
        "generatedAt": generated_at,
        "source": OUTPUT_JSON.as_posix(),
        "policy": result["policy"],
        "nextRecommendedQueue": [
            {
                "id": "FND-003-R1",
                "classification": "NeedsCapabilityRemediation",
                "title": "replacement/cut-in continuation contract",
                "reason": "FND-003-R queues would events before mutation, but does not yet resume actual delete/return/source-trash/unsuspend continuation after replacement choices.",
                "blocked": False,
            },
            {
                "id": "FND-003-R2",
                "classification": "NeedsCapabilityRemediation",
                "title": "target re-fix and stale target policy",
                "reason": "Original CardController refixes targets after cut-in windows; headless currently records payload and request only.",
                "blocked": False,
            },
            {
                "id": "FND-003-S",
                "classification": "NeedsCapabilityRemediation",
                "title": "link lifecycle unsupported remediation",
                "reason": "Remaining unsupported link timings are WhenWouldLink, WhenLinked, and OnLinkCardDiscarded.",
                "blocked": False,
            },
            {
                "id": "FND-003-T",
                "classification": "NeedsCapabilityRemediation",
                "title": "Digisorption/DigiBurst unsupported remediation",
                "reason": "Remaining unsupported cost/mechanic timings are WhenDigisorption and OnUseDigiburst.",
                "blocked": False,
            },
            {
                "id": "PARITY-001-A",
                "classification": "NeedsSourceMapping",
                "title": "Unity full-card fixture exporter scenario contract",
                "reason": "All FND-003-R targets remain NotRun for full-card parity.",
                "blocked": False,
            },
        ],
    }

    doc = render_doc(result, next_queue)
    summary_doc = render_summary(result, next_queue)
    return result, next_queue, doc, summary_doc


def render_doc(result: dict[str, Any], next_queue: dict[str, Any]) -> str:
    summary = result["summary"]
    rows = [
        [
            entry["apiName"],
            entry["previousFnd002AStatus"],
            entry["currentCapabilityStatus"],
            entry["fnd003RClassification"],
            entry["affectedCardCount"],
            entry["remainingBoundary"],
        ]
        for entry in result["entries"]
    ]
    evidence_rows = [
        [
            entry["apiName"],
            "<br>".join(entry["sourceEvidence"]["sourceFiles"][:4]),
            "<br>".join(entry["headlessEvidence"]["methodEvidence"]),
            "<br>".join(entry["headlessEvidence"]["testEvidence"]),
        ]
        for entry in result["entries"]
    ]
    queue_rows = [
        [item["id"], item["classification"], item["title"], item["reason"]]
        for item in next_queue["nextRecommendedQueue"]
    ]
    lines = [
        "# FND-003-R Replacement / Cut-In Foundation Scope",
        "",
        "## 요약",
        "",
        f"- AS-IS root: `{result['sourceRoot']}`",
        f"- Target count: `{summary['targetCount']}`",
        f"- Classification counts: `{summary['classificationCounts']}`",
        f"- Current capability status counts: `{summary['currentCapabilityStatusCounts']}`",
        f"- Unsupported -> PartiallyImplemented 전환 수: `{summary['movedFromUnsupportedToPartiallyImplementedCount']}`",
        f"- Foundation Gate: `OpenCodeReady={summary['foundationGateOpenCodeReady']}`, `Unsupported={summary['foundationGateUnsupportedCapabilityCount']}`, `PartiallyImplemented={summary['foundationGatePartiallyImplementedCapabilityCount']}`",
        "- 이번 작업은 replacement/cut-in request window foundation만 구현한다. CardEffect body, C0039 이후 card-porting, generated status 승격은 수행하지 않는다.",
        "",
        "## 대상별 상태",
        "",
        markdown_table(
            ["API", "FND-002-A status", "현재 capability status", "분류", "Affected cards", "남은 범위"],
            rows,
        ),
        "",
        "## 원본 / Headless 근거",
        "",
        markdown_table(["API", "원본 source 근거", "headless 구현 근거", "테스트 근거"], evidence_rows),
        "",
        "## 닫힌 범위",
        "",
        "- `QueuePermanentRemovalReplacementWindow`는 permanent delete/remove-field/return-to-library would payload를 실제 field/deck mutation 전에 큐잉한다.",
        "- `QueueDigivolutionCardDiscardReplacementWindow`는 source card가 source stack에 남아 있는 상태에서 `WhenWouldDigivolutionCardDiscarded`를 큐잉한다.",
        "- `QueueUnsuspendReplacementWindow`는 permanent가 아직 suspended인 상태에서 `WhenUntapAnyone`을 큐잉한다.",
        "- 기존 post-event primitive인 `DestroyPermanent`, `ReturnPermanentToHandWithEvents`, `TrashDigivolutionCardsWithEvents`, `Unsuspend`의 의미는 유지한다.",
        "",
        "## 남은 blocker",
        "",
        "- replacement body 실행 및 선택 처리",
        "- cut-in 이후 실제 zone mutation continuation",
        "- original `willBeRemoveField` / target re-fix parity",
        "- source-trash / return-to-library / unsuspend continuation과 full-card parity",
        "",
        "## 다음 작업 후보",
        "",
        markdown_table(["ID", "분류", "제목", "이유"], queue_rows),
        "",
        "## 금지사항 확인",
        "",
        "- 원본 `DCGO/Assets` 수정 없음",
        "- 개별 `CardEffect` body 구현 없음",
        "- C0039 이후 card-porting 실행 없음",
        "- generated card status 승격 없음",
        "- Foundation Gate 수치 직접 조작 없음",
        "- RL 구성요소 구현 없음",
        "- commit/push 없음",
        "",
    ]
    return "\n".join(lines)


def render_summary(result: dict[str, Any], next_queue: dict[str, Any]) -> str:
    summary = result["summary"]
    targets = ", ".join(entry["apiName"] for entry in result["entries"])
    next_ids = ", ".join(item["id"] for item in next_queue["nextRecommendedQueue"][:4])
    return "\n".join(
        [
            "# FND-003-R Summary",
            "",
            f"- AS-IS root: `{result['sourceRoot']}`",
            f"- Targets: {targets}",
            f"- Classification counts: `{summary['classificationCounts']}`",
            f"- Current status counts: `{summary['currentCapabilityStatusCounts']}`",
            f"- Gate: `OpenCodeReady={summary['foundationGateOpenCodeReady']}`, `Unsupported={summary['foundationGateUnsupportedCapabilityCount']}`, `Partial={summary['foundationGatePartiallyImplementedCapabilityCount']}`",
            "- 닫힌 범위: pre-mutation replacement/cut-in request window와 source-aligned payload.",
            "- 남은 범위: replacement continuation, target re-fix, actual mutation continuation, full-card parity.",
            f"- 다음 후보: {next_ids}",
            "- 금지사항: DCGO 원본 수정 없음, CardEffect body 구현 없음, generated status 승격 없음, commit/push 없음.",
            "",
        ]
    )


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--workspace", default=".", help="Repository/workspace root")
    args = parser.parse_args()
    workspace = Path(args.workspace).resolve()

    result, next_queue, doc, summary_doc = build(workspace)
    write_json(workspace / OUTPUT_JSON, result)
    write_json(workspace / OUTPUT_QUEUE_JSON, next_queue)
    write_text(workspace / OUTPUT_DOC, doc)
    write_text(workspace / OUTPUT_SUMMARY, summary_doc)

    summary = result["summary"]
    print(
        "generated FND-003-R artifacts: "
        f"targets={summary['targetCount']} "
        f"classifications={summary['classificationCounts']} "
        f"unsupported={summary['foundationGateUnsupportedCapabilityCount']} "
        f"partial={summary['foundationGatePartiallyImplementedCapabilityCount']}"
    )


if __name__ == "__main__":
    main()
