# C0254_special_digivolution_play - special digivolution/play mechanics card porting 19

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0254_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 31
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT13_016` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_016.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 2 |
| `BT13_017` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_017.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 4 |
| `BT13_018` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_018.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase, OnTappedAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 5 |
| `BT13_019` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_019.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectBoolean, SelectJogress` | 5 |
| `BT13_020` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_020.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress, SelectBurstDigivolution` | 6 |
| `BT13_025` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_025.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT13_028` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_028.cs` | `None, OnDeclaration, OnEndAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectOrder, SelectJogress` | 2 |
| `BT13_033` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_033.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectBurstDigivolution` | 3 |
| `BT13_035` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_035.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT13_039` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_039.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT13-016#2659@base` | `BT13-016` | 2659 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_016.asset` |
| `BT13-016#4564@P0` | `BT13-016` | 4564 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_016_P0.asset` |
| `BT13-017#2660@base` | `BT13-017` | 2660 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_017.asset` |
| `BT13-017#2661@P1` | `BT13-017` | 2661 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_017_P1.asset` |
| `BT13-017#4565@P0` | `BT13-017` | 4565 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_017_P0.asset` |
| `BT13-017#4566@P2` | `BT13-017` | 4566 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_017_P2.asset` |
| `BT13-018#2662@base` | `BT13-018` | 2662 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_018.asset` |
| `BT13-018#4567@P0` | `BT13-018` | 4567 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_018_P0.asset` |
| `BT13-018#4568@P1` | `BT13-018` | 4568 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_018_P1.asset` |
| `BT13-018#4569@P2` | `BT13-018` | 4569 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_018_P2.asset` |
| `BT13-018#8144@P3` | `BT13-018` | 8144 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_018_P3.asset` |
| `BT13-019#2663@base` | `BT13-019` | 2663 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_019.asset` |
| `BT13-019#2664@P1` | `BT13-019` | 2664 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_019_P1.asset` |
| `BT13-019#4570@P0` | `BT13-019` | 4570 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_019_P0.asset` |
| `BT13-019#4571@P2` | `BT13-019` | 4571 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_019_P2.asset` |
| `BT13-019#8145@P3` | `BT13-019` | 8145 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_019_P3.asset` |
| `BT13-020#2665@base` | `BT13-020` | 2665 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_020.asset` |
| `BT13-020#2666@P1` | `BT13-020` | 2666 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_020_P1.asset` |
| `BT13-020#2667@P2` | `BT13-020` | 2667 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_020_P2.asset` |
| `BT13-020#4572@P3` | `BT13-020` | 4572 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_020_P3.asset` |
| `BT13-020#8146@P4` | `BT13-020` | 8146 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_020_P4.asset` |
| `BT13-020#8147@P5` | `BT13-020` | 8147 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_020_P5.asset` |
| `BT13-025#2672@base` | `BT13-025` | 2672 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_025.asset` |
| `BT13-028#2675@base` | `BT13-028` | 2675 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_028.asset` |
| `BT13-028#4573@P0` | `BT13-028` | 4573 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_028_P0.asset` |
| `BT13-033#2681@base` | `BT13-033` | 2681 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_033.asset` |
| `BT13-033#2682@P1` | `BT13-033` | 2682 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_033_P1.asset` |
| `BT13-033#2683@P2` | `BT13-033` | 2683 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_033_P2.asset` |
| `BT13-035#2685@base` | `BT13-035` | 2685 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_035.asset` |
| `BT13-035#4580@P0` | `BT13-035` | 4580 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_035_P0.asset` |
| `BT13-039#2689@base` | `BT13-039` | 2689 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_039.asset` |

## Required Work

- Source Effects 표의 원본 파일을 먼저 읽고, 대응 카드별 RL effect 파일 또는 wrapper/mapping을 만든다.
- 같은 source effect를 여러 variant가 참조해도 CardId만으로 평탄화하지 않는다.
- 필요한 공통 helper는 source 의미를 숨기지 않는 범위에서만 추가한다.
- 구현 후 65 baseline에서 이 batch의 `Unsupported` blocker가 줄어드는지 확인한다.

## Tests / Validation

- 대상 카드별 unit/golden/replay 테스트를 추가하거나 갱신한다.
- `FullCardPoolValidator` deck subset 또는 batch-specific validation을 추가한다.
- 전체 regression을 실행한다.
- `DCGO/Assets/Scripts` 변경 없음과 tracked `bin/obj` 없음 확인을 보고한다.

## Blocker Conditions

- source body missing/ambiguous
- 원본 timing/selection/ordering 의미 미확인
- 공통 layer가 없는 mechanic을 카드별 workaround로 구현해야 하는 상황
- core service에 CardId 분기를 넣어야만 통과하는 설계
