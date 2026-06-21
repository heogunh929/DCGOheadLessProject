# C0121_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 17

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0121_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX5_007` | `DCGO/Assets/Scripts/CardEffect/EX5/Red/EX5_007.cs` | `None, OnDeclaration, OnStartMainPhase` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX5_036` | `DCGO/Assets/Scripts/CardEffect/EX5/Green/EX5_036.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `-` | 1 |
| `EX6_001` | `DCGO/Assets/Scripts/CardEffect/EX6/Red/EX6_001.cs` | `OnAddDigivolutionCards` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX6_012` | `DCGO/Assets/Scripts/CardEffect/EX6/Blue/EX6_012.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `EX7_001` | `DCGO/Assets/Scripts/CardEffect/EX7/Red/EX7_001.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `EX7_003` | `DCGO/Assets/Scripts/CardEffect/EX7/Yellow/EX7_003.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `EX7_004` | `DCGO/Assets/Scripts/CardEffect/EX7/Green/EX7_004.cs` | `OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX7_005` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_005.cs` | `OnAddDigivolutionCards` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX7_031` | `DCGO/Assets/Scripts/CardEffect/EX7/Green/EX7_031.cs` | `None, OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous` | `SelectCard` | 2 |
| `EX7_033` | `DCGO/Assets/Scripts/CardEffect/EX7/Green/EX7_033.cs` | `None, OnDetermineDoSecurityCheck` | `inherited, static_or_continuous` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX5-007#3046@base` | `EX5-007` | 3046 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_007.asset` |
| `EX5-007#4205@P1` | `EX5-007` | 4205 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_007_P1.asset` |
| `EX5-036#3075@base` | `EX5-036` | 3075 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_036.asset` |
| `EX6-001#3435@base` | `EX6-001` | 3435 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Red/DigiEgg/EX6_001.asset` |
| `EX6-001#3436@P1` | `EX6-001` | 3436 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Red/DigiEgg/EX6_001_P1.asset` |
| `EX6-012#3452@base` | `EX6-012` | 3452 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Blue/Digimon/EX6_012.asset` |
| `EX6-012#9156@P1` | `EX6-012` | 9156 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Blue/Digimon/EX6_012_P1.asset` |
| `EX7-001#3677@base` | `EX7-001` | 3677 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/DigiEgg/EX7_001.asset` |
| `EX7-001#3678@P1` | `EX7-001` | 3678 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/DigiEgg/EX7_001_P1.asset` |
| `EX7-003#3681@base` | `EX7-003` | 3681 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/DigiEgg/EX7_003.asset` |
| `EX7-003#3682@P1` | `EX7-003` | 3682 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/DigiEgg/EX7_003_P1.asset` |
| `EX7-004#3683@base` | `EX7-004` | 3683 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/DigiEgg/EX7_004.asset` |
| `EX7-004#3684@P1` | `EX7-004` | 3684 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/DigiEgg/EX7_004_P1.asset` |
| `EX7-005#3685@base` | `EX7-005` | 3685 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/DigiEgg/EX7_005.asset` |
| `EX7-005#3686@P1` | `EX7-005` | 3686 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/DigiEgg/EX7_005_P1.asset` |
| `EX7-031#3734@base` | `EX7-031` | 3734 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_031.asset` |
| `EX7-031#3735@P1` | `EX7-031` | 3735 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_031_P1.asset` |
| `EX7-033#3738@base` | `EX7-033` | 3738 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_033.asset` |
| `EX7-033#3739@P1` | `EX7-033` | 3739 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_033_P1.asset` |

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
