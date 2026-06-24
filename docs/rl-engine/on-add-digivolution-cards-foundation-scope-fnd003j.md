# FND-003-J OnAddDigivolutionCards Foundation Scope

## Source Evidence

- Source root: `E:\headlessDCGO\DCGO\Assets`
- `Permanent.AddDigivolutionCardsTop(...)`: `Permanent.cs:1045`
- `Permanent.AddDigivolutionCardsBottom(...)`: `Permanent.cs:1130`
- Trigger timing: `ICardEffect.cs:1002` `EffectTiming.OnAddDigivolutionCards`
- Trigger condition helper: `CardEffectCommons/CanUseEffects/OnAddDigivolutionCards.cs`
- Hashtable flags: `CardEffectCommons/GetFromHashtable.cs` `isFromSameDigimon`, `isFromDigimon`

원본은 일반 digivolve의 `AddCardSource(...)`가 아니라, 효과 또는 복합 처리로 source가 추가되는 `AddDigivolutionCardsTop/Bottom(...)` 경계에서 `OnAddDigivolutionCards`를 stack한다. Payload는 `Permanent`, `CardEffect`, `CardSources`, `isFromSameDigimon`, `isFromDigimon`를 중심으로 구성된다.

## Implemented Scope

- `Tier1PrimitiveService.AddDigivolutionCardsWithEvents(...)`를 추가했다.
- destination이 `Zone.EvolutionSources`이고 destination permanent가 일치하는지 검증한다.
- `ZoneMover.MoveCard(...)`를 통해 실제 source 이동을 수행한다.
- 이벤트를 생략하지 않는 경우 `EffectTiming.OnAddDigivolutionCards` rule event를 생성한다.
- Payload는 `Permanent`, `DestinationPermanent`, `CardEffect`, `SourceCard`, `SourcePermanent`, `Cards`, `CardSources`, `AddedDigivolutionCards`, `isFromSameDigimon`, `isFromDigimon`, `SourceZone`, `DestinationZone`, `SourceZones`, `MoveReason`, `ToTop`, `FaceUp`를 보존한다.
- `ComplexMechanicService`의 Jogress/AppFusion/DigiXros/Assembly source placement가 shared source-add primitive를 통과한다.

## Deliberate Limits

- 일반 digivolve의 `DigivolveCard(...)`에는 `OnAddDigivolutionCards`를 추가하지 않았다. 원본 `AddCardSource(...)`와 맞지 않기 때문이다.
- 복합 메커니즘에서 `CardEffect`가 없는 null-source placement는 `skipEffectAndActivateSkill=true`로 처리했다. 원본 `CanTriggerOnAddDigivolutionCard(...)`도 `CardEffect == null`이면 실제 발동하지 않는다.
- 개별 `CardEffect` body는 구현하지 않았다.
- C0039 이후 card-porting은 수행하지 않았다.
- generated status는 승격하지 않았다.

## Verification

- `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- Result: `All 620 tests passed.`

## Follow-Up Candidates

- `FND-003-K OnDigivolutionCardDiscarded`
- face-down source add parity fixture 확장
- source-bottom ordering parity fixture 확장
- source-specific card body wiring은 Foundation Gate 이후 card-porting 단계로 유지
