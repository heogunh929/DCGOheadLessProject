# 66E_mechanic_first_goal_scheduler

이 queue는 66B/66C/66D 이후 card-porting batch를 계속 실행하지 않고, unresolved mechanic을 먼저 고르는 scheduler를 만든다.

## 목표

- blocked card batch를 건너뛰어 다음 card batch로 진행하지 않는다.
- blocker capability별 affected card 수를 계산한다.
- 가장 많은 카드를 막는 unresolved mechanic을 다음 구현 대상으로 선택한다.
- mechanic 구현 후 관련 card batches를 다시 `todo`로 여는 정책을 machine-readable artifact에 기록한다.
- card batch `done` 조건은 실제 effect body, registry/status 갱신, 테스트, replay, baseline blocker 감소를 모두 요구한다.

## 완료 조건

- `docs/generated/capability-truth-audit/mechanic-first-scheduler-66E.json`을 생성한다.
- scheduler는 `capability-dependency-graph-66D.json`과 queue 상태를 함께 읽는다.
- `C0039_zone_security_recovery`는 실행 가능 batch로 선택하지 않는다.
- selector는 66E 완료 후 `mechanic-remediation` decision을 반환한다.
- 공통 layer 미구현은 `blocked`로 분류하고, `needs-review`는 사용자 판단 또는 source 의미 불명확성에만 사용한다.
- 관련 테스트와 전체 regression이 통과해야 한다.
