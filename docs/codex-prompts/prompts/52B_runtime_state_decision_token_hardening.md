# 52B runtime state and decision token hardening

## 목적

`EngineSession` pause/resume 구조를 병렬 실행과 deterministic replay에 안전하게 만든다.

## 완료 기준

- once-per-turn 사용 이력은 service graph 전역 mutable state가 아니라 `GameState`가 소유하는 rule-visible runtime state에 저장된다.
- runtime rule state는 `Clone`, `RestoreFrom`, `ComputeStateHash`에 포함된다.
- 직접 동기 API pending rollback은 runtime rule state까지 복구한다.
- public resume 경로는 `DecisionResult(Player, DecisionToken, SelectionResult)`를 요구한다.
- selection trace와 replay는 기록된 `DecisionToken`을 사용한다.
- stale token, wrong player, wrong request id는 replay에서도 실패한다.
- 전체 테스트가 통과한다.

## 범위 밖

- 53 security timing parity는 실행하지 않는다.
- 재개 가능한 runner-owned continuation/session API는 queue 52C에서 다룬다.
