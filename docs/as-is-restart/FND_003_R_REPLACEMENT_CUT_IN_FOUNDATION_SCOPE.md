# FND-003-R Replacement / Cut-In Foundation Scope

## 요약

- AS-IS root: `E:/headlessDCGO/DCGO/Assets`
- Target count: `5`
- Classification counts: `{'SourceAlignedReplacementWindowPartial': 5}`
- Current capability status counts: `{'PartiallyImplemented': 5}`
- Unsupported -> PartiallyImplemented 전환 수: `5`
- Foundation Gate: `OpenCodeReady=False`, `Unsupported=5`, `PartiallyImplemented=58`
- 이번 작업은 replacement/cut-in request window foundation만 구현한다. CardEffect body, C0039 이후 card-porting, generated status 승격은 수행하지 않는다.

## 대상별 상태

| API | FND-002-A status | 현재 capability status | 분류 | Affected cards | 남은 범위 |
| --- | --- | --- | --- | --- | --- |
| WhenPermanentWouldBeDeleted | Unsupported | PartiallyImplemented | SourceAlignedReplacementWindowPartial | 405 | replacement body execution, cut-in continuation, target re-fix after selection, and actual destroy continuation remain follow-up work. |
| WhenRemoveField | Unsupported | PartiallyImplemented | SourceAlignedReplacementWindowPartial | 304 | willBeRemoveField target marking parity, replacement body execution, target re-fix, and actual movement continuation remain follow-up work. |
| WhenReturntoLibraryAnyone | Unsupported | PartiallyImplemented | SourceAlignedReplacementWindowPartial | 25 | deck top/bottom continuation, generic WhenRemoveField continuation ordering, target re-fix, and full-card parity remain follow-up work. |
| WhenUntapAnyone | Unsupported | PartiallyImplemented | SourceAlignedReplacementWindowPartial | 1 | replacement body execution, cannot-unsuspend policy, target re-fix, and actual unsuspend continuation remain follow-up work. |
| WhenWouldDigivolutionCardDiscarded | Unsupported | PartiallyImplemented | SourceAlignedReplacementWindowPartial | 1 | replacement body execution, source trash continuation, target re-fix, and full-card parity remain follow-up work. |

## 원본 / Headless 근거

| API | 원본 source 근거 | headless 구현 근거 | 테스트 근거 |
| --- | --- | --- | --- |
| WhenPermanentWouldBeDeleted | DCGO/Assets/Scripts/Script/ICardEffect.cs:980<br>DCGO/Assets/Scripts/Script/Permanent.cs:2898<br>DCGO/Assets/Scripts/Script/Permanent.cs:2954<br>DCGO/Assets/Scripts/Script/Permanent.cs:3117 | src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:PermanentRemovalReplacementWindowRequest<br>src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:QueuePermanentRemovalReplacementWindow | FND-003-R permanent removal replacement windows queue before mutation |
| WhenRemoveField | DCGO/Assets/Scripts/Script/ICardEffect.cs:979<br>DCGO/Assets/Scripts/Script/CardController.cs:2333<br>DCGO/Assets/Scripts/Script/CardController.cs:2499<br>DCGO/Assets/Scripts/Script/CardController.cs:2932 | src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:PermanentRemovalReplacementWindowRequest<br>src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:QueuePermanentRemovalReplacementWindow | FND-003-R permanent removal replacement windows queue before mutation |
| WhenReturntoLibraryAnyone | DCGO/Assets/Scripts/Script/ICardEffect.cs:981<br>DCGO/Assets/Scripts/Script/CardController.cs:2323<br>DCGO/Assets/Scripts/Script/CardController.cs:2489<br>DCGO/Assets/Scripts/Script/CardEffectCommons/HashtableSetting.cs:70 | src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:PermanentRemovalReplacementWindowRequest<br>src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:QueuePermanentRemovalReplacementWindow | FND-003-R permanent removal replacement windows queue before mutation |
| WhenUntapAnyone | DCGO/Assets/Scripts/Script/ICardEffect.cs:983<br>DCGO/Assets/Scripts/Script/CardController.cs:5694 | src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:UnsuspendReplacementWindowRequest<br>src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:QueueUnsuspendReplacementWindow | FND-003-R unsuspend replacement window queues before mutation |
| WhenWouldDigivolutionCardDiscarded | DCGO/Assets/Scripts/Script/ICardEffect.cs:1023<br>DCGO/Assets/Scripts/Script/CardController.cs:5181<br>DCGO/Assets/Scripts/Script/CardController.cs:5293<br>DCGO/Assets/Scripts/Script/CardEffectCommons/HashtableSetting.cs:321 | src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:DigivolutionCardDiscardReplacementWindowRequest<br>src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:QueueDigivolutionCardDiscardReplacementWindow | FND-003-R digivolution source discard replacement window queues before mutation |

## 닫힌 범위

- `QueuePermanentRemovalReplacementWindow`는 permanent delete/remove-field/return-to-library would payload를 실제 field/deck mutation 전에 큐잉한다.
- `QueueDigivolutionCardDiscardReplacementWindow`는 source card가 source stack에 남아 있는 상태에서 `WhenWouldDigivolutionCardDiscarded`를 큐잉한다.
- `QueueUnsuspendReplacementWindow`는 permanent가 아직 suspended인 상태에서 `WhenUntapAnyone`을 큐잉한다.
- 기존 post-event primitive인 `DestroyPermanent`, `ReturnPermanentToHandWithEvents`, `TrashDigivolutionCardsWithEvents`, `Unsuspend`의 의미는 유지한다.

## 남은 blocker

- replacement body 실행 및 선택 처리
- cut-in 이후 실제 zone mutation continuation
- original `willBeRemoveField` / target re-fix parity
- source-trash / return-to-library / unsuspend continuation과 full-card parity

## 다음 작업 후보

| ID | 분류 | 제목 | 이유 |
| --- | --- | --- | --- |
| FND-003-R1 | NeedsCapabilityRemediation | replacement/cut-in continuation contract | FND-003-R queues would events before mutation, but does not yet resume actual delete/return/source-trash/unsuspend continuation after replacement choices. |
| FND-003-R2 | NeedsCapabilityRemediation | target re-fix and stale target policy | Original CardController refixes targets after cut-in windows; headless currently records payload and request only. |
| FND-003-S | NeedsCapabilityRemediation | link lifecycle unsupported remediation | Remaining unsupported link timings are WhenWouldLink, WhenLinked, and OnLinkCardDiscarded. |
| FND-003-T | NeedsCapabilityRemediation | Digisorption/DigiBurst unsupported remediation | Remaining unsupported cost/mechanic timings are WhenDigisorption and OnUseDigiburst. |
| PARITY-001-A | NeedsSourceMapping | Unity full-card fixture exporter scenario contract | All FND-003-R targets remain NotRun for full-card parity. |

## 금지사항 확인

- 원본 `DCGO/Assets` 수정 없음
- 개별 `CardEffect` body 구현 없음
- C0039 이후 card-porting 실행 없음
- generated card status 승격 없음
- Foundation Gate 수치 직접 조작 없음
- RL 구성요소 구현 없음
- commit/push 없음
