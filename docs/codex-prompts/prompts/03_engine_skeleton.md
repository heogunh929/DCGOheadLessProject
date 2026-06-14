# 03 - RL.Engine 프로젝트 골격

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "03_engine_skeleton.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

참조 문서:
- `docs/rl-engine/porting-overview.md`
- `docs/rl-engine/source-mapping.md`
- `docs/rl-engine/battle-flow.md`
- `docs/rl-engine/decision-and-selection.md`

참조 원본:
- `DCGO/Assets/Scripts/Script/GameContext.cs`
- `DCGO/Assets/Scripts/Script/Player.cs`
- `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- `DCGO/Assets/Scripts/Script/MainPhaseAction/*.cs`

목표:
RL headless engine의 최소 프로젝트 골격을 생성한다.

생성 위치:
- `src/DCGO.RL.Engine/`
- `src/DCGO.RL.Engine.Tests/`

구현할 것:

Core state:
- GameState
- PlayerState
- CardDefinition
- CardInstance
- PermanentState
- Zone
- Phase
- PlayerId
- CardInstanceId
- PermanentId
- GameConfig
- GameResult

Action:
- GameAction
- HatchAction
- MoveFromBreedingAction
- PlayCardAction
- DigivolveAction
- AttackAction
- PassAction
- SelectionAction placeholder

Decision:
- DecisionPoint
- DecisionKind
- LegalAction
- SelectionRequest
- SelectionResult
- SelectableTarget

Infrastructure:
- IDeterministicRng
- deterministic RNG implementation
- UnsupportedMechanicException
- DomainException base type if useful

주의:
- 원본 GameContext의 Memory, TurnPlayer, NonTurnPlayer, Phase 개념을 반영한다.
- 원본 Player의 deck, digi-egg deck, hand, trash, lost, security, executing area, field permanents 개념을 반영한다.
- SelectionRequest는 Unity UI가 아니라 선택의 의미를 표현하는 headless DTO다.
- 학습용 IRlGameEnvironment, ObservationEncoder, RewardCalculator는 아직 구현하지 않는다.

테스트:
- GameState 생성 테스트
- PlayerState zone list 초기화 테스트
- deterministic RNG 동일 seed 테스트
- GameState clone이 mutable list를 공유하지 않는 테스트
- SelectionRequest 생성 테스트
- RL.Engine 프로젝트가 Unity assembly를 참조하지 않는 테스트

완료 후 한국어로 요약:
- 변경 파일
- 원본 대비 이식 매핑
- 실행한 테스트
- 남은 TODO

