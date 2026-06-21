# C0371_special_digivolution_play - special digivolution/play mechanics card porting 136

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0371_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX4_063` | `DCGO/Assets/Scripts/CardEffect/EX4/Green/EX4_063.cs` | `BeforePayCost, OnEndTurn, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 3 |
| `EX4_064` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_064.cs` | `OnDestroyedAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `EX4_065` | `DCGO/Assets/Scripts/CardEffect/EX4/Red/EX4_065.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX4_066` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_066.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectInteger, SelectSecurity, SelectJogress` | 2 |
| `EX4_067` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_067.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX4_068` | `DCGO/Assets/Scripts/CardEffect/EX4/Yellow/EX4_068.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX4_069` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_069.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX4_070` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_070.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX4_071` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_071.cs` | `OnEndTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX4_072` | `DCGO/Assets/Scripts/CardEffect/EX4/White/EX4_072.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX4-063#2625@base` | `EX4-063` | 2625 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Tamer/EX4_063.asset` |
| `EX4-063#2626@P1` | `EX4-063` | 2626 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Tamer/EX4_063_P1.asset` |
| `EX4-063#4253@P2` | `EX4-063` | 4253 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/Tamer/EX4_063_P2.asset` |
| `EX4-064#2627@base` | `EX4-064` | 2627 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Tamer/EX4_064.asset` |
| `EX4-064#2628@P1` | `EX4-064` | 2628 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Tamer/EX4_064_P1.asset` |
| `EX4-065#2629@base` | `EX4-065` | 2629 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Option/EX4_065.asset` |
| `EX4-066#2630@base` | `EX4-066` | 2630 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Option/EX4_066.asset` |
| `EX4-066#2631@P1` | `EX4-066` | 2631 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Option/EX4_066_P1.asset` |
| `EX4-067#2632@base` | `EX4-067` | 2632 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Option/EX4_067.asset` |
| `EX4-068#2633@base` | `EX4-068` | 2633 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Option/EX4_068.asset` |
| `EX4-068#4254@P1` | `EX4-068` | 4254 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Option/EX4_068_P1.asset` |
| `EX4-069#2634@base` | `EX4-069` | 2634 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Option/EX4_069.asset` |
| `EX4-070#2635@base` | `EX4-070` | 2635 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Option/EX4_070.asset` |
| `EX4-071#2636@base` | `EX4-071` | 2636 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Option/EX4_071.asset` |
| `EX4-072#2637@base` | `EX4-072` | 2637 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/White/Option/EX4_072.asset` |
| `EX4-072#2638@P1` | `EX4-072` | 2638 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/White/Option/EX4_072_P1.asset` |

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
