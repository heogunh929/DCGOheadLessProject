# Full Card Porting C0009 Zone Security Recovery Source Mapping

## 결정

`C0009_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch의 source body 10개는 모두 로컬 DCGO 원본에서 확인되지만, runnable card body로 옮기려면 `L0006_zone_security_recovery`와 reveal/search, on-deletion, on-security-loss, on-add-hand, bounce/unsuspend common layer가 먼저 필요하다.

Queue status: blocked

이번 작업에서는 카드 효과를 추측 구현하지 않는다. No CardId branch. Catalog 동작 삽입, partial no-op wrapper, source body 통합 파일을 만들지 않는다. 각 source effect와 asset variant는 원본 경로와 variant-aware identity를 유지한 blocker로 남긴다.

## Batch Source Effects

| SourceEffectClassName | Source path | Source 의미 | Blocking reason |
| --- | --- | --- | --- |
| `BT11_003` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_003.cs` | Inherited owner-turn once-per-turn draw when owner plays Angel, Archangel, or Fallen Angel Digimon. | Requires source-aligned `OnEnterFieldAnyone`, inherited source role, once-per-turn hash, trait matching, and `DrawClass`/`OnDraw` ordering. |
| `BT11_004` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_004.cs` | Inherited owner-turn once-per-turn draw when owner plays a green Tamer. | Requires play-event payload, inherited source validation, once-per-turn state, and draw trigger boundary. |
| `BT11_005` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_005.cs` | Inherited opponent-turn once-per-turn draw when an opponent Digimon is deleted and this Digimon has Greymon in name. | Requires on-deletion event payload, source role/top-card name revalidation, turn ownership, and draw ordering. |
| `BT11_007` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_007.cs` | On play reveal top 3, choose one red Vaccine Digimon and one red Tamer, add chosen cards to hand and bottom the rest; inherited OnDeletion memory +1 if owner has red Tamer. | Needs reveal-zone modeling, two-condition selection, bottom-deck ordering, `OnAddHand`, inherited deletion payload, and memory timing. |
| `BT11_011` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_011.cs` | Static Blocker; inherited optional OnDeletion play one red Tamer cost 4 or less from hand without cost. This source is also used by `BT11-013#2280@base`. | Requires generic Blocker lifecycle, inherited OnDeletion snapshot, hand selection, play-without-cost, ETB interleaving, and shared source metadata preservation. |
| `BT11_016` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_016.cs` | Owner-turn once-per-turn `OnLoseSecurity`: optionally choose and activate one of this Digimon's OnDeletion effects; OnDeletion optional hand play of small red Avian/Bird/Beast/Animal/Sovereign Digimon with red-Tamer DP scaling. | Needs `OnLoseSecurity` candidate snapshot, copied/self OnDeletion activation with altered hashtable, effect ordering selection, hand play, tamer-count dynamic condition, and on-deletion payload. |
| `BT11_024` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_024.cs` | On play optional hand selection: place one blue level 3 or Aqua/Sea Animal Digimon under this Digimon as bottom digivolution card, then draw 1. | Requires hand-to-source movement, `OnAddDigivolutionCards`, source role update, staged optional selection, and draw ordering. |
| `BT11_027` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_027.cs` | Owner-turn once-per-turn draw when owner plays blue Tamer; inherited owner-turn once-per-turn memory +1 for same event. | Requires multiple descriptor ordering from the same timing/source family, inherited source role, once-per-turn hash separation, draw and memory timing. |
| `BT11_028` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_028.cs` | When digivolving, gain Blocker and DP until opponent turn end based on opponent hand size; inherited `OnAddHand` unsuspend self when an effect adds cards to opponent hand. | Requires duration Blocker/DP, opponent hand count snapshot policy, `OnAddHand` source payload, by-effect hand-add check, unsuspend lifecycle, and inherited source validation. |
| `BT11_032` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_032.cs` | When digivolving, optional play one blue Tamer from hand without cost; owner-turn blue Tamer play unsuspends self; once-per-turn `OnUnTappedAnyone` bounces one opponent Digimon with level cap increased by blue Tamers. | Needs optional hand play, ETB interleaving, unsuspend event payload, `OnUnTappedAnyone`, post-cut-in bounce selection, `WhenReturntoHandAnyone`/`OnPermamemtReturnedToHand`, and target refix. |

## Shared Source Note

`BT11-013#2280@base` has no `BT11_013.cs` source body. Its asset explicitly sets `CardEffectClassName: BT11_011`, so it must stay linked to `BT11_011@DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_011.cs`. This is a shared source-effect mapping, not a missing source body and not a NoEffect card.

