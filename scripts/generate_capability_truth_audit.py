"""Generate the 66B capability truth audit artifacts.

The audit is intentionally conservative: a capability is not Verified unless
there is implementation evidence, execution-test evidence, and replay or
invariant evidence. Documentation claims are recorded as claims, not proof.
"""

from __future__ import annotations

import argparse
import json
import re
from collections import Counter, defaultdict
from pathlib import Path
from typing import Any


OUT_DIR = Path("docs/generated/capability-truth-audit")
REPORT_PATH = Path("docs/rl-engine/capability-truth-audit-66B.md")

MECHANIC_INVENTORY_PATH = Path("docs/generated/full-mechanic-inventory.json")
BATCH_MANIFEST_PATH = Path("docs/generated/full-card-porting-batches-66.json")
SCAFFOLD_INDEX_PATH = Path("docs/generated/full-card-source-scaffold/index.json")
STATUS_REGISTRY_PATH = Path("docs/generated/full-card-source-scaffold/status-registry.json")
SOURCE_SCAFFOLD_DIR = Path("docs/generated/full-card-source-scaffold/sources")
QUEUE_PATH = Path("docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md")

ALLOWED_STATUSES = {"Unsupported", "PartiallyImplemented", "Verified"}

FLAG_CAPABILITIES = {
    "inherited": "InheritedSource",
    "max_count_per_turn": "OncePerTurn",
    "modifier_duration": "DurationModifier",
    "optional": "OptionalDecision",
    "security": "SecuritySkill",
    "skippable": "SkippableEffect",
    "static_or_continuous": "ContinuousOrStaticEffect",
    "trigger_on_play": "OnPlayTrigger",
    "trigger_when_digivolving": "WhenDigivolvingTrigger",
    "zone_movement": "ZoneMovement",
}

SELECTION_CAPABILITIES = {
    "SelectCard": "Selection.SelectCard",
    "SelectHand": "Selection.SelectHand",
    "SelectPermanent": "Selection.SelectPermanent",
    "SelectSecurity": "Selection.SelectSecurity",
}

VERIFIED_EVIDENCE = {
    "Blocker": {
        "implementation": ["src/DCGO.RL.Engine/Battle/AttackService.cs", "src/DCGO.RL.Engine/Mechanics/BattleKeywordService.cs"],
        "tests": ["BattleKeywords Blocker selection request", "Attack timing blocker selection switches defender"],
        "replay": ["Attack timing blocker replay deterministic"],
    },
    "Piercing": {
        "implementation": ["src/DCGO.RL.Engine/Mechanics/BattleKeywordService.cs"],
        "tests": ["BattleKeywords Piercing security check"],
        "replay": ["ST1 CardEffect replay determinism"],
    },
    "Jamming": {
        "implementation": ["src/DCGO.RL.Engine/Mechanics/BattleKeywordService.cs"],
        "tests": ["BattleKeywords Jamming security battle"],
        "replay": ["ST1 CardEffect replay determinism"],
    },
    "Reboot": {
        "implementation": ["src/DCGO.RL.Engine/Mechanics/BattleKeywordService.cs"],
        "tests": ["BattleKeywords Reboot active phase"],
        "replay": ["ComplexMechanics replay determinism"],
    },
    "Rush": {
        "implementation": ["src/DCGO.RL.Engine/Mechanics/BattleKeywordService.cs"],
        "tests": ["BattleKeywords Rush attack legality"],
        "replay": ["ComplexMechanics replay determinism"],
    },
    "Jogress": {
        "implementation": ["src/DCGO.RL.Engine/Mechanics/JogressService.cs"],
        "tests": ["ComplexMechanics Jogress legal action", "ComplexMechanics Jogress execution top sources"],
        "replay": ["ComplexMechanics replay determinism"],
    },
    "Burst": {
        "implementation": ["src/DCGO.RL.Engine/Mechanics/BurstService.cs"],
        "tests": ["ComplexMechanics Burst legal action", "ComplexMechanics Burst tamer selection request"],
        "replay": ["ComplexMechanics replay determinism"],
    },
    "AppFusion": {
        "implementation": ["src/DCGO.RL.Engine/Mechanics/AppFusionService.cs"],
        "tests": ["ComplexMechanics App Fusion link card selection", "ComplexMechanics Link card state consistency"],
        "replay": ["ComplexMechanics replay determinism"],
    },
    "DigiXros": {
        "implementation": ["src/DCGO.RL.Engine/Mechanics/DigiXrosService.cs"],
        "tests": ["ComplexMechanics DigiXros materials and cost"],
        "replay": ["ComplexMechanics replay determinism"],
    },
    "Assembly": {
        "implementation": ["src/DCGO.RL.Engine/Mechanics/AssemblyService.cs"],
        "tests": ["ComplexMechanics Assembly material selection"],
        "replay": ["ComplexMechanics replay determinism"],
    },
}

