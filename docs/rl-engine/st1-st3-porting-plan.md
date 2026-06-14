# ST1-ST3 Porting Plan

Updated: 2026-06-14

This is the fixed implementation pass plan for expanding the committed ST1 target deck baseline to the ST1-ST3 target card pool. This document is a plan only; this task does not add ST2/ST3 CardScript code.

Current worktree note: local uncommitted ST2/ST3 implementation files may already exist from earlier work. They are outside the scope of this documentation-only planning task and are not committed or accepted by this document.

Structure alignment note: ST1, ST2, and ST3 card scripts now mirror the Unity source layout under `src/DCGO.RL.Engine/CardEffects/{Set}/{Color}/{CardId}.cs`. `St1CardScriptCatalog` and `St2St3CardScriptCatalog` are registry-only. Shared helper logic that maps to common `CardEffectCommons`-style behavior lives outside the catalogs in support files such as `St1ScriptSupport`, `St2ScriptSupport`, `St3ScriptSupport`, and `StarterScriptSupport`.

## Planning Rules

- Use `DCGO/` Unity battle logic as source of truth.
- Do not modify `DCGO/Assets/Scripts` source files.
- Do not implement ST2/ST3-specific shortcuts.
- Do not accept no-op stubs for effects with original `CardEffectClassName`.
- Use existing generic boundaries first: `TriggerPipelineService`, `SelectionResultApplicator`, `TemporaryModifier`, `DurationCleanupService`, `ContinuousEffectService`, `SecurityEffectExecutionService`, `Tier1PrimitiveService`, and `ZoneMover`.
- Add a new common layer only when multiple cards or a general engine rule need it.
- Keep RL training components out of scope.

## Implementation Pass 01 - ST2 ST3 NoEffect and Vanilla Cards

Target cards:

- ST2: ST2-02 Gomamon, ST2-04 Bearmon, ST2-05 Ikkakumon, ST2-10 Plesiomon.
- ST3: ST3-02 Salamon, ST3-03 Tapirmon, ST3-06 Gatomon, ST3-10 Magnadramon.

Original files:

- No set-local CardEffect file and empty `CardEffectClassName` in `DCGO/Assets/CardBaseEntity/ST2/**` or `ST3/**`.

Implementation possibility:

- Low risk. These cards may be accepted only as explicit NoEffect entries after verifying that the Unity card asset has empty `CardEffectClassName`.
- ST2-07 and ST3-07 are excluded from this pass because they reference shared `ST1_06`.

Needed common layer:

- Explicit NoEffect registration/reporting in target pool validation.

Expected tests:

- ST2/ST3 NoEffect cards are accepted explicitly.
- NoEffect cards are not created by missing-file inference alone.
- A card with non-empty `CardEffectClassName` and missing mapping fails validation.

Card count: 8.

## Implementation Pass 02 - ST2 ST3 Existing Engine Capability Effects

Target cards:

- ST2-07 Grizzlymon: shared `ST1_06` Blocker/static and attack memory loss.
- ST2-13 Hammer Spark: main memory +1, security memory +2.
- ST3-05 Angemon: OnAllyAttack memory +1 if owner has 4+ security.
- ST3-07 Unimon: shared `ST1_06` Blocker/static and attack memory loss.
- ST3-08 MagnaAngemon: OnAllyAttack selection, opponent DP -1000 for turn.
- ST3-11 Seraphimon: OnAllyAttack selection, opponent DP -4000 for turn.
- ST3-16 Seven Heavens: option/security Activate Main, opponent DP -10000 for turn.

Original files:

- `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_13.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_05.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_08.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_11.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_16.cs`

Implementation possibility:

- These should be implementable with existing ST1-era generic layers if shared class mapping and non-red color cards are accepted by the catalog/validator.
- `ST3-16` must reuse the same main option body for hand play and security activation.

Needed common layer:

- Shared CardEffect class mapping for `ST2-07 -> ST1_06` and `ST3-07 -> ST1_06`.
- No new engine layer expected if memory, selection, temporary DP, blocker, and security Activate Main paths remain generic.

Expected tests:

- Shared `ST1_06` mapping is explicit and not treated as NoEffect.
- Blocker legal action and attack memory loss remain deterministic.
- ST2-13 main/security memory amounts are correct.
- ST3-05 security-count condition gates memory gain.
- ST3-08/ST3-11/ST3-16 valid/invalid/stale target selection and cleanup.
- ST3-16 security activation shares main target logic.

