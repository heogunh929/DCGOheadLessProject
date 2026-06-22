# -*- coding: utf-8 -*-
"""Generate the source-aligned full mechanic/effect inventory.

This is an inventory and mapping tool only. It does not port card effects or
change engine behavior. Mapping status is conservative: source usage alone is
never enough to mark an item Implemented or Verified.
"""

from __future__ import annotations

import argparse
import hashlib
import json
import re
import sys
from collections import Counter, defaultdict
from pathlib import Path
from typing import Any, Iterable

SCRIPT_DIR = Path(__file__).resolve().parent
if str(SCRIPT_DIR) not in sys.path:
    sys.path.insert(0, str(SCRIPT_DIR))

from generate_full_card_pool_manifest import (  # noqa: E402
    assert_source_lock,
    file_sha256,
    unix_path,
)


STATUS_VERIFIED = "Verified"
STATUS_IMPLEMENTED = "Implemented"
STATUS_PARTIAL = "PartiallyImplemented"
STATUS_UNSUPPORTED = "Unsupported"
STATUS_NOT_REFERENCED = "NotReferenced"
STATUS_NEEDS_REVIEW = "NeedsSourceReview"

SOURCE_SCOPE_GLOBS = [
    "DCGO/Assets/Scripts/CardEffect/**/*.cs",
    "DCGO/Assets/Scripts/Script/**/*.cs",
]

LOCAL_SOURCE_BASE_CANDIDATES = [
    Path(r"E:\headlessDCGO"),
]

TIMING_STATUS_OVERRIDES = {
    "None": STATUS_PARTIAL,
    "OnEnterFieldAnyone": STATUS_PARTIAL,
}

TIMING_STATUS_OVERRIDE_NOTES = {
    "None": "EffectTiming.None is the source enum channel used for static/continuous effects and is tracked by ContinuousOrStaticEffect foundation coverage.",
    "OnEnterFieldAnyone": "PlayCardService and DigivolveService chain a global enter-field payload after self OnPlay/WhenDigivolving groups; source ordering and all enter-field variants remain partial.",
}

CARD_EFFECT_ROOT = "DCGO/Assets/Scripts/CardEffect/"
SCRIPT_ROOT = "DCGO/Assets/Scripts/Script/"

FEATURE_PATTERNS: dict[str, list[str]] = {
    "static_or_continuous": [r"EffectTiming\.None", r"SetUpICardEffect", r"ChangeDP", r"ChangeSAttack", r"CanNot"],
    "inherited": [r"SetIsInheritedEffect\s*\(\s*true\s*\)", r"IsInheritedEffect"],
    "linked": [r"SetIsLinkedEffect\s*\(\s*true\s*\)", r"IsLinkedEffect", r"LinkedCards"],
    "security": [r"SetIsSecurityEffect\s*\(\s*true\s*\)", r"EffectTiming\.SecuritySkill", r"SecuritySkill"],
    "counter": [r"SetIsCounterEffect\s*\(\s*true\s*\)", r"EffectTiming\.OnCounterTiming", r"IsCounterEffect"],
    "declarative": [r"SetIsDeclarative\s*\(\s*true\s*\)", r"IsDeclarative"],
    "background": [r"SetIsBackgroundProcess\s*\(\s*true\s*\)", r"IsBackgroundProcess"],
    "optional": [r"SetIsOptional\s*\(\s*true\s*\)", r"Activate_Optional", r"SetUpActivateClass\([^;]*,\s*true\s*,", r"IsOptional"],
    "skippable": [r"SetIsSkippable", r"canNoSelect:\s*true", r"canNoSelect:\s*\(\)\s*=>\s*true", r"IsSkippable"],
    "max_count_per_turn": [r"SetMaxCountPerTurn", r"SetUpActivateClass\([^;]*,\s*[-0-9]+\s*,", r"MaxCountPerTurn"],
    "chain_activation": [r"SetChainActivationCount", r"ChainActivations"],
    "zone_movement": [r"AddTrashCard", r"AddHandCard", r"AddSecurity", r"ReturnToHand", r"ReturnToDeck", r"TrashCards", r"HandCards", r"SecurityCards", r"MoveTo", r"Play", r"Digivolv"],
    "modifier_duration": [r"UntilTurnEnd", r"UntilOpponentTurnEnd", r"UntilOwnerTurnEnd", r"UntilBattleEnd", r"UntilAttackEnd", r"UntilSecurityCheckEnd", r"ChangeDP", r"ChangeSAttack", r"CanNotAttack", r"CanNotBlock"],
    "trigger_priority": [r"MultipleSkills", r"AfterEffectsActivate", r"StackSkillInfos", r"TriggeredSkillProcess"],
    "replacement_or_cut_in": [r"WhenPermanentWouldBeDeleted", r"WhenWould", r"OnCounterTiming", r"IsCounterEffect", r"prevent", r"doesn't", r"CutIn"],
    "trigger_on_play": [r"\[On Play\]", r"\bIsOnPlay\b", r"OnPlayCheckHashtable", r"PermanentEnterField\.OnPlay"],
    "trigger_when_digivolving": [r"\[When Digivolving\]", r"\bIsWhenDigivolving\b", r"WhenDigivolvingCheckHashtable", r"PermanentEnterField\.WhenDigivolving"],
}

SELECTION_PATTERNS: dict[str, list[str]] = {
    "SelectCard": [r"SelectCardEffect", r"CardSelection"],
    "SelectHand": [r"SelectHandEffect"],
    "SelectPermanent": [r"SelectPermanentEffect", r"PermanentSelection"],
    "SelectDeck": [r"SelectDeck"],
    "SelectCount": [r"SelectCountEffect", r"SetCountSelection"],
    "SelectBoolean": [r"SetBoolSelection", r"SelectedBoolValue"],
    "SelectInteger": [r"SetIntSelection", r"SelectedIntValue"],
    "SelectOrder": [r"MultipleSkills", r"Select.*Order"],
    "SelectSecurity": [r"SecurityCards", r"Select.*Security"],
    "SelectFieldSlot": [r"FieldSlot", r"BattleAreaIndex"],
    "SelectAttackTarget": [r"SelectAttackEffect", r"AttackTarget"],
    "SelectJogress": [r"SelectJogressEffect", r"DNA", r"Jogress"],
    "SelectDigiXros": [r"SelectDigiXrosClass", r"DigiXros"],
    "SelectAssembly": [r"SelectAssemblyClass", r"Assembly"],
    "SelectAppFusion": [r"SelectAppFusionEffect", r"AppFusion"],
    "SelectBurstDigivolution": [r"SelectBurstDigivolutionEffect", r"Burst"],
}