PARTIAL_EVIDENCE = {
    "ContinuousOrStaticEffect": {
        "implementation": [
            "src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs",
            "src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs",
            "src/DCGO.RL.Engine/Effects/StaticRequirementService.cs",
        ],
        "tests": [
            "Continuous linked source applies from linked zone",
            "Continuous face-up security source applies",
            "Continuous face-down security source is ignored",
            "Continuous hand source applies only from hand",
            "Continuous trash source applies from trash",
            "Continuous executing source applies during execution",
            "Continuous static keyword field source grants Blocker",
            "Continuous static keyword inherited source stops after move",
            "Continuous static keyword condition gates keyword",
            "Continuous static keyword replay deterministic",
            "Static evolution requirement hand source generates and executes",
            "Static evolution requirement stops after source move",
            "Static evolution requirement condition gates target",
            "Static link requirement hand source generates and executes",
            "Static requirement replay deterministic",
            "Duration player DP modifier affects owner battle area",
            "Duration player SecurityAttack modifier affects owner Digimon",
            "Duration player runtime modifiers clone restore hash",
            "Duration player runtime modifiers replay deterministic",
        ],
        "replay": [
            "Continuous effects are derived for state hash",
            "Continuous and duration modifiers stack deterministically",
            "Continuous static keyword replay deterministic",
            "Static requirement replay deterministic",
            "Duration player runtime modifiers replay deterministic",
        ],
    },
}

