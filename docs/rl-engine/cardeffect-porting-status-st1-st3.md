# ST1-ST3 CardEffect Porting Inventory

Updated: 2026-06-14

This document is the planning baseline for expanding the completed ST1 target deck to the ST1-ST3 target card pool. It is intentionally an inventory and gap analysis, not a claim that this task implemented ST2/ST3 code.

Current worktree note: there are already uncommitted ST2/ST3 implementation files from earlier work in this local workspace. This document records the requested source inventory and pass plan baseline only. Those existing code changes are not modified by this task and are not treated here as an accepted completion gate.

## Data Sources

| Source | Use |
| --- | --- |
| `DCGO/Assets/CardBaseEntity/ST1/**` | authoritative card IDs, English card names, and `CardEffectClassName` |
| `DCGO/Assets/CardBaseEntity/ST2/**` | authoritative ST2 card IDs, names, effect class names |
| `DCGO/Assets/CardBaseEntity/ST3/**` | authoritative ST3 card IDs, names, effect class names |
| `DCGO/Assets/Scripts/CardEffect/ST1/**` | original ST1 CardEffect implementation |
| `DCGO/Assets/Scripts/CardEffect/ST2/**` | original ST2 CardEffect implementation |
| `DCGO/Assets/Scripts/CardEffect/ST3/**` | original ST3 CardEffect implementation |
| `DCGO/Assets/Scripts/Script/ICardEffect.cs` | effect timing, duration, activation semantics |
| `DCGO/Assets/Scripts/Script/CardEffectCommons.cs` | common Unity effect helpers and zone/effect operations |
| `DCGO/Assets/Scripts/Script/CardController.cs`, `AutoProcessing.cs`, `AttackProcess.cs`, `TurnStateMachine.cs` | play, security, trigger, attack, and turn timing mapping |

## Inventory Summary

| Metric | Count |
| --- | ---: |
| ST1 cards | 16 |
| ST2 cards | 16 |
| ST3 cards | 16 |
| Total target cards | 48 |
| Cards with non-empty `CardEffectClassName` | 36 |
| Unique CardEffect files under `ST1/**`, `ST2/**`, `ST3/**` | 34 |
| NoEffect candidates with empty `CardEffectClassName` | 12 |
| ST2 immediate implementation candidates | 2 |
| ST3 immediate implementation candidates | 5 |
| ST2/ST3 immediate implementation candidates total | 7 |

`ST2-07` and `ST3-07` reference the existing `ST1_06` CardEffect class instead of a set-local ST2/ST3 file. They are not NoEffect candidates; they are shared Blocker/attack-memory effect candidates.

## ST1 Baseline

ST1 is the completed baseline from local commit `4b8260e3 chore: checkpoint ST1 target deck completion`. This task does not change ST1 implementation.

| CardId | Card name | CardEffect class | Source file | Baseline status |
| --- | --- | --- | --- | --- |
| ST1-01 | Koromon | `ST1_01` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_01.cs` | Implemented |
| ST1-02 | Biyomon | none | none | NoEffect |
| ST1-03 | Agumon | `ST1_03` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_03.cs` | Implemented |
| ST1-04 | Dracomon | none | none | NoEffect |
| ST1-05 | Birdramon | none | none | NoEffect |
| ST1-06 | Coredramon | `ST1_06` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs` | Implemented |
| ST1-07 | Greymon | `ST1_07` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_07.cs` | Implemented |
| ST1-08 | Garudamon | `ST1_08` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_08.cs` | Implemented |
| ST1-09 | MetalGreymon | `ST1_09` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_09.cs` | Implemented |
| ST1-10 | Phoenixmon | none | none | NoEffect |
| ST1-11 | WarGreymon | `ST1_11` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_11.cs` | Implemented |
| ST1-12 | Tai Kamiya | `ST1_12` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_12.cs` | Implemented |
| ST1-13 | Shadow Wing | `ST1_13` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_13.cs` | Implemented |
| ST1-14 | Starlight Explosion | `ST1_14` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_14.cs` | Implemented |
| ST1-15 | Giga Destroyer | `ST1_15` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_15.cs` | Implemented |
| ST1-16 | Gaia Force | `ST1_16` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_16.cs` | Implemented |

## ST2 Porting Classification

