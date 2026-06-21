# C0207_replacement_counter_cut_in - replacement/counter/cut-in card porting 22

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0207_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT24_050` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_050.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT24_052` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_052.cs` | `None, OnEnterFieldAnyone, OnMove, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT24_055` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_055.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT24_060` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_060.cs` | `None, OnAddDigivolutionCards, OnAllyAttack, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget` | 1 |
| `BT24_062` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_062.cs` | `None, OnEndAttack, OnEndTurn, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectAttackTarget, SelectJogress, SelectAssembly` | 1 |
| `BT24_063` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_063.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `BT24_101` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_101.cs` | `None, OnEnterFieldAnyone, OnLoseSecurity, WhenRemoveField` | `max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT25_004` | `DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_004.cs` | `WhenWouldLink` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous` | `SelectCard` | 1 |
| `BT25_027` | `DCGO/Assets/Scripts/CardEffect/BT25/Blue/BT25_027.cs` | `None, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT25_029` | `DCGO/Assets/Scripts/CardEffect/BT25/Blue/BT25_029.cs` | `None, OnAddHand, OnDigivolutionCardDiscarded, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT24-050#7579@base` | `BT24-050` | 7579 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_050.asset` |
| `BT24-052#7582@base` | `BT24-052` | 7582 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_052.asset` |
| `BT24-055#7586@base` | `BT24-055` | 7586 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_055.asset` |
| `BT24-060#7591@base` | `BT24-060` | 7591 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_060.asset` |
| `BT24-062#7593@base` | `BT24-062` | 7593 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_062.asset` |
| `BT24-063#7594@base` | `BT24-063` | 7594 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_063.asset` |
| `BT24-101#7645@base` | `BT24-101` | 7645 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_101.asset` |
| `BT24-101#7646@P1` | `BT24-101` | 7646 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_101_P1.asset` |
| `BT24-101#7647@P2` | `BT24-101` | 7647 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_101_P2.asset` |
| `BT25-004#7966@base` | `BT25-004` | 7966 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/DigiEgg/BT25_004.asset` |
| `BT25-027#7991@base` | `BT25-027` | 7991 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Digimon/BT25_027.asset` |
| `BT25-029#7994@base` | `BT25-029` | 7994 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Digimon/BT25_029.asset` |
| `BT25-029#7995@P1` | `BT25-029` | 7995 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Digimon/BT25_029_P1.asset` |

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
