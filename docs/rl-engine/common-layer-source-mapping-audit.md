# Common Layer Source Mapping Audit

## 최신 상태 요약 - 2026-06-15

이 문서는 source-aligned queue 44 `common_layer_source_mapping_audit`의 결과다. 이번 작업은 감사 문서 작성만 수행했으며, 새 카드 효과 구현, ST2/ST3 구현, 기존 `DCGO/Assets/Scripts` Unity 원본 수정, remote fetch/pull/push, commit 생성을 하지 않았다.

감사 기준은 현재 worktree다. queue 40~43의 문서/구조 guard 변경은 아직 commit되지 않은 상태이며, 최신 commit은 `3b993b34 202606142346`이다. `origin` remote는 존재하지만 이번 작업에서 사용하지 않았다.

## 감사 범위

감사한 RL.Engine 공통 layer:

- `src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs`
- `src/DCGO.RL.Engine/Effects/SelectionResultApplicator.cs`
- `src/DCGO.RL.Engine/Domain/TemporaryModifier.cs`
- `src/DCGO.RL.Engine/Effects/DurationCleanupService.cs`
- `src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs`
- `src/DCGO.RL.Engine/Effects/SecurityEffectExecutionService.cs`
- `src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs`
- `src/DCGO.RL.Engine/Domain/ZoneMover.cs`
- `src/DCGO.RL.Engine/Battle/BattleKeywordService.cs`
- `src/DCGO.RL.Engine/Battle/RuleProcessor.cs`
- `src/DCGO.RL.Engine/Battle/PlayCardService.cs`
- `src/DCGO.RL.Engine/Battle/DigivolveService.cs`
- `src/DCGO.RL.Engine/Battle/AttackService.cs`
- `src/DCGO.RL.Engine/Battle/SecurityCheckService.cs`
- 관련 supporting layer: `PhaseRunner`, `EffectQueue`, `TriggerCollector`, `OncePerTurnTracker`, `BattleResolver`, `DrawService`

참조한 원본 DCGO 파일:

