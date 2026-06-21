# C0211_replacement_counter_cut_in - replacement/counter/cut-in card porting 26

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0211_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 34
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT5_112` | `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_112.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 4 |
| `BT6_056` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_056.cs` | `OnEndBattle, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent` | 4 |
| `BT6_059` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_059.cs` | `WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in` | `-` | 2 |
| `BT6_064` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_064.cs` | `OnDestroyedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |
| `BT6_091` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_091.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 2 |
| `BT6_111` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_111.cs` | `OnAllyAttack, OnEndAttack, OnEndBattle, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity` | 4 |
| `BT7_053` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_053.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT7_063` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_063.cs` | `OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectOrder, SelectJogress` | 2 |
| `BT7_064` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_064.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand` | 6 |
| `BT7_085` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_085.cs` | `None, OnDeclaration, SecuritySkill` | `inherited, max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectCard, SelectOrder, SelectJogress` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT5-112#1095@base` | `BT5-112` | 1095 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_112.asset` |
| `BT5-112#1096@P1` | `BT5-112` | 1096 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_112_P1.asset` |
| `BT5-112#8658@P2` | `BT5-112` | 8658 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_112_P2.asset` |
| `BT5-112#8659@P3` | `BT5-112` | 8659 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_112_P3.asset` |
| `BT6-056#1184@base` | `BT6-056` | 1184 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_056.asset` |
| `BT6-056#1185@P1` | `BT6-056` | 1185 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_056_P1.asset` |
| `BT6-056#8699@P2` | `BT6-056` | 8699 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_056_P2.asset` |
| `BT6-056#8700@P3` | `BT6-056` | 8700 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_056_P3.asset` |
| `BT6-059#1188@base` | `BT6-059` | 1188 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_059.asset` |
| `BT6-059#8701@P0` | `BT6-059` | 8701 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_059_P0.asset` |
| `BT6-064#1193@base` | `BT6-064` | 1193 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_064.asset` |
| `BT6-064#1194@P1` | `BT6-064` | 1194 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_064_P1.asset` |
| `BT6-064#1195@P2` | `BT6-064` | 1195 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_064_P2.asset` |
| `BT6-091#1233@base` | `BT6-091` | 1233 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Tamer/BT6_091.asset` |
| `BT6-091#8725@P0` | `BT6-091` | 8725 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Tamer/BT6_091_P0.asset` |
| `BT6-111#1255@base` | `BT6-111` | 1255 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_111.asset` |
| `BT6-111#1256@P1` | `BT6-111` | 1256 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_111_P1.asset` |
| `BT6-111#6786@P2` | `BT6-111` | 6786 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_111_P2.asset` |
| `BT6-111#6787@P3` | `BT6-111` | 6787 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_111_P3.asset` |
| `BT7-053#1459@base` | `BT7-053` | 1459 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_053.asset` |
| `BT7-053#8782@P0` | `BT7-053` | 8782 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_053_P0.asset` |
| `BT7-063#1474@base` | `BT7-063` | 1474 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_063.asset` |
| `BT7-063#1475@P1` | `BT7-063` | 1475 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_063_P1.asset` |
| `BT7-064#1476@base` | `BT7-064` | 1476 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_064.asset` |
| `BT7-064#1477@P1` | `BT7-064` | 1477 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_064_P1.asset` |
| `BT7-064#8795@P0` | `BT7-064` | 8795 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_064_P0.asset` |
| `BT7-064#8796@P2` | `BT7-064` | 8796 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_064_P2.asset` |
| `BT7-064#8797@P3` | `BT7-064` | 8797 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_064_P3.asset` |
| `BT7-064#8798@P4` | `BT7-064` | 8798 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_064_P4.asset` |
| `BT7-085#1514@base` | `BT7-085` | 1514 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Tamer/BT7_085.asset` |
| `BT7-085#1515@P1` | `BT7-085` | 1515 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Tamer/BT7_085_P1.asset` |
| `BT7-085#6791@P0` | `BT7-085` | 6791 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Tamer/BT7_085_P0.asset` |
| `BT7-085#6792@P2` | `BT7-085` | 6792 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Tamer/BT7_085_P2.asset` |
| `BT7-085#6793@P3` | `BT7-085` | 6793 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Tamer/BT7_085_P3.asset` |

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
