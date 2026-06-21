# 55D Counter Execution Persistence And Target-Switch Stage Resume Correction

목표: 55C 이후 발견된 counter 실행 persistence와 attack target-switch stage 복귀 문제를 source-aligned하게 보정한다.

## 범위

- 56 golden scenarios는 실행하지 않는다.
- commit과 push는 수행하지 않는다.
- `DCGO/Assets/Scripts` 원본은 수정하지 않는다.
- 카드별 effect body는 각 카드 파일에 유지하고, core service에 CardId 분기를 추가하지 않는다.

## 필수 보정

1. `TriggerPipelineResult.ExecutedEffects`가 resume segment-local이라는 점을 반영한다.
2. counter 후보 선택, optional yes/no, target selection 전에는 `CounterUsed=false`를 유지한다.
3. 실제 counter effect body가 실행된 즉시 `AttackRuntimeContext.CounterUsed`에 기록한다.
4. 이 기록은 nested `RulesTiming`/`AfterEffectsActivate` decision pause/resume 이후에도 유지되어야 한다.
5. counter가 실제 사용된 뒤에는 같은 counter window의 다음 후보와 non-turn group counter가 실행되지 않아야 한다.
6. target switch 후 복귀 stage를 분리한다.
   - `OnAllyAttack` switch drain 후 `CounterNonCounter`
   - non-counter `OnCounterTiming` switch drain 후 counter `OnCounterTiming`
   - 실제 counter window switch drain 후 block designation
   - block trigger switch drain 후 battle
7. A -> B -> C target switch event payload 순서와 non-block switch `Blocker=null` 의미를 유지한다.

## 테스트

- counter 선택 직후 및 target selection 전 `CounterUsed=false`
- optional decline과 CanActivate revalidation failure가 `CounterUsed`로 기록되지 않음
- counter body 후 nested `RulesTiming` decision pending 시 `CounterUsed=true`
- counter body 후 nested `AfterEffectsActivate` decision pending 시 `CounterUsed=true`
- nested decision resume 후 두 번째 counter 및 non-turn counter 미실행
- OnAllyAttack target switch 후 non-counter/counter stage 실행
- non-counter target switch 후 counter stage 실행
- block designation exactly once
- replay/state hash deterministic
- 전체 regression

## 문서

- `docs/rl-engine/attack-timing-source-mapping.md`
- `docs/codex-prompts/state/QUEUE_DCGO_FULL_CARD_POOL.md`
- `docs/codex-prompts/state/PROGRESS_DCGO_FULL_CARD_POOL.md`