ROOT_ZONE_PATTERNS: dict[str, str] = {
    "Hand": r"Root\.Hand|SelectHandEffect|HandCards",
    "Trash": r"Root\.Trash|TrashCards",
    "Deck": r"Root\.Deck|LibraryCards|Deck",
    "Security": r"SecurityCards|Root\.Security",
    "BattleArea": r"BattleArea|Permanent",
    "Breeding": r"Breeding",
    "DigiEggDeck": r"DigiEgg",
    "Executing": r"Executing|SecurityDigimon|ShowUseHandCard",
    "Revealed": r"Reveal",
}

SPECIAL_MECHANIC_PATTERNS: dict[str, list[str]] = {
    "Jogress": [r"Jogress", r"DNA"],
    "BurstDigivolution": [r"BurstDigivolution", r"Burst"],
    "AppFusion": [r"AppFusion"],
    "DigiXros": [r"DigiXros", r"Digi-Xros"],
    "Assembly": [r"Assembly"],
    "Link": [r"LinkDP", r"LinkedCards", r"WhenLinked", r"WhenWouldLink"],
    "DelayOption": [r"\bDelay\b"],
    "AceOverflow": [r"\bACE\b", r"Overflow"],
    "BlastDigivolution": [r"BlastDigivolution", r"Blast Digivol"],
    "DigiBurst": [r"DigiBurst", r"Digiburst"],
    "Digisorption": [r"Digisorption", r"DigiSorption"],
}

SOURCE_KEYWORD_NAMES = [
    "Alliance",
    "ArmorPurge",
    "Ascension",
    "Barrier",
    "BlastDNADigivolution",
    "BlastDigivolution",
    "Blitz",
    "Blocker",
    "Collision",
    "Decode",
    "Decoy",
    "Evade",
    "Execute",
    "Fortitude",
    "Fragment",
    "Iceclad",
    "Jamming",
    "Link",
    "MaterialSave",
    "MindLink",
    "Overclock",
    "Partition",
    "Pierce",
    "Piercing",
    "Progress",
    "Raid",
    "Reboot",
    "Retaliation",
    "Rush",
    "Save",
    "Scapegoat",
    "SecurityAttack",
    "Training",
    "Vortex",
]

ENGINE_FEATURE_TOKENS: dict[str, list[str]] = {
    "static_or_continuous": [
        "ContinuousEffectService",
        "ContinuousEffectDescriptor",
        "EffectiveStatService",
        "StaticEffectService",
        "StaticRequirementService",
        "StaticCardColorDescriptor",
        "StaticCardNameDescriptor",
        "StaticCardTraitDescriptor",
        "StaticCardLevelDescriptor",
        "StaticPermanentLevelDescriptor",
        "IgnoreColorRequirementDescriptor",
        "CardMetadataSnapshot",
    ],
    "inherited": ["SourceRole.Inherited", "TriggerSourceRole.Inherited", "IsInherited"],
    "linked": ["SourceRole.Linked", "LinkedCards", "Mechanic.Link"],
    "security": ["SecurityCheckService", "SecurityEffectExecutionService", "EffectTiming.SecuritySkill"],
    "counter": ["AttackService", "OnCounterTiming", "CounterCandidate"],
    "declarative": ["IsDeclarative", "Declarative"],
    "background": ["BackgroundEffects", "TriggerStackFrame", "IsBackground"],
    "optional": ["OptionalEffectBoundary", "SelectionKind.SelectYesNo"],
    "skippable": ["CanSkip", "IsSkippable"],
    "max_count_per_turn": ["OncePerTurnTracker", "RuntimeRuleState"],
    "chain_activation": ["ChainActivations", "MaxTriggerStackDepth"],
    "zone_movement": ["ZoneMover", "Tier1PrimitiveService"],
    "modifier_duration": ["TemporaryModifier", "DurationScope", "DurationCleanupService"],
    "trigger_priority": ["TriggerStackFrame", "AfterEffectsActivate", "Ordering"],
    "replacement_or_cut_in": ["OnCounterTiming", "WhenPermanentWouldBeDeleted", "UnsupportedMechanicException"],
    "trigger_on_play": ["EffectTiming.OnPlay", "OnPlay", "OnEnterFieldAnyone"],
    "trigger_when_digivolving": ["EffectTiming.WhenDigivolving", "WhenDigivolving", "OnEnterFieldAnyone"],
}

SELECTION_ENGINE_TOKENS: dict[str, list[str]] = {
    "SelectCard": ["SelectionKind.SelectCard", "SelectionTargetKind.Card"],
    "SelectHand": ["SelectionKind.SelectCard", "Zone.Hand"],
    "SelectPermanent": ["SelectionKind.SelectPermanent", "SelectionTargetKind.Permanent"],
    "SelectDeck": ["SelectionKind.SelectCard", "Zone.Deck"],
    "SelectCount": ["SelectionKind.SelectCount", "SelectionTargetKind.Count"],
    "SelectBoolean": ["SelectionKind.SelectYesNo", "SelectionTargetKind.Boolean"],
    "SelectInteger": ["SelectionKind.SelectCount", "SelectionTargetKind.Option"],
    "SelectOrder": ["SelectionKind.SelectOrder", "ordering"],
    "SelectSecurity": ["SelectionKind.SelectSecurity", "SelectionTargetKind.Security"],
    "SelectFieldSlot": ["SelectionKind.SelectFieldSlot", "SelectionTargetKind.FieldSlot"],
    "SelectAttackTarget": ["AttackTarget", "LegalActionKind.Attack"],
    "SelectJogress": ["Mechanic.Jogress", "ComplexMechanicSelectionFactory"],
    "SelectDigiXros": ["Mechanic.DigiXros", "ComplexMechanicSelectionFactory"],
    "SelectAssembly": ["Mechanic.Assembly", "ComplexMechanicSelectionFactory"],
    "SelectAppFusion": ["Mechanic.AppFusion", "ComplexMechanicSelectionFactory"],
    "SelectBurstDigivolution": ["Mechanic.BurstDigivolution", "ComplexMechanicSelectionFactory"],
}


