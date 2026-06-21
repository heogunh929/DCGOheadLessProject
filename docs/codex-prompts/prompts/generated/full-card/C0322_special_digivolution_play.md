# C0322_special_digivolution_play - special digivolution/play mechanics card porting 87

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0322_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT3_102` | `DCGO/Assets/Scripts/CardEffect/BT3/Yellow/BT3_102.cs` | `OptionSkill` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT3_103` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_103.cs` | `AfterPayCost, BeforePayCost, None, OptionSkill, SecuritySkill` | `background, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT3_104` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_104.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT3_105` | `DCGO/Assets/Scripts/CardEffect/BT3/Black/BT3_105.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT3_106` | `DCGO/Assets/Scripts/CardEffect/BT3/Black/BT3_106.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT3_107` | `DCGO/Assets/Scripts/CardEffect/BT3/Black/BT3_107.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT3_108` | `DCGO/Assets/Scripts/CardEffect/BT3/Purple/BT3_108.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT3_109` | `DCGO/Assets/Scripts/CardEffect/BT3/Purple/BT3_109.cs` | `OnDestroyedAnyone, OptionSkill` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT3_110` | `DCGO/Assets/Scripts/CardEffect/BT3/Purple/BT3_110.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `BT3_111` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_111.cs` | `None, OnDetermineDoSecurityCheck, OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT3-102#743@base` | `BT3-102` | 743 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Option/BT3_102.asset` |
| `BT3-103#744@base` | `BT3-103` | 744 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Option/BT3_103.asset` |
| `BT3-103#745@P1` | `BT3-103` | 745 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Option/BT3_103_P1.asset` |
| `BT3-103#8492@P2` | `BT3-103` | 8492 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Option/BT3_103_P2.asset` |
| `BT3-104#746@base` | `BT3-104` | 746 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Option/BT3_104.asset` |
| `BT3-105#747@base` | `BT3-105` | 747 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Option/BT3_105.asset` |
| `BT3-105#8493@P1` | `BT3-105` | 8493 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Option/BT3_105_P1.asset` |
| `BT3-106#748@base` | `BT3-106` | 748 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Option/BT3_106.asset` |
| `BT3-107#749@base` | `BT3-107` | 749 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Option/BT3_107.asset` |
| `BT3-108#750@base` | `BT3-108` | 750 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Option/BT3_108.asset` |
| `BT3-109#751@base` | `BT3-109` | 751 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Option/BT3_109.asset` |
| `BT3-110#752@base` | `BT3-110` | 752 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Option/BT3_110.asset` |
| `BT3-111#753@base` | `BT3-111` | 753 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_111.asset` |
| `BT3-111#754@P1` | `BT3-111` | 754 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_111_P1.asset` |
| `BT3-111#755@P2` | `BT3-111` | 755 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_111_P2.asset` |

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
