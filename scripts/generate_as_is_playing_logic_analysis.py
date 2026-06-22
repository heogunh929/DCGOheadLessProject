#!/usr/bin/env python3
"""Generate AS-IS inventory and playing logic analysis documents.

The script reads the local DCGO Unity source tree and the current headless
engine tree, then writes analysis-only Markdown and JSON artifacts.
"""

from __future__ import annotations

import argparse
import json
import re
from collections import Counter, defaultdict
from dataclasses import dataclass, field
from datetime import datetime, timezone
from pathlib import Path
from typing import Iterable


WORKSPACE_ROOT = Path(__file__).resolve().parents[1]
DEFAULT_FALLBACK_SOURCE = Path("E:/headlessDCGO/DCGO")

SCRIPT_REL = "Assets/Scripts/Script"
CARDEFFECT_REL = "Assets/Scripts/CardEffect"
CARD_ASSET_REL = "Assets/CardBaseEntity"


KEY_PRIORITY: dict[str, str] = {
    "GameContext": "Tier0",
    "Player": "Tier0",
    "CardSource": "Tier0",
    "Permanent": "Tier0",
    "CEntity_Base": "Tier0",
    "CEntity_Effect": "Tier0",
    "CEntity_EffectController": "Tier0",
    "ICardEffect": "Tier0",
    "ActivateICardEffect": "Tier0",
    "EffectTiming": "Tier0",
    "EffectDuration": "Tier0",
    "CardEffectCommons": "Tier0",
    "CardEffectFactory": "Tier0",
    "AutoProcessing": "Tier1",
    "CardController": "Tier1",
    "PlayCardClass": "Tier1",
    "PlayPermanentClass": "Tier1",
    "SelectCardEffect": "Tier1",
    "SelectPermanentEffect": "Tier1",
    "SelectCountEffect": "Tier1",
    "TurnStateMachine": "Tier2",
    "AttackProcess": "Tier2",
    "MultipleSkills": "Tier2",
    "CardObjectController": "Later",
}

HEADLESS_COUNTERPARTS: dict[str, list[str]] = {
    "GameContext": ["Domain/GameState.cs", "Battle/BattleEngineServices.cs"],
    "Player": ["Domain/PlayerState.cs"],
    "CardSource": ["Domain/CardInstance.cs", "Domain/CardDefinition.cs"],
    "Permanent": ["Domain/PermanentState.cs"],
    "CEntity_Base": ["Domain/CardDefinition.cs", "Domain/CardDefinitionIdentity.cs"],
    "CEntity_Effect": ["Effects/EffectDescriptor.cs", "Effects/EffectResolution.cs"],
    "CEntity_EffectController": ["Effects/TriggerCollector.cs", "Effects/StaticEffectService.cs"],
    "ICardEffect": ["Effects/EffectDescriptor.cs", "Effects/EffectResolution.cs"],
    "ActivateICardEffect": ["Effects/EffectDescriptor.cs", "Effects/TriggerPipelineService.cs"],
    "EffectTiming": ["Domain/Enums.cs", "Effects/TriggerCollector.cs"],
    "EffectDuration": ["Effects/DurationCleanupService.cs", "Domain/TemporaryModifier.cs"],
    "CardEffectCommons": [
        "CardEffects/CardEffectCommons.cs",
        "Primitives/Tier1PrimitiveService.cs",
        "Effects/StaticEffectService.cs",
        "Battle/BattleKeywordService.cs",
        "Mechanics/ComplexMechanicService.cs",
    ],
    "CardEffectFactory": ["Effects/EffectDescriptor.cs", "Primitives/Tier1PrimitiveService.cs"],
    "AutoProcessing": [
        "Effects/TriggerCollector.cs",
        "Effects/TriggerPipelineService.cs",
        "Effects/DurationCleanupService.cs",
        "Validation/EngineInvariantChecker.cs",
    ],
    "CardController": ["Battle/PlayCardService.cs", "Battle/DigivolveService.cs"],
    "PlayCardClass": ["Battle/PlayCardService.cs"],
    "PlayPermanentClass": ["Battle/PlayCardService.cs", "Primitives/Tier1PrimitiveService.cs"],
    "TurnStateMachine": ["Battle/LegalActionGenerator.cs", "Setup/GameSetupService.cs"],
    "AttackProcess": ["Battle/AttackService.cs", "Battle/BattleRules.cs"],
    "MultipleSkills": ["Effects/TriggerPipelineService.cs"],
    "SelectCardEffect": ["Decisions/SelectEffectFacades.cs", "Decisions/DecisionPoint.cs", "Decisions/SelectionRequest.cs"],
    "SelectPermanentEffect": ["Decisions/SelectEffectFacades.cs", "Decisions/DecisionPoint.cs", "Decisions/SelectionRequest.cs"],
    "SelectCountEffect": ["Decisions/SelectEffectFacades.cs", "Decisions/DecisionPoint.cs", "Decisions/SelectionRequest.cs"],
    "SelectDigiXrosClass": ["Decisions/SelectionRequest.cs", "Mechanics/ComplexMechanicService.cs"],
    "CheckEffectDisabledClass": ["Effects/StaticRequirementService.cs"],
    "CardObjectController": ["Domain/CardInstanceFactory.cs"],
}

STATIC_API_PREFIXES = [
    "CardEffectCommons",
    "CardEffectFactory",
    "CardSource",
    "Permanent",
    "Player",
    "GameContext",
    "GManager.instance",
    "TurnStateMachine",
    "AutoProcessing",
    "AttackProcess",
    "PlayCardClass",
    "PlayPermanentClass",
    "SelectCardEffect",
    "SelectPermanentEffect",
    "SelectCountEffect",
    "ContinuousController.instance",
    "CardObjectController",
]

FACADE_MEMBER_OWNERS = set(KEY_PRIORITY) | {
    "IBattle",
    "ISecurityCheck",
    "IAddSkillEffect",
    "IAddSecurityEffect",
    "CheckEffectDisabledClass",
    "SelectDigiXrosClass",
}

KEY_FILES = [
    "GameContext.cs",
    "Player.cs",
    "CardSource.cs",
    "Permanent.cs",
    "CEntity_Base.cs",
    "CEntity_Effect.cs",
    "CEntity_EffectController.cs",
    "ICardEffect.cs",
    "TurnStateMachine.cs",
    "AutoProcessing.cs",
    "MultipleSkills.cs",
    "AttackProcess.cs",
    "CardController.cs",
    "CardObjectController.cs",
    "CardEffectCommons.cs",
    "SelectCardEffect.cs",
    "SelectPermanentEffect.cs",
    "SelectCountEffect.cs",
    "SelectDigiXrosClass.cs",
    "CheckEffectDisabledClass.cs",
]

CARDEFFECT_INTERFACE_RULES = [
    {
        "category": "AdditionalSkillGrant",
        "interfaces": {"IAddSkillEffect", "IAddDetailEffect"},
        "headlessMapping": "TemporaryGrantedEffect, EffectDescriptor.TemporaryGrantedEffect, TriggerPipelineService",
        "status": "MappingRequired",
        "forbiddenShortcut": "Unity interface라는 이유로 Exclude하지 않고, CardEffects list를 임의로 직접 변형하지 않는다.",
        "verificationCandidate": "timing별 추가 effect가 deterministic descriptor 또는 temporary granted trigger로 수집되는지 검증한다.",
    },
    {
        "category": "CoroutineOptionResolution",
        "interfaces": {"IOptionResolutionEffect"},
        "headlessMapping": "EffectResolution step, pending SelectionRequest, deterministic resume flow",
        "status": "UnityFlowReplacementRequired",
        "forbiddenShortcut": "IEnumerator/Coroutine을 RL.Engine으로 들여오거나 silent no-op으로 대체하지 않는다.",
        "verificationCandidate": "option resolution이 즉시 step과 pending selection step으로 분리되어 replay 가능한지 검증한다.",
    },
    {
        "category": "KeywordGrant",
        "interfaces": {
            "IBlockerEffect",
            "ICollisionEffect",
            "IIcecladEffect",
            "IPiercingEffect",
            "IRebootEffect",
            "IRushEffect",
            "IScapegoatEffect",
            "IVortexCanAttackPlayersEffect",
        },
        "headlessMapping": "BattleKeywordService, ContinuousEffectDescriptor, TemporaryModifier",
        "status": "PartiallyCovered",
        "forbiddenShortcut": "keyword를 UI label이나 card body-local boolean으로만 처리하지 않는다.",
        "verificationCandidate": "keyword query가 BattleKeywordService를 통해 battle/action legality와 state hash에 반영되는지 검증한다.",
    },
    {
        "category": "RestrictionOrImmunity",
        "prefixes": ("ICanNot", "ICannot", "IImmune"),
        "interfaces": {"IDisableCardEffect", "IDontBattleSecurityDigimonEffect"},
        "headlessMapping": "StaticRequirementService, StaticEffectService, legal-action filtering, target filtering",
        "status": "MappingRequired",
        "forbiddenShortcut": "play/move/select 제한을 카드별 if문으로 흩뜨리거나 무시하지 않는다.",
        "verificationCandidate": "restriction interface가 legal action과 target 후보에서 deterministic하게 제외되는지 검증한다.",
    },
    {
        "category": "SelectionOrTargeting",
        "prefixes": ("ICanSelect",),
        "interfaces": {"ICanSuspendByDigisorptionEffect"},
        "headlessMapping": "SelectionRequest, SelectionValidator, ComplexMechanicService target filters",
        "status": "MappingRequired",
        "forbiddenShortcut": "Unity Select*Effect 호출로 되돌리거나 UI 클릭 가능 여부에 의존하지 않는다.",
        "verificationCandidate": "selection 후보 생성과 validator가 같은 interface 조건을 공유하는지 검증한다.",
    },
    {
        "category": "CostOrRequirement",
        "prefixes": ("IAdd",),
        "interfaces": {
            "IChangeCostEffect",
            "IChangeLinkCostEffect",
            "ICannotIgnoreDigivolutionConditionEffect",
            "IIgnoreColorConditionEffect",
            "ILinkCanDigivolveEffect",
            "ILv2CanDigivolveEffect",
            "IMustZeroCostDigiXrosEffect",
        },
        "headlessMapping": "StaticRequirementService, CostResolver, ComplexMechanicService",
        "status": "MappingRequired",
        "forbiddenShortcut": "cost/requirement 변경을 play service 내부의 임시 예외로 숨기지 않는다.",
        "verificationCandidate": "play/digivolve/link/digixros requirement 계산이 interface mapping을 통해 일관되게 바뀌는지 검증한다.",
    },
    {
        "category": "StatOrMetadataModifier",
        "prefixes": ("IChange",),
        "interfaces": {"IDontHaveDPEffect", "IInvertSAttackEffect"},
        "headlessMapping": "StaticEffectService, ContinuousEffectDescriptor, TemporaryModifier, CardDefinition runtime view",
        "status": "PartiallyCovered",
        "forbiddenShortcut": "CardDefinition 원본 metadata를 직접 수정하거나 modifier duration을 잃어버리지 않는다.",
        "verificationCandidate": "DP/level/name/trait/security attack 변경이 effective view와 cleanup 규칙에만 반영되는지 검증한다.",
    },
]


@dataclass
class SourceFileInfo:
    path: str
    classes: list[str] = field(default_factory=list)
    interfaces: list[str] = field(default_factory=list)
    enums: list[str] = field(default_factory=list)
    methods: list[str] = field(default_factory=list)
    members: list[str] = field(default_factory=list)
    dependencies: list[str] = field(default_factory=list)
    category: str = "Helper"
    cardeffect_referenced: bool = False
    headless_needed: str = "검토 필요"
    priority: str = "Later"
    current_headless_counterpart: list[str] = field(default_factory=list)
    headless_status: str = "Ambiguous"
    gap: str = ""


def read_text(path: Path) -> str:
    for encoding in ("utf-8-sig", "utf-8", "cp932", "cp949"):
        try:
            return path.read_text(encoding=encoding)
        except UnicodeDecodeError:
            continue
    return path.read_text(encoding="utf-8", errors="replace")


def write_text(path: Path, text: str) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(text, encoding="utf-8", newline="\n")


def rel_to_source(path: Path, source_root: Path) -> str:
    return "DCGO/" + path.relative_to(source_root).as_posix()


def rel_to_workspace(path: Path) -> str:
    return path.relative_to(WORKSPACE_ROOT).as_posix()


def list_files(root: Path, pattern: str = "*") -> list[Path]:
    if not root.exists():
        return []
    return sorted(p for p in root.rglob(pattern) if p.is_file())


def strip_comments(text: str) -> str:
    text = re.sub(r"/\*.*?\*/", " ", text, flags=re.S)
    text = re.sub(r"//.*", " ", text)
    return text


def parse_csharp_file(path: Path, source_root: Path) -> SourceFileInfo:
    text = read_text(path)
    no_comments = strip_comments(text)
    rel = rel_to_source(path, source_root)

    classes = re.findall(r"\b(?:public|protected|private|internal)?\s*(?:static\s+)?(?:partial\s+)?class\s+([A-Za-z_][A-Za-z0-9_]*)", no_comments)
    interfaces = re.findall(r"\b(?:public|protected|private|internal)?\s*(?:partial\s+)?interface\s+([A-Za-z_][A-Za-z0-9_]*)", no_comments)
    enums = re.findall(r"\b(?:public|protected|private|internal)?\s*enum\s+([A-Za-z_][A-Za-z0-9_]*)", no_comments)

    method_pattern = re.compile(
        r"^\s*(?:public|protected|protected\s+internal)\s+"
        r"(?:static\s+|virtual\s+|override\s+|abstract\s+|async\s+|sealed\s+|new\s+)*"
        r"(?:[A-Za-z_][A-Za-z0-9_<>,\[\]\.?]+\s+)+"
        r"([A-Za-z_][A-Za-z0-9_]*)\s*\(",
        re.M,
    )
    methods = sorted(set(method_pattern.findall(no_comments)))

    ctor_pattern = re.compile(
        r"^\s*(?:public|protected|protected\s+internal)\s+("
        + "|".join(re.escape(c) for c in classes)
        + r")\s*\(",
        re.M,
    ) if classes else None
    if ctor_pattern:
        methods.extend(m for m in ctor_pattern.findall(no_comments) if m not in methods)
        methods = sorted(set(methods))

    member_pattern = re.compile(
        r"^\s*(?:public|protected|protected\s+internal)\s+"
        r"(?:static\s+|readonly\s+|const\s+|virtual\s+|override\s+|new\s+)*"
        r"[A-Za-z_][A-Za-z0-9_<>,\[\]\.?]+\s+"
        r"([A-Za-z_][A-Za-z0-9_]*)\s*(?:[=;{])",
        re.M,
    )
    members = sorted(set(m for m in member_pattern.findall(no_comments) if m not in methods))

    dependencies = []
    dep_checks = {
        "UnityEngine": r"\bUnityEngine\b|\bMonoBehaviour\b|\bGameObject\b|\bSerializeField\b",
        "Photon": r"\bPhoton\b|\bPhotonNetwork\b|\bMonoBehaviourPunCallbacks\b",
        "Unity UI": r"\bUnityEngine\.UI\b|\bTextMeshPro\b|\bButton\b|\bImage\b|\bCanvas\b|\bRectTransform\b",
        "Coroutine/IEnumerator": r"\bIEnumerator\b|\bStartCoroutine\b|\byield\s+return\b",
    }
    for name, pattern in dep_checks.items():
        if re.search(pattern, text):
            dependencies.append(name)

    return SourceFileInfo(
        path=rel,
        classes=sorted(set(classes)),
        interfaces=sorted(set(interfaces)),
        enums=sorted(set(enums)),
        methods=methods,
        members=members,
        dependencies=dependencies,
    )


def categorize_file(info: SourceFileInfo) -> None:
    names = set(info.classes + info.interfaces + info.enums)
    path = info.path
    basename = Path(path).name

    if "CardEffectFactory/" in path or "CardEffectCommons/" in path or "CardEffectCommons.cs" in path:
        info.category = "공통 카드 효과 API"
    elif "MainPhaseAction/" in path or names & {"PlayCardClass", "PlayPermanentClass"}:
        info.category = "실제 플레이 로직"
    elif names & {
        "GameContext",
        "Player",
        "CardSource",
        "Permanent",
        "TurnStateMachine",
        "AutoProcessing",
        "AttackProcess",
        "MultipleSkills",
        "CEntity_EffectController",
    }:
        info.category = "실제 플레이 로직"
    elif names & {"CEntity_Base", "CEntity_Effect"}:
        info.category = "카드 데이터/효과 바인딩"
    elif basename.startswith("Select") or "Panel" in basename or "Unity UI" in info.dependencies:
        info.category = "선택/UI/연출"
    elif "Editor" in path:
        info.category = "Editor/데이터 정리"
    elif "UnityEngine" in info.dependencies or "Photon" in info.dependencies:
        info.category = "UnityAdapter 후보"
    else:
        info.category = "Helper/유틸리티"

    priority = "Later"
    for name in names:
        if name in KEY_PRIORITY:
            priority = min_priority(priority, KEY_PRIORITY[name])
    if "CardEffectCommons/" in path or "CardEffectFactory/" in path:
        priority = min_priority(priority, "Tier0")
    if "MainPhaseAction/" in path:
        priority = min_priority(priority, "Tier2")
    if info.category in {"선택/UI/연출", "UnityAdapter 후보"} and priority == "Later":
        priority = "Exclude"

    info.priority = priority
    info.headless_needed = "필요" if priority in {"Tier0", "Tier1", "Tier2"} else ("부분 필요" if info.cardeffect_referenced else "불필요/후순위")


def min_priority(left: str, right: str) -> str:
    order = {"Tier0": 0, "Tier1": 1, "Tier2": 2, "Later": 3, "Exclude": 4}
    return left if order.get(left, 99) <= order.get(right, 99) else right


def scan_headless() -> tuple[set[str], set[str], dict[str, list[str]]]:
    engine_root = WORKSPACE_ROOT / "src/DCGO.RL.Engine"
    files = list_files(engine_root, "*.cs")
    rel_files = {rel_to_workspace(p).replace("src/DCGO.RL.Engine/", "") for p in files}
    classes: set[str] = set()
    class_to_files: dict[str, list[str]] = defaultdict(list)
    for path in files:
        text = strip_comments(read_text(path))
        rel = rel_to_workspace(path).replace("src/DCGO.RL.Engine/", "")
        for match in re.findall(r"\b(?:public|internal)?\s*(?:sealed\s+|static\s+|partial\s+)?(?:class|interface|enum|struct|record)\s+([A-Za-z_][A-Za-z0-9_]*)", text):
            classes.add(match)
            class_to_files[match].append(rel)
    return rel_files, classes, class_to_files


