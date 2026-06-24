"""Regenerate TRUST-001-RERUN source implementation trust boundary.

The rerun uses current src files plus the latest DATA/FND/PARITY/Foundation Gate
evidence. It does not modify implementation code, does not promote generated
status, and does not claim any local implementation is Verified.
"""

from __future__ import annotations

import argparse
import json
import re
from collections import Counter, defaultdict
from datetime import datetime, timezone
from pathlib import Path
from typing import Any


PREVIOUS_TRUST_PATH = Path("docs/generated/as-is-restart/trust-001-src-implementation-trust-boundary.json")
PREVIOUS_QUEUE_PATH = Path("docs/generated/as-is-restart/trust-001-reuse-candidate-queue.json")
DATA001_POLICY_PATH = Path("docs/generated/as-is-restart/data-001-card-base-entity-policy.json")
FND001_CLOSURE_PATH = Path("docs/generated/as-is-restart/fnd-001-continuous-static-partial-closure.json")
PARITY001_PLAN_PATH = Path("docs/generated/as-is-restart/parity-001-full-card-fixture-reduction-plan.json")
FOUNDATION_GATE_PATH = Path("docs/generated/foundation-completion-gate.json")
CAPABILITY_REGISTRY_PATH = Path("docs/generated/capability-truth-audit/capability-registry.json")
SOURCE_REQUIRED_PATH = Path("docs/generated/capability-truth-audit/source-required-capabilities.json")

OUT_JSON = Path("docs/generated/as-is-restart/trust-001-rerun-src-implementation-trust-boundary.json")
OUT_QUEUE = Path("docs/generated/as-is-restart/trust-001-rerun-reuse-candidate-queue.json")
OUT_DOC = Path("docs/as-is-restart/TRUST_001_RERUN_SRC_IMPLEMENTATION_TRUST_BOUNDARY.md")
OUT_SUMMARY = Path("docs/as-is-restart/trust-001-rerun-src-implementation-trust-boundary-summary.md")

CLASSIFICATIONS = (
    "ReuseCandidate",
    "VerifiedCandidateNeedsTest",
    "PartialNeedsWork",
    "StaleOrWrongMapping",
    "BlockedByFoundation",
    "BlockedByDataPolicy",
    "DeleteCandidate",
    "ManualReview",
)

CLASS_RE = re.compile(r"\b(?:class|record|struct|interface|enum)\s+([A-Za-z_][A-Za-z0-9_]*)")
SOURCE_EFFECT_TOKEN_RE = re.compile(r"\b(?:AD|BT|EX|LM|P|RB|ST)\d{0,2}_\d{3}\b")
STATUS_TOKEN_RE = re.compile(r"\b(?:Implemented|Verified|Unsupported|NoEffect|PartiallyImplemented|StubbedForValidation)\b")
TEST_TUPLE_RE = re.compile(r'^\s*\("([^"]+)",\s*[A-Za-z_][A-Za-z0-9_]*\),', re.MULTILINE)


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


def rel(path: Path) -> str:
    return path.as_posix()


def scan_src_files(workspace: Path) -> list[str]:
    return sorted(rel(path.relative_to(workspace)) for path in (workspace / "src").rglob("*.cs"))


def file_text(workspace: Path, relative_path: str) -> str:
    return (workspace / relative_path).read_text(encoding="utf-8", errors="replace")


def project_area(relative_path: str) -> str:
    if relative_path.startswith("src/DCGO.RL.Engine.Tests"):
        return "Tests"
    if relative_path.startswith("src/DCGO.RL.Engine"):
        return "Engine"
    if relative_path.startswith("src/DCGO.RL.Cli"):
        return "Cli"
    if relative_path.startswith("src/DCGO.RL.UnityAdapter"):
        return "UnityAdapter"
    return "Unknown"


def component(relative_path: str) -> str:
    parts = relative_path.split("/")
    if len(parts) < 3:
        return "Unknown"
    if parts[1] == "DCGO.RL.Engine.Tests":
        return "Tests"
    if len(parts) >= 4 and parts[1] == "DCGO.RL.Engine":
        return parts[2]
    return parts[1]


