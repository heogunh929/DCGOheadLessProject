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

기존 `Play(...)` API는 직접 호출 호환을 위해 pending selection이 있으면 상태와 trace를 호출 전으로 되돌린 뒤 `DomainException`을 던진다. `Digivolve(...)`, `Attack(...)`, `CheckSecurity(...)`도 같은 rollback guard를 사용한다. production action boundary는 `ActionExecutor.Execute(...)`/`EngineSession`의 반환값을 사용한다.

Queue 52B부터 rollback 대상에는 `GameState.RuntimeRules`도 포함된다. once-per-turn 사용 이력은 service graph가 아니라 `GameState`가 소유하므로 clone/restore/hash/replay가 같은 rule-visible runtime state를 본다.

## ActionExecutor 선택 경계

`ActionExecutor.Execute(...)`는 `ActionExecutionResult`를 반환한다.

- pending option selection이 없으면 action trace 기록 후 result-aware `RuleProcessor.ProcessAfterActionWithResult(...)`를 실행한다.
- pending option selection이 있으면 state를 `Executing` 중간 상태로 보존하고 `SelectionRequest`와 `DecisionPoint`를 반환한다.
- 이 경우 selection 완료 전 rules timing을 실행하지 않는다.

`RandomLegalActionRunner`와 `ScriptedScenarioRunner`는 pending selection을 무시하지 않는다. services 기반 runner는 내부 action 또는 phase step 실행 결과가 pending selection이면 `ScenarioRunStatus.PausedForDecision`, pending `DecisionPoint`, stable continuation id를 `ScenarioResult`로 반환한다. 구버전 개별 dependency fallback 경로는 pending selection을 자동 처리하지 않고 명시 예외로 중단한다.

## EngineSession pause/resume 경계

Queue 52A에서 `BattleEngineServices.CreateSession(GameState, GameTrace?)`가 추가됐다. 이 session은 production service graph 위에서 `Step(GameAction)`과 `Resume(DecisionResult)`를 제공한다.

- `Step`은 pending이 없을 때만 action을 실행한다.
- action 실행 중 pending selection이 생기면 `EngineStepResult.Status = PausedForDecision`과 pending `DecisionPoint`, stable continuation id를 반환한다.
- `Resume`은 pending request id, player, `DecisionToken`을 검증한 뒤 selection을 적용한다.
- chained selection이 생기면 다시 `PausedForDecision`으로 멈춘다.
- selection chain이 모두 끝난 뒤에만 result-aware rules timing/cleanup을 실행하고, rules timing selection이 생기면 다시 `PausedForDecision`으로 멈춘다.
- `RunToMainPhase()`는 `None`/`End`에서 `Active` -> `Draw` -> `Breeding` -> `Main` 전체 boundary를 session 안에서 진행하며 phase timing selection을 반환한다.
- action, selection result, phase-only step은 trace에 남기며 replay는 `BattleEngineServices`를 받은 `ReplayRunner`에서 action/selection/phase event를 순서대로 재적용한다.
- selection trace는 `DecisionResult(Player, DecisionToken, SelectionResult)` payload를 저장한다. replay는 기록된 token을 사용하며 현재 pending token을 자동 주입하지 않는다.

현재 session resume은 hand option `OptionSkill`, chained option selection, optional yes/no + explicit target selection, normal play `OnPlay`, normal digivolve `WhenDigivolving`, `OnAllyAttack`, `OnEndAttack`, attack security check 중 `SecuritySkill`/Activate Main selection, rules timing, `OnStartTurn`, `OnStartMainPhase`, `PassAction`의 `OnEndTurn`/`OnStartTurn` pending selection을 처리한다. play/digivolve/attack/security/rule/phase runner는 pending 시 continuation을 반환하고, session resume은 selection 적용 후 남은 pipeline queue, security-check loop, action tail을 이어서 처리한다.

