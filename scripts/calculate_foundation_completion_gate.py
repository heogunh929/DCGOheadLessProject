"""Calculate the Foundation Completion Gate for the DCGO RL engine.

This gate is deliberately conservative. It does not promote any capability to
ready status; it only aggregates existing source inventory, capability truth
audit, generated/code status mismatch, and static policy scans into a single
OpenCodeReady decision.
"""

from __future__ import annotations

import argparse
import json
import re
import subprocess
from collections import Counter
from dataclasses import dataclass
from pathlib import Path
from typing import Any, Iterable


FULL_MECHANIC_INVENTORY = Path("docs/generated/full-mechanic-inventory.json")
CAPABILITY_REGISTRY = Path("docs/generated/capability-truth-audit/capability-registry.json")
STATUS_MISMATCH_REPORT = Path("docs/generated/capability-truth-audit/status-mismatch-report.json")
SOURCE_REQUIRED_CAPABILITIES = Path("docs/generated/capability-truth-audit/source-required-capabilities.json")
FULL_CARD_POOL_BASELINE = Path("docs/generated/full-card-pool-validation-baseline-65.json")
STATUS_REGISTRY = Path("docs/generated/full-card-source-scaffold/status-registry.json")
SCHEDULER_66E = Path("docs/generated/capability-truth-audit/mechanic-first-scheduler-66E.json")

OUT_JSON = Path("docs/generated/foundation-completion-gate.json")
OUT_MD = Path("docs/rl-engine/foundation-completion-gate.md")

GATE_SOURCE_LOCK_PATHS = [
    Path("DCGO/Assets"),
]

LOCAL_SOURCE_ROOT_CANDIDATES = [
    Path("DCGO/Assets"),
    Path(r"E:\headlessDCGO\DCGO\Assets"),
]

DIRECT_ZONE_MUTATION_ALLOWED_PATH_PARTS = {
    "src/DCGO.RL.Engine/Domain/ZoneMover.cs",
    "src/DCGO.RL.Engine/Domain/GameState.cs",
    "src/DCGO.RL.Engine/Domain/PlayerState.cs",
    "src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs",
    "src/DCGO.RL.Engine/Setup/DeckInstantiationService.cs",
}

DIRECT_ZONE_MUTATION_RE = re.compile(
    r"\b(?:Deck|DigiEggDeck|Hand|Trash|Security|OutsideGame|FieldPermanents|ActiveCardIds|Cards)"
    r"\.(?:Add|AddRange|Insert|Remove|RemoveAt|Clear)\s*\("
)

EMPTY_DESCRIPTOR_RE = re.compile(
    r"CardEffectPortingStatus\.(?:Unsupported|PartiallyImplemented)[\s\S]{0,900}?"
    r"Array\.Empty<(?:(?:EffectDescriptor)|(?:ContinuousEffectDescriptor))>\s*\("
)

EMPTY_RESOLVE_RE = re.compile(
    r"public\s+void\s+Resolve\s*\([^)]*\)\s*\{\s*\}",
    re.MULTILINE,
)

CARD_ID_BRANCH_RE = re.compile(
    r"\b(?:CardId|cardId|definition\.CardId)\s*(?:==|!=|is)\s*\"[A-Z0-9]+-\d+\""
)

ALTERNATE_DESCRIPTOR_CHANNELS = (
    ("IContinuousCardScript", "new ContinuousEffectDescriptor"),
    ("IContinuousKeywordCardScript", "new ContinuousKeywordDescriptor"),
    ("IStaticEvolutionRequirementCardScript", "new StaticEvolutionRequirementDescriptor"),
    ("ICannotIgnoreDigivolutionRequirementCardScript", "new CannotIgnoreDigivolutionRequirementDescriptor"),
    ("IStaticLinkRequirementCardScript", "new StaticLinkRequirementDescriptor"),
    ("IStaticCostModifierCardScript", "new StaticCostModifierDescriptor"),
    ("IStaticRestrictionCardScript", "new StaticRestrictionDescriptor"),
    ("IStaticImmunityCardScript", "new StaticImmunityDescriptor"),
)