def source_effect_tokens(text: str) -> list[str]:
    return sorted(set(SOURCE_EFFECT_TOKEN_RE.findall(text)))


def status_tokens(text: str) -> list[str]:
    return sorted(set(STATUS_TOKEN_RE.findall(text)))


def class_names(text: str) -> list[str]:
    return sorted(set(CLASS_RE.findall(text)))


def forbidden_dependency_hits(text: str) -> list[str]:
    snippets = ("UnityEngine", "Photon", "MonoBehaviour", "GameObject", "Coroutine")
    return [snippet for snippet in snippets if snippet in text]


def premature_rl_hits(text: str) -> list[str]:
    snippets = ("RlEnvironment", "Observation", "Reward", "Dataset", "Trainer")
    return [snippet for snippet in snippets if snippet in text]


def new_record(relative_path: str, text: str) -> dict[str, Any]:
    file_name = relative_path.rsplit("/", 1)[-1]
    mapped_source_files: list[str] = []
    mapped_source_classes_methods: list[str] = []
    group = "ManualReview"
    classification = "ManualReview"
    source_mapping_kind = "NoSourceMapping"
    reason = "TRUST-001 이후 추가된 src 파일이므로 SourceOfTruth mapping 확인 전 수동 검토 대상으로 둔다."
    caveats = ["New file in TRUST-001-RERUN; not trusted as Verified."]

    if file_name == "CostPaymentRuleEventPayload.cs":
        group = "BattleRuntime"
        classification = "PartialNeedsWork"
        source_mapping_kind = "DirectTimingPipelineSource"
        mapped_source_files = [
            "E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardController.cs",
            "E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardSource.cs",
            "E:/headlessDCGO/DCGO/Assets/Scripts/Script/AutoProcessing.cs",
        ]
        mapped_source_classes_methods = [
            "BeforePayCost",
            "AfterPayCost",
            "PayCost payload",
        ]
        reason = "FND-003 cost timing payload evidence와 매핑되지만 special cost/payment parity가 남아 있어 부분 작업 후보로 둔다."
        caveats = [
            "Mapped to foundation timing evidence only.",
            "Does not prove card-specific cost body parity.",
        ]
    elif file_name == "DeclarationEffectService.cs":
        group = "BattleRuntime"
        classification = "PartialNeedsWork"
        source_mapping_kind = "DirectTimingPipelineSource"
        mapped_source_files = [
            "E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardSource.cs",
            "E:/headlessDCGO/DCGO/Assets/Scripts/Script/Permanent.cs",
            "E:/headlessDCGO/DCGO/Assets/Scripts/Script/TurnStateMachine.cs",
            "E:/headlessDCGO/DCGO/Assets/Scripts/Script/MainPhaseAction/ActivateCardAction.cs",
            "E:/headlessDCGO/DCGO/Assets/Scripts/Script/MainPhaseAction/ActivatePermanentAction.cs",
        ]
        mapped_source_classes_methods = [
            "CanDeclareSkillList",
            "ActivateCardAction",
            "ActivatePermanentAction",
            "EffectTiming.OnDeclaration",
        ]
        reason = "FND-003-P OnDeclaration source evidence와 매핑되지만 exact Unity action ordering/body parity가 남아 있어 부분 작업 후보로 둔다."
        caveats = [
            "Mapped to OnDeclaration foundation evidence only.",
            "Does not prove card-specific declaration body parity.",
        ]

    return {
        "relativePath": relative_path,
        "fileName": file_name,
        "projectArea": project_area(relative_path),
        "component": component(relative_path),
        "group": group,
        "classification": classification,
        "sourceMappingKind": source_mapping_kind,
        "mappedSourceFiles": mapped_source_files,
        "mappedSourceClassesMethods": mapped_source_classes_methods,
        "classNames": class_names(text),
        "sourceEffectClassNameTokens": source_effect_tokens(text),
        "cardEffectPortingStatusTokens": status_tokens(text),
        "matchedSourceEffectClassNames": [],
        "unmatchedLocalCardEffectClassNames": source_effect_tokens(text),
        "sourceEffectSamples": [],
        "forbiddenDependencyHits": forbidden_dependency_hits(text),
        "prematureRlComponentHits": premature_rl_hits(text),
        "trustReason": reason,
        "trustCaveats": caveats,
        "testCandidateEvidence": {
            "staticTestInventoryOnly": True,
            "candidateTestFile": "src/DCGO.RL.Engine.Tests/Program.cs",
            "matchingStaticTestNames": [],
        },
        "rerunDelta": {
            "status": "AddedSinceTRUST001",
            "previousClassification": None,
            "newClassification": classification,
        },
    }


