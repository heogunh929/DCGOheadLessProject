# L0005_draw_search_reveal_hidden - draw/search/reveal/hidden information common layer blocker 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `L0005_draw_search_reveal_hidden`
- Kind: `mechanic-layer`
- Category: `draw-search-reveal-hidden` / draw/search/reveal/hidden information
- Dependencies: none
- Card identity count: 0
- Source effect count: 0
- Mechanic blocker count: 1

## Mechanic / Layer Items

| Section | Name | Status | Affected cards | Source files |
| --- | --- | --- | ---: | ---: |
| `timings` | `OnDraw` | `NeedsSourceReview` | 0 | 1 |

## Required Work

- 나열된 mechanic/layer 항목의 DCGO 원본 source 위치와 RL.Engine 공통 layer 대응을 먼저 문서화한다.
- 공통 service/primitive/selection/trigger boundary가 필요한 경우 카드 구현 전에 layer를 먼저 추가한다.
- 이 batch에서 카드별 effect body를 대량 구현하지 말고, layer와 대표 fixture만 검증한다.

## Tests / Validation

- 대상 카드별 unit/golden/replay 테스트를 추가하거나 갱신한다.
- `FullCardPoolValidator` deck subset 또는 batch-specific validation을 추가한다.
- 전체 regression을 실행한다.
- `DCGO/Assets/Scripts` 변경 없음과 tracked `bin/obj` 없음 확인을 보고한다.

## Blocker Conditions

- source body missing/ambiguous
- 원본 timing/selection/ordering 의미 미확인
- 공통 layer가 없는 mechanic을 카드별 workaround로 구현해야 하는 상황
- core service에 CardId 분기를 넣어야만 통과하는 설계
