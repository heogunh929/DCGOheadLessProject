# C0368_special_digivolution_play - special digivolution/play mechanics card porting 133

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0368_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX4_014` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_014.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectDigiXros` | 1 |
| `EX4_016` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_016.cs` | `OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress, SelectDigiXros` | 1 |
| `EX4_024` | `DCGO/Assets/Scripts/CardEffect/EX4/Yellow/EX4_024.cs` | `None, OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `EX4_025` | `DCGO/Assets/Scripts/CardEffect/EX4/Yellow/EX4_025.cs` | `None, OnAllyAttack, OnEndAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX4_026` | `DCGO/Assets/Scripts/CardEffect/EX4/Yellow/EX4_026.cs` | `None, OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX4_028` | `DCGO/Assets/Scripts/CardEffect/EX4/Yellow/EX4_028.cs` | `None, OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX4_030` | `DCGO/Assets/Scripts/CardEffect/EX4/Yellow/EX4_030.cs` | `None, OnEnterFieldAnyone, OnUseOption` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX4_032` | `DCGO/Assets/Scripts/CardEffect/EX4/Green/EX4_032.cs` | `OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `EX4_033` | `DCGO/Assets/Scripts/CardEffect/EX4/Green/EX4_033.cs` | `None, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 6 |
| `EX4_034` | `DCGO/Assets/Scripts/CardEffect/EX4/Green/EX4_034.cs` | `OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX4-014#2560@base` | `EX4-014` | 2560 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_014.asset` |
| `EX4-016#2562@base` | `EX4-016` | 2562 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_016.asset` |
| `EX4-024#2572@base` | `EX4-024` | 2572 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_024.asset` |
| `EX4-024#4242@P1` | `EX4-024` | 4242 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_024_P1.asset` |
| `EX4-024#4243@P2` | `EX4-024` | 4243 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_024_P2.asset` |
| `EX4-025#2573@base` | `EX4-025` | 2573 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_025.asset` |
| `EX4-026#2574@base` | `EX4-026` | 2574 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_026.asset` |
| `EX4-028#2577@base` | `EX4-028` | 2577 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_028.asset` |
| `EX4-030#2579@base` | `EX4-030` | 2579 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_030.asset` |
| `EX4-030#2580@P1` | `EX4-030` | 2580 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_030_P1.asset` |
| `EX4-032#2582@base` | `EX4-032` | 2582 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_032.asset` |
| `EX4-032#3296@P1` | `EX4-032` | 3296 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_032_P1.asset` |
| `EX4-032#4244@P2` | `EX4-032` | 4244 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_032_P2.asset` |
| `EX4-033#2583@base` | `EX4-033` | 2583 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_033.asset` |
| `EX4-033#2584@P1` | `EX4-033` | 2584 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_033_P1.asset` |
| `EX4-033#3297@P2` | `EX4-033` | 3297 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_033_P2.asset` |
| `EX4-033#4245@P3` | `EX4-033` | 4245 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_033_P3.asset` |
| `EX4-033#4246@P4` | `EX4-033` | 4246 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_033_P4.asset` |
| `EX4-033#4247@P0` | `EX4-033` | 4247 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_033_P0.asset` |
| `EX4-034#2585@base` | `EX4-034` | 2585 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_034.asset` |
| `EX4-034#3298@P1` | `EX4-034` | 3298 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_034_P1.asset` |
| `EX4-034#4248@P2` | `EX4-034` | 4248 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_034_P2.asset` |

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
