# AfterEffectsActivate / MultipleSkills Source Mapping

## 54B 보정 결과 - trigger stack semantic hardening

54B부터 `TriggerStackFrame`은 batch 상태로 `HadCandidate`, `HadResolutionAttempt`, `AfterEffectsActivate` candidate signature 이력을 보존한다. `RulesTiming` 후보가 0개인 empty batch 또는 stale source/`CanActivate` 실패만 있었던 batch는 원본 `TriggeredSkillProcess`에서 실제 해소 batch가 없는 경우로 취급하며, `AfterEffectsActivate`를 수집하지 않는다.

ordering decision과 effect 자체 decision은 분리한다. 같은 player group의 활성 `EffectResolution`이 2개 이상이면 optional yes/no 또는 target selection 여부와 관계없이 먼저 ordering `SelectionRequest`를 반환한다. ordering으로 효과를 하나 고른 뒤에만 해당 효과의 optional request 또는 explicit target request로 이어진다. 모든 후보가 optional이면 ordering request의 전체 skip을 허용하고, 일부 후보만 optional이면 전체 skip을 금지한다.

`RuleProcessor.StabilizeStateOnly`는 DP 0 삭제 같은 rule event를 `RuleStabilizationResult.Events`로 반환한다. `TriggerPipelineService`는 이 event를 `OnDestroyedAnyone` 등 원본 timing의 nested `TriggerStackFrame`으로 준비하고, parent frame의 remaining tail보다 먼저 모두 drain한다. `TriggerPipelineService`와 `RuleProcessor`는 직접 생성 또는 순환 의존을 만들지 않고, `BattleEngineServices`가 delegate를 연결한다.

`AfterEffectsActivate` frame이라는 이유만으로 후속 `AfterEffectsActivate` 수집을 금지하지 않는다. 현재 batch에서 실제 effect resolution이 있었고 새 `AfterEffectsActivate` 후보가 생겼다면 새 frame을 허용한다. 같은 candidate signature가 반복되면 self-loop로 보고 `UnsupportedMechanicException`으로 실패시키며, 별도 `MaxTriggerStackDepth` guard도 유지한다.

source persistence는 기본 `RequireSameRole`이다. 이는 원본 `PutStackedSkill()`의 `PermanentWhenTriggered`/`TopCardWhenTriggered` 재검증 의미에 맞춰 source role, source zone, owner/controller, trigger 당시 top card가 유지되어야 함을 뜻한다. 원본상 trigger 후 source 이동이 허용되는 효과는 카드별 descriptor가 `AllowTriggeredSourceMove`를 명시해야 하며, core service나 catalog에 CardId 분기를 만들지 않는다.

## 54A 보정 결과 - TriggerStackFrame

54A부터 `TriggerPipelineService`는 nested trigger tail과 외부 trigger tail을 하나의 list로 평탄화하지 않는다. pending continuation은 `TriggerStackFrame`을 보존하며, 각 frame은 현재 timing/context, 현재 batch의 remaining effects, background effects, parent frame, ordering candidates, AfterEffectsActivate scheduling 상태, depth guard를 가진다.

Frame drain 정책:

- 현재 frame의 활성 batch를 turn player group 전체, 그 다음 non-turn player group 순서로 처리한다.
- 같은 player group에서 활성 effect가 2개 이상이고 explicit optional/target decision이 아닌 경우 `EffectResolution` 단위 ordering request를 만든다. 같은 source card에서 script-authored descriptor가 2개 나온 경우도 자동 순서로 강제하지 않는다.
- effect body 하나가 끝나면 `RuleProcessor.StabilizeStateOnly`를 통해 state-only rule stabilization을 수행하고, 새 `RulesTiming` 후보가 있으면 nested frame으로 먼저 drain한다. nested frame이 완전히 끝난 뒤에만 parent frame으로 복귀한다.
- `AfterEffectsActivate`는 effect 1개마다 실행하지 않고 현재 batch가 끝난 뒤 다음 frame으로 수집한다. `AfterEffectsActivate` frame은 parent frame과 섞이지 않으며, 무한 재귀는 trigger stack depth guard로 차단한다.
- optional yes/no와 explicit target selection이 필요한 effect는 ordering decision이 그 decision boundary를 가리지 않도록 먼저 해당 effect의 own decision으로 pause한다.

`TriggerPipelineService`는 `RuleProcessor`를 직접 생성하지 않는다. production `BattleEngineServices`가 `RuleProcessor.StabilizeStateOnly` delegate를 trigger pipeline에 연결해 순환 생성 의존성을 피한다.

Source snapshot/revalidation:

- `EffectDescriptor`/`EffectResolution`은 trigger 당시 `TriggerSourceSnapshot`을 보존한다.
- snapshot에는 source role, source zone, source permanent, trigger 당시 top card, owner/controller가 포함된다.
- 실행 직전 `FieldTop`, `Inherited`, `Linked`, `Hand`, `Trash`, `Executing`, `FaceUpSecurity` role별 위치를 재검증하고, 그 뒤 `CanActivate`를 다시 확인한다.
- Hand->Trash, Trash->Hand, FaceUpSecurity->Trash, inherited->top, inherited->linked, field top->source 전환은 stale source로 판단되어 실행되지 않는다.

