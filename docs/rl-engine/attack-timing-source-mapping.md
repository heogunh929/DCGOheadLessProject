# Attack Timing Source Mapping

이 문서는 queue 55/55A/55B/55C/55D/55E 기준으로 `DCGO/Assets/Scripts/Script/AttackProcess.cs`의 counter, block, attack target, end attack 흐름을 `DCGO.RL.Engine`에 대응한 결과를 기록한다. 카드별 effect body는 각 카드 script 파일에 유지하며, `AttackService`에는 특정 CardId 분기를 넣지 않는다.

## 원본 흐름

`AttackProcess.Attack()`은 공격 선언 시점에 attacker/defender를 기록하고, `CounterEffectHashtable = CardEffectCommons.OnAttackCheckHashtableOfPermanent(new Permanent(AttackingPermanent.cardSources), attackEffect)`로 counter trigger 조건 payload를 만든다. 이 값은 counter effect source whitelist가 아니라 공격 선언 당시 attacker 정보를 보존하는 snapshot payload다. 이후 attacker suspend, `OnAllyAttack`, `CounterTiming`, `BlockTiming`, battle/security, `OnEndAttack`, cleanup 순서로 진행한다.

| 원본 단계 | 원본 의미 | RL.Engine 대응 |
| --- | --- | --- |
| attack declaration | attacker/defender 기록, counter 조건 payload 생성, attacker suspend | `AttackService.AttackWithResult()`가 suspend 전에 `AttackRuntimeContext`를 `GameState.RuntimeRules.Attack`에 생성하고, 공통 `Tier1PrimitiveService.Suspend()`를 사용한다. suspend 실패 시 attack context를 clear한다. |
| `CounterTiming()` non-counter | `OnCounterTiming` 중 `IsCounterEffect == false` 후보를 먼저 처리 | `RunCounterNonCounterWithResult()`가 counter cut-in source 범위에서 non-counter 후보를 drain한다. counter snapshot payload는 counter 단계와 동일하게 전달한다. |
| `CounterTiming()` counter | `OnCounterTiming` 중 `IsCounterEffect == true` 후보를 처리하되 원본 `HasCounterEffect` skipCondition으로 실제 counter effect는 최대 1개만 사용 | `RunCounterCounterWithResult()`가 turn player group, non-turn player group 순서로 진행한다. `CounterUsed`는 선택 시점이 아니라 실제 counter effect가 실행된 뒤에만 등록한다. |
| `BlockTiming()` | 상대 blocker/collision 후보 선택. Collision이 아니면 no-select 가능 | `BattleKeywordService.CreateBlockerSelectionRequest()` 결과를 `EngineSession` decision으로 반환한다. |
| block 선택 | `SwitchDefender(..., isBlock: true)`, blocker suspend, `OnBlockAnyone` | resume 후 현재 state로 후보를 재검증하고 공통 suspend primitive를 사용한다. suspend 실패 시 block은 성립하지 않는다. defender switch는 `AttackRuntimeOperations.SwitchDefender()` 공통 경로를 사용한다. |
| target changed | defender가 실제 변경된 경우에만 `OnAttackTargetChanged` stack | `AttackRuntimeContext.TargetSwitchQueue`가 각 old/new transition을 순서대로 보존한다. 각 event는 전환 당시 `OldDefender`, `NewDefender`, `IsBlocking`, `Blocker`, `SourceEffectStableId` snapshot을 가진다. |
| battle/security | stale defender/blocker로 battle하지 않음 | counter 중 기존 defender가 사라져도 attacker가 유효하면 block designation까지 진행한다. block window 후 valid defender가 없으면 attack end로 간다. 기존 defender 삭제를 자동 security target 전환으로 해석하지 않는다. |
| end attack | attacker와 top card가 남아 있고 game over가 아닐 때만 `OnEndAttack` 후보 수집 | `ShouldRunOnEndAttack()`이 direct win/game over/attacker 유실을 검사한다. `OnEndAttack` payload에는 cleanup 전 blocking context가 남아 있다. |
| cleanup | 공격 종료 후 attack-duration effect 정리 | `DurationScope.UntilBattleEnd`는 battle 직후, `DurationScope.UntilAttackEnd`는 attack cleanup에서 제거한다. `CleanupAttackRuntime()`에서 attack runtime state를 제거한다. |

