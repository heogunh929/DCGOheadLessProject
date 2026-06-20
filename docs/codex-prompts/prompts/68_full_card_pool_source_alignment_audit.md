AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 68 - 전체 카드풀 Source Alignment Audit

## 목표

포팅 완료 후 고정 원본 snapshot과 RL.Engine 전체 mapping을 다시 대조한다.

## 검사

- source lock 일치
- 모든 asset identity
- 모든 `CardEffectClassName`
- source body 존재
- shared effect mapping
- variant mapping
- registry/status/card file
- original-like path
- no direct zone mutation
- catalog registry-only
- helper body hiding
- core card-id shortcut
- full mechanic inventory status

## blocker

- false NoEffect
- missing source body
- unknown variant
- source class mismatch
- missing per-card mapping
- stale status
- unregistered source class
- source lock drift

## 출력

- machine-readable report
- set/mechanic별 한국어 report
- upstream source defect 목록
- 사용자 승인 제외 항목 목록, 있다면

사용자 승인 없는 exclusion은 허용하지 않는다.
