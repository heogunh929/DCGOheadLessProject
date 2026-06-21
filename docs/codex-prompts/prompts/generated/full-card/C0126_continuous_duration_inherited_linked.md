# C0126_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 22

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0126_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 8
- Source effect count: 4
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST7_05` | `DCGO/Assets/Scripts/CardEffect/ST7/Red/ST7_05.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 4 |
| `ST7_10` | `DCGO/Assets/Scripts/CardEffect/ST7/Red/ST7_10.cs` | `None, OnDetermineDoSecurityCheck` | `inherited, static_or_continuous` | `-` | 1 |
| `ST9_01` | `DCGO/Assets/Scripts/CardEffect/ST9/Green/ST9_01.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `ST9_07` | `DCGO/Assets/Scripts/CardEffect/ST9/Green/ST9_07.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST7-05#572@base` | `ST7-05` | 572 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_05.asset` |
| `ST7-05#573@P1` | `ST7-05` | 573 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_05_P1.asset` |
| `ST7-05#9082@P2` | `ST7-05` | 9082 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_05_P2.asset` |
| `ST7-05#9083@P3` | `ST7-05` | 9083 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_05_P3.asset` |
| `ST7-10#583@base` | `ST7-10` | 583 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_10.asset` |
| `ST9-01#1730@base` | `ST9-01` | 1730 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Green/DigiEgg/ST9_01.asset` |
| `ST9-01#4996@P1` | `ST9-01` | 4996 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Green/DigiEgg/ST9_01_P1.asset` |
| `ST9-07#1745@base` | `ST9-07` | 1745 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Green/Digimon/ST9_07.asset` |

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
