# FND-002-RERUN Unknown Common API Source Mapping

이번 문서는 기존 FND-002 결과를 historical input으로만 사용하고, 현재 `full-mechanic-inventory.json`과 Foundation Gate 재계산 결과를 기준으로 unknown common API 상태를 다시 정리한다.

## Scope

- AS-IS root: `E:/headlessDCGO/DCGO`
- Source of Truth: `E:/headlessDCGO/DCGO/Assets`
- Implementation performed: no
- Generated status promoted: no
- Foundation Gate direct manipulation: no

## Count Audit

- Previous FND-002 NeedsSourceReview count: 27
- Current NeedsSourceReview count for original FND-002 set: 0
- Reduced NeedsSourceReview count: 27
- Source-mapped partial foundation count: 15
- Source-mapped data-policy partial count: 1
- Source-known zero-card timing count: 1
- Remaining NeedsSourceReview count: 0
- Foundation Gate unknownCommonApiCount: 0
- Foundation Gate OpenCodeReady: false

## Classification Counts

- SourceKnownZeroCardTiming: 1
- SourceMappedDataPolicyPartial: 1
- SourceMappedPartialFoundation: 15
- SourceMappedUnsupportedFoundation: 10

## Source-Mapped Partial Foundation Items

| API | FND task | Affected | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- |
| OnDeclaration | FND-003-P | 578 | 1 | 28 |
| OnTappedAnyone | FND-003-O | 306 | 1 | 41 |
| BeforePayCost | FND-003-N | 284 | 2 | 95 |
| OnDetermineDoSecurityCheck | FND-003-M | 228 | 1 | 26 |
| OnEndBattle | FND-003-L | 160 | 1 | 31 |
| OnDigivolutionCardDiscarded | FND-003-K | 121 | 2 | 27 |
| OnAddDigivolutionCards | FND-003-J | 102 | 1 | 32 |
| OnMove | FND-003-I | 79 | 1 | 21 |
| OnUnTappedAnyone | FND-003-H | 70 | 1 | 26 |
| OnUseOption | FND-003-G | 65 | 1 | 28 |
| OnDiscardLibrary | FND-003-F | 51 | 1 | 19 |
| OnAddSecurity | FND-003-E | 38 | 1 | 32 |
| OnDiscardSecurity | FND-003-D | 23 | 1 | 16 |
| AfterPayCost | FND-003-C | 15 | 2 | 39 |
| OnRemovedField | FND-003-B | 4 | 1 | 13 |

## Source-Mapped Data-Policy Partial Items

| API | Classification | Affected | Engine refs | Test/doc refs | Next action |
| --- | --- | --- | --- | --- | --- |
| OnFaceUpSecurityIncreased | SourceMappedDataPolicyPartial | 2 | 1 | 6 | face-up security data/state policy와 affected-card fixture 범위 확정 |

## Source-Known Zero-Card Timing Items

| API | Current | Classification | Affected | Source call evidence | Next action |
| --- | --- | --- | --- | --- | --- |
| OnStartBattle | NotReferenced | SourceKnownZeroCardTiming | 0 | CardController.cs:4557 | Current AS-IS CardEffect references are zero; exclude this source-known battle-start timing from current full-card-pool blockers and reopen payload fixture work only if a future source card references it. |

## Remaining NeedsSourceReview Items

| API | Classification | Affected | Source call evidence | Next action |
| --- | --- | --- | --- | --- |

## Full Mapping Table

