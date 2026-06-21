# C0212_replacement_counter_cut_in - replacement/counter/cut-in card porting 27

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0212_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 33
- Source effect count: 9
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT7_087` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_087.cs` | `OnAddHand, OnDeclaration, SecuritySkill` | `inherited, max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectOrder, SelectJogress` | 4 |
| `BT7_103` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_103.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `BT8_012` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_012.cs` | `None, OnAllyAttack, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous` | `SelectJogress` | 5 |
| `BT8_023` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_023.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT8_026` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_026.cs` | `None, OnAllyAttack, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT8_038` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_038.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 5 |
| `BT8_039` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_039.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT8_048` | `DCGO/Assets/Scripts/CardEffect/BT8/Green/BT8_048.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT8_051` | `DCGO/Assets/Scripts/CardEffect/BT8/Green/BT8_051.cs` | `None, OnAllyAttack, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT7-087#1518@base` | `BT7-087` | 1518 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Tamer/BT7_087.asset` |
| `BT7-087#1519@P1` | `BT7-087` | 1519 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Tamer/BT7_087_P1.asset` |
| `BT7-087#8811@P0` | `BT7-087` | 8811 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Tamer/BT7_087_P0.asset` |
| `BT7-087#8812@P2` | `BT7-087` | 8812 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Tamer/BT7_087_P2.asset` |
| `BT7-103#1538@base` | `BT7-103` | 1538 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Option/BT7_103.asset` |
| `BT7-103#8826@P0` | `BT7-103` | 8826 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Option/BT7_103_P0.asset` |
| `BT7-103#8827@P1` | `BT7-103` | 8827 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Option/BT7_103_P1.asset` |
| `BT7-103#8828@P2` | `BT7-103` | 8828 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Option/BT7_103_P2.asset` |
| `BT8-012#1570@base` | `BT8-012` | 1570 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_012.asset` |
| `BT8-012#1571@P1` | `BT8-012` | 1571 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_012_P1.asset` |
| `BT8-012#8849@P0` | `BT8-012` | 8849 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_012_P0.asset` |
| `BT8-012#8850@P2` | `BT8-012` | 8850 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_012_P2.asset` |
| `BT8-012#8851@P3` | `BT8-012` | 8851 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_012_P3.asset` |
| `BT8-023#1585@base` | `BT8-023` | 1585 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_023.asset` |
| `BT8-023#8857@P0` | `BT8-023` | 8857 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_023_P0.asset` |
| `BT8-023#8858@P1` | `BT8-023` | 8858 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_023_P1.asset` |
| `BT8-026#1588@base` | `BT8-026` | 1588 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_026.asset` |
| `BT8-026#1589@P1` | `BT8-026` | 1589 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_026_P1.asset` |
| `BT8-026#8860@P0` | `BT8-026` | 8860 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_026_P0.asset` |
| `BT8-038#1604@base` | `BT8-038` | 1604 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_038.asset` |
| `BT8-038#1605@P1` | `BT8-038` | 1605 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_038_P1.asset` |
| `BT8-038#8864@P2` | `BT8-038` | 8864 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_038_P2.asset` |
| `BT8-038#8865@P3` | `BT8-038` | 8865 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_038_P3.asset` |
| `BT8-038#8866@P4` | `BT8-038` | 8866 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_038_P4.asset` |
| `BT8-039#1606@base` | `BT8-039` | 1606 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_039.asset` |
| `BT8-039#1607@P1` | `BT8-039` | 1607 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_039_P1.asset` |
| `BT8-039#3290@P2` | `BT8-039` | 3290 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_039_P2.asset` |
| `BT8-048#1619@base` | `BT8-048` | 1619 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_048.asset` |
| `BT8-048#8871@P0` | `BT8-048` | 8871 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_048_P0.asset` |
| `BT8-048#8872@P1` | `BT8-048` | 8872 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_048_P1.asset` |
| `BT8-051#1622@base` | `BT8-051` | 1622 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_051.asset` |
| `BT8-051#1623@P1` | `BT8-051` | 1623 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_051_P1.asset` |
| `BT8-051#8874@P0` | `BT8-051` | 8874 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_051_P0.asset` |

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
