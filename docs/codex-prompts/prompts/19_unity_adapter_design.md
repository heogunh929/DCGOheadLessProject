# 19 - UnityAdapter 설계 문서

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "19_unity_adapter_design.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이 작업은 RL.Engine을 Unity DCGO에 연결하기 위한 Adapter 설계 작업이다.
아직 실제 UnityAdapter 구현은 최소화하고, 연결 지점과 위험 요소를 문서화한다.

참조 원본:
- `DCGO/Assets/Scripts/Script/GManager.cs`
- `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- `DCGO/Assets/Scripts/Script/Player.cs`
- `DCGO/Assets/Scripts/Script/MainPhaseAction/*.cs`
- `DCGO/Assets/Scripts/Script/SelectCardEffect.cs`
- `DCGO/Assets/Scripts/Script/SelectPermanentEffect.cs`
- `DCGO/Assets/Scripts/Script/SelectCardPanel.cs`
- `DCGO/Assets/Scripts/Script/AttackProcess.cs`

목표:
RL.Engine을 나중에 Unity Bot으로 붙이기 위한 UnityAdapter 설계를 작성한다.

문서 생성:
- `docs/rl-engine/unity-adapter-plan.md`
- `docs/rl-engine/action-mapping.md`
- `docs/rl-engine/state-exporter.md`
- `docs/rl-engine/selection-ui-mapping.md`

문서에 포함:
1. Unity GameContext/Player/CardSource/Permanent를 RL GameState로 export하는 방식
2. Unity CardSource와 RL CardInstanceId 매핑 방식
3. Unity Permanent와 RL PermanentId 매핑 방식
4. RL GameAction을 기존 MainPhaseAction으로 변환하는 방식
5. main phase action queue와 연결하는 방식
6. breeding phase 선택 연결 방식
7. SelectionRequest를 SelectCardPanel/SelectPermanentEffect/outline UI로 표시하는 방식
8. SelectionResult를 Unity callback/selection queue로 변환하는 방식
9. Bot/RL Agent가 UI 없이 선택 결과를 주입하는 방식
10. 사람 플레이어에게는 기존 Unity 그래픽 선택지를 유지하는 방식
11. 현재 기존 Bot 로직을 대체할 수 있는 지점
12. UnityAdapter가 직접 battle rule을 재구현하면 안 된다는 원칙
13. UnityAdapter는 상태 변환, UI 표시, action 주입만 담당한다는 원칙
14. deterministic 검증이 어려운 부분
15. Adapter 통합 전 필요한 RL.Engine 테스트

금지:
- 기존 battle 동작 수정 금지
- 기존 Bot 제거 금지
- UnityAdapter 실제 구현은 이 작업에서 최소화
- RL.Engine에 UnityEngine 참조 추가 금지

완료 후 한국어로 요약하라.

