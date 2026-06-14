# Setup / Draw / Deck-out

## Source of Truth

이번 단계는 다음 Unity 원본 흐름을 기준으로 이식했다.

- `TurnStateMachine.StartGame`
  - `gameContext.FirstPlayer = gameContext.NonTurnPlayer`
  - 각 player가 5장 draw
  - mulligan 선택 및 재셔플 후 5장 재드로우
  - 각 player가 deck top에서 security 5장 setup
- `TurnStateMachine.DrawPhase`
  - phase를 draw로 설정
  - `TurnCount != 1`일 때만 draw
  - draw해야 하는데 `LibraryCards.Count == 0`이면 상대 player 승리
- `CardController.DrawClass`
  - deck top `LibraryCards[0]`을 뽑아 hand로 이동
- `IAddSecurityFromLibrary` / `CardObjectController.AddSecurityCard`
  - deck top을 security에 추가하며, security는 face-down이고 top insert 순서를 사용

## Engine Mapping

- `DeckInstantiationService`
  - `PlayerDeckList`의 main deck과 digi-egg deck을 `CardInstance`로 생성한다.
  - main deck card는 `Zone.Deck`, digi-egg card는 `Zone.DigiEggDeck`에 둔다.
  - setup에 필요한 최소 main deck 수량과 card id/type을 `DeckValidationReport`로 검증한다.
  - deterministic Fisher-Yates shuffle을 `IDeterministicRng`로 수행한다.
- `FirstPlayerSelector`
  - forced first player가 있으면 그대로 사용하고, 없으면 deterministic RNG로 선택한다.
- `DrawService`
  - `DrawCards`는 deck top을 `ZoneMover`로 `Deck -> Hand` 이동한다.
  - `ExecuteDrawPhase`는 `TurnCount == 1`이면 첫 턴 draw를 skip한다.
  - draw해야 하는데 deck이 비어 있으면 `WinConditionChecker`로 deck-out loss를 기록한다.
- `SecuritySetupService`
  - deck top을 `ZoneMover`로 `Deck -> Security` 이동한다.
  - security card는 face-down이며 destination top에 추가한다.
- `GameSetupService`
  - deck instantiate, shuffle, first player 결정, opening hand 5장, security 5장을 순서대로 수행한다.
  - setup 완료 후 `EngineInvariantChecker`를 실행한다.

## Current Non-Scope

Mulligan은 원본 `StartGame`에 포함되어 있지만 이번 prompt에서 구현하지 않는다. 이후 선택/decision 흐름과 연결할 때 `LegalActionKind.Mulligan` 또는 `SelectionRequest` 기반으로 별도 이식한다.

CardEffect, draw trigger, recovery effect, UI animation, Photon room property 연동은 이번 단계에 포함하지 않는다. 모든 카드 이동은 `ZoneMover` primitive로 처리한다.

## Tests

현재 테스트는 다음을 검증한다.

- 같은 seed의 setup state hash가 동일하다.
- 다른 seed에서 shuffle 결과가 달라질 수 있다.
- opening hand count가 각 player 5장이다.
- security count가 각 player 5장이고 face-down이다.
- 첫 턴 draw phase는 draw를 skip한다.
- 두 번째 턴 이후 draw phase는 1장 draw한다.
- draw phase에서 deck이 비면 해당 player가 패배한다.
- setup 이후 public zone 중복 없이 invariant가 통과한다.
