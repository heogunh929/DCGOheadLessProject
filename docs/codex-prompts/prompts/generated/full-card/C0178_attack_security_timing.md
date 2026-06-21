# C0178_attack_security_timing - attack/security timing card porting 51

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0178_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 43
- Source effect count: 1
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST1_06` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 43 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-072#240@base` | `BT1-072` | 240 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_072.asset` |
| `BT1-072#241@P1` | `BT1-072` | 241 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_072_P1.asset` |
| `BT1-072#4272@P2` | `BT1-072` | 4272 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_072_P2.asset` |
| `BT2-054#459@base` | `BT2-054` | 459 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_054.asset` |
| `BT2-072#497@base` | `BT2-072` | 497 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_072.asset` |
| `BT2-072#498@P1` | `BT2-072` | 498 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_072_P1.asset` |
| `BT2-072#499@P2` | `BT2-072` | 499 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_072_P2.asset` |
| `BT4-042#819@base` | `BT4-042` | 819 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_042.asset` |
| `BT4-042#820@P1` | `BT4-042` | 820 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_042_P1.asset` |
| `BT4-067#850@base` | `BT4-067` | 850 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_067.asset` |
| `BT4-067#851@P1` | `BT4-067` | 851 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_067_P1.asset` |
| `BT5-012#950@base` | `BT5-012` | 950 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_012.asset` |
| `BT5-012#951@P1` | `BT5-012` | 951 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_012_P1.asset` |
| `BT5-012#952@P2` | `BT5-012` | 952 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_012_P2.asset` |
| `BT5-026#971@base` | `BT5-026` | 971 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_026.asset` |
| `BT5-026#972@P1` | `BT5-026` | 972 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_026_P1.asset` |
| `BT5-026#973@P2` | `BT5-026` | 973 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_026_P2.asset` |
| `P-014#6033@base` | `P-014` | 6033 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_014.asset` |
| `P-014#6034@P1` | `P-014` | 6034 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_014_P1.asset` |
| `ST1-06#15@base` | `ST1-06` | 15 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_06.asset` |
| `ST1-06#16@P1` | `ST1-06` | 16 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_06_P1.asset` |
| `ST2-07#53@base` | `ST2-07` | 53 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_07.asset` |
| `ST2-07#54@P1` | `ST2-07` | 54 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_07_P1.asset` |
| `ST2-07#55@P2` | `ST2-07` | 55 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_07_P2.asset` |
| `ST3-07#87@base` | `ST3-07` | 87 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_07.asset` |
| `ST3-07#88@P1` | `ST3-07` | 88 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_07_P1.asset` |
| `ST3-07#89@P2` | `ST3-07` | 89 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_07_P2.asset` |
| `ST4-08#117@base` | `ST4-08` | 117 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_08.asset` |
| `ST4-08#118@P1` | `ST4-08` | 118 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_08_P1.asset` |
| `ST4-08#119@P2` | `ST4-08` | 119 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_08_P2.asset` |
| `ST4-08#120@P3` | `ST4-08` | 120 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_08_P3.asset` |
| `ST4-08#121@P4` | `ST4-08` | 121 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_08_P4.asset` |
| `ST4-08#122@P5` | `ST4-08` | 122 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_08_P5.asset` |
| `ST5-08#328@base` | `ST5-08` | 328 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_08.asset` |
| `ST5-08#329@P1` | `ST5-08` | 329 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_08_P1.asset` |
| `ST5-08#4985@P2` | `ST5-08` | 4985 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_08_P2.asset` |
| `ST6-08#347@base` | `ST6-08` | 347 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_08.asset` |
| `ST6-08#348@P1` | `ST6-08` | 348 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_08_P1.asset` |
| `ST6-08#349@P2` | `ST6-08` | 349 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_08_P2.asset` |
| `ST6-08#350@P3` | `ST6-08` | 350 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_08_P3.asset` |
| `ST6-08#4989@P4` | `ST6-08` | 4989 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_08_P4.asset` |
| `ST6-08#4990@P5` | `ST6-08` | 4990 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_08_P5.asset` |
| `ST6-08#4991@P6` | `ST6-08` | 4991 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_08_P6.asset` |

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
