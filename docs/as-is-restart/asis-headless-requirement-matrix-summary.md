# AS-IS Headless Requirement Matrix Summary

- 생성 시각(UTC): `2026-06-23T06:12:25.6037778+00:00`
- AS-IS root: `E:\headlessDCGO\DCGO`
- 전체 파일 수: 71,180
- 전체 폴더 수: 4,798
- Required 파일 수: 12,541
- Reference 파일 수: 17,394
- CandidateReview 파일 수: 259
- Excluded 파일 수: 40,986
- Unknown 파일 수: 0

## NeedLevel별 파일 수
| NeedLevel | 파일 수 |
| --- | --- |
| CandidateReview | 259 |
| Excluded | 40,986 |
| Reference | 17,394 |
| Required | 12,541 |

## Headless category별 파일 수
| Category | 파일 수 |
| --- | --- |
| CardDataRequired | 8,187 |
| CardEffectLogicRequired | 3,918 |
| DataProvenanceToolCandidate | 24 |
| ExternalDependencyReference | 2,623 |
| GeneratedOrBuildExcluded | 34,501 |
| RuntimeLogicRequired | 436 |
| SerializedAssetReviewCandidate | 235 |
| SoundOnlyExcluded | 102 |
| SourceMetaReference | 14,343 |
| UnityProjectConfigReference | 31 |
| UnityProjectSourceReference | 397 |
| VisualOnlyExcluded | 6,383 |

## Source scope별 파일 수
| Scope | 파일 수 |
| --- | --- |
| (outside SourceOfTruth) | 43,922 |
| Assets/CardBaseEntity | 17,753 |
| Assets/Editor | 374 |
| Assets/Scripts/(other) | 2 |
| Assets/Scripts/CardEffect | 8,233 |
| Assets/Scripts/Script | 896 |

## 다음 Goal 추천
- GOAL 08에서는 Required/CandidateReview 범위를 기존 src headless 구현과 비교하되, 기존 구현 결과를 신뢰하지 않고 SourceOfTruth mapping evidence로 재검증한다.
- Assets/Scripts/Script runtime 공통 로직, Assets/Scripts/CardEffect source, Assets/CardBaseEntity 카드 데이터의 대응 여부를 별도 축으로 나눈다.
- GOAL 05 call graph의 Unity/GManager/Photon/UI/Coroutine tag는 구현 우선순위가 아니라 의존성 제거 및 trust audit 리스크 지표로 사용한다.
- GOAL 06의 missing effect source candidate 39건과 duplicate CardID group은 구현 전에 데이터/variant 정책 후보로 감사한다.
- VisualOnly/SoundOnly/GeneratedCache/BuildArtifact는 GOAL 08 headless 구현 감사 기본 범위에서 제외하되, UnityAdapter 목표에서 다시 열 수 있다.
