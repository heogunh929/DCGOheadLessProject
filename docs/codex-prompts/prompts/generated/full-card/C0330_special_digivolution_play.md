# C0330_special_digivolution_play - special digivolution/play mechanics card porting 95

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0330_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT5_095` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_095.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT5_096` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_096.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 1 |
| `BT5_097` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_097.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT5_098` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_098.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `BT5_099` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_099.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT5_100` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_100.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `BT5_101` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_101.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT5_102` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_102.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress, SelectBurstDigivolution` | 2 |
| `BT5_103` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_103.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `BT5_104` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_104.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT5-095#1076@base` | `BT5-095` | 1076 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Option/BT5_095.asset` |
| `BT5-095#8642@P0` | `BT5-095` | 8642 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Option/BT5_095_P0.asset` |
| `BT5-096#1077@base` | `BT5-096` | 1077 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Option/BT5_096.asset` |
| `BT5-097#1078@base` | `BT5-097` | 1078 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Option/BT5_097.asset` |
| `BT5-097#8643@P0` | `BT5-097` | 8643 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Option/BT5_097_P0.asset` |
| `BT5-097#8644@P1` | `BT5-097` | 8644 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Option/BT5_097_P1.asset` |
| `BT5-098#1079@base` | `BT5-098` | 1079 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Option/BT5_098.asset` |
| `BT5-099#1080@base` | `BT5-099` | 1080 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Option/BT5_099.asset` |
| `BT5-099#8645@P0` | `BT5-099` | 8645 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Option/BT5_099_P0.asset` |
| `BT5-100#1081@base` | `BT5-100` | 1081 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Option/BT5_100.asset` |
| `BT5-101#1082@base` | `BT5-101` | 1082 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Option/BT5_101.asset` |
| `BT5-101#8646@P0` | `BT5-101` | 8646 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Option/BT5_101_P0.asset` |
| `BT5-102#1083@base` | `BT5-102` | 1083 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Option/BT5_102.asset` |
| `BT5-102#8647@P0` | `BT5-102` | 8647 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Option/BT5_102_P0.asset` |
| `BT5-103#1084@base` | `BT5-103` | 1084 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Option/BT5_103.asset` |
| `BT5-103#8648@P0` | `BT5-103` | 8648 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Option/BT5_103_P0.asset` |
| `BT5-104#1085@base` | `BT5-104` | 1085 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Option/BT5_104.asset` |
| `BT5-104#8649@P0` | `BT5-104` | 8649 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Option/BT5_104_P0.asset` |

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
