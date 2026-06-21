# C0218_replacement_counter_cut_in - replacement/counter/cut-in card porting 33

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0218_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX5_015` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_015.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `EX5_018` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_018.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `EX5_024` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_024.cs` | `None, OnCounterTiming, OnDestroyedAnyone, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX5_033` | `DCGO/Assets/Scripts/CardEffect/EX5/Yellow/EX5_033.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |
| `EX5_041` | `DCGO/Assets/Scripts/CardEffect/EX5/Green/EX5_041.cs` | `None, OnCounterTiming, OnDestroyedAnyone, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX5_046` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_046.cs` | `None, OnDestroyedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `EX5_050` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_050.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `EX5_053` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_053.cs` | `None, OnCounterTiming, OnDestroyedAnyone, OnSecurityCheck` | `counter, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `EX5_058` | `DCGO/Assets/Scripts/CardEffect/EX5/Purple/EX5_058.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `-` | 1 |
| `EX5_058_token` | `DCGO/Assets/Scripts/CardEffect/EX5/Purple/EX5_058_token.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectHand` | 0 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX5-015#3054@base` | `EX5-015` | 3054 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_015.asset` |
| `EX5-018#3057@base` | `EX5-018` | 3057 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_018.asset` |
| `EX5-024#3063@base` | `EX5-024` | 3063 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_024.asset` |
| `EX5-024#4215@P1` | `EX5-024` | 4215 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_024_P1.asset` |
| `EX5-033#3072@base` | `EX5-033` | 3072 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_033.asset` |
| `EX5-033#4226@P1` | `EX5-033` | 4226 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_033_P1.asset` |
| `EX5-041#3080@base` | `EX5-041` | 3080 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_041.asset` |
| `EX5-041#4228@P1` | `EX5-041` | 4228 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_041_P1.asset` |
| `EX5-046#3085@base` | `EX5-046` | 3085 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_046.asset` |
| `EX5-050#3089@base` | `EX5-050` | 3089 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_050.asset` |
| `EX5-053#3092@base` | `EX5-053` | 3092 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_053.asset` |
| `EX5-053#4231@P1` | `EX5-053` | 4231 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_053_P1.asset` |
| `EX5-058#3097@base` | `EX5-058` | 3097 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/Digimon/EX5_058.asset` |

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
