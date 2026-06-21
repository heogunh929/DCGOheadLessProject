# C0365_special_digivolution_play - special digivolution/play mechanics card porting 130

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0365_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX3_041` | `DCGO/Assets/Scripts/CardEffect/EX3/Green/EX3_041.cs` | `None, OnEndTurn` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `EX3_044` | `DCGO/Assets/Scripts/CardEffect/EX3/Green/EX3_044.cs` | `None, OnEndBattle, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX3_047` | `DCGO/Assets/Scripts/CardEffect/EX3/Black/EX3_047.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `EX3_048` | `DCGO/Assets/Scripts/CardEffect/EX3/Black/EX3_048.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX3_051` | `DCGO/Assets/Scripts/CardEffect/EX3/Black/EX3_051.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX3_052` | `DCGO/Assets/Scripts/CardEffect/EX3/Black/EX3_052.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `EX3_055` | `DCGO/Assets/Scripts/CardEffect/EX3/Purple/EX3_055.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX3_056` | `DCGO/Assets/Scripts/CardEffect/EX3/Purple/EX3_056.cs` | `None, OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX3_057` | `DCGO/Assets/Scripts/CardEffect/EX3/Purple/EX3_057.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX3_058` | `DCGO/Assets/Scripts/CardEffect/EX3/Purple/EX3_058.cs` | `OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX3-041#2222@base` | `EX3-041` | 2222 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_041.asset` |
| `EX3-041#2223@P1` | `EX3-041` | 2223 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_041_P1.asset` |
| `EX3-044#2226@base` | `EX3-044` | 2226 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_044.asset` |
| `EX3-044#2227@P1` | `EX3-044` | 2227 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_044_P1.asset` |
| `EX3-047#2231@base` | `EX3-047` | 2231 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Digimon/EX3_047.asset` |
| `EX3-048#2232@base` | `EX3-048` | 2232 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Digimon/EX3_048.asset` |
| `EX3-051#2236@base` | `EX3-051` | 2236 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Digimon/EX3_051.asset` |
| `EX3-052#2237@base` | `EX3-052` | 2237 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Digimon/EX3_052.asset` |
| `EX3-055#2242@base` | `EX3-055` | 2242 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_055.asset` |
| `EX3-056#2243@base` | `EX3-056` | 2243 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_056.asset` |
| `EX3-057#2244@base` | `EX3-057` | 2244 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_057.asset` |
| `EX3-058#2245@base` | `EX3-058` | 2245 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_058.asset` |

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