def read_json(path: Path) -> Any:
    return json.loads(path.read_text(encoding="utf-8-sig"))


def write_json(path: Path, value: Any) -> None:
    path.write_text(json.dumps(value, ensure_ascii=False, indent=2, sort_keys=False) + "\n", encoding="utf-8")


def resolve_source_base(workspace: Path) -> Path:
    candidates = [workspace, *LOCAL_SOURCE_BASE_CANDIDATES]
    for candidate in candidates:
        if (candidate / "DCGO/Assets/Scripts/Script/ICardEffect.cs").exists():
            return candidate

    raise RuntimeError("Unable to resolve DCGO source root. Expected DCGO/Assets in the workspace or E:\\headlessDCGO\\DCGO\\Assets.")


def assert_current_source_fingerprint_at(workspace: Path, source_base: Path, lock: dict[str, Any]) -> None:
    selected = lock["selectedSnapshot"]
    manifest = read_json(workspace / selected["fileManifest"])
    expected_paths: dict[str, dict[str, Any]] = {entry["path"]: entry for entry in manifest["files"]}

    for relative_path, entry in expected_paths.items():
        full_path = source_base / relative_path
        if not full_path.exists():
            raise RuntimeError(f"source fingerprint mismatch: missing file {relative_path}")
        if full_path.stat().st_size != int(entry["size"]):
            raise RuntimeError(f"source fingerprint mismatch: size changed {relative_path}")
        actual_hash = file_sha256(full_path)
        if actual_hash.lower() != str(entry["sha256"]).lower():
            raise RuntimeError(f"source fingerprint mismatch: hash changed {relative_path}")

    for root in manifest["roots"]:
        root_path = source_base / root
        if not root_path.exists():
            raise RuntimeError(f"source fingerprint mismatch: missing root {root}")
        for current_file in sorted(path for path in root_path.rglob("*") if path.is_file()):
            relative_path = unix_path(current_file, source_base)
            if relative_path not in expected_paths:
                raise RuntimeError(f"source fingerprint mismatch: unexpected file {relative_path}")


def read_all_source_files(workspace: Path, globs: Iterable[str], relative_base: Path | None = None) -> dict[str, str]:
    base = relative_base or workspace
    files: dict[str, str] = {}
    for glob in globs:
        for file_path in sorted(base.glob(glob)):
            if file_path.is_file():
                files[unix_path(file_path, base)] = file_path.read_text(encoding="utf-8-sig", errors="replace")
    return files


def regex_count(text: str, patterns: Iterable[str]) -> int:
    return sum(len(re.findall(pattern, text, flags=re.IGNORECASE | re.MULTILINE | re.DOTALL)) for pattern in patterns)


def has_any(text: str, patterns: Iterable[str]) -> bool:
    return regex_count(text, patterns) > 0


def extract_enum_values(text: str, enum_name: str) -> list[str]:
    match = re.search(rf"enum\s+{re.escape(enum_name)}\s*\{{(?P<body>.*?)\}}", text, flags=re.DOTALL)
    if not match:
        return []
    body = re.sub(r"//.*", "", match.group("body"))
    values: list[str] = []
    for raw_item in body.split(","):
        item = raw_item.strip()
        if not item:
            continue
        item = item.split("=")[0].strip()
        if re.match(r"^[A-Za-z_][A-Za-z0-9_]*$", item):
            values.append(item)
    return values


def source_count(files: dict[str, str], patterns: Iterable[str], path_prefix: str | None = None) -> tuple[int, int, list[str]]:
    matched_paths: list[str] = []
    occurrence_count = 0
    for path, text in files.items():
        if path_prefix and not path.startswith(path_prefix):
            continue
        count = regex_count(text, patterns)
        if count:
            matched_paths.append(path)
            occurrence_count += count
    return len(matched_paths), occurrence_count, matched_paths


def compact_paths(paths: list[str], limit: int = 12) -> list[str]:
    return paths[:limit]


def status_from_evidence(
    source_files: int,
    engine_symbol_present: bool,
    engine_source_refs: int,
    engine_test_refs: int,
    unsupported_refs: int = 0,
) -> str:
    if source_files == 0:
        return STATUS_NOT_REFERENCED
    if unsupported_refs > 0 and engine_source_refs == 0:
        return STATUS_UNSUPPORTED
    if not engine_symbol_present and engine_source_refs == 0:
        return STATUS_UNSUPPORTED
    if engine_source_refs > 0 and engine_test_refs > 0:
        return STATUS_VERIFIED
    if engine_source_refs > 0:
        return STATUS_PARTIAL
    if engine_symbol_present:
        return STATUS_NEEDS_REVIEW
    return STATUS_NEEDS_REVIEW


def affected_records_for_paths(
    card_records_by_effect: dict[str, list[dict[str, Any]]],
    class_names: Iterable[str],
) -> list[dict[str, Any]]:
    seen: set[tuple[str, int | None, str]] = set()
    records: list[dict[str, Any]] = []
    for class_name in class_names:
        for record in card_records_by_effect.get(class_name, []):
            key = (record["cardId"], record["cardIndex"], record["variantKey"])
            if key in seen:
                continue
            seen.add(key)
            records.append(
                {
                    "definitionStableId": record["definitionStableId"],
                    "cardId": record["cardId"],
                    "cardIndex": record["cardIndex"],
                    "variantKey": record["variantKey"],
                    "assetPath": record["assetPath"],
                }
            )
    records.sort(key=lambda item: (item["cardId"], item["cardIndex"] or -1, item["variantKey"]))
    return records


