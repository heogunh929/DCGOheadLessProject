# L0006_zone_security_recovery - zone/security/recovery common layer blocker 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `L0006_zone_security_recovery`
- Kind: `mechanic-layer`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: none
- Card identity count: 98
- Source effect count: 0
- Mechanic blocker count: 8

## Mechanic / Layer Items

| Section | Name | Status | Affected cards | Source files |
| --- | --- | --- | ---: | ---: |
| `timings` | `OnAddHand` | `NeedsSourceReview` | 49 | 22 |
| `timings` | `OnDigivolutionCardReturnToDeckBottom` | `NeedsSourceReview` | 4 | 4 |
| `timings` | `OnDiscardHand` | `NeedsSourceReview` | 59 | 35 |
| `timings` | `OnPermamemtReturnedToHand` | `NeedsSourceReview` | 5 | 3 |
| `timings` | `OnReturnCardsToHandFromTrash` | `NeedsSourceReview` | 6 | 3 |
| `timings` | `OnReturnCardsToLibraryFromTrash` | `NeedsSourceReview` | 2 | 2 |
| `timings` | `WhenReturntoHandAnyone` | `NeedsSourceReview` | 25 | 10 |
| `timings` | `WhenTopCardTrashed` | `NeedsSourceReview` | 6 | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT10-077#2123@base` | `BT10-077` | 2123 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_077.asset` |
| `BT10-080#2126@base` | `BT10-080` | 2126 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_080.asset` |
| `BT10-080#4338@P0` | `BT10-080` | 4338 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_080_P0.asset` |
| `BT11-006#2273@base` | `BT11-006` | 2273 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/DigiEgg/BT11_006.asset` |
| `BT11-028#2298@base` | `BT11-028` | 2298 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_028.asset` |
| `BT11-028#4391@P0` | `BT11-028` | 4391 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_028_P0.asset` |
| `BT11-033#2304@base` | `BT11-033` | 2304 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_033.asset` |
| `BT11-033#2305@P1` | `BT11-033` | 2305 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_033_P1.asset` |
| `BT11-062#2336@base` | `BT11-062` | 2336 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_062.asset` |
| `BT11-062#4414@P0` | `BT11-062` | 4414 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_062_P0.asset` |
| `BT11-064#2338@base` | `BT11-064` | 2338 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_064.asset` |
| `BT11-065#2339@base` | `BT11-065` | 2339 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_065.asset` |
| `BT11-065#4415@P0` | `BT11-065` | 4415 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_065_P0.asset` |
| `BT11-081#2356@base` | `BT11-081` | 2356 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_081.asset` |
| `BT11-090#2369@base` | `BT11-090` | 2369 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_090.asset` |
| `BT11-090#2370@P1` | `BT11-090` | 2370 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_090_P1.asset` |
| `BT11-090#4434@P0` | `BT11-090` | 4434 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_090_P0.asset` |
| `BT13-029#2676@base` | `BT13-029` | 2676 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_029.asset` |
| `BT13-029#4574@P0` | `BT13-029` | 4574 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_029_P0.asset` |
| `BT13-031#2679@base` | `BT13-031` | 2679 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_031.asset` |
| `BT13-031#4576@P0` | `BT13-031` | 4576 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_031_P0.asset` |
| `BT13-084#2743@base` | `BT13-084` | 2743 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_084.asset` |
| `BT14-006#2922@base` | `BT14-006` | 2922 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/DigiEgg/BT14_006.asset` |
| `BT14-006#2923@P1` | `BT14-006` | 2923 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/DigiEgg/BT14_006_P1.asset` |
| `BT14-006#4637@P0` | `BT14-006` | 4637 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/DigiEgg/BT14_006_P0.asset` |
| `BT14-006#8166@P2` | `BT14-006` | 8166 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/DigiEgg/BT14_006_P2.asset` |
| `BT14-030#2951@base` | `BT14-030` | 2951 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_030.asset` |
| `BT14-030#4649@P0` | `BT14-030` | 4649 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_030_P0.asset` |
| `BT14-070#2997@base` | `BT14-070` | 2997 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_070.asset` |
| `BT14-073#3001@base` | `BT14-073` | 3001 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_073.asset` |
| `BT14-100#3035@base` | `BT14-100` | 3035 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Option/BT14_100.asset` |
| `BT15-002#3116@base` | `BT15-002` | 3116 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/DigiEgg/BT15_002.asset` |
| `BT15-002#3117@P1` | `BT15-002` | 3117 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/DigiEgg/BT15_002_P1.asset` |
| `BT15-002#4709@P0` | `BT15-002` | 4709 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/DigiEgg/BT15_002_P0.asset` |
| `BT15-026#3149@base` | `BT15-026` | 3149 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_026.asset` |
| `BT15-026#3150@P1` | `BT15-026` | 3150 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_026_P1.asset` |
| `BT15-026#3151@P2` | `BT15-026` | 3151 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_026_P2.asset` |
| `BT15-026#8184@P3` | `BT15-026` | 8184 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_026_P3.asset` |
| `BT15-082#3217@base` | `BT15-082` | 3217 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Tamer/BT15_082.asset` |
| `BT15-082#3218@P1` | `BT15-082` | 3218 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Tamer/BT15_082_P1.asset` |
| `BT15-082#4756@P0` | `BT15-082` | 4756 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Tamer/BT15_082_P0.asset` |
| `BT15-082#4757@P2` | `BT15-082` | 4757 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Tamer/BT15_082_P2.asset` |
| `BT15-083#3219@base` | `BT15-083` | 3219 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Tamer/BT15_083.asset` |
| `BT15-083#3220@P1` | `BT15-083` | 3220 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Tamer/BT15_083_P1.asset` |
| `BT15-083#4758@P0` | `BT15-083` | 4758 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Tamer/BT15_083_P0.asset` |
| `BT16-011#3316@base` | `BT16-011` | 3316 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_011.asset` |
| `BT16-011#4779@P0` | `BT16-011` | 4779 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_011_P0.asset` |
| `BT17-028#3571@base` | `BT17-028` | 3571 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_028.asset` |
| `BT17-028#3572@P1` | `BT17-028` | 3572 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_028_P1.asset` |
| `BT17-099#3668@base` | `BT17-099` | 3668 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Option/BT17_099.asset` |
| `BT17-099#4890@P1` | `BT17-099` | 4890 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Option/BT17_099_P1.asset` |
| `BT17-099#8247@P2` | `BT17-099` | 8247 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Option/BT17_099_P2.asset` |
| `BT18-065#3925@base` | `BT18-065` | 3925 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_065.asset` |
| `BT20-074#5153@base` | `BT20-074` | 5153 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_074.asset` |
| `BT20-074#8352@P1` | `BT20-074` | 8352 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_074_P1.asset` |
| `BT21-058#5373@base` | `BT21-058` | 5373 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_058.asset` |
| `BT21-094#5421@base` | `BT21-094` | 5421 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Option/BT21_094.asset` |
| `BT24-007#7525@base` | `BT24-007` | 7525 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/DigiEgg/BT24_007.asset` |
| `BT24-009#7528@base` | `BT24-009` | 7528 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_009.asset` |
| `BT24-013#7532@base` | `BT24-013` | 7532 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_013.asset` |
| `BT24-021#7543@base` | `BT24-021` | 7543 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_021.asset` |
| `BT24-026#7548@base` | `BT24-026` | 7548 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_026.asset` |
| `BT24-042#7570@base` | `BT24-042` | 7570 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_042.asset` |
| `BT24-045#7574@base` | `BT24-045` | 7574 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_045.asset` |
| `BT25-080#8055@base` | `BT25-080` | 8055 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_080.asset` |
| `BT25-084#8059@base` | `BT25-084` | 8059 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_084.asset` |
| `BT25-084#8060@P1` | `BT25-084` | 8060 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_084_P1.asset` |
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
| `BT6-006#1109@base` | `BT6-006` | 1109 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/DigiEgg/BT6_006.asset` |
| `BT6-006#1110@P1` | `BT6-006` | 1110 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_006_P1.asset` |
| `BT6-006#8672@P1` | `BT6-006` | 8672 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/DigiEgg/BT6_006_P1.asset` |
| `BT6-006#8673@P0` | `BT6-006` | 8673 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/DigiEgg/BT6_006_P0.asset` |
| `BT8-110#1703@base` | `BT8-110` | 1703 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Option/BT8_110.asset` |
| `BT8-110#6811@P1` | `BT8-110` | 6811 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Option/BT8_110_P1.asset` |
| `BT9-012#1794@base` | `BT9-012` | 1794 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_012.asset` |
| `BT9-012#8952@P1` | `BT9-012` | 8952 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_012_P1.asset` |
| `EX10-010#7149@base` | `EX10-010` | 7149 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_010.asset` |
| `EX10-010#7277@P1` | `EX10-010` | 7277 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_010_P1.asset` |
| `EX10-010#7278@P2` | `EX10-010` | 7278 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_010_P2.asset` |
| `EX3-013#2185@base` | `EX3-013` | 2185 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_013.asset` |
| `EX3-013#2186@P1` | `EX3-013` | 2186 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_013_P1.asset` |
| `EX4-021#2567@base` | `EX4-021` | 2567 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_021.asset` |
| `EX4-021#2568@P1` | `EX4-021` | 2568 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_021_P1.asset` |
| `EX6-031#3480@base` | `EX6-031` | 3480 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_031.asset` |
| `EX6-031#3481@P1` | `EX6-031` | 3481 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_031_P1.asset` |
| `P-048#10361@P1` | `P-048` | 10361 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_048_P1.asset` |
| `P-048#6090@base` | `P-048` | 6090 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_048.asset` |
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
