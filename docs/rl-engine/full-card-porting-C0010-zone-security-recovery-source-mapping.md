# Full Card Porting C0010 Zone Security Recovery Source Mapping

## 결정

`C0010_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch의 source body 10개와 16개 asset identity는 모두 로컬 DCGO 원본에서 확인되지만, runnable card body로 옮기려면 `L0006_zone_security_recovery`와 draw/reveal/search, recovery/security movement, return-to-hand/deck replacement, suspend/can't-unsuspend, hand discard, battle-deletion, piercing common layer가 먼저 필요하다.

Queue status: blocked

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않았다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT11_033` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_033.cs` | `WhenDigivolving`: 상대 레벨 5 이하 Digimon 1장을 hand bounce한다. bounce되지 않았으면 상대 security top을 hand에 추가하고 `IReduceSecurity`를 실행한다. `OnAddHand`: effect로 상대 hand에 카드가 추가되면 상대 hand 4장마다 memory +1. | bounce의 `willBeRemoveField`/`HandBounceEffect` 판정, security top add-hand + security 감소, `OnAddHand` by-effect payload, 동적 hand count memory가 필요하다. |
| `BT11_039` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_039.cs` | `WhenDigivolving`: 선택적으로 자신의 다른 yellow Digimon 1장을 security top에 face-down으로 놓는다. | permanent-to-security top 이동, security add/recovery 계열 event payload, optional target selection, zone leave/replacement interleaving이 필요하다. |
| `BT11_045` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_045.cs` | `WhenDigivolving`: security가 5장 이하이고 deck이 있으면 recovery +1(deck). `OnLoseSecurity`: 상대 턴에 자신의 security card가 제거되면 상대 Digimon 1장 DP -4000 until turn end. | recovery +1(deck), `OnLoseSecurity` payload, security movement timing, target selection, duration DP cleanup이 필요하다. |
| `BT11_046` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_046.cs` | `OnPlay`: deck top 4 reveal, Tamer 1장을 hand에 추가, 나머지는 deck bottom any order. `None`: inherited owner-turn static self DP +2000 if owner has Tamer. | reveal/search + hidden ordering, bottom ordering selection, `OnAddHand`/`OnDraw` 연동, inherited source role continuous DP가 필요하다. |
| `BT11_047` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_047.cs` | `OnStartTurn`: owner turn start에 deck이 있으면 draw 1. 원본 body는 draw 전에 `CanAddMemory`를 확인하므로 이 비정상 guard도 source mapping에 보존해야 한다. | `OnStartTurn` draw가 `DrawClass`/`OnAddHand`/`OnDraw` timing을 거치는 공통 draw boundary와 source-aligned guard audit이 필요하다. |
| `BT11_050` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_050.cs` | inherited owner-turn once-per-turn: owner가 Tamer를 play하면 상대 Digimon 1장을 suspend한다. | inherited source validation, `OnEnterFieldAnyone` play payload, once-per-turn key, suspend primitive/replacement/trace가 필요하다. |
| `BT11_052` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_052.cs` | `OnPlay`와 `WhenDigivolving`: 선택적으로 hand의 play cost 3 이하 Tamer 1장을 cost 없이 play하고 ETB를 활성화한다. `None`: inherited owner-turn static self DP +2000 if owner has Tamer. | optional hand selection, play-without-cost from hand, ETB interleaving, duplicate descriptor ordering, inherited continuous DP가 필요하다. |
| `BT11_055` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_055.cs` | `OnPlay`와 `WhenDigivolving`: 자신의 green/black Tamer 수만큼 상대 Digimon을 suspend한 뒤, 상대 suspended Digimon 1장이 다음 unsuspend phase에 unsuspend할 수 없게 한다. inherited `OnEndBattle`: 이 Digimon이 battle로 상대 Digimon을 delete하면 상대 security top 1장을 trash한다. | multi-step suspend selection, can't-unsuspend-next-active-phase duration, battle deletion payload, `IDestroySecurity`/security trash timing, inherited source role validation이 필요하다. |
| `BT11_057` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_057.cs` | `OnDetermineDoSecurityCheck`: self piercing. `WhenDigivolving`: hand에서 최대 3장까지 trash할 수 있고, trash한 장수만큼 상대 Digimon을 suspend한 뒤 상대 suspended Digimon 수만큼 memory +1. | piercing security-check integration, hand discard timing, optional partial hand selection, suspend count linkage, memory gain after current suspended count가 필요하다. |
| `BT11_060` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_060.cs` | `None`: 자신은 opponent effect로 hand 또는 deck으로 돌아가지 않는다. | return-to-hand/deck replacement/restriction layer, opponent-effect source predicate, would-remove target filtering이 필요하다. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT11-033#2304@base` | `BT11_033` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_033.asset` | `blocked` |
| `BT11-033#2305@P1` | `BT11_033` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_033_P1.asset` | `blocked` |
| `BT11-039#2311@base` | `BT11_039` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_039.asset` | `blocked` |
| `BT11-045#2318@base` | `BT11_045` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_045.asset` | `blocked` |
| `BT11-045#4405@P0` | `BT11_045` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_045_P0.asset` | `blocked` |
| `BT11-046#2319@base` | `BT11_046` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_046.asset` | `blocked` |
| `BT11-046#4406@P0` | `BT11_046` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_046_P0.asset` | `blocked` |
| `BT11-047#2320@base` | `BT11_047` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_047.asset` | `blocked` |
| `BT11-050#2323@base` | `BT11_050` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_050.asset` | `blocked` |
| `BT11-052#2325@base` | `BT11_052` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_052.asset` | `blocked` |
| `BT11-052#4407@P0` | `BT11_052` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_052_P0.asset` | `blocked` |
| `BT11-055#2328@base` | `BT11_055` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_055.asset` | `blocked` |
| `BT11-055#4410@P0` | `BT11_055` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_055_P0.asset` | `blocked` |
| `BT11-057#2331@base` | `BT11_057` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_057.asset` | `blocked` |
| `BT11-057#4411@P0` | `BT11_057` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_057_P0.asset` | `blocked` |
| `BT11-060#2334@base` | `BT11_060` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_060.asset` | `blocked` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `blocked`; C0010 uses add-hand from security, security loss, security recovery, security trash, permanent-to-security, hand discard, and bounce-to-hand/deck restrictions.
- `L0005_draw_search_reveal_hidden` remains relevant for `BT11_046` and `BT11_047`, because reveal/bottom ordering and draw-trigger sequencing are not source-aligned enough for runnable card bodies.
- Suspend and can't-unsuspend flows must use shared primitive/replacement boundaries, not direct `IsSuspended` mutation.
- `BT11_052` has two script-authored descriptors in the same timing. They must remain descriptor-aware for ordering, optional handling, replay, and state hash.
- `BT11_060` needs generic return-to-hand/deck replacement policy. Do not implement it as a `CardId` shortcut in a core service.

## Follow-Up

The next generated queue item is `C0011_zone_security_recovery`. C0010 remains source-reviewed but blocked until the shared layers above are implemented and verified. Once those layers exist, port each source body under the planned full-card-pool source path and add card-specific selection/replay tests before changing any C0010 identity to runnable.
