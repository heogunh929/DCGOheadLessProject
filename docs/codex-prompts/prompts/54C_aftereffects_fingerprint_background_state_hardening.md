# 54C aftereffects fingerprint/background state hardening

목표: 54B의 `AfterEffectsActivate` loop guard와 background frame 전환을 deterministic replay와 source-aligned trigger stack 의미에 맞게 보강한다.

범위:

- `AfterEffectsActivate` 반복 fingerprint에는 `GameState.ComputeStateHash()`, source card instance, source permanent, controller, timing/context payload를 포함한다.
- 같은 candidate라도 state가 달라졌다면 합법적인 재활성으로 허용한다.
- 같은 state와 같은 candidate set이 반복되면 self-loop로 보고 명시적으로 실패한다.
- foreground batch 실행 후 background frame으로 전환할 때 foreground의 `HadResolutionAttempt`를 보존한다.
- foreground가 실행됐고 background candidate가 stale/`CanActivate` 실패로 skip되더라도 `AfterEffectsActivate`는 정확히 한 번 예약되어야 한다.
- `RuleProcessor.StabilizeStateOnly`의 `RuleStabilizationEvent` coverage를 감사하고, 현재 지원 범위를 문서화한다.
- core service 또는 catalog에 CardId 분기를 추가하지 않는다.
- `DCGO/Assets/Scripts` 원본은 수정하지 않는다.

완료 조건:

- changed-state AfterEffects 재활성, stale background 후 AfterEffects 예약, DP 0 state-only event coverage 테스트를 추가한다.
- 전체 regression이 통과해야 한다.
- `QUEUE_DCGO_FULL_CARD_POOL.md`, `PROGRESS_DCGO_FULL_CARD_POOL.md`, `after-effects-multiple-skills-source-mapping.md`, `effect-system.md`, `validation-strategy.md`를 실제 구현과 맞게 갱신한다.
- commit과 push는 수행하지 않는다.
