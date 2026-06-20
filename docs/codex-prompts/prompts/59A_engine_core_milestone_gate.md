AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 59A - ST1~ST3 Engine-Core Milestone Gate

## 중요

이 gate는 전체 DCGO 카드풀 completion이 아니며 RL 학습 진입 gate도 아니다.
ST1~ST3를 이용해 공통 engine core가 전체 카드풀 inventory를 시작할 수준인지 판정한다.

## Gate

- runtime composition graph
- shared dependency identity
- RuleProcessor/ZoneMover injection
- common decision pause/resume
- option Executing lifecycle
- security timing
- MultipleSkills/AfterEffects 지원 범위
- counter/block/target timing 지원 범위
- ST1~ST3 structure/source mapping
- asset registry validator
- ST3-02 unresolved source finding
- golden scenario batch 1
- replay determinism
- invariant fuzz
- parity trace contract/comparer
- forbidden dependency
- silent skip zero

## 판정

```text
ReadyForFullCardPoolInventory
BlockedByCoreParity
BlockedBySourceData
NeedsReview
```

- `ST3-02` source body 미확인이 남으면 이를 report에 반드시 표시한다.
- 해당 blocker가 full inventory 자체를 막는지, 특정 variant implementation만 막는지 구분한다.
- ST1~ST3 pass를 `EngineComplete` 또는 `ReadyForRL`로 표현하지 않는다.

## 산출물

- JSON report
- 한국어 markdown report
- gate별 evidence
- 실패 항목의 다음 작업
- scope 명시
