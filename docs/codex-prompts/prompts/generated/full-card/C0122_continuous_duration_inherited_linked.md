# C0122_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 18

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0122_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX7_038` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_038.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `EX7_041` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_041.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `EX7_058_token` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_058_token.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `-` | 0 |
| `EX8_005` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_005.cs` | `OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX8_008` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_008.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX8_013` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_013.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `EX8_037_token` | `DCGO/Assets/Scripts/CardEffect/EX8/Yellow/EX8_037_token.cs` | `None` | `inherited, static_or_continuous` | `-` | 0 |
| `EX9_016` | `DCGO/Assets/Scripts/CardEffect/EX9/Blue/EX9_016.cs` | `None, OnDeclaration` | `inherited, static_or_continuous` | `-` | 2 |
| `EX9_022` | `DCGO/Assets/Scripts/CardEffect/EX9/Yellow/EX9_022.cs` | `None, OnDeclaration` | `inherited, static_or_continuous` | `-` | 2 |
| `EX9_034` | `DCGO/Assets/Scripts/CardEffect/EX9/Green/EX9_034.cs` | `None, OnDeclaration, OnDetermineDoSecurityCheck` | `inherited, static_or_continuous` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX7-038#3748@base` | `EX7-038` | 3748 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_038.asset` |
| `EX7-038#3749@P1` | `EX7-038` | 3749 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_038_P1.asset` |
| `EX7-041#3753@base` | `EX7-041` | 3753 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_041.asset` |
| `EX7-041#3754@P1` | `EX7-041` | 3754 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_041_P1.asset` |
| `EX8-005#4057@base` | `EX8-005` | 4057 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/DigiEgg/EX8_005.asset` |
| `EX8-005#4058@P1` | `EX8-005` | 4058 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/DigiEgg/EX8_005_P1.asset` |
| `EX8-008#4063@base` | `EX8-008` | 4063 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_008.asset` |
| `EX8-008#4064@P1` | `EX8-008` | 4064 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_008_P1.asset` |
| `EX8-013#4072@base` | `EX8-013` | 4072 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_013.asset` |
| `EX8-013#4073@P1` | `EX8-013` | 4073 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_013_P1.asset` |
| `EX9-016#6863@base` | `EX9-016` | 6863 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_016.asset` |
| `EX9-016#6864@P1` | `EX9-016` | 6864 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_016_P1.asset` |
| `EX9-022#6876@base` | `EX9-022` | 6876 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_022.asset` |
| `EX9-022#6877@P1` | `EX9-022` | 6877 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_022_P1.asset` |
| `EX9-034#6900@base` | `EX9-034` | 6900 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_034.asset` |
| `EX9-034#6901@P1` | `EX9-034` | 6901 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_034_P1.asset` |

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
