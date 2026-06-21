# Full Card Porting C0011 Zone Security Recovery Source Mapping

## 결정

`C0011_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch의 source body 10개와 25개 asset identity는 모두 로컬 DCGO 원본에서 확인되지만, runnable card body로 옮기려면 `L0006_zone_security_recovery`와 reveal/top-or-bottom ordering, play-without-cost from revealed/source/security, deletion/save/retaliation, digivolution-source discard/add, suspend-cost, cost-modification, security skill, tamer memory setter, option-immunity, and `OnTappedAnyone` common layer가 먼저 필요하다.

Queue status: needs-review

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않았다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT11_068` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_068.cs` | `OnPlay`와 `WhenDigivolving`: deck top 5 reveal, play cost 4 이하 Tamer 1장을 cost 없이 play, 나머지는 deck top 또는 bottom any order. Inherited owner-turn once-per-turn: effect로 다른 Digimon을 play하면 자신의 Digimon 1장에게 opponent turn end까지 Blocker 부여. | reveal top-or-bottom ordering, library-root play without cost, ETB interleaving, descriptor ordering, by-effect play payload, inherited source validation, temporary Blocker duration이 필요하다. |
| `BT11_077` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_077.cs` | `OnPlay`: optional self-delete cost 성공 시 deck top 5 reveal, Bagra Army 1장을 hand, 나머지는 bottom any order. `OnDestroyedAnyone`: Save. `OnDigivolutionCardDiscarded`: opponent turn에 effect가 this source를 trash하면 memory +1. | self-delete cost/result continuation, reveal/search/bottom ordering, Save, `OnDigivolutionCardDiscarded` payload, source-in-trash activation, memory timing이 필요하다. |
| `BT11_079` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_079.cs` | `OnDestroyedAnyone`: Retaliation. `OnDeletion`: draw 1 then mandatory trash 1 card from hand if possible. | Retaliation battle-deletion alignment, `OnDeletion` source snapshot, draw/OnAddHand/OnDraw timing, mandatory hand discard selection이 필요하다. |
| `BT11_084` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_084.cs` | `OnDestroyedAnyone`: Retaliation. `WhenDigivolving`: draw 2 then trash up to available 2 cards from hand. Inherited all-turns once-per-turn: effect로 Digimon을 play하면 memory +1. | draw/discard sequencing, hand discard mandatory count, by-effect play payload, inherited once-per-turn source key, Retaliation timing이 필요하다. |
| `BT11_085` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_085.cs` | `OnPlay`와 `WhenDigivolving`: optional로 자신의 purple/blue Digimon source 중 purple/blue Lv3 Digimon 1장을 cost 없이 play. Inherited all-turns once-per-turn: effect로 Digimon을 play하면 memory +1. | permanent source-card selection, play from digivolution cards, source removal zone events, ETB interleaving, optional two-step selection, inherited once-per-turn by-effect play payload가 필요하다. |
| `BT11_088` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_088.cs` | `OnPlay`와 `WhenDigivolving`: 상대 Digimon 수가 1 이하이면 opponent hand를 보고 1장 trash, 2 이상이면 상대 Digimon 1장을 다른 상대 Digimon source bottom에 넣는다. `OnEnterFieldAnyone`/`OnAddDigivolutionCards`: 상대 Digimon digivolve 또는 effect source add 시, 이 Digimon의 source 1장을 trash하고 상대 security top 1장을 trash. | opponent hand reveal/discard, permanent-to-source bottom movement, `OnAddDigivolutionCards`, source trash cost/result, security trash, once-per-turn shared hash, two timing descriptors가 필요하다. |
| `BT11_089` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_089.cs` | `OnPlay`: deck top 4 reveal, red Vaccine Digimon 1장을 hand, 나머지는 bottom any order. Owner turn: effect로 red Avian/Bird/Beast/Animal/Sovereign Digimon을 play하면 이 Tamer를 suspend cost로 그 Digimon에게 Rush 부여. `SecuritySkill`: self Tamer play. | reveal/search/bottom ordering, suspend-cost lifecycle, by-effect play payload, Rush duration, security self-play and ETB interleaving이 필요하다. |
| `BT11_091` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_091.cs` | `None`: owner turn 동안 자신의 Digimon DP +1000. `BeforePayCost`: green Digimon이 level 5 이상으로 digivolve하려 할 때 optional suspend cost로 digivolution cost -1. `SecuritySkill`: self Tamer play. | tamer aura continuous DP, `BeforePayCost`/would-digivolve cost mutation, suspend-cost, temporary fixed-cost effect, security self-play가 필요하다. |
| `BT11_093` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_093.cs` | `OnStartTurn`: memory set-to-3 Tamer effect. Owner turn: Greymon-name Digimon digivolve 시 optional suspend cost로 DP +2000 until opponent turn end, same-level digivolve라면 opponent Option immunity도 부여. `SecuritySkill`: self Tamer play. | memory setter timing, suspend-cost, digivolve payload including same-level flag, option-immunity duration, security self-play가 필요하다. |
| `BT12_003` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_003.cs` | inherited owner-turn once-per-turn: 자신의 yellow/red Tamer가 suspend되면 상대 Digimon 1장 DP -1000 for turn. | `OnTappedAnyone` payload, tamer suspend source, inherited once-per-turn key, target selection, turn-end DP cleanup이 필요하다. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT11-068#2342@base` | `BT11_068` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_068.asset` | `needs-review` |
| `BT11-068#4417@P0` | `BT11_068` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_068_P0.asset` | `needs-review` |
| `BT11-068#4418@P1` | `BT11_068` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_068_P1.asset` | `needs-review` |
| `BT11-068#4419@P2` | `BT11_068` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_068_P2.asset` | `needs-review` |
| `BT11-077#2352@base` | `BT11_077` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_077.asset` | `needs-review` |
| `BT11-077#4426@P0` | `BT11_077` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_077_P0.asset` | `needs-review` |
| `BT11-079#2354@base` | `BT11_079` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_079.asset` | `needs-review` |
| `BT11-084#2360@base` | `BT11_084` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_084.asset` | `needs-review` |
| `BT11-085#2361@base` | `BT11_085` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_085.asset` | `needs-review` |
| `BT11-085#4430@P0` | `BT11_085` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_085_P0.asset` | `needs-review` |
| `BT11-088#2365@base` | `BT11_088` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_088.asset` | `needs-review` |
| `BT11-088#2366@P1` | `BT11_088` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_088_P1.asset` | `needs-review` |
| `BT11-089#2367@base` | `BT11_089` | `DCGO/Assets/CardBaseEntity/BT11/Red/Tamer/BT11_089.asset` | `needs-review` |
| `BT11-089#2368@P1` | `BT11_089` | `DCGO/Assets/CardBaseEntity/BT11/Red/Tamer/BT11_089_P1.asset` | `needs-review` |
| `BT11-089#4433@P0` | `BT11_089` | `DCGO/Assets/CardBaseEntity/BT11/Red/Tamer/BT11_089_P0.asset` | `needs-review` |
| `BT11-089#8109@P2` | `BT11_089` | `DCGO/Assets/CardBaseEntity/BT11/Red/Tamer/BT11_089_P2.asset` | `needs-review` |
| `BT11-091#2371@base` | `BT11_091` | `DCGO/Assets/CardBaseEntity/BT11/Green/Tamer/BT11_091.asset` | `needs-review` |
| `BT11-091#2372@P1` | `BT11_091` | `DCGO/Assets/CardBaseEntity/BT11/Green/Tamer/BT11_091_P1.asset` | `needs-review` |
| `BT11-091#4435@P0` | `BT11_091` | `DCGO/Assets/CardBaseEntity/BT11/Green/Tamer/BT11_091_P0.asset` | `needs-review` |
| `BT11-093#2374@base` | `BT11_093` | `DCGO/Assets/CardBaseEntity/BT11/Black/Tamer/BT11_093.asset` | `needs-review` |
| `BT11-093#2375@P1` | `BT11_093` | `DCGO/Assets/CardBaseEntity/BT11/Black/Tamer/BT11_093_P1.asset` | `needs-review` |
| `BT11-093#4438@P0` | `BT11_093` | `DCGO/Assets/CardBaseEntity/BT11/Black/Tamer/BT11_093_P0.asset` | `needs-review` |
| `BT12-003#2401@base` | `BT12_003` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/DigiEgg/BT12_003.asset` | `needs-review` |
| `BT12-003#4451@P0` | `BT12_003` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/DigiEgg/BT12_003_P0.asset` | `needs-review` |
| `BT12-003#4452@P1` | `BT12_003` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/DigiEgg/BT12_003_P1.asset` | `needs-review` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `needs-review`; C0011 uses source-card trash, permanent-to-source, security self-play, security trash, hand discard, and opponent-hand discard.
- `L0005_draw_search_reveal_hidden` remains relevant for reveal/search, deck top-or-bottom ordering, draw and hand-add timing.
- `BeforePayCost` and cost mutation must be source-aligned before `BT11_091` can become runnable.
- `BT11_068`, `BT11_085`, and `BT11_088` contain multiple script-authored descriptors in the same timing. They must remain descriptor-aware for ordering, optional handling, replay, and state hash.
- `BT11_089`, `BT11_091`, and `BT11_093` use `SecuritySkill` self-play. This must run through shared security execution and ETB interleaving, not a Tamer-specific shortcut.
- `BT12_003` depends on `OnTappedAnyone`; do not synthesize this from direct suspend mutation without the original cut-in/timing payload.

## Follow-Up

The next generated queue item is `C0012_zone_security_recovery`. C0011 remains source-reviewed but blocked until the shared layers above are implemented and verified. Once those layers exist, port each source body under the planned full-card-pool source path and add card-specific selection/replay tests before changing any C0011 identity to runnable.
