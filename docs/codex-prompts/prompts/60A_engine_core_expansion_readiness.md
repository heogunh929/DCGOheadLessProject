AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 60A - 전체 카드풀 확장 Readiness Review

## 목표

52~59A 결과를 종합해 전체 DCGO snapshot inventory 단계로 넘어갈 수 있는지 검수한다.

## 검증

- 전체 테스트
- engine-core gate
- multiple deterministic seeds
- random legal action smoke
- decision pause/resume smoke
- golden scenario
- trace export/compare synthetic fixture
- invariant fuzz
- source mapping audit
- runtime composition audit
- no Unity source modifications

## 판정

다음 중 하나만 사용한다.

```text
ReadyForFullCardPoolInventory
BlockedByCoreParity
BlockedByRuntimeDecisionBoundary
BlockedBySourceAlignment
NeedsReview
```

`ReadyForRlEnvironmentDesign` 판정은 사용하지 않는다.

## 완료 후

- ready이면 61번 source snapshot pin 진행 추천
- blocked이면 구체적인 remediation prompt 파일을 생성
- RL 구현 금지