CAPABILITY_OVERRIDES = {
    "OnDraw": {
        "status": "PartiallyImplemented",
        "reason": "L0005 keeps OnDraw blocked while L0006 documents partial pending-rule coverage; draw primitive selection/replay boundary still needs a focused mechanic gate.",
    },
    "BeforePayCost": {
        "status": "Unsupported",
        "reason": "Cost modifier/replacement layer is documented as a common blocker and is not executable for full-card batches.",
    },
    "AfterPayCost": {
        "status": "Unsupported",
        "reason": "After-pay-cost layer remains a common blocker.",
    },
    "OnDeclaration": {
        "status": "Unsupported",
        "reason": "Declaration/cost continuation is still unresolved for generated full-card batches.",
    },
    "OnEndBattle": {
        "status": "Unsupported",
        "reason": "Battle-end payload and lifecycle boundary remain unresolved for generated full-card batches.",
    },
    "WhenDigisorption": {
        "status": "Unsupported",
        "reason": "Digisorption timing remains a documented common-layer blocker.",
    },
    "WhenRemoveField": {
        "status": "Unsupported",
        "reason": "Would-remove replacement/cut-in semantics remain unresolved for generated batches.",
    },
    "OnMove": {
        "status": "Unsupported",
        "reason": "Move timing payload and source-zone semantics remain unresolved.",
    },
    "WhenReturntoLibraryAnyone": {
        "status": "Unsupported",
        "reason": "Return-to-library cut-in shares unresolved would-remove replacement semantics.",
    },
    "WhenUntapAnyone": {
        "status": "Unsupported",
        "reason": "Untap cut-in pre/post refix semantics remain unresolved.",
    },
    "Selection.SelectCard": {
        "status": "PartiallyImplemented",
        "reason": "Generic selection requests exist, but source-specific selection semantics and replay evidence are not complete for all generated sources.",
    },
    "Selection.SelectHand": {
        "status": "PartiallyImplemented",
        "reason": "Hand selection exists in fixtures, but hidden-info and full-card source semantics are not fully verified.",
    },
    "Selection.SelectPermanent": {
        "status": "PartiallyImplemented",
        "reason": "Permanent selection exists, but generated-card semantics still need capability-specific verification.",
    },
    "Selection.SelectSecurity": {
        "status": "PartiallyImplemented",
        "reason": "Security selection exists, but face-up/security-stack variants and full-card replay evidence remain incomplete.",
    },
    "ZoneMovement": {
        "status": "PartiallyImplemented",
        "reason": "ZoneMover and pending rule events exist, but many card-specific source movement variants remain blocked.",
    },
    "OptionalDecision": {
        "status": "PartiallyImplemented",
        "reason": "DecisionToken pause/resume exists, but generated card effect coverage is not complete.",
    },
    "SecuritySkill": {
        "status": "PartiallyImplemented",
        "reason": "SecuritySkill execution exists for covered fixtures, but full-card security skill bodies remain per-source.",
    },
    "ContinuousOrStaticEffect": {
        "status": "PartiallyImplemented",
        "reason": "Continuous descriptors support field top, inherited, linked, face-up security, hand, trash, and executing source scopes; continuous static keyword descriptors cover source/condition-aware supported BattleKeyword grants; StaticRequirementService covers source/condition-aware static digivolution and link requirement descriptors for generated action/execution/replay paths; and TemporaryModifier covers supported player-level DP/SecurityAttack/SecurityDigimonDP runtime stat effects with clone/hash/replay evidence. Trait/name/text metadata, unsupported static effect interfaces, ignore-digivolution-permission semantics, and full-card parity evidence remain unresolved.",
    },
    "DurationModifier": {
        "status": "PartiallyImplemented",
        "reason": "Duration modifiers are implemented for core scopes, but full-card duration variants remain incomplete.",
    },
    "InheritedSource": {
        "status": "PartiallyImplemented",
        "reason": "Inherited descriptors exist, but source role and moved-source revalidation remain capability-specific.",
    },
    "OncePerTurn": {
        "status": "PartiallyImplemented",
        "reason": "RuntimeRuleState owns once-per-turn state, but generated effect identity binding remains incomplete.",
    },
    "SkippableEffect": {
        "status": "PartiallyImplemented",
        "reason": "Skip decisions exist, but original MultipleSkills/optional semantics need per-capability verification.",
    },
    "OnPlayTrigger": {
        "status": "PartiallyImplemented",
        "reason": "OnPlay-compatible descriptors exist in fixtures, but global OnEnterFieldAnyone payload coverage is incomplete.",
    },
    "WhenDigivolvingTrigger": {
        "status": "PartiallyImplemented",
        "reason": "WhenDigivolving descriptors exist in fixtures, but full event payload/source eligibility remains incomplete.",
    },
}


def load_json(path: Path) -> Any:
    return json.loads(path.read_text(encoding="utf-8"))


