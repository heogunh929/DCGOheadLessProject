# C0284_special_digivolution_play - special digivolution/play mechanics card porting 49

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0284_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT19_049` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_049.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT19_051` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_051.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress, SelectDigiXros` | 1 |
| `BT19_056` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_056.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT19_060` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_060.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT19_065` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_065.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectDigiXros` | 2 |
| `BT19_066` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_066.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectJogress` | 1 |
| `BT19_068` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_068.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress, SelectDigiXros` | 1 |
| `BT19_069` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_069.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT19_070` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_070.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress, SelectDigiXros` | 2 |
| `BT19_076` | `DCGO/Assets/Scripts/CardEffect/BT19/White/BT19_076.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT19-049#5035@base` | `BT19-049` | 5035 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_049.asset` |
| `BT19-051#5039@base` | `BT19-051` | 5039 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_051.asset` |
| `BT19-056#5041@base` | `BT19-056` | 5041 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_056.asset` |
| `BT19-060#5043@base` | `BT19-060` | 5043 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_060.asset` |
| `BT19-065#4008@base` | `BT19-065` | 4008 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_065.asset` |
| `BT19-065#8285@P1` | `BT19-065` | 8285 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_065_P1.asset` |
| `BT19-066#4010@base` | `BT19-066` | 4010 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_066.asset` |
| `BT19-068#5050@base` | `BT19-068` | 5050 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_068.asset` |
| `BT19-069#4009@base` | `BT19-069` | 4009 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_069.asset` |
| `BT19-070#4011@base` | `BT19-070` | 4011 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_070.asset` |
| `BT19-070#8287@P1` | `BT19-070` | 8287 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_070_P1.asset` |
| `BT19-076#5055@base` | `BT19-076` | 5055 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/White/Digimon/BT19_076.asset` |

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
