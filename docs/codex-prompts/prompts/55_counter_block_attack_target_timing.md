AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 55 - Counter / Block / Attack Target Timing

## 목표

원본 `AttackProcess`의 counter, block designation, defender switch, target change, end block designation, end attack 흐름을 headless attack state machine에 연결한다.

## 원본 참조

- attack declaration
- `OnAllyAttack`
- `OnCounterTiming`
- blocker 후보와 선택
- block designation
- defender switch
- `OnAttackTargetChanged`
- battle/security resolution
- `OnEndBlockDesignation`
- `OnEndAttack`
- cleanup

## 구현 원칙

1. attack state를 명시 enum/state object로 관리한다.
2. 각 decision boundary는 `SelectionRequest`로 노출한다.
3. counter와 blocker가 없을 때 자동 진행한다.
4. target 변경 후 legality와 stale target을 재검증한다.
5. cancel/redirect가 원본에서 허용되는 범위만 구현한다.
6. timing 미지원은 명시 실패/report로 남긴다.
7. 카드별 조건은 카드 파일 descriptor에 유지한다.

## 테스트

- no-counter/no-block 기본 공격
- counter decision boundary
- blocker 선택
- target switch
- invalid/stale blocker
- OnAttackTargetChanged
- OnEndBlockDesignation
- OnEndAttack 순서
- ST1-06/ST1-09 end-to-end
- cannot-block restriction
- replay determinism/invariant

## 문서

attack state/timing mapping과 partial 범위를 갱신한다.
