# C0217_replacement_counter_cut_in - replacement/counter/cut-in card porting 32

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0217_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX3_013` | `DCGO/Assets/Scripts/CardEffect/EX3/Red/EX3_013.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted, WhenReturntoHandAnyone, WhenReturntoLibraryAnyone` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectOrder, SelectJogress` | 2 |
| `EX3_018` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_018.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, static_or_continuous` | `SelectJogress` | 1 |
| `EX3_020` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_020.cs` | `None, OnEndTurn, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `EX3_043` | `DCGO/Assets/Scripts/CardEffect/EX3/Green/EX3_043.cs` | `BeforePayCost, OnEnterFieldAnyone, WhenDigisorption` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_priority, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `EX3_046` | `DCGO/Assets/Scripts/CardEffect/EX3/Black/EX3_046.cs` | `WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in` | `-` | 1 |
| `EX4_013` | `DCGO/Assets/Scripts/CardEffect/EX4/Red/EX4_013.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `EX4_020` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_020.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 1 |
| `EX4_021` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_021.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted, WhenReturntoHandAnyone, WhenReturntoLibraryAnyone` | `max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 2 |
| `EX4_027` | `DCGO/Assets/Scripts/CardEffect/EX4/Yellow/EX4_027.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX5_013` | `DCGO/Assets/Scripts/CardEffect/EX5/Red/EX5_013.cs` | `None, OnAllyAttack, OnCounterTiming, OnDestroyedAnyone, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX3-013#2185@base` | `EX3-013` | 2185 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_013.asset` |
| `EX3-013#2186@P1` | `EX3-013` | 2186 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_013_P1.asset` |
| `EX3-018#2192@base` | `EX3-018` | 2192 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_018.asset` |
| `EX3-020#2194@base` | `EX3-020` | 2194 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_020.asset` |
| `EX3-020#2195@P1` | `EX3-020` | 2195 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_020_P1.asset` |
| `EX3-043#2225@base` | `EX3-043` | 2225 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_043.asset` |
| `EX3-046#2230@base` | `EX3-046` | 2230 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Digimon/EX3_046.asset` |
| `EX4-013#2558@base` | `EX4-013` | 2558 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_013.asset` |
| `EX4-013#2559@P1` | `EX4-013` | 2559 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_013_P1.asset` |
| `EX4-020#2566@base` | `EX4-020` | 2566 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_020.asset` |
| `EX4-021#2567@base` | `EX4-021` | 2567 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_021.asset` |
| `EX4-021#2568@P1` | `EX4-021` | 2568 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_021_P1.asset` |
| `EX4-027#2575@base` | `EX4-027` | 2575 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_027.asset` |
| `EX4-027#2576@P1` | `EX4-027` | 2576 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_027_P1.asset` |
| `EX5-013#3052@base` | `EX5-013` | 3052 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_013.asset` |
| `EX5-013#4209@P1` | `EX5-013` | 4209 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_013_P1.asset` |

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
