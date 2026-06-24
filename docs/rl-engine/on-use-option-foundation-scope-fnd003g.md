# FND-003-G OnUseOption Foundation Scope

## Goal

`FND-003-G` closes the first runtime foundation slice for `EffectTiming.OnUseOption`.
The scope is option cards played from hand: after the option is moved to `Executing`
and after cost payment / `AfterPayCost`, headless execution now runs source-aligned
`OnUseOption` trigger/background processing before the option's main `OptionSkill`.

This is foundation plumbing only. It does not implement individual card effect
bodies and does not unlock C0039+ card-porting.

## Source Evidence

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs`: `EffectTiming.OnUseOption`
  is a distinct timing from `OptionSkill`.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs`: option play removes
  the card from its previous zones, adds it to `Executing`, builds a payload with
  `Card`, `Root`, and `Cost`, then calls `StackSkillInfos(..., EffectTiming.OnUseOption)`.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs`: immediately after
  stacking `OnUseOption`, source processing calls
  `ActivateBackgroundEffects(..., EffectTiming.OnUseOption)`.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs`: only after that
  source flow iterates the option card's `EffectTiming.OptionSkill` effects.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons.cs`: option play
  legality and cost rules remain source-aligned inputs to the play pipeline.

## Headless Mapping

- `PlayCardService.PlayOptionFromHandAfterValidation(...)` now executes:
  `move hand option to Executing -> pay option cost -> AfterPayCost -> OnUseOption -> OptionSkill -> Trash`.
- `OnUseOption` is collected as a global/background trigger timing with
  `sourceCard: null`, while the payload's `Card` / `SourceCard` stays the option
  card that is being used.
- `OptionSkill` remains scoped to the executing option card and still resolves
  through the existing option lifecycle.
- `ExecuteBackgroundEffects=true` is used for `OnUseOption` to preserve the source
  order of stack processing followed by background activation.

## Payload Contract

The runtime payload supplied to `OnUseOption` includes:

| Key | Value |
| --- | --- |
| `Card` | option card instance being used |
| `Cards` | one-element option card list |
| `CardSources` | one-element option card list |
| `SourceCard` | option card instance being used |
| `Root` | `Zone.Hand` |
| `Cost` | base/source option play cost |
| `PaidCost` | resolved paid cost |
| `PayCost` | `true` |
| `SourceZone` | `Zone.Executing` |
| `ActivatedFromSecurity` | `false` |

## Verification

- Added regression: `FND-003-G OnUseOption runs before OptionSkill`.
- The regression proves that global/background `OnUseOption` effects can change
  state before the option card's `OptionSkill` activation is evaluated.
- Full regression command:
  `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- Latest expected result for this scope: `All 617 tests passed.`

## Explicitly Excluded

- No individual `CardEffect` body implementation.
- No C0039+ card-porting.
- No generated status promotion to `Implemented` or `Verified`.
- No Foundation Gate number manipulation.
- No source `DCGO/Assets` modification.
- Security-activated option main-flow parity is not changed in this slice.
- `OnUseOption` effects that produce a pending selection before `OptionSkill`
  are explicitly unsupported for now and throw `UnsupportedMechanicException`.

## Next Candidate

The next foundation queue item remains `FND-003-H OnUnTappedAnyone`, unless the
gate scheduler selects a higher-priority foundation blocker after recalculation.
