# 40 - GitHub/로컬 현재 상태 재정렬

AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, 현재 /goal을 따르라.

이번 작업은 구현이 아니라 현재 상태 감사와 정합성 정리다. 기존 DCGO Unity 원본 파일은 수정하지 마라. remote를 추가하거나 push/fetch/pull 하지 마라. 사용자 승인 없는 commit은 만들지 마라.

## 작업 시작 전 보고

반드시 실행하고 한국어로 보고하라.

- git status --short
- git diff --stat
- git diff --name-only
- git remote -v
- git log --oneline -5
- git status --short -- DCGO
- git status --short -- DCGO\Assets\Scripts
- git diff --name-only -- DCGO\Assets\Scripts

## 목표

GitHub main에서 확인되는 상태와 로컬 상태를 비교하고, 앞으로 진행할 기준 상태를 명확히 한다.

## 확인할 것

1. `README.md`가 설명하는 현재 workspace 구조와 실제 로컬 구조가 일치하는지 확인한다.
2. `docs/codex-prompts/prompts/INDEX.md`가 여전히 00~20만 나열하는지, 로컬에 21 이후 prompt/queue가 있는지 확인한다.
3. `docs/codex-prompts/GOAL_*`, `ACTIVE/*`, `state/*` queue 파일의 존재 여부를 확인한다.
4. `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_*.cs` 파일 존재 여부를 확인한다.
5. `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_*.cs`, `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_*.cs` 파일 존재 여부를 확인한다.
6. `St1CardScriptCatalog`가 registry 역할만 하는지 확인한다.
7. ST2/ST3 catalog 또는 registry 파일이 있다면 effect body를 포함하는지 확인한다.
8. 문서가 ST1 통과와 ST1 실패를 동시에 최신 상태로 말하지 않는지 확인한다.
9. 로컬과 GitHub main의 차이가 있으면 어떤 쪽이 최신 작업 기준인지 명시한다.

## 문서 생성/갱신

`docs/rl-engine/current-github-local-state.md`를 생성 또는 갱신한다.

반드시 포함:

- GitHub main 확인 요약
- 로컬 상태 확인 요약
- latest local commit
- remote 상태
- DCGO/Assets/Scripts 변경 여부
- 카드별 파일 구조 존재 여부
- 문서 정합성 문제 목록
- 다음에 실행할 queue 항목 추천

## 금지

- 코드 구현 금지
- ST2/ST3 신규 포팅 금지
- 기존 DCGO Unity 원본 수정 금지
- 학습용 RL 구성 구현 금지
- remote 추가/push/fetch/pull 금지
- 사용자 승인 없는 commit 금지

## 완료 보고

한국어로 보고하라.

- 실행한 Git 명령 결과 요약
- 생성/수정한 문서
- 현재 기준점 판단
- 다음 queue 항목
- commit은 만들지 않았다는 확인
