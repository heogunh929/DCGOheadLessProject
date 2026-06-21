# C0215_replacement_counter_cut_in - replacement/counter/cut-in card porting 30

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0215_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX10_052` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_052.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX10_055` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_055.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectDigiXros` | 1 |
| `EX10_074` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_074.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX11_012` | `DCGO/Assets/Scripts/CardEffect/EX11/Red/EX11_012.cs` | `None, OnEndAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX11_017` | `DCGO/Assets/Scripts/CardEffect/EX11/Blue/EX11_017.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX11_018` | `DCGO/Assets/Scripts/CardEffect/EX11/Blue/EX11_018.cs` | `OnAddDigivolutionCards, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 3 |
| `EX11_019` | `DCGO/Assets/Scripts/CardEffect/EX11/Yellow/EX11_019.cs` | `OnDestroyedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `-` | 2 |
| `EX11_022` | `DCGO/Assets/Scripts/CardEffect/EX11/Yellow/EX11_022.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger` | 2 |
| `EX11_023` | `DCGO/Assets/Scripts/CardEffect/EX11/Yellow/EX11_023.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX11_027` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_027.cs` | `None, OnDeclaration, OnEnterFieldAnyone, WhenRemoveField` | `linked, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX10-052#7228@base` | `EX10-052` | 7228 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_052.asset` |
| `EX10-055#7232@base` | `EX10-055` | 7232 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_055.asset` |
| `EX10-074#7267@base` | `EX10-074` | 7267 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_074.asset` |
| `EX10-074#7331@P1` | `EX10-074` | 7331 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_074_P1.asset` |
| `EX11-012#7679@base` | `EX11-012` | 7679 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_012.asset` |
| `EX11-012#7680@P1` | `EX11-012` | 7680 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_012_P1.asset` |
| `EX11-017#7689@base` | `EX11-017` | 7689 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_017.asset` |
| `EX11-017#7690@P1` | `EX11-017` | 7690 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_017_P1.asset` |
| `EX11-018#7691@base` | `EX11-018` | 7691 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_018.asset` |
| `EX11-018#7692@P1` | `EX11-018` | 7692 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_018_P1.asset` |
| `EX11-018#7693@P2` | `EX11-018` | 7693 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_018_P2.asset` |
| `EX11-019#7694@base` | `EX11-019` | 7694 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_019.asset` |
| `EX11-019#7695@P1` | `EX11-019` | 7695 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_019_P1.asset` |
| `EX11-022#7700@base` | `EX11-022` | 7700 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_022.asset` |
| `EX11-022#7701@P1` | `EX11-022` | 7701 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_022_P1.asset` |
| `EX11-023#7702@base` | `EX11-023` | 7702 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_023.asset` |
| `EX11-023#7703@P1` | `EX11-023` | 7703 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_023_P1.asset` |
| `EX11-027#7711@base` | `EX11-027` | 7711 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_027.asset` |
| `EX11-027#7712@P1` | `EX11-027` | 7712 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_027_P1.asset` |

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
