# C0405_special_digivolution_play - special digivolution/play mechanics card porting 170

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0405_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST1_14` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_14.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `ST1_15` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_15.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST1_16` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_16.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `ST20_10` | `DCGO/Assets/Scripts/CardEffect/ST20/Black/ST20_10.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectJogress` | 4 |
| `ST20_15` | `DCGO/Assets/Scripts/CardEffect/ST20/White/ST20_15.cs` | `None, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `ST21_10` | `DCGO/Assets/Scripts/CardEffect/ST21/Purple/ST21_10.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand, SelectJogress` | 3 |
| `ST21_15` | `DCGO/Assets/Scripts/CardEffect/ST21/White/ST21_15.cs` | `None, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `ST22_01` | `DCGO/Assets/Scripts/CardEffect/ST22/Yellow/ST22_01.cs` | `OnUseOption` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `ST22_04` | `DCGO/Assets/Scripts/CardEffect/ST22/Yellow/ST22_04.cs` | `OnAllyAttack, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `ST22_06` | `DCGO/Assets/Scripts/CardEffect/ST22/Yellow/ST22_06.cs` | `None, OnEnterFieldAnyone, OnLoseSecurity, OnUseOption` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST1-14#37@base` | `ST1-14` | 37 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Option/ST1_14.asset` |
| `ST1-15#38@base` | `ST1-15` | 38 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Option/ST1_15.asset` |
| `ST1-16#39@base` | `ST1-16` | 39 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Option/ST1_16.asset` |
| `ST1-16#40@P1` | `ST1-16` | 40 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Option/ST1_16_P1.asset` |
| `ST1-16#41@P2` | `ST1-16` | 41 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Option/ST1_16_P2.asset` |
| `ST1-16#42@P3` | `ST1-16` | 42 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Option/ST1_16_P3.asset` |
| `ST1-16#43@P4` | `ST1-16` | 43 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Option/ST1_16_P4.asset` |
| `ST20-10#5272@base` | `ST20-10` | 5272 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Black/Digimon/ST20_10.asset` |
| `ST20-10#5273@P1` | `ST20-10` | 5273 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Black/Digimon/ST20_10_P1.asset` |
| `ST20-10#9060@P2` | `ST20-10` | 9060 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Black/Digimon/ST20_10_P2.asset` |
| `ST20-10#9061@P3` | `ST20-10` | 9061 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Black/Digimon/ST20_10_P3.asset` |
| `ST20-15#5278@base` | `ST20-15` | 5278 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/White/Option/ST20_15.asset` |
| `ST21-10#5288@base` | `ST21-10` | 5288 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Purple/Digimon/ST21_10.asset` |
| `ST21-10#5289@P1` | `ST21-10` | 5289 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Purple/Digimon/ST21_10_P1.asset` |
| `ST21-10#9073@P2` | `ST21-10` | 9073 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Purple/Digimon/ST21_10_P2.asset` |
| `ST21-15#5294@base` | `ST21-15` | 5294 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/White/Option/ST21_15.asset` |
| `ST22-01#7491@base` | `ST22-01` | 7491 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/DigiEgg/ST22_01.asset` |
| `ST22-04#7495@base` | `ST22-04` | 7495 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Digimon/ST22_04.asset` |
| `ST22-06#7497@base` | `ST22-06` | 7497 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Digimon/ST22_06.asset` |
| `ST22-06#9076@P1` | `ST22-06` | 9076 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Digimon/ST22_06_P1.asset` |
| `ST22-06#9077@P2` | `ST22-06` | 9077 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Digimon/ST22_06_P2.asset` |
| `ST22-06#9078@P3` | `ST22-06` | 9078 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Digimon/ST22_06_P3.asset` |

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