def effect_class_name_from_path(path: str) -> str | None:
    if not path.startswith(CARD_EFFECT_ROOT) or not path.endswith(".cs"):
        return None
    return Path(path).stem


def make_evidence(
    source_file_count: int,
    source_occurrence_count: int,
    source_paths: list[str],
    engine_source_refs: int,
    engine_test_refs: int,
    notes: list[str] | None = None,
) -> dict[str, Any]:
    return {
        "sourceFileCount": source_file_count,
        "sourceOccurrenceCount": source_occurrence_count,
        "sampleSourcePaths": compact_paths(source_paths),
        "engineSourceReferenceCount": engine_source_refs,
        "engineTestReferenceCount": engine_test_refs,
        "notes": notes or [],
    }


def build_inventory(workspace: Path) -> dict[str, Any]:
    lock = assert_source_lock(workspace)
    source_base = resolve_source_base(workspace)
    assert_current_source_fingerprint_at(workspace, source_base, lock)

    card_manifest_path = workspace / "docs/generated/full-card-pool-manifest.json"
    if not card_manifest_path.exists():
        raise RuntimeError("docs/generated/full-card-pool-manifest.json 파일이 없습니다. 62번 manifest를 먼저 생성해야 합니다.")
    card_manifest = read_json(card_manifest_path)

    source_files = read_all_source_files(workspace, SOURCE_SCOPE_GLOBS, source_base)
    card_effect_source_files = {path: text for path, text in source_files.items() if path.startswith(CARD_EFFECT_ROOT)}
    engine_files = read_all_source_files(workspace, ["src/DCGO.RL.Engine/**/*.cs"])
    engine_test_files = read_all_source_files(workspace, ["src/DCGO.RL.Engine.Tests/**/*.cs"])
    docs_files = read_all_source_files(workspace, ["docs/rl-engine/**/*.md"])

    source_icard_effect = (source_base / "DCGO/Assets/Scripts/Script/ICardEffect.cs").read_text(encoding="utf-8-sig", errors="replace")
    engine_effect_timing = (workspace / "src/DCGO.RL.Engine/Effects/EffectTiming.cs").read_text(encoding="utf-8-sig", errors="replace")
    source_timings = extract_enum_values(source_icard_effect, "EffectTiming")
    engine_timings = set(extract_enum_values(engine_effect_timing, "EffectTiming"))

    engine_text = "\n".join(engine_files.values())
    engine_test_text = "\n".join(engine_test_files.values())
    docs_text = "\n".join(docs_files.values())
    engine_battle_keyword_text = (workspace / "src/DCGO.RL.Engine/Domain/Enums.cs").read_text(encoding="utf-8-sig", errors="replace")
    engine_keywords = set(extract_enum_values(engine_battle_keyword_text, "BattleKeyword"))
    engine_mechanics = set(extract_enum_values(engine_battle_keyword_text, "Mechanic"))

    card_records_by_effect: dict[str, list[dict[str, Any]]] = defaultdict(list)
    for record in card_manifest["records"]:
        class_name = record.get("cardEffectClassName")
        if class_name:
            card_records_by_effect[class_name].append(record)

    effect_source_records: list[dict[str, Any]] = []
    for path, text in sorted(card_effect_source_files.items()):
        class_name = effect_class_name_from_path(path) or Path(path).stem
        timings = sorted(set(re.findall(r"EffectTiming\.([A-Za-z_][A-Za-z0-9_]*)", text)))
        flags = {
            name: has_any(text, patterns)
            for name, patterns in FEATURE_PATTERNS.items()
        }
        selections = {
            name: has_any(text, patterns)
            for name, patterns in SELECTION_PATTERNS.items()
        }
        roots = {
            name: bool(re.search(pattern, text, flags=re.IGNORECASE | re.MULTILINE))
            for name, pattern in ROOT_ZONE_PATTERNS.items()
        }
        factory_api = sorted(set(re.findall(r"CardEffectFactory\.([A-Za-z_][A-Za-z0-9_]*)", text)))
        commons_api = sorted(set(re.findall(r"CardEffectCommons\.([A-Za-z_][A-Za-z0-9_]*)", text)))
        class_api = sorted(set(re.findall(r"new\s+([A-Za-z_][A-Za-z0-9_]*Class)\s*\(", text)))
        set_methods = sorted(set(re.findall(r"\.(Set[A-Za-z_][A-Za-z0-9_]*)\s*\(", text)))
        affected = affected_records_for_paths(card_records_by_effect, [class_name])
        effect_source_records.append(
            {
                "effectClassName": class_name,
                "sourcePath": path,
                "timings": timings,
                "flags": {key: value for key, value in flags.items() if value},
                "selectionKinds": [key for key, value in selections.items() if value],
                "rootZones": [key for key, value in roots.items() if value],
                "factoryApi": factory_api,
                "commonsApi": commons_api,
                "classApi": class_api,
                "setMethods": set_methods,
                "affectedCards": affected,
                "affectedCardCount": len(affected),
            }
        )

    timings: list[dict[str, Any]] = []
    for timing in sorted(set(source_timings) | engine_timings):
        patterns = [rf"EffectTiming\.{re.escape(timing)}\b"]
        card_source_file_count, card_source_occurrences, card_source_paths = source_count(card_effect_source_files, patterns)
        full_source_file_count, full_source_occurrences, full_source_paths = source_count(source_files, patterns)
        engine_source_refs = regex_count(engine_text, [rf"EffectTiming\.{re.escape(timing)}\b"])
        engine_test_refs = regex_count(engine_test_text + "\n" + docs_text, [rf"\b{re.escape(timing)}\b"])
        matched_class_names = sorted(
            class_name
            for class_name in (effect_class_name_from_path(path) for path in card_source_paths)
            if class_name
        )
        affected = affected_records_for_paths(card_records_by_effect, matched_class_names)
        status = status_from_evidence(
            full_source_file_count,
            timing in engine_timings,
            engine_source_refs,
            engine_test_refs,
        )
        status = TIMING_STATUS_OVERRIDES.get(timing, status)
        evidence_notes = [
            "Implemented/Verified mapping uses source usage, engine enum/service references, and test/doc evidence conservatively.",
        ]
        if timing in TIMING_STATUS_OVERRIDE_NOTES:
            evidence_notes.append(TIMING_STATUS_OVERRIDE_NOTES[timing])
        timings.append(
            {
                "name": timing,
                "sourceEnumPresent": timing in source_timings,
                "engineEnumPresent": timing in engine_timings,
                "mappingStatus": status,
                "cardEffectSourceFileCount": card_source_file_count,
                "cardEffectOccurrenceCount": card_source_occurrences,
                "sourceScopeFileCount": full_source_file_count,
                "sourceScopeOccurrenceCount": full_source_occurrences,
                "affectedCardCount": len(affected),
                "affectedCards": affected,
                "evidence": make_evidence(
                    full_source_file_count,
                    full_source_occurrences,
                    full_source_paths,
                    engine_source_refs,
                    engine_test_refs,
                    evidence_notes,
                ),
            }
        )

    features: list[dict[str, Any]] = []
    for feature_name, patterns in sorted(FEATURE_PATTERNS.items()):
        file_count, occurrence_count, paths = source_count(source_files, patterns)
        card_file_count, _, card_paths = source_count(card_effect_source_files, patterns)
        engine_tokens = ENGINE_FEATURE_TOKENS.get(feature_name, [feature_name])
        engine_source_refs = regex_count(engine_text, [re.escape(token) for token in engine_tokens])
        engine_test_refs = regex_count(engine_test_text + "\n" + docs_text, [re.escape(token) for token in engine_tokens])
        unsupported_refs = regex_count(engine_text, [rf"Unsupported.*{re.escape(feature_name)}", rf"{re.escape(feature_name)}.*Unsupported"])
        class_names = sorted(
            class_name
            for class_name in (effect_class_name_from_path(path) for path in card_paths)
            if class_name
        )
        affected = affected_records_for_paths(card_records_by_effect, class_names)
        status = status_from_evidence(file_count, engine_source_refs > 0, engine_source_refs, engine_test_refs, unsupported_refs)
        features.append(
            {
                "name": feature_name,
                "mappingStatus": status,
                "sourceFileCount": file_count,
                "sourceOccurrenceCount": occurrence_count,
                "cardEffectSourceFileCount": card_file_count,
                "affectedCardCount": len(affected),
                "affectedCards": affected,
                "evidence": make_evidence(file_count, occurrence_count, paths, engine_source_refs, engine_test_refs),
            }
        )

    selections: list[dict[str, Any]] = []
    for selection_name, patterns in sorted(SELECTION_PATTERNS.items()):
        file_count, occurrence_count, paths = source_count(source_files, patterns)
        card_file_count, _, card_paths = source_count(card_effect_source_files, patterns)
        engine_tokens = SELECTION_ENGINE_TOKENS.get(selection_name, [selection_name])
        engine_source_refs = regex_count(engine_text, [re.escape(token) for token in engine_tokens])
        engine_test_refs = regex_count(engine_test_text + "\n" + docs_text, [re.escape(token) for token in engine_tokens])
        class_names = sorted(
            class_name
            for class_name in (effect_class_name_from_path(path) for path in card_paths)
            if class_name
        )
        affected = affected_records_for_paths(card_records_by_effect, class_names)
        selections.append(
            {
                "name": selection_name,
                "mappingStatus": status_from_evidence(file_count, engine_source_refs > 0, engine_source_refs, engine_test_refs),
                "sourceFileCount": file_count,
                "sourceOccurrenceCount": occurrence_count,
                "cardEffectSourceFileCount": card_file_count,
                "affectedCardCount": len(affected),
                "affectedCards": affected,
                "evidence": make_evidence(file_count, occurrence_count, paths, engine_source_refs, engine_test_refs),
            }
        )

    root_zones: list[dict[str, Any]] = []
    for zone_name, pattern in sorted(ROOT_ZONE_PATTERNS.items()):
        file_count, occurrence_count, paths = source_count(source_files, [pattern])
        engine_source_refs = regex_count(engine_text, [rf"Zone\.{re.escape(zone_name)}\b", re.escape(zone_name)])
        engine_test_refs = regex_count(engine_test_text + "\n" + docs_text, [rf"Zone\.{re.escape(zone_name)}\b", re.escape(zone_name)])
        root_zones.append(
            {
                "name": zone_name,
                "mappingStatus": status_from_evidence(file_count, engine_source_refs > 0, engine_source_refs, engine_test_refs),
                "sourceFileCount": file_count,
                "sourceOccurrenceCount": occurrence_count,
                "evidence": make_evidence(file_count, occurrence_count, paths, engine_source_refs, engine_test_refs),
            }
        )

    special_mechanics: list[dict[str, Any]] = []
    for mechanic_name, patterns in sorted(SPECIAL_MECHANIC_PATTERNS.items()):
        file_count, occurrence_count, paths = source_count(source_files, patterns)
        card_file_count, _, card_paths = source_count(card_effect_source_files, patterns)
        engine_symbol_present = mechanic_name in engine_mechanics or mechanic_name.replace("Blast", "") in engine_mechanics
        engine_source_refs = regex_count(engine_text, [re.escape(mechanic_name)])
        engine_test_refs = regex_count(engine_test_text + "\n" + docs_text, [re.escape(mechanic_name)])
        class_names = sorted(
            class_name
            for class_name in (effect_class_name_from_path(path) for path in card_paths)
            if class_name
        )
        affected = affected_records_for_paths(card_records_by_effect, class_names)
        special_mechanics.append(
            {
                "name": mechanic_name,
                "mappingStatus": status_from_evidence(file_count, engine_symbol_present, engine_source_refs, engine_test_refs),
                "engineMechanicEnumPresent": engine_symbol_present,
                "sourceFileCount": file_count,
                "sourceOccurrenceCount": occurrence_count,
                "cardEffectSourceFileCount": card_file_count,
                "affectedCardCount": len(affected),
                "affectedCards": affected,
                "evidence": make_evidence(file_count, occurrence_count, paths, engine_source_refs, engine_test_refs),
            }
        )

    keyword_candidates = sorted(set(SOURCE_KEYWORD_NAMES) | set(engine_keywords))
    keywords: list[dict[str, Any]] = []
    card_text_keyword_counts = Counter()
    for record in card_manifest["records"]:
        for keyword in record.get("keywords", []):
            normalized = keyword.replace(" ", "").replace("-", "")
            card_text_keyword_counts[normalized] += 1
            card_text_keyword_counts[keyword] += 1

    for keyword_name in keyword_candidates:
        patterns = [rf"\b{re.escape(keyword_name)}\b", re.escape(keyword_name.replace("Piercing", "Pierce"))]
        file_count, occurrence_count, paths = source_count(source_files, patterns)
        engine_symbol_present = keyword_name in engine_keywords or (keyword_name == "Pierce" and "Piercing" in engine_keywords)
        engine_source_refs = regex_count(engine_text, [re.escape(keyword_name), re.escape("Piercing" if keyword_name == "Pierce" else keyword_name)])
        engine_test_refs = regex_count(engine_test_text + "\n" + docs_text, [re.escape(keyword_name), re.escape("Piercing" if keyword_name == "Pierce" else keyword_name)])
        text_count = card_text_keyword_counts.get(keyword_name, 0) + card_text_keyword_counts.get(keyword_name.replace(" ", ""), 0)
        source_signal_count = file_count if file_count > 0 else (1 if text_count > 0 else 0)
        status = status_from_evidence(source_signal_count, engine_symbol_present, engine_source_refs, engine_test_refs)
        keywords.append(
            {
                "name": keyword_name,
                "mappingStatus": status,
                "engineKeywordEnumPresent": engine_symbol_present,
                "sourceFileCount": file_count,
                "sourceOccurrenceCount": occurrence_count,
                "cardTextKeywordRecordCount": text_count,
                "evidence": make_evidence(file_count, occurrence_count, paths, engine_source_refs, engine_test_refs),
            }
        )

    factory_api_counter: Counter[str] = Counter()
    commons_api_counter: Counter[str] = Counter()
    set_method_counter: Counter[str] = Counter()
    for record in effect_source_records:
        factory_api_counter.update(record["factoryApi"])
        commons_api_counter.update(record["commonsApi"])
        set_method_counter.update(record["setMethods"])

    source_scope = {
        "cardEffectSourceFileCount": len(card_effect_source_files),
        "scriptSourceFileCount": len(source_files) - len(card_effect_source_files),
        "sourceScopeFileCount": len(source_files),
        "cardEffectSourceClassCount": len({record["effectClassName"] for record in effect_source_records}),
        "gameplayCardRecordCount": len(card_manifest["records"]),
    }
    status_counts = {
        "timings": dict(Counter(item["mappingStatus"] for item in timings)),
        "features": dict(Counter(item["mappingStatus"] for item in features)),
        "selections": dict(Counter(item["mappingStatus"] for item in selections)),
        "rootZones": dict(Counter(item["mappingStatus"] for item in root_zones)),
        "specialMechanics": dict(Counter(item["mappingStatus"] for item in special_mechanics)),
        "keywords": dict(Counter(item["mappingStatus"] for item in keywords)),
    }

    missing_items: list[dict[str, Any]] = []
    for category, items in [
        ("timing", timings),
        ("feature", features),
        ("selection", selections),
        ("rootZone", root_zones),
        ("specialMechanic", special_mechanics),
        ("keyword", keywords),
    ]:
        for item in items:
            if item["mappingStatus"] in {STATUS_PARTIAL, STATUS_UNSUPPORTED, STATUS_NEEDS_REVIEW} and (
                item.get("sourceFileCount", item.get("sourceScopeFileCount", 0)) > 0
                or item.get("cardTextKeywordRecordCount", 0) > 0
            ):
                missing_items.append(
                    {
                        "category": category,
                        "name": item["name"],
                        "mappingStatus": item["mappingStatus"],
                        "sourceFileCount": item.get("sourceFileCount", item.get("sourceScopeFileCount", 0)),
                        "affectedCardCount": item.get("affectedCardCount", 0),
                        "engineSourceReferenceCount": item["evidence"]["engineSourceReferenceCount"],
                        "engineTestReferenceCount": item["evidence"]["engineTestReferenceCount"],
                        "sampleSourcePaths": item["evidence"]["sampleSourcePaths"],
                    }
                )
    missing_items.sort(key=lambda item: (item["mappingStatus"], -item["affectedCardCount"], -item["sourceFileCount"], item["category"], item["name"]))

    dependency_graph = {
        "nodes": [
            {"id": "CardBaseEntity", "label": "CardBaseEntity assets", "kind": "source"},
            {"id": "CardEffectClassName", "label": "CardEffectClassName", "kind": "identity"},
            {"id": "CardEffectSource", "label": "CardEffect source files", "kind": "source"},
            {"id": "ICardEffect", "label": "ICardEffect flags/timings", "kind": "source-api"},
            {"id": "FactoryCommons", "label": "CardEffectFactory/Commons", "kind": "source-api"},
            {"id": "SelectionApis", "label": "Unity selection APIs", "kind": "source-api"},
            {"id": "TriggerPipeline", "label": "RL TriggerPipelineService", "kind": "engine"},
            {"id": "RuleProcessor", "label": "RL RuleProcessor", "kind": "engine"},
            {"id": "AttackSecurity", "label": "RL Attack/Security services", "kind": "engine"},
            {"id": "ComplexMechanics", "label": "RL ComplexMechanicService", "kind": "engine"},
            {"id": "MissingLayers", "label": "Missing/partial layers", "kind": "report"},
        ],
        "edges": [
            {"from": "CardBaseEntity", "to": "CardEffectClassName", "label": f"{source_scope['gameplayCardRecordCount']} gameplay records"},
            {"from": "CardEffectClassName", "to": "CardEffectSource", "label": f"{source_scope['cardEffectSourceClassCount']} source classes"},
            {"from": "CardEffectSource", "to": "ICardEffect", "label": "EffectTiming / flags / CanActivate"},
            {"from": "CardEffectSource", "to": "FactoryCommons", "label": "factory/commons API usage"},
            {"from": "CardEffectSource", "to": "SelectionApis", "label": "selection/root-zone usage"},
            {"from": "ICardEffect", "to": "TriggerPipeline", "label": "timing, priority, optional, background"},
            {"from": "ICardEffect", "to": "RuleProcessor", "label": "RulesTiming / state stabilization"},
            {"from": "ICardEffect", "to": "AttackSecurity", "label": "counter/block/security/battle hooks"},
            {"from": "FactoryCommons", "to": "ComplexMechanics", "label": "special play/digivolve/material mechanics"},
            {"from": "SelectionApis", "to": "MissingLayers", "label": "unsupported root/selection gaps"},
            {"from": "FactoryCommons", "to": "MissingLayers", "label": "unsupported factory/keyword gaps"},
        ],
    }

    inventory = {
        "schemaVersion": 1,
        "sourceSnapshot": {
            "kind": lock["selectedSnapshot"]["kind"],
            "path": lock["selectedSnapshot"]["path"],
            "resolvedLocalSourceRootPath": str(source_base / "DCGO/Assets"),
            "fileManifest": lock["selectedSnapshot"]["fileManifest"],
            "fileManifestSha256": lock["selectedSnapshot"]["fileManifestSha256"],
            "fileCount": lock["selectedSnapshot"]["fileCount"],
            "approvedAt": lock.get("approvedAt"),
        },
        "inputManifests": {
            "fullCardPoolManifest": {
                "path": "docs/generated/full-card-pool-manifest.json",
                "sha256": file_sha256(card_manifest_path),
            }
        },
        "statusPolicy": {
            "ImplementedOrVerifiedRequires": [
                "source usage evidence",
                "RL.Engine symbol/service evidence",
                "test or validation/doc evidence for Verified",
            ],
            "sourceStringAloneMayNotMarkImplemented": True,
            "effectBodiesChanged": False,
        },
        "sourceScope": source_scope,
        "statusCounts": status_counts,
        "timings": timings,
        "features": features,
        "selections": selections,
        "rootZones": root_zones,
        "specialMechanics": special_mechanics,
        "keywords": keywords,
        "factoryApiUsage": [
            {"name": name, "sourceFileUseCount": count}
            for name, count in sorted(factory_api_counter.items(), key=lambda item: (-item[1], item[0]))
        ],
        "commonsApiUsage": [
            {"name": name, "sourceFileUseCount": count}
            for name, count in sorted(commons_api_counter.items(), key=lambda item: (-item[1], item[0]))
        ],
        "effectSetMethodUsage": [
            {"name": name, "sourceFileUseCount": count}
            for name, count in sorted(set_method_counter.items(), key=lambda item: (-item[1], item[0]))
        ],
        "effectSourceRecords": effect_source_records,
        "missingLayerCandidates": missing_items,
        "dependencyGraph": dependency_graph,
    }
    return inventory


