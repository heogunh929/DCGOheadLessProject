# C0369_special_digivolution_play - special digivolution/play mechanics card porting 134

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0369_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX4_035` | `DCGO/Assets/Scripts/CardEffect/EX4/Green/EX4_035.cs` | `None, OnAllyAttack, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous` | `SelectJogress` | 1 |
| `EX4_036` | `DCGO/Assets/Scripts/CardEffect/EX4/Green/EX4_036.cs` | `None, OnAllyAttack, OnEndAttack, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX4_037` | `DCGO/Assets/Scripts/CardEffect/EX4/Green/EX4_037.cs` | `None, OnEndTurn, OnTappedAnyone` | `max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX4_038` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_038.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX4_039` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_039.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX4_040` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_040.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `EX4_042` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_042.cs` | `None` | `inherited, modifier_duration, static_or_continuous` | `SelectJogress` | 1 |
| `EX4_047` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_047.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 1 |
| `EX4_048` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_048.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX4_049` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_049.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectInteger, SelectOrder, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX4-035#2586@base` | `EX4-035` | 2586 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_035.asset` |
| `EX4-036#2587@base` | `EX4-036` | 2587 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_036.asset` |
| `EX4-037#2588@base` | `EX4-037` | 2588 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_037.asset` |
| `EX4-037#2589@P1` | `EX4-037` | 2589 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Digimon/EX4_037_P1.asset` |
| `EX4-038#2590@base` | `EX4-038` | 2590 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_038.asset` |
| `EX4-039#2591@base` | `EX4-039` | 2591 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_039.asset` |
| `EX4-040#2592@base` | `EX4-040` | 2592 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_040.asset` |
| `EX4-042#2594@base` | `EX4-042` | 2594 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_042.asset` |
| `EX4-047#2599@base` | `EX4-047` | 2599 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_047.asset` |
| `EX4-048#2600@base` | `EX4-048` | 2600 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_048.asset` |
| `EX4-048#2601@P1` | `EX4-048` | 2601 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_048_P1.asset` |
| `EX4-049#2602@base` | `EX4-049` | 2602 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_049.asset` |
| `EX4-049#2603@P1` | `EX4-049` | 2603 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_049_P1.asset` |

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
