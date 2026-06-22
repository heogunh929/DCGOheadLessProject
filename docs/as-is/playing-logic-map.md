# Playing Logic Map

- 생성 시각: `2026-06-23T05:37:16+09:00`

- 목적: 카드 효과 구현 전에 알아야 할 실제 플레이 로직 위치와 headless 대응 gap을 1차 정리한다.

| Logic Area | Original files | Main classes | Current headless counterpart | Gap | Priority |
| --- | --- | --- | --- | --- | --- |
| 게임 상태 | DCGO/Assets/Scripts/Script/GameContext.cs<br>DCGO/Assets/Scripts/Script/Player.cs<br>DCGO/Assets/Scripts/Script/CardSource.cs<br>DCGO/Assets/Scripts/Script/Permanent.cs<br>DCGO/Assets/Scripts/Script/CEntity_Base.cs | GameContext<br>Player<br>CardSource<br>Permanent<br>CEntity_Base | GameState<br>PlayerState<br>CardInstance<br>PermanentState<br>CardDefinition | 기본 state model은 있으나 Unity 원본의 CardEffect-facing convenience API와 zone/owner 관계 facade가 완전히 1:1은 아니다. | Tier0 |
| 게임 진행 루프 | DCGO/Assets/Scripts/Script/TurnStateMachine.cs<br>DCGO/Assets/Scripts/Script/MainPhaseAction/**/*.cs | TurnStateMachine<br>MainPhaseAction<br>PlayCardClass<br>PlayPermanentClass | GameSetupService<br>LegalActionGenerator<br>PlayCardService<br>DigivolveService | main phase action과 phase coroutine 흐름이 headless action/decision 흐름으로 분해되어 있어 source parity 문서와 facade contract가 필요하다. | Tier2 |
| 카드 플레이/진화/옵션 사용 | DCGO/Assets/Scripts/Script/CardController.cs<br>DCGO/Assets/Scripts/Script/CardEffectCommons.cs<br>DCGO/Assets/Scripts/Script/MainPhaseAction/**/*.cs | CardController<br>PlayCardClass<br>PlayPermanentClass<br>CardEffectCommons | PlayCardService<br>DigivolveService<br>CostResolver<br>Tier1PrimitiveService | pay cost, BeforePayCost/AfterPayCost, option lifecycle, security option, enter-field trigger 연결이 여러 service로 분산되어 있다. | Tier1 |
| 자동 처리/트리거/룰 처리 | DCGO/Assets/Scripts/Script/AutoProcessing.cs<br>DCGO/Assets/Scripts/Script/MultipleSkills.cs<br>DCGO/Assets/Scripts/Script/ICardEffect.cs<br>DCGO/Assets/Scripts/Script/CEntity_EffectController.cs<br>DCGO/Assets/Scripts/Script/CardEffectCommons.cs | AutoProcessing<br>MultipleSkills<br>ICardEffect<br>ActivateICardEffect<br>CEntity_EffectController | TriggerCollector<br>TriggerPipelineService<br>EffectDescriptor<br>StaticEffectService<br>DurationCleanupService | StackSkillInfos/GetSkillInfos/ActivateEffectProcess의 수집 범위와 순서가 headless trigger pipeline에서 명시 검증되어야 한다. | Tier0 |
| 공격/블록/카운터/배틀/시큐리티 체크 | DCGO/Assets/Scripts/Script/AttackProcess.cs<br>DCGO/Assets/Scripts/Script/CardEffectCommons.cs | AttackProcess<br>IBattle<br>ISecurityCheck | AttackService<br>BattleRules<br>BattleKeywordService<br>AttackRuntimeContext | 공격 상태 머신은 존재하나 Counter/Block timing, security digimon, attack cleanup source parity를 더 촘촘히 묶어야 한다. | Tier2 |
| 카드 효과 바인딩 | DCGO/Assets/Scripts/Script/CEntity_Base.cs<br>DCGO/Assets/Scripts/Script/CEntity_EffectController.cs<br>DCGO/Assets/Scripts/Script/CEntity_Effect.cs<br>DCGO/Assets/Scripts/CardEffect/**/*.cs | CEntity_Base<br>CEntity_EffectController<br>CEntity_Effect | CardDefinition<br>EffectDescriptor<br>TriggerCollector<br>StaticEffectService | CardEffectClassName -> namespace/type lookup 규칙과 fallback 실패 조건을 headless registry 계약으로 고정해야 한다. | Tier0 |

## 해석

- Unity 원본은 `Coroutine`, `GManager.instance`, UI outline, Photon 분기와 실제 룰 처리가 한 파일 안에 섞인 경우가 많다.

- headless 대응은 이미 여러 service/domain model로 분해되어 있으므로, 카드별 효과를 옮기기 전에 CardEffect-facing facade와 unsupported 실패 조건을 먼저 고정해야 한다.

- 특히 `AutoProcessing`, `CEntity_EffectController`, `CardEffectCommons`, `CardEffectFactory`가 카드별 효과 구현의 병목이다.
