중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적인 validation failure로 실패시킨다.

그래픽 UI는 RL.Engine에 이식하지 않는다.
대신 RL.Engine은 `DecisionPoint`, `LegalAction`, `SelectionRequest`, `SelectionResult`를 생성한다.
CLI는 이를 텍스트로 표시하고, UnityAdapter는 이를 기존 Unity 그래픽/UI로 표시한다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.
ObservationEncoder, RewardCalculator, DatasetExporter, 학습 루프는 엔진 완성 후 별도 단계에서만 구현한다.