def clone_previous_record(record: dict[str, Any], text: str) -> dict[str, Any]:
    copied = dict(record)
    copied["classNames"] = class_names(text)
    copied["sourceEffectClassNameTokens"] = source_effect_tokens(text)
    copied["cardEffectPortingStatusTokens"] = status_tokens(text)
    copied["forbiddenDependencyHits"] = forbidden_dependency_hits(text)
    copied["prematureRlComponentHits"] = premature_rl_hits(text)
    copied["rerunDelta"] = {
        "status": "UnchangedClassification",
        "previousClassification": record.get("classification"),
        "newClassification": record.get("classification"),
    }
    return copied


def apply_data001_reclassification(record: dict[str, Any]) -> None:
    old = record.get("classification")
    path = record["relativePath"]
    group = record.get("group")

    if group == "CardDataPolicy":
        if path.endswith("CardMetadataCriteria.cs"):
            record["classification"] = "PartialNeedsWork"
            record["trustReason"] = (
                "DATA-001 closed asset-level identity and EffectClassName policy, but effective metadata "
                "criteria still depends on FND001-CS-12/FND001-CS-13 data-policy follow-ups."
            )
            record["trustCaveats"] = [
                "DATA-001 policy is closed.",
                "Effective metadata/color/level runtime parity remains a foundation follow-up.",
                "Not trusted as Verified.",
            ]
        else:
            record["classification"] = "ReuseCandidate"
            record["trustReason"] = (
                "DATA-001 closed CardBaseEntity asset-level identity, CardIndex/variant key, and "
                "EffectClassName policy; this data model can be reused as a candidate pending importer/parity tests."
            )
            record["trustCaveats"] = [
                "ReuseCandidate only; not Verified.",
                "Importer and broad CardBaseEntity fixture tests remain follow-up work.",
            ]
        record["rerunDelta"] = {
            "status": "ReclassifiedByDATA001",
            "previousClassification": old,
            "newClassification": record["classification"],
        }

    if group == "CardEffectNoSourceToken":
        record["classification"] = "VerifiedCandidateNeedsTest"
        record["trustReason"] = (
            "DATA-001 confirms empty/no-source EffectClassName policy: these files are marker-only no-body "
            "candidates, not card effect implementations."
        )
        record["trustCaveats"] = [
            "Marker-only policy candidate; not a Verified implementation.",
            "Must stay body-free and must not promote generated status.",
        ]
        record["testCandidateEvidence"] = {
            "staticTestInventoryOnly": True,
            "candidateTestFile": "src/DCGO.RL.Engine.Tests/Program.cs",
            "matchingStaticTestNames": [
                "PortingStructure NoEffect files are marker only",
                "PortingStructure NoEffect asset conflicts are documented",
            ],
        }
        record["rerunDelta"] = {
            "status": "ReclassifiedByDATA001",
            "previousClassification": old,
            "newClassification": record["classification"],
        }


def reinforce_foundation_blockers(record: dict[str, Any], gate_summary: dict[str, Any], parity_summary: dict[str, Any]) -> None:
    if record.get("group") == "CardEffectLocalScript":
        record["classification"] = "BlockedByFoundation"
        record["trustReason"] = (
            "원본 CardEffect class token은 매칭될 수 있으나 generated full-card scaffold가 Unsupported이고 "
            f"full-card parity가 NotRun {parity_summary.get('currentNotRunSourceEffectCount', 3918)} 상태다."
        )
        record["trustCaveats"] = [
            "Local Implemented/Verified tokens are inventory only.",
            "CardEffect body trust requires OpenCodeReady=true and source-locked parity evidence.",
        ]
        record["rerunDelta"] = {
            "status": "FoundationBlockerRetained",
            "previousClassification": record.get("rerunDelta", {}).get("previousClassification", record.get("classification")),
            "newClassification": "BlockedByFoundation",
        }

    if record.get("classification") == "PartialNeedsWork":
        record.setdefault("trustCaveats", [])
        caveat = (
            f"Foundation Gate remains OpenCodeReady={gate_summary.get('openCodeReady')}; "
            f"unknown={gate_summary.get('unknownCommonApiCount')}, "
            f"unsupported={gate_summary.get('unsupportedCapabilityCount')}, "
            f"partial={gate_summary.get('partiallyImplementedCapabilityCount')}."
        )
        if caveat not in record["trustCaveats"]:
            record["trustCaveats"].append(caveat)


