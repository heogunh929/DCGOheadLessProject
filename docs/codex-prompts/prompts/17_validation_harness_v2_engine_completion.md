# 17 - Validation Harness v2 / 엔진 완성 판정

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "17_validation_harness_v2_engine_completion.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이 작업은 학습 전 최종 엔진 검증 단계다.
아직 ObservationEncoder, RewardCalculator, DatasetExporter, trainer를 구현하지 않는다.

목표:
학습 대상 카드풀 기준으로 엔진 완성 여부를 판정할 수 있는 Validation Harness를 강화한다.

구현/보강할 것:
- EngineCompletionChecklistRunner
- FullCardPoolValidationReport 또는 TargetCardPoolValidationReport
- ScenarioSuiteRunner
- ReplayDeterminismRunner
- RandomLegalActionFuzzRunner
- InvariantFuzzRunner
- UnsupportedMechanicZeroCheck
- GoldenScenario 문서/fixture

검증 항목:
- Unity/Photon 의존성 없음
- zone invariant
- permanent invariant
- deterministic replay
- deck-out loss
- security 0 direct attack win
- DP battle
- normal play/digivolve
- memory crossing turn end
- complex mechanics 지원 여부
- battle keyword 지원 여부
- 대상 card pool CardEffect 포팅 상태
- unsupported mechanic 없음 또는 대상 카드풀에서 제외
- LegalActionGenerator action은 ActionExecutor에서 실패하지 않음
- invalid action은 명확히 실패

문서:
- `docs/rl-engine/engine-completion-checklist.md` 업데이트
- `docs/rl-engine/validation-report-template.md` 생성
- `docs/rl-engine/golden-scenarios.md` 생성 또는 업데이트

금지:
- 학습용 RL Environment 구현 금지
- ObservationEncoder 구현 금지
- RewardCalculator 구현 금지
- DatasetExporter 구현 금지
- trainer 구현 금지

테스트:
- completion checklist runner 테스트
- 대상 card pool validation 테스트
- replay determinism 테스트
- invariant fuzz 테스트
- unsupported mechanic zero check 테스트

완료 후:
- 엔진 완성 여부
- 통과/실패 checklist
- 학습 단계로 넘어가기 전 남은 작업
을 한국어로 요약하라.

