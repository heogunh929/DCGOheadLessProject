# 52C resumable runner continuation

## 목적

`ScriptedScenarioRunner`와 `RandomLegalActionRunner`가 `PausedForDecision`을 반환한 뒤 같은 실행을 명시적으로 재개할 수 있는 runner-owned continuation/session API를 설계하고 구현한다.

## 제약

- mutable `EngineSession`을 그대로 public 반환하지 않는다.
- continuation은 pending `DecisionPoint`, `DecisionToken`, state/trace/rng/step index 같은 재개에 필요한 정보를 명시적으로 소유해야 한다.
- replay/determinism과 병렬 실행 안전성을 깨지 않아야 한다.
- 53 security timing parity는 이 항목 완료 전 실행하지 않는다.
