# Security Timing Source Mapping

이 문서는 DCGO Unity 원본의 security check 흐름과 headless `DCGO.RL.Engine` 구현의 대응 관계를 기록한다. `DCGO/Assets/Scripts` 원본은 읽기 전용 Source of Truth이며 이 작업에서 수정하지 않는다.

## 원본 Source

주요 원본 파일:

- `DCGO/Assets/Scripts/Script/AttackProcess.cs`
  - `DetermineAttackOutcome()`에서 security 공격이면 `new ISecurityCheck(...).SecurityCheck()`를 호출한다.
- `DCGO/Assets/Scripts/Script/CardController.cs`
  - `ISecurityCheck.SecurityCheck()`가 security card 1장 단위 loop를 수행한다.
  - `IReduceSecurity.ReduceSecurity()`가 `OnLoseSecurity` 후보를 즉시 stack하거나, 전달받은 `refSkillInfos`에 추가한다.
- `DCGO/Assets/Scripts/Script/AutoProcessing.cs`
  - `AutoProcessCheck()`는 rule process, `RulesTiming`, `TriggeredSkillProcess()` 순서로 자동 처리를 수행한다.
  - `PutStackedSkill()`은 trigger 시점의 permanent/top card snapshot을 `ActivateICardEffect`에 저장한다.

원본 `ISecurityCheck.SecurityCheck()`의 card 1장 처리 순서:

1. 공격 Digimon이 security check를 계속할 수 있는지 확인한다.
2. 매 card check 직전에 `checkedCount >= AttackingPermanent.Strike`를 검사한다. `Strike`가 효과로 바뀌면 다음 card check 가능 여부도 바뀐다.
3. top security card를 `brokenSecurityCard`로 잡고 face-down 여부를 저장한다.
4. `AutoProcessing.GetSkillInfos(..., EffectTiming.OnSecurityCheck)`로 `OnSecurityCheck` 후보를 수집한다.
5. `checkedCount++` 후 security card를 공개 연출하고 Executing area로 이동한다.
6. `IReduceSecurity(..., ref triggeredSkillInfos, null).ReduceSecurity()`로 security 감소 시점의 `OnLoseSecurity` 후보를 같은 후보 목록에 추가한다.
7. 공개된 card 자신의 `SecuritySkill` 후보를 실행한다.
8. `AutoProcessCheck()`를 수행한다.
9. 앞에서 수집한 `OnSecurityCheck`/`OnLoseSecurity` 후보를 stack한다.
10. `AutoProcessCheck()`를 다시 수행해 security timing trigger를 해소한다.
11. security Digimon이면 battle을 수행한다.
12. checked card가 아직 Executing에 있으면 Trash로 보낸다.
13. `UntilSecurityCheckEndEffects`를 양 플레이어 모두 초기화한다.
14. `AutoProcessCheck()` 후 다음 security card 여부를 판단한다.

## RL.Engine 대응

주요 구현 파일:

- `src/DCGO.RL.Engine/Battle/SecurityCheckService.cs`
  - card 1장 단위 state-machine으로 security check를 진행한다.
  - continuation은 `ChecksCompleted`, 현재 security card, prepared trigger group, pending effect/trigger continuation을 보존한다.
  - `MoveSecurityToExecuting()`은 Security -> Executing 공개 이동을 담당한다.
  - `ResolveSecurityBattleAndTrashIfNeeded()`는 security Digimon battle과 final zone 처리를 담당한다.
  - `DurationCleanupService.CleanupSecurityCheckEnd()`는 card 1장 종료마다 실행된다.
- `src/DCGO.RL.Engine/Effects/SecurityEffectExecutionService.cs`
  - card별 `SecuritySkill` body를 해당 card script에서 실행한다.
  - `SecuritySkill` context payload에는 `Attacker`, `AttackingPermanent`, `Defender`, `SecurityCard`, `IsFaceDown`, `SourceZone`을 전달한다.
