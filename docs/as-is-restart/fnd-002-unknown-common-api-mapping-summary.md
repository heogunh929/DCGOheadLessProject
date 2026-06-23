# FND-002 Unknown Common API Mapping Summary

- AS-IS root: `E:/headlessDCGO/DCGO`
- 발견 수: 27
- 27건 일치 여부: 일치
- Gate unknownCommonApiCount: 27
- Gate sample count: 20 (sample truncation, 전체 목록 아님)

## Classification Counts

| Classification | Count |
| --- | ---: |
| BlockedNeedsManualReview | 1 |
| CardEffectBodySpecific | 1 |
| DataDrivenRequired | 1 |
| FoundationRequired | 24 |

## FoundationRequired

`OnDeclaration`, `WhenPermanentWouldBeDeleted`, `OnTappedAnyone`, `WhenRemoveField`, `BeforePayCost`, `OnDetermineDoSecurityCheck`, `OnEndBattle`, `OnDigivolutionCardDiscarded`, `OnAddDigivolutionCards`, `WhenLinked`, `OnMove`, `OnUnTappedAnyone`, `OnUseOption`, `OnDiscardLibrary`, `OnAddSecurity`, `WhenReturntoLibraryAnyone`, `OnDiscardSecurity`, `WhenDigisorption`, `AfterPayCost`, `OnLinkCardDiscarded`, `OnRemovedField`, `WhenWouldLink`, `WhenUntapAnyone`, `WhenWouldDigivolutionCardDiscarded`

## BlockedNeedsManualReview

`OnStartBattle`

## Handoff

- FND-001 전달 항목: 25
- FND-003 전달 항목: 26
- Manual review 항목: 1

## Generated Files

- `docs/as-is-restart/FND_002_UNKNOWN_COMMON_API_SOURCE_MAPPING.md`
- `docs/as-is-restart/fnd-002-unknown-common-api-mapping-summary.md`
- `docs/generated/as-is-restart/fnd-002-unknown-common-api-mapping.json`
- `docs/generated/as-is-restart/fnd-002-next-action-queue.json`

## Guardrails

- 구현 없음.
- `src/` 수정 없음.
- 원본 DCGO 수정 없음.
- generated status 승격 없음.
- commit/push 없음.

추천 commit message: `docs: map FND-002 unknown common APIs`
