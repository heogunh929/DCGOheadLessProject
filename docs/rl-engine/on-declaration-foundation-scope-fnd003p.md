# FND-003-P OnDeclaration Foundation Scope

## Source Evidence

- Source root: `E:\headlessDCGO\DCGO\Assets`
- Timing definition: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs:973` (`EffectTiming.OnDeclaration`)
- Card source discovery: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs:1046-1053`
- Permanent discovery: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs:1613-1627`
- Main phase availability checks: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\TurnStateMachine.cs:917-929`
- Field command creation: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\TurnStateMachine.cs:1467-1502`
- Hand command creation: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\TurnStateMachine.cs:2802-2850`
- Selected effect resolution: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\TurnStateMachine.cs:3050-3080`
- Action payloads: `MainPhaseAction\ActivatePermanentAction.cs`, `MainPhaseAction\ActivateCardAction.cs`

## Implemented Headless Scope

- Added `DeclareEffectAction` as the headless equivalent of Unity `ActivatePermanentAction` / `ActivateCardAction`.
- Added `DeclarationEffectService` to discover declarable `EffectTiming.OnDeclaration` descriptors from field top, inherited, linked, hand, and trash sources owned by the turn player.
- `LegalActionGenerator` now emits `LegalActionKind.ActivateEffect` actions when the production service graph has a declaration service.
- `ActionExecutor` executes `DeclareEffectAction` through the shared `TriggerPipelineService`.
- `BattleEngineServices` wires `DeclarationEffectService` into the production runtime graph and validates shared dependency identity.
- The action preserves source card, source permanent, source zone, source role, once-per-turn key, and activation metadata. Execution revalidates the source role before running the selected descriptor.

## Tests

- `OnDeclaration legal actions include field hand trash`
- `OnDeclaration declare action executes selected effect`
- `OnDeclaration declare action revalidates source role`
- Full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 633 tests passed.`

## Explicit Non-Scope

- No individual `CardEffect` body was implemented.
- No C0039 or later card-porting batch was executed.
- No generated status was promoted.
- No Foundation Gate value was manually changed.
- No original `DCGO/Assets` source was modified.

## Follow-Up Candidates

- Card-specific `OnDeclaration` bodies and payment/body continuation details.
- Trash `[Main]` effects that move their source after activation, such as BT20-096 style flows.
- Strict Unity command index parity fixture for `ActivateCardAction` / `ActivatePermanentAction`.
- Broader once-per-turn legal-action hiding policy fixture.
- FND-001-A `ContinuousOrStaticEffect` first CloseableFoundationTask bundle.
