# C0362_special_digivolution_play - special digivolution/play mechanics card porting 127

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0362_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX2_059` | `DCGO/Assets/Scripts/CardEffect/EX2/Yellow/EX2_059.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `EX2_060` | `DCGO/Assets/Scripts/CardEffect/EX2/Yellow/EX2_060.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 3 |
| `EX2_061` | `DCGO/Assets/Scripts/CardEffect/EX2/Green/EX2_061.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `EX2_062` | `DCGO/Assets/Scripts/CardEffect/EX2/Black/EX2_062.cs` | `OnAllyAttack, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `EX2_065` | `DCGO/Assets/Scripts/CardEffect/EX2/Purple/EX2_065.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 2 |
| `EX2_066` | `DCGO/Assets/Scripts/CardEffect/EX2/Red/EX2_066.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectSecurity, SelectJogress` | 2 |
| `EX2_067` | `DCGO/Assets/Scripts/CardEffect/EX2/Red/EX2_067.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `EX2_068` | `DCGO/Assets/Scripts/CardEffect/EX2/Blue/EX2_068.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX2_069` | `DCGO/Assets/Scripts/CardEffect/EX2/Blue/EX2_069.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX2_070` | `DCGO/Assets/Scripts/CardEffect/EX2/Green/EX2_070.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX2-059#2007@base` | `EX2-059` | 2007 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Tamer/EX2_059.asset` |
| `EX2-060#2008@base` | `EX2-060` | 2008 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Tamer/EX2_060.asset` |
| `EX2-060#2009@P1` | `EX2-060` | 2009 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Tamer/EX2_060_P1.asset` |
| `EX2-060#9115@P2` | `EX2-060` | 9115 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Tamer/EX2_060_P2.asset` |
| `EX2-061#2010@base` | `EX2-061` | 2010 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Tamer/EX2_061.asset` |
| `EX2-061#2011@P1` | `EX2-061` | 2011 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Tamer/EX2_061_P1.asset` |
| `EX2-061#3293@P2` | `EX2-061` | 3293 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Tamer/EX2_061_P2.asset` |
| `EX2-061#9116@P0` | `EX2-061` | 9116 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Tamer/EX2_061_P0.asset` |
| `EX2-062#2012@base` | `EX2-062` | 2012 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Tamer/EX2_062.asset` |
| `EX2-062#2013@P1` | `EX2-062` | 2013 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Tamer/EX2_062_P1.asset` |
| `EX2-062#9117@P2` | `EX2-062` | 9117 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Tamer/EX2_062_P2.asset` |
| `EX2-065#2016@base` | `EX2-065` | 2016 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Tamer/EX2_065.asset` |
| `EX2-065#2017@P1` | `EX2-065` | 2017 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Tamer/EX2_065_P1.asset` |
| `EX2-066#2018@base` | `EX2-066` | 2018 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Option/EX2_066.asset` |
| `EX2-066#9119@P1` | `EX2-066` | 9119 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Option/EX2_066_P1.asset` |
| `EX2-067#2019@base` | `EX2-067` | 2019 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Option/EX2_067.asset` |
| `EX2-067#3294@P1` | `EX2-067` | 3294 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Option/EX2_067_P1.asset` |
| `EX2-067#9120@P0` | `EX2-067` | 9120 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Option/EX2_067_P0.asset` |
| `EX2-068#2020@base` | `EX2-068` | 2020 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Option/EX2_068.asset` |
| `EX2-068#9121@P1` | `EX2-068` | 9121 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Option/EX2_068_P1.asset` |
| `EX2-069#2021@base` | `EX2-069` | 2021 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Option/EX2_069.asset` |
| `EX2-070#2022@base` | `EX2-070` | 2022 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Option/EX2_070.asset` |
| `EX2-070#9122@P1` | `EX2-070` | 9122 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Option/EX2_070_P1.asset` |

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