def classify_headless_status(info: SourceFileInfo, headless_files: set[str], headless_classes: set[str]) -> None:
    names = info.classes + info.interfaces + info.enums
    counterparts: list[str] = []
    for name in names:
        counterparts.extend(HEADLESS_COUNTERPARTS.get(name, []))
        if name in headless_classes:
            counterparts.append(f"(same type) {name}")
    counterparts = sorted(set(counterparts))
    info.current_headless_counterpart = counterparts

    if any(c.startswith("(same type)") for c in counterparts):
        info.headless_status = "Exists"
    elif counterparts and all(c in headless_files for c in counterparts if not c.startswith("(")):
        info.headless_status = "Partial"
    elif counterparts and any(c in headless_files for c in counterparts if not c.startswith("(")):
        info.headless_status = "Partial"
    elif info.category in {"선택/UI/연출", "UnityAdapter 후보"}:
        info.headless_status = "UnityOnly"
    elif info.priority == "Exclude":
        info.headless_status = "Exclude"
    elif info.priority in {"Tier0", "Tier1", "Tier2"}:
        info.headless_status = "Missing"
    else:
        info.headless_status = "Ambiguous"

    if info.headless_status == "Exists":
        info.gap = "동명 타입이 존재하므로 책임 분해 확인 필요"
    elif info.headless_status == "Partial":
        info.gap = "일부 책임은 headless에 있으나 Unity 원본 API와 1:1 facade는 아님"
    elif info.headless_status == "Missing":
        info.gap = "CardEffect-facing 계약 또는 핵심 진행 책임 대응 필요"
    elif info.headless_status == "UnityOnly":
        info.gap = "RL.Engine에 직접 이식하지 않고 UnityAdapter/CLI 선택 facade로 분리 필요"
    else:
        info.gap = "후속 분류 필요"


def scan_cardeffect_references(
    cardeffect_files: list[Path],
    source_root: Path,
    infos: list[SourceFileInfo],
) -> tuple[dict[str, dict], dict[str, set[str]]]:
    type_names: dict[str, str] = {}
    member_to_owners: dict[str, set[str]] = defaultdict(set)
    for info in infos:
        for name in info.classes + info.interfaces + info.enums:
            type_names[name] = info.path
            for member in info.methods + info.members:
                if not is_noisy_member(member):
                    member_to_owners[member].add(name)

    type_ref_files: dict[str, set[str]] = defaultdict(set)
    api_counts: Counter[str] = Counter()
    api_files: dict[str, set[str]] = defaultdict(set)
    api_examples: dict[str, list[str]] = defaultdict(list)

    static_patterns = [
        (prefix, re.compile(r"\b" + re.escape(prefix) + r"\s*\.\s*([A-Za-z_][A-Za-z0-9_]*)"))
        for prefix in STATIC_API_PREFIXES
    ]
    enum_pattern = re.compile(r"\b(EffectTiming|EffectDuration)\s*\.\s*([A-Za-z_][A-Za-z0-9_]*)")
    member_access_pattern = re.compile(r"\.\s*([A-Za-z_][A-Za-z0-9_]*)\b")

    for path in cardeffect_files:
        text = strip_comments(read_text(path))
        rel = rel_to_source(path, source_root)
        unique_words = set(re.findall(r"\b[A-Za-z_][A-Za-z0-9_]*\b", text))
        for name in unique_words & set(type_names):
            type_ref_files[name].add(rel)

        for prefix, pattern in static_patterns:
            for member in pattern.findall(text):
                api = f"{prefix}.{member}"
                api_counts[api] += 1
                api_files[api].add(rel)
                add_example(api_examples, api, rel)

        for enum_name, member in enum_pattern.findall(text):
            api = f"{enum_name}.{member}"
            api_counts[api] += 1
            api_files[api].add(rel)
            add_example(api_examples, api, rel)

        for member in member_access_pattern.findall(text):
            owners = member_to_owners.get(member)
            if not owners:
                continue
            owners = owners & FACADE_MEMBER_OWNERS
            if not owners:
                continue
            owner = choose_owner(owners)
            api = f"{owner}.{member}"
            api_counts[api] += 1
            api_files[api].add(rel)
            add_example(api_examples, api, rel)

    records = {}
    for api, ref_count in api_counts.items():
        records[api] = {
            "api": api,
            "refCount": ref_count,
            "cardEffectFileCount": len(api_files[api]),
            "exampleFiles": api_examples[api],
            "headlessStatus": api_headless_status(api),
            "priority": api_priority(api),
        }
    return records, type_ref_files


def is_noisy_member(member: str) -> bool:
    return member in {
        "ToString",
        "Equals",
        "GetHashCode",
        "Count",
        "Length",
        "Add",
        "Remove",
        "Clear",
        "Contains",
        "Where",
        "Select",
        "instance",
        "Mode",
        "Root",
        "SetUp",
        "SetUpCustomMessage",
        "Activate",
        "GetComponent",
        "gameObject",
        "transform",
        "name",
        "enabled",
        "value",
        "text",
    }


def choose_owner(owners: set[str]) -> str:
    preferred = [
        "ICardEffect",
        "ActivateICardEffect",
        "CardSource",
        "Permanent",
        "Player",
        "GameContext",
        "CEntity_Base",
        "CEntity_EffectController",
        "CardEffectCommons",
        "CardEffectFactory",
    ]
    for item in preferred:
        if item in owners:
            return item
    return sorted(owners)[0]


def add_example(examples: dict[str, list[str]], api: str, rel: str) -> None:
    if len(examples[api]) < 5 and rel not in examples[api]:
        examples[api].append(rel)


def api_headless_status(api: str) -> str:
    owner = api.split(".", 1)[0]
    if owner in {"EffectTiming", "EffectDuration"}:
        return "Partial"
    if owner in HEADLESS_COUNTERPARTS:
        return "Partial"
    if owner in {"GManager", "ContinuousController"} or api.startswith("GManager.instance") or api.startswith("ContinuousController.instance"):
        return "UnityOnly"
    return "Ambiguous"


def api_priority(api: str) -> str:
    if api.startswith(("CardEffectCommons.", "CardEffectFactory.", "ICardEffect.", "ActivateICardEffect.", "CardSource.", "Permanent.", "Player.", "EffectTiming.")):
        return "Tier0"
    if api.startswith(("SelectCardEffect.", "SelectPermanentEffect.", "SelectCountEffect.", "PlayCardClass.", "PlayPermanentClass.")):
        return "Tier1"
    if api.startswith(("AttackProcess.", "TurnStateMachine.", "AutoProcessing.")):
        return "Tier2"
    if api.startswith(("GManager.instance.", "ContinuousController.instance.")):
        return "Tier1"
    return "Later"


def build_assets_tree(source_root: Path) -> list[dict]:
    assets_root = source_root / "Assets"
    rows: list[dict] = []
    if not assets_root.exists():
        return rows
    for path in sorted([assets_root, *[p for p in assets_root.iterdir() if p.is_dir()]]):
        rel = rel_to_source(path, source_root)
        file_count = sum(1 for p in path.rglob("*") if p.is_file()) if path.is_dir() else 1
        category, rl_needed, adapter_needed, description = categorize_asset_path(path, source_root)
        rows.append(
            {
                "path": rel,
                "category": category,
                "rlEngineNeeded": rl_needed,
                "unityAdapterNeeded": adapter_needed,
                "fileCount": file_count,
                "description": description,
            }
        )

    important_subdirs = [
        source_root / SCRIPT_REL,
        source_root / CARDEFFECT_REL,
        source_root / CARD_ASSET_REL,
        source_root / "Assets/Scripts/Script/CardEffectCommons",
        source_root / "Assets/Scripts/Script/CardEffectFactory",
        source_root / "Assets/Scripts/Script/MainPhaseAction",
    ]
    seen = {row["path"] for row in rows}
    for path in important_subdirs:
        if path.exists():
            rel = rel_to_source(path, source_root)
            if rel in seen:
                continue
            file_count = sum(1 for p in path.rglob("*") if p.is_file())
            category, rl_needed, adapter_needed, description = categorize_asset_path(path, source_root)
            rows.append(
                {
                    "path": rel,
                    "category": category,
                    "rlEngineNeeded": rl_needed,
                    "unityAdapterNeeded": adapter_needed,
                    "fileCount": file_count,
                    "description": description,
                }
            )
    return rows


def categorize_asset_path(path: Path, source_root: Path) -> tuple[str, str, str, str]:
    rel = rel_to_source(path, source_root)
    if rel.endswith("Assets"):
        return ("전체 Assets 루트", "간접 필요", "필요", "원본 Unity 자산 전체. headless에는 선별 이식만 허용한다.")
    if "/Scripts/Script" in rel:
        return ("실제 플레이 로직 C# 코드", "필요", "부분 필요", "게임 상태, 진행, 선택, 효과 실행 기반이 섞인 핵심 원본 코드")
    if "/Scripts/CardEffect" in rel:
        return ("카드별 효과 코드", "나중에 필요", "부분 필요", "카드 단위 효과 구현 원본. 이번 작업에서는 body 구현 대상이 아니다.")
    if "/CardBaseEntity" in rel:
        return ("카드 데이터 asset", "필요", "필요", "카드 정의 ScriptableObject asset. CardId, 이름, 비용, 색, 효과 클래스 연결 근거")
    if any(token in rel for token in ["/Image", "/Sound", "/Animation", "/Prefab", "/Materials", "/Shader", "/RenderTexture", "/3DObjects", "/Scenes", "/SCI-FI UI Components"]):
        return ("이미지/연출/UI/audio/prefab", "불필요", "필요", "그래픽/연출/UI 자산. RL.Engine에는 직접 넣지 않는다.")
    if "/Editor" in rel or "/ScriptTemplates" in rel or "/Fix" in rel:
        return ("Editor/데이터 정리", "불필요", "검토", "개발/정리용 스크립트 성격. 런타임 포팅 대상이 아니다.")
    if any(token in rel for token in ["/Photon", "/Plugins", "/TextMesh Pro", "/Settings", "/Resources", "/AddOn", "/Effect"]):
        return ("UnityAdapter/Unity runtime 의존", "불필요", "필요 가능", "Unity 런타임, 네트워크, 플러그인 또는 표시 관련 자산")
    return ("기타", "검토 필요", "검토 필요", "세부 파일 단위 분류 필요")


def build_playing_logic_rows() -> list[dict]:
    return [
        {
            "logicArea": "게임 상태",
            "originalFiles": [
                "DCGO/Assets/Scripts/Script/GameContext.cs",
                "DCGO/Assets/Scripts/Script/Player.cs",
                "DCGO/Assets/Scripts/Script/CardSource.cs",
                "DCGO/Assets/Scripts/Script/Permanent.cs",
                "DCGO/Assets/Scripts/Script/CEntity_Base.cs",
            ],
            "mainClasses": ["GameContext", "Player", "CardSource", "Permanent", "CEntity_Base"],
            "currentHeadlessCounterpart": ["GameState", "PlayerState", "CardInstance", "PermanentState", "CardDefinition"],
            "gap": "기본 state model은 있으나 Unity 원본의 CardEffect-facing convenience API와 zone/owner 관계 facade가 완전히 1:1은 아니다.",
            "priority": "Tier0",
        },
        {
            "logicArea": "게임 진행 루프",
            "originalFiles": [
                "DCGO/Assets/Scripts/Script/TurnStateMachine.cs",
                "DCGO/Assets/Scripts/Script/MainPhaseAction/**/*.cs",
            ],
            "mainClasses": ["TurnStateMachine", "MainPhaseAction", "PlayCardClass", "PlayPermanentClass"],
            "currentHeadlessCounterpart": ["GameSetupService", "LegalActionGenerator", "PlayCardService", "DigivolveService"],
            "gap": "main phase action과 phase coroutine 흐름이 headless action/decision 흐름으로 분해되어 있어 source parity 문서와 facade contract가 필요하다.",
            "priority": "Tier2",
        },
        {
            "logicArea": "카드 플레이/진화/옵션 사용",
            "originalFiles": [
                "DCGO/Assets/Scripts/Script/CardController.cs",
                "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
                "DCGO/Assets/Scripts/Script/MainPhaseAction/**/*.cs",
            ],
            "mainClasses": ["CardController", "PlayCardClass", "PlayPermanentClass", "CardEffectCommons"],
            "currentHeadlessCounterpart": ["PlayCardService", "DigivolveService", "CostResolver", "Tier1PrimitiveService"],
            "gap": "pay cost, BeforePayCost/AfterPayCost, option lifecycle, security option, enter-field trigger 연결이 여러 service로 분산되어 있다.",
            "priority": "Tier1",
        },
        {
            "logicArea": "자동 처리/트리거/룰 처리",
            "originalFiles": [
                "DCGO/Assets/Scripts/Script/AutoProcessing.cs",
                "DCGO/Assets/Scripts/Script/MultipleSkills.cs",
                "DCGO/Assets/Scripts/Script/ICardEffect.cs",
                "DCGO/Assets/Scripts/Script/CEntity_EffectController.cs",
                "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
            ],
            "mainClasses": ["AutoProcessing", "MultipleSkills", "ICardEffect", "ActivateICardEffect", "CEntity_EffectController"],
            "currentHeadlessCounterpart": ["TriggerCollector", "TriggerPipelineService", "EffectDescriptor", "StaticEffectService", "DurationCleanupService"],
            "gap": "StackSkillInfos/GetSkillInfos/ActivateEffectProcess의 수집 범위와 순서가 headless trigger pipeline에서 명시 검증되어야 한다.",
            "priority": "Tier0",
        },
        {
            "logicArea": "공격/블록/카운터/배틀/시큐리티 체크",
            "originalFiles": [
                "DCGO/Assets/Scripts/Script/AttackProcess.cs",
                "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
            ],
            "mainClasses": ["AttackProcess", "IBattle", "ISecurityCheck"],
            "currentHeadlessCounterpart": ["AttackService", "BattleRules", "BattleKeywordService", "AttackRuntimeContext"],
            "gap": "공격 상태 머신은 존재하나 Counter/Block timing, security digimon, attack cleanup source parity를 더 촘촘히 묶어야 한다.",
            "priority": "Tier2",
        },
        {
            "logicArea": "카드 효과 바인딩",
            "originalFiles": [
                "DCGO/Assets/Scripts/Script/CEntity_Base.cs",
                "DCGO/Assets/Scripts/Script/CEntity_EffectController.cs",
                "DCGO/Assets/Scripts/Script/CEntity_Effect.cs",
                "DCGO/Assets/Scripts/CardEffect/**/*.cs",
            ],
            "mainClasses": ["CEntity_Base", "CEntity_EffectController", "CEntity_Effect"],
            "currentHeadlessCounterpart": ["CardDefinition", "EffectDescriptor", "TriggerCollector", "StaticEffectService"],
            "gap": "CardEffectClassName -> namespace/type lookup 규칙과 fallback 실패 조건을 headless registry 계약으로 고정해야 한다.",
            "priority": "Tier0",
        },
    ]


def build_foundation_work_items(infos: list[SourceFileInfo]) -> list[dict]:
    status_by_name: dict[str, str] = {}
    for info in infos:
        for name in info.classes + info.interfaces + info.enums:
            status_by_name[name] = info.headless_status

    def status(*names: str) -> str:
        values = [status_by_name.get(name, "Missing") for name in names]
        if any(v == "Exists" for v in values):
            return "Exists"
        if any(v == "Partial" for v in values):
            return "Partial"
        if any(v == "UnityOnly" for v in values):
            return "UnityOnly"
        return values[0] if values else "Missing"

    items = [
        ("ICardEffect / ActivateICardEffect 대응 모델", "Tier0", "ICardEffect.cs, CEntity_Effect.cs", "필수", status("ICardEffect", "ActivateICardEffect"), "M", "-", "EffectDescriptor/Trigger 계약이 원본 timing/source/use-count 의미를 문서와 테스트로 고정"),
        ("EffectTiming / EffectDuration 대응", "Tier0", "ICardEffect.cs, CardEffectCommons.cs", "필수", status("EffectTiming", "EffectDuration"), "S", "-", "원본 timing enum과 headless enum/alias 차이가 명시됨"),
        ("CardSource / card instance facade", "Tier0", "CardSource.cs", "필수", status("CardSource"), "L", "CardDefinition", "CardEffect가 쓰는 Owner, PermanentOfThisCard, EffectList 계열 API 대응"),
        ("Permanent / field permanent facade", "Tier0", "Permanent.cs", "필수", status("Permanent"), "L", "ZoneMover", "TopCard, DP, stack, digivolution cards, battle/breeding 존재 판정 대응"),
        ("Player zone/state facade", "Tier0", "Player.cs", "필수", status("Player"), "M", "CardSource/Permanent", "hand/security/trash/field/effect list 수집 범위가 deterministic state로 대응"),
        ("GameContext turn/player/memory facade", "Tier0", "GameContext.cs", "필수", status("GameContext"), "M", "PlayerState", "TurnPlayer/NonTurnPlayer/Memory/phase 조회가 headless service에서 통일"),
        ("CEntity_Base card definition 대응", "Tier0", "CEntity_Base.cs", "필수", status("CEntity_Base"), "M", "asset registry", "CardEffectClassName, colors, level, cost, traits 등 CardEffect-facing metadata 고정"),
        ("CEntity_EffectController binding registry", "Tier0", "CEntity_EffectController.cs", "필수", status("CEntity_EffectController"), "M", "EffectDescriptor", "ClassName lookup/fallback/added effect collection 규칙이 실패 명시 방식으로 대응"),
        ("CardEffectCommons facade inventory", "Tier0", "CardEffectCommons.cs, CardEffectCommons/**/*.cs", "필수", status("CardEffectCommons"), "XL", "state facade", "상위 참조 helper부터 primitive/service 대응 표와 unsupported 명시"),
        ("CardEffectFactory primitive descriptor", "Tier0", "CardEffectFactory/**/*.cs", "필수", status("CardEffectFactory"), "L", "EffectDescriptor", "factory helper가 deterministic EffectDescriptor/primitive로 대응"),
        ("AutoProcessing trigger collection", "Tier1", "AutoProcessing.cs", "필수", status("AutoProcessing"), "L", "ICardEffect model", "StackSkillInfos/GetSkillInfos/TriggeredSkillProcess 순서와 수집 범위 재현"),
        ("selection request/result facade", "Tier1", "SelectCardEffect.cs, SelectPermanentEffect.cs, SelectCountEffect.cs", "필수", status("SelectCardEffect", "SelectPermanentEffect", "SelectCountEffect"), "M", "DecisionPoint", "Unity UI 선택이 RL/CLI SelectionRequest로 분리"),
        ("zone movement primitive coverage", "Tier1", "CardEffectCommons.cs, Player.cs, Permanent.cs", "필수", "Partial", "L", "ZoneMover", "trash/security/hand/deck/field 이동이 공통 primitive만 사용"),
        ("play/digivolve/option lifecycle", "Tier1", "CardController.cs, MainPhaseAction/**/*.cs", "필수", status("CardController", "PlayCardClass", "PlayPermanentClass"), "XL", "CostResolver", "BeforePayCost/AfterPayCost/OnEnterField/security option 흐름 대응"),
        ("security skill execution", "Tier1", "AutoProcessing.cs, AttackProcess.cs", "필수", "Partial", "M", "TriggerPipeline", "face-up security, security option, security digimon 처리 순서 대응"),
        ("AttackProcess state machine", "Tier2", "AttackProcess.cs", "필수", status("AttackProcess"), "L", "BattleRules", "Counter/Block/Battle/End/Cleanup 상태와 trigger timing 대응"),
        ("MultipleSkills nested resolution", "Tier2", "MultipleSkills.cs", "필수", status("MultipleSkills"), "M", "TriggerPipeline", "중첩 효과 처리와 IsUsing stack semantics 대응"),
        ("rule process / DP 0 deletion", "Tier2", "AutoProcessing.cs", "필수", "Partial", "M", "StaticEffectService", "룰 처리와 상태 기반 삭제/공격 종료 조건 재현"),
        ("MainPhaseAction legal action flow", "Tier2", "MainPhaseAction/**/*.cs", "필수", "Partial", "L", "LegalActionGenerator", "원본 클릭/action 후보와 headless LegalAction 차이 문서화"),
        ("Unity visual/network separation", "Later", "CardObjectController.cs, GManager, UI panels", "간접", "UnityOnly", "M", "-", "RL.Engine 금지 의존성은 UnityAdapter 또는 CLI rendering으로 격리"),
    ]
    return [
        {
            "workItem": name,
            "tier": tier,
            "originalSource": original,
            "neededForCardEffect": needed,
            "currentStatus": current_status,
            "estimatedSize": size,
            "blockedBy": blocked_by,
            "doneCondition": done_condition,
        }
        for name, tier, original, needed, current_status, size, blocked_by, done_condition in items
    ]


