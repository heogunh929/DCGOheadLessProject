# 66E Mechanic-First Scheduler

## 결정

`66E_mechanic_first_goal_scheduler`는 full-card-porting batch를 계속 밀지 않고 unresolved mechanic을 먼저 선택하는 queue 항목이다. 이 작업은 card effect body를 구현하지 않았고, `C0039_zone_security_recovery`를 실행 가능 후보로 만들지 않았다.

## 산출물

- Machine-readable scheduler: `docs/generated/capability-truth-audit/mechanic-first-scheduler-66E.json`
- 입력 graph: `docs/generated/capability-truth-audit/capability-dependency-graph-66D.json`
- 입력 queue: `docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md`
- selector: `scripts/select_next_full_card_porting_batch.py`

## 정책

- blocked card batch를 건너뛰어 다음 card batch로 진행하지 않는다.
- 모든 source `requiredCapabilities`가 `Verified`인 card batch만 실행 가능하다.
- common layer 미구현은 `blocked`로 분류한다.
- `needs-review`는 실제 사용자 판단 또는 source body/source 의미 불명확성에만 사용한다.
- card-porting batch `done` 조건은 actual effect body, registry/status 갱신, tests, replay, baseline blocker 감소다.
- mechanic 구현 후 관련 card batches를 다시 `todo`로 여는 후속 queue를 생성해야 한다.

## 현재 선택 결과

- 다음 decision: `mechanic-remediation`
- 막힌 card batch cursor: `C0039_zone_security_recovery`
- 선택된 unresolved mechanic: `ContinuousOrStaticEffect`
- 상태: `PartiallyImplemented`
- affected card count: 7867
- source effect count: 3896
- card batch count: 397

`ContinuousOrStaticEffect`가 실제 engine implementation, 실행 테스트, replay/invariant evidence로 `Verified`되기 전에는 C0039 이후 card-porting batch를 실행하지 않는다.