- `src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs`
  - `Prepare()`/`RunPrepared()`로 원본 `SkillInfo` 후보 snapshot에 대응하는 `PreparedTriggerGroup`을 만든다.
  - prepared group은 후보를 다시 수집하지 않고 같은 `GameState` instance에서만 해소된다.

현재 RL.Engine의 card 1장 처리 순서:

1. 다음 check 가능 여부를 확인한다.
2. 현재 attacker의 `SecurityAttackCount`를 다시 계산하고 `ChecksCompleted`와 비교한다.
3. top security card를 선택하고 `OnSecurityCheck` 후보를 `PreparedTriggerGroup`으로 준비한다.
4. security card를 Executing으로 이동한다.
5. security 감소 시점의 `OnLoseSecurity` 후보를 `PreparedTriggerGroup`으로 준비한다.
6. card별 `SecuritySkill`을 실행한다. pending selection이면 continuation으로 pause한다.
7. 준비된 `OnSecurityCheck` group을 해소한다. pending selection이면 continuation으로 pause한다.
8. 준비된 `OnLoseSecurity` group을 해소한다. pending selection이면 continuation으로 pause한다.
9. security Digimon battle과 checked card final zone 처리를 수행한다.
10. `UntilSecurityCheckEnd` modifier를 cleanup한다.
11. 다음 card check 가능 여부를 다시 판단한다.

## Check Count

원본은 최초 check count를 고정하지 않고 loop 안에서 매번 `AttackingPermanent.Strike`를 확인한다. RL.Engine도 최초 `checkCount`를 저장하지 않고, 매 card check 직전에 `BattleKeywordService.SecurityAttackCount(state, attacker)`를 다시 계산한다.

- 첫 check 효과로 `SecurityAttack`이 감소하면 `ChecksCompleted`가 현재 count 이상이 되어 남은 check가 중단된다.
- 첫 check 효과로 `SecurityAttack`이 증가하면 증가된 count까지 추가 check가 가능하다.
- continuation은 남은 횟수가 아니라 `ChecksCompleted`를 보존한다.

## Trigger Candidate Snapshot

원본은 `OnSecurityCheck` 후보를 security card 이동과 `SecuritySkill` 전에 수집하고, `OnLoseSecurity` 후보는 실제 security 감소 시점에 수집한다. 이후 `SecuritySkill`과 `AutoProcessCheck()`를 거친 뒤 수집된 후보를 stack한다.

RL.Engine은 이를 다음처럼 대응한다.

- `OnSecurityCheck`: security card를 Executing으로 옮기기 전에 `TriggerPipelineService.Prepare()`로 후보를 준비한다.
- `OnLoseSecurity`: Security -> Executing 이동 직후 `Prepare()`로 후보를 준비한다.
- 두 후보 group은 `SecuritySkill` 이후 `RunPrepared()`로 해소한다.
- `SecuritySkill` 이후 새로 생긴 source는 이미 진행 중인 `OnSecurityCheck`/`OnLoseSecurity` 후보에 포함되지 않는다.
- 준비된 source가 이후 zone을 벗어나도 후보 목록은 유지한다. activation 가능성의 세부 재검증, `MultipleSkills` 선택 순서, `AfterEffectsActivate` interleaving은 queue 54에서 원본 `CanActivate`/priority 정책과 함께 정렬한다.

## Source Zone Policy

Security timing 후보 source zone:

- field top
- inherited source
- hand
- trash
- face-up security

`Executing`은 제외한다. checked security card 자신의 `SecuritySkill`은 card별 body로 처리하며, 같은 checked card가 common `OnSecurityCheck`/`OnLoseSecurity` source로 중복 수집되지 않게 한다.

## 남은 54 범위

다음 항목은 53A 범위 밖이며 queue 54에서 다룬다.

- `MultipleSkills` priority와 UI 선택 순서의 원본 동등성
- `TriggeredSkillProcess()`와 `AfterEffectsActivate` timing의 전체 priority model
- security check 중간 `AutoProcessCheck()`의 rule process/RulesTiming/AfterEffectsActivate 세부 interleaving
- prepared trigger source의 activation 재검증 세부 정책
