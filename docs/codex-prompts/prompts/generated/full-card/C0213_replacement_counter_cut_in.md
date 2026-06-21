# C0213_replacement_counter_cut_in - replacement/counter/cut-in card porting 28

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0213_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT8_053` | `DCGO/Assets/Scripts/CardEffect/BT8/Green/BT8_053.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 7 |
| `BT8_054` | `DCGO/Assets/Scripts/CardEffect/BT8/Green/BT8_054.cs` | `BeforePayCost, None, WhenDigisorption` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_priority, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT8_060` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_060.cs` | `OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT9_012` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_012.cs` | `None, WhenPermanentWouldBeDeleted, WhenReturntoHandAnyone, WhenReturntoLibraryAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT9_024` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_024.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT9_037` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_037.cs` | `None, OnAllyAttack, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT9_038` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_038.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT9_044` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_044.cs` | `None, OnAllyAttack, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectAttackTarget, SelectJogress` | 2 |
| `BT9_050` | `DCGO/Assets/Scripts/CardEffect/BT9/Green/BT9_050.cs` | `None, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT9_051` | `DCGO/Assets/Scripts/CardEffect/BT9/Green/BT9_051.cs` | `None, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT8-053#1625@base` | `BT8-053` | 1625 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_053.asset` |
| `BT8-053#1626@P1` | `BT8-053` | 1626 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_053_P1.asset` |
| `BT8-053#1627@P2` | `BT8-053` | 1627 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_053_P2.asset` |
| `BT8-053#1628@P3` | `BT8-053` | 1628 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_053_P3.asset` |
| `BT8-053#6798@P0` | `BT8-053` | 6798 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_053_P0.asset` |
| `BT8-053#6799@P4` | `BT8-053` | 6799 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_053_P4.asset` |
| `BT8-053#6800@P5` | `BT8-053` | 6800 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_053_P5.asset` |
| `BT8-054#1629@base` | `BT8-054` | 1629 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_054.asset` |
| `BT8-060#1637@base` | `BT8-060` | 1637 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_060.asset` |
| `BT8-060#1638@P1` | `BT8-060` | 1638 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_060_P1.asset` |
| `BT8-060#8881@P0` | `BT8-060` | 8881 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_060_P0.asset` |
| `BT9-012#1794@base` | `BT9-012` | 1794 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_012.asset` |
| `BT9-012#8952@P1` | `BT9-012` | 8952 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_012_P1.asset` |
| `BT9-024#1810@base` | `BT9-024` | 1810 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_024.asset` |
| `BT9-037#1824@base` | `BT9-037` | 1824 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_037.asset` |
| `BT9-037#8966@P0` | `BT9-037` | 8966 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_037_P0.asset` |
| `BT9-038#1825@base` | `BT9-038` | 1825 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_038.asset` |
| `BT9-038#8967@P0` | `BT9-038` | 8967 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_038_P0.asset` |
| `BT9-044#1831@base` | `BT9-044` | 1831 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_044.asset` |
| `BT9-044#1832@P1` | `BT9-044` | 1832 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_044_P1.asset` |
| `BT9-050#1838@base` | `BT9-050` | 1838 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_050.asset` |
| `BT9-050#8977@P1` | `BT9-050` | 8977 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_050_P1.asset` |
| `BT9-051#1839@base` | `BT9-051` | 1839 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_051.asset` |
| `BT9-051#8978@P0` | `BT9-051` | 8978 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_051_P0.asset` |

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
