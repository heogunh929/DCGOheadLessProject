# L0003_existing_layer - 기존 layer로 즉시 구현 후보 common layer blocker 3

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `L0003_existing_layer`
- Kind: `mechanic-layer`
- Category: `existing-layer` / 기존 layer로 즉시 구현 후보
- Dependencies: none
- Card identity count: 142
- Source effect count: 0
- Mechanic blocker count: 8

## Mechanic / Layer Items

| Section | Name | Status | Affected cards | Source files |
| --- | --- | --- | ---: | ---: |
| `timings` | `OnMove` | `NeedsSourceReview` | 79 | 32 |
| `timings` | `OnRemovedField` | `NeedsSourceReview` | 4 | 3 |
| `timings` | `OnStartBattle` | `NeedsSourceReview` | 0 | 1 |
| `timings` | `OnTappedAnyone` | `NeedsSourceReview` | 306 | 140 |
| `timings` | `OnUnTappedAnyone` | `NeedsSourceReview` | 70 | 30 |
| `timings` | `OnUseOption` | `NeedsSourceReview` | 65 | 31 |
| `timings` | `WhenDigisorption` | `NeedsSourceReview` | 15 | 10 |
| `timings` | `WhenRemoveField` | `NeedsSourceReview` | 304 | 167 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `AD1-003#7821@base` | `AD1-003` | 7821 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_003.asset` |
| `AD1-006#7826@base` | `AD1-006` | 7826 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006.asset` |
| `AD1-006#7827@P1` | `AD1-006` | 7827 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006_P1.asset` |
| `AD1-011#7835@base` | `AD1-011` | 7835 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_011.asset` |
| `AD1-013#7838@base` | `AD1-013` | 7838 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_013.asset` |
| `AD1-014#7839@base` | `AD1-014` | 7839 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014.asset` |
| `AD1-014#7840@P1` | `AD1-014` | 7840 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014_P1.asset` |
| `AD1-016#7843@base` | `AD1-016` | 7843 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016.asset` |
| `AD1-016#7844@P1` | `AD1-016` | 7844 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016_P1.asset` |
| `AD1-021#7852@base` | `AD1-021` | 7852 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021.asset` |
| `AD1-021#7853@P1` | `AD1-021` | 7853 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021_P1.asset` |
| `AD1-023#7855@base` | `AD1-023` | 7855 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023.asset` |
| `AD1-023#7856@P1` | `AD1-023` | 7856 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023_P1.asset` |
| `AD1-025#7860@base` | `AD1-025` | 7860 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025.asset` |
| `AD1-025#7861@P1` | `AD1-025` | 7861 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025_P1.asset` |
| `BT10-004#2036@base` | `BT10-004` | 2036 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/DigiEgg/BT10_004.asset` |
| `BT10-004#4285@P0` | `BT10-004` | 4285 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/DigiEgg/BT10_004_P0.asset` |
| `BT10-011#2043@base` | `BT10-011` | 2043 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_011.asset` |
| `BT10-011#2044@P1` | `BT10-011` | 2044 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_011_P1.asset` |
| `BT10-011#4293@P0` | `BT10-011` | 4293 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_011_P0.asset` |
| `BT10-011#4294@P2` | `BT10-011` | 4294 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_011_P2.asset` |
| `BT10-011#4295@P3` | `BT10-011` | 4295 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_011_P3.asset` |
| `BT10-011#4296@P4` | `BT10-011` | 4296 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_011_P4.asset` |
| `BT10-032#2070@base` | `BT10-032` | 2070 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_032.asset` |
| `BT10-032#4309@P0` | `BT10-032` | 4309 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_032_P0.asset` |
| `BT10-044#2084@base` | `BT10-044` | 2084 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_044.asset` |
| `BT10-044#2085@P1` | `BT10-044` | 2085 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_044_P1.asset` |
| `BT10-044#4317@P0` | `BT10-044` | 4317 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_044_P0.asset` |
| `BT10-048#2089@base` | `BT10-048` | 2089 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_048.asset` |
| `BT10-048#4320@P1` | `BT10-048` | 4320 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_048_P1.asset` |
| `BT10-048#4321@P2` | `BT10-048` | 4321 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_048_P2.asset` |
| `BT10-051#2092@base` | `BT10-051` | 2092 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_051.asset` |
| `BT10-051#4323@P1` | `BT10-051` | 4323 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_051_P1.asset` |
| `BT10-052#2093@base` | `BT10-052` | 2093 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_052.asset` |
| `BT10-053#2094@base` | `BT10-053` | 2094 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_053.asset` |
| `BT10-053#4324@P0` | `BT10-053` | 4324 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_053_P0.asset` |
| `BT11-032#2302@base` | `BT11-032` | 2302 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_032.asset` |
| `BT11-032#2303@P1` | `BT11-032` | 2303 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_032_P1.asset` |
| `BT11-032#8107@P2` | `BT11-032` | 8107 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_032_P2.asset` |
| `BT11-069#2343@base` | `BT11-069` | 2343 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_069.asset` |
| `BT11-069#4420@P0` | `BT11-069` | 4420 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_069_P0.asset` |
| `BT11-069#8108@P1` | `BT11-069` | 8108 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_069_P1.asset` |
| `BT11-074#2348@base` | `BT11-074` | 2348 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_074.asset` |
| `BT11-074#2349@P1` | `BT11-074` | 2349 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_074_P1.asset` |
| `BT11-087#2364@base` | `BT11-087` | 2364 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_087.asset` |
| `BT11-087#4432@P0` | `BT11-087` | 4432 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_087_P0.asset` |
| `BT11-103#2386@base` | `BT11-103` | 2386 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Option/BT11_103.asset` |
| `BT11-111#2394@base` | `BT11-111` | 2394 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_111.asset` |
| `BT11-111#2395@P1` | `BT11-111` | 2395 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_111_P1.asset` |
| `BT11-112#2396@base` | `BT11-112` | 2396 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_112.asset` |
| `BT11-112#2397@P1` | `BT11-112` | 2397 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_112_P1.asset` |
| `BT11-112#8114@P2` | `BT11-112` | 8114 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_112_P2.asset` |
| `BT12-029#2437@base` | `BT12-029` | 2437 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_029.asset` |
| `BT12-029#2438@P1` | `BT12-029` | 2438 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_029_P1.asset` |
| `BT12-044#2456@base` | `BT12-044` | 2456 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_044.asset` |
| `BT12-044#4503@P0` | `BT12-044` | 4503 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_044_P0.asset` |
| `BT13-040#2690@base` | `BT13-040` | 2690 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_040.asset` |
| `BT13-040#2691@P1` | `BT13-040` | 2691 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_040_P1.asset` |
| `BT13-040#4581@P0` | `BT13-040` | 4581 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_040_P0.asset` |
| `BT13-040#4582@P2` | `BT13-040` | 4582 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_040_P2.asset` |
| `BT13-040#4583@P3` | `BT13-040` | 4583 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_040_P3.asset` |
| `BT13-040#4584@P4` | `BT13-040` | 4584 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_040_P4.asset` |
| `BT13-075#2732@base` | `BT13-075` | 2732 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_075.asset` |
| `BT13-075#2733@P1` | `BT13-075` | 2733 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_075_P1.asset` |
| `BT13-075#4599@P2` | `BT13-075` | 4599 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_075_P2.asset` |
| `BT13-075#4600@P3` | `BT13-075` | 4600 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_075_P3.asset` |
| `BT14-018#2937@base` | `BT14-018` | 2937 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_018.asset` |
| `BT14-018#4643@P0` | `BT14-018` | 4643 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_018_P0.asset` |
| `BT15-005#3122@base` | `BT15-005` | 3122 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/DigiEgg/BT15_005.asset` |
| `BT15-005#3123@P1` | `BT15-005` | 3123 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/DigiEgg/BT15_005_P1.asset` |
| `BT15-005#4712@P0` | `BT15-005` | 4712 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/DigiEgg/BT15_005_P0.asset` |
| `BT15-054#3185@base` | `BT15-054` | 3185 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_054.asset` |
| `BT15-054#4739@P0` | `BT15-054` | 4739 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_054_P0.asset` |
| `BT16-082#3403@base` | `BT16-082` | 3403 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082.asset` |
| `BT16-082#3404@P1` | `BT16-082` | 3404 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P1.asset` |
| `BT16-082#4812@P0` | `BT16-082` | 4812 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P0.asset` |
| `BT16-082#4813@P2` | `BT16-082` | 4813 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P2.asset` |
| `BT16-082#4814@P3` | `BT16-082` | 4814 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P3.asset` |
| `BT16-082#4815@P4` | `BT16-082` | 4815 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P4.asset` |
| `BT16-082#8209@P5` | `BT16-082` | 8209 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P5.asset` |
| `BT16-082#8210@P6` | `BT16-082` | 8210 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P6.asset` |
| `BT16-082#8211@P7` | `BT16-082` | 8211 | `P7` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P7.asset` |
| `BT17-031#3576@base` | `BT17-031` | 3576 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_031.asset` |
| `BT17-031#4850@P1` | `BT17-031` | 4850 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_031_P1.asset` |
| `BT17-031#8227@P2` | `BT17-031` | 8227 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_031_P2.asset` |
| `BT17-032#3577@base` | `BT17-032` | 3577 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_032.asset` |
| `BT17-038#3583@base` | `BT17-038` | 3583 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_038.asset` |
| `BT17-038#4851@P0` | `BT17-038` | 4851 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_038_P0.asset` |
| `BT17-038#8230@P1` | `BT17-038` | 8230 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_038_P1.asset` |
| `BT19-030#5024@base` | `BT19-030` | 5024 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_030.asset` |
| `BT19-034#5027@base` | `BT19-034` | 5027 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_034.asset` |
| `BT19-034#8279@P1` | `BT19-034` | 8279 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_034_P1.asset` |
| `BT19-040#5033@base` | `BT19-040` | 5033 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_040.asset` |
| `BT19-083#5065@base` | `BT19-083` | 5065 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Tamer/BT19_083.asset` |
| `BT19-083#8294@P1` | `BT19-083` | 8294 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Tamer/BT19_083_P1.asset` |
| `BT19-083#8295@P2` | `BT19-083` | 8295 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Tamer/BT19_083_P2.asset` |
| `BT2-002#362@base` | `BT2-002` | 362 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/DigiEgg/BT2_002.asset` |
| `BT2-004#364@base` | `BT2-004` | 364 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/DigiEgg/BT2_004.asset` |
| `BT2-004#365@P1` | `BT2-004` | 365 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_004_P1.asset` |
| `BT2-004#366@P2` | `BT2-004` | 366 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_004_P2.asset` |
| `BT2-004#367@P3` | `BT2-004` | 367 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_004_P3.asset` |
| `BT2-004#8301@P1` | `BT2-004` | 8301 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/DigiEgg/BT2_004_P1.asset` |
| `BT2-004#8302@P2` | `BT2-004` | 8302 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/DigiEgg/BT2_004_P2.asset` |
| `BT2-004#8303@P3` | `BT2-004` | 8303 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/DigiEgg/BT2_004_P3.asset` |
| `BT2-021#395@base` | `BT2-021` | 395 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_021.asset` |
| `BT2-045#441@base` | `BT2-045` | 441 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_045.asset` |
| `BT2-047#444@base` | `BT2-047` | 444 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_047.asset` |
| `BT2-047#445@P1` | `BT2-047` | 445 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_047_P1.asset` |
| `BT2-050#449@base` | `BT2-050` | 449 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_050.asset` |
| `BT22-007#6995@base` | `BT22-007` | 6995 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/DigiEgg/BT22_007.asset` |
| `BT24-034#7558@base` | `BT24-034` | 7558 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_034.asset` |
| `BT24-034#7559@P1` | `BT24-034` | 7559 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_034_P1.asset` |
| `BT24-034#7560@P2` | `BT24-034` | 7560 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_034_P2.asset` |
| `BT24-052#7582@base` | `BT24-052` | 7582 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_052.asset` |
| `BT24-069#7602@base` | `BT24-069` | 7602 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_069.asset` |
| `BT25-091#8073@base` | `BT25-091` | 8073 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Tamer/BT25_091.asset` |
| `BT25-091#8074@P1` | `BT25-091` | 8074 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Tamer/BT25_091_P1.asset` |
| `BT25-091#8075@P2` | `BT25-091` | 8075 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Tamer/BT25_091_P2.asset` |
| `BT25-104#8092@base` | `BT25-104` | 8092 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_104.asset` |
| `BT25-104#8093@P1` | `BT25-104` | 8093 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_104_P1.asset` |
| `BT25-104#8094@P2` | `BT25-104` | 8094 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_104_P2.asset` |
| `BT3-054#673@base` | `BT3-054` | 673 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_054.asset` |
| `BT3-054#674@P1` | `BT3-054` | 674 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_054_P1.asset` |
| `BT3-054#8476@P2` | `BT3-054` | 8476 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_054_P2.asset` |
| `BT3-056#676@base` | `BT3-056` | 676 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_056.asset` |
| `BT3-056#677@P1` | `BT3-056` | 677 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_056_P1.asset` |
| `BT3-088#721@base` | `BT3-088` | 721 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_088.asset` |
| `BT3-088#722@P1` | `BT3-088` | 722 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_088_P1.asset` |
| `BT3-088#8485@P2` | `BT3-088` | 8485 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_088_P2.asset` |
| `BT3-088#8486@P3` | `BT3-088` | 8486 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_088_P3.asset` |
| `BT5-044#1000@P1` | `BT5-044` | 1000 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_044_P1.asset` |
| `BT5-044#8598@P0` | `BT5-044` | 8598 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_044_P0.asset` |
| `BT5-044#8599@P2` | `BT5-044` | 8599 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_044_P2.asset` |
| `BT5-044#999@base` | `BT5-044` | 999 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_044.asset` |
| `BT5-058#1016@base` | `BT5-058` | 1016 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_058.asset` |
| `BT5-058#8607@P0` | `BT5-058` | 8607 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_058_P0.asset` |
| `BT8-054#1629@base` | `BT8-054` | 1629 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_054.asset` |
| `EX10-010#7149@base` | `EX10-010` | 7149 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_010.asset` |
| `EX10-010#7277@P1` | `EX10-010` | 7277 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_010_P1.asset` |
| `EX10-010#7278@P2` | `EX10-010` | 7278 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_010_P2.asset` |
| `EX3-043#2225@base` | `EX3-043` | 2225 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_043.asset` |
| `P-056#6098@base` | `P-056` | 6098 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_056.asset` |

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
