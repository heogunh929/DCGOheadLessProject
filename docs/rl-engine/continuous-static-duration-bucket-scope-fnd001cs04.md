# FND001-CS-04 Duration Bucket Cleanup Scope

## Scope

이 문서는 `ContinuousOrStaticEffect`가 `PartiallyImplemented`로 남아 있던 사유 중 duration bucket cleanup/provider integration 항목을 source-aligned evidence로 분해한 결과다.

- AS-IS root: `E:\headlessDCGO\DCGO\Assets`
- Task: `FND001-CS-04 duration bucket cleanup/provider integration parity`
- Verification: `docs/generated/as-is-restart/fnd-001-cs-04-duration-bucket-verification.json`
- Script: `scripts/verify_fnd001_duration_bucket_scope.py`
- Status: `ClosedByEvidence`

## Source Evidence

원본 duration bucket은 `Player`, `Permanent`, `TurnStateMachine`, `AttackProcess`에서 확인했다. 실제 cleanup callsite 중 `UntilEndBattleEffects`와 `UntilSecurityCheckEndEffects`는 `CardController.cs`에도 있으므로 추가 source cleanup callsite로 기록했다.

검증된 bucket은 7개다.

- `UntilEachTurnEndEffects` -> `DurationScope.UntilTurnEnd`
- `UntilOwnerTurnEndEffects` -> `DurationScope.UntilOwnerTurnEnd`
- `UntilOpponentTurnEndEffects` -> `DurationScope.UntilOpponentTurnEnd`
- `UntilEndBattleEffects` -> `DurationScope.UntilBattleEnd`
- `UntilEndAttackEffects` -> `DurationScope.UntilAttackEnd`
- `UntilSecurityCheckEndEffects` -> `DurationScope.UntilSecurityCheckEnd`
- `UntilOwnerActivePhaseEffects` / `UntilNextUntapEffects` -> `DurationScope.UntilOwnerActivePhaseEnd`

## Headless Evidence

현재 headless 후보는 duration data model과 cleanup timing에 대해 source-aligned evidence를 가진다.

- `src/DCGO.RL.Engine/Domain/TemporaryModifier.cs`
- `src/DCGO.RL.Engine/Domain/TemporaryGrantedEffect.cs`
- `src/DCGO.RL.Engine/Effects/DurationCleanupService.cs`
- `src/DCGO.RL.Engine/Effects/TemporaryGrantedEffectRegistry.cs`
- `src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs`
- `src/DCGO.RL.Engine/Battle/PhaseRunner.cs`
- `src/DCGO.RL.Engine/Battle/AttackService.cs`
- `src/DCGO.RL.Engine/Battle/SecurityCheckService.cs`
- `src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs`

`DurationCleanupService`는 `TemporaryModifier`와 `TemporaryGrantedEffect`를 같은 cleanup event 기준으로 제거한다. `Player.EffectList(...)`와 `Permanent.EffectList_Added(...)`는 `TemporaryGrantedEffectRegistry`를 명시적으로 받아 duration-bound granted trigger descriptor를 노출한다.

## Boundary

이번 항목에서 닫은 것은 cleanup timing evidence다. Production provider catalog adoption은 닫지 않았다.

- `providerCatalogAdoptionClosed=false`
- `providerCatalogBoundaryRetained=true`
- TRUST-001/FND-005 handoff 유지

또한 원본 `UntilNextUntapEffects`와 headless `UntilOwnerActivePhaseEnd`는 cleanup timing으로 대응하지만 이름이 일치하는 strict bucket-name parity는 닫지 않았다.

## Verification Result

- Required duration buckets: 7
- Covered source buckets: 7
- Covered headless buckets: 7
- Required tests: 16
- Covered tests: 16
- Foundation Gate: `OpenCodeReady=false`
- Selected capability: `ContinuousOrStaticEffect`
- Selected status: `PartiallyImplemented`

## Next Work

다음 CloseableFoundationTask는 `FND001-CS-06 supported static keyword descriptor parity`다.

TRUST-001-RERUN에서는 `DurationCleanupService`, `TemporaryGrantedEffectRegistry`, `ScriptRuntimeFoundation`의 reuse 가능성을 원본 `Player`/`Permanent`/`TurnStateMachine`/`AttackProcess`/`CardController` evidence와 다시 매핑해야 한다.
