# 전체 Missing Layer Report

- Inventory SHA-256: `674811df9b141a5b4ca871176469177c129f55d5f3382d4a90fea073d9cec4fb`
- Missing/partial candidate count: 42

이 문서는 63번 inventory의 보수적 mapping 결과다. `NeedsSourceReview`는 원본 사용은 보이지만 RL.Engine 대응 근거가 약하거나 source 의미를 더 읽어야 하는 항목이다.

## Top Missing / Partial Candidates

| Category | Name | Status | Source files | Affected cards | Engine refs | Test/doc refs | Sample source |
| --- | --- | --- | --- | --- | --- | --- | --- |
| timing | None | PartiallyImplemented | 2272 | 4326 | 8 | 232 | DCGO/Assets/Scripts/CardEffect/AD1/Black/AD1_023.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_010.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs |
| timing | OnEnterFieldAnyone | PartiallyImplemented | 2047 | 4141 | 2 | 137 | DCGO/Assets/Scripts/CardEffect/AD1/Black/AD1_023.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_010.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs |
| timing | OnDeclaration | PartiallyImplemented | 302 | 578 | 1 | 28 | DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_088.cs, DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_089.cs, DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_025.cs |
| timing | WhenPermanentWouldBeDeleted | PartiallyImplemented | 210 | 405 | 1 | 3 | DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_012.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_014.cs, DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_066.cs |
| timing | OnTappedAnyone | PartiallyImplemented | 140 | 306 | 1 | 41 | DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_014.cs, DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_016.cs, DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_021.cs |
| timing | WhenRemoveField | PartiallyImplemented | 167 | 304 | 3 | 22 | DCGO/Assets/Scripts/CardEffect/AD1/Black/AD1_023.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_013.cs |
| timing | BeforePayCost | PartiallyImplemented | 143 | 284 | 2 | 95 | DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_088.cs, DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_052.cs, DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_093.cs |
| timing | OnDetermineDoSecurityCheck | PartiallyImplemented | 122 | 228 | 1 | 26 | DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_004.cs, DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_008.cs, DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_009.cs |
| timing | OnEndBattle | PartiallyImplemented | 86 | 160 | 1 | 31 | DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_077.cs, DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_112.cs, DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_028.cs |
| timing | OnDigivolutionCardDiscarded | PartiallyImplemented | 54 | 121 | 2 | 27 | DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_006.cs, DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_072.cs, DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_073.cs |
| timing | OnAddDigivolutionCards | PartiallyImplemented | 51 | 102 | 1 | 32 | DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_093.cs, DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_088.cs, DCGO/Assets/Scripts/CardEffect/BT17/Black/BT17_056.cs |
| specialMechanic | DigiBurst | PartiallyImplemented | 45 | 84 | 1 | 0 | DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_068.cs, DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_072.cs, DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_026.cs |
| timing | OnMove | PartiallyImplemented | 32 | 79 | 1 | 21 | DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_087.cs, DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_054.cs, DCGO/Assets/Scripts/CardEffect/BT16/White/BT16_082.cs |
| timing | OnUnTappedAnyone | PartiallyImplemented | 30 | 70 | 1 | 26 | DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_069.cs, DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_074.cs, DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_032.cs |
| timing | OnUseOption | PartiallyImplemented | 31 | 65 | 1 | 28 | DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_032.cs, DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_044.cs, DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_031.cs |
| timing | OnDiscardLibrary | PartiallyImplemented | 21 | 51 | 1 | 19 | DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_108.cs, DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_077.cs, DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_071.cs |
| timing | OnAddSecurity | PartiallyImplemented | 15 | 38 | 1 | 32 | DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_003.cs, DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_033.cs, DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_041.cs |
| timing | WhenReturntoLibraryAnyone | PartiallyImplemented | 10 | 25 | 1 | 10 | DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_062.cs, DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_064.cs, DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_074.cs |
| timing | OnDiscardSecurity | PartiallyImplemented | 15 | 23 | 1 | 16 | DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_098.cs, DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_106.cs, DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_037.cs |
| timing | AfterPayCost | PartiallyImplemented | 8 | 15 | 2 | 39 | DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_109.cs, DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_103.cs, DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_109.cs |
| timing | OnRemovedField | PartiallyImplemented | 3 | 4 | 1 | 13 | DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_007.cs, DCGO/Assets/Scripts/CardEffect/EX10/Red/EX10_010.cs, DCGO/Assets/Scripts/Script/CardObjectController.cs |
| timing | OnFaceUpSecurityIncreased | PartiallyImplemented | 2 | 2 | 1 | 6 | DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_004.cs, DCGO/Assets/Scripts/Script/CardController.cs |
| timing | WhenUntapAnyone | PartiallyImplemented | 2 | 1 | 2 | 18 | DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_055.cs, DCGO/Assets/Scripts/Script/CardController.cs |
| timing | WhenWouldDigivolutionCardDiscarded | PartiallyImplemented | 2 | 1 | 2 | 4 | DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_084.cs, DCGO/Assets/Scripts/Script/CardController.cs |
| specialMechanic | BlastDigivolution | Unsupported | 57 | 136 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_005.cs, DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_049.cs, DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_026.cs |
| timing | WhenLinked | Unsupported | 67 | 87 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_053.cs, DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_054.cs, DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_059.cs |
| timing | WhenDigisorption | Unsupported | 10 | 15 | 0 | 12 | DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_052.cs, DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs, DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_047.cs |
| timing | OnLinkCardDiscarded | Unsupported | 8 | 14 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_030.cs, DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_062.cs, DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_070.cs |
| timing | WhenWouldLink | Unsupported | 3 | 2 | 0 | 2 | DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_004.cs, DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_045.cs, DCGO/Assets/Scripts/Script/CardController.cs |
| timing | OnUseDigiburst | Unsupported | 2 | 1 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_056.cs, DCGO/Assets/Scripts/Script/CardController.cs |
| keyword | Alliance | Unsupported | 88 | 0 | 0 | 8 | DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_012.cs, DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_009.cs, DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_016.cs |
| keyword | Barrier | Unsupported | 61 | 0 | 0 | 2 | DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_041.cs, DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_043.cs, DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_035.cs |
| keyword | ArmorPurge | Unsupported | 48 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_026.cs, DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_074.cs, DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_012.cs |
| keyword | Blitz | Unsupported | 38 | 0 | 0 | 16 | DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_009.cs, DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_070.cs, DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_014.cs |
| keyword | Evade | Unsupported | 26 | 0 | 0 | 2 | DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_012.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_014.cs, DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_112.cs |
| keyword | Scapegoat | Unsupported | 23 | 0 | 0 | 12 | DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_080.cs, DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_075.cs, DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_095.cs |
| keyword | Partition | Unsupported | 18 | 0 | 0 | 8 | DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs, DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_025.cs, DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_063.cs |
| keyword | MaterialSave | Unsupported | 15 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_024.cs, DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_009.cs, DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_013.cs |
| keyword | MindLink | Unsupported | 15 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_086.cs, DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_087.cs, DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_086.cs |
| keyword | Progress | Unsupported | 13 | 0 | 0 | 16 | DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_025.cs, DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_029.cs, DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_017.cs |
| keyword | Ascension | Unsupported | 8 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_034.cs, DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_040.cs, DCGO/Assets/Scripts/CardEffect/P/Green/P_237.cs |
| feature | declarative | Unsupported | 4 | 0 | 0 | 9 | DCGO/Assets/Scripts/Script/AutoProcessing.cs, DCGO/Assets/Scripts/Script/ICardEffect.cs, DCGO/Assets/Scripts/Script/SelectPermanentEffect.cs |

## Status Counts

- timings: NotReferenced=10, PartiallyImplemented=23, Unsupported=5, Verified=25
- features: Unsupported=1, Verified=16
- selections: NotReferenced=1, Verified=15
- rootZones: Verified=9
- specialMechanics: PartiallyImplemented=1, Unsupported=1, Verified=9
- keywords: NotReferenced=2, Unsupported=11, Verified=21

## Explicit Notes

- 이 report는 effect body를 구현하지 않는다.
- source body가 없거나 mapping 근거가 약한 항목은 silent no-op으로 숨기지 않는다.
- ST1~ST3 검증 통과는 전체 카드풀 capability completion의 증거가 아니므로, 전체 카드풀 missing layer는 별도 queue에서 처리해야 한다.
