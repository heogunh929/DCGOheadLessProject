# C0120_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 16

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0120_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 11
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX2_002` | `DCGO/Assets/Scripts/CardEffect/EX2/Blue/EX2_002.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `EX2_005` | `DCGO/Assets/Scripts/CardEffect/EX2/Black/EX2_005.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `EX2_026` | `DCGO/Assets/Scripts/CardEffect/EX2/Green/EX2_026.cs` | `None, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `SelectCard` | 1 |
| `EX2_034` | `DCGO/Assets/Scripts/CardEffect/EX2/Black/EX2_034.cs` | `None` | `inherited, modifier_duration, static_or_continuous` | `-` | 1 |
| `EX3_002` | `DCGO/Assets/Scripts/CardEffect/EX3/Black/EX3_002.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `EX3_016` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_016.cs` | `None` | `inherited, static_or_continuous` | `SelectCard` | 1 |
| `EX3_050` | `DCGO/Assets/Scripts/CardEffect/EX3/Black/EX3_050.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `EX3_060` | `DCGO/Assets/Scripts/CardEffect/EX3/Purple/EX3_060.cs` | `None` | `inherited, modifier_duration, static_or_continuous` | `-` | 1 |
| `EX4_004` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_004.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `EX5_003` | `DCGO/Assets/Scripts/CardEffect/EX5/Green/EX5_003.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX2-002#1916@base` | `EX2-002` | 1916 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/DigiEgg/EX2_002.asset` |
| `EX2-005#1921@base` | `EX2-005` | 1921 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/DigiEgg/EX2_005.asset` |
| `EX2-026#1960@base` | `EX2-026` | 1960 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Digimon/EX2_026.asset` |
| `EX2-034#1971@base` | `EX2-034` | 1971 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Digimon/EX2_034.asset` |
| `EX3-002#2172@base` | `EX3-002` | 2172 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/DigiEgg/EX3_002.asset` |
| `EX3-016#2190@base` | `EX3-016` | 2190 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_016.asset` |
| `EX3-050#2235@base` | `EX3-050` | 2235 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Digimon/EX3_050.asset` |
| `EX3-060#2247@base` | `EX3-060` | 2247 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_060.asset` |
| `EX4-004#2547@base` | `EX4-004` | 2547 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/DigiEgg/EX4_004.asset` |
| `EX5-003#3042@base` | `EX5-003` | 3042 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/DigiEgg/EX5_003.asset` |
| `EX5-003#4201@P1` | `EX5-003` | 4201 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/DigiEgg/EX5_003_P1.asset` |

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