def sort_infos(infos: Iterable[SourceFileInfo]) -> list[SourceFileInfo]:
    priority_order = {"Tier0": 0, "Tier1": 1, "Tier2": 2, "Later": 3, "Exclude": 4}
    return sorted(infos, key=lambda i: (priority_order.get(i.priority, 9), i.path))


def md_table(headers: list[str], rows: list[list[object]]) -> str:
    def cell(value: object) -> str:
        if isinstance(value, list):
            value = "<br>".join(str(v) for v in value)
        text = str(value).replace("\n", "<br>").replace("|", "\\|")
        return text

    out = ["| " + " | ".join(headers) + " |"]
    out.append("| " + " | ".join("---" for _ in headers) + " |")
    for row in rows:
        out.append("| " + " | ".join(cell(v) for v in row) + " |")
    return "\n".join(out)


def classify_cardeffect_interface(name: str) -> dict:
    for rule in CARDEFFECT_INTERFACE_RULES:
        if name in rule.get("interfaces", set()):
            return rule
        if any(name.startswith(prefix) for prefix in rule.get("prefixes", ())):
            return rule
    return {
        "category": "ReviewRequired",
        "headlessMapping": "EffectDescriptor, primitive, or service mapping must be reviewed before card body porting",
        "status": "ReviewRequired",
        "forbiddenShortcut": "Exclude로 확정하거나 silent no-op 처리하지 않는다.",
        "verificationCandidate": "원본 사용처를 확인해 descriptor/service/primitive 중 하나로 분류되는지 검증한다.",
    }


def build_cardeffect_interface_mappings(source_root: Path) -> list[dict]:
    path = source_root / SCRIPT_REL / "CardEffectInterfaces.cs"
    text = strip_comments(read_text(path))
    interface_pattern = re.compile(
        r"\bpublic\s+interface\s+([A-Za-z_][A-Za-z0-9_]*)\s*\{(?P<body>.*?)\}",
        re.S,
    )
    method_pattern = re.compile(r"\b([A-Za-z_][A-Za-z0-9_]*)\s*\(")
    rows = []
    for match in interface_pattern.finditer(text):
        name = match.group(1)
        body = match.group("body")
        methods = sorted(set(method_pattern.findall(body)))
        rule = classify_cardeffect_interface(name)
        rows.append(
            {
                "interface": name,
                "originalSource": "DCGO/Assets/Scripts/Script/CardEffectInterfaces.cs",
                "methodNames": methods,
                "category": rule["category"],
                "headlessMapping": rule["headlessMapping"],
                "status": rule["status"],
                "forbiddenShortcut": rule["forbiddenShortcut"],
                "verificationCandidate": rule["verificationCandidate"],
            }
        )
    return sorted(rows, key=lambda row: (row["category"], row["interface"]))


def render_cardeffect_interface_mapping(rows: list[dict], generated_at: str, source_root: Path) -> str:
    category_summary = Counter(row["category"] for row in rows)
    status_summary = Counter(row["status"] for row in rows)
    return "\n\n".join(
        [
            "# CardEffect Interface Mapping",
            f"- 생성 시각: `{generated_at}`",
            f"- 원본 경로: `{source_root / SCRIPT_REL / 'CardEffectInterfaces.cs'}`",
            "- 목적: `CardEffectInterfaces.cs`를 Exclude로 단정하지 않고, headless descriptor/service/primitive 계약으로 옮겨야 할 범주를 고정한다.",
            "- 범위: interface 분류와 검증 후보만 다루며, 개별 카드 effect body는 구현하지 않는다.",
            "## Category Summary",
            md_table(
                ["Category", "Count"],
                [[category, count] for category, count in sorted(category_summary.items())],
            ),
            "## Status Summary",
            md_table(
                ["Status", "Count"],
                [[status, count] for status, count in sorted(status_summary.items())],
            ),
            "## Interface Mapping",
            md_table(
                [
                    "Interface",
                    "Methods",
                    "Category",
                    "Headless mapping",
                    "Status",
                    "Forbidden shortcut",
                    "Verification candidate",
                ],
                [
                    [
                        row["interface"],
                        row["methodNames"] or "-",
                        row["category"],
                        row["headlessMapping"],
                        row["status"],
                        row["forbiddenShortcut"],
                        row["verificationCandidate"],
                    ]
                    for row in rows
                ],
            ),
            "## Porting Guard",
            "- `IAddSkillEffect`는 `TemporaryGrantedEffect` 또는 descriptor 기반 추가 trigger로 매핑해야 하며, `CardEffects` list를 카드별 임시 규칙으로 직접 조작하지 않는다.",
            "- keyword interface는 `BattleKeywordService` 또는 continuous/temporary modifier 계층으로 연결한다.",
            "- restriction/requirement/selection interface는 legal action, cost resolver, selection validator에서 같은 규칙을 공유해야 한다.",
            "- `IOptionResolutionEffect`의 coroutine 흐름은 `EffectResolution` step과 pending `SelectionRequest` resume 흐름으로 대체해야 한다.",
        ]
    ) + "\n"


def render_assets_tree(rows: list[dict], generated_at: str, source_root: Path) -> str:
    return "\n\n".join(
        [
            "# DCGO Assets AS-IS Tree",
            f"- 생성 시각: `{generated_at}`",
            f"- 원본 경로: `{source_root}`",
            "- 목적: RL.Engine 포팅 전 Assets 하위 영역을 headless 필요도와 UnityAdapter 필요도로 1차 분류한다.",
            md_table(
                ["Path", "Category", "RL.Engine 필요 여부", "UnityAdapter 필요 여부", "설명"],
                [
                    [
                        row["path"],
                        row["category"],
                        f"{row['rlEngineNeeded']} ({row['fileCount']} files)",
                        row["unityAdapterNeeded"],
                        row["description"],
                    ]
                    for row in rows
                ],
            ),
            "## 요약",
            "- `Assets/Scripts/Script`는 실제 플레이 로직과 Unity UI/Coroutine 의존이 섞인 핵심 분석 대상이다.",
            "- `Assets/Scripts/CardEffect`는 카드별 효과 구현 원본이지만 이번 작업에서는 body 구현 대상이 아니다.",
            "- `Assets/CardBaseEntity`는 CardEffectClassName과 카드 메타데이터 연결을 위해 RL.Engine에 필요한 데이터 근거다.",
            "- 이미지, 사운드, 프리팹, Photon, UI 플러그인은 RL.Engine 직접 포팅 대상이 아니며 UnityAdapter 경계로 분리해야 한다.",
        ]
    ) + "\n"


def render_script_inventory(infos: list[SourceFileInfo], generated_at: str, source_root: Path) -> str:
    sorted_items = sort_infos(infos)
    summary = Counter(info.priority for info in infos)
    category_summary = Counter(info.category for info in infos)
    rows = []
    for info in sorted_items:
        rows.append(
            [
                info.path,
                ", ".join(info.classes + info.interfaces + info.enums) or "-",
                ", ".join(info.methods[:18]) + (" ..." if len(info.methods) > 18 else ""),
                ", ".join(info.members[:18]) + (" ..." if len(info.members) > 18 else ""),
                ", ".join(info.dependencies) or "-",
                info.category,
                "Yes" if info.cardeffect_referenced else "No",
                info.headless_needed,
                info.priority,
            ]
        )
    return "\n\n".join(
        [
            "# Script File Inventory",
            f"- 생성 시각: `{generated_at}`",
            f"- 원본 경로: `{source_root / SCRIPT_REL}`",
            f"- 스캔 파일 수: `{len(infos)}`",
            "## 우선순위 요약",
            md_table(["Priority", "File count"], [[k, summary[k]] for k in ["Tier0", "Tier1", "Tier2", "Later", "Exclude"]]),
            "## 카테고리 요약",
            md_table(["Category", "File count"], [[k, v] for k, v in sorted(category_summary.items())]),
            "## 파일별 Inventory",
            md_table(
                [
                    "파일 경로",
                    "class/interface/enum",
                    "public/protected methods",
                    "public/protected property/field",
                    "Unity/Photon/UI/Coroutine 의존",
                    "분류",
                    "CardEffect 참조",
                    "headless 대응 필요",
                    "우선순위",
                ],
                rows,
            ),
        ]
    ) + "\n"


def render_playing_logic_map(rows: list[dict], generated_at: str) -> str:
    return "\n\n".join(
        [
            "# Playing Logic Map",
            f"- 생성 시각: `{generated_at}`",
            "- 목적: 카드 효과 구현 전에 알아야 할 실제 플레이 로직 위치와 headless 대응 gap을 1차 정리한다.",
            md_table(
                ["Logic Area", "Original files", "Main classes", "Current headless counterpart", "Gap", "Priority"],
                [
                    [
                        row["logicArea"],
                        row["originalFiles"],
                        row["mainClasses"],
                        row["currentHeadlessCounterpart"],
                        row["gap"],
                        row["priority"],
                    ]
                    for row in rows
                ],
            ),
            "## 해석",
            "- Unity 원본은 `Coroutine`, `GManager.instance`, UI outline, Photon 분기와 실제 룰 처리가 한 파일 안에 섞인 경우가 많다.",
            "- headless 대응은 이미 여러 service/domain model로 분해되어 있으므로, 카드별 효과를 옮기기 전에 CardEffect-facing facade와 unsupported 실패 조건을 먼저 고정해야 한다.",
            "- 특히 `AutoProcessing`, `CEntity_EffectController`, `CardEffectCommons`, `CardEffectFactory`가 카드별 효과 구현의 병목이다.",
        ]
    ) + "\n"


def render_cardeffect_dependency_index(records: dict[str, dict], generated_at: str, source_root: Path) -> str:
    top = sorted(records.values(), key=lambda r: (-r["refCount"], -r["cardEffectFileCount"], r["api"]))[:50]
    top20 = top[:20]
    return "\n\n".join(
        [
            "# CardEffect Dependency Index",
            f"- 생성 시각: `{generated_at}`",
            f"- 원본 경로: `{source_root / CARDEFFECT_REL}`",
            f"- 집계 API 수: `{len(records)}`",
            "## 상위 50 API",
            md_table(
                ["Rank", "API", "Ref count", "CardEffect file count", "Example files", "Headless status", "Priority"],
                [
                    [
                        idx,
                        row["api"],
                        row["refCount"],
                        row["cardEffectFileCount"],
                        row["exampleFiles"],
                        row["headlessStatus"],
                        row["priority"],
                    ]
                    for idx, row in enumerate(top, 1)
                ],
            ),
            "## 상위 20 API 요약",
            "\n".join(f"{idx}. `{row['api']}` - ref {row['refCount']}, files {row['cardEffectFileCount']}" for idx, row in enumerate(top20, 1)),
            "## 주의",
            "- 이 집계는 정적 텍스트 기반 1차 분석이다. instance member는 public/protected member 이름 기반으로 소유 타입을 추정하므로, 최종 포팅 전에 대표 파일을 수동 확인해야 한다.",
            "- 그럼에도 상위권은 `CardEffectCommons`, `ICardEffect`, `CardSource`, `Permanent`, `Player`, `EffectTiming` 계열에 집중되어 CardEffect-facing foundation의 우선순위를 보여준다.",
        ]
    ) + "\n"


def render_gap_matrix(infos: list[SourceFileInfo], generated_at: str) -> str:
    rows = []
    for info in sort_infos(infos):
        if info.priority == "Exclude" and not info.cardeffect_referenced:
            continue
        rows.append(
            [
                info.path + " / " + (", ".join(info.classes + info.interfaces + info.enums) or "-"),
                info.category,
                "Yes" if info.cardeffect_referenced else "No",
                info.current_headless_counterpart or "-",
                info.headless_status,
                info.gap,
                info.priority,
                recommended_work_item(info),
            ]
        )
    return "\n\n".join(
        [
            "# Script To Headless Gap Matrix",
            f"- 생성 시각: `{generated_at}`",
            "- 상태 값: `Exists`, `Partial`, `Missing`, `Ambiguous`, `Unsupported`, `UnityOnly`, `Exclude`.",
            md_table(
                [
                    "Original file/class",
                    "Original responsibility",
                    "CardEffect-facing API",
                    "Current headless counterpart",
                    "Status",
                    "Gap",
                    "Priority",
                    "Recommended work item",
                ],
                rows,
            ),
        ]
    ) + "\n"


def recommended_work_item(info: SourceFileInfo) -> str:
    names = set(info.classes + info.interfaces + info.enums)
    if names & {"GameContext", "Player", "CardSource", "Permanent", "CEntity_Base"}:
        return "state/model facade parity 문서와 테스트 작성"
    if names & {"CEntity_Effect", "CEntity_EffectController", "ICardEffect", "ActivateICardEffect"}:
        return "effect binding/descriptor contract 고정"
    if "CardEffectCommons" in info.path:
        return "CardEffectCommons helper별 primitive 대응표 작성"
    if "CardEffectFactory" in info.path:
        return "factory helper -> EffectDescriptor mapping 작성"
    if names & {"AutoProcessing", "MultipleSkills"}:
        return "trigger collection/resolution 순서 parity 작성"
    if names & {"CardController", "PlayCardClass", "PlayPermanentClass"}:
        return "play/digivolve/option lifecycle parity 작성"
    if names & {"AttackProcess"}:
        return "attack state machine parity 작성"
    if info.category in {"선택/UI/연출", "UnityAdapter 후보"}:
        return "RL decision facade와 UnityAdapter 표시 책임 분리"
    return "후속 분류"


def render_foundation_work_breakdown(items: list[dict], generated_at: str) -> str:
    return "\n\n".join(
        [
            "# Foundation Work Breakdown",
            f"- 생성 시각: `{generated_at}`",
            "- 목적: 카드별 `CardEffect` 구현 전에 필요한 기반 작업을 Tier별로 산정한다.",
            md_table(
                ["Work item", "Tier", "Original source", "Needed for CardEffect?", "Current status", "Estimated size", "Blocked by", "Done condition"],
                [
                    [
                        row["workItem"],
                        row["tier"],
                        row["originalSource"],
                        row["neededForCardEffect"],
                        row["currentStatus"],
                        row["estimatedSize"],
                        row["blockedBy"],
                        row["doneCondition"],
                    ]
                    for row in items
                ],
            ),
            "## Tier 요약",
            "- Tier0: CardEffect가 컴파일/실행 의미를 잃지 않기 위한 state, effect, metadata, helper facade 기반.",
            "- Tier1: 실제 효과 실행과 기본 검증에 필요한 trigger, selection, zone movement, play/option lifecycle 기반.",
            "- Tier2: 전투/복합 mechanic/룰 처리/메인 phase action parity를 완성하기 위한 기반.",
        ]
    ) + "\n"


