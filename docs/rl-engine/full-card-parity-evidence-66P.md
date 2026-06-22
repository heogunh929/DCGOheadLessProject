# 66P Full-Card Parity Evidence

이 문서는 generated full-card source scaffold 전체에 대한 Unity/RL parity fixture coverage를 보수적으로 집계한다.
Unity fixture가 없으면 `NotRun`이며, `NotRun`은 parity pass가 아니다.

## Summary

- Source effects: 3918
- Affected definitions: 7922
- Passed: 0
- Failed: 0
- NotRun: 3918
- Compared: 0
- All generated source effects have Unity parity: `false`

## Policy

- Missing Unity fixture is `NotRun`.
- `NotRun` does not count as `Passed`.
- Synthetic comparer fixtures do not count as generated full-card Unity parity.
- This report does not allow card-porting or status promotion.

## Inputs

- `sourceScaffoldDir`: `docs/generated/full-card-source-scaffold/sources`
- `statusRegistry`: `docs/generated/full-card-source-scaffold/status-registry.json`
- `unityFixtureDir`: `docs/generated/parity-fixtures/unity/full-card-source`
- `rlFixtureDir`: `docs/generated/parity-fixtures/rl/full-card-source`
- `comparisonReportDir`: `docs/generated/parity-fixtures/reports/full-card-source`

## Next

Unity exporter fixtures and matching RL traces must be generated per scenario before any generated full-card parity can be claimed.