@dataclass(frozen=True)
class Gate:
    gate_id: str
    name: str
    passed: bool
    value: Any
    expected: str
    details: str


def load_json(workspace: Path, relative_path: Path) -> dict[str, Any]:
    path = workspace / relative_path
    if not path.exists():
        raise FileNotFoundError(f"Required gate input is missing: {relative_path.as_posix()}")
    return json.loads(path.read_text(encoding="utf-8"))


def write_json(path: Path, payload: dict[str, Any]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(payload, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def normalize_path(path: Path) -> str:
    return path.as_posix()


def git_lines(workspace: Path, args: list[str]) -> list[str]:
    try:
        result = subprocess.run(
            ["git", *args],
            cwd=workspace,
            check=False,
            capture_output=True,
            text=True,
            encoding="utf-8",
        )
    except OSError:
        return ["<git unavailable>"]

    output = result.stdout.strip()
    if not output:
        return []
    return output.splitlines()


def count_git_status(workspace: Path, path: Path) -> int:
    return len(git_lines(workspace, ["status", "--short", "--", normalize_path(path)]))


def resolve_local_source_root(workspace: Path) -> Path | None:
    for candidate in LOCAL_SOURCE_ROOT_CANDIDATES:
        resolved = candidate if candidate.is_absolute() else workspace / candidate
        if resolved.exists():
            return resolved

    return None


def has_non_empty_alternate_descriptor_channel(text: str) -> bool:
    return any(interface in text and descriptor in text for interface, descriptor in ALTERNATE_DESCRIPTOR_CHANNELS)


def iter_cs_files(root: Path) -> Iterable[Path]:
    if not root.exists():
        return []
    return root.rglob("*.cs")


def scan_policy_counts(workspace: Path) -> dict[str, Any]:
    engine_root = workspace / "src/DCGO.RL.Engine"
    card_effect_root = engine_root / "CardEffects"

    direct_zone_mutations: list[dict[str, Any]] = []
    core_card_id_branches: list[dict[str, Any]] = []
    blocked_empty_descriptors: list[dict[str, Any]] = []
    legacy_continuous_empty_descriptors: list[dict[str, Any]] = []
    silent_noops: list[dict[str, Any]] = []

    for file_path in iter_cs_files(engine_root):
        relative = normalize_path(file_path.relative_to(workspace))
        text = file_path.read_text(encoding="utf-8")

        if relative not in DIRECT_ZONE_MUTATION_ALLOWED_PATH_PARTS:
            for match in DIRECT_ZONE_MUTATION_RE.finditer(text):
                direct_zone_mutations.append(
                    {
                        "path": relative,
                        "offset": match.start(),
                        "snippet": match.group(0),
                    }
                )

        if "/CardEffects/" not in relative:
            for match in CARD_ID_BRANCH_RE.finditer(text):
                core_card_id_branches.append(
                    {
                        "path": relative,
                        "offset": match.start(),
                        "snippet": match.group(0),
                    }
                )

        if file_path.is_relative_to(card_effect_root):
            for match in EMPTY_DESCRIPTOR_RE.finditer(text):
                sample = {
                    "path": relative,
                    "offset": match.start(),
                }
                if has_non_empty_alternate_descriptor_channel(text):
                    legacy_continuous_empty_descriptors.append(sample)
                else:
                    blocked_empty_descriptors.append(sample)

            if "CardEffectPortingStatus.NoEffect" not in text:
                for match in EMPTY_RESOLVE_RE.finditer(text):
                    silent_noops.append(
                        {
                            "path": relative,
                            "offset": match.start(),
                        }
                    )

    return {
        "directZoneMutationCount": len(direct_zone_mutations),
        "directZoneMutationSamples": direct_zone_mutations[:20],
        "coreCardIdBranchCount": len(core_card_id_branches),
        "coreCardIdBranchSamples": core_card_id_branches[:20],
        "blockedEmptyDescriptorCount": len(blocked_empty_descriptors),
        "blockedEmptyDescriptorSamples": blocked_empty_descriptors[:20],
        "legacyContinuousOnlyEmptyDescriptorCount": len(legacy_continuous_empty_descriptors),
        "legacyContinuousOnlyEmptyDescriptorSamples": legacy_continuous_empty_descriptors[:20],
        "silentNoOpCount": len(silent_noops),
        "silentNoOpSamples": silent_noops[:20],
    }


def status_counts(records: Iterable[dict[str, Any]], field: str) -> Counter[str]:
    counts: Counter[str] = Counter()
    for record in records:
        counts[str(record.get(field, ""))] += 1
    return counts


def build_gate(workspace: Path) -> dict[str, Any]:
    inventory = load_json(workspace, FULL_MECHANIC_INVENTORY)
    capability_registry = load_json(workspace, CAPABILITY_REGISTRY)
    mismatch = load_json(workspace, STATUS_MISMATCH_REPORT)
    source_required = load_json(workspace, SOURCE_REQUIRED_CAPABILITIES)
    baseline = load_json(workspace, FULL_CARD_POOL_BASELINE)
    status_registry = load_json(workspace, STATUS_REGISTRY)
    scheduler = load_json(workspace, SCHEDULER_66E)

    policy_counts = scan_policy_counts(workspace)

    missing_candidates = inventory.get("missingLayerCandidates", [])
    unknown_common_api_count = sum(
        1
        for candidate in missing_candidates
        if candidate.get("mappingStatus") == "NeedsSourceReview"
    )
    missing_candidate_status_counts = status_counts(missing_candidates, "mappingStatus")

    capabilities = capability_registry.get("capabilities", [])
    referenced_capability_ids = {
        capability
        for source_effect in source_required.get("sourceEffects", [])
        for capability in source_effect.get("requiredCapabilities", [])
    }
    referenced_capabilities = [
        capability
        for capability in capabilities
        if capability.get("capabilityId") in referenced_capability_ids
    ]
    unsupported_capability_count = sum(
        1
        for capability in referenced_capabilities
        if capability.get("status") == "Unsupported"
    )
    partial_capability_count = sum(
        1
        for capability in referenced_capabilities
        if capability.get("status") == "PartiallyImplemented"
    )

    baseline_issue_counts = baseline.get("blockingIssueCounts", {})
    false_no_effect_count = int(baseline_issue_counts.get("FalseNoEffect", 0))
    variant_identity_conflict_count = int(
        baseline_issue_counts.get("DuplicateDefinitionStableId", 0)
        + baseline_issue_counts.get("VariantIdentityConflict", 0)
        + baseline_issue_counts.get("VariantEffectClassSplit", 0)
    )

    local_source_root = resolve_local_source_root(workspace)
    source_root_available = local_source_root is not None
    source_root_path = str(local_source_root) if local_source_root is not None else None
    script_source_file_count = int(inventory.get("sourceScope", {}).get("scriptSourceFileCount", 0))
    effect_set_method_usage_count = len(inventory.get("effectSetMethodUsage", []))
    factory_api_usage_count = len(inventory.get("factoryApiUsage", []))
    commons_api_usage_count = len(inventory.get("commonsApiUsage", []))

    source_lock_changed_count = sum(count_git_status(workspace, path) for path in GATE_SOURCE_LOCK_PATHS)

    gates = [
        Gate(
            "script-cardeffects-inventory",
            "원본 Script/CardEffects inventory",
            script_source_file_count > 0 and effect_set_method_usage_count > 0,
            {
                "generatedScriptSourceFileCount": script_source_file_count,
                "effectSetMethodUsageCount": effect_set_method_usage_count,
                "localSourceRootAvailable": source_root_available,
                "localSourceRootPath": source_root_path,
            },
            "> 0",
            "원본 공통 Script source와 Effect.Set* 사용 목록이 generated inventory에 있어야 한다.",
        ),
        Gate(
            "cardeffectfactory-inventory",
            "원본 CardEffectFactory inventory",
            factory_api_usage_count > 0,
            {
                "factoryApiUsageCount": factory_api_usage_count,
                "localSourceRootAvailable": source_root_available,
                "localSourceRootPath": source_root_path,
            },
            "> 0",
            "원본 CardEffectFactory API usage가 generated inventory에 있어야 한다.",
        ),
        Gate(
            "cardeffectcommons-inventory",
            "원본 CardEffectCommons inventory",
            commons_api_usage_count > 0,
            {
                "commonsApiUsageCount": commons_api_usage_count,
                "localSourceRootAvailable": source_root_available,
                "localSourceRootPath": source_root_path,
            },
            "> 0",
            "원본 CardEffectCommons API usage가 generated inventory에 있어야 한다.",
        ),
        Gate(
            "common-api-mapping-known",
            "referenced common API unknown count",
            unknown_common_api_count == 0,
            unknown_common_api_count,
            "0",
            f"missingLayerCandidates mappingStatus 분포: {dict(missing_candidate_status_counts)}",
        ),
        Gate(
            "unsupported-capabilities-zero",
            "referenced Unsupported capability count",
            unsupported_capability_count == 0,
            unsupported_capability_count,
            "0",
            "source-required-capabilities에 등장한 capability 중 Unsupported 상태 수.",
        ),
        Gate(
            "partial-capabilities-zero",
            "referenced PartiallyImplemented capability count",
            partial_capability_count == 0,
            partial_capability_count,
            "0",
            "source-required-capabilities에 등장한 capability 중 PartiallyImplemented 상태 수.",
        ),
        Gate(
            "runtime-generated-status-match",
            "runtime/generated status mismatch count",
            int(mismatch.get("summary", {}).get("statusMismatchCount", 0)) == 0,
            int(mismatch.get("summary", {}).get("statusMismatchCount", 0)),
            "0",
            "status-mismatch-report의 generated registry와 runtime CardEffectPortingRecord 불일치 수.",
        ),
        Gate(
            "silent-noop-zero",
            "silent no-op count",
            policy_counts["silentNoOpCount"] == 0,
            policy_counts["silentNoOpCount"],
            "0",
            "NoEffect가 아닌 ICardScript의 빈 Resolve 후보를 정적 검색한다.",
        ),
        Gate(
            "blocked-empty-descriptor-zero",
            "blocked empty descriptor count",
            policy_counts["blockedEmptyDescriptorCount"] == 0,
            policy_counts["blockedEmptyDescriptorCount"],
            "0",
            "Unsupported/PartiallyImplemented 기록 근처의 빈 descriptor 후보를 정적 검색한다.",
        ),
        Gate(
            "false-noeffect-zero",
            "false NoEffect count",
            false_no_effect_count == 0,
            false_no_effect_count,
            "0",
            "full-card-pool validation baseline의 FalseNoEffect issue 수.",
        ),
        Gate(
            "variant-identity-conflict-zero",
            "variant identity conflict count",
            variant_identity_conflict_count == 0,
            variant_identity_conflict_count,
            "0",
            "variant identity 관련 baseline issue 수.",
        ),
        Gate(
            "core-cardid-branch-zero",
            "core CardId branch count",
            policy_counts["coreCardIdBranchCount"] == 0,
            policy_counts["coreCardIdBranchCount"],
            "0",
            "CardEffects 밖 core/foundation code의 특정 CardId 조건 분기 후보 수.",
        ),
        Gate(
            "direct-zone-mutation-zero",
            "direct zone mutation count",
            policy_counts["directZoneMutationCount"] == 0,
            policy_counts["directZoneMutationCount"],
            "0",
            "ZoneMover/GameState clone/setup 등 허용 경계를 제외한 zone list 직접 mutation 후보 수.",
        ),
        Gate(
            "source-lock-clean",
            "source lock 변경 없음",
            source_lock_changed_count == 0,
            source_lock_changed_count,
            "0",
            "DCGO/Assets git status 변경 수.",
        ),
    ]

    failed_gates = [gate for gate in gates if not gate.passed]
    open_code_ready = not failed_gates

    return {
        "schemaVersion": "dcgo.foundation-completion-gate.v1",
        "inputs": {
            "fullMechanicInventory": FULL_MECHANIC_INVENTORY.as_posix(),
            "capabilityRegistry": CAPABILITY_REGISTRY.as_posix(),
            "statusMismatchReport": STATUS_MISMATCH_REPORT.as_posix(),
            "sourceRequiredCapabilities": SOURCE_REQUIRED_CAPABILITIES.as_posix(),
            "fullCardPoolBaseline": FULL_CARD_POOL_BASELINE.as_posix(),
            "statusRegistry": STATUS_REGISTRY.as_posix(),
            "mechanicFirstScheduler": SCHEDULER_66E.as_posix(),
        },
        "policy": {
            "cardPortingAllowedBeforeOpenCodeReady": False,
            "rlComponentsAllowedBeforeOpenCodeReady": False,
            "emptyDescriptorMayHideUnsupported": False,
            "manualGeneratedRegistryPromotionAllowed": False,
            "coreCardIdBranchAllowed": False,
            "directZoneMutationAllowedOnlyIn": sorted(DIRECT_ZONE_MUTATION_ALLOWED_PATH_PARTS),
        },
        "summary": {
            "openCodeReady": open_code_ready,
            "passedGateCount": len(gates) - len(failed_gates),
            "failedGateCount": len(failed_gates),
            "unknownCommonApiCount": unknown_common_api_count,
            "unsupportedCapabilityCount": unsupported_capability_count,
            "partiallyImplementedCapabilityCount": partial_capability_count,
            "runtimeGeneratedStatusMismatchCount": int(mismatch.get("summary", {}).get("statusMismatchCount", 0)),
            "legacyPilotRuntimeDivergenceCount": int(mismatch.get("summary", {}).get("legacyPilotDivergenceCount", 0)),
            "silentNoOpCount": policy_counts["silentNoOpCount"],
            "blockedEmptyDescriptorCount": policy_counts["blockedEmptyDescriptorCount"],
            "legacyContinuousOnlyEmptyDescriptorCount": policy_counts["legacyContinuousOnlyEmptyDescriptorCount"],
            "falseNoEffectCount": false_no_effect_count,
            "variantIdentityConflictCount": variant_identity_conflict_count,
            "coreCardIdBranchCount": policy_counts["coreCardIdBranchCount"],
            "directZoneMutationCount": policy_counts["directZoneMutationCount"],
            "sourceLockChangedCount": source_lock_changed_count,
            "selectedNextFoundationCapability": scheduler.get("summary", {}).get("selectedCapabilityId"),
            "selectedNextFoundationStatus": (scheduler.get("selectedMechanic") or {}).get("status"),
            "sourceEffectCount": source_required.get("summary", {}).get("sourceEffectCount"),
            "sourceEffectsWithNonVerifiedCapabilities": source_required.get("summary", {}).get(
                "sourceEffectsWithNonVerifiedCapabilities"
            ),
            "generatedStatusCounts": status_registry.get("sourceScaffoldStatusCounts", {}),
            "cardMappingStatusCounts": status_registry.get("cardMappingStatusCounts", {}),
            "localSourceRootAvailable": source_root_available,
            "localSourceRootPath": source_root_path,
        },
        "gates": [
            {
                "id": gate.gate_id,
                "name": gate.name,
                "status": "passed" if gate.passed else "failed",
                "value": gate.value,
                "expected": gate.expected,
                "details": gate.details,
            }
            for gate in gates
        ],
        "samples": {
            "directZoneMutations": policy_counts["directZoneMutationSamples"],
            "coreCardIdBranches": policy_counts["coreCardIdBranchSamples"],
            "blockedEmptyDescriptors": policy_counts["blockedEmptyDescriptorSamples"],
            "legacyContinuousOnlyEmptyDescriptors": policy_counts["legacyContinuousOnlyEmptyDescriptorSamples"],
            "silentNoOps": policy_counts["silentNoOpSamples"],
            "missingCommonApiCandidates": [
                {
                    "category": candidate.get("category"),
                    "name": candidate.get("name"),
                    "mappingStatus": candidate.get("mappingStatus"),
                    "affectedCardCount": candidate.get("affectedCardCount"),
                    "sourceFileCount": candidate.get("sourceFileCount"),
                }
                for candidate in missing_candidates
                if candidate.get("mappingStatus") == "NeedsSourceReview"
            ][:20],
            "unsupportedCapabilities": [
                {
                    "capabilityId": capability.get("capabilityId"),
                    "affectedCardCount": capability.get("affectedCardCount"),
                    "sourceFileCount": capability.get("sourceFileCount"),
                }
                for capability in referenced_capabilities
                if capability.get("status") == "Unsupported"
            ][:20],
            "partiallyImplementedCapabilities": [
                {
                    "capabilityId": capability.get("capabilityId"),
                    "affectedCardCount": capability.get("affectedCardCount"),
                    "sourceFileCount": capability.get("sourceFileCount"),
                }
                for capability in referenced_capabilities
                if capability.get("status") == "PartiallyImplemented"
            ][:20],
        },
    }


def render_markdown(report: dict[str, Any]) -> str:
    summary = report["summary"]
    lines = [
        "# Foundation Completion Gate",
        "",
        "이 문서는 `scripts/calculate_foundation_completion_gate.py`가 생성한 현재 foundation gate 결과다.",
        "개별 CardEffect body 구현 가능 여부를 통과시키기 위한 문서가 아니라, OpenCode/local model에 넘기기 전에 남은 공통 foundation blocker를 보수적으로 드러내는 기준이다.",
        "",
        "## 결과",
        "",
        f"- OpenCodeReady: `{str(summary['openCodeReady']).lower()}`",
        f"- 통과 gate: {summary['passedGateCount']}",
        f"- 실패 gate: {summary['failedGateCount']}",
        f"- 다음 foundation capability 후보: `{summary.get('selectedNextFoundationCapability')}` (`{summary.get('selectedNextFoundationStatus')}`)",
        "",
        "## Gate",
        "",
        "| Gate | Status | Value | Expected |",
        "| --- | --- | ---: | --- |",
    ]

    for gate in report["gates"]:
        value = gate["value"]
        if isinstance(value, dict):
            value_text = json.dumps(value, ensure_ascii=False, sort_keys=True)
        else:
            value_text = str(value)
        lines.append(f"| {gate['name']} | `{gate['status']}` | `{value_text}` | `{gate['expected']}` |")

    lines.extend(
        [
            "",
            "## 주요 실패 수치",
            "",
            f"- referenced common API unknown count: {summary['unknownCommonApiCount']}",
            f"- referenced Unsupported capability count: {summary['unsupportedCapabilityCount']}",
            f"- referenced PartiallyImplemented capability count: {summary['partiallyImplementedCapabilityCount']}",
            f"- runtime/generated status mismatch count: {summary['runtimeGeneratedStatusMismatchCount']}",
            f"- legacy pilot runtime divergence count: {summary['legacyPilotRuntimeDivergenceCount']}",
            f"- silent no-op candidate count: {summary['silentNoOpCount']}",
            f"- blocked empty descriptor candidate count: {summary['blockedEmptyDescriptorCount']}",
            f"- legacy continuous-only empty descriptor count: {summary['legacyContinuousOnlyEmptyDescriptorCount']}",
            f"- core CardId branch candidate count: {summary['coreCardIdBranchCount']}",
            f"- direct zone mutation candidate count: {summary['directZoneMutationCount']}",
            "",
            "## 입력",
            "",
        ]
    )

    for name, path in report["inputs"].items():
        lines.append(f"- `{name}`: `{path}`")

    lines.extend(
        [
            "",
            "## 다음 작업",
            "",
            "`ContinuousOrStaticEffect`의 남은 공통 foundation 항목을 카드별 body 구현 없이 좁힌다. 현재 scheduler 기준으로 이 capability가 가장 많은 source/card batch를 막고 있다.",
            "",
        ]
    )

    return "\n".join(lines)


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace", default=".", help="Workspace root")
    parser.add_argument("--check", action="store_true", help="Do not write reports; return 1 if OpenCodeReady is false")
    args = parser.parse_args()

    workspace = Path(args.workspace).resolve()
    report = build_gate(workspace)

    if not args.check:
        write_json(workspace / OUT_JSON, report)
        (workspace / OUT_MD).parent.mkdir(parents=True, exist_ok=True)
        (workspace / OUT_MD).write_text(render_markdown(report), encoding="utf-8")

    print(json.dumps(report["summary"], ensure_ascii=False, indent=2))
    return 0 if report["summary"]["openCodeReady"] else 1 if args.check else 0


if __name__ == "__main__":
    raise SystemExit(main())