def static_test_names(workspace: Path) -> list[str]:
    path = workspace / "src/DCGO.RL.Engine.Tests/Program.cs"
    if not path.exists():
        return []
    return TEST_TUPLE_RE.findall(path.read_text(encoding="utf-8", errors="replace"))


def summarize(files: list[dict[str, Any]], previous_summary: dict[str, Any], gate: dict[str, Any], parity: dict[str, Any], tests: list[str]) -> dict[str, Any]:
    classifications = Counter(file["classification"] for file in files)
    for classification in CLASSIFICATIONS:
        classifications.setdefault(classification, 0)
    groups = Counter(file["group"] for file in files)
    mapping_kinds = Counter(file["sourceMappingKind"] for file in files)
    areas = Counter(file["projectArea"] for file in files)
    components = Counter(file["component"] for file in files)
    local_status_tokens = Counter(
        token
        for file in files
        for token in file.get("cardEffectPortingStatusTokens", [])
    )
    delta_status = Counter(file.get("rerunDelta", {}).get("status", "Unknown") for file in files)

    engine_count = sum(1 for file in files if file["projectArea"] == "Engine")
    test_count = sum(1 for file in files if file["projectArea"] == "Tests")

    card_script_files = [
        file
        for file in files
        if file["relativePath"].startswith("src/DCGO.RL.Engine/CardEffects/")
        and file["relativePath"].endswith(".cs")
    ]
    matched = {
        token
        for file in files
        for token in file.get("matchedSourceEffectClassNames", [])
    }
    unmatched = {
        token
        for file in files
        for token in file.get("unmatchedLocalCardEffectClassNames", [])
    }

    return {
        "previousSrcCSharpFileCount": previous_summary.get("srcCSharpFileCount"),
        "srcCSharpFileCount": len(files),
        "engineCSharpFileCount": engine_count,
        "testCSharpFileCount": test_count,
        "allSrcFilesClassified": sum(classifications.values()) == len(files),
        "classificationCounts": dict(sorted(classifications.items())),
        "groupCounts": dict(sorted(groups.items())),
        "sourceMappingKindCounts": dict(sorted(mapping_kinds.items())),
        "projectAreaCounts": dict(sorted(areas.items())),
        "componentCounts": dict(sorted(components.items())),
        "localStatusTokenCounts": dict(sorted(local_status_tokens.items())),
        "rerunDeltaCounts": dict(sorted(delta_status.items())),
        "addedSrcFileCount": delta_status.get("AddedSinceTRUST001", 0),
        "data001ReclassifiedFileCount": delta_status.get("ReclassifiedByDATA001", 0),
        "cardScriptFileCount": len(card_script_files),
        "matchedLocalCardEffectClassCount": len(matched),
        "unmatchedLocalCardEffectClassCount": len(unmatched),
        "staticTestCaseNamesParsed": len(tests),
        "testsExecutedInTrust001Rerun": False,
        "trustedAsVerifiedCount": 0,
        "nextImplementationStartPossible": False,
        "openCodeReady": gate.get("summary", {}).get("openCodeReady", False),
        "unknownCommonApiCount": gate.get("summary", {}).get("unknownCommonApiCount"),
        "unsupportedCapabilityCount": gate.get("summary", {}).get("unsupportedCapabilityCount"),
        "partiallyImplementedCapabilityCount": gate.get("summary", {}).get("partiallyImplementedCapabilityCount"),
        "parityPassedSourceEffectCount": parity.get("summary", {}).get("currentPassedSourceEffectCount"),
        "parityNotRunSourceEffectCount": parity.get("summary", {}).get("currentNotRunSourceEffectCount"),
        "foundationSelectedCapability": gate.get("summary", {}).get("selectedNextFoundationCapability"),
        "foundationSelectedStatus": gate.get("summary", {}).get("selectedNextFoundationStatus"),
    }


