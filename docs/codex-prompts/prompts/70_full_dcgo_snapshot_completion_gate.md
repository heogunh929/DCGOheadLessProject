AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 70 - Full DCGO Snapshot Completion Gate

## 목표

사용자 승인으로 고정된 DCGO source snapshot의 전체 카드풀에 대한 최종 엔진 completion을 판정한다.

## 필수 Gate

1. source lock
2. full asset manifest
3. full mechanic inventory
4. all asset identities registered
5. all source effects mapped
6. per-card/source-effect file structure
7. zero Unsupported
8. zero PartiallyImplemented
9. zero StubbedForValidation
10. zero NeedsSourceReview
11. zero UnknownVariant
12. zero unapproved upstream defect exclusion
13. engine-core milestone pass
14. generic decision pause/resume
15. runtime composition
16. option/security/trigger/attack timing
17. full-card validator
18. golden mechanic coverage
19. Unity/RL parity evidence
20. replay determinism
21. invariant fuzz
22. random legal action stability
23. performance smoke
24. forbidden dependency
25. RL components still absent

## 판정

```text
CompleteForPinnedDcgoSnapshot
BlockedBySourceData
BlockedByCardPorting
BlockedByMechanic
BlockedByParity
BlockedByRuntime
NeedsReview
```

`CompleteForPinnedDcgoSnapshot`는 lock에 기록된 SHA에만 유효하다.
upstream이 업데이트되면 자동으로 유지되지 않는다.

## RL

gate가 pass해도 RL 구현은 사용자 승인 전 시작하지 않는다.
