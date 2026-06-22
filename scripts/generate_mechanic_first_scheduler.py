"""Generate the 66E mechanic-first scheduler decision artifact."""

from __future__ import annotations

import argparse
import json
from pathlib import Path
from typing import Any


GRAPH_PATH = Path("docs/generated/capability-truth-audit/capability-dependency-graph-66D.json")
QUEUE_PATH = Path("docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md")
OUT_PATH = Path("docs/generated/capability-truth-audit/mechanic-first-scheduler-66E.json")

CARD_BATCH_DONE_REQUIREMENTS = [
    "actual-effect-body",
    "registry-status-update",
    "tests",
    "replay",
    "baseline-blocker-reduction",
]


def load_json(path: Path) -> dict[str, Any]:
    return json.loads(path.read_text(encoding="utf-8"))


def write_json(path: Path, payload: dict[str, Any]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(payload, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def parse_queue_statuses(path: Path) -> dict[str, str]:
    statuses: dict[str, str] = {}
    for raw_line in path.read_text(encoding="utf-8").splitlines():
        line = raw_line.strip()
        if not line.startswith("|"):
            continue

        cells = [cell.strip() for cell in line.strip("|").split("|")]
        if len(cells) < 3 or cells[0] in {"Order", "---"}:
            continue

        statuses[Path(cells[2]).stem] = cells[1]

    return statuses


def build_scheduler(workspace: Path) -> dict[str, Any]:
    graph = load_json(workspace / GRAPH_PATH)
    queue_statuses = parse_queue_statuses(workspace / QUEUE_PATH)
    unresolved = [
        capability
        for capability in graph["capabilities"]
        if capability["status"] != "Verified"
        and (
            capability["affectedCardCount"] > 0
            or capability["sourceEffectCount"] > 0
            or capability["cardBatchCount"] > 0
        )
    ]
    unresolved.sort(
        key=lambda capability: (
            -capability["affectedCardCount"],
            -capability["sourceEffectCount"],
            -capability["cardBatchCount"],
            capability["capabilityId"],
        ),
    )

    selected = unresolved[0] if unresolved else None
    selected_mechanic = None
    if selected is not None:
        selected_mechanic = {
            **selected,
            "reopenCardBatchIdsAfterVerification": selected["cardBatchIds"],
            "selectionReason": "largest unresolved affectedCardCount, then sourceEffectCount and cardBatchCount",
        }

    blocked_batches = [batch for batch in graph["batches"] if not batch["isExecutable"]]
    blocked_todo_batches = [
        batch
        for batch in blocked_batches
        if queue_statuses.get(batch["batchId"]) == "todo"
    ]
    blocked_cursor = blocked_todo_batches[0] if blocked_todo_batches else (blocked_batches[0] if blocked_batches else None)
    blocker_summary = [
        {
            "capabilityId": capability["capabilityId"],
            "status": capability["status"],
            "affectedCardCount": capability["affectedCardCount"],
            "sourceEffectCount": capability["sourceEffectCount"],
            "cardBatchCount": capability["cardBatchCount"],
            "reopenCardBatchIds": capability["cardBatchIds"],
            "sourceEffectClassNames": capability["sourceEffectClassNames"],
        }
        for capability in unresolved
    ]

    c0039 = next(batch for batch in graph["batches"] if batch["batchId"] == "C0039_zone_security_recovery")

    next_action = None
    if selected_mechanic is not None:
        next_action = {
            "type": "implement-mechanic",
            "capabilityId": selected_mechanic["capabilityId"],
            "status": selected_mechanic["status"],
            "affectedCardCount": selected_mechanic["affectedCardCount"],
            "sourceEffectCount": selected_mechanic["sourceEffectCount"],
            "cardBatchCount": selected_mechanic["cardBatchCount"],
            "reopenCardBatchIdsAfterVerification": selected_mechanic["reopenCardBatchIdsAfterVerification"],
            "reason": "Select the unresolved capability blocking the largest number of affected card definitions.",
            "doNotExecuteCardBatchId": "C0039_zone_security_recovery",
        }

    return {
        "schemaVersion": "dcgo.mechanic-first-scheduler.66E.v1",
        "inputs": {
            "capabilityDependencyGraph": GRAPH_PATH.as_posix(),
            "queue": QUEUE_PATH.as_posix(),
        },
        "policy": {
            "blockedCardBatchDoesNotAdvanceToNextCardBatch": True,
            "selectMostAffectedUnresolvedCapability": True,
            "mechanicImplementationReopensAffectedCardBatches": True,
            "cardBatchDoneRequires": CARD_BATCH_DONE_REQUIREMENTS,
            "needsReviewOnlyFor": [
                "user-judgment-required",
                "source-body-missing-or-ambiguous",
            ],
            "commonLayerUnimplementedStatus": "blocked",
        },
        "summary": {
            "unresolvedCapabilityCount": len(unresolved),
            "selectedCapabilityId": selected["capabilityId"] if selected else None,
            "selectedAffectedCardCount": selected["affectedCardCount"] if selected else 0,
            "selectedSourceEffectCount": selected["sourceEffectCount"] if selected else 0,
            "selectedCardBatchCount": selected["cardBatchCount"] if selected else 0,
            "c0039Executable": c0039["isExecutable"],
            "cardBatchCount": len(graph["batches"]),
            "blockedCardBatchCount": len(blocked_batches),
            "blockedTodoCardBatchCount": len(blocked_todo_batches),
        },
        "selectedMechanic": selected_mechanic,
        "nextAction": next_action,
        "blockedCardBatchCursor": {
            "batchId": blocked_cursor["batchId"],
            "kind": blocked_cursor["kind"],
            "category": blocked_cursor["category"],
            "coarseCategoryDependencySatisfied": blocked_cursor["coarseCategoryDependencySatisfied"],
            "requiredCapabilityGateSatisfied": blocked_cursor["requiredCapabilityGateSatisfied"],
            "isExecutable": blocked_cursor["isExecutable"],
            "affectedCardCount": blocked_cursor["affectedCardCount"],
            "blockingCapabilities": blocked_cursor["blockingCapabilities"],
        } if blocked_cursor else None,
        "capabilityBlockers": blocker_summary,
    }


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace", default=".", help="Repository workspace root.")
    args = parser.parse_args()
    workspace = Path(args.workspace).resolve()
    write_json(workspace / OUT_PATH, build_scheduler(workspace))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
