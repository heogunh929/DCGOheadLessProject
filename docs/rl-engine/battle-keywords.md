# Battle Keywords

이 문서는 battle keyword와 attack lifecycle의 현재 대응 범위를 기록한다. RL.Engine은 카드별 effect body를 각 카드 script에 유지하고, keyword/core service에 특정 CardId 분기를 두지 않는다.

## 구현된 keyword

| Keyword | RL.Engine 처리 |
| --- | --- |
| Blocker | `BattleKeywordService.CreateBlockerSelectionRequest()`가 block window에서 후보를 만들고 `AttackService`가 resume 시 현재 state로 재검증한다. |
| Collision | attacker가 Collision을 가지면 상대 battle area Digimon 전체를 block 후보처럼 취급하고 skip을 금지한다. |
| Security Attack | `BattleKeywordService.SecurityAttackCount()`가 card definition, temporary modifier, continuous effect를 합산한다. |
| Piercing | permanent battle에서 defender가 파괴되고 attacker가 남으면 security check로 이어진다. |
| Jamming | security Digimon battle에서 attacker 파괴를 방지한다. |
| Retaliation | DP battle에서 Retaliation을 가진 loser가 battle로 파괴되면 상대 permanent도 파괴 목록에 추가한다. |
| Reboot | active phase에서 non-turn player의 Reboot permanent도 unsuspend한다. |
| Rush | 등장 턴 공격 제한의 예외로 처리한다. |
| Decoy | replacement 선택 구조가 아직 없으므로 명시적으로 unsupported 처리한다. |

## Attack Lifecycle 연결

queue 55/55A/55B 기준 `AttackService`는 원본 `AttackProcess`의 공격 선언, counter timing, block timing, target switch, battle/security, end attack, cleanup 흐름을 `GameState.RuntimeRules.Attack`의 `AttackRuntimeContext`로 보존한다.

- attack context와 attacker snapshot은 suspend 전에 생성한다.
- attacker suspend는 공유 `Tier1PrimitiveService.Suspend()` 경계를 사용한다.
- `OnAllyAttack`, non-counter `OnCounterTiming`, counter 선택, blocker selection, `OnBlockAnyone`, `OnAttackTargetChanged`, battle/security, `OnEndAttack`은 `EngineSession.Resume(DecisionResult)` boundary로 이어진다.
- `OnEndBlockDesignation`은 로컬 DCGO 원본 호출 근거가 없어 attack lifecycle에서 생성하지 않는다.

## Counter Window

원본 `CounterEffectHashtable`은 counter source whitelist가 아니라 공격 선언 당시 attacker 정보를 보존하는 trigger-condition payload다. RL.Engine은 이 snapshot을 `AttackRuntimeContext.CounterSourceSnapshot`과 effect context values에 보존하되, 후보 source 제한으로 사용하지 않는다.

counter 후보 수집 범위:

- 양 플레이어 field top
- inherited sources
- linked cards
- hand
- trash
- executing
- face-up security

진행 정책:

- non-counter `OnCounterTiming`도 같은 cut-in source 범위를 사용한다.
- turn player group을 먼저 처리한다.
- turn player가 counter를 선택하면 non-turn player group은 실행하지 않는다.
- turn player group 전체가 skippable이고 skip되었으며 아직 counter가 사용되지 않았을 때만 non-turn player group으로 넘어간다.
- 양쪽을 합쳐 실제 counter effect는 최대 1개만 실행한다.
- candidate id에는 candidate index, source card instance, source permanent, effect stable id를 포함한다.
- counter 선택 자체가 activation 동의인 효과는 `CounterSelectionConsumesOptional` metadata로 optional yes/no를 중복 질문하지 않는다.

## Block And Target

- blocker 선택은 resume 시 현재 state로 `CanBlock`을 다시 검사한다.
- blocker suspend가 실패하면 block은 성립하지 않는다.
- counter 중 기존 defender가 사라져도 block designation 전에는 attack을 끝내지 않는다.
- `OnBlockAnyone` 후 blocker가 사라지면 `IsBlocking`을 해제하고 invalid blocker switch에 대한 `OnAttackTargetChanged`를 만들지 않는다.
- 한 effect batch에서 defender switch가 여러 번 발생하면 `AttackRuntimeContext.TargetSwitchQueue`가 old/new target change를 순서대로 보존한다.
- block window 종료 후 valid defender가 없으면 attack end로 이동한다. 기존 defender 삭제를 security target 전환으로 자동 해석하지 않는다.

## End Attack

`OnEndAttack`은 direct win/game over가 아니고 attacker와 top card가 남아 있을 때만 수집한다. EndAttack 상태로 전환할 때 `IsBlocking`/`Blocker`/`Defender`를 즉시 지우지 않으므로, `OnEndAttack` effect는 공격 종료 직전 context payload를 볼 수 있다. runtime state는 `CleanupAttackRuntime()`에서만 제거한다.

## 남은 범위

- 실제 ACE/Blast Evolution counter body
- replacement/cut-in priority 전체
- face-up security/player continuous keyword provider 확장
- Security Attack 음수/반전/상수화 ordering
- Decoy, barrier, evade, armor purge 같은 replacement keyword
