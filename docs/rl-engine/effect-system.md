# Effect System Foundation

## Queue 54A Trigger Stack Frame 보정

`TriggerPipelineService`의 trigger drain 단위는 flat queue tail이 아니라 `TriggerStackFrame`이다. frame은 현재 timing/context, 현재 batch remaining effects, background effects, parent frame, ordering state, AfterEffectsActivate scheduling state를 보존한다. nested `RulesTiming`/`AfterEffectsActivate` frame은 parent frame의 remaining tail과 concat되지 않으며, child frame이 종료된 뒤 parent frame으로 복귀한다.

`EffectDescriptor`와 `EffectResolution`에는 `TriggerSourceSnapshot`이 추가되었다. snapshot은 trigger 당시 source role과 zone, source permanent, top card, owner/controller를 보존한다. 실행 직전 source role별 위치를 재검증하고 `CanActivate`를 다시 평가한다. 이 때문에 후보 snapshot에는 남아 있지만 source가 Hand/Trash/Security/Inherited/Linked/FieldTop 역할에서 이탈한 effect는 silent no-op이 아니라 activation skip으로 처리된다.

effect body 하나가 완료되면 production graph의 `RuleProcessor.StabilizeStateOnly` delegate가 먼저 실행되고, 이어 새 `RulesTiming` 후보가 있으면 nested frame으로 drain된다. `TriggerPipelineService`가 `RuleProcessor`를 생성하지 않고 `BattleEngineServices`가 delegate를 연결하므로 core service 간 순환 생성 의존성은 만들지 않는다.

`AfterEffectsActivate`는 effect body마다 실행되지 않고 현재 batch가 끝난 뒤 frame으로 수집된다. 직접 self-recursive AfterEffectsActivate는 무한 stack이 되지 않도록 depth guard를 둔다.

최신 기준일: 2026-06-14

이 문서는 Unity 원본 `ICardEffect`, `AutoProcessing`, `SkillInfo`, `MultipleSkills`, `CardController` 흐름을 RL.Engine의 headless effect system으로 이식한 현재 구조를 요약한다.

## 최신 상태 요약 - 2026-06-15

- ST1 card effect body는 카드별 파일 구조로 정렬되어 있고 `St1CardScriptCatalog`는 registry 등록만 담당한다.
- ST2/ST3도 source-aligned 로컬 작업트리에서 카드별 파일/marker 구조와 registry snapshot을 가진다.
- 공통 service mapping 감사는 `docs/rl-engine/common-layer-source-mapping.md`에 분리했다.
- full `MultipleSkills` priority, counter/pay-cost 세부 timing, Unity trace parity는 아직 전체 엔진 범위 TODO다.
- RL 학습 구성은 아직 구현하지 않는다.

## 핵심 원칙

- 기존 `./DCGO` Unity battle 로직을 Source of Truth로 삼는다.
- RL.Engine에는 UnityEngine/Photon/MonoBehaviour/GameObject/Coroutine/UI 의존성을 추가하지 않는다.
- 지원하지 않는 효과는 silent no-op 하지 않고 validation failure 또는 명시 예외로 처리한다.
- zone 이동은 `ZoneMover` 또는 primitive service를 통해 수행한다.
- selection은 `DecisionPoint`, `SelectionRequest`, `SelectionResult` 경계로만 표현하고 RL.Engine에 UI를 넣지 않는다.
- base `CardDefinition` 값은 duration/continuous 효과로 직접 수정하지 않는다.

## 주요 타입과 책임

| 타입/서비스 | 책임 |
| --- | --- |
| `EffectDescriptor` | card effect timing, source, trigger predicate, resolve body metadata |
| `EffectResolution` | queue 또는 direct 실행 대상이 되는 resolved effect |
| `TriggerCollector` | descriptor를 timing/context에 맞춰 queued/background로 수집 |
| `EffectQueue` | non-background effect queue와 drain 순서 관리 |
| `TriggerPipelineService` | source 수집, predicate 평가, queue drain, optional/selection boundary, once-per-turn, invariant hook 조합 |
| `OptionalEffectBoundary` | optional effect를 shared selection boundary로 변환 |
| `SelectionResultApplicator` | request id, candidates, Min/Max, skip, stale target 검증 후 continuation 실행 |
| `OncePerTurnTracker` | stable effect id 기반 중복 실행 방지 |
| `TemporaryModifier` | runtime duration modifier 상태 |
| `DurationCleanupService` | turn/battle/security cleanup timing에서 temporary modifier 제거 |
| `ContinuousEffectDescriptor` | state에 저장하지 않는 derived continuous effect metadata |
| `ContinuousEffectSourceCollector` | field top, inherited source, field tamer top card continuous source 수집 |
| `EffectiveStatService` | base stat + temporary modifier + continuous modifier 합산 |
| `SecurityEffectExecutionService` | security check 중 `SecuritySkill` 실행과 Activate Main Option/direct body 처리 |
| `EngineSession` | pending `SelectionRequest`를 `DecisionPoint`로 노출하고 `DecisionResult`로 재개하는 runtime boundary |

## Queue 52A pause/resume foundation

`EngineSession`은 `ActionExecutor`가 반환한 pending `TriggerPipelineContinuation`을 보관하고, `Resume(DecisionResult)`에서 `TriggerPipelineService.Resume(...)`을 통해 selection 결과와 남은 queue/background tail을 이어서 처리한다. continuation은 optional 승인 단계와 explicit target selection 단계를 구분한다. optional 승인 후 target request가 있으면 body를 실행하지 않고 다시 pause하며, optional 거절은 body를 skip하고 tail을 drain한다. 적용 결과 다음 `EffectResolution`이 있으면 같은 boundary에서 다시 pause한다. chain이 끝나면 option lifecycle 후속 trash와 rules timing/cleanup을 실행한다.

trace에는 action event와 selection event가 모두 남는다. selection event는 request id와 stable continuation id를 label에 포함하고, before/after state hash를 기록한다. delegate나 memory address는 trace identity로 쓰지 않는다.

