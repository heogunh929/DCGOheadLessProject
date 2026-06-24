"""Build the PARITY-001 full-card parity fixture reduction plan.

This script is intentionally conservative. It does not generate Unity fixtures,
does not generate RL traces, does not compare traces, and does not reduce the
current NotRun count by itself. It defines the source-locked work queue and the
artifact contract required to reduce NotRun with real Unity/RL evidence later.
"""

from __future__ import annotations

import argparse
import json
from collections import Counter
from datetime import datetime, timezone
from pathlib import Path
from typing import Any


FULL_CARD_PARITY_PATH = Path("docs/generated/full-card-parity-evidence.json")
SOURCE_REQUIRED_PATH = Path("docs/generated/capability-truth-audit/source-required-capabilities.json")
SOURCE_SCAFFOLD_STATUS_PATH = Path("docs/generated/full-card-source-scaffold/status-registry.json")
PRIORITY_PATH = Path("docs/generated/as-is-restart/asis-implementation-priority-rewrite.json")
FND001_CLOSURE_PATH = Path("docs/generated/as-is-restart/fnd-001-continuous-static-partial-closure.json")
TRUST001_PATH = Path("docs/generated/as-is-restart/trust-001-src-implementation-trust-boundary.json")
FOUNDATION_GATE_PATH = Path("docs/generated/foundation-completion-gate.json")
TRACE_CONTRACT_PATH = Path("docs/rl-engine/parity-trace-contract.md")
COMPARER_DOC_PATH = Path("docs/rl-engine/parity-fixture-comparer.md")

OUT_JSON = Path("docs/generated/as-is-restart/parity-001-full-card-fixture-reduction-plan.json")
OUT_DOC = Path("docs/as-is-restart/PARITY_001_FULL_CARD_PARITY_REDUCTION_PLAN.md")
OUT_SUMMARY = Path("docs/as-is-restart/parity-001-full-card-parity-reduction-summary.md")


def load_json(workspace: Path, path: Path) -> Any:
    return json.loads((workspace / path).read_text(encoding="utf-8"))