def group_rows(files: list[dict[str, Any]]) -> list[dict[str, Any]]:
    rows: list[dict[str, Any]] = []
    by_group: dict[str, list[dict[str, Any]]] = defaultdict(list)
    for file in files:
        by_group[file["group"]].append(file)
    for group, group_files in sorted(by_group.items()):
        classifications = Counter(file["classification"] for file in group_files)
        mapping_kinds = Counter(file["sourceMappingKind"] for file in group_files)
        mapped_sources = []
        for file in group_files:
            for source in file.get("mappedSourceFiles", []):
                if source not in mapped_sources:
                    mapped_sources.append(source)
        rows.append(
            {
                "group": group,
                "fileCount": len(group_files),
                "classificationCounts": dict(sorted(classifications.items())),
                "primaryClassification": classifications.most_common(1)[0][0],
                "primarySourceMappingKind": mapping_kinds.most_common(1)[0][0],
                "mappedSourceFiles": mapped_sources[:8],
                "sampleFiles": [file["relativePath"] for file in group_files[:10]],
            }
        )
    return rows


def queue_from_files(files: list[dict[str, Any]], summary: dict[str, Any]) -> dict[str, Any]:
    def item(file: dict[str, Any]) -> dict[str, Any]:
        return {
            "relativePath": file["relativePath"],
            "group": file["group"],
            "sourceMappingKind": file["sourceMappingKind"],
            "mappedSourceFiles": file.get("mappedSourceFiles", []),
            "localStatusTokens": file.get("cardEffectPortingStatusTokens", []),
            "reason": file.get("trustReason", ""),
            "rerunDelta": file.get("rerunDelta", {}),
        }

    by_class = {classification: [] for classification in CLASSIFICATIONS}
    for file in sorted(files, key=lambda row: row["relativePath"]):
        by_class[file["classification"]].append(item(file))

    return {
        "schemaVersion": "dcgo.trust-001-rerun-reuse-candidate-queue.v1",
        "generatedAtUtc": now_utc(),
        "goal": "TRUST-001-RERUN reuse candidate queue",
        "summary": {
            "reuseCandidateCount": summary["classificationCounts"]["ReuseCandidate"],
            "verifiedCandidateNeedsTestCount": summary["classificationCounts"]["VerifiedCandidateNeedsTest"],
            "partialNeedsWorkCount": summary["classificationCounts"]["PartialNeedsWork"],
            "blockedByFoundationCount": summary["classificationCounts"]["BlockedByFoundation"],
            "blockedByDataPolicyCount": summary["classificationCounts"]["BlockedByDataPolicy"],
            "deleteCandidateCount": summary["classificationCounts"]["DeleteCandidate"],
            "manualReviewCount": summary["classificationCounts"]["ManualReview"],
            "staleOrWrongMappingCount": summary["classificationCounts"]["StaleOrWrongMapping"],
            "trustedAsVerifiedCount": 0,
        },
        "reuseCandidates": by_class["ReuseCandidate"],
        "verifiedCandidateNeedsTest": by_class["VerifiedCandidateNeedsTest"],
        "partialNeedsWork": by_class["PartialNeedsWork"],
        "blockedByFoundation": by_class["BlockedByFoundation"],
        "blockedByDataPolicy": by_class["BlockedByDataPolicy"],
        "deleteCandidates": by_class["DeleteCandidate"],
        "manualReview": by_class["ManualReview"],
        "staleOrWrongMapping": by_class["StaleOrWrongMapping"],
        "notInScopeNow": [
            "src implementation changes",
            "CardEffect body implementation",
            "C0039+ card-porting",
            "generated status promotion",
            "Foundation Gate manipulation",
            "RL Environment/Observation/Reward/Dataset/Trainer",
        ],
    }


