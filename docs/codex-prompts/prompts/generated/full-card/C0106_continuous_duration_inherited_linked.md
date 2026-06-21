# C0106_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 2

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0106_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT11_078` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_078.cs` | `None, OnDestroyedAnyone` | `inherited, modifier_duration, static_or_continuous` | `-` | 1 |
| `BT11_080` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_080.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `-` | 2 |
| `BT12_001` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_001.cs` | `None` | `inherited, modifier_duration, static_or_continuous` | `-` | 4 |
| `BT12_053` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_053.cs` | `None, OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT12_058` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_058.cs` | `None` | `static_or_continuous` | `-` | 1 |
| `BT12_060` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_060.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `-` | 1 |
| `BT12_061` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_061.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT12_067` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_067.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT12_076` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_076.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `-` | 1 |
| `BT13_002` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_002.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-078#2353@base` | `BT11-078` | 2353 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_078.asset` |
| `BT11-080#2355@base` | `BT11-080` | 2355 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_080.asset` |
| `BT11-080#4427@P0` | `BT11-080` | 4427 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_080_P0.asset` |
| `BT12-001#2398@base` | `BT12-001` | 2398 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/DigiEgg/BT12_001.asset` |
| `BT12-001#2399@P1` | `BT12-001` | 2399 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_001_P1.asset` |
| `BT12-001#4447@P0` | `BT12-001` | 4447 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/DigiEgg/BT12_001_P0.asset` |
| `BT12-001#4448@P1` | `BT12-001` | 4448 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/DigiEgg/BT12_001_P1.asset` |
| `BT12-053#2466@base` | `BT12-053` | 2466 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_053.asset` |
| `BT12-058#2472@base` | `BT12-058` | 2472 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_058.asset` |
| `BT12-060#2474@base` | `BT12-060` | 2474 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_060.asset` |
| `BT12-061#2475@base` | `BT12-061` | 2475 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_061.asset` |
| `BT12-067#2482@base` | `BT12-067` | 2482 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_067.asset` |
| `BT12-076#2492@base` | `BT12-076` | 2492 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_076.asset` |
| `BT13-002#2644@base` | `BT13-002` | 2644 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/DigiEgg/BT13_002.asset` |
| `BT13-002#4554@P0` | `BT13-002` | 4554 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/DigiEgg/BT13_002_P0.asset` |

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
