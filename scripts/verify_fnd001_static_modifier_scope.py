"""Verify FND-001-A static DP/SecurityAttack/SecurityDigimonDP evidence.

This verifier closes FND001-CS-08 only as source-aligned evidence for
continuous/static numeric modifier descriptors and runtime routing. It does
not implement source CardEffect bodies, does not promote generated status, does
not change Foundation Gate values, and does not run C0039+ card-porting.
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
BATTLE_RULES = Path("src/DCGO.RL.Engine/Battle/BattleRules.cs")
BATTLE_KEYWORD_SERVICE = Path("src/DCGO.RL.Engine/Battle/BattleKeywordService.cs")
SECURITY_CHECK_SERVICE = Path("src/DCGO.RL.Engine/Battle/SecurityCheckService.cs")
GAME_STATE = Path("src/DCGO.RL.Engine/Domain/GameState.cs")
OUT_JSON = Path("docs/generated/as-is-restart/fnd-001-cs-08-static-modifier-verification.json")


MODIFIER_GROUPS = [
    {
        "id": "DP",
        "title": "permanent DP continuous modifier",
        "sourceFactoryFile": SOURCE_ROOT / "Scripts/Script/CardEffectFactory/ChangeDP.cs",
        "sourceClassFile": SOURCE_ROOT / "Scripts/Script/CardEffects/ChangeDPClass.cs",
        "sourceClass": "ChangeDPClass",
        "sourceInterface": "IChangeDPEffect",
        "sourceMethods": ["ChangeSelfDPStaticEffect", "ChangeTargetDPStaticEffect", "ChangeDPStaticEffect"],
        "sampleFactoryApi": ["ChangeSelfDPStaticEffect", "ChangeTargetDPStaticEffect", "ChangeDPStaticEffect"],
        "sourcePatterns": [
            "typeof(Func<int>)",
            "if (isInt && (int)(object)changeValue == 0) return null;",
            "DP += _changeValue();",
            "permanent.TopCard.CanNotBeAffected(changeDPClass)",
            "SetIsInheritedEffect(isInheritedEffect)",
            "SetIsLinkedEffect(isLinkedEffect)",
        ],
        "classPatterns": [
            "public class ChangeDPClass",
            "IChangeDPEffect",
            "SetUpChangeDPClass",
            "public int GetDP",
            "PermanentCondition",
            "IsMinusDP",
        ],
        "headlessModifierKind": "DP",
        "headlessPatterns": [
            "ContinuousModifierKind.DP",
            "public int Dp(GameState state, PermanentState permanent)",
            "ContinuousPermanentModifierAmount(state, permanent, ContinuousModifierKind.DP)",
            "ChangeSelfDPStaticEffect",
            "ChangeDPStaticEffect",
            "DP change amount must not be zero.",
        ],
        "testCandidates": [
            "Continuous DP modifier affects effective DP",
            "Continuous DP modifier preserves base definition DP",
            "CardEffectFactory DP static effect maps to continuous descriptor",
            "CardEffectFactory DP static effect rejects zero amount",
        ],
    },
    {
        "id": "SecurityAttack",
        "title": "permanent/player SecurityAttack continuous modifier",
        "sourceFactoryFile": SOURCE_ROOT / "Scripts/Script/CardEffectFactory/ChangeSAttack.cs",
        "sourceClassFile": SOURCE_ROOT / "Scripts/Script/CardEffects/ChangeSAttackClass.cs",
        "sourceClass": "ChangeSAttackClass",
        "sourceInterface": "IChangeSAttackEffect",
        "sourceMethods": [
            "ChangeSelfSAttackStaticEffect",
            "ChangeTargetSAttackStaticEffect",
            "ChangeSAttackStaticEffect",
        ],
        "sampleFactoryApi": [
            "ChangeSelfSAttackStaticEffect",
            "ChangeTargetSAttackStaticEffect",
            "ChangeSAttackStaticEffect",
        ],
        "sourcePatterns": [
            "typeof(Func<int>)",
            "if (isInt && (int)(object)changeValue == 0) return null;",
            "SAttack += _changeValue();",
            "permanent.TopCard.CanNotBeAffected(changeSAttackClass)",
            "CalculateOrder.UpDownValue",
            "SetHashString(hashstring)",
        ],
        "classPatterns": [
            "public class ChangeSAttackClass",
            "IChangeSAttackEffect",
            "SetUpChangeSAttackClass",
            "public int GetSAttack",
            "PermanentCondition",
        ],
        "headlessModifierKind": "SecurityAttack",
        "headlessPatterns": [
            "ContinuousModifierKind.SecurityAttack",
            "SecurityAttackModifierAmount(GameState state, PermanentState permanent)",
            "ContinuousPermanentModifierAmount(state, permanent, ContinuousModifierKind.SecurityAttack)",
            "ContinuousPlayerModifierAmount(state, permanent.ControllerPlayerId, ContinuousModifierKind.SecurityAttack)",
            "SecurityAttackCount(GameState state, PermanentState permanent)",
        ],
        "testCandidates": [
            "Continuous dynamic SecurityAttack from source count",
            "Runtime composition primitive SecurityCheck uses continuous SecurityAttack",
            "BattleKeywords Security Attack extra checks",
        ],
    },
    {
        "id": "SecurityDigimonDP",
        "title": "security Digimon card DP continuous modifier",
        "sourceFactoryFile": SOURCE_ROOT / "Scripts/Script/CardEffectFactory/ChangeCardDP.cs",
        "sourceClassFile": SOURCE_ROOT / "Scripts/Script/CardEffects/ChangeCardDPClass.cs",
        "sourceClass": "ChangeCardDPClass",
        "sourceInterface": "IChangeCardDPEffect",
        "sourceMethods": ["ChangeSecurityDigimonCardDPStaticEffect"],
        "sampleFactoryApi": ["ChangeSecurityDigimonCardDPStaticEffect"],
        "sourcePatterns": [
            "typeof(Func<int>)",
            "if (isInt && (int)(object)changeValue == 0) return null;",
            "GManager.instance.attackProcess.SecurityDigimon == cardSource",
            "DP += _changeValue();",
            "SetIsInheritedEffect(isInheritedEffect)",
            "SetIsLinkedEffect(islinkedEffect)",
        ],
        "classPatterns": [
            "public class ChangeCardDPClass",
            "IChangeCardDPEffect",
            "SetUpChangeCardDPClass",
            "public int GetDP",
            "CardCondition",
            "IsMinusDP",
        ],
        "headlessModifierKind": "SecurityDigimonDP",
        "headlessPatterns": [
            "ContinuousModifierKind.SecurityDigimonDP",
            "public int SecurityDp(GameState state, CardInstanceId card)",
            "ContinuousPlayerModifierAmount(",
            "ContinuousModifierKind.SecurityDigimonDP",
            "_effectiveStats.SecurityDp(state, securityCard)",
        ],
        "testCandidates": [
            "Continuous trash source applies from trash",
            "Runtime composition primitive SecurityCheck uses continuous SecurityDigimonDP",
            "Duration security Digimon DP affects security battle",
        ],
    },
    {
        "id": "BaseDPBoundary",
        "title": "origin/base DP set-semantics boundary",
        "sourceFactoryFile": SOURCE_ROOT / "Scripts/Script/CardEffectFactory/ChangeOriginDP.cs",
        "sourceClassFile": SOURCE_ROOT / "Scripts/Script/CardEffects/ChangeBaseDPClass.cs",
        "sourceClass": "ChangeBaseDPClass",
        "sourceInterface": "IChangeBaseDPEffect",
        "sourceMethods": ["ChangeBaseDPStaticEffect", "ChangeBaseDPGlobalEffect"],
        "sampleFactoryApi": ["ChangeBaseDPStaticEffect", "ChangeBaseDPGlobalEffect"],
        "sourcePatterns": [
            "typeof(Func<int>)",
            "if (isInt && (int)(object)changeValue == 0) return null;",
            "DP = _changeValue();",
            "Origin DP is",
            "permanent.TopCard.CanNotBeAffected(changeBaseDPClass)",
            "SetUpChangeBaseDPClass",
        ],
        "classPatterns": [
            "public class ChangeBaseDPClass",
            "IChangeBaseDPEffect",
            "SetUpChangeBaseDPClass",
            "public int GetDP",
            "PermanentCondition",
            "IsMinusDP",
        ],
        "headlessModifierKind": "BoundaryOnly",
        "headlessPatterns": [
            "definition.Value.DP",
            "var dp = definition.DP;",
            "Continuous DP modifier preserves base definition DP",
            "Continuous effects are derived for state hash",
        ],
        "testCandidates": [
            "Continuous DP modifier preserves base definition DP",
            "Continuous effects are derived for state hash",
        ],
    },
]

REQUIRED_TESTS = sorted(
    {
        "Continuous and duration modifiers stack deterministically",
        *(test for group in MODIFIER_GROUPS for test in group["testCandidates"]),
    }
)


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


def source_modifier_report() -> list[dict[str, Any]]:
    rows = []
    for group in MODIFIER_GROUPS:
        factory_text = read_text(group["sourceFactoryFile"])
        class_text = read_text(group["sourceClassFile"])
        method_patterns = [f"public static {group['sourceClass']} {method}" for method in group["sourceMethods"]]
        factory_reports = pattern_report(factory_text, method_patterns + group["sourcePatterns"])
        class_reports = pattern_report(class_text, group["classPatterns"])
        rows.append(
            {
                "id": group["id"],
                "title": group["title"],
                "sourceFactoryFile": str(group["sourceFactoryFile"]),
                "sourceClassFile": str(group["sourceClassFile"]),
                "sourceClass": group["sourceClass"],
                "sourceInterface": group["sourceInterface"],
                "sourceMethods": group["sourceMethods"],
                "sampleFactoryApi": group["sampleFactoryApi"],
                "factoryPatterns": factory_reports,
                "classPatterns": class_reports,
                "covered": all(pattern["present"] for pattern in factory_reports + class_reports),
            }
        )

    return rows


def headless_surface_report(workspace: Path) -> dict[str, Any]:
    texts = {
        "CardEffectFactory": read_text(workspace / CARD_EFFECT_FACTORY),
        "ContinuousEffectDescriptor": read_text(workspace / CONTINUOUS_DESCRIPTOR),
        "ContinuousEffectService": read_text(workspace / CONTINUOUS_SERVICE),
        "BattleRules": read_text(workspace / BATTLE_RULES),
        "BattleKeywordService": read_text(workspace / BATTLE_KEYWORD_SERVICE),
        "SecurityCheckService": read_text(workspace / SECURITY_CHECK_SERVICE),
        "GameState": read_text(workspace / GAME_STATE),
        "Program": read_text(workspace / PROGRAM_CS),
    }
    joined = "\n".join(texts.values())

    descriptor_patterns = [
        "public enum ContinuousModifierKind",
        "DP,",
        "SecurityAttack,",
        "SecurityDigimonDP,",
        "Func<ContinuousEffectEvaluationContext, int> Amount",
        "Func<ContinuousEffectEvaluationContext, bool>? Condition",
        "CardMetadataCriteria? SourceMetadataCriteria",
        "CardMetadataCriteria? TargetMetadataCriteria",
    ]
    service_patterns = [
        "PermanentModifierAmount(GameState state, PermanentState target, ContinuousModifierKind modifierKind)",
        "PlayerModifierAmount(GameState state, PlayerId targetPlayer, ContinuousModifierKind modifierKind)",
        "EvaluateForPermanent",
        "EvaluateForPlayer",
        "DistinctBy(evaluation => evaluation.Descriptor.StableId)",
        "OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)",
    ]
    dynamic_amount_patterns = [
        "Func<ContinuousEffectEvaluationContext, int> changeValue",
        "ArgumentNullException.ThrowIfNull(changeValue)",
        "AmountStableToken(changeValue)",
        "amountStableToken",
        "dynamic",
    ]
    base_definition_patterns = [
        "definition.Value.DP",
        "var dp = definition.DP;",
        "Continuous DP modifier preserves base definition DP",
        "Continuous effects are derived for state hash",
    ]

    group_rows = []
    for group in MODIFIER_GROUPS:
        if group["id"] == "BaseDPBoundary":
            group_text = texts["GameState"] + "\n" + texts["BattleRules"] + "\n" + texts["Program"]
        else:
            group_text = joined
        patterns = pattern_report(group_text, group["headlessPatterns"])
        group_rows.append(
            {
                "id": group["id"],
                "title": group["title"],
                "headlessModifierKind": group["headlessModifierKind"],
                "patterns": patterns,
                "covered": all(pattern["present"] for pattern in patterns),
            }
        )

    runtime_kind_rows = [
        row for row in group_rows if row["headlessModifierKind"] in {"DP", "SecurityAttack", "SecurityDigimonDP"}
    ]

    return {
        "files": [
            CARD_EFFECT_FACTORY.as_posix(),
            CONTINUOUS_DESCRIPTOR.as_posix(),
            CONTINUOUS_SERVICE.as_posix(),
            BATTLE_RULES.as_posix(),
            BATTLE_KEYWORD_SERVICE.as_posix(),
            SECURITY_CHECK_SERVICE.as_posix(),
            GAME_STATE.as_posix(),
        ],
        "modifierGroups": group_rows,
        "descriptorPatterns": pattern_report(texts["ContinuousEffectDescriptor"], descriptor_patterns),
        "servicePatterns": pattern_report(texts["ContinuousEffectService"], service_patterns),
        "dynamicAmountPatterns": pattern_report(texts["CardEffectFactory"], dynamic_amount_patterns),
        "baseDefinitionBoundaryPatterns": pattern_report(
            texts["GameState"] + "\n" + texts["BattleRules"] + "\n" + texts["Program"],
            base_definition_patterns,
        ),
        "runtimeModifierKindsCovered": all(row["covered"] for row in runtime_kind_rows),
        "descriptorCovered": all(pattern in texts["ContinuousEffectDescriptor"] for pattern in descriptor_patterns),
        "serviceCovered": all(pattern in texts["ContinuousEffectService"] for pattern in service_patterns),
        "dynamicAmountCovered": all(pattern in texts["CardEffectFactory"] for pattern in dynamic_amount_patterns),
        "baseDefinitionBoundaryCovered": all(
            pattern in (texts["GameState"] + "\n" + texts["BattleRules"] + "\n" + texts["Program"])
            for pattern in base_definition_patterns
        ),
    }


def load_scaffold_records(workspace: Path) -> list[dict[str, Any]]:
    records: list[dict[str, Any]] = []
    for path in sorted((workspace / SOURCE_SCAFFOLD_DIR).glob("*.json")):
        data = load_json(workspace, path.relative_to(workspace))
        records.extend(data.get("records", []))
    return records


def source_sample_report(workspace: Path) -> dict[str, Any]:
    method_to_group = {
        method: group["id"]
        for group in MODIFIER_GROUPS
        for method in group["sampleFactoryApi"]
    }
    required = load_json(workspace, SOURCE_REQUIRED)
    parity = load_json(workspace, FULL_CARD_PARITY)
    source_required_ids = {record["sourceScaffoldId"] for record in required.get("sourceEffects", [])}
    parity_by_id = {record["sourceScaffoldId"]: record for record in parity.get("records", [])}
    samples = []
    group_record_counts: Counter[str] = Counter()
    group_reference_counts: Counter[str] = Counter()
    method_reference_counts: Counter[str] = Counter()

    for record in load_scaffold_records(workspace):
        factory_api = record.get("factoryApi", [])
        hits = [api for api in factory_api if api in method_to_group]
        if not hits:
            continue

        groups = sorted({method_to_group[api] for api in hits})
        for group_id in groups:
            group_record_counts[group_id] += 1
        for api in hits:
            group_reference_counts[method_to_group[api]] += 1
            method_reference_counts[api] += 1

        parity_record = parity_by_id.get(record["sourceScaffoldId"], {})
        samples.append(
            {
                "sourceScaffoldId": record.get("sourceScaffoldId"),
                "sourceEffectClassName": record.get("sourceEffectClassName"),
                "sourcePath": record.get("sourcePath"),
                "sourceSetId": record.get("sourceSetId"),
                "factoryApi": hits,
                "modifierGroups": groups,
                "affectedCardCount": record.get("affectedCardCount"),
                "sourceRequiredPresent": record.get("sourceScaffoldId") in source_required_ids,
                "parityCoverageStatus": parity_record.get("coverageStatus"),
                "unityFixturePath": parity_record.get("unityFixturePath"),
                "rlFixturePath": parity_record.get("rlFixturePath"),
                "comparisonReportPath": parity_record.get("comparisonReportPath"),
            }
        )

    by_group = []
    for group in MODIFIER_GROUPS:
        group_id = group["id"]
        group_samples = [sample for sample in samples if group_id in sample["modifierGroups"]]
        by_group.append(
            {
                "id": group_id,
                "title": group["title"],
                "sourceSampleCount": group_record_counts[group_id],
                "factoryReferenceCount": group_reference_counts[group_id],
                "methodReferenceCounts": {
                    method: method_reference_counts[method]
                    for method in group["sampleFactoryApi"]
                    if method_reference_counts[method] > 0
                },
                "sourceRequiredLinkedCount": sum(1 for sample in group_samples if sample["sourceRequiredPresent"]),
                "parityNotRunCount": sum(1 for sample in group_samples if sample["parityCoverageStatus"] == "NotRun"),
                "sampleRecords": group_samples[:5],
                "covered": group_record_counts[group_id] > 0
                and all(sample["sourceRequiredPresent"] for sample in group_samples)
                and all(sample["parityCoverageStatus"] == "NotRun" for sample in group_samples),
            }
        )

    return {
        "sourceScaffoldDir": SOURCE_SCAFFOLD_DIR.as_posix(),
        "sourceRequiredCapabilities": SOURCE_REQUIRED.as_posix(),
        "fullCardParityEvidence": FULL_CARD_PARITY.as_posix(),
        "sourceSampleCandidateCount": len(samples),
        "factoryReferenceCount": sum(group_reference_counts.values()),
        "sourceRequiredLinkedSampleCount": sum(1 for sample in samples if sample["sourceRequiredPresent"]),
        "parityNotRunSampleCount": sum(1 for sample in samples if sample["parityCoverageStatus"] == "NotRun"),
        "methodReferenceCounts": dict(method_reference_counts),
        "groupSourceSampleCounts": dict(group_record_counts),
        "groupFactoryReferenceCounts": dict(group_reference_counts),
        "byGroup": by_group,
        "sampleRecords": samples[:20],
        "covered": len(samples) > 0 and all(row["covered"] for row in by_group),
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
    task = find_task(fnd001, "FND001-CS-08")

    source_groups = source_modifier_report()
    headless = headless_surface_report(workspace)
    samples = source_sample_report(workspace)
    tests = test_evidence(workspace)

    parity_summary = parity.get("summary", {})
    gate_summary = gate.get("summary", {})
    full_card_parity_reduced = parity_summary.get("notRunSourceEffectCount") != 3918
    base_dp_exact_set_semantics_closed = False

    conditions = {
        "sourceModifierGroupsCovered": all(row["covered"] for row in source_groups),
        "runtimeModifierKindsCovered": headless["runtimeModifierKindsCovered"],
        "continuousDescriptorCovered": headless["descriptorCovered"],
        "continuousServiceCovered": headless["serviceCovered"],
        "dynamicAmountDelegateCovered": headless["dynamicAmountCovered"],
        "baseDefinitionMutationBoundaryCovered": headless["baseDefinitionBoundaryCovered"],
        "sourceSamplesLinked": samples["covered"],
        "testCandidatesPresent": tests["allRequiredTestsPresent"],
        "baseDpExactSetSemanticsRetainedAsBoundary": base_dp_exact_set_semantics_closed is False,
        "fullCardParityBoundaryRetained": full_card_parity_reduced is False
        and parity_summary.get("notRunSourceEffectCount") == 3918
        and parity_summary.get("passedSourceEffectCount") == 0,
        "openCodeStillFalse": gate_summary.get("openCodeReady") is False,
        "continuousStillPartial": gate_summary.get("selectedNextFoundationCapability") == "ContinuousOrStaticEffect"
        and gate_summary.get("selectedNextFoundationStatus") == "PartiallyImplemented",
    }

    passed = all(conditions.values())
    return {
        "schemaVersion": "dcgo.as-is-restart.fnd001cs08-static-modifier-verification.v1",
        "generatedAt": datetime.now(timezone.utc).isoformat(),
        "goal": "FND-001-A static DP/SecurityAttack/SecurityDigimonDP descriptor parity evidence",
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
            "id": "FND001-CS-08",
            "classification": task.get("classification"),
            "title": task.get("title"),
            "status": "ClosedByEvidence" if passed else "NeedsWork",
            "nextTaskCandidate": "FND001-CS-11",
            "baseDpExactSetSemanticsBoundary": "PARITY-001/TRUST-001-RERUN",
            "parityBoundaryTask": "PARITY-001",
        },
        "summary": {
            "passed": passed,
            "taskId": "FND001-CS-08",
            "requiredSourceModifierGroupCount": len(MODIFIER_GROUPS),
            "coveredSourceModifierGroupCount": sum(1 for row in source_groups if row["covered"]),
            "requiredHeadlessRuntimeModifierKindCount": 3,
            "coveredHeadlessRuntimeModifierKindCount": sum(
                1
                for row in headless["modifierGroups"]
                if row["headlessModifierKind"] in {"DP", "SecurityAttack", "SecurityDigimonDP"}
                and row["covered"]
            ),
            "sourceSampleCandidateCount": samples["sourceSampleCandidateCount"],
            "factoryReferenceCount": samples["factoryReferenceCount"],
            "sourceRequiredLinkedSampleCount": samples["sourceRequiredLinkedSampleCount"],
            "parityNotRunSampleCount": samples["parityNotRunSampleCount"],
            "requiredTestCount": tests["requiredTestCount"],
            "coveredTestCount": tests["coveredTestCount"],
            "dynamicAmountDelegateCovered": headless["dynamicAmountCovered"],
            "baseDefinitionMutationBoundaryCovered": headless["baseDefinitionBoundaryCovered"],
            "baseDpExactSetSemanticsClosed": base_dp_exact_set_semantics_closed,
            "fullCardParityReduced": full_card_parity_reduced,
            "parityNotRunSourceEffectCount": parity_summary.get("notRunSourceEffectCount"),
            "parityPassedSourceEffectCount": parity_summary.get("passedSourceEffectCount"),
        },
        "conditions": conditions,
        "sourceModifierGroups": source_groups,
        "headlessSurfaces": headless,
        "sourceSamples": samples,
        "testEvidence": tests,
        "baseDpBoundary": {
            "baseDpExactSetSemanticsClosed": base_dp_exact_set_semantics_closed,
            "reason": (
                "Original ChangeBaseDPStaticEffect/ChangeBaseDPGlobalEffect set origin DP. "
                "CS-08 records the source method and sample evidence and verifies the headless "
                "derived-stat policy does not mutate CardDefinition.DP. Exact per-card set-semantics "
                "must be locked by PARITY-001/TRUST-001-RERUN before status promotion."
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
                "Treat ContinuousEffectDescriptor DP/SecurityAttack/SecurityDigimonDP runtime reuse as candidate only after TRUST-001 maps it back to ChangeDP/ChangeSAttack/ChangeCardDP/ChangeOriginDP evidence.",
                "Keep ChangeBaseDP exact origin-DP set semantics as a boundary until source fixture parity proves the derived-stat policy is equivalent for affected cards.",
                "Do not use CS-08 evidence to promote generated full-card source records while full-card parity remains NotRun.",
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
