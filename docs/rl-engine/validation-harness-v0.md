# Validation Harness v0

## Source of Truth

이번 단계의 검증 기준은 Unity 원본의 battle 상태 소유 구조에서 가져왔다.

- `DCGO/Assets/Scripts/Script/GameContext.cs`: 전역 게임 상태, player 목록, active card 목록, phase/turn/memory의 중심 컨테이너.
- `DCGO/Assets/Scripts/Script/Player.cs`: deck, hand, trash, security, battle area, breeding area 등 player별 zone 소유 구조.
- `DCGO/Assets/Scripts/Script/Permanent.cs`: field permanent의 top card, source card, linked card 연결 구조.
- `DCGO/Assets/Scripts/Script/CardObjectController.cs`: `RemoveFromAllArea`와 field card 생성/제거 흐름에서 카드가 여러 zone에 동시에 남지 않아야 하는 규칙.

## Implemented Scope

`Validation` namespace에는 RL 학습용 API가 아니라 deterministic replay와 invariant 검증을 위한 최소 harness만 둔다.

- `StateHasher`: 현재 `GameState.ComputeStateHash()`를 검증 harness에서 쓰는 얇은 wrapper.
- `TraceEvent` / `GameTrace`: state hash 전후, action, zone move, decision, selection, unsupported mechanic 정보를 순서 있는 event로 기록한다.
- `EngineInvariantChecker`: `GameState` 내부 카드 소유권과 zone membership을 검사한다.
- `ReplayRunner`: 현재는 빈 trace만 처리한다. event 적용은 아직 미지원이며 명시적으로 `UnsupportedMechanicException`을 던진다.
- `ScriptedScenarioRunner`: 현재는 action 없는 scenario만 snapshot으로 기록한다. action 실행은 다음 단계 이후 구현한다.
- `UnsupportedMechanicReporter`: 미지원 메커니즘을 silent no-op으로 숨기지 않기 위한 수집기.
- `DeckValidationReport`: deck validation 결과를 표현하기 위한 skeleton.

## Invariants

현재 v0 checker는 다음을 실패로 보고한다.

- 하나의 `CardInstance`가 여러 public zone 또는 field container에 동시에 존재한다.
- `CardInstance.CurrentZone`이 실제 zone container와 다르다.
- `CardInstance.Owner`와 player zone owner가 다르다.
- field card의 `PermanentId`가 실제 `PermanentState.Id`와 다르다.
- `PermanentState.TopCardId`, `SourceCardIds`, `LinkedCardIds` 사이에 같은 card id가 중복된다.
- zone 또는 permanent가 `GameState.Cards`에 없는 card id를 참조한다.

## Explicit Non-Scope

이번 단계에서는 setup draw, mulligan, security setup, deck-out, action replay를 구현하지 않는다. `ObservationEncoder`, `RewardCalculator`, dataset exporter, trainer, RL environment API도 만들지 않는다. self-play/trace는 학습 데이터가 아니라 이후 원본 Unity와의 검증 데이터로만 사용한다.
