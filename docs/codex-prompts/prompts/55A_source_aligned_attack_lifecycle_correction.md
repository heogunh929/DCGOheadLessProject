# 55A Source-Aligned Attack Lifecycle Correction

## 목표

`DCGO/Assets/Scripts/Script/AttackProcess.cs`의 attack lifecycle을 기준으로 counter, block, switch defender, suspend, attack end, duration cleanup을 source-aligned하게 보정한다. 56 golden scenarios는 실행하지 않는다.

## 필수 조건

- commit/push 금지.
- `DCGO/Assets/Scripts` 원본 수정 금지.
- 카드별 effect body는 각 카드 파일에 유지한다.
- core service에 CardId 분기를 추가하지 않는다.
- 원본 호출 근거가 없는 timing은 prompt에 있다는 이유만으로 생성하지 않는다.

## 검증 항목

- counter window는 원본 `HasCounterEffect` skipCondition처럼 실제 counter 효과를 최대 1개만 실행한다.
- counter 후보가 여러 개이면 counter 사용 player에게 선택 decision을 반환한다.
- counter skip 허용 여부는 source mapping에 명시한다.
- attack runtime state는 session별로 격리되고 clone/rollback/replay/hash 가능한 `GameState` 소유 상태로 둔다.
- effect-driven switch와 blocker switch는 같은 공통 operation을 사용한다.
- blocker suspend는 공통 primitive/service를 사용한다.
- 각 attack stage 뒤 attacker/defender/blocker를 재검증한다.
- `OnEndAttack`은 attacker와 top card가 남아 있고 game over가 아닐 때만 수집한다.
- `UntilBattleEnd`와 `UntilAttackEnd` cleanup을 분리한다.
- `OnEndBlockDesignation` 실제 호출 위치를 원본에서 감사하고, 근거가 없으면 `NotReferenced`로 문서화한다.

## 완료 조건

- source mapping 문서 갱신.
- queue/progress 갱신.
- 전체 regression 통과.
