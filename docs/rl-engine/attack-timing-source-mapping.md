# Attack Timing Source Mapping

이 문서는 queue 55/55A/55B 기준으로 `DCGO/Assets/Scripts/Script/AttackProcess.cs`의 counter, block, attack target, end attack 흐름을 `DCGO.RL.Engine`에 대응한 결과를 기록한다. 카드별 effect body는 각 카드 script 파일에 유지하고, `AttackService`는 특정 CardId 분기를 갖지 않는다.

## 원본 흐름

`AttackProcess.Attack()`은 공격 선언 시점에 attacker/defender를 기록하고, `CounterEffectHashtable = CardEffectCommons.OnAttackCheckHashtableOfPermanent(new Permanent(AttackingPermanent.cardSources), attackEffect)`로 counter trigger 조건 평가용 attacker snapshot payload를 만든다. 이 값은 counter effect source whitelist가 아니다. 그 뒤 attacker를 suspend하고 `OnAllyAttack`, `Counter`, `Block`, `Battle`, `End`, `CleanUp` 상태로 진행한다.

| 원본 단계 | 원본 의미 | RL.Engine 대응 |
| --- | --- | --- |
| attack declaration | attacker/defender 기록, counter 조건 payload용 attacker card-source snapshot 생성, attacker suspend, `OnAllyAttack` stack | `AttackService.AttackWithResult()`가 suspend 전에 `AttackRuntimeContext`를 `GameState.RuntimeRules`에 시작하고, 공유 `Tier1PrimitiveService.Suspend()` 뒤 `OnAllyAttack`을 실행한다. suspend 실패 시 attack context를 clear한다. |
| `CounterTiming()` non-counter | `OnCounterTiming` 중 `IsCounterEffect == false` 후보를 먼저 처리 | `RunCounterNonCounterWithResult()`가 counter cut-in source 범위에서 비-counter 후보만 drain한다. |
| `CounterTiming()` counter | `OnCounterTiming` 중 `IsCounterEffect == true` 후보를 처리하되 원본 `HasCounterEffect` skipCondition 때문에 실제 counter 효과는 최대 1개만 사용 | `RunCounterCounterWithResult()`가 player-level 지원 범위, 양 플레이어 field top/inherited/linked/hand/trash/executing/face-up security source에서 counter 후보를 수집하고 turn player group 뒤 non-turn player group을 진행한다. 선택된 1개만 `RunPrepared()`로 실행한다. |
| `BlockTiming()` | 상대 battle area Digimon 중 blocker/collision 후보 선택. Collision이 아니면 no-select 가능 | `BattleKeywordService.CreateBlockerSelectionRequest()` 결과를 `EngineSession` decision으로 반환한다. |
| block 선택 | `SwitchDefender(..., isBlock: true)`, blocker suspend, `OnBlockAnyone` | resume 시 현재 state로 후보를 재검증하고 공유 `Tier1PrimitiveService.Suspend()`를 사용한다. suspend 실패 시 block은 성립하지 않는다. defender switch는 `AttackRuntimeOperations.SwitchDefender()` 공통 경로를 사용한다. |
| target changed | defender가 실제 변경된 경우에만 `OnAttackTargetChanged` stack | `AttackRuntimeContext.TargetSwitchQueue`를 순서대로 소비해 old/new defender, `IsBlock`, blocker, source effect stable id payload를 보존한 뒤 실행한다. |
| battle/security | stale defender/blocker로 battle하지 않음 | counter 중 기존 defender가 사라져도 block designation까지 진행한다. block window 뒤 valid defender가 없으면 attack end로 간다. 명시적 `SwitchToSecurityTarget()`인 경우에만 security target으로 취급한다. |
| end attack | attacker와 top card가 남아 있을 때만 `OnEndAttack` 후보 수집. direct win/game over 뒤 불필요한 attack trigger는 실행하지 않음 | `ShouldRunOnEndAttack()`이 direct win/game over/attacker 유실을 검사한다. `EndAttack` 상태 전환 시 `IsBlocking`/`Blocker`/`Defender`를 즉시 지우지 않고 `OnEndAttack` payload에 남긴다. |
| cleanup | 전체 공격 종료 후 `UntilEndAttackEffects` 정리 | `DurationScope.UntilBattleEnd`는 battle 직후, `DurationScope.UntilAttackEnd`는 attack cleanup에서 제거한다. `CleanupAttackRuntime()`에서 attack runtime state를 제거한다. |

## Runtime Context

공격 중 rule-visible mutable state는 `GameState.RuntimeRules.Attack`의 `AttackRuntimeContext`가 소유한다.

- `Attacker`, `Defender`, `Blocker`, `IsBlocking`, `State`
- `AttackerTopCardWhenDeclared`
- `CounterSourceSnapshot`
- `CounterGroup`, `CounterUsed`
- ordered `TargetSwitchQueue`

이 상태는 `GameState.Clone()`, `RestoreFrom()`, `ComputeStateHash()`에 포함된다. 따라서 direct synchronous API가 pending으로 rollback될 때 attack context까지 함께 복원되고, replay/hash 비교에서도 공격 중 continuation 상태가 누락되지 않는다.

카드별 script와 keyword service가 공격 흐름을 바꿀 때는 `AttackRuntimeOperations`를 사용한다.

- `EndAttack`
- `SwitchDefender`
- `SwitchToSecurityTarget`
- `SetBlocking`
- current attacker/defender/blocker query

