# FND-003-Q Capability Status Reconciliation

이번 문서는 FND-003에서 이미 runtime foundation과 테스트 근거가 있는 capability가 capability truth audit에서 계속 `Unsupported`로 남아 있던 상태를 source-aligned 기준으로 재조정한다. 구현은 수행하지 않는다.

## 기준선

- Source root: `E:/headlessDCGO/DCGO/Assets`
- Foundation Gate OpenCodeReady: `False`
- Gate unsupported capability count: `10`
- Gate partially implemented capability count: `53`
- Reconciled target count: `5`
- Classification counts: `{'SourceAlignedPartialFoundation': 5}`

## Reconciled Capability

| Capability | FND goal | Previous audit | Inventory | Reconciled | Affected cards | Source files | Engine refs | Test refs |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| AfterPayCost | FND-003-C | Unsupported | PartiallyImplemented | PartiallyImplemented | 15 | 7 | 2 | 39 |
| BeforePayCost | FND-003-N | Unsupported | PartiallyImplemented | PartiallyImplemented | 284 | 141 | 2 | 95 |
| OnDeclaration | FND-003-P | Unsupported | PartiallyImplemented | PartiallyImplemented | 578 | 298 | 1 | 28 |
| OnEndBattle | FND-003-L | Unsupported | PartiallyImplemented | PartiallyImplemented | 160 | 84 | 1 | 31 |
| OnMove | FND-003-I | Unsupported | PartiallyImplemented | PartiallyImplemented | 79 | 30 | 1 | 21 |

## Source Samples

| Capability | Source class | Source path | Affected cards |
| --- | --- | --- | --- |
| AfterPayCost | BT1_109 | DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_109.cs | 1 |
| AfterPayCost | BT3_103 | DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_103.cs | 3 |
| AfterPayCost | BT5_109 | DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_109.cs | 3 |
| BeforePayCost | BT10_052 | DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_052.cs | 1 |
| BeforePayCost | BT10_087 | DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_087.cs | 4 |
| BeforePayCost | BT10_088 | DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_088.cs | 3 |
| OnDeclaration | BT1_088 | DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_088.cs | 3 |
| OnDeclaration | BT1_089 | DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_089.cs | 4 |
| OnDeclaration | BT10_025 | DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_025.cs | 1 |
| OnEndBattle | BT1_077 | DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_077.cs | 3 |
| OnEndBattle | BT1_112 | DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_112.cs | 1 |
| OnEndBattle | BT10_028 | DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_028.cs | 2 |
| OnMove | BT11_087 | DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_087.cs | 2 |
| OnMove | BT15_054 | DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_054.cs | 2 |
| OnMove | BT16_082 | DCGO/Assets/Scripts/CardEffect/BT16/White/BT16_082.cs | 9 |

## Remaining Boundary

| Capability | Boundary |
| --- | --- |
| AfterPayCost | full-card parity remains NotRun and card-specific cost/body variants are not promoted. |
| BeforePayCost | full-card parity remains NotRun and replacement/cost-policy variants remain follow-up work. |
| OnDeclaration | full-card parity remains NotRun and declaration continuation/card-body variants remain follow-up work. |
| OnEndBattle | full-card parity remains NotRun and all battle lifecycle/card-body variants remain follow-up work. |
| OnMove | full-card parity remains NotRun and source-zone edge variants remain follow-up work. |

## Next Queue

- Remaining unsupported capabilities: `10`
- Remaining partially implemented capabilities: `53`
- Recommended next groups:
  - `FND-003-R`: WhenPermanentWouldBeDeleted, WhenRemoveField, WhenReturntoLibraryAnyone, WhenUntapAnyone, WhenWouldDigivolutionCardDiscarded
  - `FND-003-S`: WhenWouldLink, WhenLinked, OnLinkCardDiscarded
  - `FND-003-T`: WhenDigisorption, OnUseDigiburst
  - `FND-001-CONTINUE`: ContinuousOrStaticEffect
  - `PARITY-001-A`: non-capability parity work

## 금지 사항 준수

- `src/DCGO.RL.Engine` 구현 코드 수정 없음.
- 원본 `DCGO/Assets` 수정 없음.
- 개별 `CardEffect` body 구현 없음.
- C0039 이후 card-porting 실행 없음.
- RL Environment/Observation/Reward/Dataset/Trainer 구현 없음.
- generated card status 또는 Verified 승격 없음.
- Foundation Gate 수치 수동 조작 없음.
- commit/push 없음.