현재 이 foundation은 hand option `OptionSkill`, chained option selection, optional yes/no + explicit target selection, normal play `OnPlay`, normal digivolve `WhenDigivolving`, `OnAllyAttack`, `OnEndAttack`, attack security check 중 `SecuritySkill`/Activate Main selection, rules timing, `OnStartTurn`, `OnStartMainPhase`, `PassAction`의 `OnEndTurn`/`OnStartTurn`에 연결됐다. `SecurityEffectExecutionService`와 `SecurityCheckService`는 같은 optional/selection stage 의미로 pending selection을 continuation으로 반환하고, resume 후 security card의 battle/trash와 남은 security checks를 이어간다. queue 53에서는 `OnSecurityCheck`, `OnLoseSecurity`, `AfterEffectsActivate`를 포함한 원본 security timing 순서를 별도로 정렬한다.

## ST1 Per-Card Layout Alignment - 2026-06-14

ST1 card script body는 더 이상 `St1CardScriptCatalog.cs`에 모아두지 않는다. 원본 Unity의 `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_*.cs` 파일 단위와 대응되도록 RL.Engine도 `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_*.cs`에 카드별 body를 배치한다.

| 원본 Unity 파일 | RL.Engine 파일 | 책임 |
| --- | --- | --- |
| `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_01.cs` | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_01.cs` | ST1-01 inherited continuous DP body |
| `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_03.cs` | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_03.cs` | ST1-03 inherited continuous DP body |
| `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs` | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_06.cs` | ST1-06 blocker/memory trigger body |
| `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_07.cs` | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_07.cs` | ST1-07 declared static capability |
| `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_08.cs` | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_08.cs` | ST1-08 WhenDigivolving selection body |
| `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_09.cs` | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_09.cs` | ST1-09 OnBlockAnyone memory trigger body |
| `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_11.cs` | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_11.cs` | ST1-11 continuous SecurityAttack body |
| `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_12.cs` | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_12.cs` | ST1-12 tamer aura and security self-play body |
| `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_13.cs` | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_13.cs` | ST1-13 option/security duration body |
| `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_14.cs` | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_14.cs` | ST1-14 Security Digimon DP body |
| `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_15.cs` | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_15.cs` | ST1-15 option/security deletion body |
| `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_16.cs` | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_16.cs` | ST1-16 option/security deletion body |

`St1CardScriptCatalog`는 registry 등록만 담당한다. 공통 helper는 원본 `CardEffectCommons`/공통 effect helper에 대응되는 범위만 `src/DCGO.RL.Engine/CardEffects/ST1/Red/St1ScriptSupport.cs`에 둔다.

ST2/ST3도 같은 원칙을 따른다. `St2St3CardScriptCatalog`는 `ICardScript` 목록 생성만 담당하고, ST2 body는 `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_*.cs`, ST3 body는 `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_*.cs`에 둔다. 반복되는 source trash, no-source inherited continuous, DP-zero deletion trigger, on-attack DP reduction helper는 원본 공통 helper 역할에 대응되는 support file로 분리한다.

## 원본 DCGO Mapping

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `AutoProcessing.GetSkillInfos` | timing에 맞는 `SkillInfo` 수집 | `TriggerPipelineService` source 수집 + `TriggerCollector` |
| `AutoProcessing.StackSkillInfos` | skill stack 적재 | `EffectQueue.EnqueueRange` |
| `AutoProcessing.TriggeredSkillProcess` | stack effect resolve | `TriggerPipelineService.Run` queue drain |
| `MultipleSkills` | 여러 trigger의 player/order/UI 선택 처리 | deterministic queue + selection boundary. full priority는 TODO |
| `TurnStateMachine` | turn/phase timing | `PhaseRunner` hook |
| `AttackProcess` | attack/block/end attack timing | `AttackService` hook |
| `CardController.PlayCardClass` | OnPlay/option play timing | `PlayCardService` hook |
| `CardController.UseOptionClass` | OptionSkill 실행 | `PlayCardService` + card script descriptor |
| `CardController.ISecurityCheck` | security reveal/executing/security skill/trash | `SecurityCheckService` + `SecurityEffectExecutionService` |

## ST1-12 Security Play-Self Tamer

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `ST1_12.SecuritySkill()` | security에서 이 tamer를 비용 없이 play | `St1TaiKamiyaScript` `SecuritySkill` descriptor |
| `CardEffectFactory.PlaySelfTamerSecurityEffect` | `ExecutingCards`에 있는 card를 payCost=false로 permanent play | `St1ScriptSupport.CanPlaySelfPermanentFromExecuting`, `Tier1PrimitiveService.PlayWithoutPayingCost` |
| `CardEffectCommons.CanPlayAsNewPermanent` | permanent play 가능 여부와 target frame 확인 | controller/owner/zone/type/frame 검증 |

구현 원칙:

- security check 중 card는 `Zone.Security -> Zone.Executing`으로 이동한다.
- play-self 효과는 card가 `Executing`에 있을 때만 실행된다.
- 성공 시 `Tier1PrimitiveService.PlayWithoutPayingCost`가 `Zone.Executing -> Zone.BattleArea` 이동을 처리한다.
- 성공한 card는 더 이상 `Executing`에 남아 있지 않으므로 후속 trash 이동을 하지 않는다.
- field가 가득 차면 activation 조건 false로 처리하며 checked card는 trash로 이동한다.
- ST1-12는 selection이 필요 없는 효과라 `SelectionRequest`를 만들지 않는다.

## Duration Modifier와 Continuous Effect의 차이

- `TemporaryModifier`는 duration과 cleanup timing이 있는 runtime state다. `GameState.TemporaryModifiers`에 저장되고 StateHash에 포함된다.
- continuous effect는 field/source/tamer 상태에서 파생되는 derived value다. 별도 state로 저장하지 않고 현재 `GameState`를 평가해 effective stat에 반영한다.
- duration 효과를 continuous-effect로 우회하지 않는다.
- continuous effect로 base `CardDefinition.DP`나 `CardDefinition.SecurityAttackModifier`를 직접 변경하지 않는다.