Card count: 7.

## Implementation Pass 03 - ST2 ST3 Draw Search Reveal Memory Effects

Target cards:

- ST2-07 Grizzlymon: attack memory -2 through shared `ST1_06`.
- ST2-12 Matt Ishida: OnStartTurn memory +1 if opponent has a no-source Digimon.
- ST2-13 Hammer Spark: main memory +1; security memory +2.
- ST3-04 Patamon: inherited memory +1 on opponent DP-zero deletion.
- ST3-05 Angemon: OnAllyAttack memory +1 if owner has 4+ security.
- ST3 draw/search/reveal effects: 해당 없음.
- ST2 draw/search/reveal effects: 해당 없음.

Original files:

- `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_12.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_13.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_04.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_05.cs`

Implementation possibility:

- Direct memory mutation is already a known primitive from ST1.
- ST2-12 requires opponent no-source condition.
- ST3-04 requires DP-zero deletion event classification before it can be safely implemented.
- No ST2/ST3 CardEffect file in this pool uses draw/search/reveal/filter.

Needed common layer:

- Opponent no-source condition helper.
- DP-zero deletion payload for ST3-04.

Expected tests:

- memory gain/loss values are exact,
- memory mutation is tied to the original timing,
- ST2-12 does not trigger if no opponent no-source Digimon exists,
- ST3-04 triggers only for DP-zero deletion and once per turn.

Card count: 5 memory-related cards plus no draw/search/reveal cards.

## Implementation Pass 04 - ST2 ST3 Zone Movement Security Recovery Effects

Target cards:

- ST2-03 Gabumon: trash bottom source from opponent level 5 or less Digimon.
- ST2-06 Garurumon: trash bottom source from opponent Digimon.
- ST2-09 Zudomon: WhenDigivolving, trash bottom 2 sources from opponent Digimon.
- ST2-15 Kaiser Nail: choose own Digimon source and play it as another Digimon without paying cost.
- ST2-16 Cocytus Breath: return opponent Digimon to hand and trash sources.
- ST3-09 Angewomon: Recovery +1 (Deck).
- ST3-13 Heaven's Gate: security add this card to hand after DP effects.
- ST3-14 Heaven's Charm: security add this card to hand.

Original files:

- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_03.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_06.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_09.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_15.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_16.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_09.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_13.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_14.cs`

Implementation possibility:

- Requires generic primitives, not card-specific list mutation.
- ST2-15 is the highest-risk item in this pass because it uses chained permanent/card selection and plays a card from a digivolution source zone.

Needed common layer:

- Bottom digivolution source trash primitive.
- Return permanent to hand with source trash.
- Recover from deck to security.
- Executing security card to hand.
- Play digivolution source as new permanent.

Expected tests:

- all movements use `ZoneMover`/primitive paths,
- source order is deterministic,
- stale target/source selection fails explicitly,
- executing security card can move to hand instead of default trash,
- no duplicate card remains across security/executing/field/hand/trash,
- replay hash remains stable.

Card count: 8.

## Implementation Pass 05 - ST2 ST3 Battle Keyword and Attack Timing Effects

Target cards:

- ST2-03 Gabumon: OnAllyAttack source trash.
- ST2-06 Garurumon: OnAllyAttack source trash.
- ST2-07 Grizzlymon: Blocker and attack memory loss through `ST1_06`.
- ST2-11 MetalGarurumon: OnAllyAttack once-per-turn unsuspend self.
- ST2-14 Sorrow Blue: cannot attack/cannot block restriction.
- ST3-05 Angemon: OnAllyAttack memory condition.
- ST3-07 Unimon: Blocker and attack memory loss through `ST1_06`.
- ST3-08 MagnaAngemon: OnAllyAttack DP -1000.
- ST3-11 Seraphimon: OnAllyAttack DP -4000.

Original files:

- `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_03.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_06.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_11.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_14.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_05.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_08.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_11.cs`

Implementation possibility:

- Attack timing should use `TriggerPipelineService`, not direct calls from individual card scripts.
- ST2-11 needs a generic unsuspend primitive and once-per-turn guard.
- ST2-14 needs legal attack/block filtering to consume temporary restrictions.

Needed common layer:

- Unsuspend primitive.
- Cannot attack/cannot block temporary modifier and legal-action integration.
- Attack timing regression coverage.

Expected tests:

- OnAllyAttack descriptors are collected and drained once,
- ST2-11 cannot unsuspend twice in one turn,
- blocked/attacking legal actions reflect temporary restrictions,
- existing ST1 blocker behavior remains unchanged.

Card count: 9.

## Implementation Pass 06 - ST2 ST3 Continuous Duration Inherited Effects

Target cards:

- ST2-01 Tsunomon: inherited DP +1000 on owner turn while battling no-source opponent.
- ST2-08 WereGarurumon: inherited SecurityAttack +1 if opponent has no-source Digimon on owner turn.
- ST2-14 Sorrow Blue: cannot attack/block until opponent/owner turn end depending main/security source.
- ST3-01 Tokomon: inherited DP +1000 for turn on DP-zero deletion.
- ST3-08 MagnaAngemon: inherited DP -1000 for turn.
- ST3-11 Seraphimon: DP -4000 for turn.
- ST3-12 T.K. Takaishi: opponent-turn Security Digimon DP +2000.
- ST3-13 Heaven's Gate: main DP +3000; security field/security DP +5000 for turn.
- ST3-15 Holy Flame: SecurityAttack -3 / player-wide -1 durations.
- ST3-16 Seven Heavens: DP -10000 for turn.

Original files:

- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_01.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_08.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_14.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_01.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_08.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_11.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_12.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_13.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_15.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_16.cs`

