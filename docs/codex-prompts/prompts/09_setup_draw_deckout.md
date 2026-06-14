# 09 - Setup / Draw / Deck-out 이식

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "09_setup_draw_deckout.md 단계만 수행해줘"라고 요청한다.


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
- `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- `DCGO/Assets/Scripts/Script/CardController.cs`
- `DCGO/Assets/Scripts/Script/Player.cs`

목표:
deterministic setup, opening hand, security setup, draw phase, deck-out loss를 구현한다.

구현할 것:
- GameSetupService
- DeckInstantiationService
- DrawService
- SecuritySetupService
- FirstPlayerSelector
- WinConditionChecker의 deck-out 처리

필수 동작:
Setup:
- 양 플레이어 decklist로부터 CardInstance 생성
- main deck deterministic shuffle
- digi-egg deck deterministic shuffle
- 각 플레이어 opening hand 5장
- 각 플레이어 security 5장
- first player 결정

DrawPhase:
- 첫 턴 draw skip
- draw해야 하는데 deck이 비어 있으면 해당 player 패배
- 정상 draw 시 Deck -> Hand 이동

주의:
- CardEffect 없음
- Mulligan은 이 작업에서 구현하지 않거나 명시적 TODO로 둔다.

테스트:
- 같은 seed면 setup 결과가 동일하다
- 다른 seed면 shuffle 결과가 달라질 수 있다
- opening hand count는 5
- security count는 5
- first turn draw skip
- 두 번째 턴 이후 draw 1
- draw해야 하는데 deck이 비어 있으면 패배
- setup 후 public zone 중복 카드 없음
- replay state hash가 동일하다

완료 후:
- 원본 TurnStateMachine.StartGame/DrawPhase와의 대응 관계를 한국어로 정리하라.

