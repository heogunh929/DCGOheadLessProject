# Full Card Porting C0014 Zone Security Recovery Source Mapping

## 결정

`C0014_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch의 source body 10개와 21개 asset identity는 로컬 DCGO 원본에서 확인됐지만, runnable card body로 옮기려면 `L0006_zone_security_recovery`와 breeding-area source role, Royal Knight play-cost modifier, hand play with cost reduction, source attach ordering, security trash, OnLoseSecurity hand play, bounce, source trash, keyword grant duration, deletion target selection common layer가 먼저 필요하다.

Queue status: needs-review

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않는다. 같은 source effect를 여러 variant가 참조하더라도 `DefinitionStableId`, `CardIndex`, `variantKey`를 보존한다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT13_003` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_003.cs` | inherited `OnLoseSecurity`: owner turn once per turn, when owner security is removed, select 1 owner Digimon and grant Jamming for the turn. | OnLoseSecurity payload, inherited source snapshot, once-per-turn state, keyword duration grant, target selection pause/resume가 필요하다. |
| `BT13_006` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_006.cs` | inherited `OnDestroyedAnyone`: on deletion, optionally discard 1 hand card, then delete 1 opponent level 3 Digimon. | deletion snapshot, optional hand discard cost, discard timing, destroy target selection, inherited source revalidation이 필요하다. |
| `BT13_007` | `DCGO/Assets/Scripts/CardEffect/BT13/White/BT13_007.cs` | `None`: in breeding during owner turn, owner battle-area Digimon cannot digivolve through `CanNotDigivolveStaticEffect`. `BeforePayCost`: in breeding, once per turn, Royal Knight Digimon play cost reduction by 4 plus source count. `None`: hidden UI cost preview for same play reduction. `OnStartMainPhase`: reveal top Digi-Egg deck and place it plus all owner battle-area Royal Knight Digimon under this breeding Digimon, with order selection when multiple cards. `OnEnterFieldAnyone`: inherited once-per-turn memory +1 when Royal Knight Option is placed in battle area. | breeding-area source role, cannot-digivolve static restriction, would-play payload, temporary cost modifier preview vs real reduction, Digi-Egg reveal, moving field top cards to source bottom, order selection, option play trigger payload가 필요하다. |
| `BT13_011` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_011.cs` | `OnEnterFieldAnyone`: separate On Play and When Digivolving descriptors both delete 1 opponent Digimon with 3000 DP or less. inherited `OnDestroyedAnyone`: draw 1 on deletion. | same timing multi-descriptor identity, OnPlay vs WhenDigivolving payload split, delete target selection, DP delete threshold modifiers, draw boundary가 필요하다. |
| `BT13_014` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_014.cs` | `OnEnterFieldAnyone`: separate On Play and When Digivolving optional play 1 red Tamer cost 3 or less from hand for free. inherited `OnDestroyedAnyone`: delete 1 opponent Digimon with 6000 DP or less. | hand play with ETB interleaving, optional selection, same timing descriptor ordering, delete target selection, inherited deletion snapshot가 필요하다. |
| `BT13_030` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_030.cs` | `OnEnterFieldAnyone`: separate On Play and When Digivolving descriptors trash top 2 source cards from 1 opponent Digimon for each owner Royal Knight Digimon and blue Tamer. Also owner-turn once-per-turn when owner plays Royal Knight Digimon or blue Tamer, bounce 1 opponent Digimon with no sources. | dynamic source-trash count, source trash from top, CanNotTrashFromDigivolutionCards restriction, same timing descriptor ordering, OnPermanentPlay payload, bounce-to-hand replacement/timing이 필요하다. |
| `BT13_044` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_044.cs` | `None`: self Blocker static. `OnEnterFieldAnyone`: When Digivolving, optionally trash top owner security, then opponent Digimon gets -6000 DP until opponent turn end. `OnLoseSecurity`: all turns once per turn, optionally play 1 yellow Tamer from hand for free. | Blocker static, `IDestroySecurity` top-security trash with OnLoseSecurity timing, target DP duration UntilOpponentTurnEnd, hand play from OnLoseSecurity, security movement payload가 필요하다. |
| `BT13_048` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_048.cs` | `OnEnterFieldAnyone`: on play, `SimplifiedRevealDeckTopCardsAndSelect` reveals top 3 deck, add 1 Beast/Animal/Sovereign non-Sea-Animal Digimon and 1 Royal Knight Digimon, rest deck bottom any order. `None`: inherited owner-turn +2000 DP if top has Beast or Royal Knight trait. | reveal/search multi-bucket helper, trait normalization including non-Sea-Animal exclusion, deck-bottom ordering, OnAddHand timing, inherited DP static가 필요하다. |
| `BT13_051` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_051.cs` | `OnEnterFieldAnyone`: on play, select 1 owner Digimon and grant Piercing for the turn. `None`: inherited owner-turn +2000 DP if top has Beast or Royal Knight trait. | keyword duration grant, target selection, inherited DP static, source role validation가 필요하다. |
| `BT13_056` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_056.cs` | `OnEnterFieldAnyone`: When Digivolving once per turn, optionally play 1 green or Royal Knight Digimon from hand for cost with play cost -4 during that effect. `OnDeclaration`: main once per turn with the same hand-play and cost reduction. `OnEnterFieldAnyone`: all turns once per turn when owner plays another Digimon, grant all owner green/Royal Knight Digimon Blocker until opponent turn end. | temporary play-cost modifier scoped to a hand-play effect, main action boundary, optional hand play with cost payment, ETB interleaving, granted Blocker duration, OnPermanentPlay filtering가 필요하다. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT13-003#2645@base` | `BT13_003` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/DigiEgg/BT13_003.asset` | `needs-review` |
| `BT13-003#4555@P0` | `BT13_003` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/DigiEgg/BT13_003_P0.asset` | `needs-review` |
| `BT13-006#2648@base` | `BT13_006` | `DCGO/Assets/CardBaseEntity/BT13/Purple/DigiEgg/BT13_006.asset` | `needs-review` |
| `BT13-006#4558@P0` | `BT13_006` | `DCGO/Assets/CardBaseEntity/BT13/Purple/DigiEgg/BT13_006_P0.asset` | `needs-review` |
| `BT13-007#2649@base` | `BT13_007` | `DCGO/Assets/CardBaseEntity/BT13/White/DigiEgg/BT13_007.asset` | `needs-review` |
| `BT13-007#2650@P1` | `BT13_007` | `DCGO/Assets/CardBaseEntity/BT13/White/DigiEgg/BT13_007_P1.asset` | `needs-review` |
| `BT13-007#8139@P2` | `BT13_007` | `DCGO/Assets/CardBaseEntity/BT13/White/DigiEgg/BT13_007_P2.asset` | `needs-review` |
| `BT13-011#2654@base` | `BT13_011` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_011.asset` | `needs-review` |
| `BT13-014#2657@base` | `BT13_014` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_014.asset` | `needs-review` |
| `BT13-014#4562@P0` | `BT13_014` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_014_P0.asset` | `needs-review` |
| `BT13-030#2677@base` | `BT13_030` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_030.asset` | `needs-review` |
| `BT13-030#2678@P1` | `BT13_030` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_030_P1.asset` | `needs-review` |
| `BT13-030#4575@P0` | `BT13_030` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_030_P0.asset` | `needs-review` |
| `BT13-044#2695@base` | `BT13_044` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_044.asset` | `needs-review` |
| `BT13-044#4586@P0` | `BT13_044` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_044_P0.asset` | `needs-review` |
| `BT13-048#2700@base` | `BT13_048` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_048.asset` | `needs-review` |
| `BT13-048#4588@P0` | `BT13_048` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_048_P0.asset` | `needs-review` |
| `BT13-051#2703@base` | `BT13_051` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_051.asset` | `needs-review` |
| `BT13-056#2708@base` | `BT13_056` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_056.asset` | `needs-review` |
| `BT13-056#2709@P1` | `BT13_056` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_056_P1.asset` | `needs-review` |
| `BT13-056#4592@P0` | `BT13_056` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_056_P0.asset` | `needs-review` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `needs-review`; C0014 uses security trash, hand play, source attachment, source trash, bounce-to-hand, and inherited source-trigger movement.
- `BT13_007` requires breeding-area source role support and source attachment ordering from Digi-Egg deck and battle-area top cards. This cannot be modeled with direct zone list mutation.
- `BT13_011`, `BT13_014`, and `BT13_030` each create multiple descriptors under the same `OnEnterFieldAnyone` timing. Descriptor identity and source payload must stay separate for ordering, replay, and source revalidation.
- `BT13_044` uses `IDestroySecurity` before a DP target selection. The security removal must produce source-aligned OnLoseSecurity payload and preserve continuation state.
- `BT13_056` temporarily installs a play-cost reduction only around the chosen hand-play effect. The scope must be rollback/replay-safe and cannot leak into unrelated plays.
- `BT13_048` depends on trait-normalized reveal/search with remaining deck-bottom any-order handling.

## Follow-Up

The next generated queue item is `C0015_zone_security_recovery`. C0014 remains source-reviewed but blocked until shared zone/security/recovery, breeding-area source, play-cost, reveal/search, source-trash, security-trash, bounce, and duration keyword layers are completed. When those layers exist, port each source body under the planned full-card-pool source path and add deterministic selection/replay tests for each continuation before changing any C0014 identity to runnable.
