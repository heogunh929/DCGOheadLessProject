# 20 - UnityAdapter 최소 구현

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "20_unity_adapter_minimal_impl.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이 작업은 UnityAdapter의 최소 연결부를 구현하는 작업이다.
UnityAdapter는 battle rule을 재구현하지 않는다.
RL.Engine에 UnityEngine 참조를 추가하지 않는다.

전제:
- RL.Engine이 엔진 완성 판정을 통과했거나, 최소한 UnityAdapter 실험이 가능한 상태여야 한다.
- `docs/rl-engine/unity-adapter-plan.md`
- `docs/rl-engine/action-mapping.md`
- `docs/rl-engine/state-exporter.md`
- `docs/rl-engine/selection-ui-mapping.md`

참조 원본:
- `DCGO/Assets/Scripts/Script/GManager.cs`
- `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- `DCGO/Assets/Scripts/Script/Player.cs`
- `DCGO/Assets/Scripts/Script/MainPhaseAction/*.cs`

목표:
기존 Unity battle 상태를 RL.Engine의 의사결정 입력으로 변환하고, RL action을 기존 Unity action queue로 주입할 수 있는 최소 Adapter를 구현한다.

구현할 것:
- `src/DCGO.RL.UnityAdapter/` 또는 Unity Assets 하위 adapter 위치를 검토하고 제안
- UnityStateExporter
- UnityRlObjectMap
- UnityActionMapper
- UnitySelectionMapper
- RlBotController placeholder

동작:
- Unity GameContext/Player/CardSource/Permanent에서 RL GameState snapshot 생성
- RL GameAction을 기존 MainPhaseAction으로 변환 가능한 경우 변환
- 변환 불가능한 action은 명시적 adapter exception 발생
- SelectionRequest를 Unity UI에 표시하는 실제 구현은 최소 placeholder 또는 별도 TODO로 둔다
- Bot 모드에서는 SelectionRequest를 UI 없이 선택 결과로 주입할 수 있는 경계만 만든다

주의:
- 기존 battle 동작을 바꾸지 않는다.
- 기존 Bot 제거 금지.
- UnityAdapter는 RL.Engine을 참조할 수 있지만, RL.Engine은 UnityAdapter를 참조하면 안 된다.
- 기존 Unity 프로젝트가 컴파일되도록 주의한다.

테스트/검증:
- 가능한 범위의 adapter 단위 테스트 작성
- 컴파일 가능한지 확인
- 문서에 수동 테스트 절차 작성

완료 후:
- 변경 파일
- Unity와 RL.Engine 사이의 의존 방향
- 아직 구현하지 않은 UI mapping
- 수동 테스트 절차
를 한국어로 요약하라.

