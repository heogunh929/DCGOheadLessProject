# C0108_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 4

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0108_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT14_062` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_062.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT14_070` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_070.cs` | `OnDiscardHand` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT14_073` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_073.cs` | `OnDiscardHand` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT15_005` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_005.cs` | `OnUnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `BT15_025` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_025.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT15_046` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_046.cs` | `OnTappedAnyone` | `max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT15_047` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_047.cs` | `None, OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT16_002` | `DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_002.cs` | `None` | `static_or_continuous` | `-` | 4 |
| `BT16_003` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_003.cs` | `None` | `inherited, static_or_continuous` | `-` | 3 |
| `BT16_004` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_004.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT14-062#2988@base` | `BT14-062` | 2988 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_062.asset` |
| `BT14-070#2997@base` | `BT14-070` | 2997 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_070.asset` |
| `BT14-073#3001@base` | `BT14-073` | 3001 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_073.asset` |
| `BT15-005#3122@base` | `BT15-005` | 3122 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/DigiEgg/BT15_005.asset` |
| `BT15-005#3123@P1` | `BT15-005` | 3123 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/DigiEgg/BT15_005_P1.asset` |
| `BT15-005#4712@P0` | `BT15-005` | 4712 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/DigiEgg/BT15_005_P0.asset` |
| `BT15-025#3148@base` | `BT15-025` | 3148 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_025.asset` |
| `BT15-025#4723@P0` | `BT15-025` | 4723 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_025_P0.asset` |
| `BT15-046#3175@base` | `BT15-046` | 3175 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_046.asset` |
| `BT15-047#3176@base` | `BT15-047` | 3176 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_047.asset` |
| `BT16-002#3302@base` | `BT16-002` | 3302 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/DigiEgg/BT16_002.asset` |
| `BT16-002#3303@P1` | `BT16-002` | 3303 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/DigiEgg/BT16_002_P1.asset` |
| `BT16-002#4774@P0` | `BT16-002` | 4774 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/DigiEgg/BT16_002_P0.asset` |
| `BT16-002#8190@P2` | `BT16-002` | 8190 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/DigiEgg/BT16_002_P2.asset` |
| `BT16-003#3304@base` | `BT16-003` | 3304 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/DigiEgg/BT16_003.asset` |
| `BT16-003#3305@P1` | `BT16-003` | 3305 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/DigiEgg/BT16_003_P1.asset` |
| `BT16-003#4775@P0` | `BT16-003` | 4775 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/DigiEgg/BT16_003_P0.asset` |
| `BT16-004#3306@base` | `BT16-004` | 3306 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/DigiEgg/BT16_004.asset` |
| `BT16-004#3307@P1` | `BT16-004` | 3307 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/DigiEgg/BT16_004_P1.asset` |
| `BT16-004#4776@P0` | `BT16-004` | 4776 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/DigiEgg/BT16_004_P0.asset` |

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
