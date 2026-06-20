# Runtime Composition

Queue 51 기준 production runtime 조립은 `BattleEngineServices.Create(ICardScriptRegistry, IDecisionProvider?)`를 entry point로 사용한다.

## 필수 서비스

production service graph는 다음 의존성이 없으면 유효하지 않다.

- `TriggerPipelineService`
- `IZoneMover`
- `Tier1PrimitiveService`
- `EngineInvariantChecker`

`BattleEngineServices.ValidationReport`와 `BattleEngineServices.Validate(...)`는 위 의존성 누락을 `BattleEngineServiceGraphIssue`로 보고하고, `ThrowIfInvalid()`는 즉시 `DomainException`을 던진다.

## Production 경로

- `BattleEngineServices.Create(...)`는 card script registry를 필수로 받는다.
- `ActionExecutor`, `TurnRunner`, validation runner 기본 graph는 더 이상 trigger pipeline이 없는 조합을 만들지 않는다.
- legacy 기본 생성자는 기존 테스트/fixture 호환을 위해 fallback no-effect registry가 붙은 완성 graph를 사용한다. production code는 실제 `ICardScriptRegistry`를 전달하는 `BattleEngineServices.Create(...)`를 사용해야 한다.

## PlayCardService

`PlayCardService`는 `TriggerPipelineService`를 필수 생성자 인자로 받는다. pipeline이 없을 때 option trigger를 조용히 건너뛰던 nullable 경로와 silent return 경로를 제거했다.

hand option play는 `PlayWithResult(...)`를 통해 `PlayCardResult`를 반환한다. 선택이 필요하면 option card는 `Executing`에 남고 `PendingSelectionRequest`, `PendingResolution`이 결과에 포함된다.

기존 `Play(...)` API는 직접 호출 호환을 위해 pending selection이 있으면 `DomainException`을 던진다. production action boundary는 `ActionExecutor.Execute(...)`의 반환값을 사용한다.

## ActionExecutor 선택 경계

`ActionExecutor.Execute(...)`는 `ActionExecutionResult`를 반환한다.

- pending option selection이 없으면 action trace 기록 후 `RuleProcessor.ProcessAfterAction(...)`을 실행한다.
- pending option selection이 있으면 state를 `Executing` 중간 상태로 보존하고 `SelectionRequest`와 `DecisionPoint`를 반환한다.
- 이 경우 selection 완료 전 rules timing을 실행하지 않는다.

## 후속 queue 항목

Queue 51에서는 hand option pending selection을 action boundary로 반환하는 범위까지만 구현했다. 다음 항목은 범위가 더 넓어 별도 queue로 다루는 것이 맞다.

- `OnPlay`, `WhenDigivolving`, `OnAllyAttack`, `OnEndAttack`, phase timing, rules timing에서 발생한 pending `SelectionRequest`를 공통 engine boundary로 반환하고 재개하는 구조.
- `ActionExecutor`가 `SelectionResult` 적용 action을 공통적으로 받아 pending `EffectResolution`을 이어서 drain하는 구조.
- `PhaseRunner`, `RuleProcessor`, `AttackService`, `DigivolveService`의 direct constructor까지 모두 production/test-only builder로 분리하는 엄격한 DI 정리.

위 후속 항목을 구현하기 전까지는 source body가 없는 효과를 추측 구현하지 않는다. `ST3-02` P2 source body 미확인 finding은 whole-engine completion gate에서 계속 blocking으로 유지한다.
