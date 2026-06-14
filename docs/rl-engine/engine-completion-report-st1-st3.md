# ST1-ST3 Engine Completion Planning Report

Updated: 2026-06-14

This report defines the planned ST1-ST3 target card pool completion gate. It is not a completion claim for this task. The current task is documentation-only inventory and gap analysis; no ST2/ST3 implementation code is added here.

Current worktree note: uncommitted ST2/ST3 implementation files already exist locally from previous work. This report does not validate or accept those changes. It records the requested planning baseline before future implementation passes are reviewed.

## Current Baseline

| Area | Status |
| --- | --- |
| ST1 target deck | Completed baseline at commit `4b8260e3 chore: checkpoint ST1 target deck completion` |
| ST2 target pool | Inventory complete; implementation pass plan required |
| ST3 target pool | Inventory complete; implementation pass plan required |
| ST1-ST3 validation | Designed only in this task; not run as a completion gate |
| RL training stage | Not allowed; no ObservationEncoder/RewardCalculator/DatasetExporter/Trainer work |
| Unity source files | Must remain read-only source of truth |

## Inventory Counts

| Metric | Count |
| --- | ---: |
| ST1 cards | 16 |
| ST2 cards | 16 |
| ST3 cards | 16 |
| Total target cards | 48 |
| Cards with non-empty `CardEffectClassName` | 36 |
| Unique CardEffect files under ST1/ST2/ST3 | 34 |
| NoEffect candidates | 12 |
| ST2/ST3 immediate candidates | 7 |
| ST2/ST3 `NeedsCommonLayer` candidates | 14 |
| ST2/ST3 `NeedsComplexMechanic` candidates | 3 |

`ST2-07` and `ST3-07` use the shared `ST1_06` CardEffect class. They are counted as effect-bearing cards, not NoEffect cards.

## Planned Completion Gates

| Gate | Planned result before implementation passes | Evidence/required report |
| --- | --- | --- |
| forbidden dependency guard | Should remain pass | existing dependency guard tests; no Unity dependency in RL.Engine |
| target card pool documented | Pass after this document set | `cardeffect-porting-status-st1-st3.md` lists all 48 cards |
| ST1 baseline preserved | Pass by inspection/test | ST1 status remains unchanged |
| ST1-ST3 deck validation | Expected fail until implementation passes complete | report must list `NeedsCommonLayer` and `NeedsComplexMechanic` cards |
| unsupported mechanic zero | Expected fail or pending before implementation passes | no silent NoEffect or stub acceptance for ST2/ST3 |
| NoEffect explicitness | Planned pass | 12 empty `CardEffectClassName` cards must be explicit NoEffect candidates |
| replay determinism | Pending for ST2/ST3 | run after each implementation pass |
| invariant smoke | Pending for ST2/ST3 | run after each implementation pass |
| golden scenarios | Planned | pass 09 should add ST1-ST3 golden scenarios |
| RL training guard | Pass by policy | no RL training API implementation |

## Source Mapping Summary

| Unity source | Planned RL.Engine mapping |
| --- | --- |
| `CEntity_Base.CardID`, `CardName_ENG`, `CardEffectClassName` | `CardDefinition.CardId`, `CardDefinition.CardNameEnglish`, effect class mapping |
| `ICardEffect.EffectTiming.None` | continuous/inherited/static descriptor collection |
| `ICardEffect.EffectTiming.OnAllyAttack` | `TriggerPipelineService` attack timing |
| `ICardEffect.EffectTiming.OnEnterFieldAnyone` with `CanTriggerWhenDigivolving` | `WhenDigivolving` compatibility hook |
| `ICardEffect.EffectTiming.OnDestroyedAnyone` | destruction event payload; DP-zero classification for ST3-01/04 |
| `ICardEffect.EffectTiming.OnStartTurn` | phase/start-turn trigger hook |
| `ICardEffect.EffectTiming.OptionSkill` | option main effect descriptor and resolution |
| `ICardEffect.EffectTiming.SecuritySkill` | `SecurityEffectExecutionService` direct security effect or Activate Main routing |
| `CardEffectCommons.TrashDigivolutionCardsFromTopOrBottom` | generic digivolution-source trash primitive |
| `CardEffectCommons.PlayPermanentCards(... root: DigivolutionCards)` | generic play-source-card-as-permanent primitive |
| `CardEffectCommons.AddThisCardToHand` | generic executing security card to hand path |
| `IRecovery(...).Recovery()` | deck-top to security recovery primitive |

## ST2/ST3 Gap Summary

| Category | Cards |
| --- | --- |
| NoEffect | ST2-02, ST2-04, ST2-05, ST2-10, ST3-02, ST3-03, ST3-06, ST3-10 |
| Shared existing effect class | ST2-07, ST3-07 |
| Implementable with existing layers | ST2-13, ST3-05, ST3-08, ST3-11, ST3-16 |
| Needs source/zone primitive | ST2-03, ST2-06, ST2-09, ST2-16, ST3-09, ST3-13, ST3-14 |
| Needs condition/restriction/stat layer | ST2-01, ST2-08, ST2-12, ST2-14, ST3-12, ST3-15 |
| Needs complex event/chained selection | ST2-15, ST3-01, ST3-04 |

## Validation Structure To Add Later

The future ST1-ST3 completion runner should emit a structured report with:

- target pool count: 48,
- per-set card count: ST1 16, ST2 16, ST3 16,
- `NoEffect`, `ImplementableNow`, `NeedsCommonLayer`, `NeedsComplexMechanic`, `Unsupported` counts,
- missing layer IDs by card,
- proof that NoEffect cards have empty source `CardEffectClassName`,
- proof that shared classes such as `ST2-07 -> ST1_06` are explicitly mapped,
- replay determinism and invariant smoke results after each pass.

## Test Status For This Task

No code test was run for this documentation-only task. The reason is that this task only updates planning documents and explicitly forbids implementation changes.

The most recent known test result from previous work remains:

```powershell
$env:DOTNET_CLI_HOME='E:\headlessDCGO\.dotnet_home'
$env:NUGET_PACKAGES='E:\headlessDCGO\.nuget\packages'
$env:TEMP='E:\headlessDCGO\.tmp'
$env:TMP='E:\headlessDCGO\.tmp'
.\\.dotnet\\dotnet.exe run --no-restore --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj
```

Result from previous work: `All 194 tests passed`, with an MSBuild cache access warning. This previous result is not newly executed for the current documentation-only inventory task.

## Remaining Scope Limits

- full Unity `MultipleSkills` simultaneous trigger priority and UI choice ordering,
- full counter/pay-cost timing: `BeforePayCost`, `AfterPayCost`, `OnCounterTiming`,
- wider replacement/cut-in mechanics from later sets,
- Unity trace parity harness,
- RL training components.

These limits must remain explicit. Unsupported or not-yet-planned ST2/ST3 behavior must not be accepted as silent no-op.
