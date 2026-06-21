# C0109_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 5

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0109_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 14
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT16_005` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_005.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `BT16_050` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_050.cs` | `None` | `inherited, modifier_duration, static_or_continuous` | `-` | 1 |
| `BT17_003` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_003.cs` | `OnAddDigivolutionCards` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT17_005` | `DCGO/Assets/Scripts/CardEffect/BT17/Black/BT17_005.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT18_002` | `DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_002.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT18_005` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_005.cs` | `OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT18_045` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_045.cs` | `None` | `inherited, modifier_duration, static_or_continuous` | `-` | 1 |
| `BT18_051` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_051.cs` | `None` | `inherited, static_or_continuous` | `SelectCard` | 1 |
| `BT19_004` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_004.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT19_005` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_005.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT16-005#3308@base` | `BT16-005` | 3308 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/DigiEgg/BT16_005.asset` |
| `BT16-005#3309@P1` | `BT16-005` | 3309 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/DigiEgg/BT16_005_P1.asset` |
| `BT16-005#4777@P0` | `BT16-005` | 4777 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/DigiEgg/BT16_005_P0.asset` |
| `BT16-050#3366@base` | `BT16-050` | 3366 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_050.asset` |
| `BT17-003#3543@base` | `BT17-003` | 3543 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/DigiEgg/BT17_003.asset` |
| `BT17-003#4835@P0` | `BT17-003` | 4835 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/DigiEgg/BT17_003_P0.asset` |
| `BT17-005#3545@base` | `BT17-005` | 3545 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Black/DigiEgg/BT17_005.asset` |
| `BT17-005#4837@P0` | `BT17-005` | 4837 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Black/DigiEgg/BT17_005_P0.asset` |
| `BT18-002#3849@base` | `BT18-002` | 3849 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/DigiEgg/BT18_002.asset` |
| `BT18-005#3848@base` | `BT18-005` | 3848 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/DigiEgg/BT18_005.asset` |
| `BT18-045#3896@base` | `BT18-045` | 3896 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_045.asset` |
| `BT18-051#3912@base` | `BT18-051` | 3912 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_051.asset` |
| `BT19-004#3978@base` | `BT19-004` | 3978 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/DigiEgg/BT19_004.asset` |
| `BT19-005#5007@base` | `BT19-005` | 5007 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/DigiEgg/BT19_005.asset` |

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
