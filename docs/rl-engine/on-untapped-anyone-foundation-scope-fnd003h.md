# FND-003-H OnUnTappedAnyone Foundation Scope

## Goal

`FND-003-H` closes the first primitive foundation slice for
`EffectTiming.OnUnTappedAnyone`.

The implemented scope is actual battle-area permanent unsuspend events:
when a permanent changes from suspended to unsuspended through the common
headless primitive, the runtime now enqueues an `OnUnTappedAnyone` rule event.
The event can then be consumed by the existing trigger pipeline / auto-process
path.

This is foundation plumbing only. It does not implement individual card effect
bodies and does not unlock C0039+ card-porting.

## Source Evidence

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs`: `WhenUntapAnyone`
  and `OnUnTappedAnyone` are distinct timings.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs`: `IUnsuspendPermanents`
  filters valid suspended battle-area permanents, runs `WhenUntapAnyone` cut-in
  candidates before the state change, then sets `IsSuspended=false`.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs`: after the
  actual state change, source code stacks `EffectTiming.OnUnTappedAnyone` with
  `CardEffect` and `Permanents` payload entries.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\TurnStateMachine.cs`: active phase
  unsuspends battle-area permanents through `IUnsuspendPermanents`, while
  breeding-area unsuspend is handled directly and does not enter the event stack.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\CanUseEffects\OnUnsuspend.cs`:
  `CanTriggerWhenPermanentUnsuspends` reads the same `Permanents` payload shape
  used by suspend/unsuspend trigger helpers.
- `E:\headlessDCGO\DCGO\Assets\Scripts\CardEffect\BT9\Black\BT9_089.cs`: sample
  consumer uses `EffectTiming.OnUnTappedAnyone` and checks whether an opponent's
  Digimon became unsuspended during main phase.

## Headless Mapping

- `Tier1PrimitiveService.Unsuspend(...)` now queues `OnUnTappedAnyone` only when
  the permanent actually changes from suspended to unsuspended.
- The queued event is skipped for breeding-area permanents, matching the source
  active-phase split where breeding unsuspend is direct state repair.
- `PhaseRunner.RunActivePhaseWithResult(...)` now routes battle-area active-phase
  and Reboot unsuspend through `Tier1PrimitiveService.Unsuspend(...)` instead of
  setting `IsSuspended=false` directly.
- `BattleEngineServices.Create(...)` passes the shared primitive service into
  `PhaseRunner`, preserving the production service graph.

## Payload Contract

The runtime payload supplied to `OnUnTappedAnyone` includes:

| Key | Value |
| --- | --- |
| `Permanents` | one-element unsuspended permanent list |
| `Permanent` | unsuspended permanent |
| `UnsuspendedPermanents` | one-element unsuspended permanent list |
| `UnsuspendedPermanent` | unsuspended permanent |
| `UnsuspendedController` | controller of the unsuspended permanent |
| `UnsuspendedTopCard` | top card of the unsuspended permanent |
| `Cards` | one-element top card list |
| `CardSources` | one-element top card list |
| `CardEffect` | optional source card, currently null for default call sites |
| `SourceCard` | optional source card |
| `SourcePermanent` | optional source permanent |
| `SourceZone` | `Zone.BattleArea` for queued events in this slice |
| `DestinationZone` | `Zone.BattleArea` |

## Verification

- Added regression: `FND-003-H OnUnTappedAnyone queues actual unsuspend event`.
- The regression proves that a successful primitive unsuspend queues an
  `OnUnTappedAnyone` event, duplicate unsuspend attempts do not enqueue another
  event, and `RunAutoProcess` drains the queued event into a trigger descriptor.
- Full regression command:
  `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- Latest expected result for this scope: `All 618 tests passed.`

## Explicitly Excluded

- No individual `CardEffect` body implementation.
- No C0039+ card-porting.
- No generated status promotion to `Implemented` or `Verified`.
- No Foundation Gate number manipulation.
- No source `DCGO/Assets` modification.
- `WhenUntapAnyone` pre-cut-in/replacement behavior is not closed by this slice.
- Full `CanUnsuspend` / cannot-unsuspend policy and effect immunity re-check are
  still follow-up scope.
- Existing call sites mostly do not pass source-card/source-permanent metadata
  into the primitive yet; the payload supports it, but broad source propagation
  remains a follow-up.

## Next Candidate

The next FND-003 queue item is `FND-003-I OnMove`.
