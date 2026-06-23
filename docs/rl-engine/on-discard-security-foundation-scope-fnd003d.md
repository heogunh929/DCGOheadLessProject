# OnDiscardSecurity Foundation Scope FND-003-D

## Source Evidence

- AS-IS root: `E:\headlessDCGO\DCGO`
- Enum: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs:994` `EffectTiming.OnDiscardSecurity`
- Runtime call: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:4377`
- Source flow: `IDestroySecurity.DestroySecurity()` moves selected/top/bottom security cards to trash, records `discardedCards`, runs `IReduceSecurity.ReduceSecurity()` first, then stacks `EffectTiming.OnDiscardSecurity`.
- Source payload keys observed at the call boundary: `DiscardedCards`, `CardEffect`.

## Implemented Headless Scope

- `Tier1PrimitiveService.RemoveSecurity(...)` now queues source-aligned pending rule events when the destination is `Zone.Trash`.
- Event order is fixed as:
  1. `OnLoseSecurity`
  2. `OnDiscardSecurity`
- Payload includes `Cards`, `CardSources`, `DiscardedCards`, `SecurityCards`, `SecurityCard`, `Player`, `CardEffect`, `SourceCard`, `SourcePermanent`, `SourceZone=Security`, `DestinationZone=Trash`, and `MoveReason=Effect`.
- The event is queued only after the actual `ZoneMover` mutation succeeds.
- Non-trash security moves remain outside this hook scope.

## Deferred Scope

- Multi-card `IDestroySecurity` batching is not generalized yet; current primitive scope covers one removed security card per call.
- Battle security check ordering remains owned by `SecurityCheckService` and existing `OnSecurityCheck` / `OnLoseSecurity` flow.
- Card-specific `OnDiscardSecurity` bodies are not implemented here.
- Generated full-card status is not promoted.

## Verification

- Added regression: `FND-003-D OnDiscardSecurity queues security discard events`.
- Full regression passed: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 614 tests passed.`

## Next

- Continue with `FND-003-E OnAddSecurity`.
