# L0235_special_digivolution_play - special digivolution/play mechanics common layer blocker 2

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `L0235_special_digivolution_play`
- Kind: `mechanic-layer`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: none
- Card identity count: 40
- Source effect count: 0
- Mechanic blocker count: 3

## Mechanic / Layer Items

| Section | Name | Status | Affected cards | Source files |
| --- | --- | --- | ---: | ---: |
| `timings` | `OnLinkCardDiscarded` | `NeedsSourceReview` | 14 | 8 |
| `timings` | `OnUseDigiburst` | `NeedsSourceReview` | 1 | 2 |
| `timings` | `WhenLinked` | `NeedsSourceReview` | 87 | 67 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT21-005#5311@base` | `BT21-005` | 5311 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Green/DigiEgg/BT21_005.asset` |
| `BT21-009#5316@base` | `BT21-009` | 5316 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_009.asset` |
| `BT21-009#5317@P1` | `BT21-009` | 5317 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_009_P1.asset` |
| `BT21-018#5326@base` | `BT21-018` | 5326 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_018.asset` |
| `BT21-023#5331@base` | `BT21-023` | 5331 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_023.asset` |
| `BT21-023#5332@P1` | `BT21-023` | 5332 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_023_P1.asset` |
| `BT21-043#5354@base` | `BT21-043` | 5354 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_043.asset` |
| `BT21-053#5367@base` | `BT21-053` | 5367 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_053.asset` |
| `BT21-054#5368@base` | `BT21-054` | 5368 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_054.asset` |
| `BT21-059#5374@base` | `BT21-059` | 5374 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_059.asset` |
| `BT21-070#5387@base` | `BT21-070` | 5387 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_070.asset` |
| `BT21-071#5388@base` | `BT21-071` | 5388 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_071.asset` |
| `BT21-073#5390@base` | `BT21-073` | 5390 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_073.asset` |
| `BT21-074#5391@base` | `BT21-074` | 5391 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_074.asset` |
| `BT21-084#5405@base` | `BT21-084` | 5405 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_084.asset` |
| `BT21-084#5406@P1` | `BT21-084` | 5406 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_084_P1.asset` |
| `BT21-101#5428@base` | `BT21-101` | 5428 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/White/Digimon/BT21_101.asset` |
| `BT21-101#5429@P1` | `BT21-101` | 5429 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/White/Digimon/BT21_101_P1.asset` |
| `BT21-101#5430@P2` | `BT21-101` | 5430 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/White/Digimon/BT21_101_P2.asset` |
| `BT21-101#8419@P3` | `BT21-101` | 8419 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT21/White/Digimon/BT21_101_P3.asset` |
| `BT21-101#8420@P4` | `BT21-101` | 8420 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT21/White/Digimon/BT21_101_P4.asset` |
| `BT22-003#6991@base` | `BT22-003` | 6991 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/DigiEgg/BT22_003.asset` |
| `BT22-009#6998@base` | `BT22-009` | 6998 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Digimon/BT22_009.asset` |
| `BT22-016#7008@base` | `BT22-016` | 7008 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_016.asset` |
| `BT22-030#7027@base` | `BT22-030` | 7027 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_030.asset` |
| `BT5-056#1014@base` | `BT5-056` | 1014 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_056.asset` |
| `EX10-001#7132@base` | `EX10-001` | 7132 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/DigiEgg/EX10_001.asset` |
| `EX10-001#7269@P1` | `EX10-001` | 7269 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/DigiEgg/EX10_001_P1.asset` |
| `EX10-030#7187@base` | `EX10-030` | 7187 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_030.asset` |
| `EX10-030#7295@P1` | `EX10-030` | 7295 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_030_P1.asset` |
| `EX10-043#7211@base` | `EX10-043` | 7211 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_043.asset` |
| `EX10-043#7306@P1` | `EX10-043` | 7306 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_043_P1.asset` |
| `EX10-062#7243@base` | `EX10-062` | 7243 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Tamer/EX10_062.asset` |
| `EX10-062#7319@P1` | `EX10-062` | 7319 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Tamer/EX10_062_P1.asset` |
| `EX10-070#7259@base` | `EX10-070` | 7259 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Option/EX10_070.asset` |
| `EX10-070#7327@P1` | `EX10-070` | 7327 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Option/EX10_070_P1.asset` |
| `EX10-073#7265@base` | `EX10-073` | 7265 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_073.asset` |
| `EX10-073#7330@P1` | `EX10-073` | 7330 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_073_P1.asset` |
| `P-234#7900@base` | `P-234` | 7900 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Tamer/P_234.asset` |
| `P-234#7901@P1` | `P-234` | 7901 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Tamer/P_234_P1.asset` |

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