def render_recommended_next_task(generated_at: str, top_api_records: list[dict]) -> str:
    top_names = ", ".join(f"`{row['api']}`" for row in top_api_records[:8])
    prompt = """다음 작업은 구현이 아니라 `CardEffectCommons`와 state facade의 Script Runtime Foundation / 공통 호출 계약 고정 작업으로 진행한다.

목표:
Unity 원본 `CardEffect`가 가장 많이 참조하는 `CardEffectCommons`, `CardSource`, `Permanent`, `Player`, `ICardEffect`, `EffectTiming` 호출 계약을 기준으로, headless에서 어떤 facade/primitive가 존재해야 하는지 상위 계약부터 unsupported 실패 조건과 함께 문서화하고 최소 검증 범위를 산정한다.

금지:
- 카드별 CardEffect body 구현 금지
- 신규 카드 효과 구현 금지
- 원본 `DCGO/Assets` 수정 금지
- 대규모 RL.Engine 리팩터링 금지

산출물:
- `docs/rl-engine/script-runtime-foundation-contract.md`
- `docs/generated/as-is/script_runtime_foundation_contract_candidates.json`
- 필요한 경우 분석 스크립트만 `scripts/` 아래 추가/보강
"""
    return "\n\n".join(
        [
            "# Recommended Next Foundation Task",
            f"- 생성 시각: `{generated_at}`",
            "## 1. 다음 작업명",
            "Script Runtime Foundation 공통 호출 계약 정리",
            "## 2. 목표",
            "카드별 효과 구현을 시작하기 전에, 원본 `CardEffect`들이 기대하는 공통 호출 계약과 headless primitive/service 대응 계약을 상위 참조 계약부터 고정한다.",
            "## 3. 수정 대상 파일",
            "- 문서: `docs/rl-engine/script-runtime-foundation-contract.md`",
            "- generated JSON: `docs/generated/as-is/script_runtime_foundation_contract_candidates.json`",
            "- 필요 시 분석 스크립트: `scripts/` 아래",
            "## 4. 만들 문서/테스트",
            "- 상위 공통 호출 계약별 원본 의미, headless 대응 위치, unsupported 조건, 검증 필요성 표",
            "- 아직 구현 테스트가 아니라 계약 검증 후보와 representative source evidence 정리",
            "## 5. 금지 사항",
            "- CardEffect body 구현 금지",
            "- 신규 카드 효과 구현 금지",
            "- `DCGO/Assets` 수정 금지",
            "- RL Environment/Reward/Dataset/Trainer 구현 금지",
            "## 6. 완료 조건",
            f"- 상위 공통 호출 계약 후보가 문서화됨: {top_names}",
            "- 각 호출 계약이 `Exists/Partial/Missing/UnityOnly/Ambiguous` 중 하나로 분류됨",
            "- 다음 실제 foundation 구현 단위가 Tier0/Tier1로 분해됨",
            "## 7. Codex에게 줄 다음 프롬프트 초안",
            "```text\n" + prompt.strip() + "\n```",
        ]
    ) + "\n"


def metric_for(records: dict[str, dict], contract: str) -> dict[str, int]:
    normalized = contract.replace("()", "")
    record = records.get(normalized) or records.get(contract)
    if record is None:
        return {"referenceCount": 0, "cardEffectFileCount": 0}
    return {
        "referenceCount": int(record["refCount"]),
        "cardEffectFileCount": int(record["cardEffectFileCount"]),
    }