## ST1 Target Deck 상태

- ST1-01/03/11/12 continuous effect 구현 완료.
- ST1-08/13/15/16 selection effect 구현 완료.
- ST1-13/14/15/16 security effect 구현 완료.
- ST1-12 security play-self tamer 구현 완료.
- ST1 target deck validation 통과.

## ST2/ST3 Batch A Mapping

이번 batch는 ST2/ST3 전체 완료가 아니라, 기존 generic layer로 원본 의미를 보존 가능한 카드만 연결한 단계다.

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `ST2_13.OptionSkill` | `[Main] Gain 1 memory.` | `St2HammerSparkScript` `OptionSkill` descriptor + `Tier1PrimitiveService.ModifyMemory(+1)` |
| `ST2_13.SecuritySkill` | `[Security] Gain 2 memory.` | `St2HammerSparkScript` `SecuritySkill` descriptor + `SecurityEffectExecutionService` direct body |
| `ST3_05.OnAllyAttack` | inherited source가 battle area에 있고 security 4장 이상이면 memory +1 | `St3PatamonScript` source validation + `TriggerPipelineService` + `ModifyMemory(+1)` |
| `ST3_08.OnAllyAttack` | inherited source: 상대 Digimon 1체 DP -1000 for turn | `SelectionRequest`/`SelectionResultApplicator` + temporary DP modifier |
| `ST3_11.OnAllyAttack` | top source: 상대 Digimon 1체 DP -4000 for turn | top source validation + `SelectionResultApplicator` + temporary DP modifier |
| `ST2_11.OnAllyAttack` | `[When Attacking][Once Per Turn]` 이 Digimon unsuspend | top source attacker 검증 + `OncePerTurnTracker` + `Tier1PrimitiveService.Unsuspend` |
| `ST3_16.OptionSkill` | 상대 Digimon 1체 DP -10000 for turn | option selection descriptor + temporary DP modifier |
| `ST3_16.SecuritySkill` | Activate this card's Main effect | `SecurityEffectExecutionService.ActivateMainOption`으로 `OptionSkill` body 재사용 |

## ST2/ST3 security add-to-hand mapping

이번 갱신에서는 `security-add-this-card-to-hand` missing layer를 generic primitive로 해결했다. 원본 DCGO의 `CardEffectCommons.AddThisCardToHand`는 security effect resolution 중 활성화된 카드 자신을 hand로 이동시키며, RL.Engine에서는 security check가 먼저 `Security -> Executing`으로 카드를 이동시킨 뒤 card body가 `Tier1PrimitiveService.AddExecutingSecurityEffectCardToHand`를 통해 `Executing -> Hand`로 이동한다. `SecurityCheckService`의 후속 trash 처리는 카드가 아직 `Executing`에 남아 있을 때만 실행되므로, add-to-hand security effect와 기존 option security의 `Executing -> Trash` 정책이 분리된다.

| 원본 DCGO | 원본 책임 | RL.Engine 대상 |
| --- | --- | --- |
| `ST3_13.OptionSkill` | 내 Digimon 1체 DP +3000 for turn | `St3HolyFlameScript` `OptionSkill` descriptor + `SelectionResultApplicator` + temporary DP modifier |
| `ST3_13.SecuritySkill` | 내 Digimon과 Security Digimon 전체 DP +5000 for turn, then add this card to hand | player-target temporary DP modifier + temporary SecurityDigimonDP modifier + `AddExecutingSecurityEffectCardToHand` |
| `ST3_14.OptionSkill` | 상대 Digimon 1체 DP -2000 for turn | `St3HeavensCharmScript` `OptionSkill` descriptor + `SelectionResultApplicator` + temporary DP modifier |
| `ST3_14.SecuritySkill` | add this card to hand | `AddExecutingSecurityEffectCardToHand` |

이 경로는 ST3-13/14 card id 전용 shortcut이 아니라 security effect로 실행 중인 자기 자신을 hand로 이동시키는 공통 primitive다. 직접 zone list를 수정하지 않고 `ZoneMover`를 사용한다.

## ST3 negative SecurityAttack duration mapping

이번 갱신에서는 `negative-security-attack-duration` missing layer를 ST3-15 구현으로 해소했다. 원본 `ST3_15.OptionSkill`은 상대 Digimon 1체의 SecurityAttack을 -3 하고 `EffectDuration.UntilOpponentTurnEnd`를 사용한다. 원본 `ST3_15.SecuritySkill`은 상대 Digimon 전체의 SecurityAttack을 -1 하고 `EffectDuration.UntilEachTurnEnd` 계열로 처리한다.

RL.Engine 대응은 ST3 전용 shortcut이 아니라 기존 temporary modifier와 effective keyword 계산 경로를 확장하는 방식이다.

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `ST3_15.OptionSkill` | 상대 Digimon 1체 선택, SecurityAttack -3 until opponent turn end | `St3HolyWaveScript` `OptionSkill` descriptor + `SelectionResultApplicator` + permanent-target `TemporaryModifierKind.SecurityAttack` |
| `ST3_15.SecuritySkill` | 상대 Digimon 전체 SecurityAttack -1 for turn | `SecurityEffectExecutionService` direct body + player-target `TemporaryModifierKind.SecurityAttack` |
| `CardEffectCommons.ChangeDigimonSAttack` | target Digimon SecurityAttack duration modifier 적용 | `Tier1PrimitiveService.AddTemporarySecurityAttackModifier(PermanentId, ...)` |
| `CardEffectCommons.ChangeDigimonSAttackPlayerEffect` | player-wide SecurityAttack duration modifier 적용 | `Tier1PrimitiveService.AddTemporarySecurityAttackModifier(PlayerId, ...)` |

구현 원칙:

