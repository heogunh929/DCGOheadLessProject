# 41 - stale 문서 정합성 정리

AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, 현재 /goal을 따르라.

이번 작업은 문서 정합성 정리다. 코드 구현은 하지 마라. 기존 DCGO Unity 원본 파일은 수정하지 마라. remote를 추가하거나 push/fetch/pull 하지 마라. 사용자 승인 없는 commit은 만들지 마라.

## 작업 시작 전 보고

- git status --short
- git diff --stat
- git diff --name-only
- git remote -v
- git log --oneline -5
- git status --short -- DCGO\Assets\Scripts

## 목표

문서에 남아 있는 과거 ST1 실패 상태와 최신 ST1/ST2/ST3 상태를 분리한다. Codex가 과거 문구를 현재 상태로 오해하지 않게 한다.

## 정리 대상

- docs/rl-engine/cardeffect-porting-status.md
- docs/rl-engine/engine-completion-report-st1.md
- docs/rl-engine/engine-completion-report-st1-st3.md, 존재하는 경우
- docs/rl-engine/engine-completion-checklist.md
- docs/rl-engine/effect-system.md
- docs/rl-engine/decision-and-selection.md
- docs/rl-engine/validation-strategy.md
- docs/rl-engine/golden-scenarios.md
- docs/progress/CHATGPT_HANDOFF.md
- docs/rl-engine/current-github-local-state.md

## 수행할 것

1. 각 문서 최상단에 `최신 상태 요약` 섹션을 만든다.
2. 과거 기록은 `이전 단계 기록` 또는 `Historical Notes`로 분리한다.
3. ST1 target deck 상태, ST1~ST3 target pool 상태, 전체 엔진 완성 상태를 혼동하지 않게 분리한다.
4. `ST1 실패`, `Unsupported 7종`, `Unsupported 3종`, `ST1-12 partial` 같은 과거 문구가 최신 판정처럼 보이면 수정한다.
5. ST1~ST3가 통과라고 문서화되어 있으면 실제 code/test/report 근거를 함께 적는다. 근거가 불충분하면 `검증 필요`로 낮춘다.
6. 학습 단계 미진입 원칙을 유지한다.

## 금지

- 코드 구현 금지
- validation 결과를 거짓으로 통과 처리 금지
- 기존 DCGO Unity 원본 수정 금지
- remote 추가/push/fetch/pull 금지
- 사용자 승인 없는 commit 금지

## 테스트

문서만 수정했다면 테스트는 필수 아님. 코드가 수정되면 전체 테스트 실행.

## 완료 보고

- 수정한 문서 목록
- 최신 상태와 과거 상태 분리 결과
- 남은 모순 또는 불확실성
- 다음 queue 항목
- commit은 만들지 않았다는 확인
