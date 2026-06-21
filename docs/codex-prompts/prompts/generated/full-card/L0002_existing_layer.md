# L0002_existing_layer - 기존 layer로 즉시 구현 후보 common layer blocker 2

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `L0002_existing_layer`
- Kind: `mechanic-layer`
- Category: `existing-layer` / 기존 layer로 즉시 구현 후보
- Dependencies: none
- Card identity count: 153
- Source effect count: 0
- Mechanic blocker count: 8

## Mechanic / Layer Items

| Section | Name | Status | Affected cards | Source files |
| --- | --- | --- | ---: | ---: |
| `timings` | `None` | `NeedsSourceReview` | 4326 | 2272 |
| `timings` | `OnAddDigivolutionCards` | `NeedsSourceReview` | 102 | 51 |
| `timings` | `OnDeclaration` | `NeedsSourceReview` | 578 | 302 |
| `timings` | `OnDigivolutionCardDiscarded` | `NeedsSourceReview` | 121 | 54 |
| `timings` | `OnDiscardLibrary` | `NeedsSourceReview` | 51 | 21 |
| `timings` | `OnEndBattle` | `NeedsSourceReview` | 160 | 86 |
| `timings` | `OnEnterFieldAnyone` | `NeedsSourceReview` | 4141 | 2047 |
| `timings` | `OnLeaveFieldAnyone` | `NeedsSourceReview` | 2 | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `AD1-001#7817@base` | `AD1-001` | 7817 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001.asset` |
| `AD1-001#7818@P1` | `AD1-001` | 7818 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001_P1.asset` |
| `AD1-002#7819@base` | `AD1-002` | 7819 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002.asset` |
| `AD1-002#7820@P1` | `AD1-002` | 7820 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002_P1.asset` |
| `AD1-003#7821@base` | `AD1-003` | 7821 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_003.asset` |
| `AD1-004#7822@base` | `AD1-004` | 7822 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004.asset` |
| `AD1-004#7823@P1` | `AD1-004` | 7823 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004_P1.asset` |
| `AD1-005#7824@base` | `AD1-005` | 7824 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005.asset` |
| `AD1-005#7825@P1` | `AD1-005` | 7825 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005_P1.asset` |
| `AD1-006#7826@base` | `AD1-006` | 7826 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006.asset` |
| `AD1-006#7827@P1` | `AD1-006` | 7827 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006_P1.asset` |
| `AD1-007#7828@base` | `AD1-007` | 7828 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007.asset` |
| `AD1-007#7829@P1` | `AD1-007` | 7829 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007_P1.asset` |
| `AD1-008#7830@base` | `AD1-008` | 7830 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008.asset` |
| `AD1-008#7831@P1` | `AD1-008` | 7831 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008_P1.asset` |
| `AD1-009#7832@base` | `AD1-009` | 7832 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009.asset` |
| `AD1-009#7833@P1` | `AD1-009` | 7833 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009_P1.asset` |
| `AD1-010#7834@base` | `AD1-010` | 7834 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_010.asset` |
| `AD1-011#7835@base` | `AD1-011` | 7835 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_011.asset` |
| `AD1-012#7836@base` | `AD1-012` | 7836 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012.asset` |
| `AD1-012#7837@P1` | `AD1-012` | 7837 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012_P1.asset` |
| `AD1-013#7838@base` | `AD1-013` | 7838 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_013.asset` |
| `AD1-014#7839@base` | `AD1-014` | 7839 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014.asset` |
| `AD1-014#7840@P1` | `AD1-014` | 7840 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014_P1.asset` |
| `AD1-015#7841@base` | `AD1-015` | 7841 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_015.asset` |
| `AD1-016#7843@base` | `AD1-016` | 7843 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016.asset` |
| `AD1-025#7860@base` | `AD1-025` | 7860 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025.asset` |
| `AD1-025#7861@P1` | `AD1-025` | 7861 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025_P1.asset` |
| `BT1-077#250@base` | `BT1-077` | 250 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_077.asset` |
| `BT1-077#251@P1` | `BT1-077` | 251 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_077_P1.asset` |
| `BT1-077#252@P2` | `BT1-077` | 252 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_077_P2.asset` |
| `BT1-088#277@base` | `BT1-088` | 277 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_088.asset` |
| `BT1-088#278@P1` | `BT1-088` | 278 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_088_P1.asset` |
| `BT1-088#279@P2` | `BT1-088` | 279 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_088_P2.asset` |
| `BT1-089#280@base` | `BT1-089` | 280 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_089.asset` |
| `BT1-089#281@P1` | `BT1-089` | 281 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_089_P1.asset` |
| `BT1-089#282@P2` | `BT1-089` | 282 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_089_P2.asset` |
| `BT1-089#4276@P3` | `BT1-089` | 4276 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_089_P3.asset` |
| `BT1-112#314@base` | `BT1-112` | 314 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_112.asset` |
| `BT10-006#2038@base` | `BT10-006` | 2038 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/DigiEgg/BT10_006.asset` |
| `BT10-006#4287@P0` | `BT10-006` | 4287 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/DigiEgg/BT10_006_P0.asset` |
| `BT10-025#2062@base` | `BT10-025` | 2062 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_025.asset` |
| `BT10-028#2066@base` | `BT10-028` | 2066 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_028.asset` |
| `BT10-028#4308@P0` | `BT10-028` | 4308 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_028_P0.asset` |
| `BT10-045#2086@base` | `BT10-045` | 2086 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_045.asset` |
| `BT10-053#2094@base` | `BT10-053` | 2094 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_053.asset` |
| `BT10-053#4324@P0` | `BT10-053` | 4324 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_053_P0.asset` |
| `BT10-054#2095@base` | `BT10-054` | 2095 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_054.asset` |
| `BT10-054#2096@P1` | `BT10-054` | 2096 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_054_P1.asset` |
| `BT10-054#4325@P0` | `BT10-054` | 4325 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_054_P0.asset` |
| `BT10-072#2118@base` | `BT10-072` | 2118 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_072.asset` |
| `BT10-073#2119@base` | `BT10-073` | 2119 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_073.asset` |
| `BT10-073#4335@P0` | `BT10-073` | 4335 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_073_P0.asset` |
| `BT10-075#2121@base` | `BT10-075` | 2121 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_075.asset` |
| `BT10-075#4336@P0` | `BT10-075` | 4336 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_075_P0.asset` |
| `BT10-076#2122@base` | `BT10-076` | 2122 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_076.asset` |
| `BT10-077#2123@base` | `BT10-077` | 2123 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_077.asset` |
| `BT10-093#2148@base` | `BT10-093` | 2148 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093.asset` |
| `BT10-093#2149@P1` | `BT10-093` | 2149 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093_P1.asset` |
| `BT10-093#4351@P0` | `BT10-093` | 4351 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093_P0.asset` |
| `BT10-093#8103@P2` | `BT10-093` | 8103 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093_P2.asset` |
| `BT10-097#2153@base` | `BT10-097` | 2153 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Option/BT10_097.asset` |
| `BT10-097#4354@P0` | `BT10-097` | 4354 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Option/BT10_097_P0.asset` |
| `BT10-097#4355@P1` | `BT10-097` | 4355 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Option/BT10_097_P1.asset` |
| `BT10-100#2156@base` | `BT10-100` | 2156 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Option/BT10_100.asset` |
| `BT10-100#4356@P1` | `BT10-100` | 4356 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Option/BT10_100_P1.asset` |
| `BT10-108#2164@base` | `BT10-108` | 2164 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Option/BT10_108.asset` |
| `BT10-108#4364@P0` | `BT10-108` | 4364 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Option/BT10_108_P0.asset` |
| `BT11-029#2299@base` | `BT11-029` | 2299 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_029.asset` |
| `BT11-029#4392@P0` | `BT11-029` | 4392 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_029_P0.asset` |
| `BT11-055#2328@base` | `BT11-055` | 2328 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_055.asset` |
| `BT11-055#4410@P0` | `BT11-055` | 4410 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_055_P0.asset` |
| `BT11-059#2333@base` | `BT11-059` | 2333 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_059.asset` |
| `BT11-059#4413@P0` | `BT11-059` | 4413 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_059_P0.asset` |
| `BT11-061#2335@base` | `BT11-061` | 2335 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_061.asset` |
| `BT11-077#2352@base` | `BT11-077` | 2352 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_077.asset` |
| `BT11-077#4426@P0` | `BT11-077` | 4426 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_077_P0.asset` |
| `BT11-081#2356@base` | `BT11-081` | 2356 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_081.asset` |
| `BT11-082#2357@base` | `BT11-082` | 2357 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_082.asset` |
| `BT11-082#4428@P0` | `BT11-082` | 4428 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_082_P0.asset` |
| `BT11-088#2365@base` | `BT11-088` | 2365 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_088.asset` |
| `BT11-088#2366@P1` | `BT11-088` | 2366 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_088_P1.asset` |
| `BT12-015#2419@base` | `BT12-015` | 2419 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_015.asset` |
| `BT12-015#4466@P0` | `BT12-015` | 4466 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_015_P0.asset` |
| `BT12-015#4467@P1` | `BT12-015` | 4467 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_015_P1.asset` |
| `BT12-015#4468@P2` | `BT12-015` | 4468 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_015_P2.asset` |
| `BT12-026#2434@base` | `BT12-026` | 2434 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_026.asset` |
| `BT12-026#4483@P0` | `BT12-026` | 4483 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_026_P0.asset` |
| `BT12-036#2446@base` | `BT12-036` | 2446 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_036.asset` |
| `BT12-053#2466@base` | `BT12-053` | 2466 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_053.asset` |
| `BT12-055#2468@base` | `BT12-055` | 2468 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_055.asset` |
| `BT12-055#4509@P0` | `BT12-055` | 4509 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_055_P0.asset` |
| `BT12-089#2511@base` | `BT12-089` | 2511 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_089.asset` |
| `BT12-089#2512@P1` | `BT12-089` | 2512 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_089_P1.asset` |
| `BT12-089#4536@P0` | `BT12-089` | 4536 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_089_P0.asset` |
| `BT13-055#2707@base` | `BT13-055` | 2707 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_055.asset` |
| `BT14-028#2949@base` | `BT14-028` | 2949 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_028.asset` |
| `BT14-028#4647@P0` | `BT14-028` | 4647 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_028_P0.asset` |
| `BT14-077#3005@base` | `BT14-077` | 3005 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_077.asset` |
| `BT14-083#3013@base` | `BT14-083` | 3013 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Tamer/BT14_083.asset` |
| `BT14-083#3014@P1` | `BT14-083` | 3014 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Tamer/BT14_083_P1.asset` |
| `BT14-083#4693@P0` | `BT14-083` | 4693 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Tamer/BT14_083_P0.asset` |
| `BT15-043#3171@base` | `BT15-043` | 3171 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_043.asset` |
| `BT15-043#3172@P1` | `BT15-043` | 3172 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_043_P1.asset` |
| `BT15-047#3176@base` | `BT15-047` | 3176 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_047.asset` |
| `BT16-015#3323@base` | `BT16-015` | 3323 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_015.asset` |
| `BT16-015#4781@P0` | `BT16-015` | 4781 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_015_P0.asset` |
| `BT16-047#3362@base` | `BT16-047` | 3362 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_047.asset` |
| `BT16-047#4800@P0` | `BT16-047` | 4800 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_047_P0.asset` |
| `BT16-061#3378@base` | `BT16-061` | 3378 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_061.asset` |
| `BT17-003#3543@base` | `BT17-003` | 3543 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/DigiEgg/BT17_003.asset` |
| `BT17-003#4835@P0` | `BT17-003` | 4835 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/DigiEgg/BT17_003_P0.asset` |
| `BT17-006#3546@base` | `BT17-006` | 3546 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/DigiEgg/BT17_006.asset` |
| `BT17-006#4838@P0` | `BT17-006` | 4838 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/DigiEgg/BT17_006_P0.asset` |
| `BT17-056#3605@base` | `BT17-056` | 3605 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Black/Digimon/BT17_056.asset` |
| `BT17-090#3657@base` | `BT17-090` | 3657 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Tamer/BT17_090.asset` |
| `BT17-090#4877@P0` | `BT17-090` | 4877 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Tamer/BT17_090_P0.asset` |
| `BT17-090#4878@P1` | `BT17-090` | 4878 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Tamer/BT17_090_P1.asset` |
| `BT19-071#5051@base` | `BT19-071` | 5051 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_071.asset` |
| `BT19-097#5074@base` | `BT19-097` | 5074 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Option/BT19_097.asset` |
| `BT2-085#523@base` | `BT2-085` | 523 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Tamer/BT2_085.asset` |
| `BT2-085#524@P1` | `BT2-085` | 524 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Tamer/BT2_085_P1.asset` |
| `BT20-034#5113@base` | `BT20-034` | 5113 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_034.asset` |
| `BT20-035#5114@base` | `BT20-035` | 5114 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_035.asset` |
| `BT20-071#5150@base` | `BT20-071` | 5150 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_071.asset` |
| `BT20-080#5159@base` | `BT20-080` | 5159 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_080.asset` |
| `BT21-080#5397@base` | `BT21-080` | 5397 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_080.asset` |
| `BT21-080#5398@P1` | `BT21-080` | 5398 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_080_P1.asset` |
| `BT21-080#8411@P2` | `BT21-080` | 8411 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_080_P2.asset` |
| `BT21-080#8412@P3` | `BT21-080` | 8412 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_080_P3.asset` |
| `BT21-090#5417@base` | `BT21-090` | 5417 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Option/BT21_090.asset` |
| `BT21-090#8414@P1` | `BT21-090` | 8414 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Option/BT21_090_P1.asset` |
| `BT21-090#8415@P2` | `BT21-090` | 8415 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Option/BT21_090_P2.asset` |
| `BT8-006#1560@base` | `BT8-006` | 1560 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/DigiEgg/BT8_006.asset` |
| `BT8-006#8845@P0` | `BT8-006` | 8845 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/DigiEgg/BT8_006_P0.asset` |
| `BT8-074#1656@base` | `BT8-074` | 1656 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_074.asset` |
| `BT8-079#1661@base` | `BT8-079` | 1661 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_079.asset` |
| `BT8-079#1662@P1` | `BT8-079` | 1662 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_079_P1.asset` |
| `BT8-079#8892@P0` | `BT8-079` | 8892 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_079_P0.asset` |
| `BT8-079#8893@P2` | `BT8-079` | 8893 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_079_P2.asset` |
| `EX10-005#7140@base` | `EX10-005` | 7140 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/DigiEgg/EX10_005.asset` |
| `EX10-005#7273@P1` | `EX10-005` | 7273 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/DigiEgg/EX10_005_P1.asset` |
| `EX10-037#7200@base` | `EX10-037` | 7200 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_037.asset` |
| `EX10-037#7301@P1` | `EX10-037` | 7301 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_037_P1.asset` |
| `EX10-041#7207@base` | `EX10-041` | 7207 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_041.asset` |
| `EX10-041#7304@P1` | `EX10-041` | 7304 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_041_P1.asset` |
| `EX2-039#1978@base` | `EX2-039` | 1978 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_039.asset` |
| `EX2-039#1979@P1` | `EX2-039` | 1979 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_039_P1.asset` |
| `EX2-039#1980@P2` | `EX2-039` | 1980 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_039_P2.asset` |
| `EX2-044#1987@base` | `EX2-044` | 1987 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_044.asset` |
| `EX2-044#1988@P1` | `EX2-044` | 1988 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_044_P1.asset` |
| `EX2-044#9111@P2` | `EX2-044` | 9111 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_044_P2.asset` |
| `EX2-044#9112@P3` | `EX2-044` | 9112 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_044_P3.asset` |

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
