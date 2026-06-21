# C0375_special_digivolution_play - special digivolution/play mechanics card porting 140

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0375_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX5_065` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_065.cs` | `OnAddDigivolutionCards, OnEndTurn, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX5_066` | `DCGO/Assets/Scripts/CardEffect/EX5/Red/EX5_066.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX5_067` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_067.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX5_068` | `DCGO/Assets/Scripts/CardEffect/EX5/Yellow/EX5_068.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `EX5_069` | `DCGO/Assets/Scripts/CardEffect/EX5/Purple/EX5_069.cs` | `OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX5_070` | `DCGO/Assets/Scripts/CardEffect/EX5/White/EX5_070.cs` | `None, OptionSkill, SecuritySkill, WhenRemoveField` | `inherited, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `EX5_071` | `DCGO/Assets/Scripts/CardEffect/EX5/White/EX5_071.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `EX5_072` | `DCGO/Assets/Scripts/CardEffect/EX5/White/EX5_072.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `EX6_006` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_006.cs` | `BeforePayCost, None, OnEndTurn, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectBoolean, SelectJogress` | 4 |
| `EX6_007` | `DCGO/Assets/Scripts/CardEffect/EX6/Red/EX6_007.cs` | `None, OnAddDigivolutionCards, OnDeclaration` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX5-065#3104@base` | `EX5-065` | 3104 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Tamer/EX5_065.asset` |
| `EX5-066#3105@base` | `EX5-066` | 3105 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Option/EX5_066.asset` |
| `EX5-067#3106@base` | `EX5-067` | 3106 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Option/EX5_067.asset` |
| `EX5-068#3107@base` | `EX5-068` | 3107 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Option/EX5_068.asset` |
| `EX5-069#3108@base` | `EX5-069` | 3108 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/Option/EX5_069.asset` |
| `EX5-070#3109@base` | `EX5-070` | 3109 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/White/Option/EX5_070.asset` |
| `EX5-070#4236@P1` | `EX5-070` | 4236 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/White/Option/EX5_070_P1.asset` |
| `EX5-070#4237@P2` | `EX5-070` | 4237 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX5/White/Option/EX5_070_P2.asset` |
| `EX5-070#9149@P3` | `EX5-070` | 9149 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX5/White/Option/EX5_070_P3.asset` |
| `EX5-070#9150@P4` | `EX5-070` | 9150 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/EX5/White/Option/EX5_070_P4.asset` |
| `EX5-071#3110@base` | `EX5-071` | 3110 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/White/Option/EX5_071.asset` |
| `EX5-072#3111@base` | `EX5-072` | 3111 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/White/Option/EX5_072.asset` |
| `EX6-006#3444@base` | `EX6-006` | 3444 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/DigiEgg/EX6_006.asset` |
| `EX6-006#9152@P1` | `EX6-006` | 9152 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/DigiEgg/EX6_006_P1.asset` |
| `EX6-006#9153@P2` | `EX6-006` | 9153 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/DigiEgg/EX6_006_P2.asset` |
| `EX6-006#9154@P3` | `EX6-006` | 9154 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/DigiEgg/EX6_006_P3.asset` |
| `EX6-007#3445@base` | `EX6-007` | 3445 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Red/Digimon/EX6_007.asset` |
| `EX6-007#9155@P1` | `EX6-007` | 9155 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Red/Digimon/EX6_007_P1.asset` |

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
