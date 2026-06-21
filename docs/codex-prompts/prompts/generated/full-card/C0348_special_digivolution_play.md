# C0348_special_digivolution_play - special digivolution/play mechanics card porting 113

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0348_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT9_101` | `DCGO/Assets/Scripts/CardEffect/BT9/Green/BT9_101.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT9_102` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_102.cs` | `None, OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT9_103` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_103.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `BT9_104` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_104.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT9_105` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_105.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT9_106` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_106.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT9_107` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_107.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT9_108` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_108.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT9_109` | `DCGO/Assets/Scripts/CardEffect/BT9/White/BT9_109.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 5 |
| `BT9_110` | `DCGO/Assets/Scripts/CardEffect/BT9/White/BT9_110.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT9-101#1899@base` | `BT9-101` | 1899 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Option/BT9_101.asset` |
| `BT9-102#1900@base` | `BT9-102` | 1900 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Option/BT9_102.asset` |
| `BT9-102#9016@P1` | `BT9-102` | 9016 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Option/BT9_102_P1.asset` |
| `BT9-103#1901@base` | `BT9-103` | 1901 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Option/BT9_103.asset` |
| `BT9-103#9017@P1` | `BT9-103` | 9017 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Option/BT9_103_P1.asset` |
| `BT9-103#9018@P2` | `BT9-103` | 9018 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Option/BT9_103_P2.asset` |
| `BT9-104#1902@base` | `BT9-104` | 1902 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Option/BT9_104.asset` |
| `BT9-105#1903@base` | `BT9-105` | 1903 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Option/BT9_105.asset` |
| `BT9-105#9019@P0` | `BT9-105` | 9019 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Option/BT9_105_P0.asset` |
| `BT9-106#1904@base` | `BT9-106` | 1904 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Option/BT9_106.asset` |
| `BT9-107#1905@base` | `BT9-107` | 1905 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Option/BT9_107.asset` |
| `BT9-107#9020@P0` | `BT9-107` | 9020 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Option/BT9_107_P0.asset` |
| `BT9-108#1906@base` | `BT9-108` | 1906 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Option/BT9_108.asset` |
| `BT9-109#1907@base` | `BT9-109` | 1907 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Option/BT9_109.asset` |
| `BT9-109#1908@P1` | `BT9-109` | 1908 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Option/BT9_109_P1.asset` |
| `BT9-109#9021@P0` | `BT9-109` | 9021 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Option/BT9_109_P0.asset` |
| `BT9-109#9022@P2` | `BT9-109` | 9022 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Option/BT9_109_P2.asset` |
| `BT9-109#9023@P3` | `BT9-109` | 9023 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Option/BT9_109_P3.asset` |
| `BT9-110#1909@base` | `BT9-110` | 1909 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Option/BT9_110.asset` |
| `BT9-110#9024@P0` | `BT9-110` | 9024 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Option/BT9_110_P0.asset` |

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