- permanent-target SecurityAttack modifier는 target이 battle area Digimon일 때만 생성된다.
- player-target SecurityAttack modifier는 기존 player-wide temporary modifier 경로를 사용한다.
- `DurationScope.UntilOpponentTurnEnd`는 `ExpiresAtTurnPlayerId`를 상대 player로 계산하고, `DurationCleanupService.CleanupTurnEnd`가 해당 turn end에서 제거한다.
- `DurationScope.UntilTurnEnd` security 효과는 효과 controller의 turn end cleanup에서 제거된다.
- `BattleKeywordService.SecurityAttackCount`는 base 1에 static/temporary/continuous modifier를 합산한 뒤 0 미만으로 내려가지 않도록 clamp한다.
- ST3-15 main option은 선택 결과 없이 직접 resolve되지 않으며, 잘못된 후보나 stale target은 기존 `SelectionResultApplicator` 검증에서 실패한다.

## ST2 once-per-turn unsuspend mapping

이번 갱신에서는 `st2st3-card-body-wiring`으로 남아 있던 ST2-11의 OnAllyAttack body를 연결했다. 원본 `ST2_11`은 `CardEffectCommons.CanTriggerOnAttack`으로 이 Digimon의 공격 시점을 확인하고, `ActivateClass`의 once-per-turn count 1 제약 아래 `IUnsuspendPermanents`로 자기 permanent를 active 상태로 되돌린다.

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `ST2_11.CardEffects(EffectTiming.OnAllyAttack)` | 공격 시 trigger 후보 생성 | `St2MetalGarurumonScript` `EffectTiming.OnAllyAttack` descriptor |
| `CardEffectCommons.CanTriggerOnAttack` | source permanent가 공격자인지 확인 | `StarterScriptSupport.IsSourcePermanentAttacker` + `EffectContext.Payload["Attacker"]` |
| `SetUpActivateClass(..., 1, false, ...)` | once-per-turn 자동 effect | `EffectDescriptor.IsOncePerTurn` + `OncePerTurnTracker` |
| `IUnsuspendPermanents.Unsuspend()` | 이 Digimon unsuspend | `Tier1PrimitiveService.Unsuspend` |

구현 원칙:

- ST2-11 전용 shortcut 없이 `TriggerPipelineService`의 `OnAllyAttack` hook과 기존 once-per-turn guard를 사용한다.
- source card가 field top card이고 source permanent가 실제 attacker일 때만 trigger된다.
- 공격 선언 직후 `AttackService`가 attacker를 suspend한 뒤 pipeline을 실행하므로, body는 suspended permanent를 다시 unsuspend한다.
- 이미 active 상태인 permanent에 대해 resolve가 호출되면 silent no-op 대신 `DomainException`으로 실패한다.

## ST2 source-trash mapping

이번 갱신에서는 `digivolution-source-trash` missing layer를 `Tier1PrimitiveService.TrashBottomDigivolutionSources`로 해결했다. 원본 DCGO의 `CardEffectCommons.TrashDigivolutionCardsFromTopOrBottom(..., isFromTop: false)`는 target permanent의 bottom digivolution card부터 지정 수만큼 trash한다. RL.Engine은 `PermanentState.SourceCardIds`의 마지막 항목을 bottom source로 보고, 각 source card를 `Zone.EvolutionSources -> Zone.Trash`로 `ZoneMover`를 통해 이동한다. source가 없거나 target이 battle area Digimon이 아니면 명시적으로 실패한다.

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `ST2_03.OnAllyAttack` | inherited source가 battle area에 있을 때 상대 level 5 이하 Digimon 1체의 bottom source 1장 trash | `St2SourceTrashScript` inherited source 검증 + level/source 보유 후보 + `SelectionResultApplicator` + `TrashBottomDigivolutionSources(1)` |
| `ST2_06.OnAllyAttack` | inherited source가 battle area에 있을 때 상대 Digimon 1체의 bottom source 1장 trash | `St2SourceTrashScript` inherited source 검증 + source 보유 후보 + `SelectionResultApplicator` + `TrashBottomDigivolutionSources(1)` |
| `ST2_09.OnEnterFieldAnyone` + `CanTriggerWhenDigivolving` | When Digivolving 시 상대 Digimon 1체의 bottom source 최대 2장 trash | `EffectTiming.WhenDigivolving` descriptor + source 보유 후보 + `TrashBottomDigivolutionSources(2)` |

ST2-06 원본 target predicate에는 source count 검사가 없지만, 원본 helper는 source가 없으면 처리 없이 종료한다. RL.Engine은 silent no-op을 피하기 위해 source를 가진 opponent battle area Digimon만 legal candidate로 제공하고, resolution 시점에 source가 사라지면 primitive가 예외로 실패한다.

## ST2 bounce-to-hand mapping

이번 갱신에서는 `bounce-to-hand` missing layer를 `Tier1PrimitiveService.ReturnPermanentToHand`로 해결했다. 원본 `ST2_16.OptionSkill`은 상대 Digimon 1체를 owner's hand로 되돌리고, 그 Digimon의 digivolution cards를 모두 trash한다. 원본 `ST2_16.SecuritySkill`은 `CardEffectCommons.AddActivateMainOptionSecurityEffect`를 통해 같은 main option 효과를 security에서 활성화한다.

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `ST2_16.OptionSkill` | 상대 Digimon 1체 선택, top card hand 이동, sources trash | `St2CocytusBreathScript` `OptionSkill` descriptor + `SelectionResultApplicator` + `ReturnPermanentToHand` |
| `SelectPermanentEffect.Mode.Bounce` | 선택된 permanent bounce 처리 | source cards `Zone.EvolutionSources -> Zone.Trash` 후 top card `Zone.BattleArea -> Zone.Hand` |
| `ST2_16.SecuritySkill` | security에서 main option activate | `SecurityEffectExecutionService.ActivateMainOption`으로 `OptionSkill` body 재사용 |

구현 원칙:

