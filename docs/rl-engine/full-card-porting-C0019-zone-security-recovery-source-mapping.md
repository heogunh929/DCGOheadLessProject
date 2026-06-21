# Full Card Porting C0019 Zone Security Recovery Source Mapping

## 결정

`C0019_zone_security_recovery`는 `done`으로 승격할 수 없는 카드 포팅 batch다. DCGO 원본 source body 10개와 asset identity 17개는 모두 확인했지만, runnable card body로 옮기려면 `L0006_zone_security_recovery`와 함께 declaration cost, reveal/search, trash-to-hand, security-loss payload, digivolution-source trash, cannot-attack duration, free hand/breeding play, delete-success continuation, and static/inherited keyword layers가 먼저 source-aligned로 정리되어야 한다.

Queue status: needs-review

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않는다. 같은 source effect를 여러 variant가 참조하더라도 `DefinitionStableId`, `CardIndex`, `variantKey`를 보존한다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT15_009` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_009.cs` | `OnDeclaration`: `[Main][Once per turn]` paying 2 memory through `card.Owner.AddMemory(-2, activateClass)`, then delete 1 opponent Digimon whose DP is less than or equal to this Digimon's current DP. Hash is `Delete_BT15_009`; source marks it with `SetIsDigimonEffect(true)`. | declaration/manual activated effect boundary, memory cost payment and rollback, post-cost target legality, current DP comparison, once-per-turn hash, and target selection continuation are required. |
| `BT15_011` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_011.cs` | `None`: `AddSelfDigivolutionRequirementStaticEffect` lets level 3 SoC Digimon digivolve for cost 2, and `BlockerSelfStaticEffect` grants Blocker. `OnEnterFieldAnyone`: On Play reveal top 4 through `SimplifiedRevealDeckTopCardsAndSelect`, add 1 SoC Digimon and 1 SoC Tamer, bottom-deck the rest, then if any revealed card matched either condition, discard 1 hand card with `SelectHandEffect.Mode.Discard`. | alternate digivolution requirement, static Blocker, reveal dual-pick, bottom-deck ordering, source quirk where `added` is based on revealed candidates, mandatory hand discard, and reveal/discard trigger interleaving are required. |
| `BT15_013` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_013.cs` | `OnEnterFieldAnyone`: When Digivolving, select 1 red Digimon in owner trash with Avian/Bird/Beast/Animal/Sovereign traits and add it to hand through `SelectCardEffect.Root.Trash`. inherited `OnLoseSecurity`: owner turn once per turn, when opponent security is removed, gain 1 memory with hash `Memory+1_BT15_013`. | trash-to-hand movement, trait helper parity, inherited source snapshot/revalidation, `OnLoseSecurity` payload, once-per-turn hash, and memory gain trigger interleaving are required. |
| `BT15_016` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_016.cs` | On Play and When Digivolving descriptors share a branch: if opponent has 4 or less memory, select opponent Digimon with 8000 DP or more and apply `GainCanNotAttack` until opponent turn end; if opponent has 4 or more memory, delete opponent Digimon with DP up to `MaxDP_DeleteEffect(6000, activateClass)`. inherited `OnDestroyedAnyone`: on deletion, bounce 1 opponent Digimon with 7000 DP or less. | duplicated descriptor identity, memory-threshold branch, cannot-attack duration, delete DP boost calculation, inherited deletion source validation, and bounce-to-hand event ordering are required. |
| `BT15_017` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_017.cs` | On Play and On Deletion descriptors delete an opponent Digimon with minimum DP when opponent security is 3 or fewer, or trash top opponent security through `IDestroySecurity(fromTop: true)` when security is 4 or more. When Digivolving is optional and may play 1 red Digimon with 5000 DP or less or 1 red Tamer from hand for free via `PlayPermanentCards`. | minimum-DP target selection, top-security trash semantics, security-loss trigger payload, optional hand selection, free hand play with ETB, and separate On Play/On Deletion/When Digivolving descriptor identity are required. |
| `BT15_018` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_018.cs` | `OnEndTurn`: owner end turn once per turn with hash `DeleteLowestDP_BT15_018`, if opponent has 4 or more memory delete opponent Digimon with minimum DP. Opponent end turn once per turn with hash `DeleteHighestCost_BT15_018`, if owner has 4 or less memory delete opponent Digimon with maximum play cost. | end-turn owner/opponent filtering, separate once-per-turn hashes, min-DP and max-cost target tie semantics, and target selection at turn transition are required. |
| `BT15_019` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_019.cs` | `OnEnterFieldAnyone`: On Play select 1 opponent Digimon, trash the bottom digivolution card through `TrashDigivolutionCardsFromTopOrBottom(... isFromTop: false)`, then if opponent has no Digimon with digivolution cards, draw 1 with `DrawClass`. | bottom digivolution-source trash, source-trash event payload, post-trash board query, draw boundary, and OnAddHand/OnDraw interleaving are required. |
| `BT15_022` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_022.cs` | `OnEnterFieldAnyone`: On Play, only if played by effect via `IsByEffect`, select 1 opponent Digimon and give cannot attack until opponent turn end. `None`: inherited `JammingSelfStaticEffect`. | by-effect play payload, cannot-attack duration, inherited Jamming static layer, and selection-aware duration modifier tracking are required. |
| `BT15_023` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_023.cs` | `OnEnterFieldAnyone`: On Play select 1 opponent Digimon, trash bottom 2 digivolution cards through `TrashDigivolutionCardsFromTopOrBottom(... trashCount: 2, isFromTop: false)`, then if opponent has no Digimon with digivolution cards, gain 1 memory. | multi-card bottom source trash, source-trash event payload, post-trash board query, memory gain, and trigger interleaving are required. |
| `BT15_027` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_027.cs` | `OnEnterFieldAnyone`: reveal top 4 through `SimplifiedRevealDeckTopCardsAndSelect`, add up to 2 level 6+ cards, bottom-deck rest. `OnEndTurn`: optional owner end turn effect; select and delete 1 own Digimon, then on successful deletion may play 1 Dark Masters Digimon from hand to an empty breeding area for free with ETB. `None`: inherited `BlockerSelfStaticEffect`. | reveal multi-pick, bottom-deck ordering, optional delete-cost, delete-success continuation, breeding-area free play legality, inherited Blocker, and effect-play ETB timing are required. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT15-009#3129@base` | `BT15_009` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_009.asset` | `needs-review` |
| `BT15-011#3131@base` | `BT15_011` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_011.asset` | `needs-review` |
| `BT15-013#3133@base` | `BT15_013` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_013.asset` | `needs-review` |
| `BT15-013#4715@P0` | `BT15_013` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_013_P0.asset` | `needs-review` |
| `BT15-016#3138@base` | `BT15_016` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_016.asset` | `needs-review` |
| `BT15-016#4716@P0` | `BT15_016` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_016_P0.asset` | `needs-review` |
| `BT15-017#3139@base` | `BT15_017` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_017.asset` | `needs-review` |
| `BT15-017#4717@P0` | `BT15_017` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_017_P0.asset` | `needs-review` |
| `BT15-017#4718@P1` | `BT15_017` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_017_P1.asset` | `needs-review` |
| `BT15-017#4719@P2` | `BT15_017` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_017_P2.asset` | `needs-review` |
| `BT15-017#4720@P3` | `BT15_017` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_017_P3.asset` | `needs-review` |
| `BT15-018#3140@base` | `BT15_018` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_018.asset` | `needs-review` |
| `BT15-018#4721@P0` | `BT15_018` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_018_P0.asset` | `needs-review` |
| `BT15-019#3141@base` | `BT15_019` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_019.asset` | `needs-review` |
| `BT15-022#3145@base` | `BT15_022` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_022.asset` | `needs-review` |
| `BT15-023#3146@base` | `BT15_023` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_023.asset` | `needs-review` |
| `BT15-027#3152@base` | `BT15_027` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_027.asset` | `needs-review` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `needs-review`; C0019 depends on hand/trash/security/source movements and their trigger payloads.
- `BT15_009` requires a declaration/manual activated effect boundary with memory cost payment and rollback before target selection.
- `BT15_011` and `BT15_027` use reveal/search helpers whose selected cards, remaining bottom-deck order, and follow-up hand discard/play continuations must be modeled as deterministic, selection-aware stages.
- `BT15_013`, `BT15_016`, `BT15_017`, and `BT15_018` need inherited/deletion/end-turn source snapshots and once-per-turn hash handling to survive ordering, rollback, and replay.
- `BT15_017` and `BT15_013` depend on security-loss semantics; top-security trash and `OnLoseSecurity` must stay in shared security flow.
- `BT15_019` and `BT15_023` require bottom digivolution-source trash with source-trash payload before their draw/memory follow-up is safe.
- `BT15_016`, `BT15_022`, `BT15_011`, and `BT15_027` depend on shared duration/static keyword layers for cannot-attack, Blocker, Jamming, and alternate digivolution requirements.
- `BT15_027` requires delete-success continuation into free breeding-area play; this must not be approximated as unconditional hand play.

## Follow-Up

The next generated queue item is `C0020_zone_security_recovery`. C0019 remains source-reviewed but blocked until shared declaration-cost, reveal/search, security-loss, source-trash, duration/static keyword, free play, and delete-success continuation layers are completed. After those layers exist, port each source body under the planned full-card-pool source path and add deterministic selection/replay tests before changing any C0019 identity to runnable.
