# C0248_special_digivolution_play - special digivolution/play mechanics card porting 13

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0248_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 33
- Source effect count: 9
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT12_017` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_017.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 6 |
| `BT12_021` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_021.cs` | `OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 5 |
| `BT12_022` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_022.cs` | `BeforePayCost, None` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 5 |
| `BT12_024` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_024.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT12_025` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_025.cs` | `None, OnAllyAttack` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT12_028` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_028.cs` | `None, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT12_029` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_029.cs` | `None, OnEnterFieldAnyone, OnUnTappedAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT12_030` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_030.cs` | `None, OnEndAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT12_031` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_031.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectJogress` | 6 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT12-017#2422@base` | `BT12-017` | 2422 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_017.asset` |
| `BT12-017#2423@P1` | `BT12-017` | 2423 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_017_P1.asset` |
| `BT12-017#4471@P2` | `BT12-017` | 4471 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_017_P2.asset` |
| `BT12-017#4472@P3` | `BT12-017` | 4472 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_017_P3.asset` |
| `BT12-017#4473@P4` | `BT12-017` | 4473 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_017_P4.asset` |
| `BT12-017#4474@P5` | `BT12-017` | 4474 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_017_P5.asset` |
| `BT12-021#2429@base` | `BT12-021` | 2429 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_021.asset` |
| `BT12-021#4478@P1` | `BT12-021` | 4478 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_021_P1.asset` |
| `BT12-021#4479@P2` | `BT12-021` | 4479 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_021_P2.asset` |
| `BT12-021#8118@P3` | `BT12-021` | 8118 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_021_P3.asset` |
| `BT12-021#8119@P4` | `BT12-021` | 8119 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_021_P4.asset` |
| `BT12-022#2430@base` | `BT12-022` | 2430 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_022.asset` |
| `BT12-022#4480@P0` | `BT12-022` | 4480 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_022_P0.asset` |
| `BT12-022#4481@P1` | `BT12-022` | 4481 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_022_P1.asset` |
| `BT12-022#8120@P2` | `BT12-022` | 8120 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_022_P2.asset` |
| `BT12-022#8121@P3` | `BT12-022` | 8121 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_022_P3.asset` |
| `BT12-024#2432@base` | `BT12-024` | 2432 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_024.asset` |
| `BT12-024#4482@P0` | `BT12-024` | 4482 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_024_P0.asset` |
| `BT12-025#2433@base` | `BT12-025` | 2433 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_025.asset` |
| `BT12-028#2436@base` | `BT12-028` | 2436 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_028.asset` |
| `BT12-028#4484@P0` | `BT12-028` | 4484 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_028_P0.asset` |
| `BT12-028#4485@P1` | `BT12-028` | 4485 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_028_P1.asset` |
| `BT12-029#2437@base` | `BT12-029` | 2437 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_029.asset` |
| `BT12-029#2438@P1` | `BT12-029` | 2438 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_029_P1.asset` |
| `BT12-030#2439@base` | `BT12-030` | 2439 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_030.asset` |
| `BT12-030#4486@P0` | `BT12-030` | 4486 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_030_P0.asset` |
| `BT12-030#4487@P1` | `BT12-030` | 4487 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_030_P1.asset` |
| `BT12-031#2440@base` | `BT12-031` | 2440 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_031.asset` |
| `BT12-031#2441@P1` | `BT12-031` | 2441 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_031_P1.asset` |
| `BT12-031#4488@P2` | `BT12-031` | 4488 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_031_P2.asset` |
| `BT12-031#4489@P3` | `BT12-031` | 4489 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_031_P3.asset` |
| `BT12-031#4490@P4` | `BT12-031` | 4490 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_031_P4.asset` |
| `BT12-031#8122@P5` | `BT12-031` | 8122 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_031_P5.asset` |

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
