"""Verify FND-001-A continuous/static source collector scope evidence.

This verifier closes FND001-CS-03 only as source-aligned evidence. It does not
promote generated status, change Foundation Gate values, implement CardEffect
bodies, or run C0039+ card-porting.
"""

from __future__ import annotations

import argparse
import json
from datetime import datetime, timezone
from pathlib import Path
from typing import Any


SOURCE_ROOT = Path("E:/headlessDCGO/DCGO/Assets")
FND001_PARTIAL_CLOSURE = Path("docs/generated/as-is-restart/fnd-001-continuous-static-partial-closure.json")
FULL_CARD_PARITY = Path("docs/generated/full-card-parity-evidence.json")
FOUNDATION_GATE = Path("docs/generated/foundation-completion-gate.json")
PROGRAM_CS = Path("src/DCGO.RL.Engine.Tests/Program.cs")
CONTINUOUS_DESCRIPTOR = Path("src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs")
CONTINUOUS_SERVICE = Path("src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs")
SCRIPT_RUNTIME_EFFECT = Path("src/DCGO.RL.Engine/Effects/ScriptRuntimeEffectFoundation.cs")
SCRIPT_RUNTIME_DOMAIN = Path("src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs")
OUT_JSON = Path("docs/generated/as-is-restart/fnd-001-cs-03-source-collector-scope-verification.json")


SOURCE_FILES = {
    "AutoProcessing": SOURCE_ROOT / "Scripts/Script/AutoProcessing.cs",
    "Permanent": SOURCE_ROOT / "Scripts/Script/Permanent.cs",
    "CardSource": SOURCE_ROOT / "Scripts/Script/CardSource.cs",
    "Player": SOURCE_ROOT / "Scripts/Script/Player.cs",
    "CardController": SOURCE_ROOT / "Scripts/Script/CardController.cs",
}

SOURCE_PATTERNS = {
    "AutoProcessing": [
        "public static List<SkillInfo> GetSkillInfos",
        "player.EffectList(timing)",
        "permanent.EffectList(timing)",
        "player.TrashCards",
        "player.HandCards",
        "player.SecurityCards",
        "source.IsFlipped",
        "public static List<SkillInfo> GetSkillInfosOfCards",
        "public static IEnumerator ActivateBackgroundEffects",
    ],
    "Permanent": [
        "public List<ICardEffect> EffectList_ForCard",
        "if (!cardSource.IsFlipped)",
        "cardEffect.IsInheritedEffect && !isTopCard",
        "cardEffect.IsLinkedEffect && cardSource.IsLinked",
        "isTopCard && !cardEffect.IsInheritedEffect && !cardEffect.IsLinkedEffect",
        "EffectList_Added(timing)",
    ],
    "CardSource": [
        "public List<ICardEffect> EffectList(EffectTiming timing)",
        "public List<ICardEffect> EffectList_ForCard",
        "GetCardEffects(timing, cardSource)",
        "cardEffect.SetEffectSourceCard(this)",
    ],
    "Player": [
        "public List<CardSource> HandCards",
        "public List<CardSource> TrashCards",
        "public List<CardSource> SecurityCards",
        "public List<CardSource> ExecutingCards",
        "public List<ICardEffect> EffectList(EffectTiming timing)",
    ],
    "CardController": [
        "card.Owner.ExecutingCards.Contains(card)",
        "card.EffectList(EffectTiming.None)",
    ],
}

SOURCE_KINDS = [
    "FieldTop",
    "InheritedSource",
    "LinkedCard",
    "FaceUpSecurity",
    "Hand",
    "Trash",
    "Executing",
]

