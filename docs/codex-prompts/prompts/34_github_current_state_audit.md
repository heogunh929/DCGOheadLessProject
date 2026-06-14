# 34 - GitHub main 및 로컬 현재 상태 감사

AGENTS.md 지침, docs/progress/LOCAL_GIT_GUIDE.md 지침, docs/codex-prompts/templates/common_constraints.md, 현재 /goal을 따르라.

이번 작업은 구현이 아니라 감사 작업이다. 코드를 수정하지 마라.

## 목표

GitHub main과 현재 로컬 작업공간의 상태를 비교해, 앞으로 ST1~ST3 작업을 어떤 기준으로 진행해야 하는지 확정한다.

## 작업 시작 전 실행

- git status --short
- git diff --stat
- git diff --name-only
- git remote -v
- git log --oneline -5
- git status --short -- DCGO
- git status --short -- DCGO\Assets\Scripts

## 수행할 것

1. 현재 로컬 latest commit과 remote/main 또는 GitHub main의 최신 상태가 일치하는지 확인한다.
2. remote가 없거나 fetch 금지 정책 때문에 직접 비교할 수 없다면, 그 사실을 보고하고 현재 로컬 기준으로만 감사한다.
3. docs/rl-engine/cardeffect-porting-status.md를 확인해 최신 상태가 무엇인지 요약한다.
4. ST1, ST2, ST3 카드별 파일이 실제로 존재하는지 확인한다.
5. 다음 예시 경로가 존재하는지 확인한다.
   - src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_08.cs
   - src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_01.cs
   - src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_13.cs
6. St1CardScriptCatalog 및 ST2/ST3 catalog가 registry 이상의 effect body를 갖는지 확인한다.
7. 최신 문서에 ST1 target deck 통과, ST1~ST3 target validation 통과/실패 여부가 어떻게 기록되어 있는지 정리한다.
8. 문서 간 모순이 있으면 목록화한다. 이 작업에서는 수정하지 않고 목록만 만든다.

## 산출물

- docs/rl-engine/github-current-state-audit.md 생성 또는 갱신

반드시 포함:

- 현재 로컬 commit
- remote 상태
- GitHub main과 비교 가능 여부
- ST1/ST2/ST3 카드별 파일 존재 여부
- catalog 책임 감사 결과
- 문서 모순 목록
- 다음 작업 권고

## 금지

- 코드 수정 금지
- 문서 중 감사 보고서 외 수정 금지
- ST2/ST3 구현 금지
- commit 금지

## 완료 보고

- git status --short
- git diff --stat
- git diff --name-only
- 생성/수정한 문서
- 구조상 즉시 멈춰야 할 위험 여부