| API | Previous | Current | Rerun classification | FND task | Affected | Source files | Engine refs | Test/doc refs | Next action |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| OnDeclaration | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-P | 578 | 302 | 1 | 28 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| WhenPermanentWouldBeDeleted | NeedsSourceReview | Unsupported | SourceMappedUnsupportedFoundation |  | 405 | 210 | 0 | 1 | replacement/delete-prevention cut-in payload foundation 작업으로 분리 |
| OnTappedAnyone | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-O | 306 | 140 | 1 | 41 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| WhenRemoveField | NeedsSourceReview | Unsupported | SourceMappedUnsupportedFoundation |  | 304 | 167 | 0 | 18 | would-remove cut-in과 actual removal event의 source ordering 재분해 |
| BeforePayCost | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-N | 284 | 143 | 2 | 95 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| OnDetermineDoSecurityCheck | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-M | 228 | 122 | 1 | 26 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| OnEndBattle | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-L | 160 | 86 | 1 | 31 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| OnDigivolutionCardDiscarded | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-K | 121 | 54 | 2 | 27 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| OnAddDigivolutionCards | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-J | 102 | 51 | 1 | 32 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| WhenLinked | NeedsSourceReview | Unsupported | SourceMappedUnsupportedFoundation |  | 87 | 67 | 0 | 0 | link lifecycle source mapping과 CanTriggerWhenLinked 조건 범위 확정 |
| OnMove | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-I | 79 | 32 | 1 | 21 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| OnUnTappedAnyone | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-H | 70 | 30 | 1 | 26 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| OnUseOption | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-G | 65 | 31 | 1 | 28 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| OnDiscardLibrary | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-F | 51 | 21 | 1 | 19 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| OnAddSecurity | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-E | 38 | 15 | 1 | 32 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| WhenReturntoLibraryAnyone | NeedsSourceReview | Unsupported | SourceMappedUnsupportedFoundation |  | 25 | 10 | 0 | 8 | return-to-library would-remove/replacement payload foundation 작업으로 분리 |
| OnDiscardSecurity | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-D | 23 | 15 | 1 | 16 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| WhenDigisorption | NeedsSourceReview | Unsupported | SourceMappedUnsupportedFoundation |  | 15 | 10 | 0 | 12 | BeforePayCost cost-reduction body와 WhenDigisorption 후속 trigger를 별도 cost mechanic 작업으로 분리 |
| AfterPayCost | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-C | 15 | 8 | 2 | 39 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| OnLinkCardDiscarded | NeedsSourceReview | Unsupported | SourceMappedUnsupportedFoundation |  | 14 | 8 | 0 | 0 | link source discard lifecycle와 payload source mapping 확정 |
| OnRemovedField | NeedsSourceReview | PartiallyImplemented | SourceMappedPartialFoundation | FND-003-B | 4 | 3 | 1 | 13 | full-card parity fixture가 없으므로 Verified 승격 없이 partial/blocker queue에서 추적 |
| WhenWouldLink | NeedsSourceReview | Unsupported | SourceMappedUnsupportedFoundation |  | 2 | 3 | 0 | 2 | would-link cut-in/replacement boundary source mapping 확정 |
| OnFaceUpSecurityIncreased | NeedsSourceReview | PartiallyImplemented | SourceMappedDataPolicyPartial |  | 2 | 2 | 1 | 6 | face-up security data/state policy와 affected-card fixture 범위 확정 |
| OnUseDigiburst | NeedsSourceReview | Unsupported | SourceMappedUnsupportedFoundation |  | 1 | 2 | 0 | 0 | DigiBurst mechanic body와 common timing boundary를 분리할 수 있는지 재검토 |
| WhenUntapAnyone | NeedsSourceReview | Unsupported | SourceMappedUnsupportedFoundation |  | 1 | 2 | 0 | 16 | would-untap cut-in, 대상 재고정, actual unsuspend ordering을 별도 foundation 작업으로 분리 |
| WhenWouldDigivolutionCardDiscarded | NeedsSourceReview | Unsupported | SourceMappedUnsupportedFoundation |  | 1 | 2 | 0 | 2 | would-source-trash replacement/cut-in payload foundation 작업으로 분리 |
| OnStartBattle | NeedsSourceReview | NotReferenced | SourceKnownZeroCardTiming |  | 0 | 1 | 0 | 4 | Current AS-IS CardEffect references are zero; exclude this source-known battle-start timing from current full-card-pool blockers and reopen payload fixture work only if a future source card references it. |

## Next Goal Candidate Range

- `FND-002-A`: replacement/cut-in timing source mapping
- `FND-002-B`: link lifecycle timing source mapping
- `FND-002-C`: digisorption/digiburst timing policy
- `FND-002-D`: face-up security data policy (closed as source-mapped partial data-policy blocker when present in the data-policy table)
- `FND-002-E`: `OnStartBattle` zero-card timing review (closed when present in the zero-card table)
- `FND-003`: unsupported capability 15개는 별도 remediation queue로 계속 유지
- `FND-001`: `ContinuousOrStaticEffect` partial 48개는 별도 partial closure queue로 계속 유지

## Guardrails

- `src/DCGO.RL.Engine` implementation code was not modified by this rerun.
- Original `DCGO/Assets` was not modified.
- No individual `CardEffect` body was implemented.
- No C0039+ card-porting was run.
- No RL Environment/Observation/Reward/Dataset/Trainer was implemented.
- Generated card status was not promoted.
- Foundation Gate numbers were recalculated from generated inventory, not edited by hand.
- No commit/push was performed.