| CardId | Card name | Original CardEffect class | Original file | Main timing | Effect type | Needed primitive/capability | Existing layer fit | Expected status | Difficulty | Tests needed |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| ST2-01 | Tsunomon | `ST2_01` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_01.cs` | `None` | inherited continuous DP while owner turn and battling no-source opponent | opponent no-source battle condition, inherited continuous DP | continuous-effect exists, no-source battle helper must be reviewed | NeedsCommonLayer | Medium | effective DP condition by battle opponent/source count |
| ST2-02 | Gomamon | none | none | none | vanilla/no effect | explicit NoEffect registration | existing NoEffect policy | NoEffect | Low | deck validation accepts explicit NoEffect |
| ST2-03 | Gabumon | `ST2_03` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_03.cs` | `OnAllyAttack` | select opponent level 5 or less Digimon and trash bottom source | source trash primitive, selection, attack timing | selection and trigger pipeline exist; source-trash primitive needed | NeedsCommonLayer | Medium | valid/invalid selection, bottom source trash, stale target |
| ST2-04 | Bearmon | none | none | none | vanilla/no effect | explicit NoEffect registration | existing NoEffect policy | NoEffect | Low | deck validation accepts explicit NoEffect |
| ST2-05 | Ikkakumon | none | none | none | vanilla/no effect | explicit NoEffect registration | existing NoEffect policy | NoEffect | Low | deck validation accepts explicit NoEffect |
| ST2-06 | Garurumon | `ST2_06` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_06.cs` | `OnAllyAttack` | select opponent Digimon and trash bottom source | source trash primitive, selection, attack timing | selection and trigger pipeline exist; source-trash primitive needed | NeedsCommonLayer | Medium | candidate source count, bottom source trash |
| ST2-07 | Grizzlymon | `ST1_06` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs` | `None`, `OnAllyAttack` | shared Blocker static plus lose 2 memory on attack | existing blocker keyword and memory modifier | already covered by ST1-06 generic mapping | ImplementableNow | Low | shared class registration, blocker legal action, attack memory loss |
| ST2-08 | WereGarurumon | `ST2_08` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_08.cs` | `None` | inherited continuous SecurityAttack +1 if opponent has no-source Digimon | opponent no-source condition, continuous SecurityAttack | continuous-effect exists; condition helper needed | NeedsCommonLayer | Medium | owner turn, opponent no-source, security attack calculation |
| ST2-09 | Zudomon | `ST2_09` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_09.cs` | `OnEnterFieldAnyone` as WhenDigivolving | select opponent Digimon and trash bottom 2 sources | WhenDigivolving mapping, source trash primitive | trigger pipeline exists; source-trash primitive needed | NeedsCommonLayer | Medium | digivolve trigger and source count handling |
| ST2-10 | Plesiomon | none | none | none | vanilla/no effect | explicit NoEffect registration | existing NoEffect policy | NoEffect | Low | deck validation accepts explicit NoEffect |
| ST2-11 | MetalGarurumon | `ST2_11` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_11.cs` | `OnAllyAttack` | once per turn unsuspend self | unsuspend primitive, once-per-turn attack trigger | trigger pipeline/once tracker exist; unsuspend primitive needed | NeedsCommonLayer | Medium | once-per-turn, self source validation, suspended/unsuspended state |
| ST2-12 | Matt Ishida | `ST2_12` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_12.cs` | `OnStartTurn`, `SecuritySkill` | gain memory if opponent has no-source Digimon; security play-self tamer | no-source condition, memory, security play-self tamer | memory and security self-play exist; no-source condition needed | NeedsCommonLayer | Medium | start turn trigger, security self-play, no duplicate zone |
| ST2-13 | Hammer Spark | `ST2_13` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_13.cs` | `OptionSkill`, `SecuritySkill` | main gain 1 memory; security gain 2 memory | memory modifier, security effect execution | existing memory/security effect path should cover | ImplementableNow | Low | main/security memory amount, security card final zone |
| ST2-14 | Sorrow Blue | `ST2_14` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_14.cs` | `OptionSkill`, `SecuritySkill` | select no-source opponent Digimon; cannot attack/block duration | restriction modifiers, no-source condition, selection, security | selection/duration exist; cannot attack/block layer needed | NeedsCommonLayer | High | legal attack/block filtering, duration cleanup, security duration difference |
| ST2-15 | Kaiser Nail | `ST2_15` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_15.cs` | `OptionSkill`, `SecuritySkill` | choose own Digimon source and play it free as Digimon | chained permanent/card selection, source-card play primitive | selection boundary exists; source-origin play is complex | NeedsComplexMechanic | High | chained selection resume, source removal, new permanent invariant |
| ST2-16 | Cocytus Breath | `ST2_16` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_16.cs` | `OptionSkill`, `SecuritySkill` | return opponent Digimon to hand; trash sources | return-to-hand primitive with source trash, security main reuse | security main reuse exists; bounce primitive needed | NeedsCommonLayer | Medium | top to hand, sources to trash, security activation |

