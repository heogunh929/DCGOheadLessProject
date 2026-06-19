AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 47 - Engine Parity Queue 전환 및 기준점 확정

이번 작업은 새 기능 구현이 아니라 현재 기준점 검증과 queue 전환 작업이다.

## 목표

GitHub `main`의 현재 기준점과 로컬 상태를 확인하고, 기존 github-current queue에서 이 engine-parity queue로 안전하게 전환한다.

## 확인할 것

1. 현재 HEAD와 최신 commit message를 보고한다.
2. 이 패키지 생성 기준 GitHub main은 `a101acd2 20260618 local latest`다.
3. 로컬 HEAD가 다르면:
   - 해당 commit의 후손인지 확인한다.
   - 후손이고 clean worktree라면 더 최신 로컬 기준점을 사용한다.
   - 다른 history거나 dirty 상태 원인이 불명확하면 `needs-review`로 멈춘다.
4. `docs/codex-prompts/state/QUEUE_GITHUB_CURRENT.md`의 40~46이 done인지 확인한다.
5. 기존 47번은 이 prompt로 대체됐음을 문서화한다.
6. `README.md`와 `docs/codex-prompts/prompts/INDEX.md`가 현재 queue를 반영하지 않으면 다음을 갱신한다.
   - engine-parity queue 사용법
   - 47~60/90/91 prompt 목록
   - 과거 queue는 historical로 유지
7. `QUEUE_GITHUB_CURRENT.md`의 47 항목은 `done` 또는 `superseded-done`으로 표시할 수 있다.
8. 코드는 수정하지 않는다.

## 테스트

문서/queue만 변경했다면 전체 테스트는 필수가 아니다.
테스트를 실행하지 않은 이유를 보고한다.

## 완료 조건

- 현재 기준 commit이 명확하다.
- `QUEUE_ENGINE_PARITY.md`의 다음 todo가 48번이다.
- README/INDEX에서 새 queue 실행법을 찾을 수 있다.
- `DCGO/Assets/Scripts` 변경 없음.
