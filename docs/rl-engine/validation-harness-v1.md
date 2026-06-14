# Validation Harness v1

## Source of Truth

v1 harness는 Minimal Playable Battle의 scripted 검증과 random legal action smoke를 위한 단계다. 원본 Unity에서 비교 기준으로 본 경계는 다음과 같다.

- `TurnStateMachine.GameStateMachine`: `StartGame` 이후 turn player 전환, Active, Draw, Breeding, Main phase 순서.
- `TurnStateMachine.ActivePhase` / `DrawPhase`: phase log, turn count 증가, 첫 턴 draw skip, deck-out loss.
- `TurnStateMachine.MainPhase`: `PlayCard`, `AttackPermanent`, pass action이 main phase action으로 처리되는 구조.
- `AttackProcess.DetermineAttackOutcome`: security 0 direct win, security check, Digimon battle 분기.
- `CardController.DrawClass`, `ISecurityCheck`, `IBattle`: draw, security check, DP battle, loser trash 흐름.
- `PlayLog`: 원본은 사람이 읽는 text log를 남긴다. RL.Engine은 같은 경계를 `TraceEvent`와 state hash로 구조화해 저장한다.

## Implemented Scope

- `ScriptedScenarioRunner`
  - action step, draw phase step, run-to-main step을 실행한다.
  - 각 step 이후 `EngineInvariantChecker`를 실행하고 report를 보관한다.
  - trace에는 initial/final snapshot과 action/move event를 남긴다.
- `RandomLegalActionRunner`
  - `LegalActionGenerator`가 생성한 action만 deterministic RNG로 선택한다.
  - game over, legal action 없음, 또는 max action abort 중 하나로 종료한다.
  - phase normalization 중에도 invariant를 검사한다.
- `ScenarioResult`
  - final state, final hash, trace, invariant reports, status를 보관한다.
- `MaxTurnAbortResult`
  - random smoke가 max action 제한으로 멈춘 경우를 명시한다.
- `TraceStore`
  - trace를 JSON string으로 save/load한다.
  - replay에 필요한 action payload를 명시 직렬화한다.
- `ReplayRunner`
  - trace의 `Action` event를 재실행해 before/after hash를 검증한다.
  - snapshot/move/decision event는 replay 보조 정보로 유지하고 action 적용에는 사용하지 않는다.
- `ReplayDeterminismHelper`
  - initial state와 trace를 replay해서 expected final state hash와 비교한다.
- `CliDebugRenderer`
  - CLI 연결 전 단계의 text renderer skeleton이다.

## Required Scenarios

현재 v1 scripted scenario는 다음을 포함한다.

- deck-out loss
- security 0 direct attack win
- lower DP Digimon deleted
- equal DP battle
- normal digivolve draw
- memory crossing turn end
- hatch then move from breeding

## Random Smoke

Random smoke는 학습용 self-play가 아니다. 현재 목적은 legal action 생성과 action executor 사이 계약을 검증하고, 매 action 이후 invariant가 깨지지 않는지 확인하는 것이다. 결과가 game over, completed, max action abort 중 하나면 harness 관점에서 정상 종료다.

## Explicit Non-Scope

다음은 여전히 구현하지 않는다.

- `ObservationEncoder`
- `RewardCalculator`
- `DatasetExporter`
- trainer 또는 training loop
- RL environment API

trace와 random run 결과는 학습 데이터가 아니라 검증 데이터다.

## Tests

현재 테스트는 다음을 확인한다.

- 모든 required scripted scenario가 통과한다.
- random legal action smoke가 crash 없이 종료하거나 max action abort로 끝난다.
- max action abort 결과가 명시적으로 기록된다.
- action마다 invariant report가 기록된다.
- trace replay가 같은 final state hash를 재현한다.
- trace save/load 이후 replay hash가 유지된다.
- CLI debug renderer skeleton이 scenario result를 text로 렌더링한다.