def markdown_table(headers: list[str], rows: list[list[Any]]) -> list[str]:
    lines = ["| " + " | ".join(headers) + " |", "| " + " | ".join("---" for _ in headers) + " |"]
    for row in rows:
        lines.append("| " + " | ".join(str(value).replace("\n", " ") for value in row) + " |")
    return lines


def write_matrix(workspace: Path, inventory: dict[str, Any], inventory_hash: str) -> None:
    path = workspace / "docs/generated/full-mechanic-matrix.md"
    lines: list[str] = [
        "# 전체 Mechanic / Effect Inventory Matrix",
        "",
        f"- Source snapshot: `{inventory['sourceSnapshot']['kind']}` / `{inventory['sourceSnapshot']['fileManifest']}`",
        f"- Inventory SHA-256: `{inventory_hash}`",
        f"- CardEffect source files: {inventory['sourceScope']['cardEffectSourceFileCount']}",
        f"- Script source files in scope: {inventory['sourceScope']['scriptSourceFileCount']}",
        f"- Gameplay card records linked from 62 manifest: {inventory['sourceScope']['gameplayCardRecordCount']}",
        "",
        "## Status Policy",
        "",
        "`Implemented`/`Verified` 판정은 원본 사용량만으로 만들지 않는다. 엔진 enum/service/test 또는 검증 문서 근거가 약하면 `PartiallyImplemented`, `Unsupported`, `NeedsSourceReview`로 남긴다.",
        "",
        "## EffectTiming",
        "",
    ]
    timing_rows = [
        [
            item["name"],
            item["mappingStatus"],
            item["sourceScopeFileCount"],
            item["cardEffectSourceFileCount"],
            item["affectedCardCount"],
            item["evidence"]["engineSourceReferenceCount"],
            item["evidence"]["engineTestReferenceCount"],
        ]
        for item in sorted(inventory["timings"], key=lambda item: (-item["sourceScopeFileCount"], item["name"]))
    ]
    lines.extend(markdown_table(["Timing", "Status", "Source files", "CardEffect files", "Affected cards", "Engine refs", "Test/doc refs"], timing_rows))
    lines.extend(["", "## Effect Capability Flags", ""])
    feature_rows = [
        [item["name"], item["mappingStatus"], item["sourceFileCount"], item["cardEffectSourceFileCount"], item["affectedCardCount"], item["evidence"]["engineSourceReferenceCount"], item["evidence"]["engineTestReferenceCount"]]
        for item in sorted(inventory["features"], key=lambda item: (-item["sourceFileCount"], item["name"]))
    ]
    lines.extend(markdown_table(["Capability", "Status", "Source files", "CardEffect files", "Affected cards", "Engine refs", "Test/doc refs"], feature_rows))
    lines.extend(["", "## Selection / Root Zone", ""])
    selection_rows = [
        [item["name"], item["mappingStatus"], item["sourceFileCount"], item["cardEffectSourceFileCount"], item["affectedCardCount"], item["evidence"]["engineSourceReferenceCount"], item["evidence"]["engineTestReferenceCount"]]
        for item in sorted(inventory["selections"], key=lambda item: (-item["sourceFileCount"], item["name"]))
    ]
    lines.extend(markdown_table(["Selection", "Status", "Source files", "CardEffect files", "Affected cards", "Engine refs", "Test/doc refs"], selection_rows))
    lines.extend(["", "## Special Mechanics", ""])
    mechanic_rows = [
        [item["name"], item["mappingStatus"], item["sourceFileCount"], item["cardEffectSourceFileCount"], item["affectedCardCount"], item["engineMechanicEnumPresent"]]
        for item in sorted(inventory["specialMechanics"], key=lambda item: (-item["sourceFileCount"], item["name"]))
    ]
    lines.extend(markdown_table(["Mechanic", "Status", "Source files", "CardEffect files", "Affected cards", "Engine enum"], mechanic_rows))
    lines.extend(["", "## Keywords", ""])
    keyword_rows = [
        [item["name"], item["mappingStatus"], item["sourceFileCount"], item["cardTextKeywordRecordCount"], item["engineKeywordEnumPresent"], item["evidence"]["engineSourceReferenceCount"], item["evidence"]["engineTestReferenceCount"]]
        for item in sorted(inventory["keywords"], key=lambda item: (-item["sourceFileCount"], item["name"]))
    ]
    lines.extend(markdown_table(["Keyword", "Status", "Source files", "Card text records", "Engine enum", "Engine refs", "Test/doc refs"], keyword_rows))
    path.write_text("\n".join(lines) + "\n", encoding="utf-8")


