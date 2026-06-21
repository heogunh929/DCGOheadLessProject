# C0336_special_digivolution_play - special digivolution/play mechanics card porting 101

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0336_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 26
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT7_023` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_023.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT7_028` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_028.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT7_034` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_034.cs` | `OnDeclaration` | `max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectBurstDigivolution` | 1 |
| `BT7_036` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_036.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 4 |
| `BT7_039` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_039.cs` | `OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectOrder, SelectSecurity, SelectBurstDigivolution` | 2 |
| `BT7_040` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_040.cs` | `None, OnDeclaration` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectBurstDigivolution` | 2 |
| `BT7_046` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_046.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 4 |
| `BT7_047` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_047.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT7_056` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_056.cs` | `OnAddDigivolutionCards, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 7 |
| `BT7_057` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_057.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT7-023#1410@base` | `BT7-023` | 1410 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_023.asset` |
| `BT7-023#1411@P1` | `BT7-023` | 1411 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_023_P1.asset` |
| `BT7-028#1417@base` | `BT7-028` | 1417 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_028.asset` |
| `BT7-034#1426@base` | `BT7-034` | 1426 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_034.asset` |
| `BT7-036#1430@base` | `BT7-036` | 1430 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_036.asset` |
| `BT7-036#1431@P1` | `BT7-036` | 1431 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_036_P1.asset` |
| `BT7-036#1432@P2` | `BT7-036` | 1432 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_036_P2.asset` |
| `BT7-036#8773@P0` | `BT7-036` | 8773 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_036_P0.asset` |
| `BT7-039#1435@base` | `BT7-039` | 1435 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_039.asset` |
| `BT7-039#8775@P0` | `BT7-039` | 8775 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_039_P0.asset` |
| `BT7-040#1436@base` | `BT7-040` | 1436 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_040.asset` |
| `BT7-040#1437@P1` | `BT7-040` | 1437 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_040_P1.asset` |
| `BT7-046#1449@base` | `BT7-046` | 1449 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_046.asset` |
| `BT7-046#1450@P1` | `BT7-046` | 1450 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_046_P1.asset` |
| `BT7-046#1451@P2` | `BT7-046` | 1451 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_046_P2.asset` |
| `BT7-046#8779@P0` | `BT7-046` | 8779 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_046_P0.asset` |
| `BT7-047#1452@base` | `BT7-047` | 1452 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_047.asset` |
| `BT7-047#1453@P1` | `BT7-047` | 1453 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_047_P1.asset` |
| `BT7-056#1463@base` | `BT7-056` | 1463 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_056.asset` |
| `BT7-056#1464@P1` | `BT7-056` | 1464 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_056_P1.asset` |
| `BT7-056#8785@P0` | `BT7-056` | 8785 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_056_P0.asset` |
| `BT7-056#8786@P2` | `BT7-056` | 8786 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_056_P2.asset` |
| `BT7-056#8787@P3` | `BT7-056` | 8787 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_056_P3.asset` |
| `BT7-056#8788@P5` | `BT7-056` | 8788 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_056_P5.asset` |
| `BT7-056#8789@P6` | `BT7-056` | 8789 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_056_P6.asset` |
| `BT7-057#1465@base` | `BT7-057` | 1465 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_057.asset` |

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
