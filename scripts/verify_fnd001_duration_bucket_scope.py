"""Verify FND-001-A duration bucket cleanup/provider boundary evidence.

This verifier closes FND001-CS-04 only as source-aligned evidence for
duration-bucket cleanup timing. It deliberately keeps production provider
catalog adoption as a TRUST-001/FND-005 handoff and does not promote generated
status, change Foundation Gate values, implement CardEffect bodies, or run
C0039+ card-porting.
"""

from __future__ import annotations

import argparse
import json
from datetime import datetime, timezone
from pathlib import Path
from typing import Any


SOURCE_ROOT = Path("E:/headlessDCGO/DCGO/Assets")
FND001_PARTIAL_CLOSURE = Path("docs/generated/as-is-restart/fnd-001-continuous-static-partial-closure.json")
FOUNDATION_GATE = Path("docs/generated/foundation-completion-gate.json")
PROGRAM_CS = Path("src/DCGO.RL.Engine.Tests/Program.cs")
TEMPORARY_MODIFIER = Path("src/DCGO.RL.Engine/Domain/TemporaryModifier.cs")
TEMPORARY_GRANTED_EFFECT = Path("src/DCGO.RL.Engine/Domain/TemporaryGrantedEffect.cs")
DURATION_CLEANUP = Path("src/DCGO.RL.Engine/Effects/DurationCleanupService.cs")
TEMPORARY_GRANTED_REGISTRY = Path("src/DCGO.RL.Engine/Effects/TemporaryGrantedEffectRegistry.cs")
SCRIPT_RUNTIME_DOMAIN = Path("src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs")
PHASE_RUNNER = Path("src/DCGO.RL.Engine/Battle/PhaseRunner.cs")
ATTACK_SERVICE = Path("src/DCGO.RL.Engine/Battle/AttackService.cs")
SECURITY_CHECK_SERVICE = Path("src/DCGO.RL.Engine/Battle/SecurityCheckService.cs")
TRIGGER_PIPELINE = Path("src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs")
OUT_JSON = Path("docs/generated/as-is-restart/fnd-001-cs-04-duration-bucket-verification.json")


SOURCE_FILES = {
    "Player": SOURCE_ROOT / "Scripts/Script/Player.cs",
    "Permanent": SOURCE_ROOT / "Scripts/Script/Permanent.cs",
    "TurnStateMachine": SOURCE_ROOT / "Scripts/Script/TurnStateMachine.cs",
    "AttackProcess": SOURCE_ROOT / "Scripts/Script/AttackProcess.cs",
    "CardController": SOURCE_ROOT / "Scripts/Script/CardController.cs",
    "GiveEffectToPermanentOrPlayer": (
        SOURCE_ROOT / "Scripts/Script/CardEffectCommons/GiveEffect/GiveEffectToPermanentOrPlayer.cs"
    ),
}


