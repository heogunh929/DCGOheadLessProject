# C0123_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 19

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0123_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `LM_008` | `DCGO/Assets/Scripts/CardEffect/LM/Green/LM_008.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `P_009` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_009.cs` | `None` | `inherited, static_or_continuous` | `-` | 4 |
| `P_033` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_033.cs` | `None, OnDetermineDoSecurityCheck` | `inherited, static_or_continuous` | `-` | 3 |
| `P_046` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_046.cs` | `OnUseOption` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `P_057` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_057.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `P_143` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_143.cs` | `OnDetermineDoSecurityCheck, OnEndTurn` | `inherited, max_count_per_turn, optional, static_or_continuous` | `-` | 3 |
| `P_157` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_157.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `RB1_026` | `DCGO/Assets/Scripts/CardEffect/RB1/Black/RB1_026.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `ST10_07` | `DCGO/Assets/Scripts/CardEffect/ST10/Purple/ST10_07.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `ST12_01` | `DCGO/Assets/Scripts/CardEffect/ST12/Red/ST12_01.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `LM-008#3254@base` | `LM-008` | 3254 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Digimon/LM_008.asset` |
| `P-009#2865@base` | `P-009` | 2865 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Digimon/RB1_004.asset` |
| `P-009#6022@base` | `P-009` | 6022 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_009.asset` |
| `P-009#6023@P1` | `P-009` | 6023 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_009_P1.asset` |
| `P-009#6024@P2` | `P-009` | 6024 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_009_P2.asset` |
| `P-033#6063@base` | `P-033` | 6063 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_033.asset` |
| `P-033#6064@P1` | `P-033` | 6064 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_033_P1.asset` |
| `P-033#6065@P2` | `P-033` | 6065 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_033_P2.asset` |
| `P-046#6087@base` | `P-046` | 6087 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_046.asset` |
| `P-046#6088@P1` | `P-046` | 6088 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_046_P1.asset` |
| `P-057#6099@base` | `P-057` | 6099 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_057.asset` |
| `P-143#10297@base` | `P-143` | 10297 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_143.asset` |
| `P-143#10298@P1` | `P-143` | 10298 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_143_P1.asset` |
| `P-143#9261@P2` | `P-143` | 9261 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_143_P2.asset` |
| `P-157#10461@base` | `P-157` | 10461 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/DigiEgg/P_157.asset` |
| `P-157#10462@P1` | `P-157` | 10462 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/DigiEgg/P_157_P1.asset` |
| `RB1-026#2895@base` | `RB1-026` | 2895 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Black/Digimon/RB1_026.asset` |
| `ST10-07#1765@base` | `ST10-07` | 1765 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Purple/Digimon/ST10_07.asset` |
| `ST12-01#2785@base` | `ST12-01` | 2785 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/DigiEgg/ST12_01.asset` |

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
