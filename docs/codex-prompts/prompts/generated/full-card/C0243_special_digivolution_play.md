# C0243_special_digivolution_play - special digivolution/play mechanics card porting 8

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0243_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 25
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT11_020` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_020.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `BT11_022` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_022.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `BT11_023` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_023.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT11_029` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_029.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT11_031` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_031.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectJogress` | 5 |
| `BT11_034` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_034.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT11_036` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_036.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT11_038` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_038.cs` | `OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT11_042` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_042.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `BT11_044` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_044.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-020#2290@base` | `BT11-020` | 2290 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_020.asset` |
| `BT11-020#4384@P0` | `BT11-020` | 4384 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_020_P0.asset` |
| `BT11-020#4385@P1` | `BT11-020` | 4385 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_020_P1.asset` |
| `BT11-022#2292@base` | `BT11-022` | 2292 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_022.asset` |
| `BT11-022#4386@P0` | `BT11-022` | 4386 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_022_P0.asset` |
| `BT11-022#4387@P1` | `BT11-022` | 4387 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_022_P1.asset` |
| `BT11-023#2293@base` | `BT11-023` | 2293 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_023.asset` |
| `BT11-023#4388@P0` | `BT11-023` | 4388 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_023_P0.asset` |
| `BT11-029#2299@base` | `BT11-029` | 2299 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_029.asset` |
| `BT11-029#4392@P0` | `BT11-029` | 4392 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_029_P0.asset` |
| `BT11-031#2301@base` | `BT11-031` | 2301 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_031.asset` |
| `BT11-031#4394@P0` | `BT11-031` | 4394 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_031_P0.asset` |
| `BT11-031#4395@P1` | `BT11-031` | 4395 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_031_P1.asset` |
| `BT11-031#4396@P2` | `BT11-031` | 4396 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_031_P2.asset` |
| `BT11-031#4397@P3` | `BT11-031` | 4397 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_031_P3.asset` |
| `BT11-034#2306@base` | `BT11-034` | 2306 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_034.asset` |
| `BT11-036#2308@base` | `BT11-036` | 2308 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_036.asset` |
| `BT11-036#4398@P0` | `BT11-036` | 4398 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_036_P0.asset` |
| `BT11-038#2310@base` | `BT11-038` | 2310 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_038.asset` |
| `BT11-038#4399@P0` | `BT11-038` | 4399 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_038_P0.asset` |
| `BT11-042#2314@base` | `BT11-042` | 2314 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_042.asset` |
| `BT11-042#2315@P1` | `BT11-042` | 2315 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_042_P1.asset` |
| `BT11-042#4401@P0` | `BT11-042` | 4401 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_042_P0.asset` |
| `BT11-044#2317@base` | `BT11-044` | 2317 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_044.asset` |
| `BT11-044#4404@P0` | `BT11-044` | 4404 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_044_P0.asset` |

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
