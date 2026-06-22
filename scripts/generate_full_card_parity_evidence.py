"""Generate conservative full-card Unity/RL parity fixture coverage evidence.

This report does not execute card bodies and does not claim parity. It maps the
generated full-card source scaffold to expected Unity/RL parity fixture paths and
records whether a comparison result exists. Missing Unity fixtures are NotRun,
never Passed.
"""

from __future__ import annotations

import argparse
import json
import re
from collections import Counter
from pathlib import Path
from typing import Any


SOURCE_SCAFFOLD_DIR = Path("docs/generated/full-card-source-scaffold/sources")
STATUS_REGISTRY_PATH = Path("docs/generated/full-card-source-scaffold/status-registry.json")
UNITY_FIXTURE_DIR = Path("docs/generated/parity-fixtures/unity/full-card-source")
RL_FIXTURE_DIR = Path("docs/generated/parity-fixtures/rl/full-card-source")
REPORT_DIR = Path("docs/generated/parity-fixtures/reports/full-card-source")
OUT_JSON = Path("docs/generated/full-card-parity-evidence.json")
OUT_MD = Path("docs/rl-engine/full-card-parity-evidence-66P.md")

ALL_STATUSES = ("Passed", "Failed", "NotRun")


def load_json(path: Path) -> Any:
    return json.loads(path.read_text(encoding="utf-8"))


