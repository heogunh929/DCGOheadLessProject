# C0244_special_digivolution_play - special digivolution/play mechanics card porting 9

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0244_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT11_054` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_054.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 3 |
| `BT11_058` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_058.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT11_061` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_061.cs` | `None, OnDeclaration, OnEnterFieldAnyone` | `background, inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT11_063` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_063.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectJogress` | 1 |
| `BT11_065` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_065.cs` | `OnDigivolutionCardReturnToDeckBottom, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT11_069` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_069.cs` | `None, OnEnterFieldAnyone, OnUnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT11_070` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_070.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `BT11_071` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_071.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress, SelectDigiXros` | 1 |
| `BT11_072` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_072.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `BT11_073` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_073.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-054#2327@base` | `BT11-054` | 2327 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_054.asset` |
| `BT11-054#4408@P0` | `BT11-054` | 4408 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_054_P0.asset` |
| `BT11-054#4409@P1` | `BT11-054` | 4409 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_054_P1.asset` |
| `BT11-058#2332@base` | `BT11-058` | 2332 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_058.asset` |
| `BT11-058#4412@P0` | `BT11-058` | 4412 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_058_P0.asset` |
| `BT11-061#2335@base` | `BT11-061` | 2335 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_061.asset` |
| `BT11-063#2337@base` | `BT11-063` | 2337 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_063.asset` |
| `BT11-065#2339@base` | `BT11-065` | 2339 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_065.asset` |
| `BT11-065#4415@P0` | `BT11-065` | 4415 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_065_P0.asset` |
| `BT11-069#2343@base` | `BT11-069` | 2343 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_069.asset` |
| `BT11-069#4420@P0` | `BT11-069` | 4420 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_069_P0.asset` |
| `BT11-069#8108@P1` | `BT11-069` | 8108 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_069_P1.asset` |
| `BT11-070#2344@base` | `BT11-070` | 2344 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_070.asset` |
| `BT11-070#4421@P0` | `BT11-070` | 4421 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_070_P0.asset` |
| `BT11-071#2345@base` | `BT11-071` | 2345 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_071.asset` |
| `BT11-072#2346@base` | `BT11-072` | 2346 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_072.asset` |
| `BT11-072#4422@P0` | `BT11-072` | 4422 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_072_P0.asset` |
| `BT11-073#2347@base` | `BT11-073` | 2347 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_073.asset` |
| `BT11-073#4423@P0` | `BT11-073` | 4423 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_073_P0.asset` |

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
