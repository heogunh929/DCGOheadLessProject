# Validation Report Template

이 템플릿은 `EngineCompletionChecklistRunner` 결과를 사람이 검수하기 위한 형식이다.

## 메타데이터

- 대상 card pool/decklist:
- 실행 일시:
- 실행 commit 또는 local baseline:
- 실행 명령:
- remote 사용 여부:

## 전체 판정

- `IsComplete`:
- 실패 gate 수:
- 학습 단계 진입 가능 여부:

## Gate 결과

| Gate Id | 이름 | 결과 | 세부 내용 |
| --- | --- | --- | --- |
| `forbidden-dependencies` | RL.Engine forbidden dependency check |  |  |
| `target-card-pool-documented` | Target card pool CardEffect status documented |  |  |
| `target-deck-validation` | Target decklist validates without unsupported mechanics |  |  |
| `unsupported-mechanic-zero` | Unsupported mechanic zero check |  |  |
| `golden-scenario-suite` | Golden scripted scenarios |  |  |
| `replay-determinism` | Replay determinism |  |  |
| `invariant-fuzz` | Invariant fuzz |  |  |

## Target Card Pool Report

| CardId | EffectClassName | Status | Missing Layers | Notes |
| --- | --- | --- | --- | --- |

## Unsupported Mechanic Report

- Unsupported cards:
- Deck validation errors:
- Missing layers:

## Scenario Suite Report

| Scenario | 결과 | 세부 내용 |
| --- | --- | --- |

## Replay Determinism Report

| Scenario | 결과 | Expected Hash | Actual Hash | 세부 내용 |
| --- | --- | --- | --- | --- |

## Invariant Fuzz Report

| Run | 결과 | 세부 내용 |
| --- | --- | --- |

## DCGO 원본 변경 여부

- `DCGO/` 변경:
- `DCGO/Assets/Scripts` 변경:
- 원본 수정이 있다면 사유:

## 테스트 결과

- 실행 명령:
- 결과:
- 경고:
- 실패 원인:

## 다음 조치

- Missing layer 우선순위:
- 다음 prompt:
- 사용자 결정 필요 사항:

## Gate Scope 해석 규칙

보고서 작성자는 아래 항목을 반드시 명시한다.

- Gate scope: `ST1 target deck`, `ST1-ST3 target pool`, `whole engine` 중 하나.
- Runner type: `EngineCompletionChecklistRunner`, `TargetCardPoolValidator`, `문서 감사`, 또는 기타 정확한 runner.
- `IsComplete` 적용 범위: `IsComplete=true`는 실행한 request scope에만 유효하다.
- 학습 단계 진입 가능 여부: 기본값은 `불가`이며, whole-engine completion과 사용자 승인이 모두 없으면 `가능`으로 쓰지 않는다.

ST1 target deck 통과와 ST1-ST3 target pool validation 통과는 서로 다른 근거다. 둘 중 어느 것도 단독으로 전체 DCGO 엔진 완성 또는 RL 학습 단계 진입을 의미하지 않는다.
