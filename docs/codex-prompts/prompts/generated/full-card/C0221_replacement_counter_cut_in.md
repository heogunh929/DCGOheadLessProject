# C0221_replacement_counter_cut_in - replacement/counter/cut-in card porting 36

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0221_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 28
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX7_046` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_046.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 2 |
| `EX7_048` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_048.cs` | `None, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `EX7_059` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_059.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 4 |
| `EX7_061` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_061.cs` | `None, OnDestroyedAnyone, WhenRemoveField` | `max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX8_026` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_026.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `EX8_028` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_028.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 3 |
| `EX8_035` | `DCGO/Assets/Scripts/CardEffect/EX8/Yellow/EX8_035.cs` | `None, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX8_044` | `DCGO/Assets/Scripts/CardEffect/EX8/Green/EX8_044.cs` | `None, OnCounterTiming, OnEnterFieldAnyone, OnTappedAnyone` | `counter, max_count_per_turn, modifier_duration, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `EX8_051` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_051.cs` | `OnCounterTiming, OnDetermineDoSecurityCheck, OnDigivolutionCardDiscarded, WhenPermanentWouldBeDeleted` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent` | 4 |
| `EX8_055` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_055.cs` | `OnAllyAttack, OnEndTurn, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX7-046#3763@base` | `EX7-046` | 3763 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_046.asset` |
| `EX7-046#3764@P1` | `EX7-046` | 3764 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_046_P1.asset` |
| `EX7-048#3766@base` | `EX7-048` | 3766 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_048.asset` |
| `EX7-048#3767@P1` | `EX7-048` | 3767 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_048_P1.asset` |
| `EX7-048#9181@P2` | `EX7-048` | 9181 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_048_P2.asset` |
| `EX7-059#3785@base` | `EX7-059` | 3785 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_059.asset` |
| `EX7-059#3786@P1` | `EX7-059` | 3786 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_059_P1.asset` |
| `EX7-059#3787@P2` | `EX7-059` | 3787 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_059_P2.asset` |
| `EX7-059#9182@P3` | `EX7-059` | 9182 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_059_P3.asset` |
| `EX7-061#3789@base` | `EX7-061` | 3789 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_061.asset` |
| `EX7-061#3790@P1` | `EX7-061` | 3790 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_061_P1.asset` |
| `EX8-026#4095@base` | `EX8-026` | 4095 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_026.asset` |
| `EX8-026#4096@P1` | `EX8-026` | 4096 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_026_P1.asset` |
| `EX8-026#4098@P2` | `EX8-026` | 4098 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_026_P2.asset` |
| `EX8-028#4101@base` | `EX8-028` | 4101 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_028.asset` |
| `EX8-028#4102@P1` | `EX8-028` | 4102 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_028_P1.asset` |
| `EX8-028#9192@P2` | `EX8-028` | 9192 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_028_P2.asset` |
| `EX8-035#4114@base` | `EX8-035` | 4114 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_035.asset` |
| `EX8-035#4115@P1` | `EX8-035` | 4115 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_035_P1.asset` |
| `EX8-044#4131@base` | `EX8-044` | 4131 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_044.asset` |
| `EX8-044#4132@P1` | `EX8-044` | 4132 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_044_P1.asset` |
| `EX8-044#4134@P2` | `EX8-044` | 4134 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_044_P2.asset` |
| `EX8-051#4148@base` | `EX8-051` | 4148 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_051.asset` |
| `EX8-051#9197@P1` | `EX8-051` | 9197 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_051_P1.asset` |
| `EX8-051#9198@P2` | `EX8-051` | 9198 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_051_P2.asset` |
| `EX8-051#9199@P3` | `EX8-051` | 9199 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_051_P3.asset` |
| `EX8-055#4154@base` | `EX8-055` | 4154 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_055.asset` |
| `EX8-055#4155@P1` | `EX8-055` | 4155 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_055_P1.asset` |

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