phase-only `EngineSession.RunMainPhase()` trace replay는 `TraceEventKind.Phase`와 `RunMainPhase` details로 재생한다. full security timing sequence는 queue 53에서 원본 `ISecurityCheck`/`AutoProcessing` 순서와 함께 정렬한다.

## 남은 queue 항목

Queue 52A는 hand option, chained option, `OnPlay`, `WhenDigivolving`, `OnAllyAttack`, `OnEndAttack`, attack security check 중 `SecuritySkill`/Activate Main selection, rules timing, `OnStartMainPhase`, `PassAction`의 `OnEndTurn`/`OnStartTurn` pending selection을 `EngineSession` boundary로 재개하는 기반을 추가했다. 다음 범위는 아직 남아 있다.

- queue 53 full security timing sequence: `OnSecurityCheck`, `OnLoseSecurity`, security 감소 확정, `AfterEffectsActivate`.

위 후속 항목을 구현하기 전까지 source body가 없는 효과를 추측 구현하지 않는다. `ST3-02` P2 source body 미확인 finding은 whole-engine completion gate에서 계속 blocking으로 유지한다.

## RuntimeRuleState

`OncePerTurnTracker`는 더 이상 mutable `HashSet`을 소유하지 않는다. once-per-turn 사용 이력은 `GameState.RuntimeRules.OncePerTurnUses`가 소유한다.

- `GameState.Clone()`은 `RuntimeRules`를 복제한다.
- `GameState.RestoreFrom(...)`은 `RuntimeRules`를 snapshot으로 되돌린다.
- `GameState.ComputeStateHash()`는 once-per-turn 사용 이력을 정렬해 hash input에 포함한다.
- `GameState.TurnCount`가 앞으로 진행되면 이전 turn의 once-per-turn 이력을 정리한다.
- 같은 `BattleEngineServices`에서 여러 `EngineSession`을 만들어도 session별 `GameState`가 이력을 소유하므로 서로 영향을 주지 않는다.

## Runner Continuation

Queue 52C 기준 `ScriptedScenarioRunner`와 `RandomLegalActionRunner`는 재개 가능한 runner-owned session API를 제공한다.

- `StartSession(...)`은 external decision 모드이며 providerless `BattleEngineServices` graph만 허용한다.
- `Resume(session, DecisionResult)`는 session owner id를 먼저 검증하고, 내부 `EngineSession.Resume`에서 player, `DecisionToken`, request id를 검증한다.
- `ScenarioResult`에는 pending `DecisionPoint`, `DecisionToken`, stable continuation id, `RunnerSessionHandle`만 담고 mutable `EngineSession`은 노출하지 않는다.
- scripted session은 scenario와 현재 step index를 소유해 pause된 step을 완료한 뒤 다음 step부터 이어 간다.
- random session은 RNG, actions executed, max action count, phase normalization/action/main-phase pause 위치를 소유해 pause/resume 전후 random action sequence를 보존한다.
- shared stateful `IDecisionProvider`가 여러 `EngineSession`에 섞이지 않도록 external decision session에서는 provider graph를 거부한다. provider graph는 one-shot `Run` 비교/legacy 경로에서만 사용한다.

Queue 52D 기준 `ScenarioResult`는 live session state를 노출하지 않는다.

- `FinalState`, `Trace`, `InvariantReports`는 result 생성 시점의 snapshot이다.
- 외부 코드가 이전 result의 `FinalState`나 `Trace`를 변경해도 runner-owned session에는 영향이 없다.
- `RunnerSessionHandle`은 diagnostic identity이며 resume 권한이나 continuation payload가 아니다. handle만 보관해서는 resume할 수 없고, 원래 runner-owned session 객체가 필요하다.
- providerless one-shot `Run(...)`은 pending decision을 만나면 `StartSession(...)`을 사용하라는 `DomainException`으로 실패한다. 이 경로는 lost continuation을 `ScenarioResult`로 반환하지 않는다.
- provider가 모든 선택을 공급하는 one-shot `Run(...)`은 기존 validation harness와 replay 비교를 위해 유지한다.
