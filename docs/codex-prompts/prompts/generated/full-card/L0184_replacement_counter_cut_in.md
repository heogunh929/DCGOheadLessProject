# L0184_replacement_counter_cut_in - replacement/counter/cut-in common layer blocker 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `L0184_replacement_counter_cut_in`
- Kind: `mechanic-layer`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: none
- Card identity count: 97
- Source effect count: 0
- Mechanic blocker count: 8

## Mechanic / Layer Items

| Section | Name | Status | Affected cards | Source files |
| --- | --- | --- | ---: | ---: |
| `keywords` | `ArmorPurge` | `Unsupported` | 192 | 48 |
| `keywords` | `Barrier` | `Unsupported` | 388 | 61 |
| `keywords` | `Evade` | `Unsupported` | 140 | 26 |
| `keywords` | `Fortitude` | `Unsupported` | 152 | 35 |
| `keywords` | `MaterialSave` | `Unsupported` | 0 | 15 |
| `keywords` | `Partition` | `Unsupported` | 0 | 18 |
| `timings` | `WhenPermanentWouldBeDeleted` | `NeedsSourceReview` | 405 | 210 |
| `timings` | `WhenWouldDigivolutionCardDiscarded` | `NeedsSourceReview` | 1 | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `AD1-012#7836@base` | `AD1-012` | 7836 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012.asset` |
| `AD1-012#7837@P1` | `AD1-012` | 7837 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012_P1.asset` |
| `AD1-014#7839@base` | `AD1-014` | 7839 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014.asset` |
| `AD1-014#7840@P1` | `AD1-014` | 7840 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014_P1.asset` |
| `BT10-009#2041@base` | `BT10-009` | 2041 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_009.asset` |
| `BT10-009#4292@P0` | `BT10-009` | 4292 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_009_P0.asset` |
| `BT10-012#2045@base` | `BT10-012` | 2045 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_012.asset` |
| `BT10-012#4297@P0` | `BT10-012` | 4297 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_012_P0.asset` |
| `BT10-013#2046@base` | `BT10-013` | 2046 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_013.asset` |
| `BT10-013#2047@P1` | `BT10-013` | 2047 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_013_P1.asset` |
| `BT10-015#2049@base` | `BT10-015` | 2049 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_015.asset` |
| `BT10-015#4299@P0` | `BT10-015` | 4299 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_015_P0.asset` |
| `BT10-024#2060@base` | `BT10-024` | 2060 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_024.asset` |
| `BT10-024#2061@P1` | `BT10-024` | 2061 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_024_P1.asset` |
| `BT10-024#8096@P2` | `BT10-024` | 8096 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_024_P2.asset` |
| `BT10-026#2063@base` | `BT10-026` | 2063 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_026.asset` |
| `BT10-026#2064@P1` | `BT10-026` | 2064 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_026_P1.asset` |
| `BT10-026#4307@P0` | `BT10-026` | 4307 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_026_P0.asset` |
| `BT10-066#2110@base` | `BT10-066` | 2110 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_066.asset` |
| `BT10-066#4330@P0` | `BT10-066` | 4330 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_066_P0.asset` |
| `BT10-066#4331@P1` | `BT10-066` | 4331 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_066_P1.asset` |
| `BT10-074#2120@base` | `BT10-074` | 2120 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_074.asset` |
| `BT10-084#2132@base` | `BT10-084` | 2132 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_084.asset` |
| `BT10-111#2167@base` | `BT10-111` | 2167 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_111.asset` |
| `BT10-111#2168@P1` | `BT10-111` | 2168 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_111_P1.asset` |
| `BT11-009#2276@base` | `BT11-009` | 2276 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_009.asset` |
| `BT11-112#2396@base` | `BT11-112` | 2396 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_112.asset` |
| `BT11-112#2397@P1` | `BT11-112` | 2397 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_112_P1.asset` |
| `BT11-112#8114@P2` | `BT11-112` | 8114 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_112_P2.asset` |
| `BT13-023#2670@base` | `BT13-023` | 2670 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_023.asset` |
| `BT13-031#2679@base` | `BT13-031` | 2679 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_031.asset` |
| `BT13-031#4576@P0` | `BT13-031` | 4576 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_031_P0.asset` |
| `BT13-041#2692@base` | `BT13-041` | 2692 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_041.asset` |
| `BT13-041#4585@P0` | `BT13-041` | 4585 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_041_P0.asset` |
| `BT13-043#2694@base` | `BT13-043` | 2694 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_043.asset` |
| `BT14-021#2941@base` | `BT14-021` | 2941 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_021.asset` |
| `BT14-025#2945@base` | `BT14-025` | 2945 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_025.asset` |
| `BT14-035#2957@base` | `BT14-035` | 2957 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_035.asset` |
| `BT15-101#8187@P2` | `BT15-101` | 8187 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_101_P2.asset` |
| `BT15-101#8188@P3` | `BT15-101` | 8188 | `P3` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_101_P3.asset` |
| `BT15-101#8189@P4` | `BT15-101` | 8189 | `P4` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_101_P4.asset` |
| `BT16-031#3344@base` | `BT16-031` | 3344 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_031.asset` |
| `BT16-031#4791@P0` | `BT16-031` | 4791 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_031_P0.asset` |
| `BT16-031#8204@P1` | `BT16-031` | 8204 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_031_P1.asset` |
| `BT16-035#3348@base` | `BT16-035` | 3348 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_035.asset` |
| `BT16-035#4792@P0` | `BT16-035` | 4792 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_035_P0.asset` |
| `BT16-036#3349@base` | `BT16-036` | 3349 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_036.asset` |
| `BT16-036#3350@P1` | `BT16-036` | 3350 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_036_P1.asset` |
| `BT16-053#3370@base` | `BT16-053` | 3370 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_053.asset` |
| `BT16-086#3411@base` | `BT16-086` | 3411 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Tamer/BT16_086.asset` |
| `BT16-086#4818@P0` | `BT16-086` | 4818 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Tamer/BT16_086_P0.asset` |
| `BT16-086#4819@P1` | `BT16-086` | 4819 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Tamer/BT16_086_P1.asset` |
| `BT17-035#3580@base` | `BT17-035` | 3580 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_035.asset` |
| `BT17-035#8228@P1` | `BT17-035` | 8228 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_035_P1.asset` |
| `BT17-035#8229@P2` | `BT17-035` | 8229 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_035_P2.asset` |
| `BT17-038#3583@base` | `BT17-038` | 3583 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_038.asset` |
| `BT17-038#4851@P0` | `BT17-038` | 4851 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_038_P0.asset` |
| `BT17-038#8230@P1` | `BT17-038` | 8230 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_038_P1.asset` |
| `BT17-086#3649@base` | `BT17-086` | 3649 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_086.asset` |
| `BT17-086#3650@P1` | `BT17-086` | 3650 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_086_P1.asset` |
| `BT17-086#4873@P0` | `BT17-086` | 4873 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_086_P0.asset` |
| `BT18-039#3887@base` | `BT18-039` | 3887 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_039.asset` |
| `BT19-018#3982@base` | `BT19-018` | 3982 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_018.asset` |
| `BT20-034#5113@base` | `BT20-034` | 5113 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_034.asset` |
| `BT20-035#5114@base` | `BT20-035` | 5114 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_035.asset` |
| `BT20-045#5124@base` | `BT20-045` | 5124 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_045.asset` |
| `BT20-045#5215@P1` | `BT20-045` | 5215 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_045_P1.asset` |
| `BT20-045#5216@P2` | `BT20-045` | 5216 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_045_P2.asset` |
| `BT21-038#5349@base` | `BT21-038` | 5349 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_038.asset` |
| `BT21-052#5365@base` | `BT21-052` | 5365 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Green/Digimon/BT21_052.asset` |
| `BT21-052#5366@P1` | `BT21-052` | 5366 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Green/Digimon/BT21_052_P1.asset` |
| `BT22-051#7053@base` | `BT22-051` | 7053 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_051.asset` |
| `BT23-024#7356@base` | `BT23-024` | 7356 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Digimon/BT23_024.asset` |
| `BT23-046#7379@base` | `BT23-046` | 7379 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_046.asset` |
| `BT24-038#7564@base` | `BT24-038` | 7564 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_038.asset` |
| `BT24-049#7578@base` | `BT24-049` | 7578 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_049.asset` |
| `BT24-050#7579@base` | `BT24-050` | 7579 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_050.asset` |
| `BT25-015#7977@base` | `BT25-015` | 7977 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_015.asset` |
| `BT25-029#7994@base` | `BT25-029` | 7994 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Digimon/BT25_029.asset` |
| `BT25-058#8029@base` | `BT25-058` | 8029 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_058.asset` |
| `EX10-018#7164@base` | `EX10-018` | 7164 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_018.asset` |
| `EX10-019#7165@base` | `EX10-019` | 7165 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_019.asset` |
| `EX10-019#7284@P1` | `EX10-019` | 7284 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_019_P1.asset` |
| `EX10-022#7171@base` | `EX10-022` | 7171 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_022.asset` |
| `EX10-022#7287@P1` | `EX10-022` | 7287 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_022_P1.asset` |
| `EX11-010#7675@base` | `EX11-010` | 7675 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_010.asset` |
| `EX11-010#7676@P1` | `EX11-010` | 7676 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_010_P1.asset` |
| `EX11-011#7677@base` | `EX11-011` | 7677 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_011.asset` |
| `EX11-011#7678@P1` | `EX11-011` | 7678 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_011_P1.asset` |
| `EX5-032#3071@base` | `EX5-032` | 3071 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_032.asset` |
| `EX5-035#3074@base` | `EX5-035` | 3074 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_035.asset` |
| `EX5-036#3075@base` | `EX5-036` | 3075 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_036.asset` |
| `EX5-039#3078@base` | `EX5-039` | 3078 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_039.asset` |
| `EX5-042#3081@base` | `EX5-042` | 3081 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_042.asset` |
| `EX5-042#4229@P1` | `EX5-042` | 4229 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_042_P1.asset` |
| `EX5-049#3088@base` | `EX5-049` | 3088 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_049.asset` |
| `EX5-055#3094@base` | `EX5-055` | 3094 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_055.asset` |

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