- top card를 먼저 이동하면 `ZoneMover`가 source를 top으로 승격하므로, primitive는 source를 모두 trash한 뒤 top card를 hand로 이동한다.
- target은 battle area Digimon이어야 하며 breeding area나 non-Digimon은 명시적으로 실패한다.
- linked card가 있는 permanent bounce는 아직 generic policy가 없으므로 `UnsupportedMechanicException`으로 실패한다.
- ST2-16 security activation은 main option body를 재사용하며, bounce로 attacker가 field를 떠나면 `SecurityCheckService`는 security battle을 진행하지 않고 checked option card를 기존 정책대로 `Executing -> Trash`로 보낸다.

## ST2 opponent no-source mapping

이번 갱신에서는 `opponent-no-source-condition` missing layer를 `StarterScriptSupport.HasOpponentBattleAreaDigimonWithoutSources`로 해결했다. 이 helper는 controller의 상대 battle area에 Digimon top card가 있고 `SourceCardIds.Count == 0`인 permanent가 하나 이상 있는지를 판정한다. 조건은 continuous effect와 queued trigger 모두에서 공유한다.

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `ST2_01.None` | inherited owner turn 중 상대에게 source 없는 Digimon이 있으면 self DP +1000 | `St2NoSourceInheritedContinuousScript` + `ContinuousEffectDescriptor(SelfPermanent, DP)` |
| `ST2_08.None` | inherited owner turn 중 상대에게 source 없는 Digimon이 있으면 self SecurityAttack +1 | `St2NoSourceInheritedContinuousScript` + `ContinuousEffectDescriptor(SelfPermanent, SecurityAttack)` |
| `ST2_12.OnStartTurn` | start of owner turn에 상대에게 source 없는 Digimon이 있으면 memory +1 | `EffectTiming.OnStartTurn` descriptor + `TriggerPipelineService` + `Tier1PrimitiveService.ModifyMemory(+1)` |
| `ST2_12.SecuritySkill` | security에서 비용 없이 self tamer play | `SecurityEffectExecutionService` direct body + `PlayWithoutPayingCost(Executing -> BattleArea)` |

ST2-01 원본 구현은 `battle.enemyPermanent(...)`를 통해 battle 상대를 참조하지만, 카드 문구와 ST2-08/12의 원본 조건은 상대 field에 source 없는 Digimon이 있는지를 본다. RL.Engine은 target pool 검증을 위해 opponent battle area 전체 조건으로 일관되게 매핑했다. 이 결정은 ST2 전용 shortcut이 아니라 opponent no-source condition helper를 continuous/trigger body가 공유하는 방식이다.

## ST3 recovery-from-deck mapping

이번 갱신에서는 `recovery-from-deck`와 `on-enter-field-when-digivolving-compat` missing layer를 ST3-09 body wiring으로 해결했다. 원본 `ST3_09`는 `OnEnterFieldAnyone` timing에 등록되어 있지만 `CanTriggerWhenDigivolving` 조건을 통해 digivolve 완료 시 발동하는 패턴이다. RL.Engine은 이를 `EffectTiming.WhenDigivolving` descriptor로 명시 매핑한다.

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `ST3_09.OnEnterFieldAnyone` + `CanTriggerWhenDigivolving` | When Digivolving 시 내 security가 3장 이하이고 deck이 있으면 Recovery +1 Deck | `St3AngewomonScript` `WhenDigivolving` descriptor |
| `CardEffectCommons.IRecovery(owner, 1)` | deck top card 1장을 security에 추가 | `Tier1PrimitiveService.RecoverFromDeck(state, owner, 1)` |
| `IAddSecurityFromLibrary` | library top을 security 위에 face-down으로 배치 | `ZoneMover` `Zone.Deck -> Zone.Security`, `ToTop: true`, `FaceUp: false` |

구현 원칙:

- source permanent가 battle area top card이고 controller의 security가 3장 이하일 때만 trigger 후보가 된다.
- deck이 비어 있으면 trigger 후보에서 제외하고, primitive에 직접 잘못 호출되면 `DomainException`으로 실패한다.
- recovery는 `CardDefinition`이나 zone list를 직접 수정하지 않고 `ZoneMover`를 통해 deck top을 security top으로 이동한다.
- 원본의 더 넓은 `CannotAddSecurityEffect` replacement 계열은 아직 target pool에서 요구되지 않아 구현하지 않았다. 해당 replacement가 필요한 카드가 들어오면 별도 missing layer로 분리해야 한다.

## ST3 continuous SecurityDigimonDP mapping

이번 갱신에서는 `continuous-security-digimon-dp` missing layer를 ST3-12 body wiring으로 해결했다. 원본 `ST3_12.None`은 battle area에 있는 field tamer source가 opponent turn 동안 owner의 Security Digimon card DP를 +2000 하는 static effect를 제공하고, `SecuritySkill`은 기존 play-self tamer 보안 효과를 사용한다.

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `CardEffectFactory.ChangeSecurityDigimonCardDPStaticEffect` | owner의 Security Digimon card DP +2000 static aura | `ContinuousModifierKind.SecurityDigimonDP` + `ContinuousEffectTargetKind.OwnerPlayer` |
| `ST3_12.None` condition | source card가 battle area에 있고 opponent turn일 때만 적용 | `St3TakeruTakaishiScript` field top continuous descriptor condition |
| security battle DP 계산 | security에서 check된 Digimon DP에 static aura 반영 | `EffectiveStatService.SecurityDp` + `BattleResolver.ResolveSecurityBattle` |
| `ST3_12.SecuritySkill` | security에서 비용 없이 자기 자신을 tamer로 play | `SecurityEffectExecutionService` direct body + `PlayWithoutPayingCost(Executing -> BattleArea)` |

구현 원칙:

- continuous SecurityDigimonDP는 state에 저장되는 duration modifier가 아니라 현재 field source와 turn player에서 파생된다.
- temporary `SecurityDigimonDP`와 continuous `SecurityDigimonDP`는 `EffectiveStatService.SecurityDp`에서 deterministic하게 합산된다.
- non-Digimon security card에는 적용하지 않는다.
- ST3-12 security play-self는 ST3 전용 shortcut이 아니라 기존 generic `Executing -> BattleArea` play-without-cost primitive를 재사용한다.

