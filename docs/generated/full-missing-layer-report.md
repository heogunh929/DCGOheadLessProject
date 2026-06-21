# 전체 Missing Layer Report

- Inventory SHA-256: `e8fd1723d947f14e49cdc1250e0e146092a1e7010cce2833b5dde4f28e836c27`
- Missing/partial candidate count: 64

이 문서는 63번 inventory의 보수적 mapping 결과다. `NeedsSourceReview`는 원본 사용은 보이지만 RL.Engine 대응 근거가 약하거나 source 의미를 더 읽어야 하는 항목이다.

## Top Missing / Partial Candidates

| Category | Name | Status | Source files | Affected cards | Engine refs | Test/doc refs | Sample source |
| --- | --- | --- | --- | --- | --- | --- | --- |
| timing | None | NeedsSourceReview | 2272 | 4326 | 0 | 70 | DCGO/Assets/Scripts/CardEffect/AD1/Black/AD1_023.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_010.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs |
| timing | OnEnterFieldAnyone | NeedsSourceReview | 2047 | 4141 | 0 | 6 | DCGO/Assets/Scripts/CardEffect/AD1/Black/AD1_023.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_010.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs |
| timing | OnDeclaration | NeedsSourceReview | 302 | 578 | 0 | 1 | DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_088.cs, DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_089.cs, DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_025.cs |
| timing | WhenPermanentWouldBeDeleted | NeedsSourceReview | 210 | 405 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_012.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_014.cs, DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_066.cs |
| timing | OnTappedAnyone | NeedsSourceReview | 140 | 306 | 0 | 1 | DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_014.cs, DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_016.cs, DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_021.cs |
| timing | WhenRemoveField | NeedsSourceReview | 167 | 304 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/AD1/Black/AD1_023.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_013.cs |
| timing | BeforePayCost | NeedsSourceReview | 143 | 284 | 0 | 8 | DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_088.cs, DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_052.cs, DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_093.cs |
| timing | OnDetermineDoSecurityCheck | NeedsSourceReview | 122 | 228 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_004.cs, DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_008.cs, DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_009.cs |
| timing | OnEndBattle | NeedsSourceReview | 86 | 160 | 0 | 1 | DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_077.cs, DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_112.cs, DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_028.cs |
| timing | OnDigivolutionCardDiscarded | NeedsSourceReview | 54 | 121 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_006.cs, DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_072.cs, DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_073.cs |
| timing | OnAddDigivolutionCards | NeedsSourceReview | 51 | 102 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_093.cs, DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_088.cs, DCGO/Assets/Scripts/CardEffect/BT17/Black/BT17_056.cs |
| timing | WhenLinked | NeedsSourceReview | 67 | 87 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_053.cs, DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_054.cs, DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_059.cs |
| timing | OnMove | NeedsSourceReview | 32 | 79 | 0 | 1 | DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_087.cs, DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_054.cs, DCGO/Assets/Scripts/CardEffect/BT16/White/BT16_082.cs |
| timing | OnUnTappedAnyone | NeedsSourceReview | 30 | 70 | 0 | 1 | DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_069.cs, DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_074.cs, DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_032.cs |
| timing | OnUseOption | NeedsSourceReview | 31 | 65 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_032.cs, DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_044.cs, DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_031.cs |
| timing | OnDiscardHand | NeedsSourceReview | 35 | 59 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_080.cs, DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_006.cs, DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_084.cs |
| timing | OnDiscardLibrary | NeedsSourceReview | 21 | 51 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_108.cs, DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_077.cs, DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_071.cs |
| timing | OnAddHand | NeedsSourceReview | 22 | 49 | 0 | 1 | DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_077.cs, DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_028.cs, DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_033.cs |
| timing | OnAddSecurity | NeedsSourceReview | 15 | 38 | 0 | 3 | DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_003.cs, DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_033.cs, DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_041.cs |
| timing | WhenReturntoHandAnyone | NeedsSourceReview | 10 | 25 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_062.cs, DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_064.cs, DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_074.cs |
| timing | WhenReturntoLibraryAnyone | NeedsSourceReview | 10 | 25 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_062.cs, DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_064.cs, DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_074.cs |
| timing | OnDiscardSecurity | NeedsSourceReview | 15 | 23 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_098.cs, DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_106.cs, DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_037.cs |
| timing | WhenDigisorption | NeedsSourceReview | 10 | 15 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_052.cs, DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs, DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_047.cs |
| timing | AfterPayCost | NeedsSourceReview | 8 | 15 | 0 | 9 | DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_109.cs, DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_103.cs, DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_109.cs |
| timing | OnLinkCardDiscarded | NeedsSourceReview | 8 | 14 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_030.cs, DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_062.cs, DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_070.cs |
| timing | WhenTopCardTrashed | NeedsSourceReview | 5 | 6 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_094.cs, DCGO/Assets/Scripts/CardEffect/BT8/White/BT8_110.cs, DCGO/Assets/Scripts/CardEffect/EX10/Red/EX10_010.cs |
| timing | OnReturnCardsToHandFromTrash | NeedsSourceReview | 3 | 6 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_082.cs, DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_011.cs, DCGO/Assets/Scripts/Script/CardObjectController.cs |
| timing | OnPermamemtReturnedToHand | NeedsSourceReview | 3 | 5 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_030.cs, DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_099.cs, DCGO/Assets/Scripts/Script/CardController.cs |
| timing | OnDigivolutionCardReturnToDeckBottom | NeedsSourceReview | 4 | 4 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_065.cs, DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_065.cs, DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_058.cs |
| timing | OnRemovedField | NeedsSourceReview | 3 | 4 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_007.cs, DCGO/Assets/Scripts/CardEffect/EX10/Red/EX10_010.cs, DCGO/Assets/Scripts/Script/CardObjectController.cs |
| timing | OnLeaveFieldAnyone | NeedsSourceReview | 3 | 2 | 0 | 1 | DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_025.cs, DCGO/Assets/Scripts/Script/CardController.cs, DCGO/Assets/Scripts/Script/CardObjectController.cs |
| timing | WhenWouldLink | NeedsSourceReview | 3 | 2 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_004.cs, DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_045.cs, DCGO/Assets/Scripts/Script/CardController.cs |
| timing | OnFaceUpSecurityIncreased | NeedsSourceReview | 2 | 2 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_004.cs, DCGO/Assets/Scripts/Script/CardController.cs |
| timing | OnReturnCardsToLibraryFromTrash | NeedsSourceReview | 2 | 2 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/P/Blue/P_048.cs, DCGO/Assets/Scripts/Script/CardObjectController.cs |
| timing | OnUseDigiburst | NeedsSourceReview | 2 | 1 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_056.cs, DCGO/Assets/Scripts/Script/CardController.cs |
| timing | WhenUntapAnyone | NeedsSourceReview | 2 | 1 | 0 | 1 | DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_055.cs, DCGO/Assets/Scripts/Script/CardController.cs |
| timing | WhenWouldDigivolutionCardDiscarded | NeedsSourceReview | 2 | 1 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_084.cs, DCGO/Assets/Scripts/Script/CardController.cs |
| timing | OnDraw | NeedsSourceReview | 1 | 0 | 0 | 10 | DCGO/Assets/Scripts/Script/CardController.cs |
| timing | OnStartBattle | NeedsSourceReview | 1 | 0 | 0 | 1 | DCGO/Assets/Scripts/Script/CardController.cs |
| feature | inherited | PartiallyImplemented | 2417 | 4491 | 13 | 0 | DCGO/Assets/Scripts/CardEffect/AD1/Black/AD1_023.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_010.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs |
| selection | SelectSecurity | PartiallyImplemented | 1313 | 2588 | 3 | 0 | DCGO/Assets/Scripts/CardEffect/AD1/Black/AD1_023.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_019.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_020.cs |
| specialMechanic | DigiBurst | PartiallyImplemented | 45 | 84 | 1 | 0 | DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_068.cs, DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_072.cs, DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_026.cs |
| specialMechanic | Digisorption | PartiallyImplemented | 27 | 40 | 1 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_052.cs, DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_091.cs, DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs |
| selection | SelectCount | PartiallyImplemented | 13 | 26 | 4 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_081.cs, DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_072.cs, DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_066.cs |
| specialMechanic | BlastDigivolution | Unsupported | 57 | 136 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_005.cs, DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_049.cs, DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_026.cs |
| keyword | Raid | Unsupported | 92 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_001.cs, DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_003.cs, DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_004.cs |
| keyword | Alliance | Unsupported | 88 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_012.cs, DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_009.cs, DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_016.cs |
| keyword | Barrier | Unsupported | 61 | 0 | 0 | 2 | DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_041.cs, DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_043.cs, DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_035.cs |
| keyword | ArmorPurge | Unsupported | 48 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_026.cs, DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_074.cs, DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_012.cs |
| keyword | Blitz | Unsupported | 38 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_009.cs, DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_070.cs, DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_014.cs |
| keyword | Fortitude | Unsupported | 35 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT2/White/BT2_083.cs, DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_034.cs, DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_035.cs |
| keyword | Training | Unsupported | 32 | 0 | 0 | 30 | DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_051.cs, DCGO/Assets/Scripts/CardEffect/EX9/Blue/EX9_015.cs, DCGO/Assets/Scripts/CardEffect/EX9/Blue/EX9_016.cs |
| keyword | Vortex | Unsupported | 30 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_085.cs, DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_101.cs, DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_095.cs |
| keyword | Evade | Unsupported | 26 | 0 | 0 | 2 | DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_012.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_014.cs, DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_112.cs |
| keyword | Scapegoat | Unsupported | 23 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_080.cs, DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_075.cs, DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_095.cs |
| keyword | Decode | Unsupported | 19 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_024.cs, DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_027.cs, DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_021.cs |
| keyword | Iceclad | Unsupported | 18 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_026.cs, DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_077.cs, DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_103.cs |
| keyword | Partition | Unsupported | 18 | 0 | 0 | 8 | DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs, DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_025.cs, DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_063.cs |
| keyword | MaterialSave | Unsupported | 15 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_024.cs, DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_009.cs, DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_013.cs |
| keyword | MindLink | Unsupported | 15 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_086.cs, DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_087.cs, DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_086.cs |
| keyword | Overclock | Unsupported | 15 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_101.cs, DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_036.cs, DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_040.cs |
| keyword | Progress | Unsupported | 13 | 0 | 0 | 4 | DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_025.cs, DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_029.cs, DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_017.cs |
| keyword | Ascension | Unsupported | 8 | 0 | 0 | 0 | DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_034.cs, DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_040.cs, DCGO/Assets/Scripts/CardEffect/P/Green/P_237.cs |
| feature | declarative | Unsupported | 4 | 0 | 0 | 2 | DCGO/Assets/Scripts/Script/AutoProcessing.cs, DCGO/Assets/Scripts/Script/ICardEffect.cs, DCGO/Assets/Scripts/Script/SelectPermanentEffect.cs |

## Status Counts

- timings: NeedsSourceReview=39, NotReferenced=9, Verified=15
- features: PartiallyImplemented=1, Unsupported=1, Verified=15
- selections: NotReferenced=1, PartiallyImplemented=2, Verified=13
- rootZones: Verified=9
- specialMechanics: PartiallyImplemented=2, Unsupported=1, Verified=8
- keywords: NotReferenced=2, Unsupported=18, Verified=14

## Explicit Notes

- 이 report는 effect body를 구현하지 않는다.
- source body가 없거나 mapping 근거가 약한 항목은 silent no-op으로 숨기지 않는다.
- ST1~ST3 검증 통과는 전체 카드풀 capability completion의 증거가 아니므로, 전체 카드풀 missing layer는 별도 queue에서 처리해야 한다.
