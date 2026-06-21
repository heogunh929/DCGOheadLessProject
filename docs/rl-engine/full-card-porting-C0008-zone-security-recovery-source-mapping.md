# Full Card Porting C0008 Zone Security Recovery Source Mapping

## 결정

`C0008_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch의 source body 10개는 모두 로컬 DCGO 원본에서 확인되지만, runnable card body로 옮기려면 `L0006_zone_security_recovery`와 suspend/reveal/source-add/security-check/copy-effect common layer가 먼저 필요하다.

Queue status: blocked

이번 작업에서는 카드 효과를 추측 구현하지 않는다. No CardId branch. Catalog 동작 삽입, partial no-op wrapper, source body 통합 파일을 만들지 않는다. 각 source effect와 asset variant는 원본 경로와 variant-aware identity를 유지한 blocker로 남긴다.

## Batch Source Effects

| SourceEffectClassName | Source path | Source 의미 | Blocking reason |
| --- | --- | --- | --- |
| `BT10_044` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_044.cs` | Owner turn once-per-turn draw when owner plays a green Tamer; inherited once-per-turn draw when an opponent Digimon becomes suspended. | Requires source-aligned `OnEnterFieldAnyone`, `OnTappedAnyone`, once-per-turn hash, suspend event payload, inherited source role, and `DrawClass`/`OnDraw` ordering. |
| `BT10_046` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_046.cs` | On play reveal top 4, choose one Plant/Vegetation and one Fairy with mutual-condition handling, add chosen cards to hand, bottom the rest in any order. | Needs reveal-zone modeling, two-condition selection, mutual duplicate handling, bottom ordering, `OnAddHand`, and hidden/revealed information trace support before runnable porting. |
| `BT10_048` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_048.cs` | When digivolving, optional select/suspend one own green Digimon, then optionally play a small Plant/Fairy Digimon from hand without cost; inherited draw when an effect suspends owner Digimon. | Requires staged optional selection, suspend-cost lifecycle, by-effect `OnTappedAnyone` payload, hand play without cost, ETB interleaving, and `OnAddHand`/zone movement layers. |
| `BT10_053` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_053.cs` | Main once-per-turn declaration: select/suspend one own green Digimon, play a small Plant/Fairy Digimon from hand without cost; inherited memory +1 when an effect suspends owner Digimon. | Needs declaration-from-field activation, suspend-cost lifecycle, hand play without cost, memory timing, once-per-turn state, and by-effect suspend payload. |
| `BT10_057` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_057.cs` | When digivolving, may suspend one own Digimon, gain memory for suspended Plant/Fairy Digimon, then if 2+ memory gained unsuspend self and gain Piercing for the turn; static DP and SecurityAttack scale with suspended owner Digimon. | Needs optional target selection, suspend/unsuspend event ordering, memory add, Piercing duration, dynamic DP/SecurityAttack continuous queries, and cleanup timing. |
| `BT10_071` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_071.cs` | Inherited Retaliation while owner trash has 10 or more cards. | Needs inherited keyword source role and dynamic trash-count condition wired into battle keyword lifecycle. |
| `BT10_076` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_076.cs` | Opponent turn once-per-turn optional effect when opponent plays Digimon/Tamer: select and trash one own digivolution card to gain memory; Save on deletion; inherited memory +1 when this source is trashed by effect. | Requires digivolution-source selection/trash, `OnDigivolutionCardDiscarded`, source role transition to trash, Save, memory timing, once-per-turn, and optional continuation support. |
| `BT10_080` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_080.cs` | When owner effect trashes this hand card, if owner turn, optionally digivolve one own Digimon into a purple Undead/Dark Animal from trash for cost; when digivolving from trash, grant this Digimon a temporary OnDeletion delete-opponent-Digimon effect. | Needs `OnDiscardHand` source payload, hand-to-trash self trigger, trash digivolution with cost, temporary granted effect storage, `OnDestroyedAnyone`, target destruction, and duration cleanup. |
| `BT10_112` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_112.cs` | Static Royal Knight digivolution requirement; when digivolving, optionally choose hand/trash Royal Knight source, add it as bottom digivolution card, choose one of that card's when-digivolving effects to activate as this Digimon's effect, then Blitz; gains Piercing, Blocker, and SecurityAttack based on Royal Knight sources. | Requires hand/trash boolean selection, source insertion, `OnAddDigivolutionCards`, copied/cross-source when-digivolving effect execution, optional ordering, Blitz handoff, `OnDetermineDoSecurityCheck`, and dynamic keyword/SecurityAttack layers. |
| `BT11_001` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_001.cs` | Inherited OnDeletion draw 1 if owner has a red Tamer. | Needs inherited on-deletion source snapshot, tamer-color query, and `DrawClass`/`OnDraw` ordering. |

## Variant-aware Identity

| DefinitionStableId | SourceEffectClassName | Asset path | Decision |
| --- | --- | --- | --- |
| `BT10-044#2084@base` | `BT10_044` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_044.asset` | `blocked` |
| `BT10-044#2085@P1` | `BT10_044` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_044_P1.asset` | `blocked` |
| `BT10-044#4317@P0` | `BT10_044` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_044_P0.asset` | `blocked` |
| `BT10-046#2087@base` | `BT10_046` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_046.asset` | `blocked` |
| `BT10-046#4318@P0` | `BT10_046` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_046_P0.asset` | `blocked` |
| `BT10-046#4319@P1` | `BT10_046` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_046_P1.asset` | `blocked` |
| `BT10-046#8098@P2` | `BT10_046` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_046_P2.asset` | `blocked` |
| `BT10-048#2089@base` | `BT10_048` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_048.asset` | `blocked` |
| `BT10-048#4320@P1` | `BT10_048` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_048_P1.asset` | `blocked` |
| `BT10-048#4321@P2` | `BT10_048` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_048_P2.asset` | `blocked` |
| `BT10-053#2094@base` | `BT10_053` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_053.asset` | `blocked` |
| `BT10-053#4324@P0` | `BT10_053` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_053_P0.asset` | `blocked` |
| `BT10-057#2099@base` | `BT10_057` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_057.asset` | `blocked` |
| `BT10-057#2100@P1` | `BT10_057` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_057_P1.asset` | `blocked` |
| `BT10-057#8099@P2` | `BT10_057` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_057_P2.asset` | `blocked` |
| `BT10-071#2117@base` | `BT10_071` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_071.asset` | `blocked` |
| `BT10-076#2122@base` | `BT10_076` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_076.asset` | `blocked` |
| `BT10-080#2126@base` | `BT10_080` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_080.asset` | `blocked` |
| `BT10-080#4338@P0` | `BT10_080` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_080_P0.asset` | `blocked` |
| `BT10-112#2169@base` | `BT10_112` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_112.asset` | `blocked` |
| `BT10-112#2170@P1` | `BT10_112` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_112_P1.asset` | `blocked` |
| `BT10-112#8104@base` | `BT10_112` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_112_P2_J.asset` | `blocked` |
| `BT11-001#2268@base` | `BT11_001` | `DCGO/Assets/CardBaseEntity/BT11/Red/DigiEgg/BT11_001.asset` | `blocked` |

