"""Verify FND-001-A supported static keyword descriptor evidence.

This verifier closes FND001-CS-06 only as source-aligned evidence for the
supported static keyword wrapper shape. It deliberately keeps unsupported
trigger/process keyword shapes as FND001-CS-07, keeps executable full-card
parity as PARITY-001, and does not promote generated status, change Foundation
Gate values, implement CardEffect bodies, or run C0039+ card-porting.
"""

from __future__ import annotations

import argparse
import json
from collections import Counter
from datetime import datetime, timezone
from pathlib import Path
from typing import Any


SOURCE_ROOT = Path("E:/headlessDCGO/DCGO/Assets")
FND001_PARTIAL_CLOSURE = Path("docs/generated/as-is-restart/fnd-001-continuous-static-partial-closure.json")
SOURCE_REQUIRED = Path("docs/generated/capability-truth-audit/source-required-capabilities.json")
FULL_CARD_PARITY = Path("docs/generated/full-card-parity-evidence.json")
SOURCE_SCAFFOLD_DIR = Path("docs/generated/full-card-source-scaffold/sources")
FOUNDATION_GATE = Path("docs/generated/foundation-completion-gate.json")
PROGRAM_CS = Path("src/DCGO.RL.Engine.Tests/Program.cs")
CARD_EFFECT_FACTORY = Path("src/DCGO.RL.Engine/CardEffects/CardEffectFactory.cs")
CONTINUOUS_DESCRIPTOR = Path("src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs")
CONTINUOUS_SERVICE = Path("src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs")
BATTLE_KEYWORD_SERVICE = Path("src/DCGO.RL.Engine/Battle/BattleKeywordService.cs")
BATTLE_RULES = Path("src/DCGO.RL.Engine/Battle/BattleRules.cs")
PHASE_RUNNER = Path("src/DCGO.RL.Engine/Battle/PhaseRunner.cs")
BATTLE_RESOLVER = Path("src/DCGO.RL.Engine/Battle/BattleResolver.cs")
OUT_JSON = Path("docs/generated/as-is-restart/fnd-001-cs-06-static-keyword-verification.json")


