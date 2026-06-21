# L0001_existing_layer - 기존 layer로 즉시 구현 후보 common layer blocker 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `L0001_existing_layer`
- Kind: `mechanic-layer`
- Category: `existing-layer` / 기존 layer로 즉시 구현 후보
- Dependencies: none
- Card identity count: 161
- Source effect count: 0
- Mechanic blocker count: 8

## Mechanic / Layer Items

| Section | Name | Status | Affected cards | Source files |
| --- | --- | --- | ---: | ---: |
| `features` | `declarative` | `Unsupported` | 0 | 4 |
| `keywords` | `Scapegoat` | `Unsupported` | 100 | 23 |
| `keywords` | `Training` | `Unsupported` | 152 | 32 |
| `keywords` | `Vortex` | `Unsupported` | 148 | 30 |
| `selections` | `SelectCount` | `PartiallyImplemented` | 26 | 13 |
| `specialMechanics` | `Digisorption` | `PartiallyImplemented` | 40 | 27 |
| `timings` | `AfterPayCost` | `NeedsSourceReview` | 15 | 8 |
| `timings` | `BeforePayCost` | `NeedsSourceReview` | 284 | 143 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-109#308@base` | `BT1-109` | 308 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_109.asset` |
| `BT10-052#2093@base` | `BT10-052` | 2093 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_052.asset` |
| `BT10-081#2127@base` | `BT10-081` | 2127 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_081.asset` |
| `BT10-081#4339@P1` | `BT10-081` | 4339 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_081_P1.asset` |
| `BT10-087#2137@base` | `BT10-087` | 2137 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Tamer/BT10_087.asset` |
| `BT10-087#2138@P1` | `BT10-087` | 2138 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Tamer/BT10_087_P1.asset` |
| `BT10-087#4343@P0` | `BT10-087` | 4343 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Tamer/BT10_087_P0.asset` |
| `BT10-087#8102@P2` | `BT10-087` | 8102 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Tamer/BT10_087_P2.asset` |
| `BT10-088#2139@base` | `BT10-088` | 2139 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Tamer/BT10_088.asset` |
| `BT10-088#2140@P1` | `BT10-088` | 2140 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Tamer/BT10_088_P1.asset` |
| `BT10-088#4344@P0` | `BT10-088` | 4344 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Tamer/BT10_088_P0.asset` |
| `BT10-093#2148@base` | `BT10-093` | 2148 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093.asset` |
| `BT10-093#2149@P1` | `BT10-093` | 2149 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093_P1.asset` |
| `BT10-093#4351@P0` | `BT10-093` | 4351 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093_P0.asset` |
| `BT10-093#8103@P2` | `BT10-093` | 8103 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093_P2.asset` |
| `BT11-091#2371@base` | `BT11-091` | 2371 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Tamer/BT11_091.asset` |
| `BT11-091#2372@P1` | `BT11-091` | 2372 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Tamer/BT11_091_P1.asset` |
| `BT11-091#4435@P0` | `BT11-091` | 4435 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Tamer/BT11_091_P0.asset` |
| `BT11-095#2378@base` | `BT11-095` | 2378 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/White/Tamer/BT11_095.asset` |
| `BT11-095#4439@P0` | `BT11-095` | 4439 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/White/Tamer/BT11_095_P0.asset` |
| `BT11-095#8112@P1` | `BT11-095` | 8112 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/White/Tamer/BT11_095_P1.asset` |
| `BT11-095#8113@P2` | `BT11-095` | 8113 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/White/Tamer/BT11_095_P2.asset` |
| `BT12-022#2430@base` | `BT12-022` | 2430 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_022.asset` |
| `BT12-022#4480@P0` | `BT12-022` | 4480 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_022_P0.asset` |
| `BT12-022#4481@P1` | `BT12-022` | 4481 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_022_P1.asset` |
| `BT12-022#8120@P2` | `BT12-022` | 8120 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_022_P2.asset` |
| `BT12-022#8121@P3` | `BT12-022` | 8121 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_022_P3.asset` |
| `BT12-050#2462@base` | `BT12-050` | 2462 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_050.asset` |
| `BT18-072#3929@base` | `BT18-072` | 3929 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_072.asset` |
| `BT18-072#8262@P1` | `BT18-072` | 8262 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_072_P1.asset` |
| `BT2-045#441@base` | `BT2-045` | 441 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_045.asset` |
| `BT2-047#444@base` | `BT2-047` | 444 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_047.asset` |
| `BT2-047#445@P1` | `BT2-047` | 445 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_047_P1.asset` |
| `BT2-050#449@base` | `BT2-050` | 449 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_050.asset` |
| `BT2-066#481@base` | `BT2-066` | 481 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_066.asset` |
| `BT2-066#482@P1` | `BT2-066` | 482 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_066_P1.asset` |
| `BT2-066#483@P2` | `BT2-066` | 483 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_066_P2.asset` |
| `BT2-066#484@P3` | `BT2-066` | 484 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_066_P3.asset` |
| `BT2-066#485@P4` | `BT2-066` | 485 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_066_P4.asset` |
| `BT2-088#530@base` | `BT2-088` | 530 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Tamer/BT2_088.asset` |
| `BT2-088#8322@P1` | `BT2-088` | 8322 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Tamer/BT2_088_P1.asset` |
| `BT20-080#5159@base` | `BT20-080` | 5159 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_080.asset` |
| `BT20-101#5180@base` | `BT20-101` | 5180 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_101.asset` |
| `BT20-101#5259@P1` | `BT20-101` | 5259 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_101_P1.asset` |
| `BT20-101#5260@P2` | `BT20-101` | 5260 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_101_P2.asset` |
| `BT20-101#8365@P3` | `BT20-101` | 8365 | `P3` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_101_P3.asset` |
| `BT20-101#8366@P4` | `BT20-101` | 8366 | `P4` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_101_P4.asset` |
| `BT21-095#5422@base` | `BT21-095` | 5422 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Option/BT21_095.asset` |
| `BT21-095#8416@P1` | `BT21-095` | 8416 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Option/BT21_095_P1.asset` |
| `BT22-075#7082@base` | `BT22-075` | 7082 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_075.asset` |
| `BT22-095#7118@base` | `BT22-095` | 7118 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT22/White/Tamer/BT22_095.asset` |
| `BT22-095#7119@P1` | `BT22-095` | 7119 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT22/White/Tamer/BT22_095_P1.asset` |
| `BT23-066#7401@base` | `BT23-066` | 7401 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_066.asset` |
| `BT23-067#7402@base` | `BT23-067` | 7402 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_067.asset` |
| `BT25-053#8022@base` | `BT25-053` | 8022 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_053.asset` |
| `BT25-097#8083@base` | `BT25-097` | 8083 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Option/BT25_097.asset` |
| `BT3-054#673@base` | `BT3-054` | 673 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_054.asset` |
| `BT3-054#674@P1` | `BT3-054` | 674 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_054_P1.asset` |
| `BT3-054#8476@P2` | `BT3-054` | 8476 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_054_P2.asset` |
| `BT3-056#676@base` | `BT3-056` | 676 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_056.asset` |
| `BT3-056#677@P1` | `BT3-056` | 677 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_056_P1.asset` |
| `BT3-100#741@base` | `BT3-100` | 741 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Option/BT3_100.asset` |
| `BT3-103#744@base` | `BT3-103` | 744 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Option/BT3_103.asset` |
| `BT3-103#745@P1` | `BT3-103` | 745 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Option/BT3_103_P1.asset` |
| `BT3-103#8492@P2` | `BT3-103` | 8492 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Option/BT3_103_P2.asset` |
| `BT4-095#8546@P0` | `BT4-095` | 8546 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Tamer/BT4_095_P0.asset` |
| `BT4-095#898@base` | `BT4-095` | 898 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Tamer/BT4_095.asset` |
| `BT4-095#899@P1` | `BT4-095` | 899 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Tamer/BT4_095_P1.asset` |
| `BT5-025#970@base` | `BT5-025` | 970 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_025.asset` |
| `BT5-032#979@base` | `BT5-032` | 979 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_032.asset` |
| `BT5-032#980@P1` | `BT5-032` | 980 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_032_P1.asset` |
| `BT5-032#981@P2` | `BT5-032` | 981 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_032_P2.asset` |
| `BT5-049#1007@base` | `BT5-049` | 1007 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_049.asset` |
| `BT5-049#8603@P0` | `BT5-049` | 8603 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_049_P0.asset` |
| `BT5-058#1016@base` | `BT5-058` | 1016 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_058.asset` |
| `BT5-058#8607@P0` | `BT5-058` | 8607 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_058_P0.asset` |
| `BT5-088#1063@base` | `BT5-088` | 1063 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Tamer/BT5_088.asset` |
| `BT5-088#1064@P1` | `BT5-088` | 1064 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Tamer/BT5_088_P1.asset` |
| `BT5-088#8631@P0` | `BT5-088` | 8631 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Tamer/BT5_088_P0.asset` |
| `BT5-088#8632@P2` | `BT5-088` | 8632 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Tamer/BT5_088_P2.asset` |
| `BT5-092#1071@base` | `BT5-092` | 1071 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_092.asset` |
| `BT5-092#1072@P1` | `BT5-092` | 1072 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_092_P1.asset` |
| `BT5-092#8637@P0` | `BT5-092` | 8637 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_092_P0.asset` |
| `BT5-109#1090@base` | `BT5-109` | 1090 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Option/BT5_109.asset` |
| `BT5-109#1091@P1` | `BT5-109` | 1091 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Option/BT5_109_P1.asset` |
| `BT5-109#8655@P0` | `BT5-109` | 8655 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Option/BT5_109_P0.asset` |
| `EX1-033#1318@base` | `EX1-033` | 1318 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_033.asset` |
| `EX1-033#1319@P1` | `EX1-033` | 1319 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_033_P1.asset` |
| `EX1-033#9091@P2` | `EX1-033` | 9091 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_033_P2.asset` |
| `EX1-071#1373@base` | `EX1-071` | 1373 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Option/EX1_071.asset` |
| `EX1-071#1374@P1` | `EX1-071` | 1374 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Option/EX1_071_P1.asset` |
| `EX10-035#7196@base` | `EX10-035` | 7196 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_035.asset` |
| `EX10-035#7299@P1` | `EX10-035` | 7299 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_035_P1.asset` |
| `EX11-022#7700@base` | `EX11-022` | 7700 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_022.asset` |
| `EX11-022#7701@P1` | `EX11-022` | 7701 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_022_P1.asset` |
| `EX11-023#7702@base` | `EX11-023` | 7702 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_023.asset` |
| `EX11-023#7703@P1` | `EX11-023` | 7703 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_023_P1.asset` |
| `EX11-035#7728@base` | `EX11-035` | 7728 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_035.asset` |
| `EX11-035#7729@P1` | `EX11-035` | 7729 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_035_P1.asset` |
| `EX11-035#7730@P2` | `EX11-035` | 7730 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_035_P2.asset` |
| `EX11-036#7731@base` | `EX11-036` | 7731 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_036.asset` |
| `EX11-036#7732@P1` | `EX11-036` | 7732 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_036_P1.asset` |
| `EX11-050#7760@base` | `EX11-050` | 7760 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_050.asset` |
| `EX11-050#7761@P1` | `EX11-050` | 7761 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_050_P1.asset` |
| `EX11-062#7781@base` | `EX11-062` | 7781 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Green/Tamer/EX11_062.asset` |
| `EX11-062#7782@P1` | `EX11-062` | 7782 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Green/Tamer/EX11_062_P1.asset` |
| `EX11-074#7802@base` | `EX11-074` | 7802 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_074.asset` |
| `EX11-074#7803@P1` | `EX11-074` | 7803 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_074_P1.asset` |
| `EX11-074#7804@P2` | `EX11-074` | 7804 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_074_P2.asset` |
| `EX2-039#1978@base` | `EX2-039` | 1978 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_039.asset` |
| `EX2-039#1979@P1` | `EX2-039` | 1979 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_039_P1.asset` |
| `EX2-039#1980@P2` | `EX2-039` | 1980 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_039_P2.asset` |
| `EX2-055#2001@base` | `EX2-055` | 2001 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_055.asset` |
| `EX5-029#3068@base` | `EX5-029` | 3068 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_029.asset` |
| `EX6-052#3505@base` | `EX6-052` | 3505 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_052.asset` |
| `EX6-052#9161@P1` | `EX6-052` | 9161 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_052_P1.asset` |
| `EX6-053#3506@base` | `EX6-053` | 3506 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_053.asset` |
| `EX6-059#3516@base` | `EX6-059` | 3516 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_059.asset` |
| `EX6-059#3517@P1` | `EX6-059` | 3517 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_059_P1.asset` |
| `EX7-034#3740@base` | `EX7-034` | 3740 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_034.asset` |
| `EX7-034#9177@P1` | `EX7-034` | 9177 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_034_P1.asset` |
| `EX7-034#9178@P2` | `EX7-034` | 9178 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_034_P2.asset` |
| `EX7-034#9179@P3` | `EX7-034` | 9179 | `P3` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_034_P3.asset` |
| `EX7-036#3743@base` | `EX7-036` | 3743 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_036.asset` |
| `EX7-036#3744@P1` | `EX7-036` | 3744 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_036_P1.asset` |
| `EX7-036#3745@P2` | `EX7-036` | 3745 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_036_P2.asset` |
| `EX8-061#4164@base` | `EX8-061` | 4164 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_061.asset` |
| `EX8-061#4165@P1` | `EX8-061` | 4165 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_061_P1.asset` |
| `EX8-071#4187@base` | `EX8-071` | 4187 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Option/EX8_071.asset` |
| `EX8-071#4188@P1` | `EX8-071` | 4188 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Option/EX8_071_P1.asset` |
| `EX9-008#6846@base` | `EX9-008` | 6846 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_008.asset` |
| `EX9-008#6847@P1` | `EX9-008` | 6847 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_008_P1.asset` |
| `EX9-009#6848@base` | `EX9-009` | 6848 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_009.asset` |
| `EX9-009#6849@P1` | `EX9-009` | 6849 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_009_P1.asset` |
| `EX9-010#6850@base` | `EX9-010` | 6850 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_010.asset` |
| `EX9-010#6851@P1` | `EX9-010` | 6851 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_010_P1.asset` |
| `EX9-015#6861@base` | `EX9-015` | 6861 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_015.asset` |
| `EX9-015#6862@P1` | `EX9-015` | 6862 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_015_P1.asset` |
| `EX9-016#6863@base` | `EX9-016` | 6863 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_016.asset` |
| `EX9-016#6864@P1` | `EX9-016` | 6864 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_016_P1.asset` |
| `EX9-017#6865@base` | `EX9-017` | 6865 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_017.asset` |
| `EX9-017#6866@P1` | `EX9-017` | 6866 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_017_P1.asset` |
| `EX9-022#6876@base` | `EX9-022` | 6876 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_022.asset` |
| `EX9-022#6877@P1` | `EX9-022` | 6877 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_022_P1.asset` |
| `EX9-025#6882@base` | `EX9-025` | 6882 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_025.asset` |
| `EX9-025#6883@P1` | `EX9-025` | 6883 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_025_P1.asset` |
| `EX9-026#6884@base` | `EX9-026` | 6884 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_026.asset` |
| `EX9-026#6885@P1` | `EX9-026` | 6885 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_026_P1.asset` |
| `EX9-029#6890@base` | `EX9-029` | 6890 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_029.asset` |
| `EX9-029#6891@P1` | `EX9-029` | 6891 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_029_P1.asset` |
| `EX9-034#6900@base` | `EX9-034` | 6900 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_034.asset` |
| `EX9-034#6901@P1` | `EX9-034` | 6901 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_034_P1.asset` |
| `EX9-037#6906@base` | `EX9-037` | 6906 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_037.asset` |
| `EX9-037#6907@P1` | `EX9-037` | 6907 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_037_P1.asset` |
| `EX9-038#6908@base` | `EX9-038` | 6908 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_038.asset` |
| `EX9-063#6952@base` | `EX9-063` | 6952 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_063.asset` |
| `EX9-065#6956@base` | `EX9-065` | 6956 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_065.asset` |
| `LM-043#5444@base` | `LM-043` | 5444 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/LM/Black/Digimon/LM_043.asset` |
| `P-074#6116@base` | `P-074` | 6116 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_074.asset` |
| `ST12-15#2799@base` | `ST12-15` | 2799 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/Option/ST12_15.asset` |
| `ST12-15#4909@P1` | `ST12-15` | 4909 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/Option/ST12_15_P1.asset` |

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
