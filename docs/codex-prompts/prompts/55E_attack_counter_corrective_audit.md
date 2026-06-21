# 55E Attack Counter Corrective Audit

AGENTS.md, `docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md`, 현재 `/goal`을 따른다.

## 목표

55D 이후 attack/counter 보정이 실제 코드와 테스트에 남아 있는지 확인하고, full-card-porting으로 넘어가기 전에 counter execution persistence와 target-switch stage resume 보정 상태를 queue에 고정한다.

## 수행 범위

- 56번 golden scenario를 새로 실행하지 않는다.
- commit과 push를 수행하지 않는다.
- `DCGO/Assets/Scripts` 원본은 수정하지 않는다.
- 카드별 effect body는 각 카드 파일에 유지하고, core service에 CardId 분기를 추가하지 않는다.

## 확인 항목

1. `AttackRuntimeContext.CounterUsed`가 counter 선택, optional, target selection 시점이 아니라 실제 counter body 실행 직후 persistent state로 기록되는지 확인한다.
2. counter body 이후 nested `RulesTiming` 또는 `AfterEffectsActivate` decision으로 pause/resume해도 같은 counter window에서 두 번째 counter가 실행되지 않는지 확인한다.
3. `OnAllyAttack`, non-counter `OnCounterTiming`, counter window, block trigger, target-switch drain이 원래 예정된 attack stage로 복귀하는지 확인한다.
4. `OnAttackTargetChanged` payload가 최종 attack context가 아니라 각 `AttackTargetSwitch` snapshot을 사용하는지 확인한다.
5. 55D 테스트와 문서가 위 동작을 고정하고 있는지 확인한다.

## 완료 조건

- 55D 관련 회귀 테스트가 존재한다.
- `docs/rl-engine/attack-timing-source-mapping.md`가 segment-local `ExecutedEffects`, persistent counter-used 기록, stage별 target-switch continuation을 설명한다.
- queue/progress에 55E corrective audit 결과가 기록된다.
- 전체 regression이 통과한다.
