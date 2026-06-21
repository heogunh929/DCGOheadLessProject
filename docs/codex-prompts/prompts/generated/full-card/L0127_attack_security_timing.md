# L0127_attack_security_timing - attack/security timing common layer blocker 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `L0127_attack_security_timing`
- Kind: `mechanic-layer`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: none
- Card identity count: 162
- Source effect count: 0
- Mechanic blocker count: 8

## Mechanic / Layer Items

| Section | Name | Status | Affected cards | Source files |
| --- | --- | --- | ---: | ---: |
| `keywords` | `Alliance` | `Unsupported` | 552 | 88 |
| `keywords` | `Blitz` | `Unsupported` | 244 | 38 |
| `keywords` | `Overclock` | `Unsupported` | 8 | 15 |
| `keywords` | `Raid` | `Unsupported` | 604 | 92 |
| `selections` | `SelectSecurity` | `PartiallyImplemented` | 2588 | 1313 |
| `timings` | `OnAddSecurity` | `NeedsSourceReview` | 38 | 15 |
| `timings` | `OnDetermineDoSecurityCheck` | `NeedsSourceReview` | 228 | 122 |
| `timings` | `OnDiscardSecurity` | `NeedsSourceReview` | 23 | 15 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `AD1-001#7817@base` | `AD1-001` | 7817 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001.asset` |
| `AD1-001#7818@P1` | `AD1-001` | 7818 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001_P1.asset` |
| `AD1-003#7821@base` | `AD1-003` | 7821 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_003.asset` |
| `AD1-004#7822@base` | `AD1-004` | 7822 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004.asset` |
| `AD1-004#7823@P1` | `AD1-004` | 7823 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004_P1.asset` |
| `AD1-007#7828@base` | `AD1-007` | 7828 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007.asset` |
| `AD1-007#7829@P1` | `AD1-007` | 7829 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007_P1.asset` |
| `AD1-008#7830@base` | `AD1-008` | 7830 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008.asset` |
| `AD1-008#7831@P1` | `AD1-008` | 7831 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008_P1.asset` |
| `AD1-009#7832@base` | `AD1-009` | 7832 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009.asset` |
| `AD1-009#7833@P1` | `AD1-009` | 7833 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009_P1.asset` |
| `AD1-012#7836@base` | `AD1-012` | 7836 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012.asset` |
| `AD1-012#7837@P1` | `AD1-012` | 7837 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012_P1.asset` |
| `AD1-016#7843@base` | `AD1-016` | 7843 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016.asset` |
| `AD1-016#7844@P1` | `AD1-016` | 7844 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016_P1.asset` |
| `AD1-017#7845@base` | `AD1-017` | 7845 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_017.asset` |
| `AD1-017#7846@P1` | `AD1-017` | 7846 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_017_P1.asset` |
| `AD1-018#7847@base` | `AD1-018` | 7847 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Purple/Digimon/AD1_018.asset` |
| `AD1-018#7848@P1` | `AD1-018` | 7848 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Purple/Digimon/AD1_018_P1.asset` |
| `AD1-019#7862@base` | `AD1-019` | 7862 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_019.asset` |
| `AD1-020#7850@base` | `AD1-020` | 7850 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_020.asset` |
| `AD1-020#7851@P1` | `AD1-020` | 7851 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_020_P1.asset` |
| `AD1-021#7852@base` | `AD1-021` | 7852 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021.asset` |
| `AD1-021#7853@P1` | `AD1-021` | 7853 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021_P1.asset` |
| `AD1-022#7864@base` | `AD1-022` | 7864 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Green/AD1_022.asset` |
| `AD1-023#7855@base` | `AD1-023` | 7855 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023.asset` |
| `AD1-023#7856@P1` | `AD1-023` | 7856 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023_P1.asset` |
| `AD1-025#7860@base` | `AD1-025` | 7860 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025.asset` |
| `AD1-025#7861@P1` | `AD1-025` | 7861 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025_P1.asset` |
| `BT1-005#139@base` | `BT1-005` | 139 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/DigiEgg/BT1_005.asset` |
| `BT1-006#140@base` | `BT1-006` | 140 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_006.asset` |
| `BT1-006#4258@P1` | `BT1-006` | 4258 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/DigiEgg/BT1_006_P1.asset` |
| `BT1-017#156@base` | `BT1-017` | 156 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_017.asset` |
| `BT1-022#163@base` | `BT1-022` | 163 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_022.asset` |
| `BT1-026#171@base` | `BT1-026` | 171 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_026.asset` |
| `BT1-060#218@base` | `BT1-060` | 218 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_060.asset` |
| `BT1-060#219@P1` | `BT1-060` | 219 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_060_P1.asset` |
| `BT1-060#220@P2` | `BT1-060` | 220 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_060_P2.asset` |
| `BT1-060#4269@P3` | `BT1-060` | 4269 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_060_P3.asset` |
| `BT1-060#4270@P4` | `BT1-060` | 4270 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_060_P4.asset` |
| `BT1-063#225@base` | `BT1-063` | 225 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_063.asset` |
| `BT1-063#226@P1` | `BT1-063` | 226 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_063_P1.asset` |
| `BT1-081#258@base` | `BT1-081` | 258 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_081.asset` |
| `BT1-083#261@base` | `BT1-083` | 261 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_083.asset` |
| `BT1-083#262@P1` | `BT1-083` | 262 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_083_P1.asset` |
| `BT10-014#2048@base` | `BT10-014` | 2048 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_014.asset` |
| `BT10-014#4298@P0` | `BT10-014` | 4298 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_014_P0.asset` |
| `BT10-016#2050@base` | `BT10-016` | 2050 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_016.asset` |
| `BT10-016#2051@P1` | `BT10-016` | 2051 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_016_P1.asset` |
| `BT10-016#4300@P2` | `BT10-016` | 4300 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_016_P2.asset` |
| `BT10-049#2090@base` | `BT10-049` | 2090 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_049.asset` |
| `BT10-050#2091@base` | `BT10-050` | 2091 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_050.asset` |
| `BT10-050#4322@P1` | `BT10-050` | 4322 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_050_P1.asset` |
| `BT10-070#2116@base` | `BT10-070` | 2116 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_070.asset` |
| `BT10-070#4334@P0` | `BT10-070` | 4334 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_070_P0.asset` |
| `BT10-112#2169@base` | `BT10-112` | 2169 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_112.asset` |
| `BT10-112#2170@P1` | `BT10-112` | 2170 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_112_P1.asset` |
| `BT10-112#8104@base` | `BT10-112` | 8104 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_112_P2_J.asset` |
| `BT11-010#2277@base` | `BT11-010` | 2277 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_010.asset` |
| `BT11-014#2281@base` | `BT11-014` | 2281 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_014.asset` |
| `BT11-014#4375@P0` | `BT11-014` | 4375 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_014_P0.asset` |
| `BT11-017#2285@base` | `BT11-017` | 2285 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_017.asset` |
| `BT11-017#2286@P1` | `BT11-017` | 2286 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_017_P1.asset` |
| `BT11-057#2331@base` | `BT11-057` | 2331 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_057.asset` |
| `BT11-057#4411@P0` | `BT11-057` | 4411 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_057_P0.asset` |
| `BT12-018#2424@base` | `BT12-018` | 2424 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_018.asset` |
| `BT12-018#2425@P1` | `BT12-018` | 2425 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_018_P1.asset` |
| `BT12-018#2426@P2` | `BT12-018` | 2426 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_018_P2.asset` |
| `BT12-018#4475@P3` | `BT12-018` | 4475 | `P3` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_018_P3.asset` |
| `BT12-018#4476@P4` | `BT12-018` | 4476 | `P4` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_018_P4.asset` |
| `BT12-018#4477@P5` | `BT12-018` | 4477 | `P5` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_018_P5.asset` |
| `BT12-050#2462@base` | `BT12-050` | 2462 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_050.asset` |
| `BT12-050#4507@P0` | `BT12-050` | 4507 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_050_P0.asset` |
| `BT12-050#4508@P1` | `BT12-050` | 4508 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_050_P1.asset` |
| `BT12-068#2483@base` | `BT12-068` | 2483 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_068.asset` |
| `BT12-068#4517@P0` | `BT12-068` | 4517 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_068_P0.asset` |
| `BT12-068#4518@P1` | `BT12-068` | 4518 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_068_P1.asset` |
| `BT13-098#2764@base` | `BT13-098` | 2764 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Tamer/BT13_098.asset` |
| `BT13-098#2765@P1` | `BT13-098` | 2765 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Tamer/BT13_098_P1.asset` |
| `BT13-098#4617@P0` | `BT13-098` | 4617 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Tamer/BT13_098_P0.asset` |
| `BT13-106#2776@base` | `BT13-106` | 2776 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Option/BT13_106.asset` |
| `BT13-106#4626@P0` | `BT13-106` | 4626 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Option/BT13_106_P0.asset` |
| `BT14-003#2916@base` | `BT14-003` | 2916 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/DigiEgg/BT14_003.asset` |
| `BT14-003#2917@P1` | `BT14-003` | 2917 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/DigiEgg/BT14_003_P1.asset` |
| `BT14-003#4634@P0` | `BT14-003` | 4634 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/DigiEgg/BT14_003_P0.asset` |
| `BT14-017#2936@base` | `BT14-017` | 2936 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_017.asset` |
| `BT14-017#4642@P0` | `BT14-017` | 4642 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_017_P0.asset` |
| `BT14-033#2954@base` | `BT14-033` | 2954 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_033.asset` |
| `BT14-033#2955@P1` | `BT14-033` | 2955 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_033_P1.asset` |
| `BT14-033#8173@P2` | `BT14-033` | 8173 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_033_P2.asset` |
| `BT14-041#2964@base` | `BT14-041` | 2964 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041.asset` |
| `BT14-041#4657@P0` | `BT14-041` | 4657 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P0.asset` |
| `BT14-041#4658@P1` | `BT14-041` | 4658 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P1.asset` |
| `BT14-041#4659@P2` | `BT14-041` | 4659 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P2.asset` |
| `BT14-041#4660@P3` | `BT14-041` | 4660 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P3.asset` |
| `BT14-041#8174@P4` | `BT14-041` | 8174 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P4.asset` |
| `BT14-041#8175@P5` | `BT14-041` | 8175 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P5.asset` |
| `BT14-084#3015@base` | `BT14-084` | 3015 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Tamer/BT14_084.asset` |
| `BT14-084#3016@P1` | `BT14-084` | 3016 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Tamer/BT14_084_P1.asset` |
| `BT14-084#4694@P0` | `BT14-084` | 4694 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Tamer/BT14_084_P0.asset` |
| `BT14-087#3021@base` | `BT14-087` | 3021 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Tamer/BT14_087.asset` |
| `BT14-087#3022@P1` | `BT14-087` | 3022 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Tamer/BT14_087_P1.asset` |
| `BT14-087#4699@P0` | `BT14-087` | 4699 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Tamer/BT14_087_P0.asset` |
| `BT15-037#3162@base` | `BT15-037` | 3162 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_037.asset` |
| `BT15-037#3163@P1` | `BT15-037` | 3163 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_037_P1.asset` |
| `BT15-084#3221@base` | `BT15-084` | 3221 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Tamer/BT15_084.asset` |
| `BT15-084#3222@P1` | `BT15-084` | 3222 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Tamer/BT15_084_P1.asset` |
| `BT15-084#4759@P0` | `BT15-084` | 4759 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Tamer/BT15_084_P0.asset` |
| `BT15-084#4760@P2` | `BT15-084` | 4760 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Tamer/BT15_084_P2.asset` |
| `BT15-087#4764@P0` | `BT15-087` | 4764 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT15/Black/Tamer/BT15_087_P0.asset` |
| `BT15-087#4765@P2` | `BT15-087` | 4765 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT15/Black/Tamer/BT15_087_P2.asset` |
| `BT15-092#3233@base` | `BT15-092` | 3233 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Option/BT15_092.asset` |
| `BT15-092#4768@P0` | `BT15-092` | 4768 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Option/BT15_092_P0.asset` |
| `BT16-015#3323@base` | `BT16-015` | 3323 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_015.asset` |
| `BT16-015#4781@P0` | `BT16-015` | 4781 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_015_P0.asset` |
| `BT16-056#3373@base` | `BT16-056` | 3373 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_056.asset` |
| `BT16-079#3398@base` | `BT16-079` | 3398 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_079.asset` |
| `BT16-079#4811@P0` | `BT16-079` | 4811 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_079_P0.asset` |
| `BT17-034#3579@base` | `BT17-034` | 3579 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_034.asset` |
| `BT17-036#3581@base` | `BT17-036` | 3581 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_036.asset` |
| `BT17-049#3597@base` | `BT17-049` | 3597 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_049.asset` |
| `BT17-050#3598@base` | `BT17-050` | 3598 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_050.asset` |
| `BT17-050#4857@P0` | `BT17-050` | 4857 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_050_P0.asset` |
| `BT17-091#3658@base` | `BT17-091` | 3658 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Tamer/BT17_091.asset` |
| `BT17-091#4879@P0` | `BT17-091` | 4879 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Tamer/BT17_091_P0.asset` |
| `BT17-091#4880@P1` | `BT17-091` | 4880 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Tamer/BT17_091_P1.asset` |
| `BT18-016#3868@base` | `BT18-016` | 3868 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_016.asset` |
| `BT18-098#3969@base` | `BT18-098` | 3969 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Option/BT18_098.asset` |
| `BT19-014#5017@base` | `BT19-014` | 5017 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_014.asset` |
| `BT19-014#6828@P1` | `BT19-014` | 6828 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_014_P1.asset` |
| `BT19-053#4004@base` | `BT19-053` | 4004 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_053.asset` |
| `BT19-053#4005@P1` | `BT19-053` | 4005 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_053_P1.asset` |
| `BT19-053#8284@P2` | `BT19-053` | 8284 | `P2` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_053_P2.asset` |
| `BT19-073#4013@base` | `BT19-073` | 4013 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_073.asset` |
| `BT22-034#7031@base` | `BT22-034` | 7031 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_034.asset` |
| `BT22-037#7034@base` | `BT22-037` | 7034 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_037.asset` |
| `BT23-083#7426@base` | `BT23-083` | 7426 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Tamer/BT23_083.asset` |
| `BT23-083#7427@P1` | `BT23-083` | 7427 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Tamer/BT23_083_P1.asset` |
| `BT25-034#8000@base` | `BT25-034` | 8000 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_034.asset` |
| `BT25-038#8004@base` | `BT25-038` | 8004 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_038.asset` |
| `BT25-040#8006@base` | `BT25-040` | 8006 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_040.asset` |
| `BT5-009#8576@P0` | `BT5-009` | 8576 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_009_P0.asset` |
| `BT5-009#942@base` | `BT5-009` | 942 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_009.asset` |
| `BT5-009#943@P1` | `BT5-009` | 943 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_009_P1.asset` |
| `BT5-014#8578@P0` | `BT5-014` | 8578 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_014_P0.asset` |
| `BT5-014#954@base` | `BT5-014` | 954 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_014.asset` |
| `BT5-017#8581@P0` | `BT5-017` | 8581 | `P0` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_017_P0.asset` |
| `BT5-017#957@base` | `BT5-017` | 957 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_017.asset` |
| `BT5-019#959@base` | `BT5-019` | 959 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_019.asset` |
| `BT5-019#960@P1` | `BT5-019` | 960 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_019_P1.asset` |
| `BT5-086#1055@base` | `BT5-086` | 1055 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086.asset` |
| `BT5-086#1056@P1` | `BT5-086` | 1056 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_086_P1.asset` |
| `BT8-090#1681@base` | `BT8-090` | 1681 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Tamer/BT8_090.asset` |
| `BT8-090#1682@P1` | `BT8-090` | 1682 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Tamer/BT8_090_P1.asset` |
| `BT8-090#8910@P0` | `BT8-090` | 8910 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Tamer/BT8_090_P0.asset` |
| `BT8-090#8911@P2` | `BT8-090` | 8911 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Tamer/BT8_090_P2.asset` |
| `BT9-003#1779@base` | `BT9-003` | 1779 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/DigiEgg/BT9_003.asset` |
| `EX10-041#7207@base` | `EX10-041` | 7207 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_041.asset` |
| `EX10-041#7304@P1` | `EX10-041` | 7304 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_041_P1.asset` |
| `EX11-060#7778@base` | `EX11-060` | 7778 | `base` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Tamer/EX11_060.asset` |
| `EX11-060#7779@P1` | `EX11-060` | 7779 | `P1` | `Unsupported` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Tamer/EX11_060_P1.asset` |
| `ST22-10#7501@base` | `ST22-10` | 7501 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Option/ST22_10.asset` |

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
