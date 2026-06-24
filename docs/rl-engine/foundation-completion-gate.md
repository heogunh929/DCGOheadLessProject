# Foundation Completion Gate

이 문서는 `scripts/calculate_foundation_completion_gate.py`가 생성한 현재 foundation gate 결과다.
개별 CardEffect body 구현 가능 여부를 통과시키기 위한 문서가 아니라, OpenCode/local model에 넘기기 전에 남은 공통 foundation blocker를 보수적으로 드러내는 기준이다.

## 결과

- OpenCodeReady: `false`
- 통과 gate: 12
- 실패 gate: 2
- 다음 foundation capability 후보: `ContinuousOrStaticEffect` (`PartiallyImplemented`)

## Gate

| Gate | Status | Value | Expected |
| --- | --- | ---: | --- |
| 원본 Script/CardEffects inventory | `passed` | `{"effectSetMethodUsageCount": 102, "generatedScriptSourceFileCount": 436, "localSourceRootAvailable": true, "localSourceRootPath": "E:\\headlessDCGO\\DCGO\\Assets"}` | `> 0` |
| 원본 CardEffectFactory inventory | `passed` | `{"factoryApiUsageCount": 95, "localSourceRootAvailable": true, "localSourceRootPath": "E:\\headlessDCGO\\DCGO\\Assets"}` | `> 0` |
| 원본 CardEffectCommons inventory | `passed` | `{"commonsApiUsageCount": 263, "localSourceRootAvailable": true, "localSourceRootPath": "E:\\headlessDCGO\\DCGO\\Assets"}` | `> 0` |
| referenced common API unknown count | `passed` | `0` | `0` |
| referenced Unsupported capability count | `failed` | `5` | `0` |
| referenced PartiallyImplemented capability count | `failed` | `58` | `0` |
| runtime/generated status mismatch count | `passed` | `0` | `0` |
| silent no-op count | `passed` | `0` | `0` |
| blocked empty descriptor count | `passed` | `0` | `0` |
| false NoEffect count | `passed` | `0` | `0` |
| variant identity conflict count | `passed` | `0` | `0` |
| core CardId branch count | `passed` | `0` | `0` |
| direct zone mutation count | `passed` | `0` | `0` |
| source lock 변경 없음 | `passed` | `0` | `0` |

## 주요 실패 수치

- referenced common API unknown count: 0
- referenced Unsupported capability count: 5
- referenced PartiallyImplemented capability count: 58
- runtime/generated status mismatch count: 0
- legacy pilot runtime divergence count: 92
- silent no-op candidate count: 0
- blocked empty descriptor candidate count: 0
- legacy continuous-only empty descriptor count: 13
- core CardId branch candidate count: 0
- direct zone mutation candidate count: 0

## 입력

- `fullMechanicInventory`: `docs/generated/full-mechanic-inventory.json`
- `capabilityRegistry`: `docs/generated/capability-truth-audit/capability-registry.json`
- `statusMismatchReport`: `docs/generated/capability-truth-audit/status-mismatch-report.json`
- `sourceRequiredCapabilities`: `docs/generated/capability-truth-audit/source-required-capabilities.json`
- `fullCardPoolBaseline`: `docs/generated/full-card-pool-validation-baseline-65.json`
- `statusRegistry`: `docs/generated/full-card-source-scaffold/status-registry.json`
- `mechanicFirstScheduler`: `docs/generated/capability-truth-audit/mechanic-first-scheduler-66E.json`

## 다음 작업

`ContinuousOrStaticEffect`의 남은 공통 foundation 항목을 카드별 body 구현 없이 좁힌다. 현재 scheduler 기준으로 이 capability가 가장 많은 source/card batch를 막고 있다.