DURATION_BUCKETS = [
    {
        "bucket": "UntilEachTurnEndEffects",
        "sourceFiles": ["Player", "Permanent", "TurnStateMachine", "GiveEffectToPermanentOrPlayer"],
        "sourcePatterns": [
            "player.UntilEachTurnEndEffects = new List<Func<EffectTiming, ICardEffect>>();",
            "permanent.UntilEachTurnEndEffects = new List<Func<EffectTiming, ICardEffect>>();",
            "targetPermanent.UntilEachTurnEndEffects.Add(getCardEffect);",
            "player.UntilEachTurnEndEffects.Add(getCardEffect);",
        ],
        "headlessScope": "DurationScope.UntilTurnEnd",
        "headlessPatterns": [
            "UntilTurnEnd",
            "CleanupTurnEnd(GameState state, PlayerId endingTurnPlayer)",
            "modifier.DurationScope == DurationScope.UntilTurnEnd",
            "effect.DurationScope == DurationScope.UntilTurnEnd",
        ],
        "cleanupOwner": "PhaseRunner.CompleteEndCurrentTurnAfterEndTriggerWithResult",
        "cleanupTests": [
            "Duration cleanup UntilTurnEnd removes modifier",
            "Duration player runtime modifiers replay deterministic",
        ],
    },
    {
        "bucket": "UntilOwnerTurnEndEffects",
        "sourceFiles": ["Player", "Permanent", "TurnStateMachine", "GiveEffectToPermanentOrPlayer"],
        "sourcePatterns": [
            "gameContext.TurnPlayer.UntilOwnerTurnEndEffects = new List<Func<EffectTiming, ICardEffect>>();",
            "permanent.UntilOwnerTurnEndEffects = new List<Func<EffectTiming, ICardEffect>>();",
            "targetPermanent.UntilOwnerTurnEndEffects.Add(getCardEffect);",
            "player.UntilOwnerTurnEndEffects.Add(getCardEffect);",
        ],
        "headlessScope": "DurationScope.UntilOwnerTurnEnd",
        "headlessPatterns": [
            "UntilOwnerTurnEnd",
            "modifier.DurationScope is DurationScope.UntilOwnerTurnEnd or DurationScope.UntilOpponentTurnEnd",
            "effect.DurationScope is DurationScope.UntilOwnerTurnEnd or DurationScope.UntilOpponentTurnEnd",
            "modifier.ExpiresAtTurnPlayerId == endingTurnPlayer",
            "effect.ExpiresAtTurnPlayerId == endingTurnPlayer",
        ],
        "cleanupOwner": "PhaseRunner.CompleteEndCurrentTurnAfterEndTriggerWithResult",
        "cleanupTests": [
            "Duration cleanup keeps modifier before expiry",
            "ST2-14 attack block restriction option and security",
        ],
    },
    {
        "bucket": "UntilOpponentTurnEndEffects",
        "sourceFiles": ["Player", "Permanent", "TurnStateMachine", "GiveEffectToPermanentOrPlayer"],
        "sourcePatterns": [
            "gameContext.NonTurnPlayer.UntilOpponentTurnEndEffects = new List<Func<EffectTiming, ICardEffect>>();",
            "pokemon.UntilOpponentTurnEndEffects = new List<Func<EffectTiming, ICardEffect>>();",
            "targetPermanent.UntilOpponentTurnEndEffects.Add(getCardEffect);",
            "player.UntilOpponentTurnEndEffects.Add(getCardEffect);",
        ],
        "headlessScope": "DurationScope.UntilOpponentTurnEnd",
        "headlessPatterns": [
            "UntilOpponentTurnEnd",
            "modifier.DurationScope is DurationScope.UntilOwnerTurnEnd or DurationScope.UntilOpponentTurnEnd",
            "effect.DurationScope is DurationScope.UntilOwnerTurnEnd or DurationScope.UntilOpponentTurnEnd",
            "modifier.ExpiresAtTurnPlayerId == endingTurnPlayer",
            "effect.ExpiresAtTurnPlayerId == endingTurnPlayer",
        ],
        "cleanupOwner": "PhaseRunner.CompleteEndCurrentTurnAfterEndTriggerWithResult",
        "cleanupTests": [
            "ST2-14 attack block restriction option and security",
            "ST3-15 main and security SecurityAttack reduction",
        ],
    },
    {
        "bucket": "UntilEndBattleEffects",
        "sourceFiles": ["Player", "Permanent", "CardController", "GiveEffectToPermanentOrPlayer"],
        "sourcePatterns": [
            "permanent.UntilEndBattleEffects = new List<Func<EffectTiming, ICardEffect>>();",
            "player.UntilEndBattleEffects = new List<Func<EffectTiming, ICardEffect>>();",
            "player.UntilEndBattleEffects.Add(getCardEffect);",
        ],
        "headlessScope": "DurationScope.UntilBattleEnd",
        "headlessPatterns": [
            "UntilBattleEnd",
            "CleanupBattleEnd(GameState state)",
            "modifier.DurationScope == DurationScope.UntilBattleEnd",
            "effect.DurationScope == DurationScope.UntilBattleEnd",
            "_durationCleanupService.CleanupBattleEnd(state)",
        ],
        "cleanupOwner": "AttackService/SecurityCheckService battle-end resolution",
        "cleanupTests": [
            "Duration cleanup UntilBattleEnd removes modifier",
            "FND-003-M OnDetermineDoSecurityCheck sees battle durations before cleanup",
        ],
    },
    {
        "bucket": "UntilEndAttackEffects",
        "sourceFiles": ["Permanent", "AttackProcess", "GiveEffectToPermanentOrPlayer"],
        "sourcePatterns": [
            "permanent.UntilEndAttackEffects = new List<Func<EffectTiming, ICardEffect>>();",
            "targetPermanent.UntilEndAttackEffects.Add(getCardEffect);",
        ],
        "headlessScope": "DurationScope.UntilAttackEnd",
        "headlessPatterns": [
            "UntilAttackEnd",
            "CleanupAttackEnd(GameState state)",
            "modifier.DurationScope == DurationScope.UntilAttackEnd",
            "effect.DurationScope == DurationScope.UntilAttackEnd",
            "_durationCleanupService.CleanupAttackEnd(state)",
        ],
        "cleanupOwner": "AttackService.CleanupAttackRuntime",
        "cleanupTests": [
            "Attack timing battle and attack duration scopes",
        ],
    },
    {
        "bucket": "UntilSecurityCheckEndEffects",
        "sourceFiles": ["Player", "CardController"],
        "sourcePatterns": [
            "player.UntilSecurityCheckEndEffects = new List<Func<EffectTiming, ICardEffect>>();",
        ],
        "headlessScope": "DurationScope.UntilSecurityCheckEnd",
        "headlessPatterns": [
            "UntilSecurityCheckEnd",
            "CleanupSecurityCheckEnd(GameState state)",
            "modifier.DurationScope == DurationScope.UntilSecurityCheckEnd",
            "effect.DurationScope == DurationScope.UntilSecurityCheckEnd",
            "_durationCleanupService.CleanupSecurityCheckEnd(state)",
        ],
        "cleanupOwner": "SecurityCheckService.ContinueAfterSecurityBattleAndCleanup",
        "cleanupTests": [
            "SecurityCheckEnd cleanup runs before next security card",
        ],
    },
    {
        "bucket": "UntilOwnerActivePhaseEffects/UntilNextUntapEffects",
        "sourceFiles": ["Player", "Permanent", "TurnStateMachine", "GiveEffectToPermanentOrPlayer"],
        "sourcePatterns": [
            "gameContext.TurnPlayer.UntilOwnerActivePhaseEffects = new List<Func<EffectTiming, ICardEffect>>();",
            "permanent.UntilNextUntapEffects = new List<Func<EffectTiming, ICardEffect>>();",
            "player.Enemy.UntilOwnerActivePhaseEffects.Add(getCardEffect);",
            "targetPermanent.UntilNextUntapEffects.Add(getCardEffect);",
        ],
        "headlessScope": "DurationScope.UntilOwnerActivePhaseEnd",
        "headlessPatterns": [
            "UntilOwnerActivePhaseEnd",
            "CleanupOwnerActivePhaseEnd(GameState state, PlayerId activePlayer)",
            "modifier.DurationScope == DurationScope.UntilOwnerActivePhaseEnd",
            "effect.DurationScope == DurationScope.UntilOwnerActivePhaseEnd",
            "_durationCleanupService.CleanupOwnerActivePhaseEnd(state, state.TurnPlayerId)",
        ],
        "cleanupOwner": "PhaseRunner.RunActivePhaseWithResult after active-phase unsuspend",
        "cleanupTests": [
            "BattleKeywords Reboot active phase",
        ],
    },
]