KIND_EVIDENCE = {
    "FieldTop": {
        "source": ["permanent.EffectList(timing)", "isTopCard && !cardEffect.IsInheritedEffect && !cardEffect.IsLinkedEffect"],
        "headless": ["ContinuousEffectSourceKind.FieldTop", "permanent.TopCardId"],
        "tests": ["Continuous static keyword field source grants Blocker"],
    },
    "InheritedSource": {
        "source": ["cardEffect.IsInheritedEffect && !isTopCard"],
        "headless": ["ContinuousEffectSourceKind.InheritedSource", "permanent.SourceCardIds"],
        "tests": [
            "Continuous inherited source applies only from source zone",
            "Continuous static keyword inherited source stops after move",
        ],
    },
    "LinkedCard": {
        "source": ["cardEffect.IsLinkedEffect && cardSource.IsLinked"],
        "headless": ["ContinuousEffectSourceKind.LinkedCard", "permanent.LinkedCards"],
        "tests": ["Continuous linked source applies from linked zone"],
    },
    "FaceUpSecurity": {
        "source": ["player.SecurityCards", "source.IsFlipped"],
        "headless": ["ContinuousEffectSourceKind.FaceUpSecurity", "player.Security.Where(card => state.Cards[card].IsFaceUp)"],
        "tests": [
            "Continuous face-up security source applies",
            "Continuous face-down security source is ignored",
        ],
    },
    "Hand": {
        "source": ["player.HandCards"],
        "headless": ["ContinuousEffectSourceKind.Hand", "player.Hand"],
        "tests": ["Continuous hand source applies only from hand", "Static evolution requirement hand source generates and executes"],
    },
    "Trash": {
        "source": ["player.TrashCards"],
        "headless": ["ContinuousEffectSourceKind.Trash", "player.Trash"],
        "tests": ["Continuous trash source applies from trash"],
    },
    "Executing": {
        "source": ["ExecutingCards", "card.EffectList(EffectTiming.None)"],
        "headless": ["ContinuousEffectSourceKind.Executing", "player.Executing"],
        "tests": ["Continuous executing source applies during execution"],
    },
}

COLLECTOR_METHODS = [
    "Collect(GameState state)",
    "CollectKeywords(GameState state)",
    "CollectStaticEvolutionRequirements(GameState state)",
    "CollectCannotIgnoreDigivolutionRequirements",
    "CollectStaticLinkRequirements",
    "CollectStaticCostModifiers",
    "CollectStaticRestrictions",
    "CollectStaticCardRestrictions",
    "CollectStaticImmunities",
    "CollectStaticCardColors",
    "CollectStaticCardNames",
    "CollectStaticCardTraits",
    "CollectStaticCardLevels",
    "CollectStaticPermanentLevels",
    "CollectIgnoreColorRequirements",
]


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


def source_surface_report() -> list[dict[str, Any]]:
    surfaces = []
    for name, path in SOURCE_FILES.items():
        text = read_text(path)
        patterns = pattern_report(text, SOURCE_PATTERNS[name])
        surfaces.append(
            {
                "name": name,
                "path": str(path),
                "requiredPatternCount": len(patterns),
                "presentPatternCount": sum(1 for pattern in patterns if pattern["present"]),
                "covered": all(pattern["present"] for pattern in patterns),
                "patterns": patterns,
            }
        )

    return surfaces


def headless_surface_report(workspace: Path) -> dict[str, Any]:
    descriptor = read_text(workspace / CONTINUOUS_DESCRIPTOR)
    service = read_text(workspace / CONTINUOUS_SERVICE)
    effect = read_text(workspace / SCRIPT_RUNTIME_EFFECT)
    domain = read_text(workspace / SCRIPT_RUNTIME_DOMAIN)

    source_kind_presence = {
        kind: f"ContinuousEffectSourceKind.{kind}" in service or kind in descriptor
        for kind in SOURCE_KINDS
    }
    enumerator_presence = {
        "fieldTop": "permanent.TopCardId" in service,
        "inherited": "permanent.SourceCardIds" in service,
        "linked": "permanent.LinkedCards" in service,
        "faceUpSecurity": "player.Security.Where(card => state.Cards[card].IsFaceUp)" in service,
        "hand": "player.Hand" in service,
        "trash": "player.Trash" in service,
        "executing": "player.Executing" in service,
    }
    collector_method_presence = {method: method in service for method in COLLECTOR_METHODS}
    role_validation_presence = {
        "resolveInherited": "TriggerSourceRole.Inherited" in effect,
        "resolveLinked": "TriggerSourceRole.Linked" in effect,
        "resolveFaceUpSecurity": "TriggerSourceRole.FaceUpSecurity" in effect,
        "resolveHand": "TriggerSourceRole.Hand" in effect,
        "resolveTrash": "TriggerSourceRole.Trash" in effect,
        "resolveExecuting": "TriggerSourceRole.Executing" in effect,
        "domainEffectListForCard": "EffectList_ForCard" in domain,
        "domainFaceDownSkip": "source.IsFlipped" in domain,
        "domainLinkedEffect": "cardEffect.IsLinkedEffect && source.IsLinked" in domain,
    }

    return {
        "files": [
            CONTINUOUS_DESCRIPTOR.as_posix(),
            CONTINUOUS_SERVICE.as_posix(),
            SCRIPT_RUNTIME_EFFECT.as_posix(),
            SCRIPT_RUNTIME_DOMAIN.as_posix(),
        ],
        "sourceKindPresence": source_kind_presence,
        "enumeratorPresence": enumerator_presence,
        "collectorMethodPresence": collector_method_presence,
        "roleValidationPresence": role_validation_presence,
        "allSourceKindsPresent": all(source_kind_presence.values()),
        "allEnumeratorsPresent": all(enumerator_presence.values()),
        "allCollectorMethodsPresent": all(collector_method_presence.values()),
        "allRoleValidationPresent": all(role_validation_presence.values()),
    }


