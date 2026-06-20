# AfterEffectsActivate / MultipleSkills Source Mapping

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
  - effect body가 실제 실행된 뒤 `AfterEffectsActivate` timing을 한 번 stack/drain한다. `AfterEffectsActivate` 자체는 다시 자기 자신을 재귀 stack하지 않는다.

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
