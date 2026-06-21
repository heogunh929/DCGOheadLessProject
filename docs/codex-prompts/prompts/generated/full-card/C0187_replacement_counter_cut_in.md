# C0187_replacement_counter_cut_in - replacement/counter/cut-in card porting 2

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0187_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT10_024` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_024.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress, SelectDigiXros` | 3 |
| `BT10_026` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_026.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, modifier_duration, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress, SelectDigiXros` | 3 |
| `BT10_052` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_052.cs` | `BeforePayCost, OnAllyAttack, WhenDigisorption` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_priority, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget` | 1 |
| `BT10_056` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_056.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT10_066` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_066.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 3 |
| `BT10_074` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_074.cs` | `WhenPermanentWouldBeDeleted` | `replacement_or_cut_in` | `-` | 1 |
| `BT10_084` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_084.cs` | `OnEnterFieldAnyone, WhenWouldDigivolutionCardDiscarded` | `max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT10_111` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_111.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress, SelectDigiXros` | 2 |
| `BT11_009` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_009.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress, SelectDigiXros` | 1 |
| `BT11_012` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_012.cs` | `None, OnEnterFieldAnyone, OnStartTurn, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress, SelectDigiXros` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT10-024#2060@base` | `BT10-024` | 2060 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_024.asset` |
| `BT10-024#2061@P1` | `BT10-024` | 2061 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_024_P1.asset` |
| `BT10-024#8096@P2` | `BT10-024` | 8096 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_024_P2.asset` |
| `BT10-026#2063@base` | `BT10-026` | 2063 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_026.asset` |
| `BT10-026#2064@P1` | `BT10-026` | 2064 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_026_P1.asset` |
| `BT10-026#4307@P0` | `BT10-026` | 4307 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_026_P0.asset` |
| `BT10-052#2093@base` | `BT10-052` | 2093 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_052.asset` |
| `BT10-056#2098@base` | `BT10-056` | 2098 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_056.asset` |
| `BT10-056#4327@P0` | `BT10-056` | 4327 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_056_P0.asset` |
| `BT10-066#2110@base` | `BT10-066` | 2110 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_066.asset` |
| `BT10-066#4330@P0` | `BT10-066` | 4330 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_066_P0.asset` |
| `BT10-066#4331@P1` | `BT10-066` | 4331 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_066_P1.asset` |
| `BT10-074#2120@base` | `BT10-074` | 2120 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_074.asset` |
| `BT10-084#2132@base` | `BT10-084` | 2132 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_084.asset` |
| `BT10-111#2167@base` | `BT10-111` | 2167 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_111.asset` |
| `BT10-111#2168@P1` | `BT10-111` | 2168 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_111_P1.asset` |
| `BT11-009#2276@base` | `BT11-009` | 2276 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_009.asset` |
| `BT11-012#2279@base` | `BT11-012` | 2279 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_012.asset` |
| `BT11-012#4373@P0` | `BT11-012` | 4373 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_012_P0.asset` |
| `BT11-012#4374@P1` | `BT11-012` | 4374 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_012_P1.asset` |

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
