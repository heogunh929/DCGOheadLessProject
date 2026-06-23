# FND-003 Unsupported Capability Remediation Summary

- AS-IS root: `E:/headlessDCGO/DCGO`
- 발견 수: 26
- 26건 일치 여부: 일치
- Gate unsupportedCapabilityCount: 26
- Gate sample count: 20 (sample truncation, 전체 목록 아님)
- FND-002 handoff 일치 여부: 일치

## Classification Counts

| Classification | Count |
| --- | ---: |
| CardEffectBodySpecific | 1 |
| DataPolicyRequired | 1 |
| ImplementableFoundationTask | 15 |
| PartialNeedsSubtasks | 9 |

## ImplementableFoundationTask

`OnDeclaration`, `OnTappedAnyone`, `BeforePayCost`, `OnDetermineDoSecurityCheck`, `OnEndBattle`, `OnDigivolutionCardDiscarded`, `OnAddDigivolutionCards`, `OnMove`, `OnUnTappedAnyone`, `OnUseOption`, `OnDiscardLibrary`, `OnAddSecurity`, `OnDiscardSecurity`, `AfterPayCost`, `OnRemovedField`

## BlockedNeedsManualReview

없음

## Handoff

- FND-001 전달 항목: 25
- TRUST-001 전달 항목: 26

## Generated Files

- `docs/as-is-restart/FND_003_UNSUPPORTED_CAPABILITY_REMEDIATION.md`
- `docs/as-is-restart/fnd-003-unsupported-capability-remediation-summary.md`
- `docs/generated/as-is-restart/fnd-003-unsupported-capability-remediation.json`
- `docs/generated/as-is-restart/fnd-003-next-action-queue.json`

## Guardrails

- 구현 없음.
- `src/` 수정 없음.
- 원본 DCGO 수정 없음.
- FND-001/TRUST-001 수행 없음.
- generated status 승격 없음.
- commit/push 없음.

추천 commit message: `docs: decompose FND-003 unsupported capability remediation`
