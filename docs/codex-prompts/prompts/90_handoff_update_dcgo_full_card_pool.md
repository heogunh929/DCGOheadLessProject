AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 90 - DCGO Full Card Pool ChatGPT Handoff

새 구현 없이 `docs/progress/CHATGPT_HANDOFF.md` 최상단 snapshot을 갱신한다.

포함:

- 최신 commit/branch
- 현재 main/subqueue 항목
- 전체 목표와 현재 milestone
- source lock 상태
- 전체 asset/variant counts
- mechanic inventory counts
- porting status counts
- blocker 목록
- engine-core/full-card/full-gate/RL 범위 구분
- 테스트 결과
- Git diff/status
- DCGO 원본 변경 여부
- 다음 명령
- ChatGPT 검수 질문
- 30~50줄 복사용 요약

과거 handoff는 Historical로 유지한다.
commit은 하지 않는다.
