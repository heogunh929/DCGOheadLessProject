# Attack Timing Source Mapping

이 문서는 queue 55/55A 기준으로 `DCGO/Assets/Scripts/Script/AttackProcess.cs`의 counter, block, attack target, end attack 흐름을 `DCGO.RL.Engine`에 대응한 결과를 기록한다. 카드별 effect body는 각 카드 script 파일에 유지하고, `AttackService`는 특정 CardId 분기를 갖지 않는다.

## 원본 흐름

`AttackProcess.Attack()`은 공격 선언 시점에 attacker/defender를 기록하고, `CounterEffectHashtable = CardEffectCommons.OnAttackCheckHashtableOfPermanent(new Permanent(AttackingPermanent.cardSources), attackEffect)`로 counter trigger용 attacker snapshot을 만든다. 그 뒤 attacker를 suspend하고 `OnAllyAttack`을 처리한 다음 `Counter`, `Block`, `Battle`, `End`, `CleanUp` 상태로 진행한다.

| 원본 단계 | 원본 의미 | RL.Engine 대응 |
| --- | --- | --- |
| attack declaration | attacker/defender 기록, counter용 attacker card-source snapshot, attacker suspend, `OnAllyAttack` stack | `AttackService.AttackWithResult()`가 `AttackRuntimeContext`를 `GameState.RuntimeRules`에 시작하고, 공유 `Tier1PrimitiveService.Suspend()` 후 `OnAllyAttack`을 실행 |
| `CounterTiming()` non-counter | `OnCounterTiming` 중 `IsCounterEffect == false` 후보를 먼저 처리 | `RunCounterNonCounterWithResult()`가 descriptor filter로 비-counter 후보만 drain |
| `CounterTiming()` counter | `OnCounterTiming` 중 `IsCounterEffect == true` 후보를 처리하되 원본 `HasCounterEffect` skipCondition 때문에 실제 counter 효과는 최대 1개만 사용 | `RunCounterCounterWithResult()`가 attacker snapshot source에서 counter 후보를 준비하고, counter 사용 player에게 `ChooseAction` decision을 반환. skip도 허용하며 선택된 1개만 `RunPrepared()`로 실행 |
| `BlockTiming()` | 상대 battle area Digimon 중 blocker/collision 후보 선택. Collision이 아니면 no-select 가능 | `BattleKeywordService.CreateBlockerSelectionRequest()` 결과를 `EngineSession` decision으로 반환 |
| block 선택 | `SwitchDefender(..., isBlock: true)`, blocker suspend, `OnBlockAnyone` | resume 시 현재 state로 후보를 재검증하고 공유 `Tier1PrimitiveService.Suspend()`를 사용한다. suspend 실패 시 block은 성립하지 않는다. defender switch는 `AttackRuntimeOperations.SwitchDefender()` 공통 경로를 사용 |
| target changed | defender가 실제 변경된 경우에만 `OnAttackTargetChanged` stack | `AttackRuntimeContext.PendingTargetSwitch`를 소비해 old/new defender, `IsBlock`, blocker, source effect stable id payload를 보존한 뒤 실행 |
| battle/security | stale defender/blocker로 battle하지 않음. defender가 없으면 security target, stale defender면 공격 종료 쪽으로 정리 | `ResolveBattleWithResult()`가 각 단계 후 attacker/defender/blocker를 재검증하고 stale target 예외 대신 attack end 또는 security target 상태로 진행 |
| end attack | attacker와 top card가 남아 있을 때만 `OnEndAttack` 후보 수집. direct win/game over 뒤 불필요한 attack trigger는 실행하지 않음 | `ShouldRunOnEndAttack()`이 direct win/game over/attacker 유실을 검사한다 |
| cleanup | 전체 공격 종료 시 `UntilEndAttackEffects` 정리 | `DurationScope.UntilBattleEnd`는 battle 직후, `DurationScope.UntilAttackEnd`는 attack cleanup에서 제거 |

## Runtime Context

공격 중 rule-visible mutable state는 `GameState.RuntimeRules.Attack`의 `AttackRuntimeContext`가 소유한다.

- `Attacker`, `Defender`, `Blocker`, `IsBlocking`, `State`
- `AttackerTopCardWhenDeclared`
- `CounterSourceSnapshot`
- `PendingTargetSwitch`

