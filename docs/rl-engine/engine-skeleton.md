# Engine Skeleton

## 03 단계 산출물

`03_engine_skeleton.md` 단계에서는 구현 규칙이 아니라 원본 battle 구조를 담을 최소 타입 골격만 만든다.

- `GameState`는 원본 `GameContext`의 `Memory`, `TurnPlayer`, `NonTurnPlayer`, `TurnPhase`, active card list 역할을 headless 상태로 옮기기 위한 루트다.
- `PlayerState`는 원본 `Player`의 `LibraryCards`, `DigitamaLibraryCards`, `HandCards`, `TrashCards`, `LostCards`, `SecurityCards`, `ExecutingCards`, `FieldPermanents`를 반영한다.
- `GameAction` 계열은 원본 `MainPhaseAction`의 play/effect/attack/pass 입력 경계를 Unity/Photon 없이 표현한다.
- `DecisionPoint`, `LegalAction`, `SelectionRequest`, `SelectionResult`는 원본 UI/selection queue를 headless DTO로 분리하기 위한 기반이다.
- `IDeterministicRng`는 `UnityEngine.Random` 의존을 대체하기 위한 deterministic RNG 경계다.

이 단계에서는 `IRlGameEnvironment`, `ObservationEncoder`, `RewardCalculator`, dataset exporter, trainer를 만들지 않는다.
