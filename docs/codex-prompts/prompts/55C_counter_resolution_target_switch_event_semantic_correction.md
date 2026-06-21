# 55C Counter Resolution And Target-Switch Event Semantic Correction

## 목표

55B 이후 counter resolution과 target-switch event payload를 DCGO 원본 `AttackProcess.CounterTiming` 및 `AutoProcessing.GetSkillInfos` 의미에 맞게 보정한다.

## 필수 조건

- 56번 golden scenarios는 실행하지 않는다.
- commit과 push는 수행하지 않는다.
- `DCGO/Assets/Scripts` 원본은 수정하지 않는다.
- 카드별 effect body는 각 카드 파일에 유지한다.
- core service에 CardId 분기를 추가하지 않는다.

## 작업 항목

1. Counter snapshot payload 통일
   - non-counter `OnCounterTiming`과 counter `OnCounterTiming` 모두 동일한 공격 선언 snapshot payload를 사용한다.
   - payload에는 `Attacker`, `Defender`, `AttackerTopCardWhenDeclared`, `CounterSourceSnapshot`을 포함한다.
   - `CounterSourceSnapshot`은 후보 source whitelist로 사용하지 않는다.

2. 실제 Counter 사용 시점 보정
   - 후보 선택 시점에는 `CounterUsed=true`로 만들지 않는다.
   - 선택한 효과가 실제 실행된 경우에만 `CounterUsed`를 등록한다.
   - optional 거절 또는 `CanActivate` 재검증 실패는 `CounterUsed`로 처리하지 않는다.
   - 거절/시도된 후보는 현재 counter window에서 다시 제시하지 않는다.
   - 같은 group의 남은 후보를 계속 처리하고, group 종료 후 아직 counter가 사용되지 않았으면 다음 player group으로 진행한다.

3. Counter 후보 수별 흐름
   - 활성 후보 0개: 다음 group으로 진행.
   - 활성 후보 1개 + mandatory: 별도 ordering decision 없이 자동 실행.
   - 활성 후보 1개 + optional: 해당 effect의 optional boundary만 노출.
   - 활성 후보 여러 개: counter candidate 선택 decision 노출.
   - 여러 hand optional counter의 choice 자체가 activation 동의인 경우에만 `CounterSelectionConsumesOptional` 적용.
   - 일반 optional counter는 선택 후 optional 거절 가능.

4. Background Counter 분리
   - selectable counter 후보에는 background effect를 포함하지 않는다.
   - 원본 `!IsBackgroundProcess` 의미를 문서화한다.

5. Counter source 범위 감사
   - 원본 `GetSkillInfos`와 `CounterCutInSourceOptions`를 대조한다.
   - Executing zone은 원본 근거 없이 포함하지 않는다.
   - player-level effect는 현재 모델이 없으면 Unsupported coverage item으로 남긴다.

6. Target switch event snapshot
   - `AttackTargetSwitch`는 각 전환 당시 `OldDefender`, `NewDefender`, `IsBlocking`, `Blocker`, `SourceEffectStableId`를 보존한다.
   - non-block target switch의 `Blocker`는 null이어야 한다.
   - `OnAttackTargetChanged` payload는 최종 attack context가 아니라 해당 event snapshot을 사용한다.

7. 테스트
   - non-counter timing에서 `CounterSourceSnapshot` 확인.
   - optional counter 거절 후 같은 group의 다음 counter 실행.
   - turn-player optional counter 거절 후 non-turn counter 기회 제공.
   - 실제 실행 전 `CounterUsed=false`.
   - single mandatory counter 자동 실행.
   - single optional counter는 optional boundary만 반환.
   - background counter가 selectable candidate에 포함되지 않음.
   - Executing source의 원본 판정.
   - ABC target switch별 Defender payload.
   - non-block switch의 `Blocker=null`.
   - chained decision replay/state hash.
   - 전체 regression.

8. Queue
   - 수정 전 55B를 needs-review로 표시하고 55C를 추가한다.
   - 모든 테스트 통과 후 55B/55C를 done으로 변경한다.
   - 다음 실행 순서를 61 -> 62 -> 63 -> 56 -> 57 -> 58로 재배치한다.
   - 61 source snapshot pin은 사용자 승인 전 needs-review로 종료해야 한다.
   - 54C는 59A 이전 todo를 유지한다.

## 완료 보고

한국어로 다음을 보고한다.

- 변경 파일
- 실제 `CounterUsed` 등록 시점
- single/multiple candidate 처리
- background/player-level/executing source 판정
- target switch event payload
- 전체 테스트 결과
- git status/diff
- DCGO 원본 변경 없음
- 추천 commit message
