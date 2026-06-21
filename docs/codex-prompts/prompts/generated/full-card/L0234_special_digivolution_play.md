# L0234_special_digivolution_play - special digivolution/play mechanics common layer blocker 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `L0234_special_digivolution_play`
- Kind: `mechanic-layer`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: none
- Card identity count: 89
- Source effect count: 0
- Mechanic blocker count: 8

## Mechanic / Layer Items

| Section | Name | Status | Affected cards | Source files |
| --- | --- | --- | ---: | ---: |
| `keywords` | `Ascension` | `Unsupported` | 8 | 8 |
| `keywords` | `Decode` | `Unsupported` | 8 | 19 |
| `keywords` | `Iceclad` | `Unsupported` | 60 | 18 |
| `keywords` | `MindLink` | `Unsupported` | 58 | 15 |
| `keywords` | `Progress` | `Unsupported` | 72 | 13 |
| `specialMechanics` | `BlastDigivolution` | `Unsupported` | 136 | 57 |
| `specialMechanics` | `DigiBurst` | `PartiallyImplemented` | 84 | 45 |
| `timings` | `OnFaceUpSecurityIncreased` | `NeedsSourceReview` | 2 | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `AD1-005#7824@base` | `AD1-005` | 7824 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005.asset` |
| `AD1-005#7825@P1` | `AD1-005` | 7825 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005_P1.asset` |
| `BT15-049#3178@base` | `BT15-049` | 3178 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_049.asset` |
| `BT15-049#3179@P1` | `BT15-049` | 3179 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_049_P1.asset` |
| `BT15-049#3180@P2` | `BT15-049` | 3180 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_049_P2.asset` |
| `BT16-026#3335@base` | `BT16-026` | 3335 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_026.asset` |
| `BT16-026#3336@P1` | `BT16-026` | 3336 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_026_P1.asset` |
| `BT16-026#3337@P2` | `BT16-026` | 3337 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_026_P2.asset` |
| `BT16-026#8199@P3` | `BT16-026` | 8199 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_026_P3.asset` |
| `BT16-026#8200@P4` | `BT16-026` | 8200 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_026_P4.asset` |
| `BT16-027#3338@base` | `BT16-027` | 3338 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_027.asset` |
| `BT16-027#3339@P1` | `BT16-027` | 3339 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_027_P1.asset` |
| `BT16-027#3340@P2` | `BT16-027` | 3340 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_027_P2.asset` |
| `BT16-027#4784@P0` | `BT16-027` | 4784 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_027_P0.asset` |
| `BT16-027#8201@P3` | `BT16-027` | 8201 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_027_P3.asset` |
| `BT16-046#3360@base` | `BT16-046` | 3360 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_046.asset` |
| `BT16-046#3361@P1` | `BT16-046` | 3361 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_046_P1.asset` |
| `BT16-046#4799@P0` | `BT16-046` | 4799 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_046_P0.asset` |
| `BT16-046#6818@P2` | `BT16-046` | 6818 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_046_P2.asset` |
| `BT17-018#3559@base` | `BT17-018` | 3559 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_018.asset` |
| `BT17-018#3560@P1` | `BT17-018` | 3560 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_018_P1.asset` |
| `BT17-018#3561@P2` | `BT17-018` | 3561 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_018_P2.asset` |
| `BT17-018#8221@P3` | `BT17-018` | 8221 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_018_P3.asset` |
| `BT17-018#8222@P4` | `BT17-018` | 8222 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_018_P4.asset` |
| `BT17-041#3587@base` | `BT17-041` | 3587 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_041.asset` |
| `BT21-025#5334@base` | `BT21-025` | 5334 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_025.asset` |
| `BT21-025#8383@P1` | `BT21-025` | 8383 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_025_P1.asset` |
| `BT21-029#5338@base` | `BT21-029` | 5338 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_029.asset` |
| `BT21-029#5339@P1` | `BT21-029` | 5339 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_029_P1.asset` |
| `BT21-029#5340@P2` | `BT21-029` | 5340 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_029_P2.asset` |
| `BT21-029#8386@P3` | `BT21-029` | 8386 | `P3` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_029_P3.asset` |
| `BT21-029#8387@P4` | `BT21-029` | 8387 | `P4` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_029_P4.asset` |
| `BT21-029#8388@P5` | `BT21-029` | 8388 | `P5` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_029_P5.asset` |
| `BT22-077#7084@base` | `BT22-077` | 7084 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_077.asset` |
| `BT22-077#7085@P1` | `BT22-077` | 7085 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_077_P1.asset` |
| `BT24-017#7537@base` | `BT24-017` | 7537 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_017.asset` |
| `BT24-018#7538@base` | `BT24-018` | 7538 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_018.asset` |
| `BT24-018#7539@P1` | `BT24-018` | 7539 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_018_P1.asset` |
| `BT25-034#8000@base` | `BT25-034` | 8000 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_034.asset` |
| `BT25-040#8006@base` | `BT25-040` | 8006 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_040.asset` |
| `BT25-103#8089@base` | `BT25-103` | 8089 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_103.asset` |
| `BT25-103#8090@P1` | `BT25-103` | 8090 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_103_P1.asset` |
| `BT25-103#8091@P2` | `BT25-103` | 8091 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_103_P2.asset` |
| `BT4-004#762@base` | `BT4-004` | 762 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/DigiEgg/BT4_004.asset` |
| `BT4-004#8497@P0` | `BT4-004` | 8497 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/DigiEgg/BT4_004_P0.asset` |
| `BT4-008#770@base` | `BT4-008` | 770 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_008.asset` |
| `BT4-012#780@base` | `BT4-012` | 780 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_012.asset` |
| `BT4-017#787@base` | `BT4-017` | 787 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_017.asset` |
| `BT4-017#788@P1` | `BT4-017` | 788 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_017_P1.asset` |
| `BT4-017#8506@P2` | `BT4-017` | 8506 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_017_P2.asset` |
| `BT4-017#8507@P3` | `BT4-017` | 8507 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_017_P3.asset` |
| `BT4-017#8508@P4` | `BT4-017` | 8508 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_017_P4.asset` |
| `BT4-017#8509@P5` | `BT4-017` | 8509 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_017_P5.asset` |
| `BT4-019#790@base` | `BT4-019` | 790 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_019.asset` |
| `BT4-019#791@P1` | `BT4-019` | 791 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_019_P1.asset` |
| `BT4-019#8511@P0` | `BT4-019` | 8511 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_019_P0.asset` |
| `BT4-021#793@base` | `BT4-021` | 793 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_021.asset` |
| `BT4-026#799@base` | `BT4-026` | 799 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_026.asset` |
| `BT4-032#806@base` | `BT4-032` | 806 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_032.asset` |
| `BT4-032#8513@P0` | `BT4-032` | 8513 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_032_P0.asset` |
| `BT4-033#807@base` | `BT4-033` | 807 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_033.asset` |
| `BT4-033#808@P1` | `BT4-033` | 808 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_033_P1.asset` |
| `BT4-033#8514@P0` | `BT4-033` | 8514 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_033_P0.asset` |
| `BT4-046#824@base` | `BT4-046` | 824 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_046.asset` |
| `BT4-046#8518@P0` | `BT4-046` | 8518 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_046_P0.asset` |
| `BT4-049#829@base` | `BT4-049` | 829 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_049.asset` |
| `BT4-049#8520@P0` | `BT4-049` | 8520 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_049_P0.asset` |
| `BT4-051#831@base` | `BT4-051` | 831 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_051.asset` |
| `EX11-004#7663@base` | `EX11-004` | 7663 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/DigiEgg/EX11_004.asset` |
| `EX11-004#7664@P1` | `EX11-004` | 7664 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/DigiEgg/EX11_004_P1.asset` |
| `EX11-012#7679@base` | `EX11-012` | 7679 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_012.asset` |
| `EX11-012#7680@P1` | `EX11-012` | 7680 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_012_P1.asset` |
| `EX11-016#7687@base` | `EX11-016` | 7687 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_016.asset` |
| `EX11-016#7688@P1` | `EX11-016` | 7688 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_016_P1.asset` |
| `EX11-017#7689@base` | `EX11-017` | 7689 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_017.asset` |
| `EX11-017#7690@P1` | `EX11-017` | 7690 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_017_P1.asset` |
| `EX11-054#7768@base` | `EX11-054` | 7768 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Red/Tamer/EX11_054.asset` |
| `EX11-054#7769@P1` | `EX11-054` | 7769 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Red/Tamer/EX11_054_P1.asset` |
| `EX11-058#7774@base` | `EX11-058` | 7774 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Tamer/EX11_058.asset` |
| `EX11-058#7775@P1` | `EX11-058` | 7775 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Tamer/EX11_058_P1.asset` |
| `EX7-021#9170@P1` | `EX7-021` | 9170 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_021_P1.asset` |
| `EX7-023#9171@P2` | `EX7-023` | 9171 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_023_P2.asset` |
| `EX7-023#9172@P3` | `EX7-023` | 9172 | `P3` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_023_P3.asset` |
| `EX8-022#9191@P1` | `EX8-022` | 9191 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_022_P1.asset` |
| `EX8-028#9192@P2` | `EX8-028` | 9192 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_028_P2.asset` |
| `LM-040#5441@base` | `LM-040` | 5441 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/LM/Blue/Digimon/LM_040.asset` |
| `P-189#6979@base` | `P-189` | 6979 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_189.asset` |
| `P-189#6980@P1` | `P-189` | 6980 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_189_P1.asset` |
| `P-189#9304@P2` | `P-189` | 9304 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_189_P2.asset` |

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
