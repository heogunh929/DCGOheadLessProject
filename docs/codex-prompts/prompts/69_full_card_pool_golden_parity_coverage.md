AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 69 - 전체 카드풀 Golden / Parity Coverage

## 목표

모든 카드를 하나씩 full trace로 실행하는 대신, 전체 mechanic family와 high-risk 카드군을 빠짐없이 대표하는 검증 corpus를 만든다.

## Coverage 요구

- 모든 EffectTiming family
- 모든 keyword
- 모든 zone movement family
- 모든 duration/continuous target kind
- 모든 selection kind/root
- shared effect
- variant-specific effect
- security flow
- simultaneous trigger priority
- counter/block/replacement
- special digivolution/play mechanics
- hidden information
- source manipulation

## 산출물

- mechanic -> scenario coverage matrix
- deterministic scripted scenarios
- replay fixtures
- Unity fixture 필요 목록
- RL-only synthetic fixture 구분
- Verified 승격 정책

## 판정

- unit test만 있는 카드는 Verified로 올리지 않는다.
- Unity fixture가 필요한데 없으면 parity는 NotRun이다.
- representative coverage가 없는 mechanic은 full completion blocker다.
