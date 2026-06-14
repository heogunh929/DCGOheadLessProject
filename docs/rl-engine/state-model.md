# State Model

## 04 단계 이식 범위

`04_state_model.md` 단계는 원본 `GameContext`, `Player`, `Permanent`, `CardSource`의 battle state를 Unity 독립적인 DTO와 상태 객체로 옮긴다. 이 단계는 rule 실행, zone 이동 primitive, legal action 생성은 구현하지 않고 상태를 담는 구조와 검증 가능한 clone/hash 기반만 만든다.

## 원본과 새 타입 대응표

| 원본 Unity 타입 | 원본 필드/개념 | RL.Engine 타입 |
| --- | --- | --- |
| `GameContext` | `Memory` | `GameState.Memory` |
| `GameContext` | `TurnPlayer`, `NonTurnPlayer`, `FirstPlayer` | `GameState.TurnPlayerId`, `NonTurnPlayerId`, `FirstPlayerId` |
| `GameContext` | `TurnPhase` | `GameState.Phase` |
| `GameContext` | `ActiveCardList` | `GameState.ActiveCardIds`, `GameState.Cards` |
| `TurnStateMachine` | `TurnCount`, `endGame`, winner 처리 | `GameState.TurnCount`, `GameState.Result`, `IsGameOver`, `WinnerPlayerId` |
| `Player` | `LibraryCards` | `PlayerState.Deck` |
| `Player` | `DigitamaLibraryCards` | `PlayerState.DigiEggDeck` |
| `Player` | `HandCards` | `PlayerState.Hand` |
| `Player` | `TrashCards` | `PlayerState.Trash` |
| `Player` | `LostCards` | `PlayerState.Lost` |
| `Player` | `SecurityCards` | `PlayerState.Security` |
| `Player` | `ExecutingCards` | `PlayerState.Executing` |
| `Player` | `FieldPermanents`, battle/breeding area 조회 | `PlayerState.FieldPermanents`, `BattleAreaPermanents`, `BreedingAreaPermanent` |
| `Permanent` | `cardSources`, `TopCard`, `DigivolutionCards`, `LinkedCards` | `PermanentState.TopCardId`, `SourceCardIds`, `LinkedCards` |
| `Permanent` | owner/controller, suspended, frame, enter turn | `PermanentState.OwnerPlayerId`, `ControllerPlayerId`, `IsSuspended`, `FrameIndex`, `EnterFieldTurnCount` |
| `CardSource` | instance owner, face, current area, permanent 연결 | `CardInstance.Owner`, `IsFaceUp`, `Zone`, `PermanentId` |
| `CEntity_Base` | card kind/color/level/cost/DP/effect class | `CardDefinition` |

## StateHash

`GameState.ComputeStateHash()`는 deterministic replay 검증을 위한 안정 문자열 hash를 반환한다. hash 입력에는 config, memory, phase, turn count, turn/first player, result, active card ids, card definitions, card instances, player zone 순서, permanent stack/source/link/suspended/frame 상태가 포함된다.

같은 seed, 같은 decklist, 같은 action sequence는 같은 state hash를 만들어야 한다. 이 단계에서는 실제 zone 이동 primitive가 없으므로 테스트에서 list와 `CardInstance.Zone`을 직접 바꿔 hash 변화를 검증한다. 이후 `ZoneMover` 단계에서는 모든 상태 변경이 primitive를 통해 같은 hash 규칙을 유지해야 한다.

## 의도적으로 제외한 항목

- Unity UI field, `Transform`, `Text`, `Image`, `HandCard`, `FieldPermanentCard`, `securityObject`
- Photon player/network state
- effect queue와 rule execution
- `ObservationEncoder`, `RewardCalculator`, dataset exporter, trainer
