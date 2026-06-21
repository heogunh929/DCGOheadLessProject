# C0340_special_digivolution_play - special digivolution/play mechanics card porting 105

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0340_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT8_032` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_032.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT8_042` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_042.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 6 |
| `BT8_043` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_043.cs` | `BeforePayCost, None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT8_046` | `DCGO/Assets/Scripts/CardEffect/BT8/Green/BT8_046.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT8_061` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_061.cs` | `None` | `static_or_continuous` | `SelectJogress` | 1 |
| `BT8_062` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_062.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 2 |
| `BT8_065` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_065.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectOrder, SelectJogress` | 1 |
| `BT8_068` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_068.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT8_069` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_069.cs` | `OnAddDigivolutionCards, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT8_080` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_080.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT8-032#1595@base` | `BT8-032` | 1595 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_032.asset` |
| `BT8-032#1596@P1` | `BT8-032` | 1596 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_032_P1.asset` |
| `BT8-032#1597@P2` | `BT8-032` | 1597 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_032_P2.asset` |
| `BT8-042#1610@base` | `BT8-042` | 1610 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_042.asset` |
| `BT8-042#1611@P1` | `BT8-042` | 1611 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_042_P1.asset` |
| `BT8-042#1612@P2` | `BT8-042` | 1612 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_042_P2.asset` |
| `BT8-042#1613@P3` | `BT8-042` | 1613 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_042_P3.asset` |
| `BT8-042#8867@P0` | `BT8-042` | 8867 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_042_P0.asset` |
| `BT8-042#8868@P4` | `BT8-042` | 8868 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_042_P4.asset` |
| `BT8-043#1614@base` | `BT8-043` | 1614 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_043.asset` |
| `BT8-043#8869@P0` | `BT8-043` | 8869 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_043_P0.asset` |
| `BT8-046#1617@base` | `BT8-046` | 1617 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_046.asset` |
| `BT8-046#6796@P0` | `BT8-046` | 6796 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_046_P0.asset` |
| `BT8-046#6797@P1` | `BT8-046` | 6797 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_046_P1.asset` |
| `BT8-061#1639@base` | `BT8-061` | 1639 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_061.asset` |
| `BT8-062#1640@base` | `BT8-062` | 1640 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_062.asset` |
| `BT8-062#8882@P0` | `BT8-062` | 8882 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_062_P0.asset` |
| `BT8-065#1644@base` | `BT8-065` | 1644 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_065.asset` |
| `BT8-068#1648@base` | `BT8-068` | 1648 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_068.asset` |
| `BT8-068#8885@P0` | `BT8-068` | 8885 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_068_P0.asset` |
| `BT8-069#1649@base` | `BT8-069` | 1649 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_069.asset` |
| `BT8-069#1650@P1` | `BT8-069` | 1650 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_069_P1.asset` |
| `BT8-080#1663@base` | `BT8-080` | 1663 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_080.asset` |
| `BT8-080#8894@P0` | `BT8-080` | 8894 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_080_P0.asset` |

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
