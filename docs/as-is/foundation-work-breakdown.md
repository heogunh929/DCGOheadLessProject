# Foundation Work Breakdown

- 생성 시각: `2026-06-23T05:37:16+09:00`

- 목적: 카드별 `CardEffect` 구현 전에 필요한 기반 작업을 Tier별로 산정한다.

| Work item | Tier | Original source | Needed for CardEffect? | Current status | Estimated size | Blocked by | Done condition |
| --- | --- | --- | --- | --- | --- | --- | --- |
| ICardEffect / ActivateICardEffect 대응 모델 | Tier0 | ICardEffect.cs, CEntity_Effect.cs | 필수 | Exists | M | - | EffectDescriptor/Trigger 계약이 원본 timing/source/use-count 의미를 문서와 테스트로 고정 |
| EffectTiming / EffectDuration 대응 | Tier0 | ICardEffect.cs, CardEffectCommons.cs | 필수 | Exists | S | - | 원본 timing enum과 headless enum/alias 차이가 명시됨 |
| CardSource / card instance facade | Tier0 | CardSource.cs | 필수 | Exists | L | CardDefinition | CardEffect가 쓰는 Owner, PermanentOfThisCard, EffectList 계열 API 대응 |
| Permanent / field permanent facade | Tier0 | Permanent.cs | 필수 | Exists | L | ZoneMover | TopCard, DP, stack, digivolution cards, battle/breeding 존재 판정 대응 |
| Player zone/state facade | Tier0 | Player.cs | 필수 | Exists | M | CardSource/Permanent | hand/security/trash/field/effect list 수집 범위가 deterministic state로 대응 |
| GameContext turn/player/memory facade | Tier0 | GameContext.cs | 필수 | Exists | M | PlayerState | TurnPlayer/NonTurnPlayer/Memory/phase 조회가 headless service에서 통일 |
| CEntity_Base card definition 대응 | Tier0 | CEntity_Base.cs | 필수 | Exists | M | asset registry | CardEffectClassName, colors, level, cost, traits 등 CardEffect-facing metadata 고정 |
| CEntity_EffectController binding registry | Tier0 | CEntity_EffectController.cs | 필수 | Exists | M | EffectDescriptor | ClassName lookup/fallback/added effect collection 규칙이 실패 명시 방식으로 대응 |
| CardEffectCommons facade inventory | Tier0 | CardEffectCommons.cs, CardEffectCommons/**/*.cs | 필수 | Exists | XL | state facade | 상위 참조 helper부터 primitive/service 대응 표와 unsupported 명시 |
| CardEffectFactory primitive descriptor | Tier0 | CardEffectFactory/**/*.cs | 필수 | Exists | L | EffectDescriptor | factory helper가 deterministic EffectDescriptor/primitive로 대응 |
| AutoProcessing trigger collection | Tier1 | AutoProcessing.cs | 필수 | Partial | L | ICardEffect model | StackSkillInfos/GetSkillInfos/TriggeredSkillProcess 순서와 수집 범위 재현 |
| selection request/result facade | Tier1 | SelectCardEffect.cs, SelectPermanentEffect.cs, SelectCountEffect.cs | 필수 | Exists | M | DecisionPoint | Unity UI 선택이 RL/CLI SelectionRequest로 분리 |
| zone movement primitive coverage | Tier1 | CardEffectCommons.cs, Player.cs, Permanent.cs | 필수 | Partial | L | ZoneMover | trash/security/hand/deck/field 이동이 공통 primitive만 사용 |
| play/digivolve/option lifecycle | Tier1 | CardController.cs, MainPhaseAction/**/*.cs | 필수 | Partial | XL | CostResolver | BeforePayCost/AfterPayCost/OnEnterField/security option 흐름 대응 |
| security skill execution | Tier1 | AutoProcessing.cs, AttackProcess.cs | 필수 | Partial | M | TriggerPipeline | face-up security, security option, security digimon 처리 순서 대응 |
| AttackProcess state machine | Tier2 | AttackProcess.cs | 필수 | Partial | L | BattleRules | Counter/Block/Battle/End/Cleanup 상태와 trigger timing 대응 |
| MultipleSkills nested resolution | Tier2 | MultipleSkills.cs | 필수 | Partial | M | TriggerPipeline | 중첩 효과 처리와 IsUsing stack semantics 대응 |
| rule process / DP 0 deletion | Tier2 | AutoProcessing.cs | 필수 | Partial | M | StaticEffectService | 룰 처리와 상태 기반 삭제/공격 종료 조건 재현 |
| MainPhaseAction legal action flow | Tier2 | MainPhaseAction/**/*.cs | 필수 | Partial | L | LegalActionGenerator | 원본 클릭/action 후보와 headless LegalAction 차이 문서화 |
| Unity visual/network separation | Later | CardObjectController.cs, GManager, UI panels | 간접 | UnityOnly | M | - | RL.Engine 금지 의존성은 UnityAdapter 또는 CLI rendering으로 격리 |

## Tier 요약

- Tier0: CardEffect가 컴파일/실행 의미를 잃지 않기 위한 state, effect, metadata, helper facade 기반.

- Tier1: 실제 효과 실행과 기본 검증에 필요한 trigger, selection, zone movement, play/option lifecycle 기반.

- Tier2: 전투/복합 mechanic/룰 처리/메인 phase action parity를 완성하기 위한 기반.