def build_report(workspace: Path) -> tuple[dict[str, Any], dict[str, Any]]:
    previous = load_json(workspace, PREVIOUS_TRUST_PATH)
    data001 = load_json(workspace, DATA001_POLICY_PATH)
    fnd001 = load_json(workspace, FND001_CLOSURE_PATH)
    parity001 = load_json(workspace, PARITY001_PLAN_PATH)
    gate = load_json(workspace, FOUNDATION_GATE_PATH)
    capability = load_json(workspace, CAPABILITY_REGISTRY_PATH)
    source_required = load_json(workspace, SOURCE_REQUIRED_PATH)

    previous_by_path = {file["relativePath"]: file for file in previous.get("files", [])}
    files: list[dict[str, Any]] = []
    added_paths: list[str] = []

    for relative_path in scan_src_files(workspace):
        text = file_text(workspace, relative_path)
        if relative_path in previous_by_path:
            record = clone_previous_record(previous_by_path[relative_path], text)
        else:
            record = new_record(relative_path, text)
            added_paths.append(relative_path)
        apply_data001_reclassification(record)
        reinforce_foundation_blockers(record, gate.get("summary", {}), parity001.get("summary", {}))
        files.append(record)

    tests = static_test_names(workspace)
    summary = summarize(files, previous.get("summary", {}), gate, parity001, tests)
    queue = queue_from_files(files, summary)
    missing_previous_paths = sorted(set(previous_by_path) - {file["relativePath"] for file in files})

    report = {
        "schemaVersion": "dcgo.trust-001-rerun-src-implementation-trust-boundary.v1",
        "generatedAtUtc": now_utc(),
        "goal": "TRUST-001-RERUN source implementation trust boundary refresh",
        "sourceOfTruthRoot": "E:/headlessDCGO/DCGO",
        "sourceOfTruthAssetsRoot": gate.get("summary", {}).get("localSourceRootPath", "E:/headlessDCGO/DCGO/Assets"),
        "inputs": {
            "previousTrust001": PREVIOUS_TRUST_PATH.as_posix(),
            "previousTrust001Queue": PREVIOUS_QUEUE_PATH.as_posix(),
            "data001Policy": DATA001_POLICY_PATH.as_posix(),
            "fnd001Closure": FND001_CLOSURE_PATH.as_posix(),
            "parity001Plan": PARITY001_PLAN_PATH.as_posix(),
            "foundationGate": FOUNDATION_GATE_PATH.as_posix(),
            "capabilityRegistry": CAPABILITY_REGISTRY_PATH.as_posix(),
            "sourceRequiredCapabilities": SOURCE_REQUIRED_PATH.as_posix(),
        },
        "policy": {
            "implementationPerformed": False,
            "srcImplementationModified": False,
            "dcgoOriginalModified": False,
            "cardEffectBodyImplemented": False,
            "c0039OrLaterCardPortingRun": False,
            "rlEnvironmentObservationRewardDatasetTrainerImplemented": False,
            "existingImplementationPromotedToVerified": False,
            "generatedStatusPromoted": False,
            "foundationGateManipulated": False,
            "testsExecutedInTrust001Rerun": False,
            "commitOrPushPerformed": False,
            "rawJsonOver100MiBAdded": False,
        },
        "classificationDefinitions": previous.get("classificationDefinitions", {}),
        "summary": summary,
        "rerunDelta": {
            "previousSrcFileCount": previous.get("summary", {}).get("srcCSharpFileCount"),
            "currentSrcFileCount": len(files),
            "addedSrcFiles": added_paths,
            "missingPreviousSrcFiles": missing_previous_paths,
            "data001PolicyClosed": True,
            "data001ReclassificationPolicy": {
                "CardBaseEntityDataSource": "ReuseCandidate except effective metadata criteria remains PartialNeedsWork",
                "CardEffectNoSourceToken": "VerifiedCandidateNeedsTest marker-only policy candidate",
            },
        },
        "evidenceRefresh": {
            "data001Summary": data001.get("summary", {}),
            "fnd001Progress": fnd001.get("progress", {}),
            "fnd001ClassificationCounts": fnd001.get("classificationCounts", {}),
            "parity001Summary": parity001.get("summary", {}),
            "foundationGateSummary": gate.get("summary", {}),
            "capabilityRegistrySummary": capability.get("summary", {}),
            "sourceRequiredSummary": source_required.get("summary", {}),
        },
        "featureGroups": group_rows(files),
        "files": sorted(files, key=lambda row: row["relativePath"]),
        "testInventory": {
            "staticTestInventoryOnly": True,
            "staticTestCaseNamesParsed": len(tests),
            "sampleTestNames": tests[:40],
            "executionCandidate": ".\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj",
            "testsExecutedInTrust001Rerun": False,
        },
        "openCodeReadyPolicy": {
            "openCodeReady": gate.get("summary", {}).get("openCodeReady", False),
            "c0039AndLaterCardPorting": "Deferred",
            "rlEnvironmentObservationRewardDatasetTrainer": "Deferred",
            "foundationGateNumbers": "Recalculation evidence only; not manually manipulated.",
        },
        "conclusion": {
            "existingImplementationTrust": "Do not trust as Verified.",
            "trustedAsVerifiedCount": 0,
            "nextImplementationStartPossible": False,
            "nextImplementationReason": (
                "OpenCodeReady=false, full-card parity remains NotRun, and foundation unknown/unsupported/partial blockers remain."
            ),
            "recommendedNextScopes": [
                "PARITY-001-A Unity full-card fixture exporter scenario contract",
                "FND001-CS-07 unsupported trigger/process keyword source mapping",
                "FND002 unknown common API mapping",
                "FND003 unsupported capability remediation continuation",
            ],
        },
    }

    return report, queue


