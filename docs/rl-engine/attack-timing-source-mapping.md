# Attack timing source mapping

이 문서는 queue 55 기준 `DCGO/Assets/Scripts/Script/AttackProcess.cs`의 counter/block/attack target timing을 `DCGO.RL.Engine`에 대응시킨 기록이다. 카드별 effect body는 각 카드 script 파일에 두며, `AttackService`나 catalog에는 CardId 분기를 두지 않는다.

## 원본 흐름

`AttackProcess.Attack()`는 attacker와 defender를 보존하고 attacker를 suspend한 뒤 `OnAllyAttack`을 stack한다. 이후 `AttackState`는 `Counter`, `Block`, `Battle`, `End`, `CleanUp` 순서로 진행한다.

| 원본 단계 | 원본 의미 | RL.Engine 대응 |
| --- | --- | --- |
| attack declaration | attacker/defender 설정, attacker suspend, `OnAllyAttack` stack | `AttackService.AttackWithResult()`가 `AttackFrame` 생성 후 attacker suspend와 `OnAllyAttack` 실행 |
| `CounterTiming()` 1차 | `OnCounterTiming` 중 `IsCounterEffect == false` 후보 처리 | `RunCounterNonCounterWithResult()`가 descriptor filter로 비-counter 후보만 실행 |
| `CounterTiming()` 2차 | `OnCounterTiming` 중 counter 후보 처리 | `RunCounterCounterWithResult()`가 `IsCounterEffect == true` 후보만 실행 |
| `BlockTiming()` | 상대 blocker 후보 선택, Collision이면 skip 불가 | `BattleKeywordService.CreateBlockerSelectionRequest()` 결과를 `EngineSession` decision으로 반환 |
| block 선택 | blocker suspend, defender switch, block flag 설정 | `ResumeBlockerSelectionWithResult()`가 selection 재검증 후 blocker suspend와 `AttackFrame.Defender` 교체 |
| block trigger | `OnBlockAnyone` stack | `RunTriggerPipelineWithResult(EffectTiming.OnBlockAnyone)` |
| target changed | defender가 바뀌면 `OnAttackTargetChanged` stack | `RunAttackTargetChangedIfNeededWithResult()` |
| end block designation | block designation 종료 timing | `RunEndBlockDesignationWithResult()`가 `OnEndBlockDesignation` 실행 |
| battle/security | Digimon battle, piercing/security check/direct win | `ResolveBattleWithResult()`가 `BattleResolver`/`SecurityCheckService`/`WinConditionChecker` 연결 |
| end attack | `OnEndAttack`, battle-end cleanup | `RunEndAttackWithResult()`와 `DurationCleanupService.CleanupBattleEnd()` |

## Continuation 경계

`AttackExecutionContinuation`은 `AttackFrame`을 보존한다. trigger body 또는 security check가 selection을 요구하면 기존 `TriggerPipelineContinuation`/`SecurityCheckContinuation`과 함께 attack tail을 보존한다. blocker selection은 trigger body가 아니므로 `DirectSelectionRequest`와 synthetic `EffectResolution`으로 `EngineSession.Resume(DecisionResult)` 경계에 들어온다.

Resume 검증은 공통 engine boundary에서 player, request id, `DecisionToken`을 확인한다. blocker selection resume 시점에는 현재 state로 blocker 후보를 다시 만들고 `SelectionValidator`를 다시 실행한다. blocker가 suspend되었거나 `CannotBlock` modifier를 얻었거나 field를 떠난 경우 stale selection으로 실패한다.

## Source 조건

원본 `CardEffectCommons.CanTriggerOnAttack()`는 hashtable의 `AttackingPermanent`가 해당 card source를 포함하는지 확인한다. RL.Engine은 attack context payload의 `Attacker`를 사용한다.

- `ST1_06`/`ST2-07`/`ST3-07` shared body의 `OnAllyAttack`은 source permanent가 실제 attacker일 때만 memory -2를 실행한다.
- `ST1_09` inherited `OnBlockAnyone`은 source permanent가 실제 attacker이고 owner turn일 때만 memory +3을 실행한다.
- blocker 자체의 CardId나 source class를 core service에서 분기하지 않는다.

## 현재 제한

counter 후보 구분은 `EffectDescriptor.IsCounterEffect` metadata로 표현한다. ST1~ST3에는 counter card body가 없으므로 fixture로 timing filter와 decision boundary를 검증했다. ACE/Blast Evolution 같은 future counter body는 카드별 script에서 `IsCounterEffect`를 명시해야 한다.

`OnEndBlockDesignation`은 원본 호출 위치와 queue 요구사항에 맞춰 block window 종료 후 battle/security 전에 실행한다. 이 timing에서 발생하는 nested MultipleSkills/AfterEffects 처리는 queue 54B의 trigger stack frame 경계를 재사용한다.
