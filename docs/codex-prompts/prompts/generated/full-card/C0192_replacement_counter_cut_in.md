# C0192_replacement_counter_cut_in - replacement/counter/cut-in card porting 7

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0192_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 31
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT15_049` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_049.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectBoolean, SelectAttackTarget` | 3 |
| `BT15_072` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_072.cs` | `None, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT15_076` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_076.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |
| `BT15_101` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_101.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 5 |
| `BT16_009` | `DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_009.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT16_013` | `DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_013.cs` | `None, OnCounterTiming, OnEnterFieldAnyone, OnLoseSecurity` | `counter, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 5 |
| `BT16_021` | `DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_021.cs` | `None, OnTappedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT16_022` | `DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_022.cs` | `None, OnAllyAttack, WhenPermanentWouldBeDeleted` | `max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT16_026` | `DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_026.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 5 |
| `BT16_027` | `DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_027.cs` | `None, OnCounterTiming, OnEndAttack, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT15-049#3178@base` | `BT15-049` | 3178 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_049.asset` |
| `BT15-049#3179@P1` | `BT15-049` | 3179 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_049_P1.asset` |
| `BT15-049#3180@P2` | `BT15-049` | 3180 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_049_P2.asset` |
| `BT15-072#3205@base` | `BT15-072` | 3205 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_072.asset` |
| `BT15-076#3209@base` | `BT15-076` | 3209 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_076.asset` |
| `BT15-076#3210@P1` | `BT15-076` | 3210 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_076_P1.asset` |
| `BT15-101#3242@base` | `BT15-101` | 3242 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_101.asset` |
| `BT15-101#3243@P1` | `BT15-101` | 3243 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_101_P1.asset` |
| `BT15-101#8187@P2` | `BT15-101` | 8187 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_101_P2.asset` |
| `BT15-101#8188@P3` | `BT15-101` | 8188 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_101_P3.asset` |
| `BT15-101#8189@P4` | `BT15-101` | 8189 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_101_P4.asset` |
| `BT16-009#3314@base` | `BT16-009` | 3314 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_009.asset` |
| `BT16-013#3319@base` | `BT16-013` | 3319 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_013.asset` |
| `BT16-013#3320@P1` | `BT16-013` | 3320 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_013_P1.asset` |
| `BT16-013#3321@P2` | `BT16-013` | 3321 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_013_P2.asset` |
| `BT16-013#8191@P3` | `BT16-013` | 8191 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_013_P3.asset` |
| `BT16-013#8192@P4` | `BT16-013` | 8192 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_013_P4.asset` |
| `BT16-021#3329@base` | `BT16-021` | 3329 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_021.asset` |
| `BT16-021#8195@P1` | `BT16-021` | 8195 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_021_P1.asset` |
| `BT16-021#8196@P2` | `BT16-021` | 8196 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_021_P2.asset` |
| `BT16-022#3330@base` | `BT16-022` | 3330 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_022.asset` |
| `BT16-026#3335@base` | `BT16-026` | 3335 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_026.asset` |
| `BT16-026#3336@P1` | `BT16-026` | 3336 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_026_P1.asset` |
| `BT16-026#3337@P2` | `BT16-026` | 3337 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_026_P2.asset` |
| `BT16-026#8199@P3` | `BT16-026` | 8199 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_026_P3.asset` |
| `BT16-026#8200@P4` | `BT16-026` | 8200 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_026_P4.asset` |
| `BT16-027#3338@base` | `BT16-027` | 3338 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_027.asset` |
| `BT16-027#3339@P1` | `BT16-027` | 3339 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_027_P1.asset` |
| `BT16-027#3340@P2` | `BT16-027` | 3340 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_027_P2.asset` |
| `BT16-027#4784@P0` | `BT16-027` | 4784 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_027_P0.asset` |
| `BT16-027#8201@P3` | `BT16-027` | 8201 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_027_P3.asset` |

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
