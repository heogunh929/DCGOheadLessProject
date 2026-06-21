# C0110_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 6

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0110_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 28
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT19_040_token` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_040_token.cs` | `None` | `inherited, static_or_continuous` | `-` | 0 |
| `BT19_058` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_058.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `-` | 1 |
| `BT19_059` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_059.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `-` | 1 |
| `BT1_002` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_002.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT1_004` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_004.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT1_008` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_008.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT1_015` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_015.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT1_016` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_016.cs` | `None` | `inherited, static_or_continuous` | `-` | 15 |
| `BT1_026` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_026.cs` | `OnDetermineDoSecurityCheck` | `inherited` | `-` | 1 |
| `BT1_030` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_030.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-002#134@base` | `BT1-002` | 134 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_002.asset` |
| `BT1-004#138@base` | `BT1-004` | 138 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_004.asset` |
| `BT1-008#142@base` | `BT1-008` | 142 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_008.asset` |
| `BT1-015#153@base` | `BT1-015` | 153 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_015.asset` |
| `BT1-015#154@P1` | `BT1-015` | 154 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_015_P1.asset` |
| `BT1-016#155@base` | `BT1-016` | 155 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_016.asset` |
| `BT1-026#171@base` | `BT1-026` | 171 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_026.asset` |
| `BT1-030#180@base` | `BT1-030` | 180 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_030.asset` |
| `BT1-032#182@base` | `BT1-032` | 182 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_032.asset` |
| `BT1-052#209@base` | `BT1-052` | 209 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_052.asset` |
| `BT1-069#237@base` | `BT1-069` | 237 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_069.asset` |
| `BT14-010#2928@base` | `BT14-010` | 2928 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_010.asset` |
| `BT14-045#2969@base` | `BT14-045` | 2969 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_045.asset` |
| `BT14-069#2996@base` | `BT14-069` | 2996 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_069.asset` |
| `BT19-058#4003@base` | `BT19-058` | 4003 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_058.asset` |
| `BT19-059#4001@base` | `BT19-059` | 4001 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_059.asset` |
| `BT3-021#619@base` | `BT3-021` | 619 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_021.asset` |
| `BT3-021#620@P1` | `BT3-021` | 620 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_021_P1.asset` |
| `BT3-021#621@P2` | `BT3-021` | 621 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_021_P2.asset` |
| `BT3-021#622@P3` | `BT3-021` | 622 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_021_P3.asset` |
| `BT3-021#623@P4` | `BT3-021` | 623 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_021_P4.asset` |
| `BT3-079#711@base` | `BT3-079` | 711 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_079.asset` |
| `BT3-081#713@base` | `BT3-081` | 713 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_081.asset` |
| `BT5-075#1038@base` | `BT5-075` | 1038 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_075.asset` |
| `BT6-055#1183@base` | `BT6-055` | 1183 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_055.asset` |
| `EX2-015#1941@base` | `EX2-015` | 1941 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Digimon/EX2_015.asset` |
| `ST15-07#2836@base` | `ST15-07` | 2836 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_07.asset` |
| `ST15-07#4933@P0` | `ST15-07` | 4933 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_07_P0.asset` |

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
