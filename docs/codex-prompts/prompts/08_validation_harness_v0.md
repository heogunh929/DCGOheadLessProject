# 08 - Validation Harness v0

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "08_validation_harness_v0.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이 작업은 학습용 RL Environment가 아니다.
이식된 엔진의 deterministic 검증과 invariant 검증을 위한 최소 Validation Harness를 만든다.

목표:
초기 상태 모델, 카드 모델, ZoneMover, Decision/Selection 구조를 검증할 수 있는 도구를 만든다.

구현할 것:
- StateHasher
- TraceEvent
- GameTrace
- ReplayRunner skeleton
- EngineInvariantChecker
- ScriptedScenarioRunner skeleton
- UnsupportedMechanicReporter
- DeckValidationReport skeleton

필수 invariant:
- 한 CardInstance가 여러 public zone에 동시에 존재하지 않는다.
- CardInstance.CurrentZone과 실제 zone 소유가 일치한다.
- PermanentState의 TopCardId/SourceCardIds/LinkedCardIds가 중복 소유되지 않는다.
- invalid state를 EngineInvariantChecker가 감지한다.

금지:
- ObservationEncoder 구현 금지
- RewardCalculator 구현 금지
- DatasetExporter 구현 금지
- 학습 루프 구현 금지

테스트:
- 정상 state invariant 통과
- 중복 zone 카드 invariant 실패
- 잘못된 CurrentZone invariant 실패
- StateHasher가 동일 상태에 동일 hash를 반환
- TraceEvent 기록 테스트
- ReplayRunner skeleton이 빈 trace를 처리

완료 후:
- 구현한 검증 도구
- 아직 학습용으로 구현하지 않은 항목
- 실행한 테스트
를 한국어로 요약하라.

