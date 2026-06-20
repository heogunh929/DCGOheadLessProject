# 53A Source-Aligned Security Multi-Check Correction

목표: DCGO 원본 `ISecurityCheck.SecurityCheck()`의 card 1장 단위 state-machine을 headless engine에 맞게 보정한다. 54번 `AfterEffectsActivate`/`MultipleSkills` priority는 실행하지 않는다.

필수 범위:

- `SecurityCheckService`에서 for-loop와 recursive continuation이 동시에 remaining check를 소비하는 구조를 제거한다.
- 한 step은 현재 check 가능 여부 확인, 한 장 reveal/move, card별 `SecuritySkill`, `OnSecurityCheck`/`OnLoseSecurity`, security battle, final zone, 해당 card의 `SecurityCheckEnd` cleanup, 다음 card 여부 판단만 처리한다.
- 최초 check count를 고정 저장하지 않고 매 card check 직전에 현재 attacker의 `SecurityAttackCount`를 다시 계산한다.
- continuation은 remaining count 대신 `ChecksCompleted`를 보존한다.
- `UntilSecurityCheckEnd` cleanup은 전체 공격 종료가 아니라 각 security card check 종료마다 실행한다.
- `OnSecurityCheck` 후보는 security card 이동 및 `SecuritySkill` 전에 snapshot한다.
- `OnLoseSecurity` 후보는 실제 security 감소 시점에 snapshot한다.
- snapshot 후보는 `SecuritySkill` 이후 해소하되, 그 이후 현재 state에서 새로 재수집하지 않는다.
- `ScriptedScenarioRunner.Run`과 `RandomLegalActionRunner.Run`은 provider 유무와 관계없이 `PausedForDecision` result를 반환하지 않는다. paused라면 `StartSession`/`Resume` 사용을 안내하는 `DomainException`으로 실패한다.

검증:

- SecurityAttack 2/3 다중 check 정확성
- 첫 check 효과로 SecurityAttack 감소/증가 시 다음 check 수 반영
- 첫 card의 `UntilSecurityCheckEnd` modifier가 두 번째 card에 영향 없음
- `OnSecurityCheck` 후보가 `SecuritySkill` 전에 snapshot됨
- `SecuritySkill` 이후 새로 생긴 source가 진행 중 timing 후보에 포함되지 않음
- 첫/두 번째 security card selection pause/resume
- security timing replay deterministic
- provider optional+target one-shot `Run` 명시 실패
- ST1~ST3 security regression
- 전체 regression
