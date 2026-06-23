# GOAL 02 AS-IS Role Reclassification

> 이 문서는 GOAL 01 AS-IS 파일 인벤토리만 입력으로 사용해 파일/폴더 역할을 재분류한 기준선이다. 원본 파일 본문, C# 코드 의미, 함수 호출, call graph, 구현 분석은 수행하지 않았다.

## 입력 기준

- AS-IS root 경로: `E:\headlessDCGO\DCGO`
- 입력 inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`
- GOAL 01 생성 시각 UTC: `2026-06-23T03:57:48.2045932+00:00`
- GOAL 02 생성 시각 UTC: `2026-06-23T04:18:04.9284170+00:00`
- 원본 파일 본문 읽기: `false`
- 코드/함수/call graph 분석: `false`
- 분류 방식: GOAL 01 경로, 확장자, 기존 파일 유형 분류 기반 다중 role + 단일 primaryRole

## 전체 요약

| 항목 | 값 |
| --- | --- |
| 재분류 파일 수 | 71180 |
| 재분류 폴더 수(root 포함) | 4798 |
| GOAL 01 폴더 수(root 제외) | 4797 |
| Unknown 파일 수 | 0 |
| Unknown 폴더 수 | 0 |
| SourceOfTruth 파일 수 | 26884 |
| SourceOfTruth 폴더 수 | 1805 |

## Role 정책

| Role | 정책 |
| --- | --- |
| SourceOfTruth | 직접 이식 기준선으로 취급할 원본 게임 코드/카드 데이터. 현재는 Assets/Scripts, Assets/CardBaseEntity 및 해당 .meta sidecar 경로에 한정한다. |
| UnityProjectSource | Unity 프로젝트가 소유한 편집 가능한 프로젝트 파일, 설정, 에셋, 루트 메타데이터. 생성 캐시/빌드/로그/명시 외부 패키지는 primary에서 제외한다. |
| GeneratedCache | Unity가 재생성할 수 있는 Library/Temp 계열 캐시와 중간 데이터. |
| BuildArtifact | Client 빌드 출력 및 Library/Bee, Library/Artifacts, Library/ScriptAssemblies 등 컴파일/플레이어 빌드 산출물. |
| RuntimeOutput | Logs, Temp 및 로그 파일처럼 실행 중 생성된 출력. |
| ExternalPackage | Assets/Plugins, Photon, TextMesh Pro, SCI-FI UI Components, Library/PackageCache, Packages 내 embedded 후보, lfs object store처럼 외부 의존성 또는 외부 바이너리 성격의 항목. |
| VisualOnly | GOAL 01의 Image/VisualOnlyCandidate/Shader/Font 또는 이미지·모델·머티리얼·애니메이션·셰이더·폰트 sidecar 확장자 근거가 있는 항목. |
| SoundOnly | GOAL 01의 Sound/SoundOnlyCandidate 또는 사운드 sidecar 확장자 근거가 있는 항목. |
| Unknown | 위 규칙으로 보수적으로 분류할 수 없는 항목. |

## 파일 role별 수

다중 role을 허용하므로 role별 합계는 전체 파일 수와 다를 수 있다.

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

## 폴더 role별 수

root 폴더 `.`를 포함한다.

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

## 상위 폴더별 파일 primaryRole 수

| 상위 폴더::PrimaryRole | 파일 수 |
| --- | --- |
| Assets::SourceOfTruth | 26884 |
| Library::GeneratedCache | 13441 |
| Library::BuildArtifact | 13041 |
| Client::BuildArtifact | 7967 |
| Assets::ExternalPackage | 5179 |
| Assets::VisualOnly | 3065 |
| lfs::ExternalPackage | 762 |
| Assets::UnityProjectSource | 631 |
| Assets::SoundOnly | 102 |
| ProjectSettings::UnityProjectSource | 29 |
| Temp::RuntimeOutput | 27 |
| Logs::RuntimeOutput | 24 |
| .idea::UnityProjectSource | 8 |
| (root)::UnityProjectSource | 6 |
| hooks::UnityProjectSource | 4 |
| .vscode::UnityProjectSource | 3 |
| .github::UnityProjectSource | 2 |
| Packages::UnityProjectSource | 2 |
| UserSettings::UnityProjectSource | 2 |
| Assets::RuntimeOutput | 1 |

## Unknown 파일 목록

- 없음

## Unknown 폴더 목록

- 없음

## SourceOfTruth 범위

- SourceOfTruth 파일 전체 목록은 `docs/generated/as-is-restart/asis-role-reclassification.json`의 `files`에서 `roles`에 `SourceOfTruth`가 포함된 항목으로 확인한다.
- 아래 목록은 문서 가독성을 위한 상위 200개 샘플이다.

- `Assets/CardBaseEntity/AD1.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Black.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Black/Tamer.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_010.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_010.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_011.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_011.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_013.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_013.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024_P2.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024_P2.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Tamer.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_019.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_019.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_020.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_020.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_020_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_020_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Green.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Green/AD1_022.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Green/AD1_022.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Purple.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Purple/Digimon.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Purple/Digimon/AD1_018.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Purple/Digimon/AD1_018.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Purple/Digimon/AD1_018_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Purple/Digimon/AD1_018_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_003.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_003.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_015.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_015.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_015_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_015_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_017.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_017.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_017_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_017_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Tamer.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/DigiEgg.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/DigiEgg/BT1_003.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/DigiEgg/BT1_003.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/DigiEgg/BT1_003_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/DigiEgg/BT1_003_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/DigiEgg/BT1_003_P2.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/DigiEgg/BT1_003_P2.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/DigiEgg/BT1_003_P3.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/DigiEgg/BT1_003_P3.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_003_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_003_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_003_P2.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_003_P2.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_004.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_004.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_027.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_027.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_028.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_028.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_028_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_028_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P2.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P2.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P3.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P3.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P4.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P4.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P5.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P5.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_030.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_030.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_031.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_031.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_032.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_032.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_033.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_033.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_034.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_034.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_035.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_035.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_035_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_035_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_035_P2.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_035_P2.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_036.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_036.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_037.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_037.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_037_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_037_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_038.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_038.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_038_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_038_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_038_P2.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_038_P2.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_038_P3.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_038_P3.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_039.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_039.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_040.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_040.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_041.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_041.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_042.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_042.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_043.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_043.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_044.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_044.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_044_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_044_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_115.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_115.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_115_P1.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_115_P1.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_115_P2.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_115_P2.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Option.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Option/BT1_096.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Option/BT1_096.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Option/BT1_097.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Option/BT1_097.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Option/BT1_098.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Option/BT1_098.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Option/BT1_099.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Option/BT1_099.asset.meta` (SourceOfTruth; SourceOfTruth, UnityProjectSource)
- `Assets/CardBaseEntity/BT1/Blue/Option/BT1_100.asset` (SourceOfTruth; SourceOfTruth, UnityProjectSource)

## 분석 제외와 제한

- GOAL 01 inventory에 포함된 파일/폴더는 모두 재분류했다.
- GOAL 01의 excluded entry는 `0`개였으므로 GOAL 02에서 별도 제외 항목은 없다.
- VisualOnly/SoundOnly는 경로와 확장자 기반 역할 분류이며, Unity scene/prefab 내부 참조 의미는 분석하지 않았다.
- ExternalPackage는 경로 기반 후보 분류이며, package manifest dependency graph는 분석하지 않았다.

## 다음 Goal에서 분석해야 할 후보 범위

- SourceOfTruth로 분류된 Assets/Scripts와 Assets/CardBaseEntity의 세부 하위 폴더별 구조 분석
- GeneratedCache/BuildArtifact/RuntimeOutput 범위를 재현 가능한 AS-IS snapshot에서 제외할지 여부 결정
- ExternalPackage로 분류된 Assets/Plugins, Photon, TextMesh Pro, Library/PackageCache 의존성 경계 분석
- VisualOnly/SoundOnly 항목 중 headless engine에 불필요한 에셋 exclusion 정책 수립
- Unknown이 0으로 유지되는지 후속 inventory 정책 검증

## 금지 범위 준수

- `src/` 아래 C# 코드는 수정하지 않았다.
- headless engine 구현은 수행하지 않았다.
- 원본 `DCGO/Assets`는 수정하지 않았다.
- CardEffect body 구현은 수행하지 않았다.
- C0039 이후 card-porting은 실행하지 않았다.
- 함수 호출 분석과 call graph 생성은 수행하지 않았다.
- Foundation Gate 수치 조작과 generated status 승격은 수행하지 않았다.
- OpenCode task, RL Environment, Observation, Reward, Dataset, Trainer 구현은 수행하지 않았다.
- commit/push는 수행하지 않았다.