def write_json(path: Path, payload: Any) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(payload, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def write_text(path: Path, content: str) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(content, encoding="utf-8")


def now_utc() -> str:
    return datetime.now(timezone.utc).replace(microsecond=0).isoformat()


def source_required_map(source_required: dict[str, Any]) -> dict[str, dict[str, Any]]:
    return {
        record["sourceScaffoldId"]: record
        for record in source_required.get("sourceEffects", [])
        if record.get("sourceScaffoldId")
    }


def priority_item(priority: dict[str, Any], item_id: str) -> dict[str, Any] | None:
    for item in priority.get("summary", {}).get("priorityItems", []):
        if item.get("id") == item_id:
            return item
    return None


def fnd_task(fnd001: dict[str, Any], task_id: str) -> dict[str, Any] | None:
    for task in fnd001.get("tasks", []):
        if task.get("id") == task_id:
            return task
    return None


def missing_artifacts(record: dict[str, Any]) -> list[str]:
    missing: list[str] = []
    if record.get("missingUnityFixture"):
        missing.append("UnityFixture")
    if record.get("missingRlFixture"):
        missing.append("RlFixture")
    if record.get("missingComparisonReport"):
        missing.append("ComparisonReport")
    return missing


def reduction_blocker(record: dict[str, Any]) -> str:
    if record.get("missingUnityFixture"):
        return "BlockedMissingUnityFixture"
    if record.get("missingRlFixture"):
        return "BlockedMissingRlFixture"
    if record.get("missingComparisonReport"):
        return "BlockedMissingComparisonReport"
    return "ComparisonReportAvailable"


def build_candidate(record: dict[str, Any], required_by_id: dict[str, dict[str, Any]]) -> dict[str, Any]:
    required = required_by_id.get(record["sourceScaffoldId"], {})
    affected_ids = record.get("affectedDefinitionStableIds", [])
    return {
        "sourceScaffoldId": record["sourceScaffoldId"],
        "sourceEffectClassName": record["sourceEffectClassName"],
        "sourcePath": record["sourcePath"],
        "sourceSetId": record.get("sourceSetId"),
        "scenarioId": record["scenarioId"],
        "scaffoldStatus": record.get("scaffoldStatus"),
        "coverageStatus": record["coverageStatus"],
        "coverageReason": record["coverageReason"],
        "reductionStatus": reduction_blocker(record),
        "missingArtifacts": missing_artifacts(record),
        "unityFixturePath": record["unityFixturePath"],
        "rlFixturePath": record["rlFixturePath"],
        "comparisonReportPath": record["comparisonReportPath"],
        "affectedCardCount": record.get("affectedCardCount", 0),
        "affectedDefinitionStableIds": affected_ids,
        "requiredCapabilities": required.get("requiredCapabilities", []),
        "nonVerifiedCapabilities": required.get("nonVerifiedCapabilities", []),
        "sourceLock": {
            "sourceScaffoldId": record["sourceScaffoldId"],
            "sourcePath": record["sourcePath"],
            "sourceEffectClassName": record["sourceEffectClassName"],
            "affectedDefinitionStableIds": affected_ids,
        },
        "nextReductionSteps": [
            "Create source-locked Unity parity fixture from the original Unity DCGO scenario.",
            "Create matching RL parity trace from the same scenario seed, decklist, and action sequence.",
            "Run the typed parity comparer and write the comparison report.",
            "Regenerate full-card parity evidence; only a Passed comparison report may reduce NotRun.",
        ],
        "portingAllowedByThisCandidate": False,
    }


def aggregate_records(records: list[dict[str, Any]], required_by_id: dict[str, dict[str, Any]]) -> dict[str, Any]:
    missing_counts = Counter()
    set_counts = Counter()
    scaffold_counts = Counter()
    required_capability_counts = Counter()
    non_verified_capability_counts = Counter()

    for record in records:
        if record.get("missingUnityFixture"):
            missing_counts["UnityFixture"] += 1
        if record.get("missingRlFixture"):
            missing_counts["RlFixture"] += 1
        if record.get("missingComparisonReport"):
            missing_counts["ComparisonReport"] += 1
        set_counts[str(record.get("sourceSetId") or "Unknown")] += 1
        scaffold_counts[str(record.get("scaffoldStatus") or "Unknown")] += 1
        required = required_by_id.get(record["sourceScaffoldId"], {})
        required_capability_counts.update(required.get("requiredCapabilities", []))
        non_verified_capability_counts.update(required.get("nonVerifiedCapabilities", []))

    return {
        "missingArtifactCounts": dict(sorted(missing_counts.items())),
        "sourceSetCounts": dict(sorted(set_counts.items())),
        "scaffoldStatusCounts": dict(sorted(scaffold_counts.items())),
        "topRequiredCapabilityCounts": [
            {"capabilityId": capability, "sourceEffectCount": count}
            for capability, count in required_capability_counts.most_common(30)
        ],
        "topNonVerifiedCapabilityCounts": [
            {"capabilityId": capability, "sourceEffectCount": count}
            for capability, count in non_verified_capability_counts.most_common(30)
        ],
    }


def build_plan(workspace: Path, candidate_limit: int) -> dict[str, Any]:
    parity = load_json(workspace, FULL_CARD_PARITY_PATH)
    source_required = load_json(workspace, SOURCE_REQUIRED_PATH)
    status_registry = load_json(workspace, SOURCE_SCAFFOLD_STATUS_PATH)
    priority = load_json(workspace, PRIORITY_PATH)
    fnd001 = load_json(workspace, FND001_CLOSURE_PATH)
    trust001 = load_json(workspace, TRUST001_PATH)
    gate = load_json(workspace, FOUNDATION_GATE_PATH)
    required_by_id = source_required_map(source_required)

    records = parity.get("records", [])
    not_run_records = [record for record in records if record.get("coverageStatus") == "NotRun"]
    sorted_not_run = sorted(
        not_run_records,
        key=lambda record: (
            str(record.get("sourceSetId") or ""),
            str(record.get("sourceEffectClassName") or ""),
            str(record.get("sourcePath") or ""),
        ),
    )
    candidate_records = sorted_not_run[:candidate_limit]
    candidates = [build_candidate(record, required_by_id) for record in candidate_records]

    summary = parity["summary"]
    aggregates = aggregate_records(not_run_records, required_by_id)
    priority_parity = priority_item(priority, "PARITY-001")
    cs07 = fnd_task(fnd001, "FND001-CS-07")
    cs14 = fnd_task(fnd001, "FND001-CS-14")
    gate_summary = gate.get("summary", {})

    source_required_candidate_count = sum(
        1 for candidate in candidates if candidate["sourceScaffoldId"] in required_by_id
    )
    all_source_locked = all(
        candidate.get("sourceScaffoldId")
        and candidate.get("sourcePath")
        and candidate.get("sourceEffectClassName")
        and isinstance(candidate.get("affectedDefinitionStableIds"), list)
        for candidate in candidates
    )

    return {
        "schemaVersion": "dcgo.as-is-restart.parity001-full-card-fixture-reduction-plan.v1",
        "generatedAtUtc": now_utc(),
        "goal": "PARITY-001 full-card parity evidence NotRun reduction framework",
        "asisSourceOfTruthRoot": gate_summary.get("localSourceRootPath", "E:\\headlessDCGO\\DCGO\\Assets"),
        "inputs": {
            "fullCardParityEvidence": FULL_CARD_PARITY_PATH.as_posix(),
            "sourceRequiredCapabilities": SOURCE_REQUIRED_PATH.as_posix(),
            "sourceScaffoldStatusRegistry": SOURCE_SCAFFOLD_STATUS_PATH.as_posix(),
            "implementationPriority": PRIORITY_PATH.as_posix(),
            "fnd001Closure": FND001_CLOSURE_PATH.as_posix(),
            "trust001Boundary": TRUST001_PATH.as_posix(),
            "foundationGate": FOUNDATION_GATE_PATH.as_posix(),
            "parityTraceContract": TRACE_CONTRACT_PATH.as_posix(),
            "parityFixtureComparerDoc": COMPARER_DOC_PATH.as_posix(),
        },
        "policy": {
            "implementationPerformed": False,
            "srcImplementationModified": False,
            "dcgoOriginalModified": False,
            "cardEffectBodyImplemented": False,
            "c0039OrLaterCardPortingRun": False,
            "rlEnvironmentObservationRewardDatasetTrainerImplemented": False,
            "generatedStatusPromoted": False,
            "foundationGateManipulated": False,
            "notRunCountsAsPass": False,
            "syntheticFixturesCountAsUnityParity": False,
            "manualParityPromotionAllowed": False,
            "fullCardParityReducedByThisPlan": False,
            "rawJsonOver100MiBAdded": False,
            "commitOrPushPerformed": False,
        },
        "summary": {
            "sourceEffectCount": summary["sourceEffectCount"],
            "affectedDefinitionCount": summary["affectedDefinitionCount"],
            "currentPassedSourceEffectCount": summary["passedSourceEffectCount"],
            "currentFailedSourceEffectCount": summary["failedSourceEffectCount"],
            "currentNotRunSourceEffectCount": summary["notRunSourceEffectCount"],
            "currentComparedSourceEffectCount": summary["comparedSourceEffectCount"],
            "candidateQueueCount": len(candidates),
            "candidateWindowLimit": candidate_limit,
            "sourceRequiredCoverageForCandidateCount": source_required_candidate_count,
            "candidateQueueSourceLocked": all_source_locked,
            "missingUnityFixtureCount": aggregates["missingArtifactCounts"].get("UnityFixture", 0),
            "missingRlFixtureCount": aggregates["missingArtifactCounts"].get("RlFixture", 0),
            "missingComparisonReportCount": aggregates["missingArtifactCounts"].get("ComparisonReport", 0),
            "firstExecutableReductionTargetCount": 0,
            "reductionRequiresUnityExporter": True,
            "fixtureContractReady": True,
            "parity001CompletionEvidenceDefined": True,
            "fullCardParityReducedByThisPlan": False,
            "openCodeReady": gate_summary.get("openCodeReady", False),
            "selectedNextFoundationCapability": gate_summary.get("selectedNextFoundationCapability"),
            "selectedNextFoundationStatus": gate_summary.get("selectedNextFoundationStatus"),
        },
        "currentParityEvidence": {
            "schemaVersion": parity.get("schemaVersion"),
            "coverageStatusCounts": summary.get("coverageStatusCounts", {}),
            "allGeneratedSourceEffectsHaveUnityParity": summary.get("allGeneratedSourceEffectsHaveUnityParity"),
            "generatedImplementedOrVerifiedCount": summary.get("generatedImplementedOrVerifiedCount"),
            "generatedSourceScaffoldStatusCounts": summary.get("generatedSourceScaffoldStatusCounts", {}),
        },
        "fixtureContract": {
            "traceSchemaVersion": "dcgo.parity.trace.v1",
            "requiredArtifactsPerSourceEffect": [
                {
                    "artifact": "UnityFixture",
                    "pathPattern": "docs/generated/parity-fixtures/unity/full-card-source/{scenarioSlug}.parity.json",
                    "producer": "Future Unity exporter running the original DCGO scenario",
                    "missingCoverageStatus": "NotRun",
                },
                {
                    "artifact": "RlFixture",
                    "pathPattern": "docs/generated/parity-fixtures/rl/full-card-source/{scenarioSlug}.parity.json",
                    "producer": "Future RL.Engine parity trace exporter for the same scenario",
                    "missingCoverageStatus": "NotRun",
                },
                {
                    "artifact": "ComparisonReport",
                    "pathPattern": "docs/generated/parity-fixtures/reports/full-card-source/{scenarioSlug}.comparison.json",
                    "producer": "ParityFixtureComparer",
                    "missingCoverageStatus": "NotRun",
                },
            ],
            "requiredSourceLockFields": [
                "sourceScaffoldId",
                "sourcePath",
                "sourceEffectClassName",
                "affectedDefinitionStableIds",
                "scenarioId",
            ],
            "statusTransitionRules": [
                {
                    "condition": "Unity fixture is missing",
                    "coverageStatus": "NotRun",
                    "mayReduceNotRun": False,
                },
                {
                    "condition": "Unity fixture exists but RL fixture is missing",
                    "coverageStatus": "NotRun",
                    "mayReduceNotRun": False,
                },
                {
                    "condition": "Unity/RL fixtures exist but comparison report is missing",
                    "coverageStatus": "NotRun",
                    "mayReduceNotRun": False,
                },
                {
                    "condition": "Comparison report exists with status Passed",
                    "coverageStatus": "Passed",
                    "mayReduceNotRun": True,
                },
                {
                    "condition": "Comparison report exists with status Failed",
                    "coverageStatus": "Failed",
                    "mayReduceNotRun": True,
                },
            ],
            "forbiddenEvidence": [
                "Synthetic fixture without Unity exporter output",
                "Manual status promotion in generated scaffold registry",
                "Treating NotRun as pass",
                "CardEffect body implementation while preparing parity fixtures",
            ],
        },
        "candidateSelectionPolicy": {
            "sortOrder": ["sourceSetId", "sourceEffectClassName", "sourcePath"],
            "candidateWindowLimit": candidate_limit,
            "candidateSelectionReason": "Stable first-window queue for source-locked Unity/RL fixture generation.",
            "doesNotImplementCardBodies": True,
            "doesNotAlterOriginalSource": True,
            "doesNotChangeCurrentParityCounts": True,
        },
        "aggregates": aggregates,
        "candidateQueue": candidates,
        "foundationBoundary": {
            "foundationGateSummary": gate_summary,
            "fnd001Cs07UnsupportedKeywordMapping": cs07,
            "fnd001Cs14OrderingBoundary": cs14,
            "needsSourceMappingTaskIds": [
                task.get("id")
                for task in fnd001.get("tasks", [])
                if task.get("classification") == "NeedsSourceMapping"
            ],
        },
        "trustBoundary": {
            "summary": trust001.get("summary", {}),
            "conclusion": trust001.get("conclusion", {}),
            "trust001RerunRecommended": True,
            "reason": "Parity fixtures should be compared against source-mapped reusable code only after current trust boundaries are refreshed.",
        },
        "priorityReference": priority_parity,
        "statusRegistryReference": {
            "implementedOrVerifiedCount": status_registry.get("implementedOrVerifiedCount", 0),
            "sourceScaffoldStatusCounts": status_registry.get("sourceScaffoldStatusCounts", {}),
        },
        "nextGoalCandidates": [
            {
                "id": "PARITY-001-A",
                "title": "Unity full-card fixture exporter scenario contract",
                "status": "ReadyToPlan",
                "reason": "No NotRun source effect can become comparable before a real Unity fixture exists.",
            },
            {
                "id": "PARITY-001-B",
                "title": "RL fixture generator for source-locked full-card scenarios",
                "status": "BlockedUntilUnityContract",
                "reason": "RL fixture must use the same seed, decklist, action sequence, and scenarioId as the Unity fixture.",
            },
            {
                "id": "PARITY-001-C",
                "title": "Comparison report batch runner and NotRun reduction audit",
                "status": "BlockedUntilFixturesExist",
                "reason": "Comparison report is the only allowed source for Passed/Failed parity coverage.",
            },
            {
                "id": "FND001-CS-07",
                "title": "Unsupported trigger/process keyword source mapping",
                "status": "ParallelFoundationPrerequisite",
                "reason": "Unsupported keyword static factory shapes must not be misclassified as static descriptor coverage.",
            },
            {
                "id": "TRUST-001-RERUN",
                "title": "Refresh src reuse boundary after foundation evidence",
                "status": "AfterParityPlan",
                "reason": "Reuse candidates need refreshed SourceOfTruth mapping evidence before implementation resumes.",
            },
        ],
    }


def render_full_doc(plan: dict[str, Any]) -> str:
    summary = plan["summary"]
    policy = plan["policy"]
    aggregates = plan["aggregates"]
    candidates = plan["candidateQueue"][:25]
    lines = [
        "# PARITY-001 Full-Card Parity Reduction Plan",
        "",
        "이 문서는 full-card parity evidence의 `NotRun 3918` 상태를 실제 Unity/RL fixture 근거로 줄이기 위한 source-locked 작업 기준선이다.",
        "이번 산출물은 구현이나 fixture 생성을 수행하지 않으며, `NotRun`을 `Passed`로 간주하지 않는다.",
        "",
        "## AS-IS Root",
        "",
        f"- Source of Truth: `{plan['asisSourceOfTruthRoot']}`",
        f"- Full-card source effects: {summary['sourceEffectCount']}",
        f"- Affected definitions: {summary['affectedDefinitionCount']}",
        "",
        "## Current Parity State",
        "",
        f"- Passed: {summary['currentPassedSourceEffectCount']}",
        f"- Failed: {summary['currentFailedSourceEffectCount']}",
        f"- NotRun: {summary['currentNotRunSourceEffectCount']}",
        f"- Compared: {summary['currentComparedSourceEffectCount']}",
        f"- Candidate queue window: {summary['candidateQueueCount']} / {summary['candidateWindowLimit']}",
        f"- First executable reduction target count in this plan: {summary['firstExecutableReductionTargetCount']}",
        f"- OpenCodeReady: `{str(summary['openCodeReady']).lower()}`",
        "",
        "## Policy",
        "",
        f"- Implementation performed: `{str(policy['implementationPerformed']).lower()}`",
        f"- DCGO original modified: `{str(policy['dcgoOriginalModified']).lower()}`",
        f"- CardEffect body implemented: `{str(policy['cardEffectBodyImplemented']).lower()}`",
        f"- C0039 or later card-porting run: `{str(policy['c0039OrLaterCardPortingRun']).lower()}`",
        f"- Generated status promoted: `{str(policy['generatedStatusPromoted']).lower()}`",
        f"- Foundation Gate manipulated: `{str(policy['foundationGateManipulated']).lower()}`",
        f"- NotRun counts as pass: `{str(policy['notRunCountsAsPass']).lower()}`",
        f"- Synthetic fixture counts as Unity parity: `{str(policy['syntheticFixturesCountAsUnityParity']).lower()}`",
        "",
        "## Required Artifact Contract",
        "",
        "각 source effect가 `NotRun`에서 벗어나려면 다음 세 파일이 모두 source lock과 동일한 `scenarioId`로 존재해야 한다.",
        "",
        "| Artifact | Path Pattern | Producer | Missing Status |",
        "| --- | --- | --- | --- |",
    ]
    for artifact in plan["fixtureContract"]["requiredArtifactsPerSourceEffect"]:
        lines.append(
            f"| {artifact['artifact']} | `{artifact['pathPattern']}` | {artifact['producer']} | `{artifact['missingCoverageStatus']}` |"
        )
    lines.extend(
        [
            "",
            "허용되는 상태 전이는 comparison report의 `Passed` 또는 `Failed` 결과가 존재할 때뿐이다. Unity fixture가 없으면 항상 `NotRun`이다.",
            "",
            "## Aggregates",
            "",
            "### Missing Artifacts",
            "",
        ]
    )
    for name, count in aggregates["missingArtifactCounts"].items():
        lines.append(f"- {name}: {count}")
    lines.extend(["", "### Source Set Counts", ""])
    for name, count in aggregates["sourceSetCounts"].items():
        lines.append(f"- {name}: {count}")
    lines.extend(["", "### Top Non-Verified Capabilities", ""])
    for row in aggregates["topNonVerifiedCapabilityCounts"][:15]:
        lines.append(f"- {row['capabilityId']}: {row['sourceEffectCount']}")
    lines.extend(
        [
            "",
            "## Candidate Queue Sample",
            "",
            "| # | Source Effect | Source Path | Affected | Missing | Reduction Status |",
            "| ---: | --- | --- | ---: | --- | --- |",
        ]
    )
    for index, candidate in enumerate(candidates, 1):
        missing = ", ".join(candidate["missingArtifacts"])
        lines.append(
            f"| {index} | `{candidate['sourceEffectClassName']}` | `{candidate['sourcePath']}` | "
            f"{candidate['affectedCardCount']} | {missing} | `{candidate['reductionStatus']}` |"
        )
    lines.extend(
        [
            "",
            "## Boundary And Handoff",
            "",
            "- `FND001-CS-07`: unsupported trigger/process keyword static factory mapping은 별도 source mapping 경계로 남긴다.",
            "- `FND001-CS-14`: strict Unity source ordering parity는 별도 source mapping 경계로 남긴다.",
            "- `TRUST-001-RERUN`: fixture 비교 전에 기존 src reuse/delete/manual-review 경계를 foundation evidence 기준으로 갱신한다.",
            "",
            "## Next Goal Candidates",
            "",
        ]
    )
    for candidate in plan["nextGoalCandidates"]:
        lines.append(f"- `{candidate['id']}`: {candidate['title']} - {candidate['status']}")
    lines.append("")
    return "\n".join(lines)


def render_summary(plan: dict[str, Any]) -> str:
    summary = plan["summary"]
    lines = [
        "# PARITY-001 Summary",
        "",
        f"- Source of Truth: `{plan['asisSourceOfTruthRoot']}`",
        f"- Full-card source effects: {summary['sourceEffectCount']}",
        f"- Current parity: Passed {summary['currentPassedSourceEffectCount']}, Failed {summary['currentFailedSourceEffectCount']}, NotRun {summary['currentNotRunSourceEffectCount']}",
        f"- Candidate queue: {summary['candidateQueueCount']}",
        f"- Candidate queue source-locked: `{str(summary['candidateQueueSourceLocked']).lower()}`",
        f"- Missing Unity fixtures: {summary['missingUnityFixtureCount']}",
        f"- Missing RL fixtures: {summary['missingRlFixtureCount']}",
        f"- Missing comparison reports: {summary['missingComparisonReportCount']}",
        f"- Reduced by this plan: `{str(summary['fullCardParityReducedByThisPlan']).lower()}`",
        f"- OpenCodeReady: `{str(summary['openCodeReady']).lower()}`",
        "",
        "다음 작업은 real Unity exporter fixture contract를 고정하는 `PARITY-001-A`다. 이 문서는 pass 판정을 만들지 않고, NotRun을 줄이기 위한 입력 조건만 고정한다.",
        "",
    ]
    return "\n".join(lines)


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace", default=".", help="Repository workspace root.")
    parser.add_argument("--candidate-limit", type=int, default=100, help="Number of source-locked NotRun candidates to emit.")
    args = parser.parse_args()

    if args.candidate_limit < 1:
        raise SystemExit("--candidate-limit must be positive.")

    workspace = Path(args.workspace).resolve()
    plan = build_plan(workspace, args.candidate_limit)
    write_json(workspace / OUT_JSON, plan)
    write_text(workspace / OUT_DOC, render_full_doc(plan))
    write_text(workspace / OUT_SUMMARY, render_summary(plan))
    print(json.dumps(plan["summary"], ensure_ascii=False, indent=2))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
