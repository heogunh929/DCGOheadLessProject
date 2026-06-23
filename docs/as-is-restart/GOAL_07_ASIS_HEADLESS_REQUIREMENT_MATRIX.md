# GOAL 07 AS-IS Headless Requirement Matrix

이번 문서는 GOAL 01-06 산출물을 기준으로 원본 DCGO 파일과 폴더의 headless battle engine 필요 여부를 분류한 기준선이다.
기존 headless 구현은 신뢰하지 않고, 구현/이식 작업도 수행하지 않았다.

## 입력
- AS-IS root: `E:\headlessDCGO\DCGO`
- GOAL 02 role: `C:\Users\HG\.codex\worktrees\793a\headlessDCGO\docs\generated\as-is-restart\asis-role-reclassification.json`
- GOAL 03 SourceOfTruth audit: `C:\Users\HG\.codex\worktrees\793a\headlessDCGO\docs\generated\as-is-restart\asis-source-of-truth-audit-summary.json`
- GOAL 04 C# file index: `C:\Users\HG\.codex\worktrees\793a\headlessDCGO\docs\generated\as-is-restart\asis-csharp-file-index.json`
- GOAL 05 call graph summary/detail: `C:\Users\HG\.codex\worktrees\793a\headlessDCGO\docs\generated\as-is-restart\asis-csharp-call-graph-summary.json`, `C:\Users\HG\.codex\worktrees\793a\headlessDCGO\docs\generated\as-is-restart\asis-csharp-call-graph.json`
- GOAL 06 CardBaseEntity index: `C:\Users\HG\.codex\worktrees\793a\headlessDCGO\docs\generated\as-is-restart\asis-cardbaseentity-card-index.json`

## 분류 정책
GOAL 01-06 generated evidence only. This matrix does not modify source, does not trust existing headless implementation, and does not execute card-porting.

| NeedLevel | 의미 |
| --- | --- |
| Required | headless 엔진 기준선에 직접 필요한 원본 로직 또는 카드 데이터 |
| Reference | headless 런타임 직접 구현 대상은 아니지만 원본 재현, GUID, Unity 프로젝트 맥락, 의존성 확인에 필요한 참조 |
| CandidateReview | 후속 Goal에서 headless 필요 여부를 추가 검증해야 하는 후보 |
| Excluded | headless battle engine 기준선에는 기본적으로 제외하는 생성물, 빌드 산출물, 시각/사운드 전용 파일 |
| Unknown | 현 단계 증거만으로 필요 여부를 판단하지 않는 항목 |

## 전체 요약
| 항목 | 값 |
| --- | --- |
| 전체 파일 | 71,180 |
| 전체 폴더 | 4,798 |
| Required 파일 | 12,541 |
| Reference 파일 | 17,394 |
| CandidateReview 파일 | 259 |
| Excluded 파일 | 40,986 |
| Unknown 파일 | 0 |
| SourceOfTruth 파일 | 26,884 |
| C# 파일 | 7,723 |
| CardData 파일 | 8,186 |

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

## C# matrix
| 항목 | 값 |
| --- | --- |
| 전체 C# 파일 | 7,723 |
| SourceOfTruth C# 파일 | 4,354 |
| Required C# 파일 | 4,354 |
| CandidateReview C# 파일 | 24 |
| Excluded C# 파일 | 3,116 |
| 파일별 edgeCount 합계 | 1,378,594 |

## CardBaseEntity matrix
| 항목 | 값 |
| --- | --- |
| 카드 asset | 8,186 |
| loader asset | 1 |
| effect source 후보 없음 | 39 |
| empty effect class | 225 |
| NoEffect marker | 0 |

## SourceOfTruth audit 연결
| 항목 | 값 |
| --- | --- |
| SourceOfTruth 파일 | 26,884 |
| SourceOfTruth C# 파일 | 4,354 |
| Source CardDataCandidate | 8,187 |
| Expected source scope not source | 2 |
| False positive SourceOfTruth | 0 |
| Scriptable outside source | 67 |
| Project-owned assets outside source | 631 |
| Editor tool code outside source | 24 |

## Call graph 리스크 지표
| 항목 | 값 |
| --- | --- |
| 전체 call edge | 1,378,594 |
| SourceOfTruth caller edge | 642,654 |
| Unity tag | 122,493 |
| GManager tag | 38,651 |
| Photon tag | 32,878 |
| UI tag | 98,062 |
| Coroutine tag | 62,387 |

## Unknown 파일 샘플
| 경로 | Category | 근거 |
| --- | --- | --- |

