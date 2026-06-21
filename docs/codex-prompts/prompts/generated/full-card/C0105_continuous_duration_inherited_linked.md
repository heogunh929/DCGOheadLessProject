# C0105_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0105_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT10_001` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_001.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT10_004` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_004.cs` | `OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT10_005` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_005.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT10_007` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_007.cs` | `None` | `static_or_continuous` | `-` | 1 |
| `BT10_045` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_045.cs` | `OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT10_051` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_051.cs` | `OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT11_006` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_006.cs` | `OnDiscardHand` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT11_049` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_049.cs` | `OnStartTurn` | `max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT11_059` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_059.cs` | `None, OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous` | `SelectCard` | 2 |
| `BT11_067` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_067.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT10-001#2031@base` | `BT10-001` | 2031 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/DigiEgg/BT10_001.asset` |
| `BT10-001#4280@P0` | `BT10-001` | 4280 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/DigiEgg/BT10_001_P0.asset` |
| `BT10-004#2036@base` | `BT10-004` | 2036 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/DigiEgg/BT10_004.asset` |
| `BT10-004#4285@P0` | `BT10-004` | 4285 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/DigiEgg/BT10_004_P0.asset` |
| `BT10-005#2037@base` | `BT10-005` | 2037 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/DigiEgg/BT10_005.asset` |
| `BT10-005#4286@P0` | `BT10-005` | 4286 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/DigiEgg/BT10_005_P0.asset` |
| `BT10-007#2039@base` | `BT10-007` | 2039 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_007.asset` |
| `BT10-045#2086@base` | `BT10-045` | 2086 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_045.asset` |
| `BT10-051#2092@base` | `BT10-051` | 2092 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_051.asset` |
| `BT10-051#4323@P1` | `BT10-051` | 4323 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_051_P1.asset` |
| `BT11-006#2273@base` | `BT11-006` | 2273 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/DigiEgg/BT11_006.asset` |
| `BT11-049#2322@base` | `BT11-049` | 2322 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_049.asset` |
| `BT11-059#2333@base` | `BT11-059` | 2333 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_059.asset` |
| `BT11-059#4413@P0` | `BT11-059` | 4413 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_059_P0.asset` |
| `BT11-067#2341@base` | `BT11-067` | 2341 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_067.asset` |
| `BT11-067#4416@P0` | `BT11-067` | 4416 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_067_P0.asset` |
| `BT12-036#2446@base` | `BT12-036` | 2446 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_036.asset` |

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
