# AS-IS Porting Scope Decision Summary

- AS-IS root: `E:/headlessDCGO/DCGO`
- 전체 파일 수: 71180
- 전체 폴더 수: 4798
- 모든 파일 decision 보유: `True`
- 모든 파일 bucket 보유: `True`
- ManualReview 파일 수: 0

## Decision Counts

| Decision | Count |
| --- | --- |
| Port | 12541 |
| ReferenceOnly | 17653 |
| Exclude | 40986 |
| ManualReview | 0 |

## Bucket Counts

| Bucket | Count |
| --- | --- |
| CardData | 8187 |
| CardEffectLogic | 3918 |
| CoreRuntime | 436 |
| EditorDataProvenance | 259 |
| ExternalDependency | 2623 |
| GeneratedOrBuild | 34501 |
| ProjectConfig | 31 |
| SoundOnly | 102 |
| SourceMeta | 14343 |
| UnityOnly | 397 |
| VisualOnly | 6383 |

## Closure Notes

- CandidateReview input files 259개는 최종 decision으로 닫았다.
- CandidateReview 중 ManualReview로 남은 파일은 0개다.
- CardEffectLogic은 포팅 대상이지만 Foundation blocker 해소 전 구현 대상이 아니다.
- Visual/Sound/Generated/Build는 제외다.

추천 commit message: `docs: close AS-IS porting scope decisions`
