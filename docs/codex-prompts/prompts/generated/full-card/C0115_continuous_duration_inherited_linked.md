# C0115_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 11

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0115_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 27
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT3_092` | `DCGO/Assets/Scripts/CardEffect/BT3/Purple/BT3_092.cs` | `OnDestroyedAnyone, OnDetermineDoSecurityCheck` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT4_005` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_005.cs` | `None` | `inherited, static_or_continuous` | `-` | 8 |
| `BT4_016` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_016.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT4_018` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_018.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT4_020` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_020.cs` | `OnTappedAnyone` | `max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT4_030` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_030.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT4_038` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_038.cs` | `None` | `inherited, static_or_continuous` | `-` | 4 |
| `BT4_066` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_066.cs` | `None` | `inherited, modifier_duration, static_or_continuous` | `-` | 1 |
| `BT4_070` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_070.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT4_073` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_073.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-066#2340@base` | `BT11-066` | 2340 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_066.asset` |
| `BT3-092#731@base` | `BT3-092` | 731 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_092.asset` |
| `BT3-092#8488@P1` | `BT3-092` | 8488 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_092_P1.asset` |
| `BT4-005#763@base` | `BT4-005` | 763 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/DigiEgg/BT4_005.asset` |
| `BT4-005#764@P1` | `BT4-005` | 764 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_005_P1.asset` |
| `BT4-005#765@P2` | `BT4-005` | 765 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_005_P2.asset` |
| `BT4-005#766@P3` | `BT4-005` | 766 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_005_P3.asset` |
| `BT4-005#8498@P0` | `BT4-005` | 8498 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/DigiEgg/BT4_005_P0.asset` |
| `BT4-005#8499@P1` | `BT4-005` | 8499 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/DigiEgg/BT4_005_P1.asset` |
| `BT4-005#8500@P2` | `BT4-005` | 8500 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/DigiEgg/BT4_005_P2.asset` |
| `BT4-005#8501@P3` | `BT4-005` | 8501 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/DigiEgg/BT4_005_P3.asset` |
| `BT4-016#785@base` | `BT4-016` | 785 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_016.asset` |
| `BT4-016#786@P1` | `BT4-016` | 786 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_016_P1.asset` |
| `BT4-018#789@base` | `BT4-018` | 789 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_018.asset` |
| `BT4-018#8510@P0` | `BT4-018` | 8510 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_018_P0.asset` |
| `BT4-020#792@base` | `BT4-020` | 792 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_020.asset` |
| `BT4-020#8512@P0` | `BT4-020` | 8512 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_020_P0.asset` |
| `BT4-030#803@base` | `BT4-030` | 803 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_030.asset` |
| `BT4-030#804@P1` | `BT4-030` | 804 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_030_P1.asset` |
| `BT4-038#814@base` | `BT4-038` | 814 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_038.asset` |
| `BT4-038#815@P1` | `BT4-038` | 815 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_038_P1.asset` |
| `BT4-038#8515@P0` | `BT4-038` | 8515 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_038_P0.asset` |
| `BT4-066#849@base` | `BT4-066` | 849 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_066.asset` |
| `BT4-070#854@base` | `BT4-070` | 854 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_070.asset` |
| `BT4-073#8532@P0` | `BT4-073` | 8532 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_073_P0.asset` |
| `BT4-073#858@base` | `BT4-073` | 858 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_073.asset` |
| `BT9-026#1812@base` | `BT9-026` | 1812 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_026.asset` |

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
