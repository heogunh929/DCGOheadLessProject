# Full Card Porting Batches - Queue 66

66번은 62~65 산출물에서 전체 카드풀 porting subqueue를 생성한 작업이다. 이 queue는 전체 카드 구현을 직접 수행하지 않고, mechanic-layer, card-porting, source-review batch를 dependency-aware 순서로 나누는 역할만 한다.

## Summary

- Batch count: 423
- Mechanic-layer batches: 12
- Card-porting batches: 397
- Source-review batches: 14
- Source scaffolds assigned: 3918 / 3918
- NeedsSourceReview cards assigned: 40 / 40

## Category Counts

- `attack-security-timing`: 56
- `continuous-duration-inherited-linked`: 23
- `draw-search-reveal-hidden`: 1
- `existing-layer`: 4
- `high-risk-source-review`: 14
- `replacement-counter-cut-in`: 50
- `simultaneous-trigger-priority`: 1
- `special-digivolution-play`: 176
- `zone-security-recovery`: 98

## Generated Control Files

- `docs/codex-prompts/GOAL_FULL_CARD_PORTING_BATCHES.md`
- `docs/codex-prompts/ACTIVE/RUN_NEXT_FULL_CARD_PORTING_BATCHES.md`
- `docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md`
- `docs/codex-prompts/state/PROGRESS_FULL_CARD_PORTING_BATCHES.md`
- `docs/codex-prompts/prompts/generated/full-card/*.md`

Machine-readable manifest: `docs/generated/full-card-porting-batches-66.json`

## Queue 66A Dependency-Aware Scheduler Policy

`scripts/select_next_full_card_porting_batch.py`는 generated subqueue에서 다음 실행 가능 batch를 계산한다. 이 helper는 queue를 수정하지 않고 다음 실행 후보와 skipped 이유만 보고한다.

- `dependencyBatchIds`가 모두 `done`인 `todo` batch만 실행 가능하다.
- dependency가 `blocked` 또는 `needs-review`이면 dependent card-porting batch를 실행하지 않는다.
- 공통 layer 미구현 상태는 `needs-review`가 아니라 `blocked`다.
- `needs-review`는 실제 사용자 판단 또는 source body/source 의미 불명확성에만 사용한다.
- blocker 문서화만으로 card-porting batch를 `done` 처리하지 않는다.
- card-porting 완료 조건은 실제 effect body 구현, registry/status 갱신, 테스트, baseline blocker 감소다.

## Current Dependency State

`L0006_zone_security_recovery`는 2026-06-22에 `done`으로 처리되었다. 다음 full-card-porting 실행 시에는 selector 결과를 따른다. selector가 `C0026_zone_security_recovery`를 `executable`로 반환하더라도, C0026은 별도 사용자 요청에서 한 queue 항목으로만 진행한다.

Update 2026-06-22: `L0006_zone_security_recovery`는 `done` 처리했다. 이후 `C0026_zone_security_recovery`를 별도 queue 항목으로 진행했고, `BT18_080` runnable body와 `BT18_085` continuous partial body를 추가했다. 나머지 source bodies는 공통 layer blocker가 필요하므로 C0026은 `blocked`로 유지한다.

다음 selector 실행 때 `blocked` 또는 `needs-review` dependency가 있는 dependent card batch는 건너뛴다. card-porting batch는 실제 effect body, registry/status 갱신, 테스트, baseline blocker 감소 없이는 `done` 처리하지 않는다.
