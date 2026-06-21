# C0210_replacement_counter_cut_in - replacement/counter/cut-in card porting 25

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0210_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 31
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT2_047` | `DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_047.cs` | `BeforePayCost, OnAllyAttack, WhenDigisorption` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_priority, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `BT2_050` | `DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_050.cs` | `BeforePayCost, None, WhenDigisorption` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_priority, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT2_082` | `DCGO/Assets/Scripts/CardEffect/BT2/White/BT2_082.cs` | `OnAllyAttack, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT3_054` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_054.cs` | `BeforePayCost, WhenDigisorption` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_priority, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `BT3_056` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs` | `BeforePayCost, None, WhenDigisorption` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_priority, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT3_057` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_057.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 5 |
| `BT3_097` | `DCGO/Assets/Scripts/CardEffect/BT3/Red/BT3_097.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT4_054` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_054.cs` | `OnDeclaration` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 2 |
| `BT5_058` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_058.cs` | `BeforePayCost, None, OnEnterFieldAnyone, WhenDigisorption` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_priority, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT5_086` | `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_086.cs` | `OnEnterFieldAnyone, WhenPermanentWouldBeDeleted, WhenReturntoHandAnyone, WhenReturntoLibraryAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 10 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-047#444@base` | `BT2-047` | 444 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_047.asset` |
| `BT2-047#445@P1` | `BT2-047` | 445 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_047_P1.asset` |
| `BT2-050#449@base` | `BT2-050` | 449 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_050.asset` |
| `BT2-082#516@base` | `BT2-082` | 516 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/White/Digimon/BT2_082.asset` |
| `BT2-082#517@P1` | `BT2-082` | 517 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/White/Digimon/BT2_082_P1.asset` |
| `BT3-054#673@base` | `BT3-054` | 673 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_054.asset` |
| `BT3-054#674@P1` | `BT3-054` | 674 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_054_P1.asset` |
| `BT3-054#8476@P2` | `BT3-054` | 8476 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_054_P2.asset` |
| `BT3-056#676@base` | `BT3-056` | 676 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_056.asset` |
| `BT3-056#677@P1` | `BT3-056` | 677 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_056_P1.asset` |
| `BT3-057#678@base` | `BT3-057` | 678 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_057.asset` |
| `BT3-057#679@P1` | `BT3-057` | 679 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_057_P1.asset` |
| `BT3-057#680@P2` | `BT3-057` | 680 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_057_P2.asset` |
| `BT3-057#8477@P3` | `BT3-057` | 8477 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_057_P3.asset` |
| `BT3-057#8478@P4` | `BT3-057` | 8478 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_057_P4.asset` |
| `BT3-097#6772@P1` | `BT3-097` | 6772 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Option/BT3_097_P1.asset` |
| `BT3-097#738@base` | `BT3-097` | 738 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Option/BT3_097.asset` |
| `BT4-054#834@base` | `BT4-054` | 834 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_054.asset` |
| `BT4-054#8523@P0` | `BT4-054` | 8523 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_054_P0.asset` |
| `BT5-058#1016@base` | `BT5-058` | 1016 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_058.asset` |
| `BT5-058#8607@P0` | `BT5-058` | 8607 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_058_P0.asset` |
| `BT5-086#1055@base` | `BT5-086` | 1055 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086.asset` |
| `BT5-086#1056@P1` | `BT5-086` | 1056 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P1.asset` |
| `BT5-086#1057@P2` | `BT5-086` | 1057 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P2.asset` |
| `BT5-086#1058@P3` | `BT5-086` | 1058 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P3.asset` |
| `BT5-086#1059@P4` | `BT5-086` | 1059 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P4.asset` |
| `BT5-086#8624@P5` | `BT5-086` | 8624 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P5.asset` |
| `BT5-086#8625@P6` | `BT5-086` | 8625 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P6.asset` |
| `BT5-086#8626@P7` | `BT5-086` | 8626 | `P7` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P7.asset` |
| `BT5-086#8627@P8` | `BT5-086` | 8627 | `P8` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P8.asset` |
| `BT5-086#8628@P9` | `BT5-086` | 8628 | `P9` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P9.asset` |

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
