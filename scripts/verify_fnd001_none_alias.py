"""Verify FND-001-A EffectTiming.None alias accounting.

This verifier closes FND001-CS-02 only as evidence. It does not promote any
generated status, change Foundation Gate values, or implement CardEffect bodies.
"""

from __future__ import annotations

import argparse
import json
from datetime import datetime, timezone
from pathlib import Path
from typing import Any


CAPABILITY_REGISTRY = Path("docs/generated/capability-truth-audit/capability-registry.json")
SOURCE_REQUIRED_CAPABILITIES = Path("docs/generated/capability-truth-audit/source-required-capabilities.json")
BATCH_CAPABILITY_BLOCKERS = Path("docs/generated/capability-truth-audit/batch-capability-blockers.json")
FOUNDATION_GATE = Path("docs/generated/foundation-completion-gate.json")
OUT_JSON = Path("docs/generated/as-is-restart/fnd-001-a-none-alias-verification.json")


def load_json(workspace: Path, path: Path) -> Any:
    with (workspace / path).open("r", encoding="utf-8") as handle:
        return json.load(handle)


def write_json(path: Path, payload: dict[str, Any]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(payload, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def capability_id(value: dict[str, Any]) -> str | None:
    raw = value.get("capabilityId")
    return str(raw) if raw is not None else None


def contains_none(values: list[Any]) -> bool:
    return any(str(value) == "None" for value in values)


def build_report(workspace: Path) -> dict[str, Any]:
    registry = load_json(workspace, CAPABILITY_REGISTRY)
    source_required = load_json(workspace, SOURCE_REQUIRED_CAPABILITIES)
    batch_blockers = load_json(workspace, BATCH_CAPABILITY_BLOCKERS)
    gate = load_json(workspace, FOUNDATION_GATE)

    capabilities = registry.get("capabilities", [])
    raw_none_capabilities = [capability for capability in capabilities if capability_id(capability) == "None"]
    alias_owners = [
        capability
        for capability in capabilities
        if contains_none(list(capability.get("inventoryAliases", [])))
    ]
    continuous = next(
        (capability for capability in capabilities if capability_id(capability) == "ContinuousOrStaticEffect"),
        None,
    )

    source_effects = source_required.get("sourceEffects", [])
    source_required_raw_none = [
        effect.get("sourceScaffoldId")
        for effect in source_effects
        if contains_none(list(effect.get("requiredCapabilities", [])))
    ]
    source_non_verified_raw_none = [
        effect.get("sourceScaffoldId")
        for effect in source_effects
        if contains_none(list(effect.get("nonVerifiedCapabilities", [])))
    ]

    blocker_raw_none: list[str] = []
    for batch in batch_blockers.get("batches", []):
        for blocker in batch.get("blockingCapabilities", []):
            if capability_id(blocker) == "None":
                blocker_raw_none.append(str(batch.get("batchId")))

    gate_unsupported_raw_none = [
        capability
        for capability in gate.get("samples", {}).get("unsupportedCapabilities", [])
        if capability_id(capability) == "None"
    ]
    gate_partial_raw_none = [
        capability
        for capability in gate.get("samples", {}).get("partiallyImplementedCapabilities", [])
        if capability_id(capability) == "None"
    ]

    continuous_aliases = list(continuous.get("inventoryAliases", [])) if continuous else []
    continuous_status = str(continuous.get("status")) if continuous else None
    conditions = {
        "rawNoneCapabilityAbsent": len(raw_none_capabilities) == 0,
        "continuousAliasHasSingleOwner": len(alias_owners) == 1
        and capability_id(alias_owners[0]) == "ContinuousOrStaticEffect",
        "continuousAliasPresent": contains_none(continuous_aliases),
        "continuousStatusConservative": continuous_status == "PartiallyImplemented",
        "sourceRequiredRawNoneAbsent": len(source_required_raw_none) == 0,
        "sourceNonVerifiedRawNoneAbsent": len(source_non_verified_raw_none) == 0,
        "batchBlockerRawNoneAbsent": len(blocker_raw_none) == 0,
        "gateUnsupportedRawNoneAbsent": len(gate_unsupported_raw_none) == 0,
        "gatePartialRawNoneAbsent": len(gate_partial_raw_none) == 0,
        "openCodeStillFalse": gate.get("summary", {}).get("openCodeReady") is False,
    }

    passed = all(conditions.values())
    return {
        "schemaVersion": "dcgo.as-is-restart.fnd001a-none-alias-verification.v1",
        "generatedAt": datetime.now(timezone.utc).isoformat(),
        "goal": "FND-001-A EffectTiming.None alias verification",
        "inputs": {
            "capabilityRegistry": CAPABILITY_REGISTRY.as_posix(),
            "sourceRequiredCapabilities": SOURCE_REQUIRED_CAPABILITIES.as_posix(),
            "batchCapabilityBlockers": BATCH_CAPABILITY_BLOCKERS.as_posix(),
            "foundationGate": FOUNDATION_GATE.as_posix(),
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
            "id": "FND001-CS-02",
            "classification": "CloseableFoundationTask",
            "title": "EffectTiming.None alias gate accounting",
            "status": "ClosedByEvidence" if passed else "NeedsWork",
            "nextTaskCandidate": "FND001-CS-03",
        },
        "summary": {
            "passed": passed,
            "taskId": "FND001-CS-02",
            "rawNoneCapabilityEntryCount": len(raw_none_capabilities),
            "continuousAliasOwnerCount": len(alias_owners),
            "sourceRequiredRawNoneCount": len(source_required_raw_none),
            "sourceNonVerifiedRawNoneCount": len(source_non_verified_raw_none),
            "batchBlockerRawNoneCount": len(blocker_raw_none),
            "gateUnsupportedRawNoneCount": len(gate_unsupported_raw_none),
            "gatePartialRawNoneCount": len(gate_partial_raw_none),
        },
        "conditions": conditions,
        "continuousOrStaticEffect": {
            "capabilityId": capability_id(continuous) if continuous else None,
            "status": continuous_status,
            "inventoryAliases": continuous_aliases,
            "affectedCardCount": continuous.get("affectedCardCount") if continuous else None,
            "sourceFileCount": continuous.get("sourceFileCount") if continuous else None,
        },
        "gateSummary": {
            "openCodeReady": gate.get("summary", {}).get("openCodeReady"),
            "selectedNextFoundationCapability": gate.get("summary", {}).get("selectedNextFoundationCapability"),
            "selectedNextFoundationStatus": gate.get("summary", {}).get("selectedNextFoundationStatus"),
            "unsupportedCapabilityCount": gate.get("summary", {}).get("unsupportedCapabilityCount"),
            "partiallyImplementedCapabilityCount": gate.get("summary", {}).get("partiallyImplementedCapabilityCount"),
        },
        "samples": {
            "rawNoneCapabilities": raw_none_capabilities[:10],
            "sourceRequiredRawNone": source_required_raw_none[:10],
            "sourceNonVerifiedRawNone": source_non_verified_raw_none[:10],
            "batchBlockerRawNone": blocker_raw_none[:10],
            "gateUnsupportedRawNone": gate_unsupported_raw_none[:10],
            "gatePartialRawNone": gate_partial_raw_none[:10],
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