def build_script_runtime_contract_candidates(api_records: dict[str, dict]) -> list[dict]:
    rows = [
        (
            "CardSource.Owner",
            "B. Runtime state foundation",
            "DCGO/Assets/Scripts/Script/CardSource.cs",
            "카드 인스턴스의 소유 플레이어를 돌려주는 원본 상태 접근 계약",
            "효과 source의 owner/opponent zone과 선택 범위를 결정할 때 거의 항상 필요하다.",
            "CardInstance.Controller/Owner 의미, PlayerState, GameState",
            "Partial",
            "Owner/Controller 의미 차이를 문서화하고 effect context에서 안정적으로 조회하게 한다.",
            "카드별 effect body에서 GameState를 직접 순회하거나 owner를 임의 추정하지 않는다.",
            "동일 카드가 hand, field, security, trash에 있을 때 owner 조회가 동일하게 유지되는 fixture",
            "Tier0",
        ),
        (
            "CardSource.PermanentOfThisCard",
            "B. Runtime state foundation",
            "DCGO/Assets/Scripts/Script/CardSource.cs",
            "카드가 현재 속한 field permanent를 찾는 계약",
            "inherited/link/top-card 효과가 자신이 속한 stack과 top card를 판정해야 한다.",
            "PermanentState lookup by CardInstanceId, GameState field zones",
            "Partial",
            "CardInstanceId -> PermanentState 역조회 facade를 만들고 없을 때 null/unsupported 의미를 고정한다.",
            "zone list를 직접 뒤져서 임시 null 규칙을 카드별로 반복하지 않는다.",
            "digivolution source, top card, hand card 각각에 대해 permanent 조회 결과 검증",
            "Tier0",
        ),
        (
            "CardSource.IsDigimon",
            "B. Runtime state foundation",
            "DCGO/Assets/Scripts/Script/CardSource.cs",
            "카드 정의/현재 상태가 Digimon인지 판정하는 계약",
            "대상 조건, trigger 조건, play/digivolve 가능 여부 분기에 필요하다.",
            "CardDefinition.CardType, PermanentState kind helpers",
            "Partial",
            "CardDefinition metadata에서 카드 타입 판정을 통일한다.",
            "문자열 이름/색상 조건으로 Digimon 여부를 추정하지 않는다.",
            "Digimon/Tamer/Option 샘플 정의에 대한 type predicate 검증",
            "Tier0",
        ),
        (
            "CardSource.IsTamer",
            "B. Runtime state foundation",
            "DCGO/Assets/Scripts/Script/CardSource.cs",
            "카드가 Tamer인지 판정하는 계약",
            "Tamer 대상 선택, Tamer 효과 구분, memory setter류 효과에 필요하다.",
            "CardDefinition.CardType",
            "Partial",
            "CardDefinition 기반 type predicate로 노출한다.",
            "field permanent의 표시 상태나 UI frame으로 판정하지 않는다.",
            "Tamer 카드가 field/security/hand에서 동일하게 Tamer로 판정되는지 검증",
            "Tier0",
        ),
        (
            "CardSource.Level",
            "B. Runtime state foundation",
            "DCGO/Assets/Scripts/Script/CardSource.cs",
            "카드 level metadata 접근 계약",
            "level 조건 삭제, 진화 조건, source 조건에서 반복 사용된다.",
            "CardDefinition.Level",
            "Partial",
            "level이 없는 Option/Tamer의 null/0 의미를 원본과 비교해 고정한다.",
            "없는 level을 임의 0으로 두고 조건을 통과시키지 않는다.",
            "level 조건 helper가 level 없는 카드에서 false/unsupported 중 정한 결과를 반환하는지 검증",
            "Tier0",
        ),
        (
            "CardSource.CardColors",
            "B. Runtime state foundation",
            "DCGO/Assets/Scripts/Script/CardSource.cs",
            "카드 색상 조건 접근 계약",
            "색상 조건, 색상 수 집계, 진화/옵션 색상 요구 조건에서 반복 사용된다.",
            "CardDefinition.CardColors, StaticEffectService effective color layer",
            "Partial",
            "base metadata 색상 facade는 존재하지만 continuous/static color 변경 layer와 연결해야 한다.",
            "카드별 body에서 색상 리스트를 임의로 재구성하거나 표시 텍스트에서 추론하지 않는다.",
            "중복 색상 제거와 다색 카드 조건 fixture 검증",
            "Tier0",
        ),
        (
            "CardSource.CardNames",
            "B. Runtime state foundation",
            "DCGO/Assets/Scripts/Script/CardSource.cs",
            "카드 이름/명칭군 조건 접근 계약",
            "명칭 조건, 포함 이름, token/name alias 조건을 구현할 때 필요하다.",
            "CardDefinition.Names/NameTokens",
            "Partial",
            "asset metadata에서 이름군을 정규화해 CardDefinition에 보존한다.",
            "표시용 localization 문자열만으로 조건을 판정하지 않는다.",
            "동일 카드의 ENG/KOR/base name 조건이 deterministic하게 일치하는지 검증",
            "Tier0",
        ),
        (
            "CardSource.CardTraits",
            "B. Runtime state foundation",
            "DCGO/Assets/Scripts/Script/CardSource.cs",
            "카드 trait/species/attribute 조건 접근 계약",
            "trait 기반 검색, DP 변경, 진화 조건, target 조건에서 필요하다.",
            "CardDefinition.Traits/Attributes",
            "Partial",
            "trait normalization과 contains/match helper를 headless metadata contract로 고정한다.",
            "문자열 부분 검색을 카드별 body에 흩뿌리지 않는다.",
            "대표 trait 조건 카드가 asset metadata와 같은 결과를 내는지 검증",
            "Tier0",
        ),
        (
            "Permanent.TopCard",
            "B. Runtime state foundation",
            "DCGO/Assets/Scripts/Script/Permanent.cs",
            "field stack의 최상단 카드 접근 계약",
            "대상 Digimon의 현재 카드 정보, DP, keyword, trigger source를 결정한다.",
            "PermanentState.TopCardId, CardInstance lookup",
            "Partial",
            "empty permanent를 만들지 않도록 invariant를 두고 TopCard 조회 실패를 명시 실패로 처리한다.",
            "빈 stack을 silently no-op하지 않는다.",
            "stack 이동/진화/퇴화 후 TopCard가 원본 의미와 일치하는지 검증",
            "Tier0",
        ),
        (
            "Permanent.DigivolutionCards",
            "B. Runtime state foundation",
            "DCGO/Assets/Scripts/Script/Permanent.cs",
            "permanent 아래 진화원 목록 접근 계약",
            "진화원 조건, trash source, inherited 효과 수집에 필요하다.",
            "PermanentState.SourceCardIds",
            "Partial",
            "top card와 sources 순서, bottom/top 제거 규칙을 명시한다.",
            "List 순서를 카드별로 임의 재해석하지 않는다.",
            "bottom/top source trash와 inherited effect 수집 순서 검증",
            "Tier0",
        ),
        (
            "Player.HandCards",
            "B. Runtime state foundation",
            "DCGO/Assets/Scripts/Script/Player.cs",
            "플레이어 hand zone 접근 계약",
            "hand 조건, search, reveal, play from hand 효과의 기본 입력이다.",
            "PlayerState.Hand",
            "Partial",
            "zone 접근은 ZoneMover/zone facade를 통해 읽고 이동은 공통 primitive를 사용한다.",
            "Hand list를 직접 수정하지 않는다.",
            "draw/search/play 후 hand count와 card id sequence 검증",
            "Tier0",
        ),
        (
            "Player.Enemy",
            "B. Runtime state foundation",
            "DCGO/Assets/Scripts/Script/Player.cs",
            "상대 플레이어 접근 계약",
            "상대 battle area/security/hand/trash 조건을 간단히 표현하는 기반이다.",
            "GameState.GetOpponent(PlayerId)",
            "Partial",
            "effect context에 controller와 opponent resolver를 제공한다.",
            "player id를 0/1 상수로 직접 뒤집지 않는다.",
            "turn player/non-turn player와 owner enemy가 독립적으로 맞는지 검증",
            "Tier0",
        ),
        (
            "ICardEffect.SetUpICardEffect",
            "A. Effect model foundation",
            "DCGO/Assets/Scripts/Script/ICardEffect.cs",
            "효과 객체에 source card, timing, owner, hash 등 실행 metadata를 세팅하는 계약",
            "모든 effect descriptor가 어떤 카드에서 온 효과인지 알아야 한다.",
            "EffectDescriptor, EffectContext, TriggerSourceSnapshot",
            "Partial",
            "source snapshot, timing, controller, use-count key를 descriptor 생성 시점에 고정한다.",
            "실행 시점에 source를 다시 추정하지 않는다.",
            "source가 zone 이동 후에도 triggered snapshot 기준으로 유지되는지 검증",
            "Tier0",
        ),
        (
            "ICardEffect.SetHashString",
            "A. Effect model foundation",
            "DCGO/Assets/Scripts/Script/ICardEffect.cs",
            "동일 효과/선택/사용 횟수 식별에 쓰는 문자열 설정 계약",
            "once per turn, 중복 trigger skip, 선택 id 안정성에 필요하다.",
            "EffectDescriptor.StableId/SourceKey, DecisionToken",
            "Partial",
            "원본 hash string 의미를 stable descriptor id와 request id 정책으로 대응한다.",
            "Guid/random id로 deterministic replay를 깨지 않는다.",
            "같은 seed/action sequence에서 request id와 state hash가 동일한지 검증",
            "Tier0",
        ),
        (
            "ICardEffect.SetIsInheritedEffect",
            "A. Effect model foundation",
            "DCGO/Assets/Scripts/Script/ICardEffect.cs",
            "효과가 inherited effect인지 표시하는 계약",
            "진화원 효과 수집, top card 효과 제외, inherited 조건 판정에 필요하다.",
            "EffectDescriptor.SourceKind, TriggerSourceSnapshot",
            "Partial",
            "top/source 위치에 따른 SourceKind를 descriptor에 명시한다.",
            "card definition만 보고 inherited 여부를 고정하지 않는다.",
            "top card와 source card가 같은 definition을 가질 때 source kind가 분리되는지 검증",
            "Tier0",
        ),
        (
            "EffectTiming.None",
            "A. Effect model foundation",
            "DCGO/Assets/Scripts/Script/ICardEffect.cs",
            "상시/정적/추가 효과 수집에 쓰이는 timing 값",
            "static effect, add-skill, keyword grant가 이 timing을 기준으로 수집된다.",
            "EffectTiming.None alias, StaticEffectService, ContinuousEffectService",
            "Partial",
            "None timing을 continuous/static descriptor 수집 기준으로 명시한다.",
            "None을 아무 효과 없음으로 단순 해석하지 않는다.",
            "static/add-skill descriptor가 None timing에서 수집되는지 검증",
            "Tier0",
        ),
        (
            "EffectTiming.OnEnterFieldAnyone",
            "A. Effect model foundation",
            "DCGO/Assets/Scripts/Script/ICardEffect.cs",
            "누구든 field 진입 시 trigger되는 timing",
            "On Play/When Digivolving/enter field family trigger의 공통 진입점이다.",
            "TriggerCollector, EnterFieldEventPayload, TriggerPipelineService",
            "Partial",
            "enter field payload에 actor, card, permanent, source action을 보존한다.",
            "단순 OnPlay 또는 WhenDigivolving으로만 축소하지 않는다.",
            "자신/상대/진화/플레이 enter field 케이스별 trigger 수집 검증",
            "Tier0",
        ),
        (
            "EffectTiming.OnAllyAttack",
            "F. Execution flow foundation",
            "DCGO/Assets/Scripts/Script/ICardEffect.cs",
            "아군 공격 선언 후 trigger timing",
            "attack source, target, attack effect context가 필요한 공격계 효과의 기본 timing이다.",
            "AttackService, TriggerCollector, AttackRuntimeContext",
            "Partial",
            "attack declaration과 counter/block 전후 timing 순서를 원본과 대조해 고정한다.",
            "공격 효과를 main phase 일반 trigger로 섞지 않는다.",
            "attack declaration -> OnAllyAttack -> counter/block 순서 검증",
            "Tier2",
        ),
        (
            "CEntity_Effect.CardEffects",
            "A. Effect model foundation",
            "DCGO/Assets/Scripts/Script/CEntity_Effect.cs",
            "카드별 effect class가 timing/cardSource를 받아 List<ICardEffect>를 반환하는 원본 진입점",
            "나중에 로컬 LLM/OpenCode가 카드별 효과를 구현할 때 반드시 맞춰야 하는 반환 계약이다.",
            "EffectDescriptor registry, card script descriptor factory",
            "Missing",
            "카드별 script가 descriptor 목록을 반환하는 headless 호환 진입 계약을 Tier0로 정의한다.",
            "이 진입점을 Later로 미루거나 카드별 구현마다 임의 메서드명을 만들지 않는다.",
            "대표 카드 script가 timing별 descriptor 목록을 반환하고 null effect를 제거하는지 검증",
            "Tier0",
        ),
        (
            "CEntity_EffectController.AddCardEffect",
            "A. Effect model foundation",
            "DCGO/Assets/Scripts/Script/CEntity_EffectController.cs",
            "CardEffectClassName을 실제 CEntity_Effect 구현으로 연결하고 없으면 EmptyEffectClass를 붙이는 원본 바인딩 계약",
            "카드별 구현 전에 asset CardEffectClassName이 어떤 headless effect class로 연결되는지 deterministic하게 고정해야 한다.",
            "CEntity_EffectController injected registry, EmptyEffectClass fallback",
            "Partial",
            "ClassName lookup 후보와 fallback/실패 조건을 headless registry 계약으로 고정한다.",
            "미등록 non-empty ClassName을 EmptyEffectClass로 조용히 대체하지 않는다.",
            "direct/full namespace/token namespace lookup과 missing class 실패 검증",
            "Tier0",
        ),
        (
            "CardEffectInterfaces.cs effect interfaces",
            "A. Effect model foundation",
            "DCGO/Assets/Scripts/Script/CardEffectInterfaces.cs",
            "IAddSkillEffect, IChangeDPEffect, ICanNotPlayCardEffect, IBlockerEffect, IRushEffect, IRebootEffect 등 효과 성격을 나타내는 interface 묶음",
            "정적 효과, keyword, 조건 변경, 비용/공격 제한류를 descriptor나 primitive로 분류하는 근거다.",
            "EffectDescriptor tags, StaticEffectService, BattleKeywordService, StaticRequirementService",
            "Ambiguous",
            "Exclude 확정 대신 interface별 headless 대응 범주를 재검토 대상으로 유지한다.",
            "Unity/MonoBehaviour 의존이 있다는 이유만으로 효과 의미까지 버리지 않는다.",
            "상위 interface별 descriptor/static/primitive 대응 표 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistOnBattleArea",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
            "카드 또는 permanent가 battle area에 존재하는지 판정하는 helper 계약",
            "target 유효성, trigger source 유지, effect fizzles 판정에 자주 필요하다.",
            "GameState field zone helpers, PermanentState location predicates",
            "Partial",
            "zone/presence 판정 helper를 state facade로 제공한다.",
            "UI frame 존재 여부나 object active 상태로 판정하지 않는다.",
            "zone 이동 전후 battle area 존재 판정 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistOnBattleAreaDigimon",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
            "battle area에 존재하면서 Digimon인지를 판정하는 helper 계약",
            "대상 필터와 trigger 가능성 검사의 핵심 조건이다.",
            "PermanentState + CardDefinition type predicate",
            "Partial",
            "battle area presence와 card type predicate를 결합한 공통 helper로 둔다.",
            "카드별로 field zone과 type 조건을 따로 재작성하지 않는다.",
            "Tamer/Option permanent가 battle area에 있을 때 false인지 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistOnFieldTrigger",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "trigger condition에서 카드가 field에 존재하는지 확인하고 당시 permanent를 캡처하는 helper 계약",
            "pending/activate 시점에 source가 같은 permanent에 남아 있는지 재검증하기 위해 필요하다.",
            "ICardEffect.EffectSourcePermanent, ActivateICardEffect.PermanentWhenTriggered",
            "Partial",
            "trigger 시점 source permanent snapshot을 effect descriptor 흐름과 연결한다.",
            "원본 CardPermanenceMap을 전역 singleton mutable state로 복제하지 않는다.",
            "trigger 캡처 후 activate 재검증 fixture",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistOnBreedingAreaTrigger",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "trigger condition에서 카드가 breeding area에 존재하는지 확인하고 당시 permanent를 캡처하는 helper 계약",
            "breeding area source 효과가 activate 시점까지 같은 source를 유지하는지 검증하기 위해 필요하다.",
            "ICardEffect.EffectSourcePermanent, ActivateICardEffect.PermanentWhenTriggered",
            "Partial",
            "breeding area trigger source permanent snapshot을 effect descriptor 흐름과 연결한다.",
            "battle area source를 breeding area trigger로 조용히 통과시키지 않는다.",
            "breeding/battle 분리 trigger 캡처 fixture",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistOnBattleAreaTrigger",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "trigger condition에서 카드가 battle area에 존재하는지 확인하고 당시 permanent를 캡처하는 helper 계약",
            "battle area source 효과가 selection/pending 이후 stale source로 실행되지 않게 하기 위해 필요하다.",
            "ICardEffect.EffectSourcePermanent, ActivateICardEffect.PermanentWhenTriggered",
            "Partial",
            "battle area trigger source permanent snapshot을 effect descriptor 흐름과 연결한다.",
            "현재 field query만 보고 activate 시점 재검증을 생략하지 않는다.",
            "battle area trigger 캡처와 stale permanent reject fixture",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistOnBattleAreaDigimonTrigger",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "trigger condition에서 카드가 battle area Digimon인지 확인하고 당시 permanent를 캡처하는 helper 계약",
            "Digimon source trigger가 Tamer/Option 또는 stale source로 실행되지 않게 하기 위해 필요하다.",
            "ICardEffect.EffectSourcePermanent, ActivateICardEffect.PermanentWhenTriggered",
            "Partial",
            "battle area Digimon trigger source permanent snapshot을 effect descriptor 흐름과 연결한다.",
            "type 조건과 source snapshot을 카드별 body에 중복 구현하지 않는다.",
            "Tamer false와 Digimon trigger 캡처 fixture",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistDigivolutionCardsTrigger",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "trigger condition에서 카드가 digivolution source인지 확인하고 host permanent를 캡처하는 helper 계약",
            "inherited/source effect가 source stack의 host permanent를 기준으로 activate되는지 보장하기 위해 필요하다.",
            "ICardEffect.EffectSourcePermanent, ActivateICardEffect.PermanentWhenTriggered",
            "Partial",
            "digivolution source trigger host permanent snapshot을 effect descriptor 흐름과 연결한다.",
            "source card의 현재 zone만 보고 host permanent 재검증을 생략하지 않는다.",
            "digivolution source trigger/activate fixture",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistLinkedTrigger",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "trigger condition에서 카드가 linked card인지 확인하고 host permanent를 캡처하는 helper 계약",
            "linked source effect가 link host를 잃은 뒤에도 실행되는 것을 막기 위해 필요하다.",
            "ICardEffect.EffectSourcePermanent, ActivateICardEffect.PermanentWhenTriggered",
            "Partial",
            "linked source trigger host permanent snapshot을 effect descriptor 흐름과 연결한다.",
            "linked card source를 top-card source로 임의 변환하지 않는다.",
            "linked source trigger/activate fixture",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistOnFieldActivate",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "activate 시점에 카드가 field에 존재하고 trigger 때 캡처한 permanent와 같은지 재검증하는 helper 계약",
            "pending selection 이후 source가 이동한 효과를 fizzle시키기 위해 필요하다.",
            "ICardEffect.EffectSourcePermanent, ActivateICardEffect.PermanentWhenTriggered",
            "Partial",
            "activate 시점 current permanent와 captured permanent를 PermanentId로 비교한다.",
            "캡처 없이 현재 field 존재만으로 activate를 허용하지 않는다.",
            "field activate stale permanent reject fixture",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistOnBreedingAreaActivate",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "activate 시점에 카드가 breeding area에 존재하고 trigger 때 캡처한 permanent와 같은지 재검증하는 helper 계약",
            "breeding source가 다른 zone으로 이동한 뒤 실행되는 것을 방지하기 위해 필요하다.",
            "ICardEffect.EffectSourcePermanent, ActivateICardEffect.PermanentWhenTriggered",
            "Partial",
            "breeding activate 시점 current permanent와 captured permanent를 PermanentId로 비교한다.",
            "battle area 존재를 breeding activate 조건으로 대체하지 않는다.",
            "breeding activate false fixture",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistOnBattleAreaActivate",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "activate 시점에 카드가 battle area에 존재하고 trigger 때 캡처한 permanent와 같은지 재검증하는 helper 계약",
            "battle area source 효과의 pending/resume source parity를 보장하기 위해 필요하다.",
            "ICardEffect.EffectSourcePermanent, ActivateICardEffect.PermanentWhenTriggered",
            "Partial",
            "battle activate 시점 current permanent와 captured permanent를 PermanentId로 비교한다.",
            "현재 battle area 존재만으로 stale effect를 통과시키지 않는다.",
            "battle area trigger 후 activate fixture",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistOnBattleAreaDigimonActivate",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "activate 시점에 카드가 battle area Digimon이고 trigger 때 캡처한 permanent와 같은지 재검증하는 helper 계약",
            "Digimon source 효과의 type/location/source parity를 함께 보장하기 위해 필요하다.",
            "ICardEffect.EffectSourcePermanent, ActivateICardEffect.PermanentWhenTriggered",
            "Partial",
            "battle area Digimon activate 시점 current permanent와 captured permanent를 PermanentId로 비교한다.",
            "Digimon 타입 조건과 source parity 중 하나를 생략하지 않는다.",
            "Digimon/Tamer 분리 activate fixture",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistDigivolutionCardsActivate",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "activate 시점에 카드가 같은 host permanent의 digivolution source인지 재검증하는 helper 계약",
            "source card가 제거된 뒤 inherited/source 효과가 실행되는 것을 막기 위해 필요하다.",
            "ICardEffect.EffectSourcePermanent, ActivateICardEffect.PermanentWhenTriggered",
            "Partial",
            "digivolution source activate 시점 current host와 captured permanent를 PermanentId로 비교한다.",
            "source stack membership과 host parity를 분리해서 누락하지 않는다.",
            "digivolution source activate fixture",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistLinkedActivate",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "activate 시점에 카드가 같은 host permanent의 linked card인지 재검증하는 helper 계약",
            "linked card가 host를 떠난 뒤 linked effect가 실행되는 것을 막기 위해 필요하다.",
            "ICardEffect.EffectSourcePermanent, ActivateICardEffect.PermanentWhenTriggered",
            "Partial",
            "linked source activate 시점 current host와 captured permanent를 PermanentId로 비교한다.",
            "linked source를 별도 snapshot 없이 즉시 실행 조건으로만 처리하지 않는다.",
            "linked source activate fixture",
            "Tier0",
        ),
        (
            "CardEffectCommons.HasMatchConditionPermanent",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
            "조건에 맞는 permanent가 존재하는지 판정하는 helper 계약",
            "조건부 trigger, cost reduction, effect availability에 반복 사용된다.",
            "query helpers over GameState/PermanentState",
            "Partial",
            "owner/opponent/battle/breeding 범위와 predicate 조합을 deterministic query로 정리한다.",
            "LINQ 조건을 카드별 body에 임의 복붙하지 않는다.",
            "owner/opponent 범위별 count/existence 조건 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.MatchConditionPermanentCount",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
            "조건에 맞는 permanent 수를 계산하는 helper 계약",
            "수량 조건, DP 증가량, 반복 적용 횟수 산정에 필요하다.",
            "query/count helpers over GameState/PermanentState",
            "Partial",
            "count 대상 범위와 중복 계산 여부를 명시한다.",
            "TopCard와 source card를 중복 permanent로 계산하지 않는다.",
            "조건 permanent 수에 따라 효과량이 바뀌는 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistInSecurity",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "security zone에 있는 카드가 지정된 flipped 상태인지 판정하는 helper 계약",
            "face-down/face-up security 조건과 security effect/provider 판정에 필요하다.",
            "CardSource.IsFlipped, Player.SecurityCards",
            "Partial",
            "원본 기본값 isFlipped=false와 face-up alias를 보존한다.",
            "security zone membership과 flipped 상태를 혼동하지 않는다.",
            "face-down/face-up security card fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsExistInAnyTrash",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "카드가 owner 또는 opponent trash에 존재하는지 판정하는 helper 계약",
            "trash 이동 trigger와 trash 조건부 효과 가능성 검사에 반복 사용된다.",
            "CardSource.Owner, Player.TrashCards",
            "Partial",
            "owner/opponent trash를 read-only facade로 조회한다.",
            "CurrentZone만 보고 trash 존재를 확정하지 않는다.",
            "owner/opponent trash card fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.HasMatchConditionOwnersHand",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "owner hand에 조건을 만족하는 카드가 있는지 판정하는 helper 계약",
            "hand 조건부 cost, target, trigger availability에 필요하다.",
            "Player.HandCards query facade",
            "Partial",
            "owner hand predicate와 count helper를 공통 facade로 제공한다.",
            "카드별 body에서 hand list를 직접 순회하는 구현을 늘리지 않는다.",
            "owner hand existence/count fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.HasMatchConditionOwnersBreedingPermanent",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "owner breeding area에 조건을 만족하는 permanent가 있는지 판정하는 helper 계약",
            "breeding area 조건부 trigger와 effect availability에 필요하다.",
            "Player.GetBreedingAreaPermanents query facade",
            "Partial",
            "owner breeding permanent predicate를 공통 facade로 제공한다.",
            "battle area helper에 breeding area를 섞어 계산하지 않는다.",
            "owner/opponent breeding permanent 분리 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.GetUniqueColourCountOnOwnerBattleArea",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "owner battle area의 조건 만족 permanent top card 색상 수를 중복 제거해 계산하는 helper 계약",
            "색상 수 조건부 cost/level/target 판정에서 공통 색상 집계가 필요하다.",
            "CardSource.CardColors, Player.GetBattleAreaPermanents query facade",
            "Partial",
            "owner battle area permanent predicate와 top card 색상 집계를 공통 facade로 제공한다.",
            "breeding area나 opponent field 색상을 owner battle area 색상으로 섞어 계산하지 않는다.",
            "owner battle area unique color count fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.GetUniqueColourCountOnOpponentsBattleArea",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "opponent battle area의 조건 만족 permanent top card 색상 수를 중복 제거해 계산하는 helper 계약",
            "상대 field 색상 수 조건부 memory/target 판정에서 공통 색상 집계가 필요하다.",
            "CardSource.CardColors, Player.Enemy.GetBattleAreaPermanents query facade",
            "Partial",
            "opponent battle area permanent predicate와 top card 색상 집계를 공통 facade로 제공한다.",
            "owner field나 breeding area 색상을 opponent battle area 색상으로 섞어 계산하지 않는다.",
            "opponent battle area unique color count fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnField",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 field 전체, 즉 battle 또는 breeding area에 존재하는지 판정하는 helper 계약",
            "target 유효성, source 유지 여부, field 전체 조건부 효과 availability에 필요하다.",
            "Permanent.TopCard.Owner field membership query facade",
            "Partial",
            "battle/breeding membership을 PermanentId 기준 read-only facade로 판정한다.",
            "Permanent facade 객체 참조 동일성이나 UI frame object로 판정하지 않는다.",
            "battle/breeding/null permanent 존재 판정 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnBattleArea",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 battle area에 존재하는지 판정하는 helper 계약",
            "대상 선택과 battle-area 조건부 효과의 공통 유효성 검사에 필요하다.",
            "Permanent.TopCard.Owner.GetBattleAreaPermanents query facade",
            "Partial",
            "top card owner의 battle area membership을 PermanentId 기준으로 판정한다.",
            "breeding area permanent를 battle area 존재로 계산하지 않는다.",
            "battle/breeding 분리 permanent fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnBreedingArea",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 breeding area에 존재하는지 판정하는 helper 계약",
            "breeding area 조건부 trigger와 이동 전후 유효성 검사에 필요하다.",
            "Permanent.TopCard.Owner.GetBreedingAreaPermanents query facade",
            "Partial",
            "top card owner의 breeding area membership을 PermanentId 기준으로 판정한다.",
            "battle area permanent를 breeding area 존재로 계산하지 않는다.",
            "battle/breeding 분리 permanent fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnOwnerBattleArea",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 효과 source card owner의 battle area에 존재하는지 판정하는 helper 계약",
            "내 permanent만 대상으로 하는 target/filter 조건에 반복 사용된다.",
            "IsPermanentExistsOnBattleArea + IsOwnerPermanent facade",
            "Partial",
            "battle area membership과 owner predicate를 조합한 공통 facade를 제공한다.",
            "owner/opponent 판정을 카드별 body에서 직접 player id 비교로 복제하지 않는다.",
            "owner/opponent battle permanent 분리 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnOpponentBattleArea",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 효과 source card owner 상대의 battle area에 존재하는지 판정하는 helper 계약",
            "상대 permanent만 대상으로 하는 target/filter 조건에 반복 사용된다.",
            "IsPermanentExistsOnBattleArea + IsOpponentPermanent facade",
            "Partial",
            "battle area membership과 opponent predicate를 조합한 공통 facade를 제공한다.",
            "owner/opponent 판정을 카드별 body에서 직접 player id 비교로 복제하지 않는다.",
            "owner/opponent battle permanent 분리 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnOwnerBreedingArea",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 효과 source card owner의 breeding area에 존재하는지 판정하는 helper 계약",
            "내 breeding area 조건부 효과 availability와 source 유효성 검사에 필요하다.",
            "IsPermanentExistsOnBreedingArea + IsOwnerPermanent facade",
            "Partial",
            "breeding area membership과 owner predicate를 조합한 공통 facade를 제공한다.",
            "battle area helper에 breeding area 판정을 숨겨 섞지 않는다.",
            "owner/opponent breeding permanent 분리 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnOpponentBreedingArea",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 효과 source card owner 상대의 breeding area에 존재하는지 판정하는 helper 계약",
            "상대 breeding area 조건부 효과 availability와 source 유효성 검사에 필요하다.",
            "IsPermanentExistsOnBreedingArea + IsOpponentPermanent facade",
            "Partial",
            "breeding area membership과 opponent predicate를 조합한 공통 facade를 제공한다.",
            "battle area helper에 breeding area 판정을 숨겨 섞지 않는다.",
            "owner/opponent breeding permanent 분리 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnBattleAreaDigimon",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 battle area Digimon인지 판정하는 helper 계약",
            "Digimon 대상 선택과 DP/삭제/공격 관련 조건에서 반복 사용된다.",
            "IsPermanentExistsOnBattleArea + Permanent.IsDigimon facade",
            "Partial",
            "battle area membership과 permanent type predicate를 조합한 공통 facade를 제공한다.",
            "Tamer나 breeding permanent를 battle area Digimon으로 계산하지 않는다.",
            "battle area Digimon/Tamer/breeding 분리 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnOwnerBattleAreaDigimon",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 source owner의 battle area Digimon인지 판정하는 helper 계약",
            "내 Digimon 조건부 target/filter와 수량 조건에서 매우 자주 사용된다.",
            "IsPermanentExistsOnOwnerBattleArea + Permanent.IsDigimon facade",
            "Partial",
            "owner battle area membership과 Digimon predicate를 조합한 공통 facade를 제공한다.",
            "상대 Digimon이나 Tamer를 owner battle area Digimon으로 계산하지 않는다.",
            "owner battle area Digimon/Tamer/opponent 분리 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnOpponentBattleAreaDigimon",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 source owner 상대의 battle area Digimon인지 판정하는 helper 계약",
            "상대 Digimon target/filter와 DP/삭제 조건에서 매우 자주 사용된다.",
            "IsPermanentExistsOnOpponentBattleArea + Permanent.IsDigimon facade",
            "Partial",
            "opponent battle area membership과 Digimon predicate를 조합한 공통 facade를 제공한다.",
            "owner Digimon이나 Tamer를 opponent battle area Digimon으로 계산하지 않는다.",
            "opponent battle area Digimon/Tamer/owner 분리 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnBattleAreaTamer",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 battle area Tamer인지 판정하는 helper 계약",
            "Tamer 조건부 DP/memory/색상 조건과 target/filter에서 반복 사용된다.",
            "IsPermanentExistsOnBattleArea + Permanent.IsTamer facade",
            "Partial",
            "battle area membership과 Tamer predicate를 조합한 공통 facade를 제공한다.",
            "Digimon이나 breeding permanent를 battle area Tamer로 계산하지 않는다.",
            "battle area Tamer/Digimon/breeding 분리 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnOwnerBattleAreaTamer",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 source owner의 battle area Tamer인지 판정하는 helper 계약",
            "내 Tamer 조건부 DP/memory/색상 조건과 target/filter에서 반복 사용된다.",
            "IsPermanentExistsOnOwnerBattleArea + Permanent.IsTamer facade",
            "Partial",
            "owner battle area membership과 Tamer predicate를 조합한 공통 facade를 제공한다.",
            "상대 Tamer나 Digimon을 owner battle area Tamer로 계산하지 않는다.",
            "owner battle area Tamer/Digimon/opponent 분리 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsPermanentExistsOnOpponentBattleAreaTamer",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "permanent가 source owner 상대의 battle area Tamer인지 판정하는 helper 계약",
            "상대 Tamer 조건부 DP/memory/색상 조건과 target/filter에서 반복 사용된다.",
            "IsPermanentExistsOnOpponentBattleArea + Permanent.IsTamer facade",
            "Partial",
            "opponent battle area membership과 Tamer predicate를 조합한 공통 facade를 제공한다.",
            "owner Tamer나 Digimon을 opponent battle area Tamer로 계산하지 않는다.",
            "opponent battle area Tamer/Digimon/owner 분리 fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.HasMatchConditionPermanentDigivolutionCards",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "source permanent의 digivolution card에 조건을 만족하는 카드가 있는지 판정하는 helper 계약",
            "source 조건 효과와 inherited effect 조건에 반복 사용된다.",
            "CardSource.PermanentOfThisCard, Permanent.DigivolutionCards",
            "Partial",
            "source stack predicate를 Permanent facade를 통해 조회한다.",
            "top card와 source card를 같은 zone 카드로 취급하지 않는다.",
            "digivolution source predicate fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.HasMatchConditionOwnersSecurity",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "owner security에 조건과 flipped 상태를 만족하는 카드가 있는지 판정하는 helper 계약",
            "face-up security provider와 security 조건부 효과 availability에 필요하다.",
            "Player.SecurityCards, CardSource.IsFlipped",
            "Partial",
            "원본 기본값 flipped=true를 보존하고 owner security만 조회한다.",
            "face-down security를 기본적으로 공개 조건으로 계산하지 않는다.",
            "face-up/face-down security predicate fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.MatchConditionOwnersCardCountInTrash",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "owner trash에서 조건을 만족하는 카드 수를 계산하는 helper 계약",
            "trash 수량 조건과 효과량 산정에 필요하다.",
            "Player.TrashCards query/count facade",
            "Partial",
            "owner trash predicate count를 공통 facade로 제공한다.",
            "trash zone list를 카드별 body에서 직접 수정하거나 중복 계산하지 않는다.",
            "owner trash count fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.MatchConditionOpponentsCardCountInTrash",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "opponent trash에서 조건을 만족하는 카드 수를 계산하는 helper 계약",
            "opponent trash 조건부 효과와 수량 scaling에 필요하다.",
            "Player.Enemy.TrashCards query/count facade",
            "Partial",
            "opponent trash predicate count를 공통 facade로 제공한다.",
            "owner trash와 opponent trash 범위를 혼동하지 않는다.",
            "opponent trash count fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.HasMatchConditionOwnersCardInTrash",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "owner trash에 조건을 만족하는 카드가 있는지 판정하는 helper 계약",
            "trash 조건부 trigger/effect availability에 필요하다.",
            "Player.TrashCards query facade",
            "Partial",
            "owner trash count helper 위에 existence helper를 제공한다.",
            "silent no-op으로 조건 실패를 감추지 않는다.",
            "owner trash existence fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.HasMatchConditionOpponentsCardInTrash",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons/GameContextDeterminarion.cs",
            "opponent trash에 조건을 만족하는 카드가 있는지 판정하는 helper 계약",
            "opponent trash 조건부 trigger/effect availability에 필요하다.",
            "Player.Enemy.TrashCards query facade",
            "Partial",
            "opponent trash count helper 위에 existence helper를 제공한다.",
            "owner/opponent 기준을 local player 기준으로 바꾸지 않는다.",
            "opponent trash existence fixture 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.IsOwnerTurn",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
            "효과 source owner가 현재 turn player인지 판정하는 helper 계약",
            "turn-limited trigger와 main phase 조건에서 필요하다.",
            "GameState.TurnPlayer, EffectContext.Controller",
            "Partial",
            "owner/controller/turn player 비교를 effect context helper로 제공한다.",
            "Current player를 UI/local player와 혼동하지 않는다.",
            "first/second player 전환 후 owner turn 조건 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.CanTriggerWhenDigivolving",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
            "진화 시점에 특정 enter-field 계열 효과가 발동 가능한지 판정하는 계약",
            "OnEnterFieldAnyone과 WhenDigivolving의 원본 연결을 재현하는 데 필요하다.",
            "TriggerCollector, DigivolveService, EnterFieldEventPayload",
            "Partial",
            "digivolve payload와 trigger condition을 분리해 수집 조건을 고정한다.",
            "진화 효과를 일반 OnPlay 효과로 대체하지 않는다.",
            "digivolve action에서 enter/digivolve 조건이 모두 올바르게 수집되는지 검증",
            "Tier0",
        ),
        (
            "CardEffectCommons.CanTriggerOnPlay",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
            "play action으로 field에 나온 경우 trigger 가능한지 판정하는 계약",
            "On Play 효과와 다른 enter field 효과를 구분하기 위해 필요하다.",
            "PlayCardService, EnterFieldEventPayload",
            "Partial",
            "source action이 play인지 payload에 보존하고 trigger condition에서 사용한다.",
            "hand에서 field로 이동했다는 사실만으로 play trigger를 켜지 않는다.",
            "effect play, normal play, security play 케이스별 trigger 검증",
            "Tier1",
        ),
        (
            "CardEffectCommons.CanTriggerOnAttack",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
            "공격 중 특정 효과가 발동 가능한지 판정하는 계약",
            "attack source/target과 attack effect의 유효성 판정에 필요하다.",
            "AttackRuntimeContext, AttackService",
            "Partial",
            "attack context를 effect condition에 전달하는 계약을 둔다.",
            "전역 IsAttacking flag만 보고 카드별 조건을 통과시키지 않는다.",
            "target 변경/attack end 후 조건 결과 검증",
            "Tier2",
        ),
        (
            "CardEffectCommons.PlayPermanentCards",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
            "효과로 카드를 permanent로 내는 coroutine helper 계약",
            "play without paying cost, token play, source play 효과의 공통 실행 기반이다.",
            "Tier1PrimitiveService, PlayCardService, ZoneMover",
            "Partial",
            "payCost/isTapped/root/enter-field trigger 옵션을 primitive command로 분해한다.",
            "카드를 field list에 직접 추가하지 않는다.",
            "효과 플레이가 cost, tapped, OnEnterField trigger를 올바르게 처리하는지 검증",
            "Tier1",
        ),
        (
            "CardEffectCommons.CanPlayAsNewPermanent",
            "C. CardEffectCommons foundation",
            "DCGO/Assets/Scripts/Script/CardEffectCommons.cs",
            "카드를 새 permanent로 낼 수 있는지 확인하는 계약",
            "field slot, cost, restriction, breeding/battle area 조건을 실행 전 검증한다.",
            "StaticRequirementService, CostResolver, PlayCardService",
            "Partial",
            "play permission/restriction/cost modifier를 service에서 모아 판단한다.",
            "불가능한 play를 silent no-op하지 않는다.",
            "cannot play option, field full, cost modifier 케이스 검증",
            "Tier1",
        ),
        (
            "CardEffectFactory.AddSelfDigivolutionRequirementStaticEffect",
            "D. CardEffectFactory foundation",
            "DCGO/Assets/Scripts/Script/CardEffectFactory/**/*.cs",
            "자기 카드의 진화 조건을 추가/변경하는 정적 효과 factory 계약",
            "진화 조건 변경류 카드 효과를 descriptor로 표현하려면 필요하다.",
            "StaticRequirementService, EffectDescriptor, ContinuousEffectDescriptor",
            "Partial",
            "factory 호출을 static requirement descriptor로 변환하는 facade와 mapping을 문서화한다.",
            "진화 가능 여부를 카드별 LegalActionGenerator 예외로 박아 넣지 않는다.",
            "추가 진화 조건이 legal action 생성에 반영되는지 검증",
            "Tier0",
        ),
        (
            "SelectCardEffect.Root",
            "E. Selection / decision foundation",
            "DCGO/Assets/Scripts/Script/SelectCardEffect.cs",
            "카드 선택 UI의 선택 root/source 범위를 나타내는 enum 계약",
            "hand/trash/security/source 등 어느 zone에서 고를지 표현해야 한다.",
            "SelectionRequest, SelectableTarget, DecisionPoint",
            "Partial",
            "Headless `SelectCardEffect` facade maps Hand/Trash/Security/Executing/Inherited/Linked roots to SelectionRequest candidates; remaining work: broader Custom/Library visibility and unsupported Clock parity.",
            "Unity panel root 값을 headless UI 상태로 보존하지 않는다.",
            "root별 후보 zone이 정확히 생성되는지 검증",
            "Tier1",
        ),
        (
            "SelectCardEffect.Mode",
            "E. Selection / decision foundation",
            "DCGO/Assets/Scripts/Script/SelectCardEffect.cs",
            "카드 선택 방식, 메시지, 수량, 필터를 조합하는 원본 선택 계약",
            "CardEffect 구현자가 target selection을 공통 흐름으로 넘길 때 필요하다.",
            "SelectionRequest, SelectionValidator, SelectionResultApplicator",
            "Partial",
            "Headless `SelectCardEffect` facade preserves Mode names and maps max/skip/end-not-max to SelectionRequest bounds; AddHand/Discard continuations and explicit PlayForFree primitive placement use Tier1 primitives for supported zones. Remaining work: full PlayForFree/PlayForCost lifecycle triggers/cost handling and broader AddHand source/permanent-linked card parity.",
            "Unity 표시 메시지를 게임 규칙 조건으로 사용하지 않는다.",
            "min/max/skip/target count selection validation 검증",
            "Tier1",
        ),
        (
            "SelectPermanentEffect.Mode",
            "E. Selection / decision foundation",
            "DCGO/Assets/Scripts/Script/SelectPermanentEffect.cs",
            "permanent 선택 방식, 필터, 수량을 표현하는 원본 선택 계약",
            "battle area target, opponent target, own target 선택 효과에 필요하다.",
            "SelectionRequest, SelectableTarget.Permanent, SelectionResultApplicator",
            "Partial",
            "Headless `SelectPermanentEffect` facade maps Mode names, permanent filters, and max/skip/end-not-max to permanent SelectionRequest candidates; Destroy/Tap/UnTap continuations use Tier1 primitives. Remaining work: Bounce/PutLibrary/PutSecurity/Degenerate/Attack continuations and defender/degeneration flags.",
            "클릭/outline 상태를 규칙 상태로 저장하지 않는다.",
            "opponent/owner permanent target selection과 fizzle 처리 검증",
            "Tier1",
        ),
        (
            "SelectCountEffect.SetCandidates",
            "E. Selection / decision foundation",
            "DCGO/Assets/Scripts/Script/SelectCountEffect.cs",
            "count 선택 후보 목록과 prefer-min/max 보조 설정 계약",
            "cost, source 수, 원하는 장수 선택 효과가 Unity command panel 없이 deterministic count decision으로 멈출 수 있어야 한다.",
            "SelectionRequest, SelectionValidator, DecisionPoint",
            "Partial",
            "Headless `SelectCountEffect` facade creates count SelectionRequest candidates and validator rejects counts outside explicit candidates; remaining work: auto prefer-min/max policy service integration.",
            "ContinuousController 자동 선택 값을 RL.Engine 전역 singleton으로 복제하지 않는다.",
            "custom count candidates와 invalid count reject 검증",
            "Tier1",
        ),
        (
            "ContinuousController.instance.StartCoroutine",
            "F. Execution flow foundation",
            "DCGO/Assets/Scripts/Script/**/*.cs",
            "Unity coroutine으로 효과 실행 단계를 순차 실행하는 계약",
            "대부분의 원본 CardEffect body가 yield 흐름으로 selection과 primitive를 연결한다.",
            "EffectResolution, EffectStep, TriggerPipelineService, pending SelectionRequest",
            "UnityOnly",
            "즉시 실행 가능한 step과 pending decision step을 deterministic resolution 흐름으로 대체한다.",
            "RL.Engine에 Coroutine/MonoBehaviour/StartCoroutine 의존성을 넣지 않는다.",
            "selection pending/resume 후 state hash가 deterministic한지 검증",
            "Tier1",
        ),
        (
            "GManager.instance.GetComponent",
            "F. Execution flow foundation",
            "DCGO/Assets/Scripts/Script/**/*.cs",
            "Unity scene singleton에서 service/component를 찾아 쓰는 계약",
            "원본 효과가 selection, controller, process service에 접근할 때 자주 사용한다.",
            "BattleEngineServices, explicit service dependencies, EffectContext",
            "UnityOnly",
            "필요한 service를 context/dependency로 명시 주입하고 singleton lookup을 제거한다.",
            "전역 singleton이나 service locator를 RL.Engine에 복제하지 않는다.",
            "각 effect resolution이 필요한 service를 생성자/context로 받는지 구조 검증",
            "Tier1",
        ),
        (
            "GManager.instance.userSelectionManager",
            "E. Selection / decision foundation",
            "DCGO/Assets/Scripts/Script/**/*.cs",
            "Unity 사용자 선택 UI를 시작하고 결과를 받는 manager 접근 계약",
            "선택이 필요한 CardEffect를 headless decision flow로 끊어야 한다.",
            "DecisionPoint, SelectionRequest, SelectionResult, IDecisionProvider",
            "UnityOnly",
            "selection은 request를 반환하고 외부 agent/CLI/test provider가 SelectionResult를 공급하는 방식으로 대체한다.",
            "engine 내부에서 UI 클릭이나 blocking input을 기다리지 않는다.",
            "pending SelectionRequest 생성과 SelectionResult resume 검증",
            "Tier1",
        ),
    ]

    candidates = []
    runtime_facade_contracts = {
        "CardSource.Owner",
        "CardSource.PermanentOfThisCard",
        "CardSource.IsDigimon",
        "CardSource.IsTamer",
        "CardSource.Level",
        "CardSource.CardColors",
        "CardSource.CardNames",
        "CardSource.CardTraits",
        "Permanent.TopCard",
        "Permanent.DigivolutionCards",
        "Player.HandCards",
        "Player.Enemy",
    }
    effect_runtime_contracts = {
        "ICardEffect.SetUpICardEffect",
        "ICardEffect.SetHashString",
        "ICardEffect.SetIsInheritedEffect",
        "CEntity_Effect.CardEffects",
        "CEntity_EffectController.AddCardEffect",
    }
    cardeffect_commons_query_contracts = {
        "CardEffectCommons.IsExistOnBattleArea",
        "CardEffectCommons.IsExistOnBattleAreaDigimon",
        "CardEffectCommons.IsExistOnFieldTrigger",
        "CardEffectCommons.IsExistOnBreedingAreaTrigger",
        "CardEffectCommons.IsExistOnBattleAreaTrigger",
        "CardEffectCommons.IsExistOnBattleAreaDigimonTrigger",
        "CardEffectCommons.IsExistDigivolutionCardsTrigger",
        "CardEffectCommons.IsExistLinkedTrigger",
        "CardEffectCommons.IsExistOnFieldActivate",
        "CardEffectCommons.IsExistOnBreedingAreaActivate",
        "CardEffectCommons.IsExistOnBattleAreaActivate",
        "CardEffectCommons.IsExistOnBattleAreaDigimonActivate",
        "CardEffectCommons.IsExistDigivolutionCardsActivate",
        "CardEffectCommons.IsExistLinkedActivate",
        "CardEffectCommons.CanPlayAsNewPermanent",
        "CardEffectCommons.IsExistInSecurity",
        "CardEffectCommons.IsExistInAnyTrash",
        "CardEffectCommons.HasMatchConditionPermanent",
        "CardEffectCommons.MatchConditionPermanentCount",
        "CardEffectCommons.HasMatchConditionOwnersHand",
        "CardEffectCommons.HasMatchConditionOwnersBreedingPermanent",
        "CardEffectCommons.GetUniqueColourCountOnOwnerBattleArea",
        "CardEffectCommons.GetUniqueColourCountOnOpponentsBattleArea",
        "CardEffectCommons.IsPermanentExistsOnField",
        "CardEffectCommons.IsPermanentExistsOnBattleArea",
        "CardEffectCommons.IsPermanentExistsOnBreedingArea",
        "CardEffectCommons.IsPermanentExistsOnOwnerBattleArea",
        "CardEffectCommons.IsPermanentExistsOnOpponentBattleArea",
        "CardEffectCommons.IsPermanentExistsOnOwnerBreedingArea",
        "CardEffectCommons.IsPermanentExistsOnOpponentBreedingArea",
        "CardEffectCommons.IsPermanentExistsOnBattleAreaDigimon",
        "CardEffectCommons.IsPermanentExistsOnOwnerBattleAreaDigimon",
        "CardEffectCommons.IsPermanentExistsOnOpponentBattleAreaDigimon",
        "CardEffectCommons.IsPermanentExistsOnBattleAreaTamer",
        "CardEffectCommons.IsPermanentExistsOnOwnerBattleAreaTamer",
        "CardEffectCommons.IsPermanentExistsOnOpponentBattleAreaTamer",
        "CardEffectCommons.HasMatchConditionPermanentDigivolutionCards",
        "CardEffectCommons.HasMatchConditionOwnersSecurity",
        "CardEffectCommons.MatchConditionOwnersCardCountInTrash",
        "CardEffectCommons.MatchConditionOpponentsCardCountInTrash",
        "CardEffectCommons.HasMatchConditionOwnersCardInTrash",
        "CardEffectCommons.HasMatchConditionOpponentsCardInTrash",
        "CardEffectCommons.IsOwnerTurn",
    }
    cardeffect_commons_query_required_work = {
        "CardEffectCommons.IsExistOnBattleArea": (
            "Headless `CardEffectCommons` facade maps field/battle/breeding/source/linked presence checks to "
            "`CardSource.PermanentOfThisCard` and read-only `Player` field queries. Remaining work: broader "
            "trigger/activate permanence snapshot parity for stale permanent checks."
        ),
        "CardEffectCommons.IsExistOnBattleAreaDigimon": (
            "Headless `CardEffectCommons` facade combines battle-area presence with `Permanent.IsDigimon` from "
            "the read-only runtime facade. Remaining work: broader type predicates affected by future metadata "
            "modifiers."
        ),
        "CardEffectCommons.HasMatchConditionPermanent": (
            "Headless `CardEffectCommons` facade provides explicit `GameState` overloads plus owner/opponent "
            "`CardSource` overloads; the original no-context signature fails explicitly instead of reading a "
            "`GManager` singleton. Remaining work: migrate card ports to explicit state/context forms."
        ),
        "CardEffectCommons.MatchConditionPermanentCount": (
            "Headless `CardEffectCommons` facade counts battle-area permanents deterministically, with an "
            "explicit include-breeding flag and owner/opponent `CardSource` overloads; the original no-context "
            "signature fails explicitly. Hand/security/trash/source existence and count helpers now cover the "
            "first original collection-query contracts. Remaining work: broader source collection count variants "
            "and trigger-specific snapshots."
        ),
        "CardEffectCommons.IsExistInSecurity": (
            "Headless `CardEffectCommons` facade checks owner security membership plus `CardSource.IsFlipped`, "
            "preserving the original `isFlipped=false` default. Remaining work: security look/peek visibility "
            "parity for effects that temporarily reveal cards."
        ),
        "CardEffectCommons.IsExistInAnyTrash": (
            "Headless `CardEffectCommons` facade checks trash membership through owner/opponent `Player` "
            "facades without using a global context. Remaining work: movement-event payload parity for cards "
            "that just entered trash."
        ),
        "CardEffectCommons.HasMatchConditionOwnersHand": (
            "Headless `CardEffectCommons` facade provides owner hand existence and count helpers over "
            "`Player.HandCards`. Remaining work: selection visibility policies for hidden hand information."
        ),
        "CardEffectCommons.HasMatchConditionOwnersBreedingPermanent": (
            "Headless `CardEffectCommons` facade queries owner breeding permanents through "
            "`Player.GetBreedingAreaPermanents` without reading a global `GameContext`. Remaining work: "
            "breeding-area trigger payload parity for move-from-breeding and evolution edge cases."
        ),
        "CardEffectCommons.GetUniqueColourCountOnOwnerBattleArea": (
            "Headless `CardEffectCommons` facade counts distinct `CardSource.CardColors` from owner battle-area "
            "top cards after applying the supplied permanent predicate. Remaining work: integrate effective "
            "static card-color layers where the original `CardSource.CardColors` would include color-changing "
            "continuous effects."
        ),
        "CardEffectCommons.GetUniqueColourCountOnOpponentsBattleArea": (
            "Headless `CardEffectCommons` facade counts distinct `CardSource.CardColors` from opponent "
            "battle-area top cards after applying the supplied permanent predicate. Remaining work: integrate "
            "effective static card-color layers where the original `CardSource.CardColors` would include "
            "color-changing continuous effects."
        ),
        "CardEffectCommons.CanPlayAsNewPermanent": (
            "Headless `CardEffectCommons` facade validates the original empty-frame play predicate through "
            "`CardSource` root membership, option gating, battle/breeding slot availability, basic playable "
            "cost metadata, max-payable memory cost, fixed cost overrides, static play-cost modifiers, option "
            "color requirements, and optional `StaticEffectService` restrictions. Remaining work: full "
            "`BeforePayCost`/`AfterPayCost` timing windows, DigiXros/Assembly selected-material exclusion, "
            "and effect-specific `CanEnterField` replacement layers."
        ),
        "CardEffectCommons.IsPermanentExistsOnField": (
            "Headless `CardEffectCommons` facade checks battle and breeding area membership through the "
            "permanent top card owner and compares `PermanentId` instead of relying on Unity object identity. "
            "Remaining work: controller-vs-owner parity for stolen/control-changed permanents."
        ),
        "CardEffectCommons.IsPermanentExistsOnBattleArea": (
            "Headless `CardEffectCommons` facade checks battle area membership through the permanent top card "
            "owner and compares `PermanentId` instead of relying on Unity object identity. Remaining work: "
            "controller-vs-owner parity for stolen/control-changed permanents."
        ),
        "CardEffectCommons.IsPermanentExistsOnBreedingArea": (
            "Headless `CardEffectCommons` facade checks breeding area membership through the permanent top card "
            "owner and compares `PermanentId` instead of relying on Unity object identity. Remaining work: "
            "move-from-breeding payload parity."
        ),
        "CardEffectCommons.IsPermanentExistsOnOwnerBattleArea": (
            "Headless `CardEffectCommons` facade composes battle area membership with `IsOwnerPermanent`. "
            "Remaining work: controller-vs-owner parity for stolen/control-changed permanents."
        ),
        "CardEffectCommons.IsPermanentExistsOnOpponentBattleArea": (
            "Headless `CardEffectCommons` facade composes battle area membership with `IsOpponentPermanent`. "
            "Remaining work: controller-vs-owner parity for stolen/control-changed permanents."
        ),
        "CardEffectCommons.IsPermanentExistsOnOwnerBreedingArea": (
            "Headless `CardEffectCommons` facade composes breeding area membership with `IsOwnerPermanent`. "
            "Remaining work: move-from-breeding and source snapshot parity."
        ),
        "CardEffectCommons.IsPermanentExistsOnOpponentBreedingArea": (
            "Headless `CardEffectCommons` facade composes breeding area membership with `IsOpponentPermanent`. "
            "Remaining work: move-from-breeding and source snapshot parity."
        ),
        "CardEffectCommons.IsPermanentExistsOnBattleAreaDigimon": (
            "Headless `CardEffectCommons` facade composes battle area membership with `Permanent.IsDigimon`. "
            "Remaining work: effective type changes from static metadata layers."
        ),
        "CardEffectCommons.IsPermanentExistsOnOwnerBattleAreaDigimon": (
            "Headless `CardEffectCommons` facade composes owner battle area membership with `Permanent.IsDigimon`. "
            "Remaining work: effective type changes and controller-vs-owner parity."
        ),
        "CardEffectCommons.IsPermanentExistsOnOpponentBattleAreaDigimon": (
            "Headless `CardEffectCommons` facade composes opponent battle area membership with `Permanent.IsDigimon`. "
            "Remaining work: effective type changes and controller-vs-owner parity."
        ),
        "CardEffectCommons.IsPermanentExistsOnBattleAreaTamer": (
            "Headless `CardEffectCommons` facade composes battle area membership with `Permanent.IsTamer`. "
            "Remaining work: effective type changes from static metadata layers."
        ),
        "CardEffectCommons.IsPermanentExistsOnOwnerBattleAreaTamer": (
            "Headless `CardEffectCommons` facade composes owner battle area membership with `Permanent.IsTamer`. "
            "Remaining work: effective type changes and controller-vs-owner parity."
        ),
        "CardEffectCommons.IsPermanentExistsOnOpponentBattleAreaTamer": (
            "Headless `CardEffectCommons` facade composes opponent battle area membership with `Permanent.IsTamer`. "
            "Remaining work: effective type changes and controller-vs-owner parity."
        ),
        "CardEffectCommons.HasMatchConditionPermanentDigivolutionCards": (
            "Headless `CardEffectCommons` facade queries a card's current permanent source stack through "
            "`Permanent.DigivolutionCards`. Remaining work: stale permanent snapshot parity for source cards "
            "removed during pending selections."
        ),
        "CardEffectCommons.HasMatchConditionOwnersSecurity": (
            "Headless `CardEffectCommons` facade queries owner security cards with the original `flipped=true` "
            "default for face-up security providers. Remaining work: richer security visibility and reveal "
            "window semantics."
        ),
        "CardEffectCommons.MatchConditionOwnersCardCountInTrash": (
            "Headless `CardEffectCommons` facade counts owner trash cards through `Player.TrashCards`. "
            "Remaining work: trash ordering/top-card event helpers."
        ),
        "CardEffectCommons.MatchConditionOpponentsCardCountInTrash": (
            "Headless `CardEffectCommons` facade counts opponent trash cards through `Player.Enemy.TrashCards`. "
            "Remaining work: trash ordering/top-card event helpers."
        ),
        "CardEffectCommons.HasMatchConditionOwnersCardInTrash": (
            "Headless `CardEffectCommons` facade exposes owner-trash existence via the shared count helper. "
            "Remaining work: trash movement trigger payload parity."
        ),
        "CardEffectCommons.HasMatchConditionOpponentsCardInTrash": (
            "Headless `CardEffectCommons` facade exposes opponent-trash existence via the shared count helper. "
            "Remaining work: trash movement trigger payload parity."
        ),
        "CardEffectCommons.IsOwnerTurn": (
            "Headless `CardEffectCommons` facade compares `CardSource.Owner` to `GameState.TurnPlayerId` without "
            "UI/local-player state. Remaining work: controller-vs-owner edge cases for stolen permanents."
        ),
    }
    for contract in (
        "CardEffectCommons.IsExistOnFieldTrigger",
        "CardEffectCommons.IsExistOnBreedingAreaTrigger",
        "CardEffectCommons.IsExistOnBattleAreaTrigger",
        "CardEffectCommons.IsExistOnBattleAreaDigimonTrigger",
        "CardEffectCommons.IsExistDigivolutionCardsTrigger",
        "CardEffectCommons.IsExistLinkedTrigger",
        "CardEffectCommons.IsExistOnFieldActivate",
        "CardEffectCommons.IsExistOnBreedingAreaActivate",
        "CardEffectCommons.IsExistOnBattleAreaActivate",
        "CardEffectCommons.IsExistOnBattleAreaDigimonActivate",
        "CardEffectCommons.IsExistDigivolutionCardsActivate",
        "CardEffectCommons.IsExistLinkedActivate",
    ):
        cardeffect_commons_query_required_work[contract] = (
            "Headless `CardEffectCommons` facade captures the trigger-time source permanent on "
            "`ICardEffect.EffectSourcePermanent` and `ActivateICardEffect.PermanentWhenTriggered`, then compares "
            "the activate-time current permanent by `PermanentId`. Remaining work: integrate this facade with "
            "broader pending/resume fizzle paths and stale source movement payloads."
        )
    effect_runtime_required_work = {
        "ICardEffect.SetUpICardEffect": (
            "Descriptor bridge captures source card, source permanent, controller, once-per-turn key, "
            "and `TriggerSourceSnapshot`; pending selection resume revalidates that snapshot and skips "
            "stale sources after later zone moves, and a direct ScriptRuntime selection-continuation "
            "fixture validates pending/resume through the descriptor bridge. Remaining work: broader "
            "direct descriptor parity for optional/count/card-root selection forms."
        ),
        "ICardEffect.SetHashString": (
            "Descriptor bridge maps `HashString` to `EffectDescriptor.StableId` and once-per-turn keys; "
            "ScriptRuntime selection descriptor helper derives explicit selection request ids from the same "
            "stable identity. Remaining work: non-ICardEffect direct script request-id audit and DecisionToken "
            "policy documentation."
        ),
        "ICardEffect.SetIsInheritedEffect": (
            "`SetIsInheritedEffect(true)`, `SetIsLinkedEffect(true)`, and `SetIsSecurityEffect(true)` map to "
            "Inherited/Linked/FaceUpSecurity source snapshots and validate source zone membership plus face-up "
            "security state; hand/trash/executing snapshots derive from owner player-zone membership and fail on "
            "mismatched zone lists; pending selection resume revalidates stale snapshots before applying selection. "
            "Remaining work: broader explicit-source mismatch coverage."
        ),
        "CEntity_Effect.CardEffects": (
            "`CardEffects(timing, cardSource)` is bridged to descriptor creation and null filtering; "
            "`CEntity_EffectController.AddCardEffect` now owns class-name lookup/fallback, and "
            "`CEntity_EffectController.GetCardEffects` expands `IAddSkillEffect` providers from field top, "
            "inherited source, and face-up security sources while respecting collected `ICanNotAffectedEffect` "
            "provider gates for that added-effect path. Remaining work: player-level effect providers, broad "
            "primitive target-immunity integration, and duration-scoped grants must be mapped to "
            "`TemporaryGrantedEffect` descriptors."
        ),
        "CEntity_EffectController.AddCardEffect": (
            "`AddCardEffect(ID, ClassName)` resolves direct class names, `DCGO.CardEffects.{IDPrefix}` names, "
            "and token names through an injected headless registry; empty class names map to `EmptyEffectClass`, "
            "while missing non-empty class names fail explicitly. Remaining work: asset registry integration should "
            "feed the controller registry instead of card bodies doing ad hoc lookup."
        ),
    }
    effect_runtime_evidence = {
        "CEntity_Effect.CardEffects": (
            "src/DCGO.RL.Engine/Effects/ScriptRuntimeEffectFoundation.cs; "
            "ScriptRuntimeEffectControllerAppliesAddedSkillEffects; "
            "ScriptRuntimeEffectControllerRespectsCannotAffectedProviders"
        ),
        "CEntity_EffectController.AddCardEffect": (
            "src/DCGO.RL.Engine/Effects/ScriptRuntimeEffectFoundation.cs; "
            "ScriptRuntimeEffectControllerResolvesClassNames"
        ),
    }
    cardeffect_factory_required_work = {
        "CardEffectFactory.AddSelfDigivolutionRequirementStaticEffect": (
            "Headless `CardEffectFactory` facade maps the original self digivolution requirement factory call "
            "to `StaticEvolutionRequirementDescriptor`, preserving source card/controller, fixed cost, cost "
            "equation, color, exact level, min/max level, target condition, source/card condition, and ignore "
            "permission flags. Exact `level` takes precedence over min/max bounds to match the original "
            "`GetEvoCost` branch. It also maps supported original static keyword factory shapes (`Blocker`, "
            "`Rush`, `Reboot`, `Collision`, `Jamming`) to `ContinuousKeywordDescriptor` while preserving source "
            "card/permanent/controller, target kind, condition, metadata gates, and stable IDs; unsupported "
            "trigger/process keyword shapes fail explicitly instead of being silently mapped to static descriptors. "
            "It maps the first original DP static factory shapes to `ContinuousEffectDescriptor` with fixed or "
            "dynamic DP amount delegates, preserving source card/permanent/controller, target kind, condition, "
            "metadata gates, and stable IDs; fixed zero DP changes fail explicitly instead of producing a silent "
            "no-op descriptor. Remaining work: broaden factory facade coverage to trigger/process keyword shapes "
            "such as `Pierce` and `Retaliation`, play/cost restriction, and additional digivolution/link "
            "requirement helpers."
        ),
    }
    cardeffect_factory_evidence = {
        "CardEffectFactory.AddSelfDigivolutionRequirementStaticEffect": (
            "src/DCGO.RL.Engine/CardEffects/CardEffectFactory.cs; "
            "CardEffectFactorySelfEvolutionRequirementMapsToStaticDescriptor; "
            "CardEffectFactoryBlockerStaticEffectMapsToKeywordDescriptor; "
            "CardEffectFactoryStaticKeywordWrappersMapSupportedKeywords; "
            "CardEffectFactoryKeywordStaticEffectRejectsUnsupportedKeywordShape; "
            "CardEffectFactoryDpStaticEffectMapsToContinuousDescriptor; "
            "CardEffectFactoryDpStaticEffectRejectsZeroAmount"
        ),
    }

    for row in rows:
        contract = row[0]
        metric = metric_for(api_records, contract)
        candidate = {
            "originalCallContract": contract,
            "group": row[1],
            "originalSource": row[2],
            "originalMeaning": row[3],
            "whyCardEffectNeedsIt": row[4],
            "headlessCounterpart": row[5],
            "status": row[6],
            "requiredWork": row[7],
            "forbiddenShortcut": row[8],
            "verificationCandidate": row[9],
            "priority": row[10],
            "referenceCount": metric["referenceCount"],
            "cardEffectFileCount": metric["cardEffectFileCount"],
            "implementationEvidence": "",
        }

        if contract in runtime_facade_contracts:
            candidate["headlessCounterpart"] = f"{row[5]}; Domain/ScriptRuntimeFoundation.cs read-only facade"
            candidate["requiredWork"] = f"Read-only facade exists in `ScriptRuntimeFoundation.cs`; remaining work: {row[7]}"
            candidate["implementationEvidence"] = "src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs; ScriptRuntimeStateFacadeExposesOriginalContracts"

        if contract in effect_runtime_contracts:
            candidate["headlessCounterpart"] = f"{row[5]}; Effects/ScriptRuntimeEffectFoundation.cs descriptor bridge"
            if candidate["status"] == "Missing":
                candidate["status"] = "Partial"
            candidate["requiredWork"] = effect_runtime_required_work[contract]
            candidate["implementationEvidence"] = effect_runtime_evidence.get(
                contract,
                "src/DCGO.RL.Engine/Effects/ScriptRuntimeEffectFoundation.cs; ScriptRuntimeEffectModelCreatesDescriptors")

        if contract in cardeffect_commons_query_contracts:
            candidate["headlessCounterpart"] = f"{row[5]}; CardEffects/CardEffectCommons.cs query facade"
            candidate["status"] = "Partial"
            candidate["requiredWork"] = cardeffect_commons_query_required_work[contract]
            candidate["implementationEvidence"] = (
                "src/DCGO.RL.Engine/CardEffects/CardEffectCommons.cs; "
                "CardEffectCommonsQueryFacadeMatchesOriginalContracts")

        if contract in cardeffect_factory_required_work:
            candidate["headlessCounterpart"] = f"{row[5]}; CardEffects/CardEffectFactory.cs descriptor facade"
            candidate["status"] = "Partial"
            candidate["requiredWork"] = cardeffect_factory_required_work[contract]
            candidate["implementationEvidence"] = cardeffect_factory_evidence[contract]

        candidates.append(candidate)
    return candidates