## Runtime Context

공격 중 rule-visible mutable state는 `GameState.RuntimeRules.Attack`의 `AttackRuntimeContext`가 소유한다.

- `Attacker`, `Defender`, `Blocker`, `IsBlocking`, `State`
- `AttackerTopCardWhenDeclared`
- `CounterSourceSnapshot`
- `CounterGroup`, `CounterUsed`
- `AttemptedCounterCandidates`
- ordered `TargetSwitchQueue`

이 상태는 `GameState.Clone()`, `RestoreFrom()`, `ComputeStateHash()`에 포함된다. direct synchronous API가 pending으로 rollback하면 attack context까지 복원되고, replay/hash 비교에서 공격 중 continuation 상태가 빠지지 않는다.

카드별 script와 keyword service는 공격 흐름을 바꿀 때 `AttackRuntimeOperations`를 사용한다.

- `EndAttack`
- `SwitchDefender`
- `SwitchToSecurityTarget`
- `SetBlocking`
- current attacker/defender/blocker query

`SwitchDefender`는 원본 `AttackingPermanent.CanSwitchAttackTarget`에 대응하는 `CannotSwitchAttackTarget` runtime restriction을 확인한다. 같은 effect batch에서 여러 번 defender를 바꾸면 `TargetSwitchQueue`가 각 transition을 순서대로 보존한다.

## Counter Snapshot Payload

non-counter `OnCounterTiming`과 counter `OnCounterTiming`은 모두 동일한 공격 선언 snapshot payload를 받는다.

- `Attacker`
- `Defender`
- `AttackerTopCardWhenDeclared`
- `CounterSourceSnapshot`

`CounterSourceSnapshot`은 후보 source whitelist가 아니다. 후보 수집은 원본 `AutoProcessing.GetSkillInfos`의 cut-in source 범위로 수행하고, snapshot은 `EffectContext.Values`의 조건 데이터로만 전달한다.

## Counter Source Coverage

원본 `AutoProcessing.GetSkillInfos`는 `!IsBackgroundProcess` 조건의 foreground effect를 대상으로 다음 source를 수집한다.

- player-level effects
- 양 플레이어 field top
- inherited sources
- linked cards
- trash
- hand
- face-up security

현재 RL.Engine 지원 상태는 다음과 같다.

| Source | Status | 비고 |
| --- | --- | --- |
| field top | supported | `TriggerSourceZone.FieldTop` |
| inherited source | supported | `TriggerSourceZone.Inherited` |
| linked card | supported | `TriggerSourceZone.Linked` |
| trash | supported | `TriggerSourceZone.Trash` |
| hand | supported | `TriggerSourceZone.Hand` |
| face-up security | supported | `TriggerSourceZone.FaceUpSecurity` |
| player-level effect | unsupported | 현재 모델에 player-level effect container가 없다. source coverage 항목으로 남긴다. |
| executing | not referenced | 원본 cut-in `GetSkillInfos` 경로의 source 범위 근거가 없어 `CounterCutInSourceOptions`에서 제외한다. |
| background counter | not selectable | 원본 `!IsBackgroundProcess` 의미에 맞춰 selectable counter 후보에서 제외한다. 별도 background counter timing 근거는 아직 없다. |

non-counter `OnCounterTiming`도 기본 `TriggerSourceZone`이 아니라 같은 cut-in source 범위를 사용한다.

## Counter Resolution Policy

원본 `CounterTiming()`은 첫 번째 `TriggeredSkillProcess(true, HasCounterEffect)`에서 어떤 counter effect가 실제 사용된 경우 이후 counter effect를 skip한다. RL.Engine은 이를 명시적인 group/selection state로 표현한다.

