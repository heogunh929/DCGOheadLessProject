# C0292_special_digivolution_play - special digivolution/play mechanics card porting 57

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0292_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 10
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT20_040` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_040.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT20_042` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_042.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT20_043` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_043.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `BT20_044` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_044.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `BT20_046` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_046.cs` | `None` | `inherited, static_or_continuous` | `SelectCard, SelectJogress` | 1 |
| `BT20_051` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_051.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT20_053` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_053.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `BT20_057` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_057.cs` | `BeforePayCost, None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 1 |
| `BT20_058` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_058.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 1 |
| `BT20_059` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_059.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT20-040#5119@base` | `BT20-040` | 5119 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_040.asset` |
| `BT20-042#5121@base` | `BT20-042` | 5121 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_042.asset` |
| `BT20-043#5122@base` | `BT20-043` | 5122 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_043.asset` |
| `BT20-044#5123@base` | `BT20-044` | 5123 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_044.asset` |
| `BT20-046#5125@base` | `BT20-046` | 5125 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_046.asset` |
| `BT20-051#5130@base` | `BT20-051` | 5130 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_051.asset` |
| `BT20-053#5132@base` | `BT20-053` | 5132 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_053.asset` |
| `BT20-057#5136@base` | `BT20-057` | 5136 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_057.asset` |
| `BT20-058#5137@base` | `BT20-058` | 5137 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_058.asset` |
| `BT20-059#5138@base` | `BT20-059` | 5138 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_059.asset` |

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
