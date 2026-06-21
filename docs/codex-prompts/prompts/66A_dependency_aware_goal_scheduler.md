# 66A Dependency-Aware Goal Scheduler

AGENTS.md, `docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md`, 현재 `/goal`을 따른다.

## 목표

generated full-card-porting subqueue가 dependency를 무시하고 card-porting batch를 계속 진행하지 못하게 한다. dependency가 끝나지 않은 card batch는 실행 대상이 아니며, 공통 layer 미구현은 `needs-review`가 아니라 `blocked`로 분류한다.

## 필수 정책

- `dependencyBatchIds`가 모두 `done`인 `todo` batch만 실행 가능하다.
- dependency가 `blocked` 또는 `needs-review`이면 dependent card batch를 건너뛴다.
- 공통 layer 미구현 상태는 `needs-review`가 아니라 `blocked`로 분류한다.
- `needs-review`는 실제 사용자 판단 또는 source body missing/ambiguous 같은 source 불명확성에만 사용한다.
- blocker 문서화만으로 card-porting batch를 완료 처리하지 않는다.
- card-porting 완료 조건은 실제 effect body, registry/status 갱신, 테스트, baseline blocker 감소다.
- 한 번에 queue 하나만 수행한다.
- scheduler가 unresolved mechanic dependency를 발견하면 card batch 대신 해당 dependency를 remediation target으로 보고하고 goal을 일시 정지한다.

## 구현 범위

- generated full-card-porting runner 지침을 dependency-aware 정책으로 갱신한다.
- machine-readable scheduler helper를 추가해 다음 실행 가능 batch 또는 가장 앞의 unresolved mechanic dependency를 계산한다.
- L0001~L0006 및 L0006에 막힌 C0007~C0025의 과거 상태를 `blocked`로 정리한다.
- C0026은 L0006이 `done`이 아닐 때 실행 대상이 아니어야 한다.
- 66A 완료 후 다음 실제 구현 대상은 C0026이 아니라 `L0006_zone_security_recovery`다.

## 완료 조건

- scheduler helper가 C0026을 executable로 반환하지 않고 L0006을 unresolved mechanic dependency로 보고한다.
- active runner 문서가 dependency-aware 정책과 card-porting 완료 조건을 설명한다.
- queue/progress에 66A 결과가 기록된다.
- 전체 regression이 통과한다.
- commit과 push를 수행하지 않는다.
