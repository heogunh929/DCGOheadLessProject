# C0409_special_digivolution_play - special digivolution/play mechanics card porting 174

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0409_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 21
- Source effect count: 8
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST7_11` | `DCGO/Assets/Scripts/CardEffect/ST7/Red/ST7_11.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `ST7_12` | `DCGO/Assets/Scripts/CardEffect/ST7/Red/ST7_12.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST8_04` | `DCGO/Assets/Scripts/CardEffect/ST8/Blue/ST8_04.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 5 |
| `ST8_11` | `DCGO/Assets/Scripts/CardEffect/ST8/Blue/ST8_11.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST8_12` | `DCGO/Assets/Scripts/CardEffect/ST8/Blue/ST8_12.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST9_05` | `DCGO/Assets/Scripts/CardEffect/ST9/Blue/ST9_05.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 7 |
| `ST9_14` | `DCGO/Assets/Scripts/CardEffect/ST9/Blue/ST9_14.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `ST9_15` | `DCGO/Assets/Scripts/CardEffect/ST9/Green/ST9_15.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST7-11#584@base` | `ST7-11` | 584 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Option/ST7_11.asset` |
| `ST7-11#585@P1` | `ST7-11` | 585 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Option/ST7_11_P1.asset` |
| `ST7-12#586@base` | `ST7-12` | 586 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Option/ST7_12.asset` |
| `ST8-04#1714@base` | `ST8-04` | 1714 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_04.asset` |
| `ST8-04#1715@P1` | `ST8-04` | 1715 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_04_P1.asset` |
| `ST8-04#1716@P2` | `ST8-04` | 1716 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_04_P2.asset` |
| `ST8-04#1717@P3` | `ST8-04` | 1717 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_04_P3.asset` |
| `ST8-04#1718@P4` | `ST8-04` | 1718 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_04_P4.asset` |
| `ST8-11#1728@base` | `ST8-11` | 1728 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Option/ST8_11.asset` |
| `ST8-12#1729@base` | `ST8-12` | 1729 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Option/ST8_12.asset` |
| `ST9-05#1738@base` | `ST9-05` | 1738 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_05.asset` |
| `ST9-05#1739@P1` | `ST9-05` | 1739 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_05_P1.asset` |
| `ST9-05#1740@P2` | `ST9-05` | 1740 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_05_P2.asset` |
| `ST9-05#1741@P3` | `ST9-05` | 1741 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_05_P3.asset` |
| `ST9-05#1742@P4` | `ST9-05` | 1742 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_05_P4.asset` |
| `ST9-05#4999@P5` | `ST9-05` | 4999 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_05_P5.asset` |
| `ST9-05#9084@P6` | `ST9-05` | 9084 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_05_P6.asset` |
| `ST9-14#1752@base` | `ST9-14` | 1752 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Option/ST9_14.asset` |
| `ST9-14#5003@P1` | `ST9-14` | 5003 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Option/ST9_14_P1.asset` |
| `ST9-14#5004@P2` | `ST9-14` | 5004 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Option/ST9_14_P2.asset` |
| `ST9-15#1753@base` | `ST9-15` | 1753 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Green/Option/ST9_15.asset` |

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