def write_missing_report(workspace: Path, inventory: dict[str, Any], inventory_hash: str) -> None:
    path = workspace / "docs/generated/full-missing-layer-report.md"
    missing = inventory["missingLayerCandidates"]
    top = missing[:80]
    lines: list[str] = [
        "# 전체 Missing Layer Report",
        "",
        f"- Inventory SHA-256: `{inventory_hash}`",
        f"- Missing/partial candidate count: {len(missing)}",
        "",
        "이 문서는 63번 inventory의 보수적 mapping 결과다. `NeedsSourceReview`는 원본 사용은 보이지만 RL.Engine 대응 근거가 약하거나 source 의미를 더 읽어야 하는 항목이다.",
        "",
        "## Top Missing / Partial Candidates",
        "",
    ]
    rows = [
        [
            item["category"],
            item["name"],
            item["mappingStatus"],
            item["sourceFileCount"],
            item["affectedCardCount"],
            item["engineSourceReferenceCount"],
            item["engineTestReferenceCount"],
            ", ".join(item["sampleSourcePaths"][:3]),
        ]
        for item in top
    ]
    lines.extend(markdown_table(["Category", "Name", "Status", "Source files", "Affected cards", "Engine refs", "Test/doc refs", "Sample source"], rows))
    lines.extend(["", "## Status Counts", ""])
    for category, counts in inventory["statusCounts"].items():
        lines.append(f"- {category}: " + ", ".join(f"{key}={value}" for key, value in sorted(counts.items())))
    lines.extend(
        [
            "",
            "## Explicit Notes",
            "",
            "- 이 report는 effect body를 구현하지 않는다.",
            "- source body가 없거나 mapping 근거가 약한 항목은 silent no-op으로 숨기지 않는다.",
            "- ST1~ST3 검증 통과는 전체 카드풀 capability completion의 증거가 아니므로, 전체 카드풀 missing layer는 별도 queue에서 처리해야 한다.",
        ]
    )
    path.write_text("\n".join(lines) + "\n", encoding="utf-8")


