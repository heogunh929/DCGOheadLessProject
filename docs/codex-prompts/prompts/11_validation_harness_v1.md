# 11 - Validation Harness v1

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "11_validation_harness_v1.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이 작업은 학습용 환경 구현이 아니라 Minimal Playable Battle 검증 강화 작업이다.

목표:
Minimal Playable Battle을 scripted scenario와 random legal action smoke로 검증한다.

구현/보강할 것:
- ScriptedScenarioRunner
- RandomLegalActionRunner
- ScenarioResult
- MaxTurnAbortResult
- Trace save/load
- invariant check after every action
- replay determinism helper
- CLI debug renderer skeleton

필수 scenario:
- deck-out loss
- security 0 direct attack win
- lower DP Digimon deleted
- equal DP battle 처리
- normal digivolve draw
- memory crossing turn end
- hatch then move from breeding

금지:
- ObservationEncoder 구현 금지
- RewardCalculator 구현 금지
- DatasetExporter 구현 금지
- 학습 루프 구현 금지

테스트:
- 각 scripted scenario 통과
- random legal action run이 crash 없이 종료 또는 max-turn abort
- 매 action 후 invariant check 실행
- trace replay가 동일 final StateHash를 재현

완료 후:
- 추가한 scenario 목록
- 검증 결과
- 아직 학습용으로 구현하지 않은 항목
를 한국어로 요약하라.

