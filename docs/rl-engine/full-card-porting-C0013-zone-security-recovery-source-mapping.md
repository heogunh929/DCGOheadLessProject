# Full Card Porting C0013 Zone Security Recovery Source Mapping

## 결정

`C0013_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch의 source body 10개와 24개 asset identity는 로컬 DCGO 원본에서 확인됐지만, runnable card body로 옮기려면 `L0006_zone_security_recovery`와 reveal/search, hand/trash-to-source movement, Tamer-source cost reduction, security-skill play, inherited/security timing, suspend-cost, duration modifier, deletion target selection common layer가 먼저 필요하다.

Queue status: needs-review

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않는다. 같은 source effect를 여러 variant가 참조하더라도 `DefinitionStableId`와 `CardIndex`를 보존한다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT12_086` | `DCGO/Assets/Scripts/CardEffect/BT12/White/BT12_086.cs` | `None`: self Blocker static. `OnEnterFieldAnyone`: on-play deck top 3 reveal, up to 2 differently colored Save-text Digimon to hand, rest deck bottom any order. `OnDestroyedAnyone`: Save. `None`: inherited Jamming while owner turn and top card has Save text. | reveal/select with different-color predicate, deck-bottom ordering, `OnAddHand` timing, Save, inherited static role validation, same timing multiple descriptor identity가 필요하다. |
| `BT12_087` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_087.cs` | `OnStartMainPhase`: optional hand Save-text Digimon under this Tamer, then draw 1. `BeforePayCost`: suspend this Tamer, move 1 source from one of your Tamers under the would-digivolve Digimon, then reduce cost by 1. `SecuritySkill`: play self Tamer. | hand-to-source bottom movement, draw boundary, suspend-cost lifecycle, would-digivolve payload, Tamer source selection, source removal/add payload, temporary cost modifier, security Tamer play continuation이 필요하다. |
| `BT12_088` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_088.cs` | `SecuritySkill`: play self Tamer. `OnStartTurn`: memory setter to 3. `None`: inherited owner-turn +2000 DP. `OnSecurityCheck`: inherited once-per-turn memory +2 if this Digimon checks security and has at least 10000 DP. | security Tamer play, memory setter timing, inherited DP static, OnSecurityCheck attacker payload, once-per-turn runtime state, DP revalidation이 필요하다. |
| `BT12_091` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_091.cs` | `OnStartMainPhase`: optional hand Save-text Digimon under this Tamer, then opponent Digimon -2000 DP for turn. `BeforePayCost`: same Tamer-source cost-reduction pattern as `BT12_087`. `SecuritySkill`: play self Tamer. | hand-to-source cost/result continuation, target selection, DP duration cleanup, Tamer-source transfer, cost modifier, security play가 필요하다. |
| `BT12_093` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_093.cs` | `OnStartMainPhase`: optional hand Save-text Digimon under this Tamer, then your Digimon +2000 DP for turn. `BeforePayCost`: same Tamer-source cost-reduction pattern. `SecuritySkill`: play self Tamer. | hand-to-source movement, target selection, positive DP duration, suspend-cost/source transfer/cost reduction, security play가 필요하다. |
| `BT12_094` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_094.cs` | `OnStartMainPhase`: optional hand Save-text Digimon under this Tamer, then memory +1. `BeforePayCost`: same Tamer-source cost-reduction pattern. `SecuritySkill`: play self Tamer. | hand-to-source movement, memory boundary, suspend-cost/source transfer/cost reduction, security play가 필요하다. |
| `BT12_096` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_096.cs` | `OnStartTurn`: memory setter to 3. `BeforePayCost`: same Tamer-source cost-reduction pattern. 원본 `SetHashString`은 `DigivolutionCost-1_BT12_094`를 사용한다. `SecuritySkill`: play self Tamer. | memory setter timing, suspend-cost/source transfer/cost reduction, security play가 필요하다. SourceEffectClassName은 `BT12_096`으로 보존하고 hash string alias는 별도 metadata로 취급해야 한다. |
| `BT12_097` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_097.cs` | `OnStartMainPhase`: if this Tamer has 2 or fewer sources, optional trash Save-text Digimon under this Tamer. `BeforePayCost`: same Tamer-source cost-reduction pattern. `SecuritySkill`: play self Tamer. | trash-to-source bottom movement, source count condition, source-add payload, suspend-cost/source transfer/cost reduction, security play가 필요하다. |
| `BT12_098` | `DCGO/Assets/Scripts/CardEffect/BT12/White/BT12_098.cs` | `OnEnterFieldAnyone`: on-play deck top 3 reveal, add 1 Save-text Digimon and 1 Hunter card, rest deck bottom any order. `OnDeclaration`: main effect suspends this Tamer if you have 4+ Tamers, then gives Save-text Digimon SecurityAttack +1 for turn. `SecuritySkill`: play self Tamer. | mutual reveal selection, deck-bottom ordering, suspend-cost lifecycle, main effect action boundary, SecurityAttack duration modifier, target selection, security play가 필요하다. |
| `BT13_001` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_001.cs` | inherited `OnDestroyedAnyone`: on deletion, delete 1 opponent Digimon with 2000 DP or less, using owner `MaxDP_DeleteEffect`. | deletion snapshot, inherited source role validation, delete target selection, DP delete threshold modifier, destroy pipeline and nested trigger timing이 필요하다. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT12-086#2506@base` | `BT12_086` | `DCGO/Assets/CardBaseEntity/BT12/White/Digimon/BT12_086.asset` | `needs-review` |
| `BT12-086#4533@P0` | `BT12_086` | `DCGO/Assets/CardBaseEntity/BT12/White/Digimon/BT12_086_P0.asset` | `needs-review` |
| `BT12-087#2507@base` | `BT12_087` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_087.asset` | `needs-review` |
| `BT12-087#2508@P1` | `BT12_087` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_087_P1.asset` | `needs-review` |
| `BT12-087#4534@P0` | `BT12_087` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_087_P0.asset` | `needs-review` |
| `BT12-088#2509@base` | `BT12_088` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_088.asset` | `needs-review` |
| `BT12-088#2510@P1` | `BT12_088` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_088_P1.asset` | `needs-review` |
| `BT12-088#4535@P0` | `BT12_088` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_088_P0.asset` | `needs-review` |
| `BT12-091#2515@base` | `BT12_091` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Tamer/BT12_091.asset` | `needs-review` |
| `BT12-091#4538@P0` | `BT12_091` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Tamer/BT12_091_P0.asset` | `needs-review` |
| `BT12-093#2518@base` | `BT12_093` | `DCGO/Assets/CardBaseEntity/BT12/Green/Tamer/BT12_093.asset` | `needs-review` |
| `BT12-093#4540@P0` | `BT12_093` | `DCGO/Assets/CardBaseEntity/BT12/Green/Tamer/BT12_093_P0.asset` | `needs-review` |
| `BT12-094#2519@base` | `BT12_094` | `DCGO/Assets/CardBaseEntity/BT12/Black/Tamer/BT12_094.asset` | `needs-review` |
| `BT12-094#4541@P0` | `BT12_094` | `DCGO/Assets/CardBaseEntity/BT12/Black/Tamer/BT12_094_P0.asset` | `needs-review` |
| `BT12-096#2522@base` | `BT12_096` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Tamer/BT12_096.asset` | `needs-review` |
| `BT12-096#2523@P1` | `BT12_096` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Tamer/BT12_096_P1.asset` | `needs-review` |
| `BT12-096#4543@P0` | `BT12_096` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Tamer/BT12_096_P0.asset` | `needs-review` |
| `BT12-096#8136@P2` | `BT12_096` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Tamer/BT12_096_P2.asset` | `needs-review` |
| `BT12-097#2524@base` | `BT12_097` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Tamer/BT12_097.asset` | `needs-review` |
| `BT12-097#4544@P0` | `BT12_097` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Tamer/BT12_097_P0.asset` | `needs-review` |
| `BT12-098#2525@base` | `BT12_098` | `DCGO/Assets/CardBaseEntity/BT12/White/Tamer/BT12_098.asset` | `needs-review` |
| `BT12-098#4545@P0` | `BT12_098` | `DCGO/Assets/CardBaseEntity/BT12/White/Tamer/BT12_098_P0.asset` | `needs-review` |
| `BT13-001#2643@base` | `BT13_001` | `DCGO/Assets/CardBaseEntity/BT13/Red/DigiEgg/BT13_001.asset` | `needs-review` |
| `BT13-001#4553@P0` | `BT13_001` | `DCGO/Assets/CardBaseEntity/BT13/Red/DigiEgg/BT13_001_P0.asset` | `needs-review` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `needs-review`; C0013 repeatedly uses hand/trash-to-source bottom, Tamer source transfer, source removal, Save, and security play.
- `L0005_draw_search_reveal_hidden` remains relevant for `SimplifiedRevealDeckTopCardsAndSelect`, mutual reveal choices, different-color predicates, and deck-bottom any-order placement.
- `BT12_087`, `BT12_091`, `BT12_093`, `BT12_094`, `BT12_096`, and `BT12_097` share a `BeforePayCost` Tamer-source cost-reduction shape. Implement this as shared source-aligned digivolution-cost timing and source movement, not six CardId branches.
- `BT12_096` has a source hash string copied from `BT12_094`. Preserve `SourceEffectClassName` and registry lookup/hash alias separately so metadata does not collapse the two source effects.
- `BT12_086` and `BT12_098` require reveal/search helpers with multiple selection buckets and remaining deck-bottom ordering; direct list mutation would hide hidden-information and ordering semantics.
- `BT12_088` and `BT13_001` require inherited source snapshots and revalidation for security/deletion triggers.

## Follow-Up

The next generated queue item is `C0014_zone_security_recovery`. C0013 remains source-reviewed but blocked until shared zone/security/recovery layers are completed. When those layers exist, port each source body under the planned full-card-pool source path, keep card-specific effect body files separate, and add deterministic selection/replay tests for reveal, source movement, security play, and cost-reduction continuations before changing any C0013 identity to runnable.