## ST3 Porting Classification

| CardId | Card name | Original CardEffect class | Original file | Main timing | Effect type | Needed primitive/capability | Existing layer fit | Expected status | Difficulty | Tests needed |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| ST3-01 | Tokomon | `ST3_01` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_01.cs` | `OnDestroyedAnyone` | inherited once/turn DP +1000 when opponent deleted by 0 DP | DP-zero deletion event classification, once tracker, duration DP | trigger/duration exist; DP-zero payload needed | NeedsComplexMechanic | High | fires only for DP-zero deletion, not battle/effect delete |
| ST3-02 | Salamon | none | none | none | vanilla/no effect | explicit NoEffect registration | existing NoEffect policy | NoEffect | Low | deck validation accepts explicit NoEffect |
| ST3-03 | Tapirmon | none | none | none | vanilla/no effect | explicit NoEffect registration | existing NoEffect policy | NoEffect | Low | deck validation accepts explicit NoEffect |
| ST3-04 | Patamon | `ST3_04` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_04.cs` | `OnDestroyedAnyone` | inherited once/turn memory +1 when opponent deleted by 0 DP | DP-zero deletion event classification, once tracker, memory | trigger/memory exist; DP-zero payload needed | NeedsComplexMechanic | High | fires only for DP-zero deletion, once-per-turn |
| ST3-05 | Angemon | `ST3_05` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_05.cs` | `OnAllyAttack` | gain 1 memory if owner has 4+ security | attack trigger, security-count condition, memory | existing trigger/memory path should cover | ImplementableNow | Low | threshold 4 security, attack trigger source validation |
| ST3-06 | Gatomon | none | none | none | vanilla/no effect | explicit NoEffect registration | existing NoEffect policy | NoEffect | Low | deck validation accepts explicit NoEffect |
| ST3-07 | Unimon | `ST1_06` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs` | `None`, `OnAllyAttack` | shared Blocker static plus lose 2 memory on attack | existing blocker keyword and memory modifier | already covered by ST1-06 generic mapping | ImplementableNow | Low | shared class registration, blocker legal action, attack memory loss |
| ST3-08 | MagnaAngemon | `ST3_08` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_08.cs` | `OnAllyAttack` | select opponent Digimon and DP -1000 for turn | selection, temporary DP, attack trigger | existing selection/duration/trigger should cover | ImplementableNow | Low | valid/invalid/stale target, cleanup |
| ST3-09 | Angewomon | `ST3_09` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_09.cs` | `OnEnterFieldAnyone` as WhenDigivolving | Recovery +1 from deck if 3 or less security | recovery/add security primitive, digivolve trigger | trigger exists; recovery primitive needed | NeedsCommonLayer | Medium | deck top to security, empty deck failure, condition threshold |
| ST3-10 | Magnadramon | none | none | none | vanilla/no effect | explicit NoEffect registration | existing NoEffect policy | NoEffect | Low | deck validation accepts explicit NoEffect |
| ST3-11 | Seraphimon | `ST3_11` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_11.cs` | `OnAllyAttack` | select opponent Digimon and DP -4000 for turn | selection, temporary DP, attack trigger | existing selection/duration/trigger should cover | ImplementableNow | Low | valid/invalid/stale target, cleanup |
| ST3-12 | T.K. Takaishi | `ST3_12` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_12.cs` | `None`, `SecuritySkill` | opponent-turn security Digimon DP +2000; security play-self tamer | security Digimon effective DP, security self-play tamer | self-play exists; SecurityDigimonDP continuous layer needed | NeedsCommonLayer | Medium | security battle DP, owner/opponent turn, self-play zones |
| ST3-13 | Heaven's Gate | `ST3_13` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_13.cs` | `OptionSkill`, `SecuritySkill` | main DP +3000; security all Digimon/Security Digimon DP +5000 then add this card to hand | player-wide DP, SecurityDigimonDP, add security card to hand | selection/duration exists; add-to-hand/security DP layer needed | NeedsCommonLayer | High | executing-to-hand, field/security DP, cleanup |
| ST3-14 | Heaven's Charm | `ST3_14` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_14.cs` | `OptionSkill`, `SecuritySkill` | main opponent DP -2000; security add this card to hand | selection, temporary DP, add security card to hand | main path exists; security add-to-hand needed | NeedsCommonLayer | Medium | option DP, security final zone hand |
| ST3-15 | Holy Flame | `ST3_15` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_15.cs` | `OptionSkill`, `SecuritySkill` | target SecurityAttack -3 until opponent turn end; security opponent-wide -1 for turn | negative SecurityAttack duration, player-wide duration | duration exists; SecurityAttack lower-bound/legal check needed | NeedsCommonLayer | High | effective SecurityAttack clamp/policy, cleanup scope |
| ST3-16 | Seven Heavens | `ST3_16` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_16.cs` | `OptionSkill`, `SecuritySkill` | opponent Digimon DP -10000; security activates main | selection, temporary DP, security main reuse | existing option/security-main path should cover | ImplementableNow | Low | main/security share target logic, DP-zero rule interaction |

