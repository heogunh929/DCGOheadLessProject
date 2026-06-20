# 54B trigger stack semantic hardening

목표: DCGO 원본 `AutoProcessing.TriggeredSkillProcess`와 `MultipleSkills`의 trigger batch 의미를 더 좁게 맞춘다.

- 빈 RulesTiming batch 또는 stale/CanActivate 실패만 있었던 batch에서는 `AfterEffectsActivate`를 수집하지 않는다.
- 같은 player group의 활성 `EffectResolution`이 2개 이상이면 optional/target selection 여부와 무관하게 먼저 ordering decision을 낸다.
- 모든 후보가 optional일 때만 ordering 단계의 전체 skip을 허용하고, 일부만 optional인 batch에서는 전체 skip을 금지한다.
- `RuleProcessor` state-only stabilization 중 발생한 rule event를 버리지 않고 nested trigger frame으로 drain한다.
- `AfterEffectsActivate` frame 이후 새 후보가 실제로 생기면 연쇄 frame을 허용하되, 동일 candidate signature 반복과 max depth로 무한 loop를 차단한다.
- source snapshot은 기본 `RequireSameRole`로 재검증하고, 원본상 이동 후 해소 가능한 효과는 카드별 descriptor의 `AllowTriggeredSourceMove` 정책으로 명시한다.
- core service 또는 catalog에 CardId 분기를 추가하지 않는다.
- DCGO Unity 원본은 수정하지 않는다.

완료 조건:

- 신규 54B regression과 전체 regression이 통과한다.
- `QUEUE_DCGO_FULL_CARD_POOL.md`, `PROGRESS_DCGO_FULL_CARD_POOL.md`, `after-effects-multiple-skills-source-mapping.md`, `effect-system.md`, `decision-and-selection.md`, `security-timing-source-mapping.md`, `validation-strategy.md`를 실제 구현과 맞게 갱신한다.
