# Battle Flow 분석

## 전체 루프

원본의 전체 루프는 `TurnStateMachine.GameStateMachine()`이다.

1. `StartGame()` 실행
2. `endGame`이면 종료
3. `GameContext.SwitchTurnPlayer()`
4. `ActivePhase()`
5. `DrawPhase()`
6. `BreedingPhase()`
7. `MainPhase()`
8. `EndPhase()`
9. 2번으로 반복

RL.Engine에서는 이 흐름을 coroutine 대신 `RuleProcessor.Step()` 또는 `TurnEngine.Advance()` 계열의 순수 단계 처리로 옮긴다. 입력이 필요한 지점에서는 즉시 멈추고 `DecisionPoint`를 반환한다.

## Start Game

원본 `StartGame()`은 다음 순서다.

- `FirstPlayer`를 설정한다.
- non-turn-player 순서 기준으로 각 플레이어가 5장 드로우한다.
- 각 플레이어가 mulligan 여부를 선택한다.
- mulligan이면 손패를 deck bottom에 넣고 shuffle 후 5장 다시 드로우한다.
- 각 플레이어가 deck top에서 security 5장을 세팅한다.
- `DoneStartGame = true`가 된다.

RL.Engine 초기 harness에서는 mulligan 정책을 고정하거나 action sequence에 포함한다. 모든 shuffle은 seed 기반 deterministic RNG를 사용한다.

## Active Phase

원본 active phase는 다음 일을 한다.

- turn start timestamp와 turn count를 갱신한다.
- `EffectTiming.OnStartTurn`을 stack한다.
- `AutoProcessCheck()`와 active attack 처리를 반복한다.
- `EndTurnCheck()`로 memory crossing을 확인한다.
- field permanent를 unsuspend한다. breeding area permanent는 직접 unsuspend하고, battle area는 `IUnsuspendPermanents` primitive를 사용한다.
- turn-player active phase duration effect와 next untap duration effect를 정리한다.

RL.Engine에서는 animation과 UI 표시를 제거하고, `UnsuspendPermanents` primitive와 effect duration reset을 명시 이벤트로 남긴다.

## Draw Phase

원본 draw phase는 `TurnCount != 1`일 때만 draw를 수행한다.

- 드로우 전에 deck이 비어 있으면 `EndGame(nonTurnPlayer)`를 호출한다.
- deck이 있으면 `DrawClass(turnPlayer, 1, null).Draw()`를 실행한다.
- 이후 `AutoProcessCheck()`, active attack 처리, `EndTurnCheck()`를 수행한다.

Minimal Playable Battle에서 deck-out loss는 반드시 포함한다.

## Breeding Phase

원본 breeding phase는 `CanHatch` 또는 `CanMove`가 있을 때 선택을 요청한다.

- `CanHatch`가 가능하거나 move가 불가능하면 hatch 선택을 표시한다.
- hatch가 불가능하고 `CanMove`가 가능하면 breeding area permanent move 선택을 표시한다.
- 선택 결과가 true이면 `HatchDigiEggClass.Hatch()` 또는 `CardObjectController.MovePermanent()`를 실행한다.
- 이후 `AutoProcessCheck()`, active attack 처리, `EndTurnCheck()`를 수행한다.

RL.Engine에서는 hatch/move를 `LegalAction`으로 노출하거나 phase 내부 `SelectionRequest`로 노출할 수 있다. CLI와 UnityAdapter가 같은 정보를 쓰도록 요청/결과 구조를 통일한다.

## Main Phase

원본 main phase의 반복 조건은 `CanSelect()`다. 선택 가능한 항목은 다음이다.

- 손패에서 play 가능한 카드
- field permanent의 선언 효과
- attack 가능한 permanent
- hand/trash card의 선언 효과

main phase loop는 매 반복마다 `AutoProcessCheck()`, active attack state 처리, `EndTurnCheck()`를 수행하고, 이후 사용자의 main phase action을 기다린다. 선택된 결과는 `PlayCard`, `UseCardEffect`, `AttackingPermanent` 중 하나로 저장된다.

실행 분기는 다음이다.

- `UseCardEffect`가 있으면 `AutoProcessing.ActivateEffectProcess()`로 선언 효과를 실행한다.
- `PlayCard`가 있으면 `PlayCardClass.PlayCard()`로 normal play, digivolve, Jogress, Burst, AppFusion, option use를 분기 처리한다.
- `AttackingPermanent`가 있으면 `AttackProcess.Attack()`으로 공격을 시작한다.
- 선택지가 없거나 pass action이면 `EndTurnProcess()`로 넘어간다.

RL.Engine에서는 `SetMainPhase()`가 UI에 붙이는 click/drag 처리를 구현하지 않는다. 대신 `LegalActionGenerator`가 play/effect/attack/pass action을 생성하고 `ActionExecutor`가 실행한다.

## End Phase와 memory crossing

원본 `AutoProcessing.EndTurnCheck()`는 현재 phase가 End가 아니고 active attack이 없을 때, non-turn-player 관점 memory가 `TurnEndMinMemory` 이상이면 `EndTurnProcess()`를 호출한다. `EndTurnProcess()`는 pass에 의한 종료라면 memory를 상대 3으로 보정하고, `EffectTiming.OnEndTurn`, `AutoProcessCheck()`, active attack 처리를 수행한 뒤 조건이 유지되면 `TurnPhase = End`로 설정한다.

`EndPhase()`는 attack count, per-turn effect map, duration effect, digivolve count, use count를 정리한다.

RL.Engine에서는 memory를 단일 정수로 유지하되 각 player 관점 계산을 명확히 두고, memory crossing이 발생한 정확한 action 이후 `EndTurnProcess` equivalent가 실행되도록 trace에 남긴다.

## Attack Flow

`AttackProcess.Attack()`은 attacker/defender를 설정하고, attacker가 battle area Digimon이면 공격을 시작한다. 기본 흐름은 다음이다.

1. attacker suspend
2. `[When Attacking]` 계열 trigger stack
3. counter timing
4. blocker selection
5. defender가 있으면 `IBattle.Battle()`
6. defender가 없고 security가 0이면 attacker owner 승리
7. security가 있으면 `ISecurityCheck.SecurityCheck()`
8. `[On End Attack]` trigger
9. until-end-attack effect 정리

`IBattle`은 DP 비교로 loser permanent 또는 security Digimon을 판정하고 `DestroyPermanentsClass` 또는 trash 이동을 실행한다. Minimal Playable Battle에서는 keyword와 복잡 replacement를 제외한 기본 DP battle을 먼저 검증한다.