def build_script_runtime_implementation_units() -> list[dict]:
    return [
        {
            "implementationUnit": "CardSource/Permanent/Player facade contract",
            "group": "B. Runtime state foundation",
            "priority": "Tier0",
            "neededBeforeLocalLlmCardEffectWork": "Yes",
            "dependsOn": "GameState, CardInstance, PermanentState, PlayerState",
            "suggestedFiles": "src/DCGO.RL.Engine/Domain/*, docs/rl-engine/script-runtime-foundation-contract.md",
            "doneCondition": "Owner, Enemy, TopCard, DigivolutionCards, Hand/Security/Trash read 계약과 이동 금지 규칙이 테스트 후보까지 정리됨",
        },
        {
            "implementationUnit": "ICardEffect/CEntity_Effect compatibility layer",
            "group": "A. Effect model foundation",
            "priority": "Tier0",
            "neededBeforeLocalLlmCardEffectWork": "Yes",
            "dependsOn": "EffectDescriptor, TriggerSourceSnapshot",
            "suggestedFiles": "src/DCGO.RL.Engine/Effects/*",
            "doneCondition": "CardEffects(timing, cardSource) 반환 의미가 descriptor factory 계약으로 대응됨",
        },
        {
            "implementationUnit": "CardEffectInterfaces mapping review",
            "group": "A. Effect model foundation",
            "priority": "Tier0",
            "neededBeforeLocalLlmCardEffectWork": "Yes",
            "dependsOn": "EffectDescriptor, StaticEffectService, BattleKeywordService",
            "suggestedFiles": "docs/as-is/cardeffect-interface-mapping.md, docs/generated/as-is/cardeffect_interface_mapping.json",
            "doneCondition": "IAddSkillEffect, IChangeDPEffect, ICanNotPlayCardEffect, keyword interfaces가 Exclude가 아니라 mapping 상태로 분류됨",
        },
        {
            "implementationUnit": "CardEffectCommons top helper mapping",
            "group": "C. CardEffectCommons foundation",
            "priority": "Tier0",
            "neededBeforeLocalLlmCardEffectWork": "Yes",
            "dependsOn": "state facade, ZoneMover, Tier1PrimitiveService",
            "suggestedFiles": "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs, docs/rl-engine/tier1-primitives.md",
            "doneCondition": "상위 helper 30개가 primitive/service/status/unsupported 조건으로 분류됨",
        },
        {
            "implementationUnit": "CardEffectFactory top primitive mapping",
            "group": "D. CardEffectFactory foundation",
            "priority": "Tier0",
            "neededBeforeLocalLlmCardEffectWork": "Yes",
            "dependsOn": "EffectDescriptor, StaticRequirementService, ContinuousEffectService",
            "suggestedFiles": "src/DCGO.RL.Engine/Effects/*",
            "doneCondition": "진화 조건, DP, keyword, play/cost restriction factory가 descriptor 종류로 mapping됨",
        },
        {
            "implementationUnit": "SelectionRequest replacement contract",
            "group": "E. Selection / decision foundation",
            "priority": "Tier1",
            "neededBeforeLocalLlmCardEffectWork": "Yes",
            "dependsOn": "DecisionPoint, SelectionResult, SelectionResultApplicator",
            "suggestedFiles": "src/DCGO.RL.Engine/Decisions/*, src/DCGO.RL.Engine/Effects/SelectionResultApplicator.cs",
            "doneCondition": "SelectCard/Permanent/Count의 root/mode/count가 SelectionRequest와 검증 규칙으로 대응됨",
        },
        {
            "implementationUnit": "Coroutine replacement contract",
            "group": "F. Execution flow foundation",
            "priority": "Tier1",
            "neededBeforeLocalLlmCardEffectWork": "Yes",
            "dependsOn": "EffectResolution, TriggerPipelineService, EngineSession",
            "suggestedFiles": "src/DCGO.RL.Engine/Effects/*, src/DCGO.RL.Engine/Battle/EngineSession.cs",
            "doneCondition": "즉시 실행 step과 pending selection step의 resume 규칙이 deterministic하게 정리됨",
        },
        {
            "implementationUnit": "GManager replacement contract",
            "group": "F. Execution flow foundation",
            "priority": "Tier1",
            "neededBeforeLocalLlmCardEffectWork": "Yes",
            "dependsOn": "BattleEngineServices, EffectContext",
            "suggestedFiles": "src/DCGO.RL.Engine/Battle/BattleEngineServices.cs",
            "doneCondition": "singleton lookup이 명시 service/context dependency로 대체되는 규칙이 정리됨",
        },
        {
            "implementationUnit": "Attack timing foundation",
            "group": "F. Execution flow foundation",
            "priority": "Tier2",
            "neededBeforeLocalLlmCardEffectWork": "After Tier0/Tier1",
            "dependsOn": "AttackService, BattleRules, TriggerCollector",
            "suggestedFiles": "src/DCGO.RL.Engine/Battle/AttackService.cs",
            "doneCondition": "OnAllyAttack, counter, block, battle, cleanup timing의 source 대응이 정리됨",
        },
    ]


