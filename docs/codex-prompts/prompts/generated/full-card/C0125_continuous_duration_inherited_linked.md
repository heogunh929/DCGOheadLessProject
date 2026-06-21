# C0125_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 21

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0125_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 27
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST20_01` | `DCGO/Assets/Scripts/CardEffect/ST20/Black/ST20_01.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `ST24_08` | `DCGO/Assets/Scripts/CardEffect/ST24/Green/ST24_08.cs` | `None` | `inherited, static_or_continuous` | `SelectCard` | 1 |
| `ST2_01` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_01.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `ST2_08` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_08.cs` | `None` | `inherited, static_or_continuous` | `-` | 3 |
| `ST3_01` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_01.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `ST3_04` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_04.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 7 |
| `ST4_01` | `DCGO/Assets/Scripts/CardEffect/ST4/Green/ST4_01.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `ST5_01` | `DCGO/Assets/Scripts/CardEffect/ST5/Black/ST5_01.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `ST5_11` | `DCGO/Assets/Scripts/CardEffect/ST5/Black/ST5_11.cs` | `None` | `inherited, static_or_continuous` | `-` | 4 |
| `ST7_01` | `DCGO/Assets/Scripts/CardEffect/ST7/Red/ST7_01.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT14-055#2980@base` | `BT14-055` | 2980 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_055.asset` |
| `BT3-072#699@base` | `BT3-072` | 699 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_072.asset` |
| `ST2-01#44@base` | `ST2-01` | 44 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/DigiEgg/ST2_01.asset` |
| `ST2-01#4972@P1` | `ST2-01` | 4972 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/DigiEgg/ST2_01_P1.asset` |
| `ST2-08#56@base` | `ST2-08` | 56 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_08.asset` |
| `ST2-08#57@P1` | `ST2-08` | 57 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_08_P1.asset` |
| `ST2-08#58@P2` | `ST2-08` | 58 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_08_P2.asset` |
| `ST20-01#5263@base` | `ST20-01` | 5263 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Black/DigiEgg/ST20_01.asset` |
| `ST20-01#9051@P2` | `ST20-01` | 9051 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Black/DigiEgg/ST20_01_P2.asset` |
| `ST24-08#7917@base` | `ST24-08` | 7917 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Green/Digimon/ST24_08.asset` |
| `ST3-01#4976@P1` | `ST3-01` | 4976 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/DigiEgg/ST3_01_P1.asset` |
| `ST3-01#75@base` | `ST3-01` | 75 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/DigiEgg/ST3_01.asset` |
| `ST3-04#4978@P4` | `ST3-04` | 4978 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_04_P4.asset` |
| `ST3-04#4979@P5` | `ST3-04` | 4979 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_04_P5.asset` |
| `ST3-04#4980@P6` | `ST3-04` | 4980 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_04_P6.asset` |
| `ST3-04#79@base` | `ST3-04` | 79 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_04.asset` |
| `ST3-04#80@P1` | `ST3-04` | 80 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_04_P1.asset` |
| `ST3-04#81@P2` | `ST3-04` | 81 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_04_P2.asset` |
| `ST3-04#82@P3` | `ST3-04` | 82 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_04_P3.asset` |
| `ST4-01#107@base` | `ST4-01` | 107 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/DigiEgg/ST4_01.asset` |
| `ST4-01#4983@P1` | `ST4-01` | 4983 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/DigiEgg/ST4_01_P1.asset` |
| `ST5-01#320@base` | `ST5-01` | 320 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/DigiEgg/ST5_01.asset` |
| `ST5-01#4984@P1` | `ST5-01` | 4984 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/DigiEgg/ST5_01_P1.asset` |
| `ST5-11#332@base` | `ST5-11` | 332 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_11.asset` |
| `ST5-11#4986@P1` | `ST5-11` | 4986 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_11_P1.asset` |
| `ST7-01#4992@P1` | `ST7-01` | 4992 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/DigiEgg/ST7_01_P1.asset` |
| `ST7-01#563@base` | `ST7-01` | 563 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/DigiEgg/ST7_01.asset` |

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
