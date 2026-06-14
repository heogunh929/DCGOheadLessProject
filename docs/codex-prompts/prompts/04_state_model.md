# 04 - GameContext / Player 상태 모델 이식

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "04_state_model.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

참조 원본:
- `DCGO/Assets/Scripts/Script/GameContext.cs`
- `DCGO/Assets/Scripts/Script/Player.cs`
- `DCGO/Assets/Scripts/Script/Permanent.cs`
- `DCGO/Assets/Scripts/Script/CardSource.cs`

목표:
원본 GameContext/Player의 battle state를 Unity 독립적인 GameState/PlayerState/PermanentState/CardInstance 구조로 이식한다.

GameState에는 최소한 다음 개념을 포함한다.

- Memory
- TurnPlayerId
- FirstPlayerId
- Phase
- TurnCount
- Players[2]
- ActiveCardIds
- IsGameOver
- WinnerPlayerId
- GameResult
- deterministic state hash 계산용 데이터 접근

PlayerState에는 최소한 다음 zone을 포함한다.

- Deck
- DigiEggDeck
- Hand
- Trash
- Lost
- Security
- Executing
- BattleArea permanents
- BreedingArea permanent
- Field permanents 전체 조회 helper

PermanentState에는 최소한 다음 개념을 포함한다.

- PermanentId
- OwnerPlayerId
- ControllerPlayerId
- TopCardId
- SourceCardIds
- LinkedCardIds
- IsSuspended
- EnterFieldTurnCount
- IsDigimon/Tamer/Option 판정 helper는 CardDefinition을 통해 가능하게 설계

구현 원칙:
- CardDefinition과 CardInstance를 분리한다.
- PlayerState는 원본 Player의 battle zone 상태만 이식한다.
- UI 필드, Transform, Text, Image, HandCard, FieldPermanentCard, securityObject 같은 Unity UI 요소는 이식하지 않는다.

테스트:
- GameState 초기화 시 두 PlayerState가 존재한다.
- PlayerState의 zone list가 서로 공유되지 않는다.
- PermanentState가 top card와 evolution source를 구분한다.
- TurnPlayer/NonTurnPlayer helper가 정상 동작한다.
- Clone 후 원본 수정이 복제본에 영향을 주지 않는다.
- StateHash가 같은 상태에서 동일하고, zone 이동 후 달라진다.

완료 후:
- 원본 GameContext/Player/Permanent/CardSource와 새 타입의 대응표를 한국어로 정리하라.
- 실행한 테스트와 남은 TODO를 한국어로 요약하라.

