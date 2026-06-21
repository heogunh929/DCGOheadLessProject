"""Select the next full-card porting batch with dependency awareness.

This helper is intentionally conservative. It does not mutate queue state; it
only reports what may run and why earlier todo rows are blocked or skipped.
"""

from __future__ import annotations

import argparse
import json
from dataclasses import dataclass
from pathlib import Path
from typing import Any


QUEUE_PATH = Path("docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md")
MANIFEST_PATH = Path("docs/generated/full-card-porting-batches-66.json")

BLOCKING_DEPENDENCY_STATES = {"blocked", "needs-review"}
TERMINAL_STATES = {"done", "blocked", "needs-review"}


@dataclass(frozen=True)
class QueueRow:
    order: str
    status: str
    prompt_file: str
    kind: str
    category: str
    cards: int
    sources: int
    purpose: str

    @property
    def batch_id(self) -> str:
        return Path(self.prompt_file).stem


def parse_queue(path: Path) -> list[QueueRow]:
    rows: list[QueueRow] = []
    for raw_line in path.read_text(encoding="utf-8").splitlines():
        line = raw_line.strip()
        if not line.startswith("|"):
            continue
        cells = [cell.strip() for cell in line.strip("|").split("|")]
        if len(cells) < 8 or not cells[0].isdigit():
            continue
        rows.append(
            QueueRow(
                order=cells[0],
                status=cells[1],
                prompt_file=cells[2],
                kind=cells[3],
                category=cells[4],
                cards=int(cells[5]),
                sources=int(cells[6]),
                purpose=cells[7],
            )
        )
    return rows


def load_manifest(path: Path) -> dict[str, Any]:
    return json.loads(path.read_text(encoding="utf-8"))


def row_payload(row: QueueRow, dependencies: list[str] | None = None) -> dict[str, Any]:
    return {
        "order": row.order,
        "batchId": row.batch_id,
        "status": row.status,
        "promptFile": row.prompt_file,
        "kind": row.kind,
        "category": row.category,
        "cards": row.cards,
        "sources": row.sources,
        "purpose": row.purpose,
        "dependencyBatchIds": dependencies or [],
    }


def select_next(workspace: Path) -> dict[str, Any]:
    queue_path = workspace / QUEUE_PATH
    manifest_path = workspace / MANIFEST_PATH
    rows = parse_queue(queue_path)
    manifest = load_manifest(manifest_path)
    batch_by_id = {batch["batchId"]: batch for batch in manifest["batches"]}
    row_by_id = {row.batch_id: row for row in rows}
    status_by_id = {row.batch_id: row.status for row in rows}
    skipped: list[dict[str, Any]] = []
    first_unresolved_dependency: dict[str, Any] | None = None

    for row in rows:
        if row.status != "todo":
            continue

        batch = batch_by_id.get(row.batch_id)
        if batch is None:
            skipped.append(
                {
                    **row_payload(row),
                    "reason": "manifest-missing",
                    "message": "Queue row does not have a matching generated manifest batch.",
                }
            )
            continue

        dependencies = list(batch.get("dependencyBatchIds", []))
        dependency_statuses = {
            dependency: status_by_id.get(dependency, "missing")
            for dependency in dependencies
        }
        unresolved = [
            dependency
            for dependency, status in dependency_statuses.items()
            if status != "done"
        ]
        if unresolved:
            skipped.append(
                {
                    **row_payload(row, dependencies),
                    "reason": "dependencies-not-done",
                    "dependencyStatuses": dependency_statuses,
                    "message": "Only todo batches whose dependencyBatchIds are all done are executable.",
                }
            )
            for dependency in unresolved:
                dependency_row = row_by_id.get(dependency)
                if dependency_row is None:
                    continue
                if dependency_row.kind == "mechanic-layer" and first_unresolved_dependency is None:
                    first_unresolved_dependency = {
                        **row_payload(
                            dependency_row,
                            list(batch_by_id.get(dependency, {}).get("dependencyBatchIds", [])),
                        ),
                        "blockingDependentBatchId": row.batch_id,
                    }
            continue

        if first_unresolved_dependency is not None:
            return {
                "decision": "blocked-dependency",
                "selected": first_unresolved_dependency,
                "skipped": skipped,
                "firstUnresolvedMechanicDependency": first_unresolved_dependency,
                "policy": scheduler_policy(),
            }

        return {
            "decision": "executable",
            "selected": row_payload(row, dependencies),
            "skipped": skipped,
            "firstUnresolvedMechanicDependency": first_unresolved_dependency,
            "policy": scheduler_policy(),
        }

    decision = "blocked-dependency" if first_unresolved_dependency is not None else "none"
    return {
        "decision": decision,
        "selected": first_unresolved_dependency,
        "skipped": skipped,
        "firstUnresolvedMechanicDependency": first_unresolved_dependency,
        "policy": scheduler_policy(),
    }


def scheduler_policy() -> dict[str, Any]:
    return {
        "requiresAllDependencyBatchIdsDone": True,
        "skipCardBatchWhenDependencyBlockedOrNeedsReview": True,
        "commonLayerUnimplementedStatus": "blocked",
        "needsReviewAllowedFor": [
            "user-judgment-required",
            "source-body-missing-or-ambiguous",
        ],
        "blockerDocumentationCompletesCardPortingBatch": False,
        "cardPortingDoneRequires": [
            "actual-effect-body",
            "registry-status-update",
            "tests",
            "baseline-blocker-reduction",
        ],
        "oneQueueItemAtATime": True,
    }


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace", default=".", help="Repository workspace root.")
    args = parser.parse_args()
    result = select_next(Path(args.workspace).resolve())
    print(json.dumps(result, ensure_ascii=False, indent=2))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
