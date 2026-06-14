# 07 - Decision / Selection 구조 이식

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "07_decision_selection.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이 작업은 Unity 그래픽 선택 UI를 RL.Engine으로 옮기는 작업이 아니다.
선택의 의미를 headless 데이터 구조로 이식하는 작업이다.

참조 원본:
- `DCGO/Assets/Scripts/Script/Player.cs`
- `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- `DCGO/Assets/Scripts/Script/SelectCardEffect.cs`
- `DCGO/Assets/Scripts/Script/SelectPermanentEffect.cs`
- `DCGO/Assets/Scripts/Script/SelectCardPanel.cs`
- `DCGO/Assets/Scripts/Script/MainPhaseAction/*.cs`

목표:
CLI, Validation Harness, RL Agent, UnityAdapter가 모두 공유할 수 있는 선택 구조를 구현한다.

구현할 것:
- DecisionPoint
- DecisionKind
- GameAction metadata
- LegalAction
- SelectionRequest
- SelectionKind
- SelectableTarget
- SelectionResult
- IDecisionProvider
- TestDecisionProvider
- Selection validation helper

SelectionKind에는 최소한 포함:
- ChooseAction
- SelectCard
- SelectPermanent
- SelectSecurity
- SelectFieldSlot
- SelectCount
- SelectYesNo
- SelectOrder

규칙:
- RL.Engine은 Unity UI를 호출하지 않는다.
- RL.Engine은 선택 후보와 제약을 SelectionRequest로 표현한다.
- CLI는 SelectionRequest를 텍스트 목록으로 표시할 수 있어야 한다.
- UnityAdapter는 나중에 SelectionRequest를 SelectCardPanel, SelectPermanentEffect, outline, 클릭 이벤트로 표시한다.
- Bot/RL Agent는 SelectionRequest를 UI 없이 선택할 수 있다.
- 선택 결과는 SelectionResult로 검증되어야 한다.

문서:
- `docs/rl-engine/decision-and-selection.md` 업데이트
- CLI 표시 예시와 UnityAdapter 표시 예시를 포함한다.

테스트:
- SelectionRequest 생성 테스트
- 후보 수/MinCount/MaxCount 검증 테스트
- skip 가능한 선택 테스트
- invalid target 선택 시 실패 테스트
- LegalAction에서 SelectionRequest로 변환 가능한 케이스 테스트
- CLI 표시용 DebugLabel 생성 테스트

완료 후:
- 원본 Unity 선택 UI와 SelectionRequest의 대응 관계를 한국어로 정리하라.

