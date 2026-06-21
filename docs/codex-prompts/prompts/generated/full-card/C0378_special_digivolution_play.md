# C0378_special_digivolution_play - special digivolution/play mechanics card porting 143

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0378_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX6_060` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_060.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX6_061` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_061.cs` | `OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX6_062` | `DCGO/Assets/Scripts/CardEffect/EX6/White/EX6_062.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX6_065` | `DCGO/Assets/Scripts/CardEffect/EX6/Red/EX6_065.cs` | `None, OptionSkill, SecuritySkill, WhenRemoveField` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX6_066` | `DCGO/Assets/Scripts/CardEffect/EX6/Blue/EX6_066.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX6_067` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_067.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX6_068` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_068.cs` | `OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `EX6_069` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_069.cs` | `OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectInteger, SelectSecurity, SelectJogress` | 1 |
| `EX6_070` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_070.cs` | `OnEndTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX6_072` | `DCGO/Assets/Scripts/CardEffect/EX6/White/EX6_072.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX6-060#3518@base` | `EX6-060` | 3518 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_060.asset` |
| `EX6-060#3519@P1` | `EX6-060` | 3519 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_060_P1.asset` |
| `EX6-061#3520@base` | `EX6-061` | 3520 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_061.asset` |
| `EX6-061#3521@P1` | `EX6-061` | 3521 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_061_P1.asset` |
| `EX6-062#3525@base` | `EX6-062` | 3525 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/White/Digimon/EX6_062.asset` |
| `EX6-062#3526@P1` | `EX6-062` | 3526 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/White/Digimon/EX6_062_P1.asset` |
| `EX6-065#3531@base` | `EX6-065` | 3531 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Red/Option/EX6_065.asset` |
| `EX6-066#3532@base` | `EX6-066` | 3532 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Blue/Option/EX6_066.asset` |
| `EX6-067#3533@base` | `EX6-067` | 3533 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Option/EX6_067.asset` |
| `EX6-068#3534@base` | `EX6-068` | 3534 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Option/EX6_068.asset` |
| `EX6-069#3535@base` | `EX6-069` | 3535 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Option/EX6_069.asset` |
| `EX6-070#3536@base` | `EX6-070` | 3536 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Option/EX6_070.asset` |
| `EX6-072#3538@base` | `EX6-072` | 3538 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/White/Option/EX6_072.asset` |

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
