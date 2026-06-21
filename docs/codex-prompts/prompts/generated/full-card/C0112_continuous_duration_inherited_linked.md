# C0112_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 8

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0112_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT21_004` | `DCGO/Assets/Scripts/CardEffect/BT21/Yellow/BT21_004.cs` | `OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `BT21_034` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_034.cs` | `None, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT24_019` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_019.cs` | `None` | `inherited, static_or_continuous` | `SelectCard` | 1 |
| `BT2_002` | `DCGO/Assets/Scripts/CardEffect/BT2/Blue/BT2_002.cs` | `OnUnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT2_003` | `DCGO/Assets/Scripts/CardEffect/BT2/Yellow/BT2_003.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT2_004` | `DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_004.cs` | `OnUnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 7 |
| `BT2_005` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_005.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT2_010` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_010.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT2_026` | `DCGO/Assets/Scripts/CardEffect/BT2/Blue/BT2_026.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT2_029` | `DCGO/Assets/Scripts/CardEffect/BT2/Blue/BT2_029.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-002#362@base` | `BT2-002` | 362 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/DigiEgg/BT2_002.asset` |
| `BT2-003#363@base` | `BT2-003` | 363 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/DigiEgg/BT2_003.asset` |
| `BT2-004#364@base` | `BT2-004` | 364 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/DigiEgg/BT2_004.asset` |
| `BT2-004#365@P1` | `BT2-004` | 365 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_004_P1.asset` |
| `BT2-004#366@P2` | `BT2-004` | 366 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_004_P2.asset` |
| `BT2-004#367@P3` | `BT2-004` | 367 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_004_P3.asset` |
| `BT2-004#8301@P1` | `BT2-004` | 8301 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/DigiEgg/BT2_004_P1.asset` |
| `BT2-004#8302@P2` | `BT2-004` | 8302 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/DigiEgg/BT2_004_P2.asset` |
| `BT2-004#8303@P3` | `BT2-004` | 8303 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/DigiEgg/BT2_004_P3.asset` |
| `BT2-005#368@base` | `BT2-005` | 368 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/DigiEgg/BT2_005.asset` |
| `BT2-010#373@base` | `BT2-010` | 373 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_010.asset` |
| `BT2-010#374@P1` | `BT2-010` | 374 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_010_P1.asset` |
| `BT2-026#403@base` | `BT2-026` | 403 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_026.asset` |
| `BT2-026#404@P1` | `BT2-026` | 404 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_026_P1.asset` |
| `BT2-029#410@base` | `BT2-029` | 410 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_029.asset` |
| `BT21-004#5310@base` | `BT21-004` | 5310 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/DigiEgg/BT21_004.asset` |
| `BT21-004#8370@P1` | `BT21-004` | 8370 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/DigiEgg/BT21_004_P1.asset` |
| `BT21-004#8371@P2` | `BT21-004` | 8371 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/DigiEgg/BT21_004_P2.asset` |
| `BT21-034#5345@base` | `BT21-034` | 5345 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_034.asset` |
| `BT24-019#7540@base` | `BT24-019` | 7540 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_019.asset` |

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
