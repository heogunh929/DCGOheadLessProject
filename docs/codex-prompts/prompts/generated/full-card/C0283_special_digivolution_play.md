# C0283_special_digivolution_play - special digivolution/play mechanics card porting 48

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0283_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT19_020` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_020.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT19_030` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_030.cs` | `OnStartMainPhase, OnUseOption` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT19_033` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_033.cs` | `OnDestroyedAnyone, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT19_034` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_034.cs` | `OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT19_035` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_035.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress, SelectDigiXros` | 1 |
| `BT19_038` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_038.cs` | `None, OnDestroyedAnyone, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress, SelectDigiXros` | 1 |
| `BT19_040` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_040.cs` | `None, OnEnterFieldAnyone, OnUseOption` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT19_042` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_042.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity, SelectJogress` | 2 |
| `BT19_044` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_044.cs` | `OnAllyAttack, OnStartMainPhase` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT19_047` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_047.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT19-020#5020@base` | `BT19-020` | 5020 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_020.asset` |
| `BT19-030#5024@base` | `BT19-030` | 5024 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_030.asset` |
| `BT19-033#5026@base` | `BT19-033` | 5026 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_033.asset` |
| `BT19-034#5027@base` | `BT19-034` | 5027 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_034.asset` |
| `BT19-034#8279@P1` | `BT19-034` | 8279 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_034_P1.asset` |
| `BT19-035#5028@base` | `BT19-035` | 5028 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_035.asset` |
| `BT19-038#5032@base` | `BT19-038` | 5032 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_038.asset` |
| `BT19-040#5033@base` | `BT19-040` | 5033 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_040.asset` |
| `BT19-042#3997@base` | `BT19-042` | 3997 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_042.asset` |
| `BT19-042#3998@P1` | `BT19-042` | 3998 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_042_P1.asset` |
| `BT19-044#5034@base` | `BT19-044` | 5034 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_044.asset` |
| `BT19-047#5079@base` | `BT19-047` | 5079 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_047.asset` |

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
