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
