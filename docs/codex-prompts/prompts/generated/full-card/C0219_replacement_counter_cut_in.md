# C0219_replacement_counter_cut_in - replacement/counter/cut-in card porting 34

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0219_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 26
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX5_073` | `DCGO/Assets/Scripts/CardEffect/EX5/Red/EX5_073.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `EX6_011` | `DCGO/Assets/Scripts/CardEffect/EX6/Red/EX6_011.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `EX6_019` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_019.cs` | `OnAllyAttack, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous` | `-` | 1 |
| `EX6_022` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_022.cs` | `OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX6_027` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_027.cs` | `None, OnCounterTiming, OnEnterFieldAnyone, OnLoseSecurity` | `counter, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity, SelectAttackTarget, SelectJogress` | 3 |
| `EX6_028` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_028.cs` | `None, OnAddSecurity, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `EX6_029` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_029.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 4 |
| `EX6_030` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_030.cs` | `OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `EX6_031` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_031.cs` | `None, OnEndTurn, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted, WhenReturntoHandAnyone, WhenReturntoLibraryAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress, SelectDigiXros` | 2 |
| `EX6_035` | `DCGO/Assets/Scripts/CardEffect/EX6/Green/EX6_035.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX5-073#3112@base` | `EX5-073` | 3112 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_073.asset` |
| `EX5-073#4238@P1` | `EX5-073` | 4238 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_073_P1.asset` |
| `EX5-073#9151@P2` | `EX5-073` | 9151 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_073_P2.asset` |
| `EX6-011#3449@base` | `EX6-011` | 3449 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Red/Digimon/EX6_011.asset` |
| `EX6-011#3450@P1` | `EX6-011` | 3450 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Red/Digimon/EX6_011_P1.asset` |
| `EX6-011#3451@P2` | `EX6-011` | 3451 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Red/Digimon/EX6_011_P2.asset` |
| `EX6-019#3461@base` | `EX6-019` | 3461 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_019.asset` |
| `EX6-022#3464@base` | `EX6-022` | 3464 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_022.asset` |
| `EX6-027#3469@base` | `EX6-027` | 3469 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_027.asset` |
| `EX6-027#3470@P1` | `EX6-027` | 3470 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_027_P1.asset` |
| `EX6-027#3471@P2` | `EX6-027` | 3471 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_027_P2.asset` |
| `EX6-028#3472@base` | `EX6-028` | 3472 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_028.asset` |
| `EX6-028#3473@P1` | `EX6-028` | 3473 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_028_P1.asset` |
| `EX6-028#3474@P2` | `EX6-028` | 3474 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_028_P2.asset` |
| `EX6-029#3475@base` | `EX6-029` | 3475 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_029.asset` |
| `EX6-029#3476@P1` | `EX6-029` | 3476 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_029_P1.asset` |
| `EX6-029#3477@P2` | `EX6-029` | 3477 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_029_P2.asset` |
| `EX6-029#9157@P3` | `EX6-029` | 9157 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_029_P3.asset` |
| `EX6-030#3478@base` | `EX6-030` | 3478 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_030.asset` |
| `EX6-030#3479@P1` | `EX6-030` | 3479 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_030_P1.asset` |
| `EX6-031#3480@base` | `EX6-031` | 3480 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_031.asset` |
| `EX6-031#3481@P1` | `EX6-031` | 3481 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_031_P1.asset` |
| `EX6-035#3485@base` | `EX6-035` | 3485 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Green/Digimon/EX6_035.asset` |
| `EX6-035#3486@P1` | `EX6-035` | 3486 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Green/Digimon/EX6_035_P1.asset` |
| `EX6-035#3487@P2` | `EX6-035` | 3487 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Green/Digimon/EX6_035_P2.asset` |
| `EX6-035#9158@P3` | `EX6-035` | 9158 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Green/Digimon/EX6_035_P3.asset` |

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