def write_dependency_graph(workspace: Path, inventory: dict[str, Any], inventory_hash: str) -> None:
    path = workspace / "docs/generated/full-mechanic-dependency-graph.md"
    graph = inventory["dependencyGraph"]
    lines = [
        "# Mechanic Dependency Graph",
        "",
        f"- Inventory SHA-256: `{inventory_hash}`",
        "",
        "```mermaid",
        "flowchart TD",
    ]
    labels = {node["id"]: node["label"] for node in graph["nodes"]}
    for node in graph["nodes"]:
        lines.append(f'  {node["id"]}["{node["label"]}"]')
    for edge in graph["edges"]:
        lines.append(f'  {edge["from"]} -->|"{edge["label"]}"| {edge["to"]}')
    lines.extend(
        [
            "```",
            "",
            "## Nodes",
            "",
        ]
    )
    lines.extend(markdown_table(["Node", "Label"], [[node_id, label] for node_id, label in sorted(labels.items())]))
    path.write_text("\n".join(lines) + "\n", encoding="utf-8")


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--workspace", default=".")
    args = parser.parse_args()
    workspace = Path(args.workspace).resolve()
    output_dir = workspace / "docs/generated"
    output_dir.mkdir(parents=True, exist_ok=True)

    inventory = build_inventory(workspace)
    inventory_path = output_dir / "full-mechanic-inventory.json"
    write_json(inventory_path, inventory)
    inventory_hash = file_sha256(inventory_path)
    write_matrix(workspace, inventory, inventory_hash)
    write_missing_report(workspace, inventory, inventory_hash)
    write_dependency_graph(workspace, inventory, inventory_hash)
    print(
        json.dumps(
            {
                "inventoryPath": "docs/generated/full-mechanic-inventory.json",
                "inventorySha256": inventory_hash,
                "matrixPath": "docs/generated/full-mechanic-matrix.md",
                "missingLayerReportPath": "docs/generated/full-missing-layer-report.md",
                "dependencyGraphPath": "docs/generated/full-mechanic-dependency-graph.md",
                "sourceScope": inventory["sourceScope"],
                "statusCounts": inventory["statusCounts"],
                "missingLayerCandidates": len(inventory["missingLayerCandidates"]),
            },
            ensure_ascii=False,
            indent=2,
        )
    )


if __name__ == "__main__":
    main()