REQUIRED_TESTS = sorted(
    {
        "Script runtime Permanent added effect list uses temporary granted effects",
        "Script runtime Player effect list uses temporary granted effects",
        "Duration player runtime modifiers replay deterministic",
        "Duration temporary keyword grants Blocker until cleanup",
        "Duration temporary granted trigger runs from target permanent timing",
        "Duration temporary granted trigger hash and replay deterministic",
        "Duration invariant detects invalid granted trigger",
        *(test for bucket in DURATION_BUCKETS for test in bucket["cleanupTests"]),
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


def pattern_presence(texts: dict[str, str], pattern: str) -> dict[str, Any]:
    matches = []
    for name, text in texts.items():
        lines = line_numbers(text, pattern)
        if lines:
            matches.append(
                {
                    "file": name,
                    "path": str(SOURCE_FILES[name]),
                    "lines": lines[:10],
                }
            )

    return {
        "pattern": pattern,
        "present": len(matches) > 0,
        "matches": matches,
    }


def source_bucket_report() -> list[dict[str, Any]]:
    texts = {name: read_text(path) for name, path in SOURCE_FILES.items()}
    rows = []
    for bucket in DURATION_BUCKETS:
        source_texts = {name: texts[name] for name in bucket["sourceFiles"]}
        pattern_reports = [pattern_presence(source_texts, pattern) for pattern in bucket["sourcePatterns"]]
        rows.append(
            {
                "bucket": bucket["bucket"],
                "sourceFiles": [str(SOURCE_FILES[name]) for name in bucket["sourceFiles"]],
                "headlessScope": bucket["headlessScope"],
                "cleanupOwner": bucket["cleanupOwner"],
                "sourcePatterns": pattern_reports,
                "sourceEvidencePresent": all(pattern["present"] for pattern in pattern_reports),
                "cleanupTests": bucket["cleanupTests"],
            }
        )

    return rows


def headless_surface_report(workspace: Path) -> dict[str, Any]:
    texts = {
        "TemporaryModifier": read_text(workspace / TEMPORARY_MODIFIER),
        "TemporaryGrantedEffect": read_text(workspace / TEMPORARY_GRANTED_EFFECT),
        "DurationCleanupService": read_text(workspace / DURATION_CLEANUP),
        "TemporaryGrantedEffectRegistry": read_text(workspace / TEMPORARY_GRANTED_REGISTRY),
        "ScriptRuntimeFoundation": read_text(workspace / SCRIPT_RUNTIME_DOMAIN),
        "PhaseRunner": read_text(workspace / PHASE_RUNNER),
        "AttackService": read_text(workspace / ATTACK_SERVICE),
        "SecurityCheckService": read_text(workspace / SECURITY_CHECK_SERVICE),
        "TriggerPipelineService": read_text(workspace / TRIGGER_PIPELINE),
    }
    joined = "\n".join(texts.values())

    model_patterns = [
        "public enum DurationScope",
        "public sealed record TemporaryModifier",
        "public sealed record TemporaryGrantedEffect",
        "PlayerId? ExpiresAtTurnPlayerId",
        "CardMetadataCriteria? TargetMetadataCriteria",
        "state.TemporaryModifiers.RemoveAll",
        "state.TemporaryGrantedEffects.RemoveAll",
    ]
    registry_patterns = [
        "TemporaryGrantedEffectDescriptorContext",
        "TryCreateDescriptor",
        "Resolve(CardScriptExecutionContext context, TemporaryGrantedEffect grantedEffect)",
        "TemporaryGrantedEffectRegistry.Unsupported",
    ]
    facade_patterns = [
        "Player.EffectList requires duration-scoped granted effect registry support",
        "public IReadOnlyList<ICardEffect> EffectList(",
        "Permanent.EffectList_Added requires duration-scoped granted effect registry support",
        "public IReadOnlyList<ICardEffect> EffectList_Added(",
        "TemporaryGrantedEffectRegistry temporaryGrantedEffectRegistry",
        "DescriptorBackedCardEffect",
    ]
    pipeline_patterns = [
        "CollectTemporaryGrantedEffectDescriptors",
        "state.TemporaryGrantedEffects",
        "TemporaryGrantedEffect = grantedEffect",
    ]

    bucket_rows = []
    for bucket in DURATION_BUCKETS:
        bucket_patterns = [
            {
                "pattern": pattern,
                "present": pattern in joined,
            }
            for pattern in bucket["headlessPatterns"]
        ]
        bucket_rows.append(
            {
                "bucket": bucket["bucket"],
                "headlessScope": bucket["headlessScope"],
                "cleanupOwner": bucket["cleanupOwner"],
                "patterns": bucket_patterns,
                "covered": all(pattern["present"] for pattern in bucket_patterns),
            }
        )

    return {
        "files": [
            TEMPORARY_MODIFIER.as_posix(),
            TEMPORARY_GRANTED_EFFECT.as_posix(),
            DURATION_CLEANUP.as_posix(),
            TEMPORARY_GRANTED_REGISTRY.as_posix(),
            SCRIPT_RUNTIME_DOMAIN.as_posix(),
            PHASE_RUNNER.as_posix(),
            ATTACK_SERVICE.as_posix(),
            SECURITY_CHECK_SERVICE.as_posix(),
            TRIGGER_PIPELINE.as_posix(),
        ],
        "modelPatterns": [{"pattern": pattern, "present": pattern in joined} for pattern in model_patterns],
        "registryPatterns": [{"pattern": pattern, "present": pattern in joined} for pattern in registry_patterns],
        "facadePatterns": [{"pattern": pattern, "present": pattern in joined} for pattern in facade_patterns],
        "pipelinePatterns": [{"pattern": pattern, "present": pattern in joined} for pattern in pipeline_patterns],
        "durationBuckets": bucket_rows,
        "modelsCovered": all(pattern in joined for pattern in model_patterns),
        "registryCovered": all(pattern in joined for pattern in registry_patterns),
        "facadesCovered": all(pattern in joined for pattern in facade_patterns),
        "pipelineCovered": all(pattern in joined for pattern in pipeline_patterns),
        "durationBucketsCovered": all(row["covered"] for row in bucket_rows),
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


def find_task(fnd001: dict[str, Any], task_id: str) -> dict[str, Any]:
    for task in fnd001.get("tasks", []):
        if task.get("id") == task_id:
            return task
    raise KeyError(f"{task_id} task not found.")


def build_report(workspace: Path) -> dict[str, Any]:
    fnd001 = load_json(workspace, FND001_PARTIAL_CLOSURE)
    gate = load_json(workspace, FOUNDATION_GATE)

    task = find_task(fnd001, "FND001-CS-04")
    source_buckets = source_bucket_report()
    headless = headless_surface_report(workspace)
    tests = test_evidence(workspace)
    gate_summary = gate.get("summary", {})

    provider_catalog_adoption_closed = False
    provider_catalog_boundary_retained = provider_catalog_adoption_closed is False
    strict_bucket_name_parity_closed = False

    conditions = {
        "sourceBucketsCovered": all(bucket["sourceEvidencePresent"] for bucket in source_buckets),
        "headlessDurationModelsCovered": headless["modelsCovered"],
        "headlessCleanupCovered": headless["durationBucketsCovered"],
        "temporaryGrantedRegistryCovered": headless["registryCovered"],
        "runtimeFacadesCovered": headless["facadesCovered"],
        "triggerPipelineTemporaryGrantedCovered": headless["pipelineCovered"],
        "testCandidatesPresent": tests["allRequiredTestsPresent"],
        "providerCatalogBoundaryRetained": provider_catalog_boundary_retained,
        "strictBucketNameBoundaryRetained": strict_bucket_name_parity_closed is False,
        "openCodeStillFalse": gate_summary.get("openCodeReady") is False,
        "continuousStillPartial": gate_summary.get("selectedNextFoundationCapability") == "ContinuousOrStaticEffect"
        and gate_summary.get("selectedNextFoundationStatus") == "PartiallyImplemented",
    }

    passed = all(conditions.values())
    return {
        "schemaVersion": "dcgo.as-is-restart.fnd001cs04-duration-bucket-verification.v1",
        "generatedAt": datetime.now(timezone.utc).isoformat(),
        "goal": "FND-001-A duration bucket cleanup/provider integration parity evidence",
        "inputs": {
            "asIsSourceRoot": str(SOURCE_ROOT),
            "fnd001PartialClosure": FND001_PARTIAL_CLOSURE.as_posix(),
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
            "id": "FND001-CS-04",
            "classification": task.get("classification"),
            "title": task.get("title"),
            "status": "ClosedByEvidence" if passed else "NeedsWork",
            "nextTaskCandidate": "FND001-CS-06",
            "strictBucketNameParityBoundaryTask": "NeedsSourceMapping follow-up",
        },
        "summary": {
            "passed": passed,
            "taskId": "FND001-CS-04",
            "requiredDurationBucketCount": len(DURATION_BUCKETS),
            "coveredSourceBucketCount": sum(1 for bucket in source_buckets if bucket["sourceEvidencePresent"]),
            "coveredHeadlessBucketCount": sum(1 for bucket in headless["durationBuckets"] if bucket["covered"]),
            "requiredTestCount": tests["requiredTestCount"],
            "coveredTestCount": tests["coveredTestCount"],
            "providerCatalogAdoptionClosed": provider_catalog_adoption_closed,
            "providerCatalogBoundaryRetained": provider_catalog_boundary_retained,
            "strictBucketNameParityClosed": strict_bucket_name_parity_closed,
            "additionalSourceCleanupCallsite": "DCGO/Assets/Scripts/Script/CardController.cs",
        },
        "conditions": conditions,
        "sourceBuckets": source_buckets,
        "headlessSurfaces": headless,
        "testEvidence": tests,
        "providerCatalogBoundary": {
            "adoptionClosed": provider_catalog_adoption_closed,
            "handoffRequired": True,
            "handoffTargets": ["TRUST-001-RERUN", "FND-005"],
            "reason": (
                "Duration cleanup and TemporaryGrantedEffect facades are source-aligned, but production "
                "PermanentEffects/provider catalog adoption still requires source-of-truth mapping and "
                "reuse/delete/manual-review classification before it can be trusted."
            ),
        },
        "bucketNameBoundary": {
            "strictBucketNameParityClosed": strict_bucket_name_parity_closed,
            "reason": (
                "Original UntilNextUntapEffects and UntilOwnerActivePhaseEffects map to the headless "
                "UntilOwnerActivePhaseEnd cleanup event by timing, not by identical bucket naming."
            ),
        },
        "gateSummary": {
            "openCodeReady": gate_summary.get("openCodeReady"),
            "selectedNextFoundationCapability": gate_summary.get("selectedNextFoundationCapability"),
            "selectedNextFoundationStatus": gate_summary.get("selectedNextFoundationStatus"),
            "unsupportedCapabilityCount": gate_summary.get("unsupportedCapabilityCount"),
            "partiallyImplementedCapabilityCount": gate_summary.get("partiallyImplementedCapabilityCount"),
        },
        "trust001Handoff": {
            "handoffRequired": True,
            "items": [
                "Treat DurationCleanupService and TemporaryGrantedEffectRegistry as ReuseCandidate only after TRUST-001 maps them back to Player/Permanent/TurnStateMachine/AttackProcess/CardController evidence.",
                "Production provider catalog adoption remains unclosed and should be separated from duration cleanup evidence.",
                "Strict bucket-name parity for UntilNextUntapEffects versus UntilOwnerActivePhaseEnd remains source-mapping review, not a status promotion reason.",
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
