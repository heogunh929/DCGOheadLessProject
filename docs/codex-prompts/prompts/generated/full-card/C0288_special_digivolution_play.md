# C0288_special_digivolution_play - special digivolution/play mechanics card porting 53

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0288_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 11
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT1_097` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_097.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectJogress` | 1 |
| `BT1_098` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_098.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT1_099` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_099.cs` | `OptionSkill` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT1_100` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_100.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT1_101` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_101.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectSecurity, SelectJogress` | 2 |
| `BT1_102` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_102.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 1 |
| `BT1_103` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_103.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT1_104` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_104.cs` | `None, OnAllyAttack, OptionSkill` | `max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT1_105` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_105.cs` | `OptionSkill` | `max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT1_106` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_106.cs` | `OptionSkill` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-097#291@base` | `BT1-097` | 291 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Option/BT1_097.asset` |
| `BT1-098#292@base` | `BT1-098` | 292 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Option/BT1_098.asset` |
| `BT1-099#293@base` | `BT1-099` | 293 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Option/BT1_099.asset` |
| `BT1-100#294@base` | `BT1-100` | 294 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Option/BT1_100.asset` |
| `BT1-101#295@base` | `BT1-101` | 295 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Option/BT1_101.asset` |
| `BT1-101#296@P1` | `BT1-101` | 296 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Option/BT1_101_P1.asset` |
| `BT1-102#297@base` | `BT1-102` | 297 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Option/BT1_102.asset` |
| `BT1-103#298@base` | `BT1-103` | 298 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Option/BT1_103.asset` |
| `BT1-104#299@base` | `BT1-104` | 299 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Option/BT1_104.asset` |
| `BT1-105#300@base` | `BT1-105` | 300 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Option/BT1_105.asset` |
| `BT1-106#301@base` | `BT1-106` | 301 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Option/BT1_106.asset` |

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