def test_evidence(workspace: Path) -> dict[str, Any]:
    text = read_text(workspace / PROGRAM_CS)
    required = sorted({test for evidence in KIND_EVIDENCE.values() for test in evidence["tests"]})
    presence = {test: test in text for test in required}
    return {
        "program": PROGRAM_CS.as_posix(),
        "requiredTests": required,
        "presence": presence,
        "coveredTestCount": sum(1 for present in presence.values() if present),
        "requiredTestCount": len(required),
        "allRequiredTestsPresent": all(presence.values()),
    }


def find_cs03_task(fnd001: dict[str, Any]) -> dict[str, Any]:
    for task in fnd001.get("tasks", []):
        if task.get("id") == "FND001-CS-03":
            return task
    raise KeyError("FND001-CS-03 task not found.")


def fixture_candidates(
    task: dict[str, Any],
    parity: dict[str, Any],
) -> list[dict[str, Any]]:
    card_data = task.get("sourceEvidence", {}).get("cardData", [])
    records = {record.get("sourceScaffoldId"): record for record in parity.get("records", [])}
    candidates = []
    for index, kind in enumerate(SOURCE_KINDS):
        sample = card_data[index % len(card_data)] if card_data else {}
        record = records.get(sample.get("sourceScaffoldId"), {})
        candidates.append(
            {
                "sourceKind": kind,
                "sourceScaffoldId": sample.get("sourceScaffoldId"),
                "sourceEffectClassName": sample.get("sourceEffectClassName"),
                "sourcePath": sample.get("sourcePath"),
                "affectedCardCount": sample.get("affectedCardCount"),
                "coverageStatus": record.get("coverageStatus"),
                "unityFixturePath": record.get("unityFixturePath"),
                "rlFixturePath": record.get("rlFixturePath"),
                "comparisonReportPath": record.get("comparisonReportPath"),
                "status": "FixtureCandidateOnly",
            }
        )

    return candidates


def source_kind_coverage(
    workspace: Path,
    source_surfaces: list[dict[str, Any]],
    headless: dict[str, Any],
    tests: dict[str, Any],
    fixtures: list[dict[str, Any]],
) -> list[dict[str, Any]]:
    source_text = "\n".join(read_text(path) for path in SOURCE_FILES.values())
    headless_text = "\n".join(
        read_text(workspace / path)
        for path in [
            Path("src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs"),
            Path("src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs"),
            Path("src/DCGO.RL.Engine/Effects/ScriptRuntimeEffectFoundation.cs"),
            Path("src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs"),
        ]
    )
    fixture_by_kind = {fixture["sourceKind"]: fixture for fixture in fixtures}
    rows = []
    for kind in SOURCE_KINDS:
        evidence = KIND_EVIDENCE[kind]
        source_present = all(pattern in source_text for pattern in evidence["source"])
        headless_present = all(pattern in headless_text for pattern in evidence["headless"])
        tests_present = all(tests["presence"].get(test, False) for test in evidence["tests"])
        fixture = fixture_by_kind.get(kind)
        rows.append(
            {
                "sourceKind": kind,
                "sourcePatterns": evidence["source"],
                "headlessPatterns": evidence["headless"],
                "testCandidates": evidence["tests"],
                "sourceEvidencePresent": source_present,
                "headlessEvidencePresent": headless_present,
                "testEvidencePresent": tests_present,
                "fixtureCandidate": fixture,
                "covered": source_present and headless_present and tests_present and fixture is not None,
            }
        )

    return rows