- `DCGO/Assets/Scripts/Script/AutoProcessing.cs`
- `DCGO/Assets/Scripts/Script/MultipleSkills.cs`
- `DCGO/Assets/Scripts/Script/SkillInfo.cs`
- `DCGO/Assets/Scripts/Script/ICardEffect.cs`
- `DCGO/Assets/Scripts/Script/CardEffectCommons.cs`
- `DCGO/Assets/Scripts/Script/CardController.cs`
- `DCGO/Assets/Scripts/Script/AttackProcess.cs`
- `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- `DCGO/Assets/Scripts/Script/MainPhaseAction/*.cs`
- `DCGO/Assets/Scripts/Script/SelectCardEffect.cs`
- `DCGO/Assets/Scripts/Script/SelectPermanentEffect.cs`

## 결론

현재 core common layer 자체에는 `ST1-`, `ST2-`, `ST3-` 같은 직접 카드 ID shortcut은 발견되지 않았다. 카드별 body는 주로 `CardEffects/ST*/...`와 set별 support helper에 있고, core service는 zone 이동, trigger 수집, selection result 적용, temporary/continuous modifier, security check, play/digivolve/attack 같은 공통 책임을 담당한다.

다만 source-alignment 관점에서 다음 위험은 남아 있다.

- `TriggerPipelineService`는 원본 `AutoProcessing`과 `MultipleSkills`의 여러 책임을 한 service로 압축했다. 현재 FIFO drain, optional boundary, once-per-turn, selection application까지 포함하지만 원본의 동시 trigger 선택, turn player/non-turn player 우선순위, cut-in/counter priority, `AfterEffectsActivate` 후속 stack은 부분 대응이다.
- `Tier1PrimitiveService`는 원본 `CardEffectCommons`, `CardController`, `Player`, `Permanent`, `AttackProcess`의 primitive 역할을 넓게 흡수했다. 카드 ID shortcut은 없지만 계속 커지면 원본 책임 추적성이 떨어질 수 있다.
- `PlayCardService`의 hand option 처리 흐름은 원본 `UseOptionClass`와 차이가 있다. 원본은 option을 executing으로 이동해 `OptionSkill`을 실행한 뒤 trash로 보낸다. 현재 구현은 hand option을 바로 trash로 이동한 뒤 `OptionSkill`을 실행하고 `SourceZone = Trash`로 기록한다. 이 차이는 source-alignment 재검토가 필요하다.
- `SecurityCheckService`는 security card를 `Security -> Executing -> Trash`로 처리하고 `SecurityEffectExecutionService`를 호출하지만, 원본 `ISecurityCheck`의 `OnSecurityCheck`, `OnLoseSecurity` stack 흐름 전체를 아직 실행하지 않는다.
- `PlayCardService`, `DigivolveService`, `AttackService`, `PhaseRunner`, `RuleProcessor`는 production `BattleEngineServices` graph에서 동일 `TriggerPipelineService`와 `IZoneMover`를 공유하도록 검증한다. 개별 service를 테스트 편의로 직접 생성하는 경로는 production runtime으로 쓰지 않는다.
- `RuleProcessor.TrimExcessLinkedCards`는 queue 52에서 injected `IZoneMover`를 사용하도록 정렬했다. linked overflow 테스트는 spy mover 호출을 확인하고, source guard는 `RuleProcessor` 내부 direct `new ZoneMover` 재발을 막는다.

## 원본 vs RL.Engine 공통 layer 매핑

| RL.Engine layer | 원본 DCGO 대응 파일/클래스/메서드 | 매핑 책임 | 카드별 로직 포함 여부 | 구조 적합성 | 수정 권고 |
| --- | --- | --- | --- | --- | --- |
| `TriggerPipelineService` | `AutoProcessing.GetSkillInfos`, `StackSkillInfos`, `TriggeredSkillProcess`, `MultipleSkills.ActivateMultipleSkills`, `SkillInfo` | timing별 descriptor 수집, trigger predicate 평가, background/queued 분리, `EffectQueue` drain, optional/selection boundary, once-per-turn 등록 | core service에는 카드 ID 없음. 카드 body는 registry script로 위임 | 부분 적합 / 과도 통합 위험 | `AfterEffectsActivate`, full `MultipleSkills` priority, cut-in/counter priority를 unsupported/partial gate에 노출한다. 필요 시 priority/order resolver를 별도 service로 분리한다. |
| `TriggerCollector`, `EffectQueue`, `OncePerTurnTracker` | `AutoProcessing.PutStackedSkill`, `skillInfos_used`, `HasExecutedSameEffect`, `MultipleSkills` 내부 active list | descriptor를 `EffectResolution`으로 변환하고 FIFO queue로 실행하며 once-per-turn 중복을 막는다 | 카드별 로직 없음 | 부분 적합 | FIFO가 원본 선택/우선순위를 대체한다고 문서화하면 안 된다. 동시 trigger 선택 모델은 별도 missing layer로 유지한다. |
| `SelectionResultApplicator` | `SelectCardEffect`, `SelectPermanentEffect`, `ActivateEffectProcess` callback | `SelectionRequest`와 `SelectionResult`를 검증하고 stale target을 재검증한 뒤 continuation을 실행한다 | 카드별 로직 없음 | 적합 | 원본 UI metadata 전체는 아직 단순화되어 있으므로 CLI/UnityAdapter 표시 정보 확장 시 별도 검증을 추가한다. |
| `TemporaryModifier`, `DurationCleanupService` | `CardEffectCommons` duration effect, `TurnStateMachine` turn-end cleanup lists, `AttackProcess` battle/security cleanup | runtime modifier를 상태에 저장하고 turn/battle/security/active cleanup에서 제거한다 | 카드별 로직 없음 | 부분 적합 | 원본의 모든 until timing, cut-in/counter 전용 cleanup, next unsuspend류 scope는 아직 전체 대응으로 보지 않는다. |
| `ContinuousEffectService`, `ContinuousEffectSourceCollector`, `EffectiveStatService` | `ICardEffect` `EffectTiming.None`, background/continuous 효과, `Permanent`/`Player` stat query | field top/inherited source에서 continuous descriptor를 수집하고 effective DP/SecurityAttack을 파생 계산한다 | 카드별 로직 없음. continuous card script가 descriptor를 제공 | 부분 적합 | player-level, hand, trash, face-up security continuous source까지 일반화됐다고 보지 않는다. source kind 확장 전에 원본 `EffectList(EffectTiming.None)` 조회 범위를 재감사한다. |
| `SecurityEffectExecutionService` | `CardController.ISecurityCheck.SecurityCheck`, `SecuritySkill`, option security `Activate Main` 흐름 | executing/security card의 `SecuritySkill`을 실행하고 `ActivateMainOption`은 동일 card의 `OptionSkill` 경로를 재사용한다 | 카드별 로직 없음 | 부분 적합 | multiple security skill priority, `OnSecurityCheck`/`OnLoseSecurity` stack과의 순서, direct security body invariant hook을 추가 검증한다. |
| `Tier1PrimitiveService` | `CardEffectCommons`, `CardController.DrawClass`, `IAddSecurity`, `IRecovery`, `ITrashStack`, `ITrashDigivolutionCards`, `DestroyPermanentsClass`, `UseOptionClass`, `PlayPermanentClass`, `IBattle`, `ISecurityCheck` | draw/reveal/search request/trash/delete/return/recover/memory/modifier/play/digivolve/security/battle primitive를 카드 body에 제공한다 | 카드 ID shortcut은 없음. 다만 원본 여러 책임을 한 service가 흡수 | 적합하나 넓음 / 과도 통합 위험 | primitive별 source mapping 표와 invariant test를 유지한다. card id 조건 또는 ST 전용 분기를 추가하면 구조 위반으로 간주한다. |
| `ZoneMover` | `CardObjectController`의 area remove/add, field/source/link movement, permanent top/source 전환 | 모든 zone 이동의 단일 primitive, `CardInstance.CurrentZone`과 zone membership 일관성 유지 | 카드별 로직 없음 | 적합 | 원본 UI frame/preferred placement와 1:1은 아니므로 deterministic placement 정책을 계속 문서화한다. |
| `BattleKeywordService` | `CardEffectCommons/KeyWordEffects/*`, `Permanent` keyword query, attack/security keyword query | keyword 합산, SecurityAttack 수 계산, blocker 후보 생성, unsupported keyword 실패 처리 | 카드별 로직 없음 | 부분 적합 | Decoy/replacement류는 `UnsupportedMechanicException` 유지. keyword가 card definition/source/continuous에서 오는 범위를 지속 검증한다. |
| `RuleProcessor` | `AutoProcessing.AutoProcessCheck`, `RuleProcess`, `EffectTiming.RulesTiming`, DP 0 삭제, invalid permanent cleanup | rules timing hook, invariant 확인, stale duration cleanup, DP zero/invalid breeding/face-down permanent 정리, linked overflow trim | 카드별 로직 없음 | 부분 적합 | linked overflow 이동은 injected `IZoneMover`를 사용한다. `OnDestroyedAnyone` payload와 원본 deletion timing 순서를 추가 검증한다. |
| `PhaseRunner` | `TurnStateMachine.ActivePhase`, `DrawPhase`, `BreedingPhase`, `MainPhase`, `EndTurnProcess` | phase 전환, start/end timing hook, turn-end duration cleanup, draw phase 처리 | 카드별 로직 없음 | 부분 적합 | 원본 breeding/main UI/action 대기와 `EndTurnCheck` 전체는 아직 단순화되어 있다. pipeline 미주입 시 trigger silent skip 방지 guard가 필요하다. |
| `PlayCardService` | `CardController.PlayCardClass`, `PlayPermanentClass`, `UseOptionClass` | hand card play, cost 지불, permanent 생성, option play, `OnPlay`/`OptionSkill` hook | 카드별 로직 없음 | 부분 적합 / 불명확 | hand option을 `Hand -> Trash` 후 `OptionSkill` 실행하는 현재 흐름은 원본 `Hand -> Executing -> Trash`와 다르다. option source zone과 after-resolution trash 정책을 source-aligned로 재검토한다. |
| `DigivolveService` | `CardController.PlayCardClass`, `PlayPermanentClass`, digivolve path | hand digivolve, cost 지불, `ZoneMover.DigivolveCard`, draw 1, `WhenDigivolving` hook | 카드별 로직 없음 | 부분 적합 | special digivolve, burst/jogress/app fusion, Before/AfterPayCost timing은 지원 범위 밖으로 유지한다. pipeline 미주입 silent skip guard가 필요하다. |
| `AttackService`, `BattleResolver` | `AttackProcess.Attack`, `CounterTiming`, `BlockTiming`, `DetermineAttackOutcome`, `EndAttack`, `IBattle` | attack 선언, attacker suspend, OnAllyAttack/OnEndAttack hooks, permanent/security battle, piercing, direct win | 카드별 로직 없음 | 부분 적합 | counter timing, attack target change, blocker 선택 적용 순서, collision/block priority는 partial이다. `RunBlockTrigger`는 별도 호출이라 full attack state machine과 아직 1:1이 아니다. |
| `SecurityCheckService` | `CardController.ISecurityCheck.SecurityCheck`, `IReduceSecurity`, security battle cleanup | security top check, `Security -> Executing`, `SecurityEffectExecutionService`, security Digimon battle, executing 잔존 card trash, security-check cleanup | 카드별 로직 없음 | 부분 적합 | 원본의 `OnSecurityCheck`, `OnLoseSecurity`, security skill choice, `UntilSecurityCheckEndEffects` 순서를 full pipeline에 연결해야 한다. |

## 과도 통합 위험

| 위치 | 위험 | 현재 판단 | 권고 |
| --- | --- | --- | --- |
| `TriggerPipelineService` | 원본 `AutoProcessing`, `MultipleSkills`, optional selection, once-per-turn, invariant hook이 한 service에 모임 | 기능적으로는 통합 layer가 필요하지만, 원본 priority 미지원이 묻힐 수 있음 | 동시 trigger priority와 cut-in/counter는 별도 "partial" gate/test로 계속 노출한다. |
| `Tier1PrimitiveService` | 많은 원본 primitive가 한 service에 누적됨 | 현재는 카드 ID shortcut이 없어 허용 가능 | primitive별 source mapping과 구조 guard를 추가한다. service가 커지면 draw/security/source/field primitive 그룹으로 분리 검토한다. |
| `StarterScriptSupport`, set별 `St*ScriptSupport` | 카드별 body가 helper 안으로 숨을 수 있음 | queue 43 guard가 concrete class 위치를 강화했지만 support helper body 위험은 남음 | helper는 candidate/query/primitive wrapper까지만 허용하고, card-specific amount/timing/source mapping은 카드별 파일에 남긴다. |
| `PlayCardService` option path | 원본 `UseOptionClass`의 executing zone 흐름과 다름 | source zone audit 필요 | option play도 `Executing`을 거치는 generic path로 맞출지 별도 작업에서 결정한다. |
| `RuleProcessor.TrimExcessLinkedCards` | queue 52에서 injected `IZoneMover` 사용으로 정렬 | dependency boundary 위험은 해소. 원본 deletion timing parity는 별도 검증 필요 | spy mover/guard 테스트를 유지하고, future rules timing 변경 시 같은 mover graph를 유지한다. |

## 누락 또는 불명확한 원본 대응

| 원본 책임 | 현재 상태 | 영향 |
| --- | --- | --- |
| `MultipleSkills`의 turn player/non-turn player 순서, 여러 trigger 선택 UI, 자동 순서 | FIFO queue로 부분 대응 | 동시 trigger가 많은 카드군에서 원본 순서와 다를 수 있다. |
| `AutoProcessing.TriggeredSkillProcess` 후 `AfterEffectsActivate` stack | `TriggerPipelineService` 결과에 직접 후속 hook이 보이지 않음 | after-effect trigger 카드에서 누락 가능성이 있다. |
| `AttackProcess.CounterTiming` | `EffectTiming.OnCounterTiming` enum은 있으나 service 연결은 미완성 | counter/cut-in 카드 구현 전 missing layer로 유지해야 한다. |
| `AttackProcess.SwitchDefender`와 `OnAttackTargetChanged` | enum은 있으나 full attack state machine 연결은 미완성 | attack target change 카드 구현 전 source-aligned 설계가 필요하다. |
| `CardController.ISecurityCheck`의 `OnSecurityCheck`, `OnLoseSecurity` stack | `SecurityCheckService`는 security skill execution 중심 | security 관련 trigger가 많은 카드군에서 누락 가능성이 있다. |
| `CardController.UseOptionClass` option executing lifecycle | hand option은 현재 바로 trash로 이동 | option body에서 source zone/executing card를 기대하는 효과와 불일치 가능성이 있다. |
| `TurnStateMachine.EndTurnCheck` memory crossing 전체 | `RuleProcessor.ProcessAfterAction`이 memory < 0이면 턴 전환 | pass action, active attack 처리, EndTurnProcess 세부 순서와 완전 1:1은 아니다. |
| `MainPhaseAction.ActivatePermanentAction` / `ActivateCardAction` | action model과 일부 hook은 있으나 full declared skill path 감사 필요 | 선언형 effect 카드군 구현 전 별도 audit 필요하다. |

## 카드별 파일로 이동하거나 노출해야 할 후보

현재 core service에서 카드별 파일로 옮겨야 할 직접 카드 로직은 발견되지 않았다. 다만 다음은 카드별 파일에 더 명확히 노출해야 하는 후보로 남긴다.

- set별 support helper가 가진 amount, duration, prompt/debug label, source mapping은 카드별 파일에서 확인 가능해야 한다.
- `St1OptionDeleteScript`처럼 여러 카드를 공유하는 helper는 target 조건이 완전히 같은지 계속 검증해야 한다. 카드별 차이가 커지면 helper가 아니라 각 카드 파일로 body를 분리한다.
- `St2ScriptSupport`, `St3ScriptSupport`의 반복 trigger body는 helper가 커질수록 원본 파일 추적성이 떨어진다. helper는 generic candidate/query/primitive wrapper로 제한한다.

## 수정 우선순위 제안

1. `PlayCardService` option lifecycle을 원본 `UseOptionClass`와 대조해 `Hand -> Executing -> Trash` 흐름으로 맞출지 결정한다.
2. `SecurityCheckService`에 `OnSecurityCheck`/`OnLoseSecurity` trigger hook이 필요한지 원본 security 흐름 기준으로 별도 작업을 만든다.
3. `TriggerPipelineService`의 `AfterEffectsActivate`와 full `MultipleSkills` priority 미지원 범위를 completion report에 직접 노출한다.
4. service composition guard를 유지해 `PlayCardService`, `DigivolveService`, `AttackService`, `PhaseRunner`, `RuleProcessor`가 production에서 동일 `TriggerPipelineService`/`IZoneMover` graph를 공유하는지 검증한다.
5. 개별 service 생성자의 테스트 편의 fallback은 production runtime으로 쓰지 않으며, full-card-pool 단계에서 필요하면 테스트 전용 builder로 더 분리한다.

## 테스트 상태

이번 작업은 문서 감사만 수행했으므로 전체 테스트를 새로 실행하지 않았다. 직전 queue 43에서 다음 명령이 성공했다.

```powershell
$env:DOTNET_CLI_HOME='E:\headlessDCGO\.dotnet_home'
$env:NUGET_PACKAGES='E:\headlessDCGO\.nuget\packages'
$env:TEMP='E:\headlessDCGO\.tmp'
$env:TMP='E:\headlessDCGO\.tmp'
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: `All 214 tests passed.` MSBuild cache/temp 접근 경고는 있었지만 test runner는 성공 종료했다.

## 다음 queue 항목

다음 항목은 `45_st1_st3_completion_gate_reconcile.md`다.
