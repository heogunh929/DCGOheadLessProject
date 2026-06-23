# AS-IS Role Reclassification Summary

> 이 문서는 GOAL 01 AS-IS 파일 인벤토리만 입력으로 사용해 파일/폴더 역할을 재분류한 기준선이다. 원본 파일 본문, C# 코드 의미, 함수 호출, call graph, 구현 분석은 수행하지 않았다.

## 기준선

- AS-IS root 경로: `E:\headlessDCGO\DCGO`
- 입력 inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`
- 재분류 파일 수: `71180`
- 재분류 폴더 수(root 포함): `4798`
- Unknown 파일 수: `0`
- Unknown 폴더 수: `0`

## 파일 primaryRole별 수

| PrimaryRole | 파일 수 |
| --- | --- |
| SourceOfTruth | 26884 |
| BuildArtifact | 21008 |
| GeneratedCache | 13441 |
| ExternalPackage | 5941 |
| VisualOnly | 3065 |
| UnityProjectSource | 687 |
| SoundOnly | 102 |
| RuntimeOutput | 52 |

## 폴더 primaryRole별 수

| PrimaryRole | 폴더 수 |
| --- | --- |
| SourceOfTruth | 1805 |
| ExternalPackage | 1159 |
| GeneratedCache | 1154 |
| BuildArtifact | 303 |
| VisualOnly | 256 |
| UnityProjectSource | 111 |
| RuntimeOutput | 7 |
| SoundOnly | 3 |

## 파일 role별 수

| Role | 파일 수 |
| --- | --- |
| UnityProjectSource | 35918 |
| SourceOfTruth | 26884 |
| GeneratedCache | 26509 |
| BuildArtifact | 21008 |
| ExternalPackage | 18727 |
| VisualOnly | 17162 |
| SoundOnly | 126 |
| RuntimeOutput | 52 |

## 폴더 role별 수

| Role | 폴더 수 |
| --- | --- |
| UnityProjectSource | 2328 |
| ExternalPackage | 2040 |
| SourceOfTruth | 1805 |
| GeneratedCache | 1431 |
| BuildArtifact | 303 |
| VisualOnly | 256 |
| RuntimeOutput | 7 |
| SoundOnly | 3 |

## Unknown

- Unknown 파일: `0`
- Unknown 폴더: `0`

## 다음 Goal 추천

- SourceOfTruth로 분류된 Assets/Scripts와 Assets/CardBaseEntity의 세부 하위 폴더별 구조 분석
- GeneratedCache/BuildArtifact/RuntimeOutput 범위를 재현 가능한 AS-IS snapshot에서 제외할지 여부 결정
- ExternalPackage로 분류된 Assets/Plugins, Photon, TextMesh Pro, Library/PackageCache 의존성 경계 분석
- VisualOnly/SoundOnly 항목 중 headless engine에 불필요한 에셋 exclusion 정책 수립
- Unknown이 0으로 유지되는지 후속 inventory 정책 검증