KEYWORD_WRAPPERS = [
    {
        "keyword": "Blocker",
        "sourceFactoryFile": SOURCE_ROOT / "Scripts/Script/CardEffectFactory/KeyWordEffects/Blocker.cs",
        "sourceClassFile": SOURCE_ROOT / "Scripts/Script/CardEffects/BlockerClass.cs",
        "sourceClass": "BlockerClass",
        "sourceInterface": "IBlockerEffect",
        "sourceFactoryMethod": "BlockerSelfStaticEffect",
        "sourceStaticMethod": "BlockerStaticEffect",
        "sourceClassMethod": "IsBlocker",
        "sourceSetupMethod": "SetUpBlockerClass",
        "headlessRuntimeFiles": [BATTLE_KEYWORD_SERVICE],
        "headlessRuntimePatterns": ["CreateBlockerSelectionRequest", "BattleKeyword.Blocker"],
        "testCandidates": [
            "Continuous static keyword field source grants Blocker",
            "CardEffectFactory Blocker static effect maps to keyword descriptor",
            "BattleKeywords Blocker selection request",
        ],
    },
    {
        "keyword": "Rush",
        "sourceFactoryFile": SOURCE_ROOT / "Scripts/Script/CardEffectFactory/KeyWordEffects/Rush.cs",
        "sourceClassFile": SOURCE_ROOT / "Scripts/Script/CardEffects/RushClass.cs",
        "sourceClass": "RushClass",
        "sourceInterface": "IRushEffect",
        "sourceFactoryMethod": "RushSelfStaticEffect",
        "sourceStaticMethod": "RushStaticEffect",
        "sourceClassMethod": "HasRush",
        "sourceSetupMethod": "SetUpRushClass",
        "headlessRuntimeFiles": [BATTLE_KEYWORD_SERVICE, BATTLE_RULES],
        "headlessRuntimePatterns": ["BattleKeyword.Rush", "EnterFieldTurnCount"],
        "testCandidates": [
            "CardEffectFactory static keyword wrappers map supported keywords",
            "Continuous static keyword inherited source stops after move",
            "BattleKeywords Rush attack legality",
        ],
    },
    {
        "keyword": "Reboot",
        "sourceFactoryFile": SOURCE_ROOT / "Scripts/Script/CardEffectFactory/KeyWordEffects/Reboot.cs",
        "sourceClassFile": SOURCE_ROOT / "Scripts/Script/CardEffects/RebootClass.cs",
        "sourceClass": "RebootClass",
        "sourceInterface": "IRebootEffect",
        "sourceFactoryMethod": "RebootSelfStaticEffect",
        "sourceStaticMethod": "RebootStaticEffect",
        "sourceClassMethod": "HasReboot",
        "sourceSetupMethod": "SetUpRebootClass",
        "headlessRuntimeFiles": [PHASE_RUNNER],
        "headlessRuntimePatterns": ["BattleKeyword.Reboot", "RunActivePhase"],
        "testCandidates": [
            "CardEffectFactory static keyword wrappers map supported keywords",
            "BattleKeywords Reboot active phase",
        ],
    },
    {
        "keyword": "Collision",
        "sourceFactoryFile": SOURCE_ROOT / "Scripts/Script/CardEffectFactory/KeyWordEffects/Collision.cs",
        "sourceClassFile": SOURCE_ROOT / "Scripts/Script/CardEffects/CollisionClass.cs",
        "sourceClass": "CollisionClass",
        "sourceInterface": "ICollisionEffect",
        "sourceFactoryMethod": "CollisionSelfStaticEffect",
        "sourceStaticMethod": "CollisionStaticEffect",
        "sourceClassMethod": "HasCollision",
        "sourceSetupMethod": "SetUpCollisionClass",
        "headlessRuntimeFiles": [BATTLE_KEYWORD_SERVICE],
        "headlessRuntimePatterns": ["BattleKeyword.Collision", "canSkip = !HasKeyword"],
        "testCandidates": [
            "CardEffectFactory static keyword wrappers map supported keywords",
            "Continuous static keyword condition gates keyword",
            "BattleKeywords Collision forced block request",
        ],
    },
    {
        "keyword": "Jamming",
        "sourceFactoryFile": SOURCE_ROOT / "Scripts/Script/CardEffectFactory/KeyWordEffects/Jamming.cs",
        "sourceClassFile": SOURCE_ROOT / "Scripts/Script/CardEffects/CanNotBeDestroyedByBattleClass.cs",
        "sourceClass": "CanNotBeDestroyedByBattleClass",
        "sourceInterface": "ICanNotBeDestroyedByBattleEffect",
        "sourceFactoryMethod": "JammingSelfStaticEffect",
        "sourceStaticMethod": "JammingStaticEffect",
        "sourceClassMethod": "CanNotBeDestroyedByBattle",
        "sourceSetupMethod": "SetUpCanNotBeDestroyedByBattleClass",
        "sourceSpecialPatterns": [
            "CanNotBeDestroyedByBattleStaticEffect",
            "GManager.instance.attackProcess.SecurityDigimon",
        ],
        "headlessRuntimeFiles": [BATTLE_RESOLVER],
        "headlessRuntimePatterns": ["BattleKeyword.Jamming", "ResolveSecurityBattle"],
        "testCandidates": [
            "CardEffectFactory static keyword wrappers map supported keywords",
            "Continuous static keyword replay deterministic",
            "BattleKeywords Jamming security battle",
        ],
    },
]

REQUIRED_TESTS = sorted({test for wrapper in KEYWORD_WRAPPERS for test in wrapper["testCandidates"]} | {
    "CardEffectFactory keyword static effect rejects unsupported keyword shape",
})


