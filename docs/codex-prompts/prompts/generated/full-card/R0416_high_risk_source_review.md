# R0416_high_risk_source_review - BT4 source body review 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `R0416_high_risk_source_review`
- Kind: `source-review`
- Category: `high-risk-source-review` / source 불명확 review
- Dependencies: none
- Card identity count: 10
- Source effect count: 0
- Mechanic blocker count: 0

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT4-001#8494@P0` | `BT4-001` | 8494 | `P0` | `NeedsSourceReview` | `DCGO/Assets/CardBaseEntity/BT4/Red/DigiEgg/BT4_001_P0.asset` |
| `BT4-042#8516@P2` | `BT4-042` | 8516 | `P2` | `NeedsSourceReview` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_042_P2.asset` |
| `BT4-043#8517@P0` | `BT4-043` | 8517 | `P0` | `NeedsSourceReview` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_043_P0.asset` |
| `BT4-052#8521@P0` | `BT4-052` | 8521 | `P0` | `NeedsSourceReview` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_052_P0.asset` |
| `BT4-053#8522@P0` | `BT4-053` | 8522 | `P0` | `NeedsSourceReview` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_053_P0.asset` |
| `BT4-064#8529@P0` | `BT4-064` | 8529 | `P0` | `NeedsSourceReview` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_064_P0.asset` |
| `BT4-067#8530@P0` | `BT4-067` | 8530 | `P0` | `NeedsSourceReview` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_067_P0.asset` |
| `BT4-077#8535@P0` | `BT4-077` | 8535 | `P0` | `NeedsSourceReview` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_077_P0.asset` |
| `BT4-080#8538@P0` | `BT4-080` | 8538 | `P0` | `NeedsSourceReview` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_080_P0.asset` |
| `BT4-082#8539@P0` | `BT4-082` | 8539 | `P0` | `NeedsSourceReview` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_082_P0.asset` |

## Required Work

- 원본 source body가 실제로 존재하는지 asset path, CardEffectClassName, source lookup alias를 다시 대조한다.
- source body가 없으면 `NeedsSourceReview`를 유지하고 구현하지 않는다.
- source body를 찾으면 64 scaffold/65 baseline의 source mapping을 갱신하는 별도 queue 항목으로 넘긴다.

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