## ST2 attack/block restriction mapping

이번 갱신에서는 `cannot-attack-block-duration` missing layer를 ST2-14 body wiring으로 해결했다. 원본 `ST2_14`는 source 없는 opponent battle area Digimon 1체를 선택하고 `GainCanNotAttack`/`GainCanNotBlock` duration effect를 부여한다. RL.Engine은 이를 temporary restriction modifier로 표현하고, attack/block legal filtering에서 파생해 사용한다.

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `ST2_14.OptionSkill` | source 없는 상대 Digimon 1체 선택, opponent turn end까지 attack/block 불가 | `St2HowlingBlasterScript` `OptionSkill` selection + `CannotAttack`/`CannotBlock` `UntilOpponentTurnEnd` |
| `ST2_14.SecuritySkill` | source 없는 상대 Digimon 1체 선택, owner turn end까지 attack/block 불가 | `SecurityEffectExecutionService` direct selection + `CannotAttack`/`CannotBlock` `UntilOwnerTurnEnd` |
| `CardEffectCommons.GainCanNotAttack` | target permanent attack legal 여부 차단 | `TemporaryModifierKind.CannotAttack` + `BattleRules.CanAttack` |
| `CardEffectCommons.GainCanNotBlock` | target permanent block legal 여부 차단 | `TemporaryModifierKind.CannotBlock` + `BattleKeywordService.CanBlock` |

구현 원칙:

- target candidate는 opponent battle area Digimon 중 `SourceCardIds.Count == 0`인 permanent로 제한한다.
- selection 이후 target이 source를 얻거나 field를 떠난 경우 resolution 또는 stale cleanup에서 명시적으로 실패/정리한다.
- restriction modifier는 state에 저장되는 duration modifier이며 `DurationCleanupService`의 기존 turn-end/stale-target 정책을 공유한다.
- attack/block 여부만 바꾸며 DP, SecurityAttack, keyword 자체는 변경하지 않는다.

## ST2 evolution-source-card-play mapping

이번 갱신에서는 `evolution-source-card-play` missing layer를 ST2-15 body wiring으로 해결했다. 원본 `ST2_15.OptionSkill`은 내 battle area Digimon 1체를 먼저 선택하고, 그 Digimon 아래의 Digimon digivolution card 1장을 다시 선택한 뒤 `CardEffectCommons.PlayPermanentCards(payCost:false, root:DigivolutionCards, activateETB:true)`로 별도 Digimon을 비용 없이 play한다. `SecuritySkill`은 `AddActivateMainOptionSecurityEffect`로 같은 main option body를 security에서 활성화한다.

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `ST2_15.OptionSkill` | 내 Digimon host 선택 후 playable Digimon source card 선택 | `St2WereGarurumonScript` chained `SelectionResultApplicator` |
| `SelectPermanentEffect` | source를 가진 내 battle area Digimon 1체 선택 | `CreateOwnerBattleAreaDigimonWithPlayableEvolutionSourceSelectionRequest` |
| `SelectCardEffect` | 선택된 host의 playable Digimon source 1장 선택 | `CreatePlayableEvolutionSourceCardSelectionRequest` |
| `CardEffectCommons.CanPlayAsNewPermanent(payCost:false)` | source card가 새 permanent로 비용 없이 play 가능한지 검증 | `StarterScriptSupport.IsPlayableEvolutionSource` + `Tier1PrimitiveService.PlayEvolutionSourceAsNewPermanent` |
| `CardEffectCommons.PlayPermanentCards` | `EvolutionSources -> BattleArea` 이동과 새 permanent 생성 | `ZoneMover`를 통과하는 `PlayWithoutPayingCost` |
| `ST2_15.SecuritySkill` | security에서 main option activate | `SecurityEffectExecutionService.ActivateMainOption` |

구현 원칙:

- ST2-15 전용 zone list 수정은 없다. source card 제거와 새 permanent 생성은 `ZoneMover`가 처리한다.
- 첫 selection result가 두 번째 selection resolution을 생성하는 generic chained selection 경로를 `SelectionResultApplicator`에 추가했다.
- source card는 선택 요청 생성 시점과 적용 시점에 모두 battle area host의 Digimon source인지 재검증한다.
- stale source target은 `SelectionResultApplicator` 또는 `PlayEvolutionSourceAsNewPermanent`에서 `DomainException`으로 실패한다.
- 현재 ST1~ST3 target pool에는 ST2-15로 play될 수 있는 source card 중 별도 OnPlay body가 필요한 카드가 없다. 향후 target pool에서 `activateETB:true`가 관측 가능한 카드가 들어오면 play-without-cost 후 `OnPlay` trigger hook을 같은 generic primitive/service 경계에서 확장해야 한다.

## ST3 DP-zero deletion trigger mapping

이번 갱신에서는 `dp-zero-deletion-trigger` missing layer를 ST3-01/ST3-04 body wiring으로 해결했다. 원본 `ST3_01`과 `ST3_04`는 inherited `EffectTiming.OnDestroyedAnyone` 효과이며, `[Your Turn][Once Per Turn]` 조건과 “상대 Digimon이 DP 0으로 삭제됨” 조건을 모두 요구한다.

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `RuleProcessor` 대응 DP 0 삭제 | DP가 0 이하인 battle area Digimon 삭제 | `RuleProcessor`가 DP-zero permanent를 분리해 destroy 후 `OnDestroyedAnyone` payload 발생 |
| `CardEffectCommons.CanTriggerOnPermanentDeleted` | 삭제된 permanent가 조건에 맞는지 확인 | payload `DestroyedPermanent`, `DestroyedController`, `DestroyedTopCard` |
| `CardEffectCommons.IsDPZeroDelete` | DP 0 삭제 reason 확인 | payload `DestroyedByDpZero = true` |
| `ST3_01.OnDestroyedAnyone` | inherited owner-turn once/turn, 상대 DP-zero 삭제 시 self DP +1000 for turn | `St3DpZeroDeletionTriggerScript` + `TemporaryModifierKind.DP` |
| `ST3_04.OnDestroyedAnyone` | inherited owner-turn once/turn, 상대 DP-zero 삭제 시 memory +1 | `St3DpZeroDeletionTriggerScript` + `Tier1PrimitiveService.ModifyMemory(+1)` |
| `SetUpActivateClass(..., 1, false, ...)` | once-per-turn 제한 | `EffectDescriptor.IsOncePerTurn` + `OncePerTurnTracker` |

