# C0119_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 15

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0119_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT8_077` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_077.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `-` | 2 |
| `BT9_004` | `DCGO/Assets/Scripts/CardEffect/BT9/Green/BT9_004.cs` | `None` | `inherited, static_or_continuous` | `-` | 6 |
| `BT9_005` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_005.cs` | `None` | `inherited, static_or_continuous` | `-` | 4 |
| `BT9_059` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_059.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `EX11_004` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_004.cs` | `OnFaceUpSecurityIncreased` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX1_013` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_013.cs` | `OnUnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX1_036` | `DCGO/Assets/Scripts/CardEffect/EX1/Green/EX1_036.cs` | `OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `EX1_039` | `DCGO/Assets/Scripts/CardEffect/EX1/Green/EX1_039.cs` | `OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `EX1_043` | `DCGO/Assets/Scripts/CardEffect/EX1/Green/EX1_043.cs` | `None, OnEndBattle` | `inherited, max_count_per_turn, optional, static_or_continuous` | `-` | 2 |
| `EX1_057` | `DCGO/Assets/Scripts/CardEffect/EX1/Purple/EX1_057.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT8-077#1659@base` | `BT8-077` | 1659 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_077.asset` |
| `BT8-077#8891@P0` | `BT8-077` | 8891 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_077_P0.asset` |
| `BT9-004#1781@base` | `BT9-004` | 1781 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/DigiEgg/BT9_004.asset` |
| `BT9-004#1782@P1` | `BT9-004` | 1782 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_004_P1.asset` |
| `BT9-004#1783@P2` | `BT9-004` | 1783 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_004_P2.asset` |
| `BT9-004#8942@P1` | `BT9-004` | 8942 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/DigiEgg/BT9_004_P1.asset` |
| `BT9-004#8943@P0` | `BT9-004` | 8943 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/DigiEgg/BT9_004_P0.asset` |
| `BT9-004#8944@P2` | `BT9-004` | 8944 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/DigiEgg/BT9_004_P2.asset` |
| `BT9-005#1784@base` | `BT9-005` | 1784 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/DigiEgg/BT9_005.asset` |
| `BT9-005#1785@P1` | `BT9-005` | 1785 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_005_P1.asset` |
| `BT9-005#8945@P1` | `BT9-005` | 8945 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/DigiEgg/BT9_005_P1.asset` |
| `BT9-005#8946@P0` | `BT9-005` | 8946 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/DigiEgg/BT9_005_P0.asset` |
| `BT9-059#1849@base` | `BT9-059` | 1849 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_059.asset` |
| `EX1-013#1284@base` | `EX1-013` | 1284 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_013.asset` |
| `EX1-013#1285@P1` | `EX1-013` | 1285 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_013_P1.asset` |
| `EX1-036#1323@base` | `EX1-036` | 1323 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_036.asset` |
| `EX1-039#1327@base` | `EX1-039` | 1327 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_039.asset` |
| `EX1-039#1328@P1` | `EX1-039` | 1328 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_039_P1.asset` |
| `EX1-039#1329@P2` | `EX1-039` | 1329 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_039_P2.asset` |
| `EX1-043#1334@base` | `EX1-043` | 1334 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_043.asset` |
| `EX1-043#1335@P1` | `EX1-043` | 1335 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_043_P1.asset` |
| `EX1-057#1355@base` | `EX1-057` | 1355 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_057.asset` |
| `EX11-004#7663@base` | `EX11-004` | 7663 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/DigiEgg/EX11_004.asset` |
| `EX11-004#7664@P1` | `EX11-004` | 7664 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/DigiEgg/EX11_004_P1.asset` |

## Required Work

- Source Effects 표의 원본 파일을 먼저 읽고, 대응 카드별 RL effect 파일 또는 wrapper/mapping을 만든다.
- 같은 source effect를 여러 variant가 참조해도 CardId만으로 평탄화하지 않는다.
- 필요한 공통 helper는 source 의미를 숨기지 않는 범위에서만 추가한다.
- 구현 후 65 baseline에서 이 batch의 `Unsupported` blocker가 줄어드는지 확인한다.

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
