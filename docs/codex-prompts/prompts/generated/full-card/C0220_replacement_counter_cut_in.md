# C0220_replacement_counter_cut_in - replacement/counter/cut-in card porting 35

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0220_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX6_042` | `DCGO/Assets/Scripts/CardEffect/EX6/Black/EX6_042.cs` | `None, OnAddDigivolutionCards, OnDeclaration, OnStartMainPhase, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget` | 1 |
| `EX6_052` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_052.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |
| `EX6_053` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_053.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `EX6_057` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_057.cs` | `None, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, modifier_duration, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |
| `EX6_059` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_059.cs` | `OnDiscardHand, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |
| `EX7_012` | `DCGO/Assets/Scripts/CardEffect/EX7/Red/EX7_012.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX7_026` | `DCGO/Assets/Scripts/CardEffect/EX7/Yellow/EX7_026.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX7_027` | `DCGO/Assets/Scripts/CardEffect/EX7/Yellow/EX7_027.cs` | `OnEndTurn, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 4 |
| `EX7_029` | `DCGO/Assets/Scripts/CardEffect/EX7/Yellow/EX7_029.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX7_044` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_044.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX6-042#3494@base` | `EX6-042` | 3494 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/Digimon/EX6_042.asset` |
| `EX6-052#3505@base` | `EX6-052` | 3505 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_052.asset` |
| `EX6-052#9161@P1` | `EX6-052` | 9161 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_052_P1.asset` |
| `EX6-053#3506@base` | `EX6-053` | 3506 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_053.asset` |
| `EX6-057#3512@base` | `EX6-057` | 3512 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_057.asset` |
| `EX6-057#3513@P1` | `EX6-057` | 3513 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_057_P1.asset` |
| `EX6-057#9163@P2` | `EX6-057` | 9163 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_057_P2.asset` |
| `EX6-059#3516@base` | `EX6-059` | 3516 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_059.asset` |
| `EX6-059#3517@P1` | `EX6-059` | 3517 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_059_P1.asset` |
| `EX7-012#3697@base` | `EX7-012` | 3697 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_012.asset` |
| `EX7-012#3698@P1` | `EX7-012` | 3698 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_012_P1.asset` |
| `EX7-026#3724@base` | `EX7-026` | 3724 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_026.asset` |
| `EX7-026#3725@P1` | `EX7-026` | 3725 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_026_P1.asset` |
| `EX7-027#3726@base` | `EX7-027` | 3726 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_027.asset` |
| `EX7-027#9173@P1` | `EX7-027` | 9173 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_027_P1.asset` |
| `EX7-027#9174@P2` | `EX7-027` | 9174 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_027_P2.asset` |
| `EX7-027#9175@P3` | `EX7-027` | 9175 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_027_P3.asset` |
| `EX7-029#3729@base` | `EX7-029` | 3729 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_029.asset` |
| `EX7-029#3730@P1` | `EX7-029` | 3730 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_029_P1.asset` |
| `EX7-044#3759@base` | `EX7-044` | 3759 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_044.asset` |
| `EX7-044#3760@P1` | `EX7-044` | 3760 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_044_P1.asset` |

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