Implementation possibility:

- Separate continuous derived effects from temporary duration modifiers.
- Do not model duration effects as continuous effects just to simplify cleanup.
- Security Digimon effective DP should be derived for security battle/check calculations without mutating `CardDefinition`.

Needed common layer:

- Opponent no-source condition helper.
- Security Digimon effective DP.
- Negative SecurityAttack effective stat policy.
- Duration scope tests for owner/opponent turn end.

Expected tests:

- continuous and duration modifiers add deterministically,
- base card definition stats are unchanged,
- source zone removal disables inherited effects,
- SecurityAttack cannot create illegal security-check counts,
- StateHash/replay determinism remains stable.

Card count: 10.

## Implementation Pass 07 - ST2 ST3 Security Option and Security Digimon Effects

Target cards:

- ST2-12 Matt Ishida: security play-self tamer.
- ST2-13 Hammer Spark: security memory +2.
- ST2-14 Sorrow Blue: security direct restriction effect.
- ST2-15 Kaiser Nail: security activates main option.
- ST2-16 Cocytus Breath: security activates main option.
- ST3-12 T.K. Takaishi: security play-self tamer.
- ST3-13 Heaven's Gate: security field/security DP +5000 then add to hand.
- ST3-14 Heaven's Charm: security add to hand.
- ST3-15 Holy Flame: security opponent-wide SecurityAttack -1 for turn.
- ST3-16 Seven Heavens: security activates main option.

Original files:

- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_12.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_13.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_14.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_15.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_16.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_12.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_13.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_14.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_15.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_16.cs`

Implementation possibility:

- ST1 security option execution and play-self tamer layers provide a base, but ST3 add-to-hand and Security Digimon DP paths need extension.
- Security direct effects must choose their own after-resolution zone policy: trash, field, or hand.

Needed common layer:

- Security direct effect routing by effect type.
- Add checked security card to hand.
- Security Digimon DP effective stat.
- Security activation regression for existing ST1-15/16.

Expected tests:

- security option main reuse still sends options to trash unless original effect says otherwise,
- security self-play creates a permanent and avoids trash,
- add-to-hand moves executing card to hand and avoids trash,
- security direct effect selection uses `SelectionRequest`/`SelectionResult` boundary.

Card count: 10.

## Implementation Pass 08 - ST2 ST3 High Risk Unsupported Effects

Target cards:

- ST2-15 Kaiser Nail: chained selection and play source as new permanent.
- ST3-01 Tokomon: DP-zero deletion attribution.
- ST3-04 Patamon: DP-zero deletion attribution and memory gain.
- ST2-14 Sorrow Blue: cross-turn cannot attack/block duration from both main and security paths.
- ST3-15 Holy Flame: negative SecurityAttack with lower-bound policy and different main/security scopes.

Original files:

- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_14.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_15.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_01.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_04.cs`
- `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_15.cs`

