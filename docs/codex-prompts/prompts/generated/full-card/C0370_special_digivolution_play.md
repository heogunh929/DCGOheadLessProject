# C0370_special_digivolution_play - special digivolution/play mechanics card porting 135

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0370_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX4_050` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_050.cs` | `None, OnDestroyedAnyone, OnLoseSecurity` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `EX4_051` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_051.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 2 |
| `EX4_053` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_053.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX4_054` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_054.cs` | `None, OnAllyAttack, OnEndAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX4_055` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_055.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `EX4_056` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_056.cs` | `OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX4_058` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_058.cs` | `None, OnDestroyedAnyone, OnEndAttack, OnEndTurn` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `EX4_060` | `DCGO/Assets/Scripts/CardEffect/EX4/White/EX4_060.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `EX4_061` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_061.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `EX4_062` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_062.cs` | `BeforePayCost, None, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress, SelectDigiXros` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX4-050#2604@base` | `EX4-050` | 2604 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_050.asset` |
| `EX4-050#2605@P1` | `EX4-050` | 2605 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_050_P1.asset` |
| `EX4-050#4249@P2` | `EX4-050` | 4249 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_050_P2.asset` |
| `EX4-050#4250@P3` | `EX4-050` | 4250 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_050_P3.asset` |
| `EX4-050#4251@P4` | `EX4-050` | 4251 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_050_P4.asset` |
| `EX4-051#2606@base` | `EX4-051` | 2606 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_051.asset` |
| `EX4-051#2607@P1` | `EX4-051` | 2607 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_051_P1.asset` |
| `EX4-053#2609@base` | `EX4-053` | 2609 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_053.asset` |
| `EX4-053#2610@P1` | `EX4-053` | 2610 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_053_P1.asset` |
| `EX4-054#2611@base` | `EX4-054` | 2611 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_054.asset` |
| `EX4-055#2612@base` | `EX4-055` | 2612 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_055.asset` |
| `EX4-056#2613@base` | `EX4-056` | 2613 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_056.asset` |
| `EX4-058#2615@base` | `EX4-058` | 2615 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_058.asset` |
| `EX4-058#2616@P1` | `EX4-058` | 2616 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_058_P1.asset` |
| `EX4-060#2618@base` | `EX4-060` | 2618 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/White/Digimon/EX4_060.asset` |
| `EX4-060#2619@P1` | `EX4-060` | 2619 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/White/Digimon/EX4_060_P1.asset` |
| `EX4-060#2620@P2` | `EX4-060` | 2620 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX4/White/Digimon/EX4_060_P2.asset` |
| `EX4-061#2621@base` | `EX4-061` | 2621 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Tamer/EX4_061.asset` |
| `EX4-061#2622@P1` | `EX4-061` | 2622 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Tamer/EX4_061_P1.asset` |
| `EX4-062#2623@base` | `EX4-062` | 2623 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Tamer/EX4_062.asset` |
| `EX4-062#2624@P1` | `EX4-062` | 2624 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Tamer/EX4_062_P1.asset` |

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
