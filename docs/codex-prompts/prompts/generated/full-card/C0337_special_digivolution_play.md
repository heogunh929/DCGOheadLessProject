# C0337_special_digivolution_play - special digivolution/play mechanics card porting 102

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0337_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT7_058` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_058.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 4 |
| `BT7_059` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_059.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT7_072` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_072.cs` | `None, OnDiscardHand` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT7_073` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_073.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 3 |
| `BT7_082` | `DCGO/Assets/Scripts/CardEffect/BT7/White/BT7_082.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 2 |
| `BT7_083` | `DCGO/Assets/Scripts/CardEffect/BT7/White/BT7_083.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `BT7_084` | `DCGO/Assets/Scripts/CardEffect/BT7/White/BT7_084.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT7_092` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_092.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT7_093` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_093.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT7_094` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_094.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT7-058#1466@base` | `BT7-058` | 1466 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_058.asset` |
| `BT7-058#8790@P0` | `BT7-058` | 8790 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_058_P0.asset` |
| `BT7-058#8791@P1` | `BT7-058` | 8791 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_058_P1.asset` |
| `BT7-058#8792@P2` | `BT7-058` | 8792 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_058_P2.asset` |
| `BT7-059#1467@base` | `BT7-059` | 1467 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_059.asset` |
| `BT7-072#1491@base` | `BT7-072` | 1491 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_072.asset` |
| `BT7-073#1492@base` | `BT7-073` | 1492 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_073.asset` |
| `BT7-073#1493@P1` | `BT7-073` | 1493 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_073_P1.asset` |
| `BT7-073#8802@P0` | `BT7-073` | 8802 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_073_P0.asset` |
| `BT7-082#1511@base` | `BT7-082` | 1511 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_082.asset` |
| `BT7-082#8808@P0` | `BT7-082` | 8808 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_082_P0.asset` |
| `BT7-083#1512@base` | `BT7-083` | 1512 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_083.asset` |
| `BT7-083#6790@P0` | `BT7-083` | 6790 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_083_P0.asset` |
| `BT7-084#1513@base` | `BT7-084` | 1513 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_084.asset` |
| `BT7-084#8809@P0` | `BT7-084` | 8809 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_084_P0.asset` |
| `BT7-092#1527@base` | `BT7-092` | 1527 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Option/BT7_092.asset` |
| `BT7-092#8819@P1` | `BT7-092` | 8819 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Option/BT7_092_P1.asset` |
| `BT7-093#1528@base` | `BT7-093` | 1528 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Option/BT7_093.asset` |
| `BT7-093#8820@P0` | `BT7-093` | 8820 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Option/BT7_093_P0.asset` |
| `BT7-094#1529@base` | `BT7-094` | 1529 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Option/BT7_094.asset` |

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
