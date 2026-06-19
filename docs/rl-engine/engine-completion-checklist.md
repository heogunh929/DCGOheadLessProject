# 엔진 완성 체크리스트

최신 기준일: 2026-06-14  
대상: ST1 target deck validation gate  
현재 판정: ST1 target deck 기준 통과

이 체크리스트는 학습 단계 진입 전 검증 기준을 관리한다. 현재 통과 판정은 ST1 target deck 기준이며, 전체 DCGO 카드풀/룰 완성을 의미하지 않는다.

## 최신 상태 요약 - 2026-06-19

- ST1 target deck gate: 통과.
- 최신 source-aligned 구조 guard 기록: `All 225 tests passed.`
- 과거 ST1 완료 시점 테스트 기록 `All 170 tests passed.`는 historical checkpoint 기록이며 최신 구조 guard 수와 다르다.
- ST1~ST3 registry snapshot은 별도 상태표에서 관리한다. shared `ST1_06` mapping은 asset registry validator로 고정됐고, `ST3-02` P2 source body 미확인은 ST1 gate 통과와 별개의 needs-review로 남아 있다.

## 최신 Gate 결과

| Gate | 결과 | 근거 |
| --- | --- | --- |
| `forbidden-dependencies` | 통과 | RL.Engine에 UnityEngine/Photon/MonoBehaviour/GameObject/Coroutine/UI 의존성 없음 |
| `target-card-pool-documented` | 통과 | ST1-01부터 ST1-16까지 status 문서화 |
| `target-deck-validation` | 통과 | ST1 target deck에 `Unsupported`/`PartiallyImplemented` 없음 |
| `unsupported-mechanic-zero` | 통과 | ST1 target deck 기준 unsupported/partial report가 비어 있음 |
| `selection-result-application` | 통과 | ST1-08/13/15/16 selection request/result 검증 유지 |
| `duration-cleanup` | 통과 | DP/SecurityAttack/SecurityDigimonDP temporary modifier cleanup 검증 유지 |
| `security-option-execution` | 통과 | ST1-12 play-self tamer, ST1-13/14 direct security body, ST1-15/16 Activate Main Option 검증 |
| `full-trigger-pipeline` | 통과/범위 제한 명시 | ST1 대상 timing은 통과, full MultipleSkills priority는 향후 범위 |
| `continuous-effect` | 통과 | ST1-01/03/11/12 continuous effect 검증 |
| `golden-scenario-suite` | 통과 | 기존 minimal golden scenarios 유지 |
| `replay-determinism` | 통과 | replay 가능한 subset과 state hash determinism 유지 |
| `invariant-fuzz` | 통과 | zone/permanent/modifier invariant 유지 |

## ST1 Completion 요약

- ST1 target deck validation: 통과
- Unsupported card/effect: 0
- PartiallyImplemented card/effect: 0
- Failed gate: 0
- historical ST1 checkpoint 테스트 결과: `All 170 tests passed.`
- 최신 구조 guard 테스트 결과: `All 225 tests passed.`

## 통과 범위의 의미

ST1 target deck의 현재 card effect와 관련 gate는 통과한다. 그러나 아래 항목은 전체 DCGO 엔진 완성을 위해 여전히 검토해야 한다.

- full `MultipleSkills` simultaneous trigger priority와 UI 선택 순서
- `BeforePayCost` / `AfterPayCost`
- `OnCounterTiming`, `OnAttackTargetChanged`, `OnEndBlockDesignation`
- blocker/counter 세부 흐름의 end-to-end integration
- 더 넓은 카드풀의 continuous/replacement/cut-in 효과
- Unity 원본 trace와 RL.Engine trace 비교 harness

## 학습 단계 진입 제한

ST1 gate 통과만으로 RL 학습 구성으로 바로 넘어가지 않는다. `ObservationEncoder`, `RewardCalculator`, `DatasetExporter`, `Trainer`, RL Environment API는 전체 엔진 완성 기준과 사용자 승인이 있기 전까지 구현하지 않는다.

## Queue 45 Gate 범위 정합성 - 2026-06-15

현재 completion 관련 결과는 아래 세 범위를 분리해서 해석한다.

| 범위 | runner/근거 | 현재 결과 | 해석 |
| --- | --- | --- | --- |
| ST1 target deck | `EngineCompletionChecklistRunner` / `ValidationHarnessV2 completion gate reports ST1 complete` | 통과, failed gate 0 | ST1 target deck에 한정된 completion gate |
| ST1~ST3 target pool | `TargetCardPoolValidator` / `ST1-ST3 target pool validation passes` | 통과, 48장 registry/deck validation 기준 | 카드풀 registry/status/file validation 통과이며 전체 엔진 completion gate가 아님 |
| 전체 엔진 completion | 별도 whole-engine `EngineCompletionChecklistRunner` request 없음 | 미실행 / 검증 필요 | RL 학습 단계 진입 근거로 사용할 수 없음 |

`EngineCompletionReport.IsComplete`는 request scope에 한정한다. ST1 request의 `IsComplete=true`를 ST1~ST3 전체 completion 또는 전체 DCGO 엔진 completion으로 재사용하지 않는다.

학습 단계 진입 상태: 불가. ST1 및 ST1~ST3 target validation이 통과하더라도 전체 엔진 completion request, Unity trace parity, 확장 golden scenario 근거가 없으면 RL 학습 구성으로 넘어가지 않는다.