이 문서는 DCGO Unity 원본의 `AutoProcessing`, `MultipleSkills`, `ISecurityCheck` 흐름을 queue 54 구현과 대응시킨다. 원본 `DCGO/Assets/Scripts`는 읽기 전용 Source of Truth로 유지한다.

## 원본 Source

- `DCGO/Assets/Scripts/Script/AutoProcessing.cs`
  - `AutoProcessCheck()`는 `RuleProcess()` 이후 `StackSkillInfos(null, EffectTiming.RulesTiming)`를 수행하고, 이어 `TriggeredSkillProcess(false, null)`로 stacked trigger를 해소한다.
  - `TriggeredSkillProcess()`는 현재 `StackedSkillInfos` snapshot을 꺼내 `MultipleSkills.ActivateMultipleSkills(...)`에 넘긴 뒤 `StackSkillInfos(null, EffectTiming.AfterEffectsActivate)`를 다시 수행한다.
  - `ActivateEffectProcess()`는 실행 직전에 `ActivateICardEffect.CanActivate(...)`를 다시 확인한다.
  - `PutStackedSkill()`은 trigger 시점의 permanent/top-card metadata를 `ActivateICardEffect`에 저장한다.
- `DCGO/Assets/Scripts/Script/MultipleSkills.cs`
  - incoming trigger를 turn player source와 non-turn player source로 분리한다.
  - turn player group을 모두 처리한 뒤 non-turn player group을 처리한다.
  - 같은 player group 안에 활성 trigger가 여러 개면 플레이어에게 처리 순서를 선택하게 한다.
  - 각 effect 실행 뒤 `RuleProcess()`와 `TriggeredSkillProcess(...)`를 다시 수행해 새 trigger를 현재 tail보다 먼저 해소한다.
  - 처리 직전마다 `CardEffect.CanActivate(...)`와 source card 존재를 다시 검사한다.
- `DCGO/Assets/Scripts/Script/CardController.cs`
  - `ISecurityCheck.SecurityCheck()`는 security card 1장 처리 중 다음 위치에서 `AutoProcessCheck()`를 호출한다.
  - `SecuritySkill` 해소 직후.
  - prepared `OnSecurityCheck`/`OnLoseSecurity` 후보를 stack한 직후.
  - battle/final zone 처리와 `UntilSecurityCheckEnd` cleanup 직후.

## RL.Engine 대응

- `TriggerPipelineService`
  - `EffectDescriptor.CanTrigger`는 후보 수집 시점 조건으로 유지한다.
  - `EffectDescriptor.CanActivate`와 공통 source revalidation은 실행 직전 조건으로 분리했다.
  - `PreparedTriggerGroup`은 원본 `SkillInfo` snapshot에 해당한다. snapshot 후보 목록은 보존하지만 실행 직전에 source card/permanent 소속과 `CanActivate`를 다시 확인한다.
  - queued trigger drain은 매 step마다 활성 후보를 다시 계산한다.
  - 활성 후보 중 turn player controller 후보가 있으면 그 group을 먼저 처리하고, 없으면 non-turn player group을 처리한다.
  - 같은 player group에서 서로 다른 source card 후보가 여러 개이면 ordering `SelectionRequest`를 만들 수 있다.
  - production `BattleEngineServices` graph는 provider 없는 external decision mode에서 ordering decision을 pause로 반환한다.
  - direct/test fixture pipeline 기본값은 deterministic first-order로 유지해 legacy synchronous helper가 불필요하게 pause되지 않게 한다.
  - 같은 source card가 만든 내부 tail descriptor는 script-authored order로 drain한다.
  - effect body가 실제 실행된 뒤 현재 batch 종료 시 `AfterEffectsActivate` timing을 stack/drain한다. `AfterEffectsActivate` frame 내부에서 새 후보가 실제로 생기면 연쇄 frame을 허용하고, 동일 candidate signature 반복은 self-loop로 명시 실패시킨다.

- `SecurityCheckService`
  - security card 1장 state machine에 원본 `AutoProcessCheck()` 위치를 반영했다.
  - `SecuritySkill` 완료 뒤 `RulesTiming` auto process를 실행한다.
  - prepared `OnSecurityCheck`/`OnLoseSecurity` 해소 뒤 battle 전 `RulesTiming` auto process를 실행한다.
  - `UntilSecurityCheckEnd` cleanup 뒤 다음 security card 판단 전 `RulesTiming` auto process를 실행한다.
  - 각 auto process는 `TriggerPipelineService`의 same priority/order/after-effect drain을 사용한다.
  - pending selection이 생기면 `SecurityCheckContinuationStage`로 resume 위치를 보존한다.

## 현재 명시 범위

- 카드별 effect body는 기존 카드별 script에 남긴다.
- `Catalog`와 `SecurityCheckService`에는 특정 `CardId` 분기를 추가하지 않았다.
- counter/cut-in 및 attack target 변경 priority 전체는 queue 55 범위다.
- 원본 `RuleProcess()`의 모든 세부 상태 정리는 기존 `RuleProcessor` 책임으로 유지한다. queue 54에서는 security interleaving 지점의 `RulesTiming`/trigger drain을 source-aligned로 배치했다.
