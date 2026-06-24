# FND-003-O OnTappedAnyone Foundation Scope

## 결론

`FND-003-O OnTappedAnyone`는 실제 suspend가 성공한 뒤 `OnTappedAnyone` pending rule event를 생성하는 foundation scope로 닫았다.

원본 Unity 기준에서 `SuspendPermanentsClass.Tap()`은 이미 suspended인 permanent와 `CanSuspend=false` 대상을 제외하고, 실제 suspend 후 `Permanents`, `IsBlock`, 선택적 `CardEffect` payload로 `EffectTiming.OnTappedAnyone`을 stack한다. Headless 쪽은 이 semantics를 `Tier1PrimitiveService.Suspend(...)` 경계에 연결했다.

## Source Of Truth

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs:1000`
  - `EffectTiming.OnTappedAnyone`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:5558-5650`
  - `SuspendPermanentsClass.Tap()`
  - actual suspend 후 `Permanents`, `IsBlock`, `CardEffect` payload로 `StackSkillInfos(..., EffectTiming.OnTappedAnyone)` 실행
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\AttackProcess.cs:544-557`
  - block 시 `IsBlock=true`를 hashtable에 넣고 blocker suspend 후 `OnBlockAnyone`으로 진행
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\GetFromHashtable.cs:87-96`
  - `CardEffectCommons.IsBlock(...)`

## 구현 범위

- `Tier1PrimitiveService.Suspend(...)`
  - actual suspended state change가 있을 때만 `OnTappedAnyone` pending rule event 생성
  - idempotent suspend는 `false`를 반환하고 event를 추가하지 않음
  - payload: `Permanents`, `Permanent`, `TappedPermanents`, `TappedPermanent`, `SuspendedPermanents`, `SuspendedPermanent`, `TappedController`, `TappedTopCard`, `CardSources`, `CardEffect`, `SourceCard`, `SourcePermanent`, `SourceZone`, `DestinationZone`, `IsBlock`
- `AttackService`
  - attack declaration suspend 직후 pending `OnTappedAnyone` event를 먼저 drain한 뒤 `OnAllyAttack`으로 진행
  - block suspend는 `IsBlock=true` payload를 남기고, `OnTappedAnyone`을 먼저 drain한 뒤 `OnBlockAnyone`으로 진행
  - pending selection 발생 시 attack continuation으로 이어질 수 있게 continuation kind를 추가
- `SelectEffectFacades`
  - generic tap selection은 `EffectResolution.SourceCard` / `SourcePermanent`를 primitive에 전달

## 검증

- `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- 결과: `All 630 tests passed.`

추가 fixture:

- `FND-003-O OnTappedAnyone queues actual suspend event`
  - actual suspend event 생성
  - already suspended target 재호출 시 event 미생성
  - `CardEffect`/`SourceCard`/`SourcePermanent` payload 보존
  - block suspend `IsBlock=true` payload 보존
  - auto process 후 probe가 payload를 관측

## 제외 및 후속 작업

- `CanSuspend` / `ICanNotSuspendEffect` static policy parity는 이번 scope에서 구현하지 않았다.
- individual `CardEffect` body source metadata wiring은 카드별 구현 범위이므로 확장하지 않았다.
- `SuspendPermanentsClass` stack object의 엄격한 Unity parity와 card-specific `OnTappedAnyone` body parity는 후속 작업으로 남긴다.
- generated status를 `Implemented` 또는 `Verified`로 승격하지 않았다.
- Foundation Gate 수치는 직접 조작하지 않았다.
- C0039 이후 card-porting과 RL 구성요소 작업은 계속 금지 상태다.

## 다음 후보

다음 foundation 후보는 `FND-003-P OnDeclaration`이다.