- 활성 후보가 0개면 다음 player group으로 진행한다.
- 활성 후보가 1개이고 mandatory이면 별도 ordering decision 없이 자동 실행한다.
- 활성 후보가 1개이고 optional이면 해당 effect의 optional yes/no boundary만 반환한다.
- 활성 후보가 여러 개일 때만 counter candidate 선택 decision을 반환한다.
- group의 모든 후보가 `IsSkippable`일 때만 전체 skip을 허용한다.
- 일부 mandatory 후보가 있으면 skip은 거부한다.
- turn player group이 counter를 실제 실행하면 전체 counter window를 종료한다.
- turn player group이 모두 skip/decline하고 아직 counter가 사용되지 않았다면 non-turn player group으로 진행한다.
- counter candidate를 선택했다는 이유만으로 `CounterUsed`를 켜지 않는다.
- 선택한 effect가 optional 거절되거나 실행 직전 `CanActivate` 재검증에 실패하면 `CounterUsed`로 처리하지 않는다.
- 거절/시도한 candidate는 `AttemptedCounterCandidates`에 기록해 같은 counter window에서 다시 제시하지 않는다.
- 실제 counter effect가 `TriggerPipelineResult.ExecutedEffects`에 기록된 뒤에만 `AttackRuntimeContext.MarkCounterUsed()`를 호출한다.

Blast Digivolution처럼 counter choice 자체가 activation 동의인 효과는 `CounterSelectionConsumesOptional` metadata를 사용해 optional yes/no를 중복 질문하지 않는다. 일반 optional counter는 selection 이후에도 optional 거절이 가능하고, 거절 후 같은 group의 남은 후보 또는 다음 group으로 복귀한다.

counter candidate selection id는 session-local identity로 만든다.

- candidate index
- source card instance id
- source permanent id
- effect stable id

따라서 같은 ACE 카드 두 장이 hand에 있어도 각각 선택할 수 있고, replay trace에도 선택된 source instance가 보존된다.

## 55D Counter Execution Persistence

`TriggerPipelineResult.ExecutedEffects`는 `TriggerPipelineService.RunPrepared()` 또는 `Resume()` 한 번의 반환 segment 안에서만 누적된다. counter body 실행 후 `RulesTiming` 또는 `AfterEffectsActivate`가 decision으로 다시 pause되면, 다음 `Resume()` 결과의 `ExecutedEffects`에는 이전 segment에서 실행한 counter effect가 포함되지 않을 수 있다.

따라서 counter 사용 여부는 마지막 pipeline segment의 `ExecutedEffects`만으로 판단하지 않는다. RL.Engine은 counter 후보 선택 시점에는 `CounterUsed=false`를 유지하고, 실제 counter body가 실행되어 해당 segment의 `ExecutedEffects.Any(effect => effect.IsCounterEffect)`가 확인되는 즉시 `AttackRuntimeContext.MarkCounterUsed()`를 호출해 `GameState.RuntimeRules.Attack`에 persistent하게 기록한다.

이 기록은 다음 상황에서 모두 유지된다.

- counter 선택 직후: `CounterUsed=false`
- optional yes/no 전: `CounterUsed=false`
- optional no 또는 `CanActivate` 재검증 실패: `CounterUsed=false`
- target selection 전: `CounterUsed=false`
- counter body 실행 직후: `CounterUsed=true`
- counter body 이후 nested `RulesTiming`/`AfterEffectsActivate` decision pause/resume: `CounterUsed=true`

`CompleteCounterEffectWithResult()`는 현재 resume segment의 `ExecutedEffects`와 이미 persisted된 `AttackRuntimeContext.CounterUsed`를 함께 확인한다. 이미 counter가 사용된 상태라면 같은 counter window의 남은 후보와 non-turn player group을 제시하지 않고 block designation으로 진행한다.

## Target Switch Event Payload

`AttackTargetSwitch`는 각 전환 당시 다음 payload를 보존한다.

- `OldDefender`
- `NewDefender`
- `IsBlocking`
- `Blocker`
- `SourceEffectStableId`

`OnAttackTargetChanged`의 `Defender`, `Blocker`, `IsBlocking` payload는 최종 `AttackRuntimeContext`가 아니라 해당 event snapshot을 사용한다. 예를 들어 A -> B -> C 전환에서는 첫 trigger에 `Defender=B`, 두 번째 trigger에 `Defender=C`가 전달된다. non-block target switch에서는 `Blocker=null`을 유지한다.

