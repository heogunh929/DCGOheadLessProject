"""Generate the 66D source-effect capability dependency graph.

The graph promotes the 66B capability truth audit from an audit artifact into
the machine-readable gate used by the full-card porting scheduler.
"""

from __future__ import annotations

import argparse
import json
from collections import defaultdict
from pathlib import Path
from typing import Any


OUT_PATH = Path("docs/generated/capability-truth-audit/capability-dependency-graph-66D.json")
CAPABILITY_REGISTRY_PATH = Path("docs/generated/capability-truth-audit/capability-registry.json")
SOURCE_REQUIRED_PATH = Path("docs/generated/capability-truth-audit/source-required-capabilities.json")
BATCH_BLOCKERS_PATH = Path("docs/generated/capability-truth-audit/batch-capability-blockers.json")
FULL_BATCH_MANIFEST_PATH = Path("docs/generated/full-card-porting-batches-66.json")


def load_json(path: Path) -> dict[str, Any]:
    return json.loads(path.read_text(encoding="utf-8"))


def write_json(path: Path, payload: dict[str, Any]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(payload, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def build_graph(workspace: Path) -> dict[str, Any]:
    capability_registry = load_json(workspace / CAPABILITY_REGISTRY_PATH)
    source_required = load_json(workspace / SOURCE_REQUIRED_PATH)
    batch_blockers = load_json(workspace / BATCH_BLOCKERS_PATH)
    full_manifest = load_json(workspace / FULL_BATCH_MANIFEST_PATH)

    capability_status = {
        capability["capabilityId"]: capability["status"]
        for capability in capability_registry["capabilities"]
    }
    source_by_class = {
        source["sourceEffectClassName"]: source
        for source in source_required["sourceEffects"]
    }
    batch_blocker_by_id = {
        batch["batchId"]: batch
        for batch in batch_blockers["batches"]
    }

    capability_index: dict[str, dict[str, Any]] = {}
    affected_cards_by_capability: dict[str, set[str]] = defaultdict(set)
    source_effects_by_capability: dict[str, set[str]] = defaultdict(set)
    batches_by_capability: dict[str, set[str]] = defaultdict(set)

    graph_batches: list[dict[str, Any]] = []
    for batch in full_manifest["batches"]:
        if batch.get("kind") != "card-porting":
            continue

        blocker = batch_blocker_by_id.get(batch["batchId"])
        source_effects: list[dict[str, Any]] = []
        blocking_capabilities = blocker.get("blockingCapabilities", []) if blocker else []
        blocking_capability_ids = {
            capability["capabilityId"]
            for capability in blocking_capabilities
        }

        for source_effect in batch.get("sourceEffects", []):
            source_name = source_effect.get("sourceEffectClassName", "")
            source_record = source_by_class.get(source_name)
            required_capabilities = list(source_record.get("requiredCapabilities", [])) if source_record else []
            affected_cards = list(source_record.get("affectedCards", [])) if source_record else list(source_effect.get("affectedCards", []))
            non_verified = [
                capability_id
                for capability_id in required_capabilities
                if capability_status.get(capability_id) != "Verified"
            ]

            for capability_id in required_capabilities:
                source_effects_by_capability[capability_id].add(source_name)
                batches_by_capability[capability_id].add(batch["batchId"])
                for card in affected_cards:
                    if definition_id := card.get("definitionStableId"):
                        affected_cards_by_capability[capability_id].add(definition_id)

            source_effects.append(
                {
                    "sourceEffectClassName": source_name,
                    "sourcePath": source_effect.get("sourcePath", source_record.get("sourcePath", "") if source_record else ""),
                    "requiredCapabilities": required_capabilities,
                    "nonVerifiedCapabilities": non_verified,
                    "affectedCards": affected_cards,
                }
            )

        is_executable = blocker.get("isExecutable", False) if blocker else False
        if blocking_capability_ids:
            is_executable = False

        graph_batches.append(
            {
                "batchId": batch["batchId"],
                "kind": batch.get("kind", ""),
                "category": batch.get("category", ""),
                "dependencyBatchIds": batch.get("dependencyBatchIds", []),
                "coarseCategoryDependencySatisfied": bool(batch.get("dependencyBatchIds")),
                "requiredCapabilityGateSatisfied": is_executable,
                "isExecutable": is_executable,
                "affectedCardCount": blocker.get("affectedCardCount", len(batch.get("cards", []))) if blocker else len(batch.get("cards", [])),
                "sourceEffectCount": len(source_effects),
                "sourceEffects": source_effects,
                "blockingCapabilities": blocking_capabilities,
            }
        )

        for capability_id in blocking_capability_ids:
            batches_by_capability[capability_id].add(batch["batchId"])

    for capability_id, status in sorted(capability_status.items()):
        capability_index[capability_id] = {
            "capabilityId": capability_id,
            "status": status,
            "sourceEffectCount": len(source_effects_by_capability[capability_id]),
            "affectedCardCount": len(affected_cards_by_capability[capability_id]),
            "cardBatchCount": len(batches_by_capability[capability_id]),
            "sourceEffectClassNames": sorted(source_effects_by_capability[capability_id]),
            "affectedDefinitionStableIds": sorted(affected_cards_by_capability[capability_id]),
            "cardBatchIds": sorted(batches_by_capability[capability_id]),
        }

    c0039 = next(batch for batch in graph_batches if batch["batchId"] == "C0039_zone_security_recovery")
    blocked_batches = [batch for batch in graph_batches if not batch["isExecutable"]]

    return {
        "schemaVersion": "dcgo.capability-dependency-graph.66D.v1",
        "inputs": {
            "capabilityRegistry": CAPABILITY_REGISTRY_PATH.as_posix(),
            "sourceRequiredCapabilities": SOURCE_REQUIRED_PATH.as_posix(),
            "batchCapabilityBlockers": BATCH_BLOCKERS_PATH.as_posix(),
            "fullCardPortingBatches": FULL_BATCH_MANIFEST_PATH.as_posix(),
        },
        "policy": {
            "cardBatchExecutableOnlyWhenAllRequiredCapabilitiesVerified": True,
            "coarseCategoryDependencyMayExecuteCardBatch": False,
            "blockerDocumentationCompletesCardPortingBatch": False,
            "sourceEffectRequiredCapabilitiesAreAuthoritative": True,
        },
        "summary": {
            "sourceEffectCount": source_required["summary"]["sourceEffectCount"],
            "sourceEffectsWithNonVerifiedCapabilities": source_required["summary"]["sourceEffectsWithNonVerifiedCapabilities"],
            "cardBatchCount": len(graph_batches),
            "blockedCardBatchCount": len(blocked_batches),
            "executableCardBatchCount": len(graph_batches) - len(blocked_batches),
            "c0039Executable": c0039["isExecutable"],
            "capabilityCount": len(capability_index),
        },
        "capabilities": list(capability_index.values()),
        "batches": graph_batches,
    }


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace", default=".", help="Repository workspace root.")
    args = parser.parse_args()
    workspace = Path(args.workspace).resolve()
    write_json(workspace / OUT_PATH, build_graph(workspace))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
