# TRUST-001-RERUN Src Implementation Trust Boundary

이 문서는 최신 DATA/FND/PARITY evidence 기준으로 기존 `src` 구현 신뢰 경계를 다시 계산한 결과다.
구현, 원본 DCGO 수정, CardEffect body 구현, generated status 승격, Foundation Gate 조작은 수행하지 않았다.

## Scope

- SourceOfTruth root: `E:/headlessDCGO/DCGO`
- SourceOfTruth assets root: `E:\headlessDCGO\DCGO\Assets`
- Previous src C# file count: 338
- Current src C# file count: 340
- Engine/Test file count: 334 / 6
- OpenCodeReady: `false`
- Full-card parity Passed/NotRun: 0 / 3918
- Trusted as Verified: 0

## Rerun Delta

- Added src files: 2
  - `src/DCGO.RL.Engine/Battle/CostPaymentRuleEventPayload.cs`
  - `src/DCGO.RL.Engine/Battle/DeclarationEffectService.cs`
- Missing previous src files: 0
- DATA-001 reclassified files: 17

## Classification Counts

| Classification | Count |
| --- | ---: |
| BlockedByDataPolicy | 0 |
| BlockedByFoundation | 185 |
| DeleteCandidate | 7 |
| ManualReview | 0 |
| PartialNeedsWork | 53 |
| ReuseCandidate | 56 |
| StaleOrWrongMapping | 0 |
| VerifiedCandidateNeedsTest | 39 |

## Key Queues

- ReuseCandidate: 56
- VerifiedCandidateNeedsTest: 39
- PartialNeedsWork: 53
- BlockedByFoundation: 185
- BlockedByDataPolicy: 0
- DeleteCandidate: 7
- ManualReview: 0

## Evidence Refresh

- DATA-001 policy closed: `true`
- FND-001 closed first-group closeable tasks: `true`
- PARITY-001 fixture contract ready: `true`
- Foundation selected capability: `ContinuousOrStaticEffect` / `PartiallyImplemented`
- Unknown/Unsupported/Partial blockers: 27 / 26 / 37

## Trust Conclusion

- 기존 구현은 Verified로 신뢰하지 않는다.
- SourceOfTruth mapping이 있는 파일도 `ReuseCandidate` 또는 `PartialNeedsWork` 후보일 뿐이다.
- CardEffectLocalScript 185개는 full-card parity `NotRun 3918`과 generated `Unsupported` 상태 때문에 계속 `BlockedByFoundation`이다.
- DATA-001 이후 CardBaseEntity data model 일부는 `ReuseCandidate`로 이동했지만 importer/parity 검증 전 Verified가 아니다.
- 다음 구현 시작은 불가능하다. 먼저 foundation/parity/trust 후속을 닫아야 한다.

## Generated Files

- `docs/as-is-restart/TRUST_001_RERUN_SRC_IMPLEMENTATION_TRUST_BOUNDARY.md`
- `docs/as-is-restart/trust-001-rerun-src-implementation-trust-boundary-summary.md`
- `docs/generated/as-is-restart/trust-001-rerun-src-implementation-trust-boundary.json`
- `docs/generated/as-is-restart/trust-001-rerun-reuse-candidate-queue.json`
