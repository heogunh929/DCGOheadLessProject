# OnDigivolutionCardDiscarded Foundation Scope (FND-003-K)

## Source of Truth

- AS-IS root: `E:\headlessDCGO\DCGO\Assets`
- Primary source: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:5127-5234`
- Trigger condition source: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\CanUseEffects\OnTrashDigivolutionCard.cs`
- Payload reader source: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\GetFromHashtable.cs:569`

## Source Contract

원본 `ITrashDigivolutionCards.TrashDigivolutionCards()` 경로는 대상 `Permanent`의 진화원 중 trash 가능한 카드만 고르고, `WhenDigivolutionCardWouldDiscarded` cut-in을 거친 뒤 실제 source removal과 trash 이동을 수행한다. 실제 discard 이벤트는 `EffectTiming.OnDigivolutionCardDiscarded`로 stack되며 payload에는 최소 `CardEffect`, `Permanent`, `DiscardedCards`가 들어간다.

`CanTriggerOnTrashDigivolutionCard`는 `Permanent`, `Permanent.TopCard`, `CardEffect`, `DiscardedCards`가 모두 있고, discard된 카드 조건 중 하나라도 맞을 때만 trigger를 허용한다.

## Implemented Scope

- `Tier1PrimitiveService.TrashDigivolutionCardsWithEvents(...)`를 추가해 battle-area Digimon의 `EvolutionSources` 대상만 검증한다.
- 선택된 source card는 `ZoneMover`를 통해 `Zone.EvolutionSources -> Zone.Trash`로 이동한다.
- effect source가 있을 때만 `OnDigivolutionCardDiscarded` pending rule event를 enqueue한다.
- `TrashBottomDigivolutionSources(...)`는 같은 shared primitive를 사용하도록 연결했다.
- `skipEffectAndActivateSkill=true` 경로는 원본 null-effect 조건처럼 trigger enqueue를 생략한다.
- `TriggerPipelineService`는 `TriggeredSourceCards`/`DiscardedCards` payload를 trash-zone source 후보로 수집해, 버려진 진화원 카드가 해당 event를 관찰할 수 있게 했다.

## Payload Contract

Headless payload는 원본 키와 검증용 이동 metadata를 함께 보존한다.

- 원본 정렬 키: `Permanent`, `CardEffect`, `DiscardedCards`
- 대상/출처 키: `TargetPermanent`, `DiscardedFromPermanent`, `DiscardedFromTopCard`, `SourceCard`, `SourcePermanent`
- 이동 키: `Cards`, `CardSources`, `SourceZone`, `DestinationZone`, `MoveReason`
- discarded-source trigger 후보 키: `DiscardedCard`, `TriggeredSourceCards`, `TriggeredSourceZone`, `TriggeredSourceOriginalZone`, `TriggeredSourceOriginalPermanent`

## Verification

- Test: `FND-003-K OnDigivolutionCardDiscarded queues source trash event`
- Command: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- Result: `All 621 tests passed.`
- Foundation Gate recalculation: `OpenCodeReady=false`, passed gate 11, failed gate 3.

## Remaining Follow-Ups

- `WhenWouldDigivolutionCardDiscarded` cut-in/replacement parity
- strict pre-move inherited-source snapshot parity
- `CanNotTrashFromDigivolutionCards` static policy integration
- card-specific body wiring
- source-order parity fixtures for top/bottom and multi-source discard

## Guardrails

- 원본 `DCGO/Assets`는 수정하지 않았다.
- 개별 `CardEffect` body는 구현하지 않았다.
- C0039 이후 card-porting은 실행하지 않았다.
- generated status를 `Implemented` 또는 `Verified`로 승격하지 않았다.
- Foundation Gate 수치는 수동 조작하지 않았다.