def render_script_runtime_foundation_contract(candidates: list[dict], implementation_units: list[dict], generated_at: str) -> str:
    group_rows = []
    for group in [
        "A. Effect model foundation",
        "B. Runtime state foundation",
        "C. CardEffectCommons foundation",
        "D. CardEffectFactory foundation",
        "E. Selection / decision foundation",
        "F. Execution flow foundation",
    ]:
        group_candidates = [c for c in candidates if c["group"] == group]
        group_rows.append(
            [
                group,
                len(group_candidates),
                ", ".join(sorted(set(c["priority"] for c in group_candidates))),
                "; ".join(c["originalCallContract"] for c in group_candidates[:6]) + (" ..." if len(group_candidates) > 6 else ""),
            ]
        )

    contract_rows = [
        [
            c["originalCallContract"],
            c["originalSource"],
            c["originalMeaning"],
            c["whyCardEffectNeedsIt"],
            c["headlessCounterpart"],
            c["status"],
            c["requiredWork"],
            c["forbiddenShortcut"],
            c["verificationCandidate"],
            c["priority"],
        ]
        for c in candidates
    ]

    unity_rows = [
        [
            c["originalCallContract"],
            c["headlessCounterpart"],
            c["requiredWork"],
            c["forbiddenShortcut"],
            c["verificationCandidate"],
        ]
        for c in candidates
        if c["status"] == "UnityOnly"
    ]

    tier0_rows = [
        [
            c["originalCallContract"],
            c["group"],
            c["status"],
            c["requiredWork"],
            c["verificationCandidate"],
        ]
        for c in candidates
        if c["priority"] == "Tier0"
    ]

    implementation_evidence_rows = [
        [
            c["originalCallContract"],
            c["group"],
            c["status"],
            c["implementationEvidence"],
        ]
        for c in candidates
        if c.get("implementationEvidence")
    ]

    return "\n\n".join(
        [
            "# Script Runtime Foundation Contract",
            f"- 생성 시각: `{generated_at}`",
            "- 목적: 카드별 `CardEffect` 구현 전에 필요한 원본 `Scripts/Script` 공통 호출 계약과 headless primitive/service 대응 계약을 정리한다.",
            "- 범위: `DCGO/Assets/Scripts/Script`는 계약 근거이며, `DCGO/Assets/Scripts/CardEffect`는 참조 빈도 확인용 읽기 전용 자료다.",
            "## Misinterpretation Guard",
            "- 이 문서에서 과거 AS-IS 문서의 `API`라는 표현은 외부 서버 계약이 아니라 공통 호출 계약을 뜻한다.",
            "- 이 문서에서는 가능하면 `공통 호출 계약`, `Script 런타임 기반`, `원본 Script 대응 계약`, `headless primitive/service 대응 계약`이라는 표현을 사용한다.",
            "- `CardEffect` 폴더는 구현 대상이 아니라 읽기 전용 참고 자료다.",
            "- Codex의 역할은 CardEffect 개별 구현이 아니라 Script 런타임 공통 기반 정리다.",
            "- 로컬 LLM/OpenCode가 나중에 CardEffect 구현을 담당한다.",
            "- headless RL.Engine에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 넣지 않는다.",
            "- 원본과 이름을 맞추더라도 내부 구현은 deterministic headless service/primitive로 연결한다.",
            "## 공통 호출 계약 그룹",
            md_table(["Group", "Contract count", "Priorities", "Examples"], group_rows),
            "## 현재 구현 감사 결과",
            md_table(["Contract", "Group", "Status", "Evidence"], implementation_evidence_rows),
            "## 계약 후보 표",
            md_table(
                [
                    "Original call contract",
                    "Original source",
                    "Original meaning",
                    "Why CardEffect needs it",
                    "Headless counterpart",
                    "Status",
                    "Required work",
                    "Forbidden shortcut",
                    "Verification candidate",
                    "Priority",
                ],
                contract_rows,
            ),
            "## Tier0 우선 작업 후보",
            md_table(["Contract", "Group", "Status", "Required work", "Verification candidate"], tier0_rows),
            "## UnityOnly 대체 패턴",
            md_table(
                ["Original Unity call", "Headless replacement", "Required work", "Forbidden shortcut", "Verification candidate"],
                unity_rows,
            ),
            "## 구현 단위 분리",
            md_table(
                [
                    "Implementation unit",
                    "Group",
                    "Priority",
                    "Needed before local LLM CardEffect work?",
                    "Depends on",
                    "Suggested files",
                    "Done condition",
                ],
                [
                    [
                        row["implementationUnit"],
                        row["group"],
                        row["priority"],
                        row["neededBeforeLocalLlmCardEffectWork"],
                        row["dependsOn"],
                        row["suggestedFiles"],
                        row["doneCondition"],
                    ]
                    for row in implementation_units
                ],
            ),
            "## 보정 항목",
            "- `CEntity_Effect.CardEffects`는 카드별 effect class의 `List<ICardEffect>` 반환 진입점이므로 `Tier0` 계약으로 보정한다.",
            "- `CardEffectInterfaces.cs`는 `Exclude` 확정 대상이 아니라 `EffectDescriptor`, primitive, static effect, condition contract와 연결될 수 있는 재검토 대상이다.",
            "- `ContinuousController.instance.StartCoroutine`, `GManager.instance.GetComponent`, `GManager.instance.userSelectionManager`, `Select*Effect`는 삭제 대상이 아니라 headless execution/decision 흐름으로 대체할 계약이다.",
            "## 다음 구현 단계 추천",
            "1. `CardSource/Permanent/Player` facade 계약과 읽기 전용 state helper를 먼저 고정한다.",
            "2. `CEntity_Effect.CardEffects`와 `ICardEffect` metadata를 `EffectDescriptor` 생성 계약으로 연결한다.",
            "3. `CardEffectCommons` 상위 helper 30개를 primitive/service/status/unsupported 조건으로 mapping한다.",
            "4. `SelectionRequest`와 coroutine replacement 계약을 테스트 가능한 pending/resume 흐름으로 정리한다.",
        ]
    ) + "\n"


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--source-root", type=Path, default=None)
    parser.add_argument("--workspace-root", type=Path, default=WORKSPACE_ROOT)
    args = parser.parse_args()

    source_root = args.source_root
    if source_root is None:
        local = WORKSPACE_ROOT / "DCGO"
        source_root = local if local.exists() else DEFAULT_FALLBACK_SOURCE
    source_root = source_root.resolve()

    script_root = source_root / SCRIPT_REL
    cardeffect_root = source_root / CARDEFFECT_REL
    if not script_root.exists():
        raise SystemExit(f"Script root not found: {script_root}")
    if not cardeffect_root.exists():
        raise SystemExit(f"CardEffect root not found: {cardeffect_root}")

    generated_at = datetime.now(timezone.utc).astimezone().isoformat(timespec="seconds")
    headless_files, headless_classes, _ = scan_headless()

    source_files = list_files(script_root, "*.cs")
    cardeffect_files = list_files(cardeffect_root, "*.cs")
    infos = [parse_csharp_file(path, source_root) for path in source_files]

    api_records, type_ref_files = scan_cardeffect_references(cardeffect_files, source_root, infos)
    for info in infos:
        names = set(info.classes + info.interfaces + info.enums)
        info.cardeffect_referenced = any(type_ref_files.get(name) for name in names)
        categorize_file(info)
        classify_headless_status(info, headless_files, headless_classes)

    assets_rows = build_assets_tree(source_root)
    playing_logic_rows = build_playing_logic_rows()
    foundation_items = build_foundation_work_items(infos)
    top_api_records = sorted(api_records.values(), key=lambda r: (-r["refCount"], -r["cardEffectFileCount"], r["api"]))[:50]
    script_runtime_contract_candidates = build_script_runtime_contract_candidates(api_records)
    script_runtime_implementation_units = build_script_runtime_implementation_units()
    cardeffect_interface_mappings = build_cardeffect_interface_mappings(source_root)

    as_is_dir = WORKSPACE_ROOT / "docs/as-is"
    generated_dir = WORKSPACE_ROOT / "docs/generated/as-is"
    as_is_dir.mkdir(parents=True, exist_ok=True)
    generated_dir.mkdir(parents=True, exist_ok=True)

    script_inventory = {
        "schemaVersion": 1,
        "generatedAt": generated_at,
        "sourceRoot": str(source_root),
        "scriptRoot": str(script_root),
        "fileCount": len(infos),
        "files": [info.__dict__ for info in sort_infos(infos)],
    }
    cardeffect_index = {
        "schemaVersion": 1,
        "generatedAt": generated_at,
        "sourceRoot": str(source_root),
        "cardEffectRoot": str(cardeffect_root),
        "cardEffectFileCount": len(cardeffect_files),
        "apiCount": len(api_records),
        "topApis": top_api_records,
        "allApis": sorted(api_records.values(), key=lambda r: (-r["refCount"], r["api"])),
    }
    gap_matrix = {
        "schemaVersion": 1,
        "generatedAt": generated_at,
        "sourceRoot": str(source_root),
        "rows": [
            {
                "originalFileClass": info.path,
                "originalResponsibility": info.category,
                "cardEffectFacingApi": info.cardeffect_referenced,
                "currentHeadlessCounterpart": info.current_headless_counterpart,
                "status": info.headless_status,
                "gap": info.gap,
                "priority": info.priority,
                "recommendedWorkItem": recommended_work_item(info),
            }
            for info in sort_infos(infos)
        ],
    }
    foundation_json = {
        "schemaVersion": 1,
        "generatedAt": generated_at,
        "sourceRoot": str(source_root),
        "workItems": foundation_items,
    }
    script_runtime_contract_json = {
        "schemaVersion": 1,
        "generatedAt": generated_at,
        "sourceRoot": str(source_root),
        "scope": {
            "workName": "Script Runtime Foundation Contract 정리",
            "cardEffectBodyImplementation": "forbidden",
            "cardEffectSourceUse": "read-only reference for common call contract discovery",
            "preferredTerminology": [
                "공통 호출 계약",
                "Script 런타임 기반",
                "CardEffect 구현 기반 공통 구조",
                "원본 Script 대응 계약",
                "headless primitive/service 대응 계약",
            ],
        },
        "corrections": [
            {
                "item": "CEntity_Effect.CardEffects",
                "correction": "Tier0",
                "reason": "원본 카드별 effect class가 timing/cardSource를 받아 List<ICardEffect>를 반환하는 필수 진입점이다.",
            },
            {
                "item": "CardEffectInterfaces.cs",
                "correction": "ReviewRequired",
                "reason": "effect interface들은 EffectDescriptor, primitive, static effect, condition contract와 연결될 수 있으므로 Exclude로 확정하지 않는다.",
            },
            {
                "item": "UnityOnly calls",
                "correction": "ReplacementPatternRequired",
                "reason": "Coroutine, GManager, Select*Effect 호출은 삭제가 아니라 deterministic execution/decision 흐름으로 대체한다.",
            },
        ],
        "contractCount": len(script_runtime_contract_candidates),
        "contracts": script_runtime_contract_candidates,
        "implementationUnits": script_runtime_implementation_units,
        "relatedGeneratedArtifacts": {
            "cardEffectInterfaceMapping": "docs/generated/as-is/cardeffect_interface_mapping.json",
            "cardEffectInterfaceMappingMarkdown": "docs/as-is/cardeffect-interface-mapping.md",
        },
    }
    cardeffect_interface_mapping_json = {
        "schemaVersion": 1,
        "generatedAt": generated_at,
        "sourceRoot": str(source_root),
        "sourceFile": "DCGO/Assets/Scripts/Script/CardEffectInterfaces.cs",
        "scope": {
            "cardEffectBodyImplementation": "forbidden",
            "purpose": "Map original CardEffectInterfaces contracts to deterministic headless descriptors, services, and primitives.",
        },
        "interfaceCount": len(cardeffect_interface_mappings),
        "interfaces": cardeffect_interface_mappings,
    }

    write_text(as_is_dir / "dcgo-assets-tree.md", render_assets_tree(assets_rows, generated_at, source_root))
    write_text(as_is_dir / "script-file-inventory.md", render_script_inventory(infos, generated_at, source_root))
    write_text(as_is_dir / "playing-logic-map.md", render_playing_logic_map(playing_logic_rows, generated_at))
    write_text(as_is_dir / "cardeffect-dependency-index.md", render_cardeffect_dependency_index(api_records, generated_at, source_root))
    write_text(as_is_dir / "script-to-headless-gap-matrix.md", render_gap_matrix(infos, generated_at))
    write_text(as_is_dir / "foundation-work-breakdown.md", render_foundation_work_breakdown(foundation_items, generated_at))
    write_text(as_is_dir / "recommended-next-foundation-task.md", render_recommended_next_task(generated_at, top_api_records))
    write_text(as_is_dir / "cardeffect-interface-mapping.md", render_cardeffect_interface_mapping(cardeffect_interface_mappings, generated_at, source_root))
    write_text(
        WORKSPACE_ROOT / "docs/rl-engine/script-runtime-foundation-contract.md",
        render_script_runtime_foundation_contract(
            script_runtime_contract_candidates,
            script_runtime_implementation_units,
            generated_at,
        ),
    )

    write_text(generated_dir / "script_file_inventory.json", json.dumps(script_inventory, ensure_ascii=False, indent=2))
    write_text(generated_dir / "cardeffect_api_references.json", json.dumps(cardeffect_index, ensure_ascii=False, indent=2))
    write_text(generated_dir / "script_to_headless_gap_matrix.json", json.dumps(gap_matrix, ensure_ascii=False, indent=2))
    write_text(generated_dir / "foundation_work_items.json", json.dumps(foundation_json, ensure_ascii=False, indent=2))
    write_text(generated_dir / "cardeffect_interface_mapping.json", json.dumps(cardeffect_interface_mapping_json, ensure_ascii=False, indent=2))
    write_text(
        generated_dir / "script_runtime_foundation_contract_candidates.json",
        json.dumps(script_runtime_contract_json, ensure_ascii=False, indent=2),
    )

    print(json.dumps({
        "sourceRoot": str(source_root),
        "scriptFileCount": len(infos),
        "cardEffectFileCount": len(cardeffect_files),
        "apiCount": len(api_records),
        "scriptRuntimeContractCount": len(script_runtime_contract_candidates),
        "cardEffectInterfaceCount": len(cardeffect_interface_mappings),
        "outputs": [
            "docs/as-is/dcgo-assets-tree.md",
            "docs/as-is/script-file-inventory.md",
            "docs/generated/as-is/script_file_inventory.json",
            "docs/as-is/playing-logic-map.md",
            "docs/as-is/cardeffect-dependency-index.md",
            "docs/generated/as-is/cardeffect_api_references.json",
            "docs/as-is/script-to-headless-gap-matrix.md",
            "docs/generated/as-is/script_to_headless_gap_matrix.json",
            "docs/as-is/foundation-work-breakdown.md",
            "docs/generated/as-is/foundation_work_items.json",
            "docs/as-is/recommended-next-foundation-task.md",
            "docs/as-is/cardeffect-interface-mapping.md",
            "docs/generated/as-is/cardeffect_interface_mapping.json",
            "docs/rl-engine/script-runtime-foundation-contract.md",
            "docs/generated/as-is/script_runtime_foundation_contract_candidates.json",
        ],
    }, ensure_ascii=False, indent=2))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