## Variant-aware Identity

| DefinitionStableId | SourceEffectClassName | Asset path | Decision |
| --- | --- | --- | --- |
| `BT11-003#2270@base` | `BT11_003` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/DigiEgg/BT11_003.asset` | `blocked` |
| `BT11-003#4368@P0` | `BT11_003` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/DigiEgg/BT11_003_P0.asset` | `blocked` |
| `BT11-004#2271@base` | `BT11_004` | `DCGO/Assets/CardBaseEntity/BT11/Green/DigiEgg/BT11_004.asset` | `blocked` |
| `BT11-005#2272@base` | `BT11_005` | `DCGO/Assets/CardBaseEntity/BT11/Black/DigiEgg/BT11_005.asset` | `blocked` |
| `BT11-005#4369@P0` | `BT11_005` | `DCGO/Assets/CardBaseEntity/BT11/Black/DigiEgg/BT11_005_P0.asset` | `blocked` |
| `BT11-005#4370@P1` | `BT11_005` | `DCGO/Assets/CardBaseEntity/BT11/Black/DigiEgg/BT11_005_P1.asset` | `blocked` |
| `BT11-007#2274@base` | `BT11_007` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_007.asset` | `blocked` |
| `BT11-007#4371@P0` | `BT11_007` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_007_P0.asset` | `blocked` |
| `BT11-007#8105@P1` | `BT11_007` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_007_P1.asset` | `blocked` |
| `BT11-011#2278@base` | `BT11_011` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_011.asset` | `blocked` |
| `BT11-011#4372@P0` | `BT11_011` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_011_P0.asset` | `blocked` |
| `BT11-013#2280@base` | `BT11_011` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_013.asset` | `blocked` |
| `BT11-016#2283@base` | `BT11_016` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_016.asset` | `blocked` |
| `BT11-016#2284@P1` | `BT11_016` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_016_P1.asset` | `blocked` |
| `BT11-024#2294@base` | `BT11_024` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_024.asset` | `blocked` |
| `BT11-027#2297@base` | `BT11_027` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_027.asset` | `blocked` |
| `BT11-027#4389@P0` | `BT11_027` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_027_P0.asset` | `blocked` |
| `BT11-027#4390@P1` | `BT11_027` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_027_P1.asset` | `blocked` |
| `BT11-028#2298@base` | `BT11_028` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_028.asset` | `blocked` |
| `BT11-028#4391@P0` | `BT11_028` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_028_P0.asset` | `blocked` |
| `BT11-032#2302@base` | `BT11_032` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_032.asset` | `blocked` |
| `BT11-032#2303@P1` | `BT11_032` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_032_P1.asset` | `blocked` |
| `BT11-032#8107@P2` | `BT11_032` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_032_P2.asset` | `blocked` |

## Common-layer Blockers

- `L0006_zone_security_recovery` remains `blocked`; C0009 uses `OnAddHand`, hand-to-source movement, hand play, bounce-to-hand, and security-loss timing.
- Reveal/search effects need source-aligned revealed-card visibility, two-condition selection, mutual selection handling, and bottom-deck ordering.
- `OnLoseSecurity` must preserve the original security-loss candidate snapshot and copied OnDeletion effect execution context before `BT11_016` can be ported.
- `OnUnTappedAnyone` and bounce must preserve unsuspend event payload, `WhenReturntoHandAnyone`, `OnPermamemtReturnedToHand`, and post-cut-in target refix.
- `BT11_027` has multiple effects at the same timing family; ordering/once-per-turn hash separation must be descriptor-aware, not CardId-level.
- `BT11_013` must preserve `SourceEffectClassName = BT11_011` metadata; do not flatten it to `BT11_013`, NoEffect, or a CardId branch.

## Follow-up

The next generated queue item is `C0010_zone_security_recovery`. C0009 remains a source-reviewed blocker set until the common layers above are implemented and verified. Once those layers exist, port each source body under the planned full-card-pool source path and add targeted selection/replay tests before changing any C0009 identity to runnable.
