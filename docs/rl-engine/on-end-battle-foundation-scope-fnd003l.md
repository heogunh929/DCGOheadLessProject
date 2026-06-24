# FND-003-L OnEndBattle Foundation Scope

## Source of Truth

- Source root: `E:\headlessDCGO\DCGO\Assets`
- Primary source flow: `Scripts\Script\CardController.cs:4675-4718`
- Timing definition: `Scripts\Script\ICardEffect.cs:1012`
- Factory/trigger evidence: `Scripts\Script\CardEffectFactory.cs:452`

## Source Contract

Unity battle flow resolves winner/loser/tie state, keeps winner and loser permanent snapshots, resolves battle destruction, then stacks `EffectTiming.OnEndBattle`. The battle-end trigger occurs before the later security-check determination and before `UntilEndBattleEffects` cleanup.

The source payload shape observed around the battle-end stack includes:

- `WinnerPermanents`
- `WinnerPermanents_real`
- `LoserPermanents`
- `LoserPermanents_real`
- `LoserCard`
- `WasTie`
- `battle`

Destroyed loser permanent references may become stale after battle destruction, so the headless implementation preserves top-card snapshots for deterministic post-destruction inspection.

## Implemented Scope

`AttackService` now runs `EffectTiming.OnEndBattle` for permanent-vs-permanent battles after `BattleResolver.ResolvePermanentBattle(...)` and before `DurationCleanupService.CleanupBattleEnd(...)`.

`ContinueAfterEndBattle` was added so selection-bound `OnEndBattle` effects can pause and resume before cleanup, piercing security checks, and `OnEndAttack`.

The implemented runtime scope does not add or implement any card-specific `CardEffect` body.

## Payload Contract

The headless payload preserves the source-aligned battle-end aliases plus deterministic inspection fields:

- `Battle`, `battle`, `BattleResult`
- `Permanents`, `BattlePermanents`
- `WinnerPermanents`, `WinnerPermanents_real`, `WinnerTopCards`
- `LoserPermanents`, `LoserPermanents_real`, `DestroyedPermanents`, `LoserTopCards`
- `LoserCard`
- `WasTie`
- `Attacker`, `Defender`, `Blocker`, `IsBlocking`
- `AttackerTopCard`, `DefenderTopCard`

## Verification

- `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- Result: `All 623 tests passed.`

Added regression coverage:

- `FND-003-L OnEndBattle queues battle end event`
- `FND-003-L OnEndBattle selection pauses and resumes`

## Remaining Follow-Ups

- Security Digimon battle `OnEndBattle` parity remains separate from the permanent-vs-permanent battle scope.
- Strict Unity clone parity for `WinnerPermanents_real` and `LoserPermanents_real` remains a parity candidate.
- `LoserCard` security-card payload parity remains a candidate for later security battle work.
- Battle object lifetime and stale permanent handling should be expanded with source trace fixtures.
- Card-specific battle-end effect bodies remain deferred until Foundation Gate allows card-porting.

## Guardrails

- Original `DCGO/Assets` was not modified.
- No individual `CardEffect` body was implemented.
- No C0039 or later card-porting batch was executed.
- No Foundation Gate numbers were manually manipulated.
- No generated status was promoted to `Implemented` or `Verified`.
- No RL Environment, Observation, Reward, Dataset, or Trainer component was implemented.
