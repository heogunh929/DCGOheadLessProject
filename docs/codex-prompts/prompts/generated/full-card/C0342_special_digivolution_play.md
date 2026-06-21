# C0342_special_digivolution_play - special digivolution/play mechanics card porting 107

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0342_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT8_098` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_098.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT8_099` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_099.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectSecurity, SelectJogress` | 3 |
| `BT8_100` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_100.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT8_101` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_101.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT8_102` | `DCGO/Assets/Scripts/CardEffect/BT8/Green/BT8_102.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT8_103` | `DCGO/Assets/Scripts/CardEffect/BT8/Green/BT8_103.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT8_104` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_104.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT8_105` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_105.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT8_106` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_106.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT8_107` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_107.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT8-098#1691@base` | `BT8-098` | 1691 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Option/BT8_098.asset` |
| `BT8-098#8925@P0` | `BT8-098` | 8925 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Option/BT8_098_P0.asset` |
| `BT8-099#1692@base` | `BT8-099` | 1692 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Option/BT8_099.asset` |
| `BT8-099#8926@P0` | `BT8-099` | 8926 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Option/BT8_099_P0.asset` |
| `BT8-099#8927@P1` | `BT8-099` | 8927 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Option/BT8_099_P1.asset` |
| `BT8-100#1693@base` | `BT8-100` | 1693 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Option/BT8_100.asset` |
| `BT8-101#1694@base` | `BT8-101` | 1694 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Option/BT8_101.asset` |
| `BT8-101#8928@P0` | `BT8-101` | 8928 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Option/BT8_101_P0.asset` |
| `BT8-102#1695@base` | `BT8-102` | 1695 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Option/BT8_102.asset` |
| `BT8-102#8929@P0` | `BT8-102` | 8929 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Option/BT8_102_P0.asset` |
| `BT8-102#8930@P1` | `BT8-102` | 8930 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Option/BT8_102_P1.asset` |
| `BT8-103#1696@base` | `BT8-103` | 1696 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Option/BT8_103.asset` |
| `BT8-104#1697@base` | `BT8-104` | 1697 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Option/BT8_104.asset` |
| `BT8-104#8931@P0` | `BT8-104` | 8931 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Option/BT8_104_P0.asset` |
| `BT8-105#1698@base` | `BT8-105` | 1698 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Option/BT8_105.asset` |
| `BT8-105#8932@P0` | `BT8-105` | 8932 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Option/BT8_105_P0.asset` |
| `BT8-106#1699@base` | `BT8-106` | 1699 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Option/BT8_106.asset` |
| `BT8-107#1700@base` | `BT8-107` | 1700 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Option/BT8_107.asset` |
| `BT8-107#8933@P0` | `BT8-107` | 8933 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Option/BT8_107_P0.asset` |

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
