# C0193_replacement_counter_cut_in - replacement/counter/cut-in card porting 8

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0193_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT16_031` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_031.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 3 |
| `BT16_032` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_032.cs` | `None, OnAttackTargetChanged, OnCounterTiming, WhenPermanentWouldBeDeleted` | `counter, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectAttackTarget, SelectJogress` | 1 |
| `BT16_033` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_033.cs` | `None, OnSecurityCheck, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 1 |
| `BT16_035` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_035.cs` | `None, OnLoseSecurity, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `-` | 2 |
| `BT16_036` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_036.cs` | `None, OnEndTurn, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted, WhenRemoveField` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT16_046` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_046.cs` | `None, OnCounterTiming, OnEnterFieldAnyone, OnTappedAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `BT16_052_token` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_052_token.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, modifier_duration, replacement_or_cut_in, static_or_continuous` | `-` | 0 |
| `BT16_053` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_053.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT16_057` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_057.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT16_058` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_058.cs` | `None, OnCounterTiming, OnEnterFieldAnyone, OnStartMainPhase` | `counter, inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT16-031#3344@base` | `BT16-031` | 3344 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_031.asset` |
| `BT16-031#4791@P0` | `BT16-031` | 4791 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_031_P0.asset` |
| `BT16-031#8204@P1` | `BT16-031` | 8204 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_031_P1.asset` |
| `BT16-032#3345@base` | `BT16-032` | 3345 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_032.asset` |
| `BT16-033#3346@base` | `BT16-033` | 3346 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_033.asset` |
| `BT16-035#3348@base` | `BT16-035` | 3348 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_035.asset` |
| `BT16-035#4792@P0` | `BT16-035` | 4792 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_035_P0.asset` |
| `BT16-036#3349@base` | `BT16-036` | 3349 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_036.asset` |
| `BT16-036#3350@P1` | `BT16-036` | 3350 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_036_P1.asset` |
| `BT16-046#3360@base` | `BT16-046` | 3360 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_046.asset` |
| `BT16-046#3361@P1` | `BT16-046` | 3361 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_046_P1.asset` |
| `BT16-046#4799@P0` | `BT16-046` | 4799 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_046_P0.asset` |
| `BT16-046#6818@P2` | `BT16-046` | 6818 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_046_P2.asset` |
| `BT16-053#3370@base` | `BT16-053` | 3370 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_053.asset` |
| `BT16-057#3374@base` | `BT16-057` | 3374 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_057.asset` |
| `BT16-058#3375@base` | `BT16-058` | 3375 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_058.asset` |

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
