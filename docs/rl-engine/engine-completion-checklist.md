# 엔진 완성 체크리스트

최신 기준일: 2026-06-13  
대상: ST1 target deck validation gate  
현재 판정: ST1 target deck 기준 통과

이 체크리스트는 학습 단계 진입 전 검증 기준을 관리한다. 현재 통과 판정은 ST1 target deck 기준이며, 전체 DCGO 카드풀/룰 완성을 의미하지 않는다.

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
- 테스트 결과: `All 170 tests passed.`

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

