# C0195_replacement_counter_cut_in - replacement/counter/cut-in card porting 10

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0195_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT17_038` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_038.cs` | `None, OnEnterFieldAnyone, OnUseOption, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 3 |
| `BT17_039` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_039.cs` | `OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT17_041` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_041.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT17_050` | `DCGO/Assets/Scripts/CardEffect/BT17/Green/BT17_050.cs` | `None, OnAllyAttack, OnDeclaration, OnEndAttack, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectAttackTarget, SelectJogress` | 2 |
| `BT17_054` | `DCGO/Assets/Scripts/CardEffect/BT17/Black/BT17_054.cs` | `OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT17_056` | `DCGO/Assets/Scripts/CardEffect/BT17/Black/BT17_056.cs` | `OnAddDigivolutionCards, OnAttackTargetChanged, OnCounterTiming` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard, SelectAttackTarget, SelectJogress` | 1 |
| `BT17_057` | `DCGO/Assets/Scripts/CardEffect/BT17/Black/BT17_057.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 2 |
| `BT17_064` | `DCGO/Assets/Scripts/CardEffect/BT17/Purple/BT17_064.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT17_065` | `DCGO/Assets/Scripts/CardEffect/BT17/Purple/BT17_065.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT17_067` | `DCGO/Assets/Scripts/CardEffect/BT17/Purple/BT17_067.cs` | `None, OnEndAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT17-038#3583@base` | `BT17-038` | 3583 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_038.asset` |
| `BT17-038#4851@P0` | `BT17-038` | 4851 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_038_P0.asset` |
| `BT17-038#8230@P1` | `BT17-038` | 8230 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_038_P1.asset` |
| `BT17-039#3584@base` | `BT17-039` | 3584 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_039.asset` |
| `BT17-039#4852@P0` | `BT17-039` | 4852 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_039_P0.asset` |
| `BT17-041#3587@base` | `BT17-041` | 3587 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_041.asset` |
| `BT17-041#3588@P1` | `BT17-041` | 3588 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_041_P1.asset` |
| `BT17-041#3589@P2` | `BT17-041` | 3589 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_041_P2.asset` |
| `BT17-050#3598@base` | `BT17-050` | 3598 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_050.asset` |
| `BT17-050#4857@P0` | `BT17-050` | 4857 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_050_P0.asset` |
| `BT17-054#3603@base` | `BT17-054` | 3603 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Black/Digimon/BT17_054.asset` |
| `BT17-056#3605@base` | `BT17-056` | 3605 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Black/Digimon/BT17_056.asset` |
| `BT17-057#3606@base` | `BT17-057` | 3606 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Black/Digimon/BT17_057.asset` |
| `BT17-057#4858@P0` | `BT17-057` | 4858 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Black/Digimon/BT17_057_P0.asset` |
| `BT17-064#3614@base` | `BT17-064` | 3614 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_064.asset` |
| `BT17-065#3615@base` | `BT17-065` | 3615 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_065.asset` |
| `BT17-067#3617@base` | `BT17-067` | 3617 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_067.asset` |

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
