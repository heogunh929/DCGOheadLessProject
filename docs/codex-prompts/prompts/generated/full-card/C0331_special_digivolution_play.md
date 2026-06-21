# C0331_special_digivolution_play - special digivolution/play mechanics card porting 96

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0331_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 27
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT5_105` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_105.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT5_106` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_106.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT5_107` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_107.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT5_108` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_108.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT5_109` | `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_109.cs` | `AfterPayCost, BeforePayCost, None, OnEndTurn, OptionSkill, SecuritySkill` | `background, max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `BT5_110` | `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_110.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT5_111` | `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_111.cs` | `None, OnAllyAttack` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `BT6_007` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_007.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `BT6_009` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_009.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 5 |
| `BT6_011` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_011.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT5-105#1086@base` | `BT5-105` | 1086 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Option/BT5_105.asset` |
| `BT5-105#8650@P1` | `BT5-105` | 8650 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Option/BT5_105_P1.asset` |
| `BT5-106#1087@base` | `BT5-106` | 1087 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Option/BT5_106.asset` |
| `BT5-106#8651@P1` | `BT5-106` | 8651 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Option/BT5_106_P1.asset` |
| `BT5-107#1088@base` | `BT5-107` | 1088 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Option/BT5_107.asset` |
| `BT5-107#8652@P0` | `BT5-107` | 8652 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Option/BT5_107_P0.asset` |
| `BT5-107#8653@P1` | `BT5-107` | 8653 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Option/BT5_107_P1.asset` |
| `BT5-108#1089@base` | `BT5-108` | 1089 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Option/BT5_108.asset` |
| `BT5-108#8654@P0` | `BT5-108` | 8654 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Option/BT5_108_P0.asset` |
| `BT5-109#1090@base` | `BT5-109` | 1090 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Option/BT5_109.asset` |
| `BT5-109#1091@P1` | `BT5-109` | 1091 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Option/BT5_109_P1.asset` |
| `BT5-109#8655@P0` | `BT5-109` | 8655 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Option/BT5_109_P0.asset` |
| `BT5-110#1092@base` | `BT5-110` | 1092 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Option/BT5_110.asset` |
| `BT5-110#8656@P0` | `BT5-110` | 8656 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Option/BT5_110_P0.asset` |
| `BT5-111#1093@base` | `BT5-111` | 1093 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_111.asset` |
| `BT5-111#1094@P1` | `BT5-111` | 1094 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_111_P1.asset` |
| `BT5-111#8657@P2` | `BT5-111` | 8657 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_111_P2.asset` |
| `BT6-007#1111@base` | `BT6-007` | 1111 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_007.asset` |
| `BT6-007#1112@P1` | `BT6-007` | 1112 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_007_P1.asset` |
| `BT6-007#8676@P0` | `BT6-007` | 8676 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_007_P0.asset` |
| `BT6-009#1114@base` | `BT6-009` | 1114 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_009.asset` |
| `BT6-009#1115@P1` | `BT6-009` | 1115 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_009_P1.asset` |
| `BT6-009#1116@P2` | `BT6-009` | 1116 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_009_P2.asset` |
| `BT6-009#1117@P3` | `BT6-009` | 1117 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_009_P3.asset` |
| `BT6-009#8677@P0` | `BT6-009` | 8677 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_009_P0.asset` |
| `BT6-011#1119@base` | `BT6-011` | 1119 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_011.asset` |
| `BT6-011#1120@P1` | `BT6-011` | 1120 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_011_P1.asset` |

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