`SwitchDefender`는 원본 `AttackingPermanent.CanSwitchAttackTarget`에 대응하는 `CannotSwitchAttackTarget` runtime restriction을 확인한다. CardId 예외는 core에 두지 않는다. 같은 effect batch에서 여러 번 defender를 바꿔도 `TargetSwitchQueue`가 각 old/new transition을 순서대로 보존한다.

## Counter Window

원본 `CounterTiming()`은 두 번의 `StackOnAttackCheck()`를 호출한다.

1. `!cardEffect.IsCounterEffect`
2. `cardEffect.IsCounterEffect`

두 번째 호출은 `TriggeredSkillProcess(true, HasCounterEffect)`로 실행되고, `HasCounterEffect`는 이미 사용된 `skillInfos_used`에 counter effect가 1개 이상 있으면 이후 counter effect를 skip한다. RL.Engine은 이 의미를 명시적인 counter 선택 decision으로 표현한다.

- `CounterEffectHashtable`은 attacker snapshot 조건 payload다. 후보 source whitelist로 사용하지 않는다.
- 후보 source 범위는 현재 지원되는 cut-in source 전체다: 양 플레이어 field top, inherited, linked, hand, trash, executing, face-up security. player-level effect는 별도 모델이 생기면 같은 수집 경계에 추가한다.
- non-counter `OnCounterTiming`도 기본 `TriggerSourceZone`이 아니라 같은 cut-in source 범위를 사용한다.
- turn player group을 먼저 보여주고, turn player가 counter를 선택하면 counter window를 종료한다.
- turn player group의 모든 후보가 skippable이고 skip하면, 아직 counter가 사용되지 않았을 때 non-turn player group으로 넘어간다.
- 모든 후보가 skippable일 때만 전체 skip을 허용한다. 일부 mandatory 후보가 있으면 skip은 거부된다.
- Blast Digivolution처럼 counter 선택 자체가 activation 동의인 효과는 `CounterSelectionConsumesOptional` metadata로 optional yes/no를 중복 질문하지 않는다.
- counter candidate selection id는 `candidate index + source card instance id + source permanent + effect stable id`를 포함한다. 같은 ACE 카드 두 장이 hand에 있어도 각각 선택할 수 있고 replay trace에 source instance가 남는다.
- 선택된 counter effect body가 target selection을 요구하면 기존 trigger pipeline continuation으로 pause/resume한다.

## Target Revalidation

다음 단계 종료 후 attacker/defender/blocker를 재검증한다.

- `OnAllyAttack`
- non-counter window
- counter window
- `OnBlockAnyone`
- `OnAttackTargetChanged`
- block window 종료

attacker가 사라지거나 Digimon이 아니면 attack end로 이동한다. counter 중 기존 defender가 사라져도 block designation 전에는 attack을 끝내지 않는다. blocker가 새로 선택되면 blocker가 defender가 되고, block window 뒤 valid defender가 없으면 attack end로 이동한다. 기존 defender 삭제를 security target 전환으로 자동 해석하지 않는다.

`OnBlockAnyone` 해소 후 blocker가 사라졌다면 `IsBlocking`을 해제하고, 그 invalid blocker switch에 대해서는 `OnAttackTargetChanged`를 발생시키지 않는다. attacker가 사라졌다면 곧바로 attack end로 이동한다.

## Duration Cleanup

`DurationScope.UntilBattleEnd`와 `DurationScope.UntilAttackEnd`를 분리한다.

- permanent battle 직후 `CleanupBattleEnd`
- security battle 직후 `CleanupBattleEnd`
- 각 security card 종료 후 `CleanupSecurityCheckEnd`
- 공격 종료 cleanup 때 `CleanupAttackEnd`

따라서 `UntilBattleEnd` modifier는 `OnEndAttack` 이전에 사라지고, `UntilAttackEnd` modifier는 `OnEndAttack` 중에는 보이며 attack cleanup 이후 제거된다.

## OnEndBlockDesignation 판정

로컬 `DCGO/Assets/Scripts` 전체에서 `OnEndBlockDesignation`은 enum/descriptor 언급 외 실제 호출 위치가 확인되지 않았다. `AttackProcess.SwitchDefender()`와 `BlockTiming()`에도 무조건 호출하는 흐름이 없다.

현재 status: `NotReferenced`.

RL.Engine은 prompt에 있다는 이유만으로 `OnEndBlockDesignation` timing을 생성하지 않는다. source 근거가 발견되면 별도 queue에서 source path와 조건을 문서화한 뒤 추가한다.

## 추가 테스트

55A/55B에서 추가/보정한 핵심 테스트:

- counter 후보 2개 중 1개만 실행
- skippable counter skip, mandatory counter skip 거부
- counter attacker snapshot payload와 source whitelist 분리
- non-turn player hand counter 후보
- field/trash/face-up-security counter 후보
- turn player skip 후 non-turn player counter 선택
- turn player counter 사용 후 non-turn player counter 미실행
- 동일 counter 카드 인스턴스 2장 selection id 구분
- counter 선택 후 optional 중복 질문 없음
- counter 중 defender 삭제 후 blocker 선택 가능
- `OnBlockAnyone` 중 blocker 삭제 시 target-change 미발생
- effect-driven `SwitchDefender`, double switch queue 순서 보존
- `CannotSwitchAttackTarget` restriction
- blocker suspend lifecycle
- `OnEndAttack` attacker 존재 조건과 blocking context payload
- `UntilBattleEnd`/`UntilAttackEnd` 분리
- `OnEndBlockDesignation` NotReferenced audit
- attack decision replay/ST1 regression
