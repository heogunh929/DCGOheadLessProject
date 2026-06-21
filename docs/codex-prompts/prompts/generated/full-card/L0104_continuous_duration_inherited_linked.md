# L0104_continuous_duration_inherited_linked - continuous/duration/inherited/linked common layer blocker 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `L0104_continuous_duration_inherited_linked`
- Kind: `mechanic-layer`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: none
- Card identity count: 25
- Source effect count: 0
- Mechanic blocker count: 1

## Mechanic / Layer Items

| Section | Name | Status | Affected cards | Source files |
| --- | --- | --- | ---: | ---: |
| `features` | `inherited` | `PartiallyImplemented` | 4491 | 2417 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `AD1-001#7817@base` | `AD1-001` | 7817 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001.asset` |
| `AD1-001#7818@P1` | `AD1-001` | 7818 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001_P1.asset` |
| `AD1-002#7819@base` | `AD1-002` | 7819 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002.asset` |
| `AD1-002#7820@P1` | `AD1-002` | 7820 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002_P1.asset` |
| `AD1-003#7821@base` | `AD1-003` | 7821 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_003.asset` |
| `AD1-004#7822@base` | `AD1-004` | 7822 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004.asset` |
| `AD1-004#7823@P1` | `AD1-004` | 7823 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004_P1.asset` |
| `AD1-005#7824@base` | `AD1-005` | 7824 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005.asset` |
| `AD1-005#7825@P1` | `AD1-005` | 7825 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005_P1.asset` |
| `AD1-007#7828@base` | `AD1-007` | 7828 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007.asset` |
| `AD1-007#7829@P1` | `AD1-007` | 7829 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007_P1.asset` |
| `AD1-008#7830@base` | `AD1-008` | 7830 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008.asset` |
| `AD1-008#7831@P1` | `AD1-008` | 7831 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008_P1.asset` |
| `AD1-009#7832@base` | `AD1-009` | 7832 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009.asset` |
| `AD1-009#7833@P1` | `AD1-009` | 7833 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009_P1.asset` |
| `AD1-010#7834@base` | `AD1-010` | 7834 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_010.asset` |
| `AD1-011#7835@base` | `AD1-011` | 7835 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_011.asset` |
| `AD1-012#7836@base` | `AD1-012` | 7836 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012.asset` |
| `AD1-012#7837@P1` | `AD1-012` | 7837 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012_P1.asset` |
| `AD1-013#7838@base` | `AD1-013` | 7838 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_013.asset` |
| `AD1-014#7839@base` | `AD1-014` | 7839 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014.asset` |
| `AD1-014#7840@P1` | `AD1-014` | 7840 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014_P1.asset` |
| `AD1-015#7841@base` | `AD1-015` | 7841 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_015.asset` |
| `AD1-015#7842@P1` | `AD1-015` | 7842 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_015_P1.asset` |
| `AD1-016#7843@base` | `AD1-016` | 7843 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016.asset` |

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