def render_doc(report: dict[str, Any], queue: dict[str, Any]) -> str:
    summary = report["summary"]
    lines = [
        "# TRUST-001-RERUN Src Implementation Trust Boundary",
        "",
        "이 문서는 최신 DATA/FND/PARITY evidence 기준으로 기존 `src` 구현 신뢰 경계를 다시 계산한 결과다.",
        "구현, 원본 DCGO 수정, CardEffect body 구현, generated status 승격, Foundation Gate 조작은 수행하지 않았다.",
        "",
        "## Scope",
        "",
        f"- SourceOfTruth root: `{report['sourceOfTruthRoot']}`",
        f"- SourceOfTruth assets root: `{report['sourceOfTruthAssetsRoot']}`",
        f"- Previous src C# file count: {summary['previousSrcCSharpFileCount']}",
        f"- Current src C# file count: {summary['srcCSharpFileCount']}",
        f"- Engine/Test file count: {summary['engineCSharpFileCount']} / {summary['testCSharpFileCount']}",
        f"- OpenCodeReady: `{str(summary['openCodeReady']).lower()}`",
        f"- Full-card parity Passed/NotRun: {summary['parityPassedSourceEffectCount']} / {summary['parityNotRunSourceEffectCount']}",
        f"- Trusted as Verified: {summary['trustedAsVerifiedCount']}",
        "",
        "## Rerun Delta",
        "",
    ]
    delta = report["rerunDelta"]
    lines.append(f"- Added src files: {len(delta['addedSrcFiles'])}")
    for path in delta["addedSrcFiles"]:
        lines.append(f"  - `{path}`")
    lines.extend(
        [
            f"- Missing previous src files: {len(delta['missingPreviousSrcFiles'])}",
            f"- DATA-001 reclassified files: {summary['data001ReclassifiedFileCount']}",
            "",
            "## Classification Counts",
            "",
            "| Classification | Count |",
            "| --- | ---: |",
        ]
    )
    for classification, count in summary["classificationCounts"].items():
        lines.append(f"| {classification} | {count} |")
    lines.extend(
        [
            "",
            "## Key Queues",
            "",
            f"- ReuseCandidate: {queue['summary']['reuseCandidateCount']}",
            f"- VerifiedCandidateNeedsTest: {queue['summary']['verifiedCandidateNeedsTestCount']}",
            f"- PartialNeedsWork: {queue['summary']['partialNeedsWorkCount']}",
            f"- BlockedByFoundation: {queue['summary']['blockedByFoundationCount']}",
            f"- BlockedByDataPolicy: {queue['summary']['blockedByDataPolicyCount']}",
            f"- DeleteCandidate: {queue['summary']['deleteCandidateCount']}",
            f"- ManualReview: {queue['summary']['manualReviewCount']}",
            "",
            "## Evidence Refresh",
            "",
            f"- DATA-001 policy closed: `{str(report['rerunDelta']['data001PolicyClosed']).lower()}`",
            f"- FND-001 closed first-group closeable tasks: `{str(report['evidenceRefresh']['fnd001Progress'].get('closedAllFirstGroupCloseableFoundationTasks')).lower()}`",
            f"- PARITY-001 fixture contract ready: `{str(report['evidenceRefresh']['parity001Summary'].get('fixtureContractReady')).lower()}`",
            f"- Foundation selected capability: `{summary['foundationSelectedCapability']}` / `{summary['foundationSelectedStatus']}`",
            f"- Unknown/Unsupported/Partial blockers: {summary['unknownCommonApiCount']} / {summary['unsupportedCapabilityCount']} / {summary['partiallyImplementedCapabilityCount']}",
            "",
            "## Trust Conclusion",
            "",
            "- 기존 구현은 Verified로 신뢰하지 않는다.",
            "- SourceOfTruth mapping이 있는 파일도 `ReuseCandidate` 또는 `PartialNeedsWork` 후보일 뿐이다.",
            "- CardEffectLocalScript 185개는 full-card parity `NotRun 3918`과 generated `Unsupported` 상태 때문에 계속 `BlockedByFoundation`이다.",
            "- DATA-001 이후 CardBaseEntity data model 일부는 `ReuseCandidate`로 이동했지만 importer/parity 검증 전 Verified가 아니다.",
            "- 다음 구현 시작은 불가능하다. 먼저 foundation/parity/trust 후속을 닫아야 한다.",
            "",
            "## Generated Files",
            "",
            f"- `{OUT_DOC.as_posix()}`",
            f"- `{OUT_SUMMARY.as_posix()}`",
            f"- `{OUT_JSON.as_posix()}`",
            f"- `{OUT_QUEUE.as_posix()}`",
            "",
        ]
    )
    return "\n".join(lines)


