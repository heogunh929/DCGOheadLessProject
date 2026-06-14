# 01 - 이식 분석 문서 생성

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "01_create_porting_docs.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

목표:
DCGO battle 구현을 RL.Engine으로 이식하기 위한 분석 문서를 작성한다.
아직 구현 코드는 만들지 않는다.

먼저 아래 원본 파일을 읽고 분석하라.

- `DCGO/Assets/Scripts/Script/GameContext.cs`
- `DCGO/Assets/Scripts/Script/Player.cs`
- `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- `DCGO/Assets/Scripts/Script/CardController.cs`
- `DCGO/Assets/Scripts/Script/AutoProcessing.cs`
- `DCGO/Assets/Scripts/Script/AttackProcess.cs`
- `DCGO/Assets/Scripts/Script/CEntity_Base.cs`
- `DCGO/Assets/Scripts/Script/CardSource.cs`
- `DCGO/Assets/Scripts/Script/Permanent.cs`
- `DCGO/Assets/Scripts/Script/MainPhaseAction/*.cs`
- `DCGO/Assets/Scripts/Script/CardEffectCommons.cs`
- `DCGO/Assets/Scripts/Script/ICardEffect.cs`

생성할 문서:

- `docs/rl-engine/porting-overview.md`
- `docs/rl-engine/source-mapping.md`
- `docs/rl-engine/battle-flow.md`
- `docs/rl-engine/decision-and-selection.md`
- `docs/rl-engine/validation-strategy.md`
- `docs/rl-engine/complex-mechanics.md`
- `docs/rl-engine/cardeffect-porting-plan.md`
- `docs/rl-engine/unity-adapter-plan.md`

문서에 반드시 포함할 내용:

1. 원본 battle 흐름 요약
2. 원본 클래스와 RL.Engine 클래스의 매핑
3. GameContext, Player, CardSource, Permanent, TurnStateMachine, CardController, AutoProcessing, AttackProcess의 책임
4. Minimal Playable Battle의 정확한 정의
5. Tier1 공통 primitive 목록과 각 primitive가 필요한 이유
6. 개별 CardEffect 포팅을 나중으로 미루기 위한 인터페이스 설계 방향
7. DecisionPoint / LegalAction / SelectionRequest / SelectionResult 구조
8. CLI에서 선택지를 표시하는 방식
9. UnityAdapter가 나중에 선택지를 기존 그래픽/UI 위에 표시하는 방식
10. UnityAdapter가 Bot 역할을 대체하는 장기 방향
11. 기존 Unity 코드에서 제거해야 할 의존성
12. 원본과 다르게 처리해야 할 가능성이 있는 부분과 위험 요소
13. 테스트 전략
14. 이식 우선순위
15. 엔진 완성 판정 checklist의 위치

금지:
- 기존 Unity 파일 수정 금지
- 구현 클래스 생성 금지
- 개별 CardEffect 포팅 금지

완료 후:
- 생성한 파일 목록
- 주요 결론
- 다음 구현 단계 제안
을 한국어로 요약하라.