## Common Missing Layers Extracted From ST2/ST3

| Missing layer | Cards | Notes |
| --- | --- | --- |
| draw/search/reveal/filter | none observed in ST2/ST3 source inventory | keep as future card-pool category; no ST2/ST3 effect file currently requires it |
| add security/recover | ST3-09 | `IRecovery` maps to deck top to security |
| return to hand/deck top/deck bottom | ST2-16 | hand bounce with source trash; deck top/bottom not observed in ST2/ST3 |
| trash/delete by DP | ST2-03, ST2-06, ST2-09, ST3-01, ST3-04 | source trash and DP-zero deletion classification are distinct |
| memory gain/loss | ST2-07, ST2-12, ST2-13, ST3-04, ST3-05, ST3-07 | primitive exists, but triggers/conditions differ |
| DP/SecurityAttack modifier | ST2-01, ST2-08, ST3-01, ST3-08, ST3-11, ST3-13, ST3-14, ST3-15, ST3-16 | includes temporary, continuous, and negative SecurityAttack |
| option color requirement | ST2-13, ST2-14, ST2-15, ST2-16, ST3-13, ST3-14, ST3-15, ST3-16 | general option play legality should remain outside card-specific bodies |
| security effect execution | ST2-12, ST2-13, ST2-14, ST2-15, ST2-16, ST3-12, ST3-13, ST3-14, ST3-15, ST3-16 | includes security main reuse, direct security effects, self-play, add-to-hand |
| tamer/player aura | ST2-12, ST3-12 | field tamer and security self-play split must stay explicit |
| inherited effect | ST2-01, ST2-08, ST3-01, ST3-04, ST3-05, ST3-08 | source collection and top/source zone distinction must stay deterministic |
| blocker/keyword/battle timing | ST2-07, ST2-11, ST3-07 | shared Blocker plus unsuspend timing |
| counter/cut-in/replacement effect | none observed in ST2/ST3 source inventory | future complex mechanic bucket |
| complex chained/replacement mechanic | ST2-15, ST3-01, ST3-04 | multi-selection source play and DP-zero event attribution |

## Validation Planning

The ST1 target deck remains the committed baseline. ST1-ST3 validation should be introduced as a new target-pool gate that reports:

- all 48 target cards,
- NoEffect candidates with empty `CardEffectClassName`,
- shared effect-class cards such as `ST2-07` and `ST3-07`,
- `ImplementableNow`, `NeedsCommonLayer`, `NeedsComplexMechanic`, and `Unsupported` counts,
- per-card missing layer IDs.

Until the implementation passes in `st1-st3-porting-plan.md` are accepted, ST1-ST3 validation should not be treated as complete.