## Common-layer Blockers

- `L0006_zone_security_recovery` is still `blocked`; C0008 uses hand/trash/digivolution-source moves that cannot be flattened to primitive zone moves.
- `OnTappedAnyone` must preserve actual suspended permanent, by-effect state, duplicate-effect filtering, and post-cut-in semantics before BT10 green inherited effects are runnable.
- Reveal/search needs a source-aligned revealed-card zone, multi-condition selection, bottom-deck ordering, and hidden information trace.
- `OnDiscardHand` must aggregate discarded cards and source `CardEffect`; BT10_080 must not be implemented as a generic hand-discard shortcut.
- `OnDigivolutionCardDiscarded` and add-digivolution-card bottom must preserve source role and current permanent identity.
- `BT10_112` needs copied source effect execution with selected Royal Knight source identity. Do not call another source's effect through a CardId shortcut.
- Keyword/continuous layers for Retaliation, Piercing, Blocker, Blitz, SecurityAttack, and dynamic DP must remain generic and source-aware.
- Duplicate CardId variants, including `BT10-112#2169@base` and `BT10-112#8104@base`, must remain separate by `DefinitionStableId` and `CardIndex`.

## Follow-up

The next generated queue item is `C0009_zone_security_recovery`. C0008 remains a source-reviewed blocker set until the common layers above are implemented and verified. Once those layers exist, port each source body under the planned full-card-pool source path and add targeted selection/replay tests before changing any C0008 identity to runnable.
