# DCGO Assets AS-IS Tree

- 생성 시각: `2026-06-23T05:37:16+09:00`

- 원본 경로: `E:\headlessDCGO\DCGO`

- 목적: RL.Engine 포팅 전 Assets 하위 영역을 headless 필요도와 UnityAdapter 필요도로 1차 분류한다.

| Path | Category | RL.Engine 필요 여부 | UnityAdapter 필요 여부 | 설명 |
| --- | --- | --- | --- | --- |
| DCGO/Assets | 전체 Assets 루트 | 간접 필요 (35862 files) | 필요 | 원본 Unity 자산 전체. headless에는 선별 이식만 허용한다. |
| DCGO/Assets/3DObjects | 이미지/연출/UI/audio/prefab | 불필요 (2 files) | 필요 | 그래픽/연출/UI 자산. RL.Engine에는 직접 넣지 않는다. |
| DCGO/Assets/AddOn | UnityAdapter/Unity runtime 의존 | 불필요 (1115 files) | 필요 가능 | Unity 런타임, 네트워크, 플러그인 또는 표시 관련 자산 |
| DCGO/Assets/Animation | 이미지/연출/UI/audio/prefab | 불필요 (322 files) | 필요 | 그래픽/연출/UI 자산. RL.Engine에는 직접 넣지 않는다. |
| DCGO/Assets/CardBaseEntity | 카드 데이터 asset | 필요 (17753 files) | 필요 | 카드 정의 ScriptableObject asset. CardId, 이름, 비용, 색, 효과 클래스 연결 근거 |
| DCGO/Assets/Editor | Editor/데이터 정리 | 불필요 (374 files) | 검토 | 개발/정리용 스크립트 성격. 런타임 포팅 대상이 아니다. |
| DCGO/Assets/Effect | UnityAdapter/Unity runtime 의존 | 불필요 (918 files) | 필요 가능 | Unity 런타임, 네트워크, 플러그인 또는 표시 관련 자산 |
| DCGO/Assets/Image | 이미지/연출/UI/audio/prefab | 불필요 (547 files) | 필요 | 그래픽/연출/UI 자산. RL.Engine에는 직접 넣지 않는다. |
| DCGO/Assets/Materials | 이미지/연출/UI/audio/prefab | 불필요 (2 files) | 필요 | 그래픽/연출/UI 자산. RL.Engine에는 직접 넣지 않는다. |
| DCGO/Assets/Photon | UnityAdapter/Unity runtime 의존 | 불필요 (950 files) | 필요 가능 | Unity 런타임, 네트워크, 플러그인 또는 표시 관련 자산 |
| DCGO/Assets/Plugins | UnityAdapter/Unity runtime 의존 | 불필요 (41 files) | 필요 가능 | Unity 런타임, 네트워크, 플러그인 또는 표시 관련 자산 |
| DCGO/Assets/Prefab | 이미지/연출/UI/audio/prefab | 불필요 (173 files) | 필요 | 그래픽/연출/UI 자산. RL.Engine에는 직접 넣지 않는다. |
| DCGO/Assets/Presets | 기타 | 검토 필요 (15 files) | 검토 필요 | 세부 파일 단위 분류 필요 |
| DCGO/Assets/RenderTexture | 이미지/연출/UI/audio/prefab | 불필요 (12 files) | 필요 | 그래픽/연출/UI 자산. RL.Engine에는 직접 넣지 않는다. |
| DCGO/Assets/Resources | UnityAdapter/Unity runtime 의존 | 불필요 (127 files) | 필요 가능 | Unity 런타임, 네트워크, 플러그인 또는 표시 관련 자산 |
| DCGO/Assets/Scenes | 이미지/연출/UI/audio/prefab | 불필요 (8 files) | 필요 | 그래픽/연출/UI 자산. RL.Engine에는 직접 넣지 않는다. |
| DCGO/Assets/SCI-FI UI Components | 이미지/연출/UI/audio/prefab | 불필요 (4124 files) | 필요 | 그래픽/연출/UI 자산. RL.Engine에는 직접 넣지 않는다. |
| DCGO/Assets/Scripts | 기타 | 검토 필요 (9131 files) | 검토 필요 | 세부 파일 단위 분류 필요 |
| DCGO/Assets/ScriptTemplates | Editor/데이터 정리 | 불필요 (2 files) | 검토 | 개발/정리용 스크립트 성격. 런타임 포팅 대상이 아니다. |
| DCGO/Assets/Settings | UnityAdapter/Unity runtime 의존 | 불필요 (10 files) | 필요 가능 | Unity 런타임, 네트워크, 플러그인 또는 표시 관련 자산 |
| DCGO/Assets/Shader_Material | 이미지/연출/UI/audio/prefab | 불필요 (46 files) | 필요 | 그래픽/연출/UI 자산. RL.Engine에는 직접 넣지 않는다. |
| DCGO/Assets/Sound | 이미지/연출/UI/audio/prefab | 불필요 (102 files) | 필요 | 그래픽/연출/UI 자산. RL.Engine에는 직접 넣지 않는다. |
| DCGO/Assets/TextMesh Pro | UnityAdapter/Unity runtime 의존 | 불필요 (64 files) | 필요 가능 | Unity 런타임, 네트워크, 플러그인 또는 표시 관련 자산 |
| DCGO/Assets/Scripts/Script | 실제 플레이 로직 C# 코드 | 필요 (896 files) | 부분 필요 | 게임 상태, 진행, 선택, 효과 실행 기반이 섞인 핵심 원본 코드 |
| DCGO/Assets/Scripts/CardEffect | 카드별 효과 코드 | 나중에 필요 (8233 files) | 부분 필요 | 카드 단위 효과 구현 원본. 이번 작업에서는 body 구현 대상이 아니다. |
| DCGO/Assets/Scripts/Script/CardEffectCommons | 실제 플레이 로직 C# 코드 | 필요 (253 files) | 부분 필요 | 게임 상태, 진행, 선택, 효과 실행 기반이 섞인 핵심 원본 코드 |
| DCGO/Assets/Scripts/Script/CardEffectFactory | 실제 플레이 로직 C# 코드 | 필요 (119 files) | 부분 필요 | 게임 상태, 진행, 선택, 효과 실행 기반이 섞인 핵심 원본 코드 |
| DCGO/Assets/Scripts/Script/MainPhaseAction | 실제 플레이 로직 C# 코드 | 필요 (14 files) | 부분 필요 | 게임 상태, 진행, 선택, 효과 실행 기반이 섞인 핵심 원본 코드 |

## 요약

- `Assets/Scripts/Script`는 실제 플레이 로직과 Unity UI/Coroutine 의존이 섞인 핵심 분석 대상이다.

- `Assets/Scripts/CardEffect`는 카드별 효과 구현 원본이지만 이번 작업에서는 body 구현 대상이 아니다.

- `Assets/CardBaseEntity`는 CardEffectClassName과 카드 메타데이터 연결을 위해 RL.Engine에 필요한 데이터 근거다.

- 이미지, 사운드, 프리팹, Photon, UI 플러그인은 RL.Engine 직접 포팅 대상이 아니며 UnityAdapter 경계로 분리해야 한다.
