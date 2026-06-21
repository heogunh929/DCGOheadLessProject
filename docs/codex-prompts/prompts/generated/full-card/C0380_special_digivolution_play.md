# C0380_special_digivolution_play - special digivolution/play mechanics card porting 145

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0380_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX7_047` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_047.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX7_050` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_050.cs` | `None` | `inherited, static_or_continuous` | `SelectCard, SelectJogress` | 1 |
| `EX7_055` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_055.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX7_058` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_058.cs` | `None, OnDestroyedAnyone, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 1 |
| `EX7_066` | `DCGO/Assets/Scripts/CardEffect/EX7/Red/EX7_066.cs` | `None, OnDigivolutionCardDiscarded, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress, SelectBurstDigivolution` | 2 |
| `EX7_067` | `DCGO/Assets/Scripts/CardEffect/EX7/Blue/EX7_067.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `EX7_068` | `DCGO/Assets/Scripts/CardEffect/EX7/Yellow/EX7_068.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `EX7_069` | `DCGO/Assets/Scripts/CardEffect/EX7/Green/EX7_069.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX7_071` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_071.cs` | `None, OnDigivolutionCardDiscarded, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX7_072` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_072.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX7-047#3765@base` | `EX7-047` | 3765 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_047.asset` |
| `EX7-050#3770@base` | `EX7-050` | 3770 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_050.asset` |
| `EX7-055#3778@base` | `EX7-055` | 3778 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_055.asset` |
| `EX7-055#3779@P1` | `EX7-055` | 3779 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_055_P1.asset` |
| `EX7-058#3784@base` | `EX7-058` | 3784 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_058.asset` |
| `EX7-066#3800@base` | `EX7-066` | 3800 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Option/EX7_066.asset` |
| `EX7-066#3801@P1` | `EX7-066` | 3801 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Option/EX7_066_P1.asset` |
| `EX7-067#3802@base` | `EX7-067` | 3802 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Option/EX7_067.asset` |
| `EX7-067#3803@P1` | `EX7-067` | 3803 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Option/EX7_067_P1.asset` |
| `EX7-068#3804@base` | `EX7-068` | 3804 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Option/EX7_068.asset` |
| `EX7-068#3805@P1` | `EX7-068` | 3805 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Option/EX7_068_P1.asset` |
| `EX7-069#3806@base` | `EX7-069` | 3806 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Option/EX7_069.asset` |
| `EX7-069#3807@P1` | `EX7-069` | 3807 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Option/EX7_069_P1.asset` |
| `EX7-071#3810@base` | `EX7-071` | 3810 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Option/EX7_071.asset` |
| `EX7-071#3811@P1` | `EX7-071` | 3811 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Option/EX7_071_P1.asset` |
| `EX7-072#3812@base` | `EX7-072` | 3812 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Option/EX7_072.asset` |

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