구현 원칙:

- DP 0으로 인한 rule deletion만 `DestroyedByDpZero` payload를 만든다. battle DP 패배, security battle, 일반 destroy primitive는 이번 hook에 포함하지 않는다.
- invalid breeding permanent, face-down permanent 정리와 DP-zero deletion을 분리해 원본 trigger reason이 섞이지 않게 했다.
- ST3-01/04는 source card가 inherited source로 battle area permanent 아래에 있을 때만 trigger 후보가 된다.
- owner turn이 아니거나 삭제된 permanent controller가 effect controller와 같으면 trigger되지 않는다.
- 중복 실행은 기존 `OncePerTurnTracker` 정책을 따른다.

## 아직 Partial인 전체 엔진 범위

- full `MultipleSkills` simultaneous trigger priority/UI 선택 순서
- `BeforePayCost`, `AfterPayCost`, `OnCounterTiming`
- block selection result application and full end-to-end integration
- future cards that require replacement/cut-in effects

## ST1-ST3 inventory and gap-analysis stage

2026-06-14 기준 ST1-ST3 확장 작업은 먼저 원본 CardEffect inventory와 missing layer를 문서화하는 단계로 분리한다. 이번 단계에서는 ST2/ST3 CardScript나 primitive를 새로 구현하지 않는다.

Source mapping:

| Unity source | 분석한 책임 | RL.Engine 설계 경계 |
| --- | --- | --- |
| `DCGO/Assets/Scripts/CardEffect/ST1/**` | ST1 baseline 효과와 NoEffect 후보 | 기존 ST1 target deck completion 기준점 |
| `DCGO/Assets/Scripts/CardEffect/ST2/**` | blue starter effect 후보, source trash, bounce, memory, restriction | ST2 batch별 common layer 후보 |
| `DCGO/Assets/Scripts/CardEffect/ST3/**` | yellow starter effect 후보, recovery, DP-zero trigger, SecurityAttack 감소 | ST3 batch별 common layer 후보 |
| `ICardEffect.cs` | `EffectTiming`, inherited/security/background/once-per-turn metadata | `EffectDescriptor`, `TriggerPipelineService`, `OncePerTurnTracker` |
| `CardEffectCommons.cs` | Unity 공통 helper: play, option, source trash, bounce, recovery, DP/SecurityAttack | `Tier1PrimitiveService`, `SecurityEffectExecutionService`, `ZoneMover` |
| `AutoProcessing.cs` / `MultipleSkills.cs` | trigger 수집/stack/drain과 동시 trigger 처리 | 현재 pipeline + 향후 priority/choice TODO |
| `CardController.cs` / `AttackProcess.cs` / `TurnStateMachine.cs` | play, option, security check, attack, phase cleanup | battle/phase/security service hook |

분석 결과:

- ST1-ST3 target card pool은 48장이다.
- 원본 CardEffect 파일은 34개이며, NoEffect 후보는 14장이다.
- 즉시 구현 가능 후보는 `ST2-13`, `ST3-05`, `ST3-08`, `ST3-11`, `ST3-16`으로 분류했다.
- 새 common missing layer는 source trash, opponent no-source condition, unsuspend, cannot attack/block duration, play digivolution source as permanent, return to hand, recovery, DP-zero deletion classification, security Digimon DP, add security card to hand, negative SecurityAttack duration이다.
- 자세한 per-card 표는 `docs/rl-engine/cardeffect-porting-status-st1-st3.md`에 둔다.
Existing partial timing notes from the previous ST1/ST3 sections:

Current review correction:

- NoEffect candidates in the full ST1-ST3 source inventory are 12 cards with empty `CardEffectClassName`.
- Immediate ST2/ST3 implementation candidates are 7 cards: `ST2-07`, `ST2-13`, `ST3-05`, `ST3-07`, `ST3-08`, `ST3-11`, `ST3-16`.
- The implementation mapping below is historical local worktree context from previous uncommitted work. It is not evidence that the current inventory-review task implemented or validated ST2/ST3.

## Historical local worktree ST1-ST3 implementation note

This section is historical local worktree context from previous uncommitted work. It does not supersede the planning baseline override below and must not be used as evidence that this inventory-review task completed ST2/ST3 validation.

Prior local worktree result, not newly verified in the current inventory-review task:

- target card pool: 48 cards.
- original CardEffect files: 34.
- explicit NoEffect cards in full ST1-ST3 source inventory: 12.
- unsupported/partial card scripts for ST1-ST3: 0.
- missing layers for the current ST1-ST3 target pool: 0.
- latest executed test result: `All 194 tests passed`.
- RL training components remain not implemented.

Source mapping:

| Unity source | Original responsibility | RL.Engine mapping |
| --- | --- | --- |
| `ST2_03`, `ST2_06`, `ST2_09` | select opponent Digimon and trash bottom digivolution source cards | `SelectionResultApplicator` + `Tier1PrimitiveService.TrashBottomDigivolutionSources` |
| `ST2_11` | once-per-turn self unsuspend on attack | `TriggerPipelineService.OnAllyAttack` + `OncePerTurnTracker` + `Tier1PrimitiveService.Unsuspend` |
| `ST2_14` | cannot attack/block duration | temporary `CannotAttack`/`CannotBlock` modifiers and legal action filtering |
| `ST2_15` | choose source card and play it free as another Digimon | chained `SelectionResultApplicator` + `PlayEvolutionSourceAsNewPermanent` |
| `ST2_16` | return opponent Digimon to hand and trash sources | `Tier1PrimitiveService.ReturnPermanentToHand` |
| `ST3_01`, `ST3_04` | trigger on opponent Digimon deleted by 0 DP | `RuleProcessor` DP-zero payload + `EffectTiming.OnDestroyedAnyone` |
| `ST3_09` | Recovery +1 Deck | `Tier1PrimitiveService.RecoverFromDeck` |
| `ST3_12`, `ST3_13` | Security Digimon DP modifiers | continuous/temporary `SecurityDigimonDP` effective stat path |
| `ST3_13`, `ST3_14` | add checked security card to hand | generic executing security card to hand primitive |
| `ST3_15` | negative SecurityAttack duration | temporary `SecurityAttack` modifiers and effective SecurityAttack calculation |