## CandidateReview 샘플
| 경로 | Category | 근거 |
| --- | --- | --- |
| Assets/Editor/AttachCardData.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/AttachCardPoolPrefab.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/AttachShuffledNumberIDs.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/AutoTextureConvert.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/CheckDeckCodeParse.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/ConvertDigiEggCardKind.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/CsvLoader_CardEntity.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/FindEntitiesFromCardIndex.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/AdjustSetSpecificCardIndex.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/ChangeAssetsToEnglish.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/CleanUpClassName.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/FindInconsistentCardType.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/FindInconsistentNaming.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/FindMissingAAs.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/FixDigiEggCost.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/FixErrataImages.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/FixForDualTypings.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/FixRestrictedValues.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/GetHighestCardIndex.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/GetListOfInvalidClassNames.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/GetLowestCardIndex.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/MatchClassNames.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Fixing Scripts/ScriptableObjects/CardEntity_Inconsistency.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/GetAsset.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/JSONLoader_CardEntity.cs | DataProvenanceToolCandidate | Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보. |
| Assets/Editor/Missing AAs/BT19/Blue/Tamer/BT19_081_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT19/Red/Tamer/BT19_079_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Black/Digimon/BT20_048_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Black/Digimon/BT20_052_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Black/Digimon/BT20_053_P1.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Black/Digimon/BT20_057_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Black/Digimon/BT20_058_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Black/Digimon/BT20_059_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Black/Tamer/BT20_086_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Black/Tamer/BT20_087_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Blue/Digimon/BT20_025_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Blue/Digimon/BT20_026_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Green/Digimon/BT20_042_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Green/Digimon/BT20_043_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Green/Tamer/BT20_085_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Purple/Digimon/BT20_063_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Purple/Digimon/BT20_064_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Purple/Digimon/BT20_068_P1.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Purple/Digimon/BT20_069_P1.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Purple/Digimon/BT20_072_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Purple/Digimon/BT20_080_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Purple/Option/BT20_096_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Purple/Option/BT20_098_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Purple/Tamer/BT20_088_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |
| Assets/Editor/Missing AAs/BT20/Purple/Tamer/BT20_089_P0.asset | SerializedAssetReviewCandidate | Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보. |

