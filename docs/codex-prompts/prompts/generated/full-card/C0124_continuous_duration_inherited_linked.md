# C0124_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 20

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0124_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 35
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST12_09` | `DCGO/Assets/Scripts/CardEffect/ST12/Red/ST12_09.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `ST15_11` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_11.cs` | `None` | `inherited, static_or_continuous` | `-` | 3 |
| `ST16_07` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_07.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous` | `-` | 2 |
| `ST16_10` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_10.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `-` | 2 |
| `ST18_02` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_02.cs` | `OnDestroyedAnyone` | `inherited` | `-` | 1 |
| `ST18_07` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_07.cs` | `None, OnDetermineDoSecurityCheck` | `inherited, static_or_continuous` | `-` | 1 |
| `ST1_01` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_01.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `ST1_03` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_03.cs` | `None` | `inherited, static_or_continuous` | `-` | 8 |
| `ST1_07` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_07.cs` | `None` | `inherited, static_or_continuous` | `-` | 9 |
| `ST1_11` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_11.cs` | `None` | `inherited, static_or_continuous` | `-` | 6 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-043#438@base` | `BT2-043` | 438 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_043.asset` |
| `BT4-015#783@base` | `BT4-015` | 783 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_015.asset` |
| `BT4-015#784@P1` | `BT4-015` | 784 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_015_P1.asset` |
| `ST1-01#1@base` | `ST1-01` | 1 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/DigiEgg/ST1_01.asset` |
| `ST1-01#4892@P1` | `ST1-01` | 4892 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/DigiEgg/ST1_01_P1.asset` |
| `ST1-03#10@P4` | `ST1-03` | 10 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_03_P4.asset` |
| `ST1-03#11@P5` | `ST1-03` | 11 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_03_P5.asset` |
| `ST1-03#12@P6` | `ST1-03` | 12 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_03_P6.asset` |
| `ST1-03#6@base` | `ST1-03` | 6 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_03.asset` |
| `ST1-03#7@P1` | `ST1-03` | 7 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_03_P1.asset` |
| `ST1-03#8@P2` | `ST1-03` | 8 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_03_P2.asset` |
| `ST1-03#9@P3` | `ST1-03` | 9 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_03_P3.asset` |
| `ST1-07#17@base` | `ST1-07` | 17 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_07.asset` |
| `ST1-07#18@P1` | `ST1-07` | 18 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_07_P1.asset` |
| `ST1-07#19@P2` | `ST1-07` | 19 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_07_P2.asset` |
| `ST1-07#20@P3` | `ST1-07` | 20 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_07_P3.asset` |
| `ST1-07#4893@P4` | `ST1-07` | 4893 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_07_P4.asset` |
| `ST1-07#4894@P5` | `ST1-07` | 4894 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_07_P5.asset` |
| `ST1-07#4895@P6` | `ST1-07` | 4895 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_07_P6.asset` |
| `ST1-11#28@base` | `ST1-11` | 28 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_11.asset` |
| `ST1-11#29@P1` | `ST1-11` | 29 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_11_P1.asset` |
| `ST1-11#30@P2` | `ST1-11` | 30 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_11_P2.asset` |
| `ST1-11#31@P3` | `ST1-11` | 31 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_11_P3.asset` |
| `ST1-11#4899@P4` | `ST1-11` | 4899 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_11_P4.asset` |
| `ST1-11#4900@P5` | `ST1-11` | 4900 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_11_P5.asset` |
| `ST12-09#2793@base` | `ST12-09` | 2793 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/Digimon/ST12_09.asset` |
| `ST15-11#2840@base` | `ST15-11` | 2840 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_11.asset` |
| `ST15-11#4936@P0` | `ST15-11` | 4936 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_11_P0.asset` |
| `ST15-11#4937@P1` | `ST15-11` | 4937 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_11_P1.asset` |
| `ST16-07#2852@base` | `ST16-07` | 2852 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_07.asset` |
| `ST16-07#4950@P0` | `ST16-07` | 4950 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_07_P0.asset` |
| `ST16-10#2855@base` | `ST16-10` | 2855 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_10.asset` |
| `ST16-10#4952@P0` | `ST16-10` | 4952 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_10_P0.asset` |
| `ST18-02#3819@base` | `ST18-02` | 3819 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_02.asset` |
| `ST18-07#3824@base` | `ST18-07` | 3824 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_07.asset` |

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
