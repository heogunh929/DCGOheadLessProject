AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 91 - Engine Parity Checkpoint Commit

이 prompt는 사용자가 명시적으로 승인했을 때만 실행한다.

## 작업

1. worktree 변경 범위를 보고한다.
2. `DCGO/Assets/Scripts` 변경이 없는지 확인한다.
3. 전체 관련 테스트를 실행한다.
4. 문서와 코드 상태가 일치하는지 확인한다.
5. 사용자에게 추천 commit message를 제시한다.
6. prompt 호출 자체에 commit 승인 문구가 포함된 경우에만 commit한다.
7. push는 하지 않는다.

## 금지

- 테스트 실패 상태 commit
- 원본 Unity 변경 포함 commit
- build artifact commit
- remote push
- 여러 queue 작업을 섞은 불명확한 commit

## 완료 보고

- commit hash/message
- 포함 범위
- 테스트 결과
- clean/dirty status
- remote 사용 없음
