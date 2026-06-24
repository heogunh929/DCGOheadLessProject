# FND-003-I OnMove Foundation Scope

## Source Evidence

- Source root: `E:\headlessDCGO\DCGO\Assets`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardObjectController.cs:1111`
  - moved `Permanent`를 `Hashtable { "Permanent", movingPermanent }`에 넣고 `EffectTiming.OnMove`를 stack한다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\CanUseEffects\OnMove.cs:10`
  - `GetMovedPermanentFromHashtable`로 `Permanent`를 꺼낸다.
  - moved permanent가 battle area에 존재할 때만 `CanTriggerOnMove`가 true가 된다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory.cs:854/940`
  - `WhenMovingClass`가 `EffectTiming.OnMove`를 source effect로 생성한다.
  - 자기 permanent가 이동한 경우에만 `CanTriggerOnMove` 조건을 통과한다.

## Implemented Scope

- `Tier1PrimitiveService.MovePermanentWithEvents(...)`를 추가했다.
- `MoveFromBreedingService.Move(...)`는 `Zone.BreedingArea -> Zone.BattleArea` 이동 후 공유 primitive를 통해 `OnMove` rule event를 생성한다.
- `BattleEngineServices`는 `MoveFromBreedingService`에 동일한 `Tier1PrimitiveService` instance를 주입한다.
- `OnMove` event는 이동 후 permanent가 battle area에 남아 있을 때만 생성한다.
- `Zone.BattleArea -> Zone.BreedingArea`는 원본의 battle-area 생존 조건에 맞지 않으므로 `OnMove`를 생성하지 않는다.

## Payload Contract

- `Cards`
- `CardSources`
- `Permanents`
- `Permanent`
- `MovedPermanents`
- `MovedPermanent`
- `MovedController`
- `MovedTopCard`
- `CardEffect`
- `SourceCard`
- `SourcePermanent`
- `SourceZone`
- `DestinationZone`
- `OldZone`
- `NewZone`
- `MoveReason`
- `BattleAreaSurvived`

## Verification

- Test: `FND-003-I OnMove queues battle-area permanent move event`
- Command: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- Result: `All 619 tests passed.`

## Deferred Scope

- Broader Unity frame movement parity beyond current breeding-to-battle action.
- Source metadata expansion for non-breeding movement call sites.
- Stale target parity fixtures for source-specific `CanTriggerOnMove` cases.
- Individual card body wiring remains blocked until Foundation Gate allows card-porting.

## Policy Notes

- No individual `CardEffect` body was implemented.
- C0039+ card-porting was not run.
- Generated status was not promoted.
- Foundation Gate values were not manually manipulated.
- Original `DCGO/Assets` files were not modified.