Implementation possibility:

- These should be implemented only after the lower-level common layer is reviewed.
- If any original meaning remains uncertain, keep that card `NeedsComplexMechanic` or `Unsupported`; do not promote it to Implemented.

Needed common layer:

- DP-zero deletion event classification.
- Multi-step selection continuation for source-card play.
- Cross-turn restriction cleanup.
- Negative SecurityAttack policy.

Expected tests:

- DP-zero triggers do not fire for battle deletion or normal effect deletion,
- chained selections can pause/resume without UI coupling,
- source-card play removes exactly one source and creates exactly one permanent,
- restrictions and negative SecurityAttack clean up at exact owner/opponent turn boundary.

Card count: 5.

## Implementation Pass 09 - ST1 ST3 Completion Gate and Golden Scenarios

Target cards:

- Full target pool: ST1-01 through ST1-16, ST2-01 through ST2-16, ST3-01 through ST3-16.

Original files:

- All files under `DCGO/Assets/Scripts/CardEffect/ST1/**`, `ST2/**`, `ST3/**`.
- `DCGO/Assets/CardBaseEntity/ST1/**`, `ST2/**`, `ST3/**`.

Implementation possibility:

- This pass should not start until passes 01-08 have explicit reports and tests.
- The completion gate should pass only when ST1-ST3 validation reports no unsupported or partial cards for the target pool.

Needed common layer:

- No new rule layer should be introduced in pass 09 unless a prior pass missed a real gap.
- Completion runner/reporting and golden scenario fixtures are allowed.

Expected tests:

- target pool count is exactly 48,
- ST1 baseline remains passing,
- ST2/ST3 Unsupported and NeedsCommonLayer lists are empty only after implementation,
- replay determinism and invariant smoke pass,
- golden scenarios cover representative security, attack, inherited, continuous, zone movement, and chained-selection cases,
- no RL training components are present.

Card count: 48.

## Pass Size Review And Subtask Split

The following passes are too large to implement as one coding task. Split them before implementation:

| Pass | Split into smaller tasks |
| --- | --- |
| Pass 03 | 03A memory-only existing primitive cards, 03B opponent no-source start-turn condition, 03C DP-zero memory trigger after DP-zero event layer exists |
| Pass 04 | 04A bottom digivolution source trash, 04B return permanent to hand with source trash, 04C recovery from deck, 04D executing security card add-to-hand, 04E source-card play from digivolution source |
| Pass 05 | 05A shared `ST1_06` blocker mapping, 05B attack-trigger source trash regressions, 05C unsuspend once-per-turn, 05D cannot attack/block legal-action filtering |
| Pass 06 | 06A opponent no-source continuous conditions, 06B Security Digimon effective DP, 06C negative SecurityAttack policy, 06D cross-turn duration cleanup regression |
| Pass 07 | 07A security direct memory/self-play, 07B security Activate Main option regressions, 07C security add-this-card-to-hand, 07D security player-wide stat modifiers |
| Pass 08 | 08A DP-zero event attribution, 08B chained source-card play, 08C high-risk duration/restriction edge cases |
| Pass 09 | 09A target-pool validation report, 09B golden scenario fixtures, 09C replay/invariant smoke, 09D no-RL-training guard |

Pass 01 and Pass 02 are small enough to implement as single passes if the pre/post Git and test checks stay clean.

## Suggested Order

1. Review inventory against Unity `CardBaseEntity` and CardEffect files.
2. Implement pass 01 and pass 02 first because they have the least new mechanics.
3. Add one common primitive/layer at a time for passes 03-07.
4. Treat pass 08 as review-heavy and keep failing cards explicit until mechanics are proven.
5. Run pass 09 only after all prior pass reports are green.

## Questions For Review

- Should `ST2-01` follow the literal Unity implementation, which checks the battle opponent through `battle.enemyPermanent(...)`, or the card-text-like broader condition used by related ST2 cards? This needs source-of-truth review before implementation.
- Should `ST3-12` Security Digimon DP be modeled as continuous derived stat or as security-check-only effective stat?
- Should `ST2-15` source-card play reuse normal play placement rules or use a stricter source-origin play primitive?
- Should negative SecurityAttack be clamped at legal minimum during effective calculation, or should illegal negative check counts be represented and rejected by battle rules?