def render_summary(report: dict[str, Any], queue: dict[str, Any]) -> str:
    summary = report["summary"]
    lines = [
        "# TRUST-001-RERUN Summary",
        "",
        f"- src C# file count: {summary['srcCSharpFileCount']} (previous {summary['previousSrcCSharpFileCount']})",
        f"- Added src files: {summary['addedSrcFileCount']}",
        f"- DATA-001 reclassified files: {summary['data001ReclassifiedFileCount']}",
        f"- OpenCodeReady: `{str(summary['openCodeReady']).lower()}`",
        f"- Full-card parity Passed/NotRun: {summary['parityPassedSourceEffectCount']} / {summary['parityNotRunSourceEffectCount']}",
        f"- Trusted as Verified: {summary['trustedAsVerifiedCount']}",
        "",
        "## Classification Counts",
        "",
    ]
    for classification, count in summary["classificationCounts"].items():
        lines.append(f"- {classification}: {count}")
    lines.extend(
        [
            "",
            "## Conclusion",
            "",
            "- 기존 src 구현은 Verified로 신뢰하지 않는다.",
            "- 다음 구현 시작 가능 여부: `false`",
            "- 다음 권장 작업: `PARITY-001-A`, `FND001-CS-07`, `FND002`, `FND003` 후속.",
            "",
            f"추천 commit message: `docs: refresh TRUST-001 src trust boundary`",
            "",
        ]
    )
    return "\n".join(lines)


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace", default=".", help="Repository workspace root.")
    args = parser.parse_args()

    workspace = Path(args.workspace).resolve()
    report, queue = build_report(workspace)

    if not report["summary"]["allSrcFilesClassified"]:
        raise SystemExit("Not all src files were classified.")
    if report["summary"]["trustedAsVerifiedCount"] != 0:
        raise SystemExit("TRUST-001-RERUN must not trust local implementation as Verified.")

    write_json(workspace / OUT_JSON, report)
    write_json(workspace / OUT_QUEUE, queue)
    write_text(workspace / OUT_DOC, render_doc(report, queue))
    write_text(workspace / OUT_SUMMARY, render_summary(report, queue))

    print(json.dumps(report["summary"], ensure_ascii=False, indent=2))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
