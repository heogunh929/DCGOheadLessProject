# TRUST-001 Src Implementation Trust Boundary Summary

- src C# file count: 338
- Engine/Test file count: 332 / 6
- OpenCodeReady: `False`
- Generated Implemented/Verified count: 0
- Full-card parity passed/not-run: 0 / 3918
- Trusted as Verified in TRUST-001: 0

## Classification Counts

| Classification | Count |
| --- | --- |
| ReuseCandidate | 52 |
| VerifiedCandidateNeedsTest | 27 |
| PartialNeedsWork | 50 |
| StaleOrWrongMapping | 0 |
| BlockedByFoundation | 185 |
| BlockedByDataPolicy | 17 |
| DeleteCandidate | 7 |
| ManualReview | 0 |

## Key Queues

- ReuseCandidate: 52
- DeleteCandidate: 7
- BlockedByFoundation: 185
- ManualReview: 0

## Trust Conclusion

- 기존 구현은 Verified로 신뢰하지 않는다.
- SourceOfTruth 매핑이 있는 파일은 재사용 후보 또는 부분 작업 후보로만 남긴다.
- CardEffect 파일은 원본 class match가 있어도 OpenCodeReady=false, generated status Unsupported, parity NotRun 때문에 대부분 BlockedByFoundation이다.
- 다음 구현 시작은 불가하다. 먼저 FND-001/FND-003/DataPolicy blocker를 별도 goal로 닫아야 한다.

## Generated Files

- `docs/as-is-restart/TRUST_001_SRC_IMPLEMENTATION_TRUST_BOUNDARY.md`
- `docs/as-is-restart/trust-001-src-implementation-trust-boundary-summary.md`
- `docs/generated/as-is-restart/trust-001-src-implementation-trust-boundary.json`
- `docs/generated/as-is-restart/trust-001-reuse-candidate-queue.json`

추천 commit message: `docs: define TRUST-001 src implementation trust boundary`
