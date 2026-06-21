# Full Card Porting C0012 Zone Security Recovery Source Mapping

## 결정

`C0012_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch의 source body 10개와 17개 asset identity는 모두 로컬 DCGO 원본에서 확인되지만, runnable card body로 옮기려면 `L0006_zone_security_recovery`와 draw/reveal/search, hand discard, play-from-source, permanent-to-source, source-add/source-trash, De-Digivolve, Save, alternate digivolution requirement, inherited Blocker, and descriptor-aware same-timing ordering common layer가 먼저 필요하다.

Queue status: blocked

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않았다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT12_004` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_004.cs` | inherited owner-turn once-per-turn: owner가 green Digimon을 play하면 이 Digimon DP +2000 for turn. | inherited source validation, `OnEnterFieldAnyone` play payload, once-per-turn key, duration DP cleanup이 필요하다. |
| `BT12_005` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_005.cs` | inherited owner-turn once-per-turn: owner가 `<Save>` text Digimon을 play하면 draw 1. | play payload with text predicate, draw boundary, `OnAddHand`/`OnDraw` sequencing, inherited once-per-turn state가 필요하다. |
| `BT12_006` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_006.cs` | inherited `OnDeletion`: 이 Digimon이 `<Save>` text를 가지고 있고 deck이 있으면 draw 1. | deletion snapshot, `CanActivateSelefOnDeletionWithSaveText`, draw timing, inherited source role validation이 필요하다. |
| `BT12_009` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_009.cs` | `OnPlay`: optional로 hand의 Hybrid Digimon 1장을 trash하면 draw 2. `None`: inherited owner-turn self DP +2000 if top is Hybrid or Ten Warriors. | optional hand discard cost/result, draw timing, Hybrid/Ten Warriors trait normalization, inherited continuous DP가 필요하다. |
| `BT12_026` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_026.cs` | `WhenDigivolving`: optional로 hand의 blue level 5 이하 Digimon 1장을 self source bottom에 넣으면 상대 Digimon 최대 2장의 bottom source 2장씩 trash. `OnDigivolutionCardDiscarded`: 상대 Digimon source가 trash되면 memory +1 once per turn. | hand-to-source bottom movement, `OnAddDigivolutionCards`, target multi-select, bottom-source trash, `OnDigivolutionCardDiscarded` payload, cost/result continuation이 필요하다. |
| `BT12_027` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_027.cs` | `OnPlay`와 `WhenDigivolving`: optional로 자신의 다른 blue Digimon을 이 Digimon의 source bottom에 넣고, 실제 top card가 source에 들어갔으면 memory +2. | permanent-to-source bottom movement, selected top-card verification, source-add payload, optional selection, memory after successful movement가 필요하다. |
| `BT12_032` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_032.cs` | `WhenDigivolving`: optional로 자신의 blue Digimon source 중 Hybrid/Aqua/Sea Animal Digimon 1장을 cost 없이 play. `OnDeletion`: optional로 hand의 blue level 4 이하 Hybrid Digimon 1장을 cost 없이 play. | play-from-digivolution-cards, play-from-hand on deletion, ETB interleaving, optional two-step selection, deletion snapshot, source removal events가 필요하다. |
| `BT12_045` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_045.cs` | `OnPlay`: deck top 1 reveal, green Digimon이면 hand에 추가하고 아니면 deck bottom. | reveal-and-process-all helper, hidden information, hand-add timing, deck bottom movement가 필요하다. |
| `BT12_064` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_064.cs` | `None`: level 3 Save-text target에 self digivolution requirement를 추가. `WhenDigivolving`: 상대 level 5 이하 plus own source count scaling target에 De-Digivolve 1. `OnDeletion`: Save 후 trash의 Save-text Digimon 1장을 Tamer source bottom에 둔다. `None`: opponent turn, top has Save text이면 inherited Blocker. | alternate digivolution requirement, De-Digivolve, Save process, trash-to-Tamer-source movement, inherited Blocker, same source multiple `None` descriptor handling이 필요하다. |
| `BT12_080` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_080.cs` | `OnPlay`와 `WhenDigivolving`: deck top 3 reveal, purple/black Digimon 1장을 hand, 나머지는 deck top 또는 bottom any order. `None`: inherited Blocker. | reveal top-or-bottom ordering, `OnAddHand`, descriptor-aware same timing ordering, inherited Blocker static layer가 필요하다. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT12-004#2402@base` | `BT12_004` | `DCGO/Assets/CardBaseEntity/BT12/Green/DigiEgg/BT12_004.asset` | `blocked` |
| `BT12-004#4453@P0` | `BT12_004` | `DCGO/Assets/CardBaseEntity/BT12/Green/DigiEgg/BT12_004_P0.asset` | `blocked` |
| `BT12-005#2403@base` | `BT12_005` | `DCGO/Assets/CardBaseEntity/BT12/Black/DigiEgg/BT12_005.asset` | `blocked` |
| `BT12-005#4454@P0` | `BT12_005` | `DCGO/Assets/CardBaseEntity/BT12/Black/DigiEgg/BT12_005_P0.asset` | `blocked` |
| `BT12-006#2404@base` | `BT12_006` | `DCGO/Assets/CardBaseEntity/BT12/Purple/DigiEgg/BT12_006.asset` | `blocked` |
| `BT12-006#4455@P0` | `BT12_006` | `DCGO/Assets/CardBaseEntity/BT12/Purple/DigiEgg/BT12_006_P0.asset` | `blocked` |
| `BT12-009#2408@base` | `BT12_009` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_009.asset` | `blocked` |
| `BT12-009#4456@P1` | `BT12_009` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_009_P1.asset` | `blocked` |
| `BT12-026#2434@base` | `BT12_026` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_026.asset` | `blocked` |
| `BT12-026#4483@P0` | `BT12_026` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_026_P0.asset` | `blocked` |
| `BT12-027#2435@base` | `BT12_027` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_027.asset` | `blocked` |
| `BT12-032#2442@base` | `BT12_032` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_032.asset` | `blocked` |
| `BT12-032#4491@P0` | `BT12_032` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_032_P0.asset` | `blocked` |
| `BT12-045#2457@base` | `BT12_045` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_045.asset` | `blocked` |
| `BT12-064#2478@base` | `BT12_064` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_064.asset` | `blocked` |
| `BT12-064#2479@P1` | `BT12_064` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_064_P1.asset` | `blocked` |
| `BT12-080#2497@base` | `BT12_080` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_080.asset` | `blocked` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `blocked`; C0012 uses hand-to-source, permanent-to-source, trash-to-source, source trash, source add, and zone movement after deletion.
- `L0005_draw_search_reveal_hidden` remains relevant for draw, reveal-and-process-all, and deck top-or-bottom ordering.
- `BT12_026`, `BT12_027`, `BT12_032`, and `BT12_064` require source movement payloads that cannot be emulated by direct zone-list mutation.
- `BT12_064` needs alternate digivolution requirement and De-Digivolve common layers before runnable implementation.
- `BT12_080` has separate `OnPlay` and `WhenDigivolving` descriptors plus inherited Blocker. Keep descriptor identity separate for ordering/replay/hash.

## Follow-Up

The next generated queue item is `C0013_zone_security_recovery`. C0012 remains source-reviewed but blocked until the shared layers above are implemented and verified. Once those layers exist, port each source body under the planned full-card-pool source path and add card-specific selection/replay tests before changing any C0012 identity to runnable.