## 55D Target Switch Stage Resume

target switch drain은 `OnAttackTargetChanged`를 실행한 뒤 예상 switch가 발생한 원래 intended stage로 복귀해야 한다. 단일 `ContinueAfterAttackTargetChangedToBlockDesignation`으로 평탄화하면 `OnAllyAttack` 또는 non-counter timing에서 발생한 target switch가 counter stage를 건너뛰는 문제가 생긴다.

현재 stage mapping은 다음과 같다.

| switch 발생 위치 | `OnAttackTargetChanged` drain 후 복귀 |
| --- | --- |
| `OnAllyAttack` 이후 | `RunCounterNonCounterWithResult` |
| non-counter `OnCounterTiming` 이후 | `RunCounterCounterWithResult` |
| 실제 counter window 이후 | `RunBlockDesignationWithResult` |
| `OnBlockAnyone` 이후 | `ResolveBattleWithResult` |

이를 위해 attack continuation은 다음 네 경로를 구분한다.

- `ContinueAfterAttackTargetChangedToCounterNonCounter`
- `ContinueAfterAttackTargetChangedToCounterCounter`
- `ContinueAfterAttackTargetChangedToBlockDesignation`
- `ContinueAfterAttackTargetChangedToBattle`

한 effect batch에서 A -> B -> C target switch가 발생하면 `TargetSwitchQueue`가 각 event를 순서대로 drain하고, 모든 `OnAttackTargetChanged` frame이 끝난 뒤에만 원래 intended stage로 한 번 복귀한다. 이 보정은 target switch event snapshot payload와 non-block switch `Blocker=null` 의미를 변경하지 않는다.

## Target Revalidation

다음 단계 종료 후 attacker/defender/blocker를 재검증한다.

- `OnAllyAttack`
- non-counter window
- counter window
- `OnBlockAnyone`
- `OnAttackTargetChanged`
- block window 종료

attacker가 사라지거나 Digimon이 아니면 attack end로 이동한다. `OnBlockAnyone` 해소 후 blocker가 사라졌다면 `IsBlocking`을 해제하고 그 invalid blocker switch에 대해서는 `OnAttackTargetChanged`를 발생시키지 않는다. attacker가 사라졌다면 곧바로 attack end로 이동한다.

## Duration Cleanup

`DurationScope.UntilBattleEnd`와 `DurationScope.UntilAttackEnd`를 분리한다.

- permanent battle 직후 `CleanupBattleEnd`
- security battle 직후 `CleanupBattleEnd`
- 각 security card 종료 후 `CleanupSecurityCheckEnd`
- 공격 종료 cleanup 때 `CleanupAttackEnd`

따라서 `UntilBattleEnd` modifier는 `OnEndAttack` 이전에 사라지고, `UntilAttackEnd` modifier는 `OnEndAttack` 중에는 보이며 attack cleanup 이후 제거된다.

## OnEndBlockDesignation 판정

로컬 `DCGO/Assets/Scripts` 전체에서 `OnEndBlockDesignation`은 enum/descriptor 언급 외 실제 호출 위치가 확인되지 않았다. `AttackProcess.SwitchDefender()`나 `BlockTiming()`에도 무조건 호출 흐름이 없다.

현재 status: `NotReferenced`.

RL.Engine은 prompt에 있다는 이유만으로 `OnEndBlockDesignation` timing을 생성하지 않는다. source 근거가 발견되면 별도 queue에서 source path와 조건을 문서화한 뒤 추가한다.

## 55E Audit Coverage

55E는 다음 55D 보정이 테스트와 문서에 남아 있는지 확인한다.

- counter body 실제 실행 직후 persistent `CounterUsed=true`
- counter body 이후 nested decision pause/resume에서도 두 번째 counter 미제시
- `OnAllyAttack`/non-counter/counter/block trigger에서 발생한 target switch가 원래 stage로 복귀
- `OnAttackTargetChanged` payload가 각 `AttackTargetSwitch` snapshot을 사용
- non-block switch의 `Blocker=null`
- counter chained decision replay/state hash determinism
- ST1-06/ST1-09 regression
