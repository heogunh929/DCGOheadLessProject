"""Verify FND-001-A static evolution/link requirement evidence.

This verifier closes FND001-CS-11 only as source-aligned evidence for
continuous/static evolution and link requirement descriptors, effective
metadata/level gates, and cannot-ignore digivolution permission boundaries. It
does not implement source CardEffect bodies, does not promote generated status,
does not change Foundation Gate values, and does not run C0039+ card-porting.
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
STATIC_REQUIREMENT_SERVICE = Path("src/DCGO.RL.Engine/Effects/StaticRequirementService.cs")
BATTLE_RULES = Path("src/DCGO.RL.Engine/Battle/BattleRules.cs")
LEGAL_ACTION_GENERATOR = Path("src/DCGO.RL.Engine/Battle/LegalActionGenerator.cs")
DIGIVOLVE_SERVICE = Path("src/DCGO.RL.Engine/Battle/DigivolveService.cs")
COMPLEX_MECHANIC_SERVICE = Path("src/DCGO.RL.Engine/Mechanics/ComplexMechanicService.cs")
OUT_JSON = Path("docs/generated/as-is-restart/fnd-001-cs-11-static-requirement-verification.json")


REQUIREMENT_GROUPS = [
    {
        "id": "EvolutionRequirement",
        "title": "static digivolution/evolution requirement",
        "sourceFactoryFile": SOURCE_ROOT / "Scripts/Script/CardEffectFactory/AddDigivolutionRequirement.cs",
        "sourceClassFile": SOURCE_ROOT / "Scripts/Script/CardEffects/AddEvolutionConditionClass.cs",
        "sourceInterfaceFile": SOURCE_ROOT / "Scripts/Script/CardEffectInterfaces.cs",
        "sourceConsumerFile": SOURCE_ROOT / "Scripts/Script/CardSource.cs",
        "sourceClass": "AddDigivolutionRequirementClass",
        "sourceInterface": "IAddDigivolutionRequirementEffect",
        "sourceMethods": ["AddSelfDigivolutionRequirementStaticEffect", "AddDigivolutionRequirementStaticEffect"],
        "sampleFactoryApi": ["AddSelfDigivolutionRequirementStaticEffect", "AddDigivolutionRequirementStaticEffect"],
        "sourcePatterns": [
            "cardCondition ?? ((cardSource) => cardSource == card)",
            "effectName: effectName ?? \"Can digivolve to this card\"",
            "SetUpAddDigivolutionRequirementClass(getEvoCost: GetEvoCost)",
            "cardSource.Owner.CanIgnoreDigivolutionRequirement(permanent, cardSource)",
            "permanent.TopCard.CardColors.Contains(cardColor)",
            "permanent.TopCard.Level == level",
            "return costEquation != null ? costEquation() : digivolutionCost;",
        ],
        "classPatterns": [
            "public class AddDigivolutionRequirementClass",
            "IAddDigivolutionRequirementEffect",
            "SetUpAddDigivolutionRequirementClass",
            "public int GetEvoCost",
            "return _getEvoCost(permanent, cardSource, ignore, isCheckAvailability);",
            "return -1;",
        ],
        "interfacePatterns": [
            "public interface IAddDigivolutionRequirementEffect",
            "int GetEvoCost(Permanent permanent, CardSource cardSource, CardEffectCommons.IgnoreRequirement ignore, bool checkAvailability);",
        ],
        "consumerPatterns": [
            "cardEffect is IAddDigivolutionRequirementEffect && cardEffect.CanUse(null)",
            "((IAddDigivolutionRequirementEffect)cardEffect).GetEvoCost(targetPermanent, this, ignore, checkAvailability)",
            "BaseEvoCostsFromEntity",
            "Owner.CanIgnoreDigivolutionRequirement(targetPermanent, this)",
        ],
        "headlessDescriptor": "StaticEvolutionRequirementDescriptor",
        "headlessPatterns": [
            "public sealed record StaticEvolutionRequirementDescriptor",
            "CardColor RequiredColor = CardColor.None",
            "int RequiredLevel = -1",
            "bool IgnoreDigivolutionRequirement = false",
            "Func<StaticEvolutionRequirementEvaluationContext, bool>? SourceCardCondition",
            "Func<StaticEvolutionRequirementEvaluationContext, bool>? TargetPermanentCondition",
            "Func<StaticEvolutionRequirementEvaluationContext, int>? CostEquation",
            "public IReadOnlyList<StaticEvolutionRequirementDescriptor> CollectStaticEvolutionRequirements",
            "public IReadOnlyList<StaticEvolutionRequirementEvaluation> EvaluateEvolutionRequirements",
            "staticRequirements?.FirstEvolutionRequirement(state, card, targetPermanent, staticEffects)",
            "BattleRules.CanDigivolve(state, card, permanent, out _, _staticRequirements, _staticEffects)",
            "DigivolveWithResult",
        ],
        "testCandidates": [
            "Static evolution requirement hand source generates and executes",
            "CardEffectFactory self evolution requirement maps to static descriptor",
            "Static evolution requirement stops after source move",
            "Static evolution requirement condition gates target",
            "Static evolution requirement ignore permission generates and executes",
            "Static evolution requirement ignore permission requires target gate",
            "Static requirement metadata criteria gates source and target",
            "Static card level modifier feeds permanent level requirement",
            "Static card color modifier affects digivolution color requirement",
            "Static requirement replay deterministic",
        ],
    },
    {
        "id": "LinkRequirement",
        "title": "static link requirement",
        "sourceFactoryFile": SOURCE_ROOT / "Scripts/Script/CardEffectFactory/AddLinkRequirement.cs",
        "sourceClassFile": SOURCE_ROOT / "Scripts/Script/CardEffects/AddLinkConditionClass.cs",
        "sourceInterfaceFile": SOURCE_ROOT / "Scripts/Script/CardEffectInterfaces.cs",
        "sourceConsumerFile": SOURCE_ROOT / "Scripts/Script/CardSource.cs",
        "sourceClass": "AddLinkConditionClass",
        "sourceInterface": "IAddLinkConditionEffect",
        "sourceMethods": ["AddSelfLinkConditionStaticEffect", "AddLinkConditionStaticEffect"],
        "sampleFactoryApi": ["AddSelfLinkConditionStaticEffect", "AddLinkConditionStaticEffect"],
        "sourcePatterns": [
            "cardCondition: cardCondition ?? ((cardSource) => cardSource == card)",
            "effectName: effectName ?? \"Link\"",
            "SetUpAddLinkConditionClass(getLinkCondition: GetLink)",
            "LinkCondition GetLink(CardSource cardSource)",
            "new LinkCondition(",
            "digimonCondition: PermanentCondition",
            "cost: linkCost",
        ],
        "classPatterns": [
            "public class AddLinkConditionClass",
            "IAddLinkConditionEffect",
            "SetUpAddLinkConditionClass",
            "public LinkCondition GetLinkCondition",
            "return _getLinkCondition(cardSource);",
            "return null;",
        ],
        "interfacePatterns": [
            "public interface IAddLinkConditionEffect",
            "LinkCondition GetLinkCondition(CardSource cardSource);",
        ],
        "consumerPatterns": [
            "cardEffect is IAddLinkConditionEffect",
            "((IAddLinkConditionEffect)cardEffect).GetLinkCondition(this) != null",
            "return ((IAddLinkConditionEffect)addLinkConditonEffect).GetLinkCondition(this);",
            "return null;",
        ],
        "headlessDescriptor": "StaticLinkRequirementDescriptor",
        "headlessPatterns": [
            "public sealed record StaticLinkRequirementDescriptor",
            "int LinkCost",
            "Func<StaticLinkRequirementEvaluationContext, bool>? SourceCardCondition",
            "Func<StaticLinkRequirementEvaluationContext, bool>? TargetPermanentCondition",
            "public IReadOnlyList<StaticLinkRequirementDescriptor> CollectStaticLinkRequirements",
            "public IReadOnlyList<StaticLinkRequirementEvaluation> EvaluateLinkRequirements",
            "_staticRequirements?.FirstLinkRequirement(state, action.LinkCard, target, _staticEffects)",
            "GenerateStaticLinkActions",
            "_costResolver.ResolveLink",
            "[\"staticRequirement\"] = \"link\"",
        ],
        "testCandidates": [
            "Static link requirement hand source generates and executes",
            "Static link requirement uses effective metadata and level",
            "Static link cost modifier adjusts link cost",
            "Static requirement replay deterministic",
        ],
    },
    {
        "id": "IgnorePermission",
        "title": "ignore digivolution requirement permission and cannot-ignore restriction",
        "sourceFactoryFile": SOURCE_ROOT
        / "Scripts/Script/CardEffectCommons/GiveEffect/GiveEffectToPlayer/IgnoreDigivolutionRequirement.cs",
        "sourceClassFile": SOURCE_ROOT / "Scripts/Script/CardEffects/CannotIgnoreDigivolutionConditionClass.cs",
        "sourceInterfaceFile": SOURCE_ROOT / "Scripts/Script/CardEffectInterfaces.cs",
        "sourceConsumerFile": SOURCE_ROOT / "Scripts/Script/Player.cs",
        "sourceClass": "CannotIgnoreDigivolutionConditionClass",
        "sourceInterface": "ICannotIgnoreDigivolutionConditionEffect",
        "sourceMethods": ["GainIgnoreDigivolutionRequirementPlayerEffect"],
        "sampleFactoryApi": ["GainIgnoreDigivolutionRequirementPlayerEffect"],
        "sourcePatterns": [
            "GainIgnoreDigivolutionRequirementPlayerEffect",
            "if (activateClass == null) return null;",
            "if (activateClass.EffectSourceCard == null) return null;",
            "CardEffectFactory.AddDigivolutionRequirementStaticEffect",
            "effectName: \"Ignore Digivolution requirements and change digivolution cost\"",
            "return GetCardEffectByEffectTiming(timing: EffectTiming.None, cardEffect: addDigivolutionRequirementClass);",
        ],
        "classPatterns": [
            "public class CannotIgnoreDigivolutionConditionClass",
            "ICannotIgnoreDigivolutionConditionEffect",
            "SetUpCannotIgnoreDigivolutionConditionClass",
            "public bool cannotIgnoreDigivolutionCondition",
            "IgnoreDigivolutionCondition(player, targetPermanent, cardSource)",
            "return false;",
        ],
        "interfacePatterns": [
            "public interface ICannotIgnoreDigivolutionConditionEffect",
            "bool cannotIgnoreDigivolutionCondition(Player player, Permanent targetPermanent, CardSource cardSource);",
        ],
        "consumerPatterns": [
            "public bool CanIgnoreDigivolutionRequirement(Permanent targetPermanent, CardSource cardSource)",
            "cardEffect1 is ICannotIgnoreDigivolutionConditionEffect",
            "cardEffect1.CanUse(null)",
            "cannotIgnoreDigivolutionCondition(this, targetPermanent, cardSource)",
            "return false;",
            "return true;",
        ],
        "headlessDescriptor": "CannotIgnoreDigivolutionRequirementDescriptor",
        "headlessPatterns": [
            "public sealed record CannotIgnoreDigivolutionRequirementDescriptor",
            "StaticEffectPlayerTargetKind AppliesTo",
            "CardMetadataCriteria? EvolvingCardMetadataCriteria",
            "public IReadOnlyList<CannotIgnoreDigivolutionRequirementDescriptor> CollectCannotIgnoreDigivolutionRequirements",
            "public IReadOnlyList<CannotIgnoreDigivolutionRequirementEvaluation> EvaluateCannotIgnoreDigivolutionRequirements",
            "CanIgnoreDigivolutionRequirement(state, descriptor.ControllerPlayerId, evolvingCard, targetPermanent)",
            "descriptor.IgnoreDigivolutionRequirement && !HasExplicitTargetGate(descriptor)",
            "Static evolution requirement cannot-ignore restriction blocks permission",
            "Static evolution requirement cannot-ignore restriction condition gates",
        ],
        "testCandidates": [
            "Static evolution requirement ignore permission generates and executes",
            "Static evolution requirement cannot-ignore restriction blocks permission",
            "Static evolution requirement cannot-ignore restriction condition gates",
            "Static evolution requirement ignore permission requires target gate",
        ],
    },
]

REQUIRED_TESTS = sorted(
    {
        *(test for group in REQUIREMENT_GROUPS for test in group["testCandidates"]),
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


def source_requirement_report() -> list[dict[str, Any]]:
    rows = []
    for group in REQUIREMENT_GROUPS:
        factory_text = read_text(group["sourceFactoryFile"])
        class_text = read_text(group["sourceClassFile"])
        interface_text = read_text(group["sourceInterfaceFile"])
        consumer_text = read_text(group["sourceConsumerFile"])
        method_patterns = group["sourceMethods"]
        reports = {
            "factoryPatterns": pattern_report(factory_text, method_patterns + group["sourcePatterns"]),
            "classPatterns": pattern_report(class_text, group["classPatterns"]),
            "interfacePatterns": pattern_report(interface_text, group["interfacePatterns"]),
            "consumerPatterns": pattern_report(consumer_text, group["consumerPatterns"]),
        }
        all_reports = [item for report in reports.values() for item in report]
        rows.append(
            {
                "id": group["id"],
                "title": group["title"],
                "sourceFactoryFile": str(group["sourceFactoryFile"]),
                "sourceClassFile": str(group["sourceClassFile"]),
                "sourceInterfaceFile": str(group["sourceInterfaceFile"]),
                "sourceConsumerFile": str(group["sourceConsumerFile"]),
                "sourceClass": group["sourceClass"],
                "sourceInterface": group["sourceInterface"],
                "sourceMethods": group["sourceMethods"],
                "sampleFactoryApi": group["sampleFactoryApi"],
                **reports,
                "covered": all(pattern["present"] for pattern in all_reports),
            }
        )

    return rows


def headless_surface_report(workspace: Path) -> dict[str, Any]:
    texts = {
        "CardEffectFactory": read_text(workspace / CARD_EFFECT_FACTORY),
        "ContinuousEffectDescriptor": read_text(workspace / CONTINUOUS_DESCRIPTOR),
        "ContinuousEffectService": read_text(workspace / CONTINUOUS_SERVICE),
        "StaticRequirementService": read_text(workspace / STATIC_REQUIREMENT_SERVICE),
        "BattleRules": read_text(workspace / BATTLE_RULES),
        "LegalActionGenerator": read_text(workspace / LEGAL_ACTION_GENERATOR),
        "DigivolveService": read_text(workspace / DIGIVOLVE_SERVICE),
        "ComplexMechanicService": read_text(workspace / COMPLEX_MECHANIC_SERVICE),
        "Program": read_text(workspace / PROGRAM_CS),
    }
    joined = "\n".join(texts.values())

    service_patterns = [
        "public sealed class StaticRequirementService",
        "EvaluateEvolutionRequirements",
        "FirstEvolutionRequirement",
        "EvaluateCannotIgnoreDigivolutionRequirements",
        "EvaluateLinkRequirements",
        "FirstLinkRequirement",
        "staticEffects?.EffectiveCardColors",
        "staticEffects?.EffectivePermanentLevel",
        "staticEffects.MatchesCardMetadata",
        "DistinctBy(evaluation => evaluation.Descriptor.StableId)",
        "OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)",
    ]
    factory_patterns = [
        "AddSelfDigivolutionRequirementStaticEffect",
        "AddDigivolutionRequirementStaticEffect",
        "Digivolution cost must not be negative without a cost equation.",
        "StableEvolutionRequirementId",
        "CombineSourceConditions",
    ]
    evolution_runtime_patterns = [
        "staticRequirements?.FirstEvolutionRequirement(state, card, targetPermanent, staticEffects)",
        "BattleRules.CanDigivolve(state, card, permanent, out _, _staticRequirements, _staticEffects)",
        "BattleRules.CanDigivolve(state, action.Card, permanent, out var cost, _staticRequirements, _staticEffects)",
        "BattleRules.CanDigivolve(state, action.Card, permanent, out cost, _staticRequirements, _staticEffects)",
    ]
    link_runtime_patterns = [
        "_staticRequirements?.FirstLinkRequirement(state, action.LinkCard, target, _staticEffects)",
        "GenerateStaticLinkActions",
        "_staticRequirements.EvaluateLinkRequirements(state, card, target, _staticEffects)",
        "_costResolver.ResolveLink",
        "[\"staticRequirement\"] = \"link\"",
    ]

    group_rows = []
    for group in REQUIREMENT_GROUPS:
        patterns = pattern_report(joined, group["headlessPatterns"])
        group_rows.append(
            {
                "id": group["id"],
                "title": group["title"],
                "headlessDescriptor": group["headlessDescriptor"],
                "patterns": patterns,
                "covered": all(pattern["present"] for pattern in patterns),
            }
        )

    link_factory_wrapper_present = "AddSelfLinkConditionStaticEffect" in texts["CardEffectFactory"] or (
        "AddLinkConditionStaticEffect" in texts["CardEffectFactory"]
    )

    return {
        "files": [
            CARD_EFFECT_FACTORY.as_posix(),
            CONTINUOUS_DESCRIPTOR.as_posix(),
            CONTINUOUS_SERVICE.as_posix(),
            STATIC_REQUIREMENT_SERVICE.as_posix(),
            BATTLE_RULES.as_posix(),
            LEGAL_ACTION_GENERATOR.as_posix(),
            DIGIVOLVE_SERVICE.as_posix(),
            COMPLEX_MECHANIC_SERVICE.as_posix(),
        ],
        "requirementGroups": group_rows,
        "servicePatterns": pattern_report(texts["StaticRequirementService"], service_patterns),
        "evolutionFactoryPatterns": pattern_report(texts["CardEffectFactory"], factory_patterns),
        "evolutionRuntimePatterns": pattern_report(joined, evolution_runtime_patterns),
        "linkRuntimePatterns": pattern_report(joined, link_runtime_patterns),
        "staticRequirementServiceCovered": all(pattern in texts["StaticRequirementService"] for pattern in service_patterns),
        "evolutionFactoryWrapperCovered": all(pattern in texts["CardEffectFactory"] for pattern in factory_patterns),
        "evolutionRuntimeWiringCovered": all(pattern in joined for pattern in evolution_runtime_patterns),
        "linkRuntimeWiringCovered": all(pattern in joined for pattern in link_runtime_patterns),
        "linkSourceFacingFactoryWrapperPresent": link_factory_wrapper_present,
        "linkSourceFacingFactoryWrapperBoundary": not link_factory_wrapper_present,
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
        for group in REQUIREMENT_GROUPS
        for method in group["sampleFactoryApi"]
    }
    scaffold_optional_groups = {"IgnorePermission"}
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
                "requirementGroups": groups,
                "affectedCardCount": record.get("affectedCardCount"),
                "sourceRequiredPresent": record.get("sourceScaffoldId") in source_required_ids,
                "parityCoverageStatus": parity_record.get("coverageStatus"),
                "unityFixturePath": parity_record.get("unityFixturePath"),
                "rlFixturePath": parity_record.get("rlFixturePath"),
                "comparisonReportPath": parity_record.get("comparisonReportPath"),
            }
        )

    by_group = []
    for group in REQUIREMENT_GROUPS:
        group_id = group["id"]
        group_samples = [sample for sample in samples if group_id in sample["requirementGroups"]]
        has_required_scaffold = group_id not in scaffold_optional_groups
        covered = (
            (not has_required_scaffold and group_record_counts[group_id] == 0)
            or (
                group_record_counts[group_id] > 0
                and all(sample["sourceRequiredPresent"] for sample in group_samples)
                and all(sample["parityCoverageStatus"] == "NotRun" for sample in group_samples)
            )
        )
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
                "requiresDirectSourceScaffold": has_required_scaffold,
                "directScaffoldBoundaryRetained": not has_required_scaffold,
                "sampleRecords": group_samples[:5],
                "covered": covered,
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
    task = find_task(fnd001, "FND001-CS-11")

    source_groups = source_requirement_report()
    headless = headless_surface_report(workspace)
    samples = source_sample_report(workspace)
    tests = test_evidence(workspace)

    parity_summary = parity.get("summary", {})
    gate_summary = gate.get("summary", {})
    full_card_parity_reduced = parity_summary.get("notRunSourceEffectCount") != 3918
    link_factory_wrapper_parity_closed = headless["linkSourceFacingFactoryWrapperPresent"]

    conditions = {
        "sourceRequirementGroupsCovered": all(row["covered"] for row in source_groups),
        "headlessDescriptorGroupsCovered": all(row["covered"] for row in headless["requirementGroups"]),
        "staticRequirementServiceCovered": headless["staticRequirementServiceCovered"],
        "evolutionFactoryWrapperCovered": headless["evolutionFactoryWrapperCovered"],
        "evolutionRuntimeWiringCovered": headless["evolutionRuntimeWiringCovered"],
        "linkRuntimeWiringCovered": headless["linkRuntimeWiringCovered"],
        "sourceSamplesLinked": samples["covered"],
        "ignorePermissionBoundaryRetained": samples["groupSourceSampleCounts"].get("IgnorePermission", 0) == 0,
        "linkFactoryWrapperBoundaryRetained": link_factory_wrapper_parity_closed is False,
        "testCandidatesPresent": tests["allRequiredTestsPresent"],
        "fullCardParityBoundaryRetained": full_card_parity_reduced is False
        and parity_summary.get("notRunSourceEffectCount") == 3918
        and parity_summary.get("passedSourceEffectCount") == 0,
        "openCodeStillFalse": gate_summary.get("openCodeReady") is False,
        "continuousStillPartial": gate_summary.get("selectedNextFoundationCapability") == "ContinuousOrStaticEffect"
        and gate_summary.get("selectedNextFoundationStatus") == "PartiallyImplemented",
    }

    passed = all(conditions.values())
    return {
        "schemaVersion": "dcgo.as-is-restart.fnd001cs11-static-requirement-verification.v1",
        "generatedAt": datetime.now(timezone.utc).isoformat(),
        "goal": "FND-001-A static evolution/link requirement effective gate evidence",
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
            "id": "FND001-CS-11",
            "classification": task.get("classification"),
            "title": task.get("title"),
            "status": "ClosedByEvidence" if passed else "NeedsWork",
            "nextTaskCandidate": "FND001-CS-07",
            "parityBoundaryTask": "PARITY-001",
            "trustBoundaryTask": "TRUST-001-RERUN",
            "linkFactoryWrapperParityClosed": link_factory_wrapper_parity_closed,
        },
        "summary": {
            "passed": passed,
            "taskId": "FND001-CS-11",
            "requiredSourceRequirementGroupCount": len(REQUIREMENT_GROUPS),
            "coveredSourceRequirementGroupCount": sum(1 for row in source_groups if row["covered"]),
            "requiredHeadlessRequirementGroupCount": len(REQUIREMENT_GROUPS),
            "coveredHeadlessRequirementGroupCount": sum(1 for row in headless["requirementGroups"] if row["covered"]),
            "sourceSampleCandidateCount": samples["sourceSampleCandidateCount"],
            "factoryReferenceCount": samples["factoryReferenceCount"],
            "sourceRequiredLinkedSampleCount": samples["sourceRequiredLinkedSampleCount"],
            "parityNotRunSampleCount": samples["parityNotRunSampleCount"],
            "evolutionFactoryReferenceCount": samples["groupFactoryReferenceCounts"].get("EvolutionRequirement", 0),
            "linkFactoryReferenceCount": samples["groupFactoryReferenceCounts"].get("LinkRequirement", 0),
            "directIgnorePermissionSourceSampleCount": samples["groupSourceSampleCounts"].get("IgnorePermission", 0),
            "requiredTestCount": tests["requiredTestCount"],
            "coveredTestCount": tests["coveredTestCount"],
            "staticRequirementServiceCovered": headless["staticRequirementServiceCovered"],
            "evolutionFactoryWrapperCovered": headless["evolutionFactoryWrapperCovered"],
            "linkSourceFacingFactoryWrapperPresent": headless["linkSourceFacingFactoryWrapperPresent"],
            "linkFactoryWrapperParityClosed": link_factory_wrapper_parity_closed,
            "ignorePermissionBoundaryRetained": samples["groupSourceSampleCounts"].get("IgnorePermission", 0) == 0,
            "fullCardParityReduced": full_card_parity_reduced,
            "parityNotRunSourceEffectCount": parity_summary.get("notRunSourceEffectCount"),
            "parityPassedSourceEffectCount": parity_summary.get("passedSourceEffectCount"),
        },
        "conditions": conditions,
        "sourceRequirementGroups": source_groups,
        "headlessSurfaces": headless,
        "sourceSamples": samples,
        "testEvidence": tests,
        "retainedBoundaries": [
            {
                "id": "LinkFactoryWrapperParity",
                "owner": "FND001-CS-07/PARITY-001",
                "reason": (
                    "Original AddLinkRequirement factory evidence and headless static link descriptor/runtime evidence are "
                    "present, but a source-facing headless CardEffectFactory AddSelfLinkConditionStaticEffect wrapper is "
                    "not present. CS-11 closes descriptor/runtime effective gates only."
                ),
            },
            {
                "id": "IgnorePermissionDirectScaffold",
                "owner": "TRUST-001-RERUN/PARITY-001",
                "reason": (
                    "Ignore digivolution permission is represented by a common helper and cannot-ignore restriction path, "
                    "not direct full-card source scaffold samples. It remains a boundary for full-card parity and trust rerun."
                ),
            },
            {
                "id": "FullCardParityNotRun",
                "owner": "PARITY-001",
                "reason": "Full-card source effects remain NotRun and were not converted into parity pass evidence.",
            },
        ],
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
                "Map StaticEvolutionRequirementDescriptor reuse to AddDigivolutionRequirementClass/GetEvoCost source evidence before trusting production card scripts.",
                "Map StaticLinkRequirementDescriptor reuse to AddLinkConditionClass/GetLinkCondition source evidence; keep missing source-facing factory wrapper as a review boundary.",
                "Map CannotIgnoreDigivolutionRequirementDescriptor reuse to CannotIgnoreDigivolutionConditionClass and Player.CanIgnoreDigivolutionRequirement before trusting ignore-permission behavior.",
                "Do not use CS-11 evidence to promote generated full-card source records while full-card parity remains NotRun.",
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
