# Security Timing Source Mapping

이 문서는 DCGO 원본의 security check 처리 순서와 RL.Engine의 대응 지점을 기록한다. 원본 Unity 파일은 읽기 전용 Source of Truth이며, 이 작업에서는 수정하지 않았다.

## 원본 흐름

참조 원본:

- `DCGO/Assets/Scripts/Script/AttackProcess.cs`
  - `DetermineAttackOutcome()`에서 security 공격이면 `new ISecurityCheck(...).SecurityCheck()`를 호출한다.
- `DCGO/Assets/Scripts/Script/CardController.cs`
  - `ISecurityCheck.SecurityCheck()`가 실제 security check loop를 수행한다.
  - `IReduceSecurity.ReduceSecurity()`가 `OnLoseSecurity` 후보를 수집하거나 즉시 stack한다.
- `DCGO/Assets/Scripts/Script/AutoProcessing.cs`
  - `AutoProcessCheck()`는 rule process, `RulesTiming`, `TriggeredSkillProcess()`를 순서대로 실행한다.
  - `TriggeredSkillProcess()`는 queued skill을 처리한 뒤 `AfterEffectsActivate`를 stack한다.

원본 `ISecurityCheck.SecurityCheck()`의 핵심 순서:

1. 공격 Digimon이 더 이상 security check를 지속할 수 없는지 확인한다.
2. top security card를 `brokenSecurityCard`로 잡고 face-down 여부를 저장한다.
3. `OnSecurityCheck` 후보를 `AutoProcessing.GetSkillInfos(...)`로 수집한다.
4. security card를 공개 표시하고 Executing area로 이동한다.
5. `IReduceSecurity(..., ref triggeredSkillInfos, null).ReduceSecurity()`로 `OnLoseSecurity` 후보를 같은 임시 목록에 추가한다.
6. 공개된 card 자신의 `SecuritySkill` 후보를 실행한다.
7. `AutoProcessCheck()`를 한 번 실행한다.
8. 수집해 둔 `OnSecurityCheck`/`OnLoseSecurity` skill을 `autoProcessing` stack에 넣는다.
9. `AutoProcessCheck()`를 다시 실행해 security timing trigger를 해소한다.
10. security Digimon이면 battle을 수행한다.
11. checked card가 아직 Executing에 있으면 Trash로 보낸다.
12. `UntilSecurityCheckEnd` 효과를 정리하고 자동 처리를 한 번 더 실행한다.

## RL.Engine 대응

대응 구현:

- `src/DCGO.RL.Engine/Battle/SecurityCheckService.cs`
  - `CheckSecurityWithResult()`가 security loop와 pause/resume continuation을 소유한다.
  - `MoveSecurityToExecuting()`이 Security -> Executing 공개 이동을 담당한다.
  - `RunSecurityTimingWithResult()`가 `OnSecurityCheck`와 `OnLoseSecurity`를 공통 `TriggerPipelineService`로 실행한다.
  - `ResolveSecurityBattleAndTrashIfNeeded()`가 security Digimon battle과 checked card 최종 Trash를 처리한다.
  - `DurationCleanupService.CleanupSecurityCheckEnd()`가 `UntilSecurityCheckEnd` cleanup을 담당한다.
- `src/DCGO.RL.Engine/Effects/SecurityEffectExecutionService.cs`
  - card별 `SecuritySkill` body를 각 card script에서 실행한다.
- `src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs`
  - field/inherited/hand/trash/face-up security source의 common security timing trigger를 수집하고 실행한다.

RL.Engine의 53번 구현 순서:

1. Security card를 Executing으로 이동한다.
2. checked card의 card-specific `SecuritySkill`을 실행한다.
3. 공통 pipeline으로 `OnSecurityCheck`를 실행한다.
4. 공통 pipeline으로 `OnLoseSecurity`를 실행한다.
5. security Digimon battle과 checked card final zone 처리를 진행한다.
6. security check 종료 cleanup을 수행한다.

원본은 `OnSecurityCheck`/`OnLoseSecurity` 후보를 SecuritySkill 전에 수집하지만 실제 해소는 SecuritySkill 이후 자동 처리에서 일어난다. RL.Engine은 현재 별도 `SkillInfo` 보관 큐가 없으므로, 실행 순서를 SecuritySkill 이후로 맞추고 checked card가 trigger source로 잘못 포함되지 않도록 security timing source zone에서 `Executing`을 제외한다.

## Source Zone 정책

`OnSecurityCheck`/`OnLoseSecurity`는 다음 source zone만 사용한다.

- field top
- inherited source
- hand
- trash
- face-up security

`Executing`은 제외한다. 원본 `AutoProcessing.GetSkillInfos(...)`의 security timing 수집에서 checked security card가 Executing에 있다고 해서 그 card 자신의 `OnSecurityCheck`/`OnLoseSecurity` source로 다시 잡히는 구조가 아니기 때문이다. face-up security card는 원본과 같이 source 후보가 될 수 있다.

## 보류된 차이

다음 항목은 53번의 common security timing 연결 범위를 넘어선다.

- `MultipleSkills` priority와 선택 순서 UI의 완전한 원본 동등성
- `TriggeredSkillProcess()` 뒤 `AfterEffectsActivate` timing의 전체 priority 모델
- security check 중간 `AutoProcessCheck()`의 rule process/RulesTiming/AfterEffectsActivate 세부 interleaving

위 항목은 queue 54 이후의 `AfterEffectsActivate/MultipleSkills priority` 작업에서 다룬다.
