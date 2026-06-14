# 18 - 엔진 완성 후 RL Environment 구현

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "18_rl_environment_after_engine_complete.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이 작업은 엔진 완성 판정 후에만 수행한다.
이제부터 생성되는 self-play 데이터는 학습 데이터로 사용할 수 있다.

전제 조건:
- `17_validation_harness_v2_engine_completion.md` 단계가 통과되어야 한다.
- 학습 대상 카드풀에서 unsupported mechanic이 없어야 한다.
- replay determinism과 invariant 검증이 통과되어야 한다.

목표:
완성된 RL.Engine을 강화학습 환경으로 노출한다.

구현할 것:
- IRlGameEnvironment
- RlGameEnvironment
- Observation
- ObservationEncoder
- ActionEncoder
- LegalActionMask
- RewardCalculator
- SelfPlayDatasetExporter
- EpisodeBatchRunner
- PolicyInference interface placeholder

필수 API:
```csharp
public interface IRlGameEnvironment
{
    Observation Reset(GameConfig config, int seed);
    IReadOnlyList<GameAction> LegalActions();
    StepResult Step(GameAction action);
    GameState Snapshot();
}
```

Observation 최소 포함:
- phase
- memory
- turn player
- security count
- deck count
- hand count
- visible field state
- visible trash state
- current player's hand card IDs
- pending SelectionRequest 정보
- legal action metadata

Reward 기본값:
- win +1
- loss -1
- draw/max-turn abort 0
- reward shaping은 옵션으로 분리

테스트:
- Reset deterministic
- Step deterministic
- LegalActionMask와 LegalActions 일치
- 완성 카드풀 self-play episode 생성
- replay deterministic
- reward 검증
- dataset export/import 검증

완료 후 한국어로 요약하라.

