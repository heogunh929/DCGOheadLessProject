# Full Card Porting C0021 Zone Security Recovery Source Mapping

## 결정

`C0021_zone_security_recovery`는 `done`으로 승격할 수 없는 카드 포팅 batch다. DCGO 원본 source body 10개와 asset identity 18개는 모두 확인했다. 그러나 runnable card body로 옮기려면 `L0006_zone_security_recovery`와 함께 reveal/search, hand discard, free hand/breeding play, hand digivolve, by-effect play payload, temporary granted effects, deletion/battle/source snapshots, static/inherited keywords, and return-to-library-bottom layers가 먼저 source-aligned로 정리되어야 한다.

Queue status: needs-review

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않는다. 같은 source effect를 여러 variant가 참조하더라도 `DefinitionStableId`, `CardIndex`, `variantKey`를 보존한다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT15_062` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_062.cs` | `OnEnterFieldAnyone`: On Play reveal top 4 through `SimplifiedRevealDeckTopCardsAndSelect`, add up to 2 level 6+ cards, bottom-deck rest. `OnEndTurn`: optional owner end turn effect; select and delete 1 own Digimon through `DeletePeremanentAndProcessAccordingToResult`, then on successful deletion may play 1 Dark Masters Digimon from hand to empty breeding area for free through `PlayPermanentCards` with ETB. `None`: inherited `RebootSelfStaticEffect`. | reveal multi-pick, bottom-deck ordering, delete-success continuation, breeding-area free play, effect-play ETB, and inherited Reboot timing are required. |
| `BT15_063` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_063.cs` | `None`: `BlockerSelfStaticEffect` and `AddSelfDigivolutionRequirementStaticEffect` from level 4 DigiPolice/X Antibody for cost 3. inherited `OnTappedAnyone`: once per turn, when an effect suspends another Digimon/Tamer, untap 1 suspended owner Beast Dragon/DigiPolice Digimon with hash `unsuspend_BT15_063`. A second optional `OnTappedAnyone` descriptor may digivolve this Digimon from hand for free with `DigivolveIntoHandOrTrashCard` if it has a DigiPolice Tamer source. | effect-caused suspend payload, duplicate same-timing descriptor ordering, untap selection with `canEndNotMax`, hand digivolve continuation, alternate digivolution requirement, source condition, and once-per-turn hash are required. |
| `BT15_067` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_067.cs` | `None`: `BlockerSelfStaticEffect` and level 5 DigiPolice/X Antibody alternate digivolution for cost 4. `OnTappedAnyone`: once per turn when this Digimon becomes suspended, may play 1 Beast Dragon/DigiPolice Digimon with 5000 DP or less from hand for free through `PlayPermanentCards`, hash `PlayDigimon_BT15_067`. When Digivolving: if this Digimon has a DigiPolice Tamer source, return 1 suspended opponent Digimon/Tamer to deck bottom through `SelectPermanentEffect.Mode.PutLibraryBottom`. | self-suspend payload, free hand play legality and ETB, suspended target return-to-library-bottom, source condition, alternate digivolution requirement, and once-per-turn hash are required. |
| `BT15_068` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_068.cs` | `OnEnterFieldAnyone`: On Play select 1 opponent Digimon and add a temporary granted `OnDestroyedAnyone` effect to that permanent through `CardEffectCommons.AddEffectToPermanent`; the granted effect loses 1 memory on deletion and uses `SetEffectSourcePermanent`. A second inherited `OnEnterFieldAnyone` descriptor gains 1 memory once per turn when an opponent Digimon is played by effect, hash `Memory1_BT15_068`. | temporary granted effect lifecycle, granted source permanent, deletion payload for the granted effect, by-effect play payload, inherited source validation, debuff visual equivalent as trace/invariant, and once-per-turn hash are required. |
| `BT15_069` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_069.cs` | `OnDestroyedAnyone`: on deletion, if opponent has 1 or less memory, draw 1 with `DrawClass`; if opponent has 1 or more memory, gain 1 memory. At exactly 1 memory both branches execute. | on-deletion source validation, memory-threshold branch ordering, draw boundary, memory gain, and exact overlapping threshold behavior are required. |
| `BT15_070` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_070.cs` | `OnEnterFieldAnyone`: On Play reveal top 4, add 1 card whose text contains `Myotismon`, bottom-deck rest, then if revealed candidates existed, discard 1 hand card with `SelectHandEffect.Mode.Discard`. inherited `OnDestroyedAnyone`: `RetaliationSelfEffect`. | reveal/search by text, source quirk where follow-up discard is based on revealed candidates, mandatory hand discard, bottom-deck ordering, and inherited Retaliation timing are required. |
| `BT15_073` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_073.cs` | When Digivolving and On Deletion descriptors both draw 1 with `DrawClass`, then discard 1 hand card if possible through `SelectHandEffect.Mode.Discard`. inherited `OnDestroyedAnyone`: `RetaliationSelfEffect`. | draw-then-discard sequencing, hand discard selection, OnDeletion/WhenDigivolving descriptor identity, inherited Retaliation, and draw/add-hand/discard trigger interleaving are required. |
| `BT15_074` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_074.cs` | `None`: `BlockerSelfStaticEffect` and owner-turn `CanNotAttackSelfStaticEffect` while opponent has no Digimon. On Play and When Digivolving descriptors let opponent optionally discard 1 Digimon from hand; if they do not, owner gains 1 memory. inherited `OnEnterFieldAnyone`: once per turn, when an effect plays an opponent Digimon, gain 1 memory with hash `Memory1_BT15_074`. | opponent-controlled optional discard, fallback memory branch, by-effect play payload, static cannot-attack restriction, Blocker, inherited source validation, and once-per-turn hash are required. |
| `BT15_077` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_077.cs` | `OnEnterFieldAnyone`: On Play reveal top 4, add up to 2 level 6+ cards, bottom-deck rest. `OnEndTurn`: optional owner end turn effect; delete 1 own Digimon through `DeletePeremanentAndProcessAccordingToResult`, then on successful deletion may play 1 Dark Masters Digimon from hand to empty breeding area for free through `PlayPermanentCards`. inherited `OnDestroyedAnyone`: `RetaliationSelfEffect`. | reveal multi-pick, delete-success continuation, breeding-area free play, inherited Retaliation, bottom-deck ordering, and effect-play ETB are required. |
| `BT15_080` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_080.cs` | `None`: `BlockerSelfStaticEffect`. On Play, When Digivolving, and On Deletion descriptors all select and delete 1 opponent Digimon with level 5 or lower through `SelectPermanentEffect.Mode.Destroy`, with deletion variant requiring `CanActivateOnDeletion`. | duplicated descriptor identity, level target legality, on-deletion source validation, destroy target selection, and Blocker static layer are required. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT15-062#3194@base` | `BT15_062` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_062.asset` | `needs-review` |
| `BT15-063#3195@base` | `BT15_063` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_063.asset` | `needs-review` |
| `BT15-063#4744@P0` | `BT15_063` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_063_P0.asset` | `needs-review` |
| `BT15-067#3200@base` | `BT15_067` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_067.asset` | `needs-review` |
| `BT15-067#4747@P0` | `BT15_067` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_067_P0.asset` | `needs-review` |
| `BT15-067#4748@P1` | `BT15_067` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_067_P1.asset` | `needs-review` |
| `BT15-067#4749@P2` | `BT15_067` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_067_P2.asset` | `needs-review` |
| `BT15-067#4750@P3` | `BT15_067` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_067_P3.asset` | `needs-review` |
| `BT15-068#3201@base` | `BT15_068` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_068.asset` | `needs-review` |
| `BT15-069#3202@base` | `BT15_069` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_069.asset` | `needs-review` |
| `BT15-070#3203@base` | `BT15_070` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_070.asset` | `needs-review` |
| `BT15-070#8186@P1` | `BT15_070` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_070_P1.asset` | `needs-review` |
| `BT15-073#3206@base` | `BT15_073` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_073.asset` | `needs-review` |
| `BT15-074#3207@base` | `BT15_074` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_074.asset` | `needs-review` |
| `BT15-074#4752@P0` | `BT15_074` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_074_P0.asset` | `needs-review` |
| `BT15-077#3211@base` | `BT15_077` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_077.asset` | `needs-review` |
| `BT15-080#3214@base` | `BT15_080` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_080.asset` | `needs-review` |
| `BT15-080#4755@P0` | `BT15_080` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_080_P0.asset` | `needs-review` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `needs-review`; C0021 depends on hand, deck-bottom, source, deletion, and by-effect play movement payloads.
- `BT15_062` and `BT15_077` require delete-success continuation into free breeding-area play; this must not be collapsed into unconditional play.
- `BT15_063` and `BT15_067` require effect-caused suspend payloads and source conditions before untap, hand digivolve, or free play bodies can be safe.
- `BT15_068` requires temporary granted effect support with source permanent tracking through `SetEffectSourcePermanent` and `CardEffectCommons.AddEffectToPermanent`.
- `BT15_069`, `BT15_073`, `BT15_074`, and `BT15_080` need on-deletion source snapshot/revalidation before draw, discard, memory, Retaliation, or deletion effects are runnable.
- `BT15_070` keeps the same reveal-candidate follow-up discard quirk seen in earlier SoC/Myotismon searches; the follow-up must be based on the source helper behavior, not guessed selected count.
- `BT15_074` makes the opponent choose whether to discard; selection ownership and fallback memory gain must be represented explicitly.
- `BT15_063`, `BT15_067`, `BT15_074`, and `BT15_080` depend on shared static/inherited keyword layers for Blocker, Reboot, Retaliation, and cannot-attack restrictions.

## Follow-Up

The next generated queue item is `C0022_zone_security_recovery`. C0021 remains source-reviewed but blocked until shared reveal/search, delete-success, hand digivolve, by-effect play, temporary granted effect, opponent discard, on-deletion, static keyword, and return-to-library-bottom layers are completed. After those layers exist, port each source body under the planned full-card-pool source path and add deterministic selection/replay tests before changing any C0021 identity to runnable.
