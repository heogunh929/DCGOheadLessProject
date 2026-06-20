# Runtime Composition

Queue 51 기준 production runtime 조립 entry point는 `BattleEngineServices.Create(ICardScriptRegistry, IDecisionProvider?)`다.

## 필수 서비스

production service graph는 다음 의존성이 없으면 유효하지 않다.

- `TriggerPipelineService`
- `IZoneMover`
- `Tier1PrimitiveService`
- `SecurityCheckService`
- `EngineInvariantChecker`

`BattleEngineServices.ValidationReport`는 단순 non-null만 보지 않는다. 다음 공유 관계도 검증한다.

- `PlayCardService`, `DigivolveService`, `SecurityCheckService`, `Tier1PrimitiveService`가 같은 `ZoneMover`를 쓴다.
- `PlayCardService`, `DigivolveService`, `AttackService`, `PhaseRunner`, `RuleProcessor`가 같은 `TriggerPipelineService`를 쓴다.
- `TriggerPipelineService`, `PlayCardService`, `SecurityEffectExecutionService`가 같은 `Tier1PrimitiveService`를 쓴다.
- `AttackService`와 `Tier1PrimitiveService`가 같은 runtime `SecurityCheckService`를 쓴다.
- `ActionExecutor`, `TurnRunner`, `RuleProcessor`가 root에서 만든 동일 서비스 인스턴스를 쓴다.

## Production 경로

- production code는 실제 `ICardScriptRegistry`를 전달해서 `BattleEngineServices.Create(...)`를 호출해야 한다.
- `ActionExecutor`와 `TurnRunner`의 public 기본 생성자는 더 이상 fallback graph를 만들지 않는다.
- `ActionExecutor`는 일부 custom service와 일부 default service를 섞는 부분 주입을 허용하지 않는다. 모든 dependency는 같은 service graph에서 온 완성 인스턴스여야 한다.
- 테스트에서 no-effect graph가 필요하면 테스트 전용 `ICardScriptRegistry`를 만들어 `BattleEngineServices.Create(...)`에 명시 전달한다.

## PlayCardService

`PlayCardService`는 `TriggerPipelineService`를 필수 생성자 인자로 받는다. pipeline이 없을 때 option trigger를 조용히 건너뛰던 nullable 경로와 silent return 경로는 없다.

hand option play는 `PlayWithResult(...)`로 `PlayCardResult`를 반환한다. 선택이 필요하면 option card는 `Executing`에 남고 `PendingSelectionRequest`, `PendingResolution`이 결과에 포함된다.

기존 `Play(...)` API는 직접 호출 호환을 위해 pending selection이 있으면 `DomainException`을 던진다. production action boundary는 `ActionExecutor.Execute(...)`의 반환값을 사용한다.

## ActionExecutor 선택 경계

`ActionExecutor.Execute(...)`는 `ActionExecutionResult`를 반환한다.

- pending option selection이 없으면 action trace 기록 후 `RuleProcessor.ProcessAfterAction(...)`을 실행한다.
- pending option selection이 있으면 state를 `Executing` 중간 상태로 보존하고 `SelectionRequest`와 `DecisionPoint`를 반환한다.
- 이 경우 selection 완료 전 rules timing을 실행하지 않는다.

`RandomLegalActionRunner`와 `ScriptedScenarioRunner`는 pending selection을 무시하지 않는다. runner 내부 action 실행 결과가 pending selection이면 즉시 `DomainException`으로 중단한다.

## 후속 queue 항목

Queue 51에서는 hand option pending selection을 action boundary로 반환하는 범위까지만 구현했다. 다음 항목은 범위가 더 넓어 별도 queue로 다룬다.

- `OnPlay`, `WhenDigivolving`, `OnAllyAttack`, `OnEndAttack`, phase timing, rules timing에서 발생한 pending `SelectionRequest`를 공통 engine boundary로 반환하고 재개하는 구조.
- `ActionExecutor`가 `SelectionResult` 적용 action을 공통적으로 받아 pending `EffectResolution`을 이어서 drain하는 구조.
- `ReplayRunner`, `RandomLegalActionRunner`, `ScriptedScenarioRunner`가 공통 resume boundary를 사용해 pending selection을 처리하는 구조.

위 후속 항목을 구현하기 전까지 source body가 없는 효과를 추측 구현하지 않는다. `ST3-02` P2 source body 미확인 finding은 whole-engine completion gate에서 계속 blocking으로 유지한다.