def build_report(workspace: Path) -> dict[str, Any]:
    fnd001 = load_json(workspace, FND001_PARTIAL_CLOSURE)
    parity = load_json(workspace, FULL_CARD_PARITY)
    gate = load_json(workspace, FOUNDATION_GATE)

    task = find_cs03_task(fnd001)
    source_surfaces = source_surface_report()
    headless = headless_surface_report(workspace)
    tests = test_evidence(workspace)
    fixtures = fixture_candidates(task, parity)
    kind_coverage = source_kind_coverage(workspace, source_surfaces, headless, tests, fixtures)

    parity_summary = parity.get("summary", {})
    gate_summary = gate.get("summary", {})
    strict_ordering_closed = False
    full_card_parity_reduced = parity_summary.get("notRunSourceEffectCount") != 3918

    conditions = {
        "sourceSurfacesCovered": all(surface["covered"] for surface in source_surfaces),
        "headlessSourceKindsCovered": headless["allSourceKindsPresent"] and headless["allEnumeratorsPresent"],
        "collectorMethodsCovered": headless["allCollectorMethodsPresent"],
        "roleValidationCovered": headless["allRoleValidationPresent"],
        "sourceKindRowsCovered": all(row["covered"] for row in kind_coverage),
        "testCandidatesPresent": tests["allRequiredTestsPresent"],
        "fixtureCandidatesLinked": len(fixtures) == len(SOURCE_KINDS)
        and all(fixture.get("sourcePath") for fixture in fixtures)
        and all(fixture.get("coverageStatus") == "NotRun" for fixture in fixtures),
        "strictOrderingBoundaryRetained": strict_ordering_closed is False,
        "fullCardParityBoundaryRetained": full_card_parity_reduced is False
        and parity_summary.get("notRunSourceEffectCount") == 3918
        and parity_summary.get("passedSourceEffectCount") == 0,
        "openCodeStillFalse": gate_summary.get("openCodeReady") is False,
        "continuousStillPartial": gate_summary.get("selectedNextFoundationCapability") == "ContinuousOrStaticEffect"
        and gate_summary.get("selectedNextFoundationStatus") == "PartiallyImplemented",
    }

    passed = all(conditions.values())
    return {
        "schemaVersion": "dcgo.as-is-restart.fnd001cs03-source-collector-scope-verification.v1",
        "generatedAt": datetime.now(timezone.utc).isoformat(),
        "goal": "FND-001-A continuous/static source collector scope parity evidence",
        "inputs": {
            "asIsSourceRoot": str(SOURCE_ROOT),
            "fnd001PartialClosure": FND001_PARTIAL_CLOSURE.as_posix(),
            "fullCardParityEvidence": FULL_CARD_PARITY.as_posix(),
            "foundationGate": FOUNDATION_GATE.as_posix(),
            "programTests": PROGRAM_CS.as_posix(),
        },
        "policy": {
            "implementationPerformed": False,
            "srcImplementationModified": False,
            "dcgoOriginalModified": False,
            "cardEffectBodyImplemented": False,
            "c0039OrLaterCardPortingRun": False,
            "rlComponentsImplemented": False,
            "foundationGateManipulated": False,
            "generatedStatusPromoted": False,
            "commitOrPushPerformed": False,
        },
        "task": {
            "id": "FND001-CS-03",
            "classification": "CloseableFoundationTask",
            "title": "continuous/static source collector scope parity",
            "status": "ClosedByEvidence" if passed else "NeedsWork",
            "nextTaskCandidate": "FND001-CS-04",
            "orderingBoundaryTask": "FND001-CS-14",
        },
        "summary": {
            "passed": passed,
            "taskId": "FND001-CS-03",
            "requiredSourceKindCount": len(SOURCE_KINDS),
            "coveredSourceKindCount": sum(1 for row in kind_coverage if row["covered"]),
            "sourceSurfaceCount": len(source_surfaces),
            "coveredSourceSurfaceCount": sum(1 for surface in source_surfaces if surface["covered"]),
            "requiredTestCount": tests["requiredTestCount"],
            "coveredTestCount": tests["coveredTestCount"],
            "fixtureCandidateSourceEffectCount": len({fixture["sourceScaffoldId"] for fixture in fixtures}),
            "parityNotRunSourceEffectCount": parity_summary.get("notRunSourceEffectCount"),
            "parityPassedSourceEffectCount": parity_summary.get("passedSourceEffectCount"),
            "strictUnityOrderingParityClosed": strict_ordering_closed,
            "fullCardParityReduced": full_card_parity_reduced,
            "orderingBoundaryTaskId": "FND001-CS-14",
        },
        "conditions": conditions,
        "sourceSurfaces": source_surfaces,
        "headlessSurfaces": headless,
        "sourceKinds": kind_coverage,
        "fixtureCandidates": fixtures,
        "testEvidence": tests,
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
            "strictUnityOrderingParityClosed": strict_ordering_closed,
            "strictUnityOrderingParityOwner": "FND001-CS-14 AutoProcessing priority/cut-in/background ordering parity",
        },
        "trust001Handoff": {
            "handoffRequired": True,
            "items": [
                "ContinuousEffectSourceCollector source kind coverage should be treated as ReuseCandidate only after TRUST-001 maps it to original AutoProcessing/CardSource/Permanent/Player evidence.",
                "Strict ordering parity remains NeedsSourceMapping and must not be inferred from source kind coverage.",
                "Full-card parity remains NotRun until PARITY-001 creates executable Unity/RL fixture reports.",
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
