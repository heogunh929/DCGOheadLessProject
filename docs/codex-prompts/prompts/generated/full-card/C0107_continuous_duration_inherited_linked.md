# C0107_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 3

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0107_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT13_004` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_004.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT13_047` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_047.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT13_052` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_052.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT13_063` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_063.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT13_067` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_067.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT14_002` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_002.cs` | `None` | `inherited, static_or_continuous` | `-` | 3 |
| `BT14_004` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_004.cs` | `OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `BT14_028` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_028.cs` | `None, OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous` | `-` | 2 |
| `BT14_057` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_057.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `-` | 1 |
| `BT14_059` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_059.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT13-004#2646@base` | `BT13-004` | 2646 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/DigiEgg/BT13_004.asset` |
| `BT13-004#4556@P0` | `BT13-004` | 4556 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/DigiEgg/BT13_004_P0.asset` |
| `BT13-047#2699@base` | `BT13-047` | 2699 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_047.asset` |
| `BT13-052#2704@base` | `BT13-052` | 2704 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_052.asset` |
| `BT13-063#2720@base` | `BT13-063` | 2720 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_063.asset` |
| `BT13-066#2723@base` | `BT13-066` | 2723 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_066.asset` |
| `BT13-067#2724@base` | `BT13-067` | 2724 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_067.asset` |
| `BT14-002#2914@base` | `BT14-002` | 2914 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/DigiEgg/BT14_002.asset` |
| `BT14-002#2915@P1` | `BT14-002` | 2915 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/DigiEgg/BT14_002_P1.asset` |
| `BT14-002#4633@P0` | `BT14-002` | 4633 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/DigiEgg/BT14_002_P0.asset` |
| `BT14-004#2918@base` | `BT14-004` | 2918 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/DigiEgg/BT14_004.asset` |
| `BT14-004#2919@P1` | `BT14-004` | 2919 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/DigiEgg/BT14_004_P1.asset` |
| `BT14-004#4635@P0` | `BT14-004` | 4635 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/DigiEgg/BT14_004_P0.asset` |
| `BT14-028#2949@base` | `BT14-028` | 2949 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_028.asset` |
| `BT14-028#4647@P0` | `BT14-028` | 4647 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_028_P0.asset` |
| `BT14-057#2983@base` | `BT14-057` | 2983 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_057.asset` |
| `BT14-059#2985@base` | `BT14-059` | 2985 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_059.asset` |

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
