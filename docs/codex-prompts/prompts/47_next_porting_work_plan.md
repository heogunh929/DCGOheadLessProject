# 47 - 다음 포팅 작업 계획 생성

AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, 현재 /goal을 따르라.

이번 작업은 다음 개발 계획 생성이다. 코드 구현은 하지 마라. 기존 DCGO Unity 원본 파일은 수정하지 마라. remote를 추가하거나 push/fetch/pull 하지 마라. 사용자 승인 없는 commit은 만들지 마라.

## 작업 시작 전 보고

- git status --short
- git diff --stat
- git diff --name-only
- git remote -v
- git log --oneline -5
- git status --short -- DCGO\Assets\Scripts

## 목표

34~46 감사 결과를 바탕으로 다음에 실제로 진행할 포팅 작업 queue를 생성한다.

## 수행할 것

1. 구조 감사 결과를 종합한다.
2. 문서 정합성 문제가 해결되었는지 확인한다.
3. 카드별 파일 구조 guard가 있는지 확인한다.
4. ST1~ST3 validation 상태를 확인한다.
5. 다음 작업을 아래 중 하나로 결정한다.
   - ST2/ST3 추가 포팅 계속
   - 구조 refactor 보강
   - 공통 layer 보강
   - golden scenario 구현
   - Unity trace 비교 준비
6. 결정한 방향에 맞는 prompt queue 문서를 작성한다.

## 생성/갱신할 수 있는 파일

- docs/codex-prompts/state/QUEUE_NEXT_PORTING.md
- docs/codex-prompts/ACTIVE/RUN_NEXT_PORTING.md
- docs/codex-prompts/GOAL_NEXT_PORTING.md
- docs/codex-prompts/prompts/48_*.md 이후 파일

## 금지

- 실제 포팅 구현 금지
- 기존 DCGO Unity 원본 수정 금지
- 학습용 RL 구성 구현 금지
- remote 추가/push/fetch/pull 금지
- 사용자 승인 없는 commit 금지

## 완료 보고

- 다음 목표
- 생성한 prompt queue 파일
- 사용자가 실행할 /goal 문구
- 다음에 입력할 명령