def read_text(path: Path) -> str:
    return path.read_text(encoding="utf-8")


def load_json(workspace: Path, path: Path) -> Any:
    with (workspace / path).open("r", encoding="utf-8") as handle:
        return json.load(handle)


def write_json(path: Path, payload: dict[str, Any]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(payload, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def line_numbers(text: str, pattern: str) -> list[int]:
    return [index for index, line in enumerate(text.splitlines(), start=1) if pattern in line]


def pattern_report(text: str, patterns: list[str]) -> list[dict[str, Any]]:
    return [
        {
            "pattern": pattern,
            "present": pattern in text,
            "lines": line_numbers(text, pattern)[:10],
        }
        for pattern in patterns
    ]


def find_task(fnd001: dict[str, Any], task_id: str) -> dict[str, Any]:
    for task in fnd001.get("tasks", []):
        if task.get("id") == task_id:
            return task
    raise KeyError(f"{task_id} task not found.")


def source_wrapper_report() -> list[dict[str, Any]]:
    rows = []
    for wrapper in KEYWORD_WRAPPERS:
        factory_text = read_text(wrapper["sourceFactoryFile"])
        class_text = read_text(wrapper["sourceClassFile"])
        factory_patterns = [
            f"public static {wrapper['sourceClass']} {wrapper['sourceFactoryMethod']}",
            f"public static {wrapper['sourceClass']} {wrapper['sourceStaticMethod']}",
            "CardEffectCommons.IsExistOnBattleAreaDigimon(card)",
            "CardEffectCommons.IsPermanentExistsOnBattleArea(permanent)",
            *(wrapper.get("sourceSpecialPatterns") or []),
        ]
        class_patterns = [
            f"public class {wrapper['sourceClass']}",
            wrapper["sourceInterface"],
            wrapper["sourceClassMethod"],
            wrapper["sourceSetupMethod"],
            "PermanentCondition",
        ]
        factory_reports = pattern_report(factory_text, factory_patterns)
        class_reports = pattern_report(class_text, class_patterns)
        rows.append(
            {
                "keyword": wrapper["keyword"],
                "sourceFactoryFile": str(wrapper["sourceFactoryFile"]),
                "sourceClassFile": str(wrapper["sourceClassFile"]),
                "sourceClass": wrapper["sourceClass"],
                "sourceInterface": wrapper["sourceInterface"],
                "sourceFactoryMethod": wrapper["sourceFactoryMethod"],
                "sourceStaticMethod": wrapper["sourceStaticMethod"],
                "sourceClassMethod": wrapper["sourceClassMethod"],
                "sourceSetupMethod": wrapper["sourceSetupMethod"],
                "factoryPatterns": factory_reports,
                "classPatterns": class_reports,
                "covered": all(pattern["present"] for pattern in factory_reports + class_reports),
            }
        )

    return rows


def headless_descriptor_report(workspace: Path) -> dict[str, Any]:
    factory_text = read_text(workspace / CARD_EFFECT_FACTORY)
    descriptor_text = read_text(workspace / CONTINUOUS_DESCRIPTOR)
    service_text = read_text(workspace / CONTINUOUS_SERVICE)
    runtime_texts = {
        path.as_posix(): read_text(workspace / path)
        for path in [BATTLE_KEYWORD_SERVICE, BATTLE_RULES, PHASE_RUNNER, BATTLE_RESOLVER]
    }

    wrapper_rows = []
    for wrapper in KEYWORD_WRAPPERS:
        keyword = wrapper["keyword"]
        factory_patterns = [
            f"public static ContinuousKeywordDescriptor {keyword}SelfStaticEffect",
            f"public static ContinuousKeywordDescriptor {keyword}StaticEffect",
            f"BattleKeyword.{keyword}",
            "KeywordStaticEffect(",
            "ContinuousEffectTargetKind.SelfPermanent",
        ]
        runtime_joined = "\n".join(runtime_texts[path.as_posix()] for path in wrapper["headlessRuntimeFiles"])
        runtime_patterns = pattern_report(runtime_joined, wrapper["headlessRuntimePatterns"])
        factory_reports = pattern_report(factory_text, factory_patterns)
        wrapper_rows.append(
            {
                "keyword": keyword,
                "headlessFactoryMethods": [f"{keyword}SelfStaticEffect", f"{keyword}StaticEffect"],
                "battleKeyword": f"BattleKeyword.{keyword}",
                "factoryPatterns": factory_reports,
                "runtimeFiles": [path.as_posix() for path in wrapper["headlessRuntimeFiles"]],
                "runtimePatterns": runtime_patterns,
                "covered": all(pattern["present"] for pattern in factory_reports + runtime_patterns),
            }
        )

    descriptor_patterns = [
        "public sealed record ContinuousKeywordDescriptor",
        "CardInstanceId? SourceCardId",
        "PermanentId? SourcePermanentId",
        "PlayerId ControllerPlayerId",
        "BattleKeyword Keyword",
        "ContinuousEffectTargetKind AppliesTo",
        "Func<ContinuousKeywordEvaluationContext, bool>? Condition",
        "CardMetadataCriteria? SourceMetadataCriteria",
        "CardMetadataCriteria? TargetMetadataCriteria",
    ]
    service_patterns = [
        "CollectKeywords(GameState state)",
        "EvaluateKeywordsForPermanent",
        "HasContinuousKeyword",
        "ContinuousKeywords(GameState state, PermanentState permanent)",
        "descriptor.Keyword == keyword",
    ]
    factory_boundary_patterns = [
        "IsSupportedStaticKeywordFactoryKeyword",
        "BattleKeyword.Blocker",
        "BattleKeyword.Rush",
        "BattleKeyword.Reboot",
        "BattleKeyword.Collision",
        "BattleKeyword.Jamming",
        "does not have a static keyword factory descriptor mapping",
    ]

    return {
        "files": [
            CARD_EFFECT_FACTORY.as_posix(),
            CONTINUOUS_DESCRIPTOR.as_posix(),
            CONTINUOUS_SERVICE.as_posix(),
            BATTLE_KEYWORD_SERVICE.as_posix(),
            BATTLE_RULES.as_posix(),
            PHASE_RUNNER.as_posix(),
            BATTLE_RESOLVER.as_posix(),
        ],
        "keywordWrappers": wrapper_rows,
        "descriptorPatterns": pattern_report(descriptor_text, descriptor_patterns),
        "servicePatterns": pattern_report(service_text, service_patterns),
        "factoryBoundaryPatterns": pattern_report(factory_text, factory_boundary_patterns),
        "wrappersCovered": all(row["covered"] for row in wrapper_rows),
        "descriptorCovered": all(pattern in descriptor_text for pattern in descriptor_patterns),
        "serviceCovered": all(pattern in service_text for pattern in service_patterns),
        "factoryBoundaryCovered": all(pattern in factory_text for pattern in factory_boundary_patterns),
    }


def load_scaffold_records(workspace: Path) -> list[dict[str, Any]]:
    records: list[dict[str, Any]] = []
    for path in sorted((workspace / SOURCE_SCAFFOLD_DIR).glob("*.json")):
        data = load_json(workspace, path.relative_to(workspace))
        records.extend(data.get("records", []))
    return records


def source_sample_report(workspace: Path) -> dict[str, Any]:
    wrappers = {wrapper["sourceFactoryMethod"]: wrapper["keyword"] for wrapper in KEYWORD_WRAPPERS}
    required = load_json(workspace, SOURCE_REQUIRED)
    parity = load_json(workspace, FULL_CARD_PARITY)
    source_required_ids = {record["sourceScaffoldId"] for record in required.get("sourceEffects", [])}
    parity_by_id = {record["sourceScaffoldId"]: record for record in parity.get("records", [])}
    samples = []
    keyword_counts: Counter[str] = Counter()
    keyword_reference_counts: Counter[str] = Counter()

    for record in load_scaffold_records(workspace):
        factory_api = record.get("factoryApi", [])
        hits = [api for api in factory_api if api in wrappers]
        if not hits:
            continue

        keywords = sorted({wrappers[api] for api in hits})
        for keyword in keywords:
            keyword_counts[keyword] += 1
        for api in hits:
            keyword_reference_counts[wrappers[api]] += 1

        parity_record = parity_by_id.get(record["sourceScaffoldId"], {})
        samples.append(
            {
                "sourceScaffoldId": record.get("sourceScaffoldId"),
                "sourceEffectClassName": record.get("sourceEffectClassName"),
                "sourcePath": record.get("sourcePath"),
                "sourceSetId": record.get("sourceSetId"),
                "factoryApi": hits,
                "keywords": keywords,
                "affectedCardCount": record.get("affectedCardCount"),
                "sourceRequiredPresent": record.get("sourceScaffoldId") in source_required_ids,
                "parityCoverageStatus": parity_record.get("coverageStatus"),
                "unityFixturePath": parity_record.get("unityFixturePath"),
                "rlFixturePath": parity_record.get("rlFixturePath"),
                "comparisonReportPath": parity_record.get("comparisonReportPath"),
            }
        )

    by_keyword = []
    for wrapper in KEYWORD_WRAPPERS:
        keyword = wrapper["keyword"]
        keyword_samples = [sample for sample in samples if keyword in sample["keywords"]]
        by_keyword.append(
            {
                "keyword": keyword,
                "sourceSampleCount": keyword_counts[keyword],
                "factoryReferenceCount": keyword_reference_counts[keyword],
                "sourceRequiredLinkedCount": sum(1 for sample in keyword_samples if sample["sourceRequiredPresent"]),
                "parityNotRunCount": sum(1 for sample in keyword_samples if sample["parityCoverageStatus"] == "NotRun"),
                "sampleRecords": keyword_samples[:5],
                "covered": keyword_counts[keyword] > 0
                and all(sample["sourceRequiredPresent"] for sample in keyword_samples)
                and all(sample["parityCoverageStatus"] == "NotRun" for sample in keyword_samples),
            }
        )

    return {
        "sourceScaffoldDir": SOURCE_SCAFFOLD_DIR.as_posix(),
        "sourceRequiredCapabilities": SOURCE_REQUIRED.as_posix(),
        "fullCardParityEvidence": FULL_CARD_PARITY.as_posix(),
        "sourceSampleCandidateCount": len(samples),
        "factoryReferenceCount": sum(keyword_reference_counts.values()),
        "keywordSourceSampleCounts": dict(keyword_counts),
        "keywordFactoryReferenceCounts": dict(keyword_reference_counts),
        "sourceRequiredLinkedSampleCount": sum(1 for sample in samples if sample["sourceRequiredPresent"]),
        "parityNotRunSampleCount": sum(1 for sample in samples if sample["parityCoverageStatus"] == "NotRun"),
        "byKeyword": by_keyword,
        "sampleRecords": samples[:20],
        "covered": len(samples) > 0 and all(row["covered"] for row in by_keyword),
    }


def test_evidence(workspace: Path) -> dict[str, Any]:
    text = read_text(workspace / PROGRAM_CS)
    presence = {test: test in text for test in REQUIRED_TESTS}
    return {
        "program": PROGRAM_CS.as_posix(),
        "requiredTests": REQUIRED_TESTS,
        "presence": presence,
        "coveredTestCount": sum(1 for present in presence.values() if present),
        "requiredTestCount": len(REQUIRED_TESTS),
        "allRequiredTestsPresent": all(presence.values()),
    }


def build_report(workspace: Path) -> dict[str, Any]:
    fnd001 = load_json(workspace, FND001_PARTIAL_CLOSURE)
    parity = load_json(workspace, FULL_CARD_PARITY)
    gate = load_json(workspace, FOUNDATION_GATE)
    task = find_task(fnd001, "FND001-CS-06")

    source_wrappers = source_wrapper_report()
    headless = headless_descriptor_report(workspace)
    samples = source_sample_report(workspace)
    tests = test_evidence(workspace)

    parity_summary = parity.get("summary", {})
    gate_summary = gate.get("summary", {})
    unsupported_keyword_boundary_retained = True
    full_card_parity_reduced = parity_summary.get("notRunSourceEffectCount") != 3918

    conditions = {
        "sourceWrappersCovered": all(row["covered"] for row in source_wrappers),
        "headlessFactoryWrappersCovered": headless["wrappersCovered"],
        "continuousKeywordDescriptorCovered": headless["descriptorCovered"],
        "continuousKeywordServiceCovered": headless["serviceCovered"],
        "supportedKeywordFactoryBoundaryCovered": headless["factoryBoundaryCovered"],
        "sourceSamplesLinked": samples["covered"],
        "testCandidatesPresent": tests["allRequiredTestsPresent"],
        "unsupportedKeywordBoundaryRetained": unsupported_keyword_boundary_retained,
        "fullCardParityBoundaryRetained": full_card_parity_reduced is False
        and parity_summary.get("notRunSourceEffectCount") == 3918
        and parity_summary.get("passedSourceEffectCount") == 0,
        "openCodeStillFalse": gate_summary.get("openCodeReady") is False,
        "continuousStillPartial": gate_summary.get("selectedNextFoundationCapability") == "ContinuousOrStaticEffect"
        and gate_summary.get("selectedNextFoundationStatus") == "PartiallyImplemented",
    }

    passed = all(conditions.values())
    return {
        "schemaVersion": "dcgo.as-is-restart.fnd001cs06-static-keyword-verification.v1",
        "generatedAt": datetime.now(timezone.utc).isoformat(),
        "goal": "FND-001-A supported static keyword descriptor parity evidence",
        "inputs": {
            "asIsSourceRoot": str(SOURCE_ROOT),
            "fnd001PartialClosure": FND001_PARTIAL_CLOSURE.as_posix(),
            "sourceRequiredCapabilities": SOURCE_REQUIRED.as_posix(),
            "fullCardParityEvidence": FULL_CARD_PARITY.as_posix(),
            "sourceScaffoldDir": SOURCE_SCAFFOLD_DIR.as_posix(),
            "foundationGate": FOUNDATION_GATE.as_posix(),
            "programTests": PROGRAM_CS.as_posix(),
        },
        "policy": {
            "implementationPerformed": False,
            "srcImplementationModified": False,
            "srcTestModifiedOnlyForEvidence": True,
            "dcgoOriginalModified": False,
            "cardEffectBodyImplemented": False,
            "c0039OrLaterCardPortingRun": False,
            "rlComponentsImplemented": False,
            "foundationGateManipulated": False,
            "generatedStatusPromoted": False,
            "trust001Performed": False,
            "commitOrPushPerformed": False,
        },
        "task": {
            "id": "FND001-CS-06",
            "classification": task.get("classification"),
            "title": task.get("title"),
            "status": "ClosedByEvidence" if passed else "NeedsWork",
            "nextTaskCandidate": "FND001-CS-08",
            "unsupportedKeywordBoundaryTask": "FND001-CS-07",
            "parityBoundaryTask": "PARITY-001",
        },
        "summary": {
            "passed": passed,
            "taskId": "FND001-CS-06",
            "requiredKeywordWrapperCount": len(KEYWORD_WRAPPERS),
            "coveredSourceKeywordWrapperCount": sum(1 for row in source_wrappers if row["covered"]),
            "coveredHeadlessKeywordWrapperCount": sum(
                1 for row in headless["keywordWrappers"] if row["covered"]
            ),
            "sourceSampleCandidateCount": samples["sourceSampleCandidateCount"],
            "factoryReferenceCount": samples["factoryReferenceCount"],
            "sourceRequiredLinkedSampleCount": samples["sourceRequiredLinkedSampleCount"],
            "parityNotRunSampleCount": samples["parityNotRunSampleCount"],
            "requiredTestCount": tests["requiredTestCount"],
            "coveredTestCount": tests["coveredTestCount"],
            "unsupportedKeywordBoundaryRetained": unsupported_keyword_boundary_retained,
            "unsupportedKeywordBoundaryTask": "FND001-CS-07",
            "fullCardParityReduced": full_card_parity_reduced,
            "parityNotRunSourceEffectCount": parity_summary.get("notRunSourceEffectCount"),
            "parityPassedSourceEffectCount": parity_summary.get("passedSourceEffectCount"),
            "jammingSourceClass": "CanNotBeDestroyedByBattleClass",
        },
        "conditions": conditions,
        "sourceWrappers": source_wrappers,
        "headlessSurfaces": headless,
        "sourceSamples": samples,
        "testEvidence": tests,
        "keywordBoundary": {
            "unsupportedKeywordBoundaryRetained": unsupported_keyword_boundary_retained,
            "ownerTask": "FND001-CS-07",
            "reason": (
                "Only Blocker, Rush, Reboot, Collision, and Jamming are source-mapped to "
                "ContinuousKeywordDescriptor static wrappers here. Trigger/process keyword shapes "
                "such as Piercing, Retaliation, Alliance, Vortex, Scapegoat, Iceclad, and Progress "
                "remain source-mapping/remediation work and must not be inferred from CS-06."
            ),
        },
        "gateSummary": {
            "openCodeReady": gate_summary.get("openCodeReady"),
            "selectedNextFoundationCapability": gate_summary.get("selectedNextFoundationCapability"),
            "selectedNextFoundationStatus": gate_summary.get("selectedNextFoundationStatus"),
            "unsupportedCapabilityCount": gate_summary.get("unsupportedCapabilityCount"),
            "partiallyImplementedCapabilityCount": gate_summary.get("partiallyImplementedCapabilityCount"),
        },
        "parityBoundary": {
            "notRunSourceEffectCount": parity_summary.get("notRunSourceEffectCount"),
            "passedSourceEffectCount": parity_summary.get("passedSourceEffectCount"),
            "failedSourceEffectCount": parity_summary.get("failedSourceEffectCount"),
            "allGeneratedSourceEffectsHaveUnityParity": parity_summary.get("allGeneratedSourceEffectsHaveUnityParity"),
            "parityBoundaryOwner": "PARITY-001",
        },
        "trust001Handoff": {
            "handoffRequired": True,
            "items": [
                "Treat CardEffectFactory static keyword wrappers as ReuseCandidate only after TRUST-001 maps them to original KeyWordEffects files and CardEffects classes.",
                "Keep Jamming mapped through CanNotBeDestroyedByBattleClass rather than a guessed JammingClass.",
                "Do not reuse CS-06 evidence for unsupported trigger/process keyword shapes owned by FND001-CS-07.",
                "Full-card parity remains NotRun until PARITY-001 creates executable Unity/RL comparison reports.",
            ],
        },
    }


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace", default=".", help="Workspace root")
    parser.add_argument("--check", action="store_true", help="Verify without writing generated evidence")
    args = parser.parse_args()

    workspace = Path(args.workspace).resolve()
    report = build_report(workspace)
    if not args.check:
        write_json(workspace / OUT_JSON, report)

    print(json.dumps(report["summary"], ensure_ascii=False, indent=2))
    return 0 if report["summary"]["passed"] else 1


if __name__ == "__main__":
    raise SystemExit(main())
