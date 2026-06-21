# C0189_replacement_counter_cut_in - replacement/counter/cut-in card porting 4

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0189_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT12_072` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_072.cs` | `None, OnStartMainPhase, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT12_084` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_084.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, modifier_duration, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress, SelectDigiXros` | 2 |
| `BT13_023` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_023.cs` | `OnAllyAttack, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT13_031` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_031.cs` | `OnAddHand, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT13_041` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_041.cs` | `OnDestroyedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 2 |
| `BT13_043` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_043.cs` | `WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in` | `-` | 1 |
| `BT13_053` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_053.cs` | `None, OnEnterFieldAnyone` | `background, inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT13_058` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_058.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT13_059` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_059.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectBoolean, SelectJogress` | 3 |
| `BT13_065` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_065.cs` | `OnDestroyedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT12-072#2488@base` | `BT12-072` | 2488 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_072.asset` |
| `BT12-072#4523@P1` | `BT12-072` | 4523 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_072_P1.asset` |
| `BT12-084#2503@base` | `BT12-084` | 2503 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_084.asset` |
| `BT12-084#4531@P0` | `BT12-084` | 4531 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_084_P0.asset` |
| `BT13-023#2670@base` | `BT13-023` | 2670 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_023.asset` |
| `BT13-031#2679@base` | `BT13-031` | 2679 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_031.asset` |
| `BT13-031#4576@P0` | `BT13-031` | 4576 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_031_P0.asset` |
| `BT13-041#2692@base` | `BT13-041` | 2692 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_041.asset` |
| `BT13-041#4585@P0` | `BT13-041` | 4585 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_041_P0.asset` |
| `BT13-043#2694@base` | `BT13-043` | 2694 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_043.asset` |
| `BT13-053#2705@base` | `BT13-053` | 2705 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_053.asset` |
| `BT13-053#4591@P0` | `BT13-053` | 4591 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_053_P0.asset` |
| `BT13-058#2711@base` | `BT13-058` | 2711 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_058.asset` |
| `BT13-058#2712@P1` | `BT13-058` | 2712 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_058_P1.asset` |
| `BT13-059#2713@base` | `BT13-059` | 2713 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_059.asset` |
| `BT13-059#2714@P1` | `BT13-059` | 2714 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_059_P1.asset` |
| `BT13-059#4594@P0` | `BT13-059` | 4594 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_059_P0.asset` |
| `BT13-065#2722@base` | `BT13-065` | 2722 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_065.asset` |

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