## Required 대용량 파일 샘플
| 경로 | Category | 크기 |
| --- | --- | --- |
| Assets/Scripts/Script/CardController.cs | RuntimeLogicRequired | 220786 bytes |
| Assets/Scripts/Script/TurnStateMachine.cs | RuntimeLogicRequired | 164412 bytes |
| Assets/Scripts/Script/Permanent.cs | RuntimeLogicRequired | 148542 bytes |
| Assets/Scripts/Script/CardSource.cs | RuntimeLogicRequired | 129087 bytes |
| Assets/Scripts/Script/Effects.cs | RuntimeLogicRequired | 83054 bytes |
| Assets/Scripts/Script/CardEffectFactory.cs | RuntimeLogicRequired | 80522 bytes |
| Assets/Scripts/Script/OfficialCardListUtility.cs | RuntimeLogicRequired | 74892 bytes |
| Assets/Scripts/Script/ContinuousController.cs | RuntimeLogicRequired | 58898 bytes |
| Assets/Scripts/Script/CardEffectCommons.cs | RuntimeLogicRequired | 53992 bytes |
| Assets/Scripts/Script/Player.cs | RuntimeLogicRequired | 53022 bytes |
| Assets/Scripts/Script/EditDeck.cs | RuntimeLogicRequired | 46122 bytes |
| Assets/Scripts/CardEffect/EX3/Red/EX3_013.cs | CardEffectLogicRequired | 45533 bytes |
| Assets/Scripts/Script/SelectPermanentEffect.cs | RuntimeLogicRequired | 44417 bytes |
| Assets/Scripts/Script/SelectDigiXrosClass.cs | RuntimeLogicRequired | 42971 bytes |
| Assets/Scripts/Script/ProfanityFilter/ProfanityList.cs | RuntimeLogicRequired | 42886 bytes |
| Assets/Scripts/CardEffect/BT11/Blue/BT11_030.cs | CardEffectLogicRequired | 41968 bytes |
| Assets/Scripts/Script/AutoProcessing.cs | RuntimeLogicRequired | 41940 bytes |
| Assets/Scripts/Script/CardObjectController.cs | RuntimeLogicRequired | 41813 bytes |
| Assets/Scripts/CardEffect/BT10/Red/BT10_015.cs | CardEffectLogicRequired | 39777 bytes |
| Assets/Scripts/CardEffect/EX9/Black/EX9_073.cs | CardEffectLogicRequired | 39691 bytes |
| Assets/Scripts/Script/SelectCardEffect.cs | RuntimeLogicRequired | 39665 bytes |
| Assets/Scripts/CardEffect/BT20/Red/BT20_021.cs | CardEffectLogicRequired | 39348 bytes |
| Assets/Scripts/Script/SelectHandEffect.cs | RuntimeLogicRequired | 38063 bytes |
| Assets/Scripts/CardEffect/BT10/Blue/BT10_026.cs | CardEffectLogicRequired | 36958 bytes |
| Assets/Scripts/CardEffect/BT12/Purple/BT12_084.cs | CardEffectLogicRequired | 36691 bytes |
| Assets/Scripts/Script/ICardEffect.cs | RuntimeLogicRequired | 35868 bytes |
| Assets/Scripts/CardEffect/BT10/Red/BT10_012.cs | CardEffectLogicRequired | 35506 bytes |
| Assets/Scripts/CardEffect/BT21/Purple/BT21_074.cs | CardEffectLogicRequired | 35417 bytes |
| Assets/Scripts/Script/CardEffectCommons/RevealLibrary.cs | RuntimeLogicRequired | 35311 bytes |
| Assets/Scripts/Script/CardEffectCommons/DNADigivolveEffects.cs | RuntimeLogicRequired | 34405 bytes |
| Assets/Scripts/CardEffect/BT17/Blue/BT17_026.cs | CardEffectLogicRequired | 33427 bytes |
| Assets/Scripts/CardEffect/EX6/Purple/EX6_006.cs | CardEffectLogicRequired | 33340 bytes |
| Assets/Scripts/CardEffect/BT8/White/BT8_112.cs | CardEffectLogicRequired | 33005 bytes |
| Assets/Scripts/CardEffect/BT12/Purple/BT12_081.cs | CardEffectLogicRequired | 32665 bytes |
| Assets/Scripts/CardEffect/EX2/White/EX2_048.cs | CardEffectLogicRequired | 31850 bytes |
| Assets/Scripts/CardEffect/BT17/Red/BT17_095.cs | CardEffectLogicRequired | 31516 bytes |
| Assets/Scripts/Script/DataBase.cs | RuntimeLogicRequired | 31514 bytes |
| Assets/Scripts/CardEffect/EX6/Yellow/EX6_029.cs | CardEffectLogicRequired | 31338 bytes |
| Assets/Scripts/CardEffect/BT11/Black/BT11_071.cs | CardEffectLogicRequired | 31233 bytes |
| Assets/Scripts/CardEffect/BT12/Purple/BT12_111.cs | CardEffectLogicRequired | 30697 bytes |
| Assets/Scripts/CardEffect/BT13/White/BT13_112.cs | CardEffectLogicRequired | 30218 bytes |
| Assets/Scripts/CardEffect/BT7/Black/BT7_063.cs | CardEffectLogicRequired | 30051 bytes |
| Assets/Scripts/CardEffect/BT17/Blue/BT17_027.cs | CardEffectLogicRequired | 29999 bytes |
| Assets/Scripts/CardEffect/EX10/Purple/EX10_059.cs | CardEffectLogicRequired | 29949 bytes |
| Assets/Scripts/CardEffect/BT18/Black/BT18_073.cs | CardEffectLogicRequired | 29930 bytes |
| Assets/Scripts/CardEffect/BT11/Purple/BT11_088.cs | CardEffectLogicRequired | 29795 bytes |
| Assets/Scripts/CardEffect/BT18/Black/BT18_066.cs | CardEffectLogicRequired | 29599 bytes |
| Assets/Scripts/Script/HandCard.cs | RuntimeLogicRequired | 29427 bytes |
| Assets/Scripts/CardEffect/EX9/Blue/EX9_018.cs | CardEffectLogicRequired | 29118 bytes |
| Assets/Scripts/CardEffect/BT10/White/BT10_086.cs | CardEffectLogicRequired | 28541 bytes |

## 다음 Goal 추천
- GOAL 08에서는 Required/CandidateReview 범위를 기존 src headless 구현과 비교하되, 기존 구현 결과를 신뢰하지 않고 SourceOfTruth mapping evidence로 재검증한다.
- Assets/Scripts/Script runtime 공통 로직, Assets/Scripts/CardEffect source, Assets/CardBaseEntity 카드 데이터의 대응 여부를 별도 축으로 나눈다.
- GOAL 05 call graph의 Unity/GManager/Photon/UI/Coroutine tag는 구현 우선순위가 아니라 의존성 제거 및 trust audit 리스크 지표로 사용한다.
- GOAL 06의 missing effect source candidate 39건과 duplicate CardID group은 구현 전에 데이터/variant 정책 후보로 감사한다.
- VisualOnly/SoundOnly/GeneratedCache/BuildArtifact는 GOAL 08 headless 구현 감사 기본 범위에서 제외하되, UnityAdapter 목표에서 다시 열 수 있다.

## 금지 사항 준수
- `src/` C# 코드 수정 없음.
- 원본 `DCGO/Assets` 수정 없음.
- CardEffect body 구현 없음.
- C0039 이후 card-porting 실행 없음.
- commit/push 없음.