Still out of current target-pool scope:

- full `MultipleSkills` simultaneous trigger priority/UI choice,
- `BeforePayCost`, `AfterPayCost`, `OnCounterTiming`,
- `OnAttackTargetChanged`, `OnEndBlockDesignation`,
- wider replacement/cut-in effects,
- Unity trace parity harness.

## ST1-ST3 planning baseline override - 2026-06-14

This section is the authoritative note for the current documentation-only inventory task. It supersedes earlier ST1-ST3 completion wording in this file for planning purposes.

Current task scope:

- no new implementation code,
- no Unity source modification,
- no ST2/ST3 validation pass attempt,
- no RL training API work.

Source mapping used for the ST1-ST3 plan:

| Unity source | Planning responsibility | RL.Engine boundary to use later |
| --- | --- | --- |
| `DCGO/Assets/CardBaseEntity/ST1/**` | ST1 card ID, card name, effect class baseline | committed ST1 target deck baseline |
| `DCGO/Assets/CardBaseEntity/ST2/**` | ST2 card ID, card name, effect class inventory | future ST2 catalog/validator entries |
| `DCGO/Assets/CardBaseEntity/ST3/**` | ST3 card ID, card name, effect class inventory | future ST3 catalog/validator entries |
| `DCGO/Assets/Scripts/CardEffect/ST2/**` | ST2 effect timing/body mapping | generic primitives and trigger pipeline only |
| `DCGO/Assets/Scripts/CardEffect/ST3/**` | ST3 effect timing/body mapping | generic primitives and trigger pipeline only |
| `ICardEffect.cs` | `EffectTiming`, duration, inherited/security flags | `EffectDescriptor`, `TriggerPipelineService`, `TemporaryModifier` |
| `CardEffectCommons.cs` | source trash, play source, recovery, add-to-hand, DP/SecurityAttack helpers | `Tier1PrimitiveService`, `SecurityEffectExecutionService`, `ContinuousEffectService`, `ZoneMover` |

Planning counts:

- ST1-ST3 target pool: 48 cards.
- ST2 cards: 16.
- ST3 cards: 16.
- cards with non-empty `CardEffectClassName`: 36.
- unique CardEffect files under ST1/ST2/ST3: 34.
- NoEffect candidates with empty `CardEffectClassName`: 12.
- immediate ST2/ST3 candidates: 7 (`ST2-07`, `ST2-13`, `ST3-05`, `ST3-07`, `ST3-08`, `ST3-11`, `ST3-16`).

New or reviewed common layer candidates:

- digivolution source trash,
- opponent no-source condition,
- unsuspend primitive,
- cannot attack/cannot block duration restrictions,
- play digivolution source as a new permanent,
- return permanent to hand with source trash,
- recover from deck to security,
- DP-zero deletion event classification,
- Security Digimon effective DP,
- checked security card add-to-hand,
- negative SecurityAttack duration and lower-bound policy.

The fixed implementation pass sections are defined in `docs/rl-engine/st1-st3-porting-plan.md`.

- `OnAttackTargetChanged`, `OnEndBlockDesignation`
- block selection result application의 full end-to-end integration
- 더 넓은 카드풀의 replacement/cut-in effect

## Queue 50 - option 실행 lifecycle

원본 `CardController.UseOptionClass.UseOption()`은 hand option을 사용할 때 card를 먼저 모든 zone에서 제거한 뒤 `ExecutingCards`에 넣고, `OptionSkill` 및 option resolution을 처리한 뒤 아직 `ExecutingCards`에 남아 있을 때만 trash로 보낸다. RL.Engine의 `PlayCardService`도 queue 50부터 같은 lifecycle을 사용한다.

Hand play option 흐름:

1. turn player와 hand 소유 여부를 검증한다.
2. play cost를 지급한다.
3. `ZoneMover`로 `Zone.Hand -> Zone.Executing` 이동한다.
4. `EffectTiming.OptionSkill`을 실행하며 context payload의 `SourceZone`은 `Zone.Executing`이다.
5. pending `SelectionRequest`가 있으면 card를 `Executing`에 둔 채 `OptionPlayResult`로 request/resolution을 반환한다.
6. selection 결과 적용 후 다음 chained selection이 없고 card가 아직 `Executing`이면 `Zone.Executing -> Zone.Trash`로 이동한다.
7. option body가 source card를 다른 zone으로 이동했다면 후속 trash를 생략한다.

Security option은 기존처럼 `SecurityCheckService`가 `Zone.Security -> Zone.Executing`으로 이동시킨 뒤 `SecurityEffectExecutionService.ActivateMainOption`이 같은 `OptionSkill` body를 실행한다. 비용 지급 여부와 시작 zone은 hand play와 다르지만, 효과 body가 보는 source card는 `Executing`이다. security check cleanup도 card가 여전히 `Executing`일 때만 trash로 이동한다.

검증:

- `Option lifecycle hand play moves through Executing to Trash`
- `Option lifecycle source context is Executing`
- `Option lifecycle pending selection keeps Executing`
- `Option lifecycle selection completion trashes option`
- `Option lifecycle moved source skips follow-up Trash`
- `Option lifecycle invalid action leaves zones clean`
- `Option lifecycle action trace replay deterministic`
- `Option lifecycle ST1 hand option regression`
- `Option lifecycle ST2/ST3 hand option regression`
