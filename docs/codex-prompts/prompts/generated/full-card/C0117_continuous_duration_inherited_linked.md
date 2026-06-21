# C0117_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 13

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0117_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT5_076` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_076.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT5_080` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_080.cs` | `OnDestroyedAnyone` | `inherited` | `-` | 3 |
| `BT6_010` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_010.cs` | `OnDetermineDoSecurityCheck` | `inherited` | `-` | 2 |
| `BT6_013` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_013.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT6_020` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_020.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT6_052` | `DCGO/Assets/Scripts/CardEffect/BT6/Green/BT6_052.cs` | `OnEndBattle` | `max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT6_053` | `DCGO/Assets/Scripts/CardEffect/BT6/Green/BT6_053.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT6_057` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_057.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT6_061` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_061.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT6_062` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_062.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT5-076#1039@base` | `BT5-076` | 1039 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_076.asset` |
| `BT5-080#1043@base` | `BT5-080` | 1043 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_080.asset` |
| `BT5-080#8620@P0` | `BT5-080` | 8620 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_080_P0.asset` |
| `BT6-010#1118@base` | `BT6-010` | 1118 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_010.asset` |
| `BT6-010#8678@P0` | `BT6-010` | 8678 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_010_P0.asset` |
| `BT6-013#1122@base` | `BT6-013` | 1122 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_013.asset` |
| `BT6-020#1135@base` | `BT6-020` | 1135 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_020.asset` |
| `BT6-052#1180@base` | `BT6-052` | 1180 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_052.asset` |
| `BT6-052#8696@P0` | `BT6-052` | 8696 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_052_P0.asset` |
| `BT6-053#1181@base` | `BT6-053` | 1181 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_053.asset` |
| `BT6-057#1186@base` | `BT6-057` | 1186 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_057.asset` |
| `BT6-061#1190@base` | `BT6-061` | 1190 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_061.asset` |
| `BT6-062#1191@base` | `BT6-062` | 1191 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_062.asset` |
| `BT6-062#8702@P0` | `BT6-062` | 8702 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_062_P0.asset` |
| `BT8-076#1658@base` | `BT8-076` | 1658 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_076.asset` |

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
