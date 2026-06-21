# C0113_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 9

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0113_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 31
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT2_031` | `DCGO/Assets/Scripts/CardEffect/BT2/Blue/BT2_031.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT2_032` | `DCGO/Assets/Scripts/CardEffect/BT2/Blue/BT2_032.cs` | `OnTappedAnyone, OnUnTappedAnyone` | `max_count_per_turn, static_or_continuous` | `-` | 5 |
| `BT2_046` | `DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_046.cs` | `OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT2_055` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_055.cs` | `None` | `inherited, static_or_continuous` | `-` | 8 |
| `BT2_057` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_057.cs` | `None` | `inherited, static_or_continuous` | `-` | 3 |
| `BT2_058` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_058.cs` | `None` | `inherited, modifier_duration, static_or_continuous` | `-` | 2 |
| `BT2_063` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_063.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT2_065` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_065.cs` | `None` | `inherited, static_or_continuous` | `-` | 3 |
| `BT2_073` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_073.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT2_074` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_074.cs` | `OnDestroyedAnyone` | `inherited` | `-` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-031#413@base` | `BT2-031` | 413 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_031.asset` |
| `BT2-032#414@base` | `BT2-032` | 414 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_032.asset` |
| `BT2-032#415@P1` | `BT2-032` | 415 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_032_P1.asset` |
| `BT2-032#416@P2` | `BT2-032` | 416 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_032_P2.asset` |
| `BT2-032#417@P3` | `BT2-032` | 417 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_032_P3.asset` |
| `BT2-032#8308@P4` | `BT2-032` | 8308 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_032_P4.asset` |
| `BT2-046#442@base` | `BT2-046` | 442 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_046.asset` |
| `BT2-046#443@P1` | `BT2-046` | 443 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_046_P1.asset` |
| `BT2-055#460@base` | `BT2-055` | 460 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_055.asset` |
| `BT2-055#461@P1` | `BT2-055` | 461 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_055_P1.asset` |
| `BT2-055#462@P2` | `BT2-055` | 462 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_055_P2.asset` |
| `BT2-055#8313@P3` | `BT2-055` | 8313 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_055_P3.asset` |
| `BT2-057#466@base` | `BT2-057` | 466 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_057.asset` |
| `BT2-057#467@P1` | `BT2-057` | 467 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_057_P1.asset` |
| `BT2-058#468@base` | `BT2-058` | 468 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_058.asset` |
| `BT2-058#469@P1` | `BT2-058` | 469 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_058_P1.asset` |
| `BT2-063#475@base` | `BT2-063` | 475 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_063.asset` |
| `BT2-063#476@P1` | `BT2-063` | 476 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_063_P1.asset` |
| `BT2-065#478@base` | `BT2-065` | 478 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_065.asset` |
| `BT2-065#479@P1` | `BT2-065` | 479 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_065_P1.asset` |
| `BT2-065#480@P2` | `BT2-065` | 480 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_065_P2.asset` |
| `BT2-073#500@base` | `BT2-073` | 500 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_073.asset` |
| `BT2-073#501@P1` | `BT2-073` | 501 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_073_P1.asset` |
| `BT2-074#502@base` | `BT2-074` | 502 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_074.asset` |
| `BT2-074#503@P1` | `BT2-074` | 503 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_074_P1.asset` |
| `BT2-074#8320@P2` | `BT2-074` | 8320 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_074_P2.asset` |
| `BT5-064#1023@base` | `BT5-064` | 1023 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_064.asset` |
| `ST15-03#2832@base` | `ST15-03` | 2832 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_03.asset` |
| `ST15-03#4929@P0` | `ST15-03` | 4929 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_03_P0.asset` |
| `ST15-06#2835@base` | `ST15-06` | 2835 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_06.asset` |
| `ST15-06#4932@P0` | `ST15-06` | 4932 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_06_P0.asset` |

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