이 상태는 `GameState.Clone()`, `RestoreFrom()`, `ComputeStateHash()`에 포함된다. 따라서 direct synchronous API가 pending으로 rollback될 때 attack context도 함께 복원되고, replay/hash 비교에서도 공격 중 continuation 상태가 누락되지 않는다.

카드별 script나 keyword service가 공격 흐름을 바꿀 때는 `AttackRuntimeOperations`를 사용한다.

- `EndAttack`
- `SwitchDefender`
- `SwitchToSecurityTarget`
- `SetBlocking`
- current attacker/defender/blocker query

`SwitchDefender`는 원본 `AttackingPermanent.CanSwitchAttackTarget`에 대응하는 `CannotSwitchAttackTarget` runtime restriction을 확인한다. CardId 예외는 core에 두지 않는다.

## Counter Window

원본 `CounterTiming()`은 두 번의 `StackOnAttackCheck()`를 호출한다.

1. `!cardEffect.IsCounterEffect`
2. `cardEffect.IsCounterEffect`

두 번째 호출은 `TriggeredSkillProcess(true, HasCounterEffect)`로 실행되고, `HasCounterEffect`는 이미 사용된 `skillInfos_used`에 counter effect가 1개 이상 있으면 이후 counter effect를 skip한다. RL.Engine은 이 의미를 명시적인 counter 선택 decision으로 표현한다.

- counter 후보가 0개면 block window로 진행한다.
- 후보가 있으면 counter 사용 player에게 `ChooseAction` request를 반환한다.
- skip은 허용된다.
- 선택한 후보 1개만 실행한다.
- 선택된 counter effect body가 다시 optional/target selection을 요구하면 기존 trigger pipeline continuation으로 pause/resume한다.
- counter 후보는 attack 선언 시 저장한 `CounterSourceSnapshot`을 기준으로 준비하여, live attacker source collection 변화만으로 후보가 바뀌지 않게 한다.

## Target Revalidation

다음 단계 종료 후 attacker/defender/blocker를 재검증한다.

- `OnAllyAttack`
- non-counter window
- counter window
- `OnBlockAnyone`
- `OnAttackTargetChanged`
- block window 종료

attacker가 사라지거나 Digimon이 아니면 attack end로 이동한다. defender/blocker가 사라지면 stale target으로 battle하지 않고 source-aligned attack end 경로로 정리한다. defender가 명시적으로 `SwitchToSecurityTarget()`으로 null이 된 경우에만 security target으로 처리한다.

## Duration Cleanup

`DurationScope.UntilBattleEnd`와 `DurationScope.UntilAttackEnd`를 분리한다.

- permanent battle 직후 `CleanupBattleEnd`
- security battle 직후 `CleanupBattleEnd`
- 각 security card 종료 시 `CleanupSecurityCheckEnd`
- 공격 종료 cleanup 시 `CleanupAttackEnd`

따라서 `UntilBattleEnd` modifier는 `OnEndAttack` 이전에 사라지고, `UntilAttackEnd` modifier는 `OnEndAttack` 중에는 보이며 attack cleanup 이후 제거된다.

## OnEndBlockDesignation 판정

로컬 `DCGO/Assets/Scripts` 전체에서 `OnEndBlockDesignation`은 enum/descriptor 언급 외 실제 호출 위치가 확인되지 않았다. `AttackProcess.SwitchDefender()`와 `BlockTiming()`에도 무조건 호출하는 흐름이 없다.

현재 status: `NotReferenced`.

RL.Engine은 prompt에 있다는 이유만으로 `OnEndBlockDesignation` timing을 생성하지 않는다. source 근거가 발견되면 별도 queue에서 source path와 조건을 문서화한 뒤 추가한다.

## 회귀 테스트

55A에서 추가/보정한 핵심 테스트:

- counter 후보 2개 중 1개만 실행
- counter skip
- counter attacker snapshot payload
- `OnBlockAnyone`이 blocker를 삭제
- counter가 defender/attacker를 삭제
- effect-driven `SwitchDefender`
- `CannotSwitchAttackTarget` restriction
- blocker suspend lifecycle
- `OnEndAttack` attacker 존재 조건
- `UntilBattleEnd`/`UntilAttackEnd` 분리
- `OnEndBlockDesignation` NotReferenced audit
- attack decision replay/ST1 regression
