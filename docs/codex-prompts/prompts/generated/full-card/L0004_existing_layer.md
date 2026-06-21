# L0004_existing_layer - 기존 layer로 즉시 구현 후보 common layer blocker 4

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `L0004_existing_layer`
- Kind: `mechanic-layer`
- Category: `existing-layer` / 기존 layer로 즉시 구현 후보
- Dependencies: none
- Card identity count: 26
- Source effect count: 0
- Mechanic blocker count: 2

## Mechanic / Layer Items

| Section | Name | Status | Affected cards | Source files |
| --- | --- | --- | ---: | ---: |
| `timings` | `WhenReturntoLibraryAnyone` | `NeedsSourceReview` | 25 | 10 |
| `timings` | `WhenUntapAnyone` | `NeedsSourceReview` | 1 | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-062#2336@base` | `BT11-062` | 2336 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_062.asset` |
| `BT11-062#4414@P0` | `BT11-062` | 4414 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_062_P0.asset` |
| `BT11-064#2338@base` | `BT11-064` | 2338 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_064.asset` |
| `BT20-074#5153@base` | `BT20-074` | 5153 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_074.asset` |
| `BT20-074#8352@P1` | `BT20-074` | 8352 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_074_P1.asset` |
| `BT5-086#1055@base` | `BT5-086` | 1055 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086.asset` |
| `BT5-086#1056@P1` | `BT5-086` | 1056 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P1.asset` |
| `BT5-086#1057@P2` | `BT5-086` | 1057 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P2.asset` |
| `BT5-086#1058@P3` | `BT5-086` | 1058 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P3.asset` |
| `BT5-086#1059@P4` | `BT5-086` | 1059 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P4.asset` |
| `BT5-086#8624@P5` | `BT5-086` | 8624 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P5.asset` |
| `BT5-086#8625@P6` | `BT5-086` | 8625 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P6.asset` |
| `BT5-086#8626@P7` | `BT5-086` | 8626 | `P7` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P7.asset` |
| `BT5-086#8627@P8` | `BT5-086` | 8627 | `P8` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P8.asset` |
| `BT5-086#8628@P9` | `BT5-086` | 8628 | `P9` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P9.asset` |
| `BT7-055#1462@base` | `BT7-055` | 1462 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_055.asset` |
| `BT9-012#1794@base` | `BT9-012` | 1794 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_012.asset` |
| `BT9-012#8952@P1` | `BT9-012` | 8952 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_012_P1.asset` |
| `EX3-013#2185@base` | `EX3-013` | 2185 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_013.asset` |
| `EX3-013#2186@P1` | `EX3-013` | 2186 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_013_P1.asset` |
| `EX4-021#2567@base` | `EX4-021` | 2567 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_021.asset` |
| `EX4-021#2568@P1` | `EX4-021` | 2568 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_021_P1.asset` |
| `EX6-031#3480@base` | `EX6-031` | 3480 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_031.asset` |
| `EX6-031#3481@P1` | `EX6-031` | 3481 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_031_P1.asset` |
| `P-072#10391@P1` | `P-072` | 10391 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_072_P1.asset` |
| `P-072#6114@base` | `P-072` | 6114 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_072.asset` |

## Required Work

- 나열된 mechanic/layer 항목의 DCGO 원본 source 위치와 RL.Engine 공통 layer 대응을 먼저 문서화한다.
- 공통 service/primitive/selection/trigger boundary가 필요한 경우 카드 구현 전에 layer를 먼저 추가한다.
- 이 batch에서 카드별 effect body를 대량 구현하지 말고, layer와 대표 fixture만 검증한다.

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
