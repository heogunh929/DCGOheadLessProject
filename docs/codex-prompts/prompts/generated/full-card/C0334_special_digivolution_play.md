# C0334_special_digivolution_play - special digivolution/play mechanics card porting 99

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0334_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT6_098` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_098.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT6_099` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_099.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT6_100` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_100.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT6_101` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_101.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT6_102` | `DCGO/Assets/Scripts/CardEffect/BT6/Green/BT6_102.cs` | `OnDestroyedAnyone, OptionSkill` | `max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT6_103` | `DCGO/Assets/Scripts/CardEffect/BT6/Green/BT6_103.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT6_104` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_104.cs` | `OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT6_105` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_105.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `BT6_106` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_106.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `BT6_107` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_107.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT6-098#1241@base` | `BT6-098` | 1241 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Option/BT6_098.asset` |
| `BT6-098#8731@P0` | `BT6-098` | 8731 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Option/BT6_098_P0.asset` |
| `BT6-099#1242@base` | `BT6-099` | 1242 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Option/BT6_099.asset` |
| `BT6-099#8732@P0` | `BT6-099` | 8732 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Option/BT6_099_P0.asset` |
| `BT6-100#1243@base` | `BT6-100` | 1243 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Option/BT6_100.asset` |
| `BT6-100#8733@P1` | `BT6-100` | 8733 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Option/BT6_100_P1.asset` |
| `BT6-101#1244@base` | `BT6-101` | 1244 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Option/BT6_101.asset` |
| `BT6-101#8734@P0` | `BT6-101` | 8734 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Option/BT6_101_P0.asset` |
| `BT6-102#1245@base` | `BT6-102` | 1245 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Option/BT6_102.asset` |
| `BT6-103#1246@base` | `BT6-103` | 1246 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Option/BT6_103.asset` |
| `BT6-103#8735@P0` | `BT6-103` | 8735 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Option/BT6_103_P0.asset` |
| `BT6-104#1247@base` | `BT6-104` | 1247 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Option/BT6_104.asset` |
| `BT6-105#1248@base` | `BT6-105` | 1248 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Option/BT6_105.asset` |
| `BT6-105#8736@P0` | `BT6-105` | 8736 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Option/BT6_105_P0.asset` |
| `BT6-106#1249@base` | `BT6-106` | 1249 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Option/BT6_106.asset` |
| `BT6-106#8737@P0` | `BT6-106` | 8737 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Option/BT6_106_P0.asset` |
| `BT6-107#1250@base` | `BT6-107` | 1250 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Option/BT6_107.asset` |
| `BT6-107#8738@P1` | `BT6-107` | 8738 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Option/BT6_107_P1.asset` |

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