def write_json(path: Path, payload: Any) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(payload, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def source_records(workspace: Path) -> list[dict[str, Any]]:
    records: list[dict[str, Any]] = []
    for path in sorted((workspace / SOURCE_SCAFFOLD_DIR).glob("*.json")):
        payload = load_json(path)
        records.extend(payload.get("records", []))
    return records


def scenario_id(record: dict[str, Any]) -> str:
    raw = f"{record['sourceEffectClassName']}@{record['sourcePath']}"
    return "full-card-source:" + re.sub(r"[^A-Za-z0-9_.-]+", "_", raw).strip("_")


def scenario_slug(scenario: str) -> str:
    return re.sub(r"[^A-Za-z0-9_.-]+", "_", scenario).strip("_")


def comparison_status(report_path: Path) -> str | None:
    if not report_path.exists():
        return None
    payload = load_json(report_path)
    status = str(payload.get("status", ""))
    return status if status in ALL_STATUSES else "Failed"


def build_record(workspace: Path, record: dict[str, Any]) -> dict[str, Any]:
    scenario = scenario_id(record)
    slug = scenario_slug(scenario)
    unity_path = UNITY_FIXTURE_DIR / f"{slug}.parity.json"
    rl_path = RL_FIXTURE_DIR / f"{slug}.parity.json"
    report_path = REPORT_DIR / f"{slug}.comparison.json"

    missing_unity = not (workspace / unity_path).exists()
    missing_rl = not (workspace / rl_path).exists()
    missing_report = not (workspace / report_path).exists()
    status = comparison_status(workspace / report_path)
    reason = ""

    if missing_unity:
        coverage_status = "NotRun"
        reason = "Unity parity fixture file is missing."
    elif missing_rl:
        coverage_status = "NotRun"
        reason = "RL parity fixture file is missing."
    elif missing_report:
        coverage_status = "NotRun"
        reason = "Unity/RL comparison report is missing."
    else:
        coverage_status = status or "Failed"
        reason = "Comparison report exists."

    return {
        "sourceScaffoldId": record["sourceScaffoldId"],
        "sourceEffectClassName": record["sourceEffectClassName"],
        "sourcePath": record["sourcePath"],
        "sourceSetId": record.get("sourceSetId"),
        "scaffoldStatus": record.get("scaffoldStatus", "Unsupported"),
        "scenarioId": scenario,
        "unityFixturePath": unity_path.as_posix(),
        "rlFixturePath": rl_path.as_posix(),
        "comparisonReportPath": report_path.as_posix(),
        "coverageStatus": coverage_status,
        "coverageReason": reason,
        "missingUnityFixture": missing_unity,
        "missingRlFixture": missing_rl,
        "missingComparisonReport": missing_report,
        "affectedCardCount": record.get("affectedCardCount", 0),
        "affectedDefinitionStableIds": [
            card.get("definitionStableId")
            for card in record.get("affectedCards", [])
            if card.get("definitionStableId")
        ],
    }


def build_report(workspace: Path) -> dict[str, Any]:
    records = [build_record(workspace, record) for record in source_records(workspace)]
    counts = Counter(record["coverageStatus"] for record in records)
    for status in ALL_STATUSES:
        counts.setdefault(status, 0)

    status_registry = load_json(workspace / STATUS_REGISTRY_PATH)
    affected_definition_ids = sorted(
        {
            definition
            for record in records
            for definition in record["affectedDefinitionStableIds"]
        }
    )

    return {
        "schemaVersion": "dcgo.full-card-parity-evidence.66P.v1",
        "inputs": {
            "sourceScaffoldDir": SOURCE_SCAFFOLD_DIR.as_posix(),
            "statusRegistry": STATUS_REGISTRY_PATH.as_posix(),
            "unityFixtureDir": UNITY_FIXTURE_DIR.as_posix(),
            "rlFixtureDir": RL_FIXTURE_DIR.as_posix(),
            "comparisonReportDir": REPORT_DIR.as_posix(),
        },
        "policy": {
            "missingUnityFixtureStatus": "NotRun",
            "notRunCountsAsPass": False,
            "syntheticFixturesCountAsUnityParity": False,
            "cardPortingAllowedByThisReport": False,
            "manualParityPromotionAllowed": False,
        },
        "summary": {
            "sourceEffectCount": len(records),
            "affectedDefinitionCount": len(affected_definition_ids),
            "coverageStatusCounts": dict(sorted(counts.items())),
            "passedSourceEffectCount": counts["Passed"],
            "failedSourceEffectCount": counts["Failed"],
            "notRunSourceEffectCount": counts["NotRun"],
            "comparedSourceEffectCount": counts["Passed"] + counts["Failed"],
            "allGeneratedSourceEffectsHaveUnityParity": bool(records) and counts["Passed"] == len(records),
            "generatedImplementedOrVerifiedCount": status_registry.get("implementedOrVerifiedCount", 0),
            "generatedSourceScaffoldStatusCounts": status_registry.get("sourceScaffoldStatusCounts", {}),
        },
        "records": records,
    }


def render_markdown(report: dict[str, Any]) -> str:
    summary = report["summary"]
    lines = [
        "# 66P Full-Card Parity Evidence",
        "",
        "이 문서는 generated full-card source scaffold 전체에 대한 Unity/RL parity fixture coverage를 보수적으로 집계한다.",
        "Unity fixture가 없으면 `NotRun`이며, `NotRun`은 parity pass가 아니다.",
        "",
        "## Summary",
        "",
        f"- Source effects: {summary['sourceEffectCount']}",
        f"- Affected definitions: {summary['affectedDefinitionCount']}",
        f"- Passed: {summary['passedSourceEffectCount']}",
        f"- Failed: {summary['failedSourceEffectCount']}",
        f"- NotRun: {summary['notRunSourceEffectCount']}",
        f"- Compared: {summary['comparedSourceEffectCount']}",
        f"- All generated source effects have Unity parity: `{str(summary['allGeneratedSourceEffectsHaveUnityParity']).lower()}`",
        "",
        "## Policy",
        "",
        "- Missing Unity fixture is `NotRun`.",
        "- `NotRun` does not count as `Passed`.",
        "- Synthetic comparer fixtures do not count as generated full-card Unity parity.",
        "- This report does not allow card-porting or status promotion.",
        "",
        "## Inputs",
        "",
    ]
    for name, path in report["inputs"].items():
        lines.append(f"- `{name}`: `{path}`")
    lines.extend(
        [
            "",
            "## Next",
            "",
            "Unity exporter fixtures and matching RL traces must be generated per scenario before any generated full-card parity can be claimed.",
            "",
        ]
    )
    return "\n".join(lines)


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace", default=".", help="Repository workspace root.")
    args = parser.parse_args()
    workspace = Path(args.workspace).resolve()
    report = build_report(workspace)
    write_json(workspace / OUT_JSON, report)
    (workspace / OUT_MD).parent.mkdir(parents=True, exist_ok=True)
    (workspace / OUT_MD).write_text(render_markdown(report), encoding="utf-8")
    print(json.dumps(report["summary"], ensure_ascii=False, indent=2))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