def write_json(path: Path, payload: Any) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(payload, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def parse_queue_statuses(path: Path) -> dict[str, str]:
    statuses: dict[str, str] = {}
    for raw_line in path.read_text(encoding="utf-8").splitlines():
        line = raw_line.strip()
        if not line.startswith("|"):
            continue
        cells = [cell.strip() for cell in line.strip("|").split("|")]
        if len(cells) < 4 or cells[0] in {"Order", "---"}:
            continue
        prompt_file = cells[2]
        if not prompt_file.endswith(".md"):
            continue
        statuses[Path(prompt_file).stem] = cells[1]
    return statuses


def source_records(workspace: Path) -> list[dict[str, Any]]:
    records: list[dict[str, Any]] = []
    for path in sorted((workspace / SOURCE_SCAFFOLD_DIR).glob("*.json")):
        payload = load_json(path)
        records.extend(payload.get("records", []))
    return records


def infer_required_capabilities(record: dict[str, Any]) -> list[str]:
    capabilities: set[str] = set()
    for timing in record.get("timings", []):
        capabilities.add(str(timing))
    flags = record.get("flags", {})
    if isinstance(flags, dict):
        flag_names = [name for name, enabled in flags.items() if enabled]
    else:
        flag_names = list(flags)
    for flag in flag_names:
        capability = FLAG_CAPABILITIES.get(str(flag))
        if capability:
            capabilities.add(capability)
    for selection in record.get("selectionKinds", []):
        capability = SELECTION_CAPABILITIES.get(str(selection))
        if capability:
            capabilities.add(capability)
    return sorted(capabilities)


def collect_inventory_capabilities(inventory: dict[str, Any]) -> dict[str, dict[str, Any]]:
    capabilities: dict[str, dict[str, Any]] = {}
    for section in ("timings", "features", "selections", "rootZones", "specialMechanics", "keywords"):
        for item in inventory.get(section, []):
            name = str(item.get("name", ""))
            if not name:
                continue
            capabilities[name] = {
                "capabilityId": name,
                "inventorySection": section,
                "inventoryStatus": item.get("mappingStatus", "Unknown"),
                "affectedCardCount": item.get("affectedCardCount", 0),
                "sourceFileCount": item.get("cardEffectSourceFileCount", item.get("sourceFileCount", 0)),
            }
    return capabilities


def default_status(raw_status: str) -> str:
    if raw_status == "Unsupported":
        return "Unsupported"
    if raw_status in {"Verified", "PartiallyImplemented"}:
        return "PartiallyImplemented"
    return "Unsupported"


def collect_text_evidence(workspace: Path, capability_id: str) -> dict[str, list[str]]:
    code_paths: list[str] = []
    for root in (workspace / "src/DCGO.RL.Engine").rglob("*.cs"):
        try:
            text = root.read_text(encoding="utf-8")
        except UnicodeDecodeError:
            continue
        if capability_id in text:
            code_paths.append(root.relative_to(workspace).as_posix())
    tests: list[str] = []
    test_path = workspace / "src/DCGO.RL.Engine.Tests/Program.cs"
    if test_path.exists():
        for line in test_path.read_text(encoding="utf-8").splitlines():
            if line.strip().startswith("(\"") and capability_id.lower() in line.lower():
                match = re.search(r'\("([^"]+)"', line)
                if match:
                    tests.append(match.group(1))
    return {
        "implementationEvidence": code_paths[:12],
        "testEvidence": tests[:12],
    }


def document_claims(workspace: Path, capability_id: str, queue_statuses: dict[str, str]) -> list[dict[str, str]]:
    claims: list[dict[str, str]] = []
    doc_paths = sorted((workspace / "docs/rl-engine").glob("full-card-porting-*.md"))
    queue_id_by_prefix: dict[str, str] = {}
    for batch_id in queue_statuses:
        match = re.match(r"^([LCR]\d{4})", batch_id)
        if match:
            queue_id_by_prefix.setdefault(match.group(1), batch_id)

    for path in doc_paths:
        match = re.match(r"^full-card-porting-([LCR]\d{4})", path.stem)
        batch_id = queue_id_by_prefix.get(match.group(1), "") if match else ""
        if batch_id:
            text = path.read_text(encoding="utf-8", errors="ignore")
            if capability_id not in text:
                continue
            status = queue_statuses.get(batch_id, "unknown")
            claims.append(
                {
                    "path": path.relative_to(workspace).as_posix(),
                    "queueStatus": status,
                }
            )
    return claims


def build_capability_registry(workspace: Path) -> dict[str, Any]:
    inventory = load_json(workspace / MECHANIC_INVENTORY_PATH)
    queue_statuses = parse_queue_statuses(workspace / QUEUE_PATH)
    records = source_records(workspace)
    capabilities = collect_inventory_capabilities(inventory)

    for record in records:
        for capability_id in infer_required_capabilities(record):
            capabilities.setdefault(
                capability_id,
                {
                    "capabilityId": capability_id,
                    "inventorySection": "source-required",
                    "inventoryStatus": "Derived",
                    "affectedCardCount": 0,
                    "sourceFileCount": 0,
                },
            )

    capability_entries: list[dict[str, Any]] = []
    conflicts: list[dict[str, Any]] = []

    for capability_id in sorted(capabilities, key=str.lower):
        base = capabilities[capability_id]
        override = CAPABILITY_OVERRIDES.get(capability_id, {})
        verified = VERIFIED_EVIDENCE.get(capability_id)
        status = str(override.get("status") or default_status(str(base.get("inventoryStatus", ""))))
        reason = str(override.get("reason") or "Conservative audit status derived from inventory and local evidence.")

        evidence = collect_text_evidence(workspace, capability_id)
        replay_evidence: list[str] = []
        if partial := PARTIAL_EVIDENCE.get(capability_id):
            evidence["implementationEvidence"] = sorted(
                set(evidence["implementationEvidence"]) | set(partial["implementation"])
            )[:12]
            evidence["testEvidence"] = sorted(
                set(evidence["testEvidence"]) | set(partial["tests"])
            )[:12]
            replay_evidence = sorted(set(partial["replay"]))[:12]
        if verified:
            status = "Verified"
            reason = "Implementation, execution tests, and replay/invariant evidence are all present in the current engine test suite."
            evidence["implementationEvidence"] = verified["implementation"]
            evidence["testEvidence"] = verified["tests"]
            replay_evidence = verified["replay"]

        claims = document_claims(workspace, capability_id, queue_statuses)
        queue_claim_statuses = {claim["queueStatus"] for claim in claims}
        has_conflict = bool({"blocked", "needs-review"} & queue_claim_statuses and {"done"} & queue_claim_statuses)
        if has_conflict:
            conflicts.append(
                {
                    "capabilityId": capability_id,
                    "documentClaims": claims,
                    "finalStatus": status,
                    "resolution": "Documentation conflict is not allowed to auto-promote the capability. Final status follows implementation/test/replay evidence.",
                }
            )

        if status == "Verified" and (
            not evidence["implementationEvidence"] or not evidence["testEvidence"] or not replay_evidence
        ):
            status = "PartiallyImplemented"
            reason = "Downgraded because Verified requires implementation, execution tests, and replay/invariant evidence."

        capability_entries.append(
            {
                "capabilityId": capability_id,
                "status": status,
                "inventorySection": base.get("inventorySection"),
                "inventoryStatus": base.get("inventoryStatus"),
                "affectedCardCount": base.get("affectedCardCount", 0),
                "sourceFileCount": base.get("sourceFileCount", 0),
                "reason": reason,
                "implementationEvidence": evidence["implementationEvidence"],
                "testEvidence": evidence["testEvidence"],
                "replayOrInvariantEvidence": replay_evidence,
                "documentClaims": claims,
                "hasDocumentationConflict": has_conflict,
            }
        )

    status_counts = Counter(entry["status"] for entry in capability_entries)
    return {
        "schemaVersion": "dcgo.capability-truth-audit.66B.v1",
        "statusPolicy": {
            "allowedStatuses": sorted(ALLOWED_STATUSES),
            "verifiedRequires": [
                "actual engine implementation evidence",
                "execution test evidence",
                "replay or invariant evidence",
            ],
            "documentationOrEnumOnlyMayVerify": False,
            "needsReviewAllowedOnlyFor": [
                "user judgment",
                "source unclear or missing",
            ],
            "commonLayerUnimplementedStatus": "Unsupported",
        },
        "summary": {
            "capabilityCount": len(capability_entries),
            "statusCounts": dict(sorted(status_counts.items())),
            "documentationConflictCount": len(conflicts),
        },
        "conflicts": conflicts,
        "capabilities": capability_entries,
    }


def build_required_capabilities(workspace: Path, registry: dict[str, Any]) -> dict[str, Any]:
    status_by_capability = {
        entry["capabilityId"]: entry["status"]
        for entry in registry["capabilities"]
    }
    entries = []
    for record in source_records(workspace):
        required = infer_required_capabilities(record)
        non_verified = [
            capability for capability in required
            if status_by_capability.get(capability, "Unsupported") != "Verified"
        ]
        entries.append(
            {
                "sourceScaffoldId": record["sourceScaffoldId"],
                "sourceEffectClassName": record["sourceEffectClassName"],
                "sourcePath": record["sourcePath"],
                "affectedCards": record.get("affectedCards", []),
                "affectedCardCount": record.get("affectedCardCount", 0),
                "requiredCapabilities": required,
                "nonVerifiedCapabilities": non_verified,
            }
        )
    return {
        "schemaVersion": "dcgo.source-required-capabilities.66B.v1",
        "capabilityRegistry": "docs/generated/capability-truth-audit/capability-registry.json",
        "summary": {
            "sourceEffectCount": len(entries),
            "sourceEffectsWithNonVerifiedCapabilities": sum(1 for entry in entries if entry["nonVerifiedCapabilities"]),
        },
        "sourceEffects": entries,
    }


def build_batch_blockers(workspace: Path, required_doc: dict[str, Any], registry: dict[str, Any]) -> dict[str, Any]:
    manifest = load_json(workspace / BATCH_MANIFEST_PATH)
    required_by_source = {
        entry["sourceScaffoldId"]: entry
        for entry in required_doc["sourceEffects"]
    }
    status_by_capability = {
        entry["capabilityId"]: entry["status"]
        for entry in registry["capabilities"]
    }
    batch_entries = []
    for batch in manifest["batches"]:
        if batch.get("kind") != "card-porting":
            continue
        blockers: dict[str, dict[str, Any]] = {}
        for source in batch.get("sourceEffects", []):
            source_entry = required_by_source.get(source.get("sourceScaffoldId"))
            if not source_entry:
                continue
            for capability in source_entry["nonVerifiedCapabilities"]:
                blocker = blockers.setdefault(
                    capability,
                    {
                        "capabilityId": capability,
                        "status": status_by_capability.get(capability, "Unsupported"),
                        "sourceEffectClassNames": [],
                    },
                )
                blocker["sourceEffectClassNames"].append(source_entry["sourceEffectClassName"])
        batch_entries.append(
            {
                "batchId": batch["batchId"],
                "kind": batch["kind"],
                "category": batch["category"],
                "dependencyBatchIds": batch.get("dependencyBatchIds", []),
                "affectedCardCount": len(batch.get("cards", [])),
                "sourceEffectCount": len(batch.get("sourceEffects", [])),
                "isExecutable": False if blockers else True,
                "blockingCapabilities": sorted(blockers.values(), key=lambda item: item["capabilityId"]),
            }
        )
    c0039 = next((entry for entry in batch_entries if entry["batchId"] == "C0039_zone_security_recovery"), None)
    return {
        "schemaVersion": "dcgo.batch-capability-blockers.66B.v1",
        "policy": {
            "cardBatchExecutableOnlyWhenAllRequiredCapabilitiesVerified": True,
            "doNotSelectC0039": True,
            "blockerDocumentationDoesNotCompleteCardPortingBatch": True,
        },
        "summary": {
            "cardBatchCount": len(batch_entries),
            "blockedCardBatchCount": sum(1 for entry in batch_entries if not entry["isExecutable"]),
            "c0039Executable": bool(c0039 and c0039["isExecutable"]),
        },
        "batches": batch_entries,
    }


def collect_code_porting_records(workspace: Path) -> list[dict[str, str]]:
    records: list[dict[str, str]] = []
    pattern = re.compile(r"CardEffectPortingStatus\.(Unsupported|NoEffect|StubbedForValidation|PartiallyImplemented|Implemented|Verified)")
    for path in sorted((workspace / "src/DCGO.RL.Engine/CardEffects").rglob("*.cs")):
        name = path.stem
        if not re.match(r"^(BT|ST|EX|P|RB|LM|AD)\d*_[A-Za-z0-9_]+$", name):
            continue
        text = path.read_text(encoding="utf-8", errors="ignore")
        match = pattern.search(text)
        if not match:
            continue
        records.append(
            {
                "effectClassName": name,
                "status": match.group(1),
                "path": path.relative_to(workspace).as_posix(),
            }
        )
    return records


def build_status_mismatches(workspace: Path) -> dict[str, Any]:
    scaffold_status = {
        record["sourceEffectClassName"]: record.get("scaffoldStatus", "Unsupported")
        for record in source_records(workspace)
    }
    code_records = collect_code_porting_records(workspace)
    mismatches = []
    for record in code_records:
        generated = scaffold_status.get(record["effectClassName"])
        if generated is None:
            continue
        if generated != record["status"]:
            mismatches.append(
                {
                    "effectClassName": record["effectClassName"],
                    "cardEffectPortingRecordStatus": record["status"],
                    "generatedSourceScaffoldStatus": generated,
                    "path": record["path"],
                }
            )

    status_registry = load_json(workspace / STATUS_REGISTRY_PATH)
    registry_source = workspace / "src/DCGO.RL.Engine/CardEffects/CardScriptRegistry.cs"
    registry_text = registry_source.read_text(encoding="utf-8")
    variant_registry_integrated = (
        "_byDefinitionStableId" in registry_text
        and "DefinitionStableId" in registry_text
        and "VariantKey" in registry_text
        and "HasDefinitionIdentity" in registry_text
    )
    card_id_only_blocker = not variant_registry_integrated
    issues = []
    if card_id_only_blocker:
        issues.append(
            {
                "severity": "blocker",
                "code": "CardIdOnlyRuntimeRegistry",
                "message": "Runtime CardScriptRegistry resolves by CardId/effect class and cannot represent CardId#CardIndex@VariantKey identities.",
                "evidencePaths": [registry_source.relative_to(workspace).as_posix()],
                "requiredPolicy": "CardId#CardIndex@VariantKey",
            }
        )

    return {
        "schemaVersion": "dcgo.status-mismatch-report.66B.v1",
        "inputs": {
            "generatedStatusRegistry": STATUS_REGISTRY_PATH.as_posix(),
            "cardEffectPortingRecords": "src/DCGO.RL.Engine/CardEffects/**/*.cs",
        },
        "summary": {
            "generatedImplementedOrVerifiedCount": status_registry.get("implementedOrVerifiedCount", 0),
            "codePortingRecordCount": len(code_records),
            "statusMismatchCount": len(mismatches),
            "blockerIssueCount": len([issue for issue in issues if issue["severity"] == "blocker"]),
        },
        "mismatches": mismatches,
        "issues": issues,
    }


def write_report(
    workspace: Path,
    registry: dict[str, Any],
    required: dict[str, Any],
    batch_blockers: dict[str, Any],
    mismatch: dict[str, Any],
) -> None:
    c0039 = next(
        entry for entry in batch_blockers["batches"]
        if entry["batchId"] == "C0039_zone_security_recovery"
    )
    on_draw = next(
        entry for entry in registry["capabilities"]
        if entry["capabilityId"] == "OnDraw"
    )
    lines = [
        "# 66B Capability Truth Audit",
        "",
        "## 결정",
        "",
        "`66B_capability_truth_audit`는 mechanic-first remediation 전환을 위해 수행한 감사 항목이다. C0039 이후 card-porting batch는 실행 가능 후보로 선택하지 않는다.",
        "",
        "## 상태 정책",
        "",
        "- 상태 값은 `Unsupported`, `PartiallyImplemented`, `Verified`만 사용한다.",
        "- `Verified`는 실제 engine implementation, 실행 테스트, replay/invariant evidence가 모두 있을 때만 부여한다.",
        "- 문서 문자열, enum 존재, queue `done` 표기만으로는 `Verified` 처리하지 않는다.",
        "- 공통 layer 미구현은 `needs-review`가 아니라 blocker 성격의 `Unsupported` 또는 `PartiallyImplemented`로 분류한다.",
        "",
        "## 핵심 발견",
        "",
        f"- Capability total: {registry['summary']['capabilityCount']}",
        f"- Status counts: {registry['summary']['statusCounts']}",
        f"- Documentation conflicts: {registry['summary']['documentationConflictCount']}",
        f"- Source effects with non-verified capabilities: {required['summary']['sourceEffectsWithNonVerifiedCapabilities']} / {required['summary']['sourceEffectCount']}",
        f"- Blocked card batches: {batch_blockers['summary']['blockedCardBatchCount']} / {batch_blockers['summary']['cardBatchCount']}",
        f"- Status mismatches: {mismatch['summary']['statusMismatchCount']}",
        "",
        "## OnDraw 충돌",
        "",
        "`OnDraw`는 L0005에서 blocked로 남아 있고 L0006에서는 pending rule event coverage로 구현되었다고 기록되어 있다. 66B 최종 분류는 다음과 같다.",
        "",
        f"- Final status: `{on_draw['status']}`",
        f"- Reason: {on_draw['reason']}",
        "- L0006의 coverage는 실제 구현/테스트 근거로 인정하지만, L0005의 draw primitive selection-aware boundary blocker를 숨기지 않는다.",
        "",
        "## C0039 실행 가능성",
        "",
        f"- C0039 executable: `{str(c0039['isExecutable']).lower()}`",
        f"- Blocking capability count: {len(c0039['blockingCapabilities'])}",
        "- C0039는 66B 이후 실행 후보가 아니다. 66C, 66D, 66E가 runtime status/variant registry와 capability dependency graph를 정렬하기 전에는 card-porting batch로 넘어가지 않는다.",
        "",
        "## Runtime Registry Status",
        "",
        *(
            [
                "- `CardScriptRegistry`는 현재 CardId/effect-class lookup을 사용한다.",
                "- full-card identity policy는 `CardId#CardIndex@VariantKey`이다.",
                "- 이 불일치는 variant-aware runtime registry가 들어가기 전까지 blocker로 유지한다.",
            ]
            if any(issue["code"] == "CardIdOnlyRuntimeRegistry" for issue in mismatch["issues"])
            else [
                "- `CardScriptRegistry`는 exact `DefinitionStableId` lookup을 우선하고 legacy CardId fallback을 단일 정의 경로로 제한한다.",
                "- full-card identity policy `CardId#CardIndex@VariantKey`와 runtime registry lookup 경계가 연결되었다.",
                "- generated status registry와 실제 `CardEffectPortingRecord` 불일치는 여전히 남아 있으며 66D/66E의 capability dependency gate에서 계속 차단된다.",
            ]
        ),
        "",
        "## Machine-Readable Outputs",
        "",
        "- `docs/generated/capability-truth-audit/capability-registry.json`",
        "- `docs/generated/capability-truth-audit/source-required-capabilities.json`",
        "- `docs/generated/capability-truth-audit/batch-capability-blockers.json`",
        "- `docs/generated/capability-truth-audit/status-mismatch-report.json`",
        "",
        "## 다음 계획",
        "",
        "- `66C_runtime_status_variant_registry_integration`: 실제 `ICardScript` `PortingRecord`를 전체 status registry와 validator에 연결하고 variant-aware runtime registry를 도입한다.",
        "- `66D_card_effect_capability_dependency_graph`: 각 source effect의 required capability를 batch scheduler가 직접 사용하게 한다.",
        "- `66E_mechanic_first_goal_scheduler`: blocked card batch를 우회하지 않고 가장 많은 카드를 막는 unresolved mechanic을 다음 구현 대상으로 선택한다.",
    ]
    (workspace / REPORT_PATH).write_text("\n".join(lines) + "\n", encoding="utf-8")


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace", default=".", help="Repository workspace root.")
    args = parser.parse_args()
    workspace = Path(args.workspace).resolve()

    registry = build_capability_registry(workspace)
    required = build_required_capabilities(workspace, registry)
    batch_blockers = build_batch_blockers(workspace, required, registry)
    mismatch = build_status_mismatches(workspace)

    write_json(workspace / OUT_DIR / "capability-registry.json", registry)
    write_json(workspace / OUT_DIR / "source-required-capabilities.json", required)
    write_json(workspace / OUT_DIR / "batch-capability-blockers.json", batch_blockers)
    write_json(workspace / OUT_DIR / "status-mismatch-report.json", mismatch)
    write_report(workspace, registry, required, batch_blockers, mismatch)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
