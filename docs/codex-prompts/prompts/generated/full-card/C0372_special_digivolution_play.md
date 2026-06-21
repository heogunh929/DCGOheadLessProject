# C0372_special_digivolution_play - special digivolution/play mechanics card porting 137

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0372_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX4_073` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_073.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `EX4_074` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_074.cs` | `None, OnDestroyedAnyone, OnEndAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 5 |
| `EX5_004` | `DCGO/Assets/Scripts/CardEffect/EX5/Green/EX5_004.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `SelectJogress` | 2 |
| `EX5_009` | `DCGO/Assets/Scripts/CardEffect/EX5/Red/EX5_009.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `EX5_010` | `DCGO/Assets/Scripts/CardEffect/EX5/Red/EX5_010.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `EX5_011` | `DCGO/Assets/Scripts/CardEffect/EX5/Red/EX5_011.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `EX5_019` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_019.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `EX5_021` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_021.cs` | `OnAllyAttack, OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `EX5_022` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_022.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `EX5_023` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_023.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX4-073#2639@base` | `EX4-073` | 2639 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_073.asset` |
| `EX4-073#2640@P1` | `EX4-073` | 2640 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_073_P1.asset` |
| `EX4-073#9140@P2` | `EX4-073` | 9140 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_073_P2.asset` |
| `EX4-074#2641@base` | `EX4-074` | 2641 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_074.asset` |
| `EX4-074#2642@P1` | `EX4-074` | 2642 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_074_P1.asset` |
| `EX4-074#6826@P2` | `EX4-074` | 6826 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_074_P2.asset` |
| `EX4-074#9141@P3` | `EX4-074` | 9141 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_074_P3.asset` |
| `EX4-074#9142@P4` | `EX4-074` | 9142 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_074_P4.asset` |
| `EX5-004#3043@base` | `EX5-004` | 3043 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/DigiEgg/EX5_004.asset` |
| `EX5-004#4202@P1` | `EX5-004` | 4202 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/DigiEgg/EX5_004_P1.asset` |
| `EX5-009#3048@base` | `EX5-009` | 3048 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_009.asset` |
| `EX5-010#3049@base` | `EX5-010` | 3049 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_010.asset` |
| `EX5-011#3050@base` | `EX5-011` | 3050 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_011.asset` |
| `EX5-019#3058@base` | `EX5-019` | 3058 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_019.asset` |
| `EX5-021#3060@base` | `EX5-021` | 3060 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_021.asset` |
| `EX5-022#3061@base` | `EX5-022` | 3061 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_022.asset` |
| `EX5-023#3062@base` | `EX5-023` | 3062 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_023.asset` |

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
