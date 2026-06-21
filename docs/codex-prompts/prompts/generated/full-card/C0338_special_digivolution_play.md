# C0338_special_digivolution_play - special digivolution/play mechanics card porting 103

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0338_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT7_095` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_095.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `BT7_096` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_096.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT7_097` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_097.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT7_098` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_098.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT7_099` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_099.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT7_100` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_100.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT7_101` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_101.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT7_102` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_102.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT7_104` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_104.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT7_105` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_105.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT7-095#1530@base` | `BT7-095` | 1530 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Option/BT7_095.asset` |
| `BT7-096#1531@base` | `BT7-096` | 1531 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Option/BT7_096.asset` |
| `BT7-096#8821@P0` | `BT7-096` | 8821 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Option/BT7_096_P0.asset` |
| `BT7-097#1532@base` | `BT7-097` | 1532 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Option/BT7_097.asset` |
| `BT7-098#1533@base` | `BT7-098` | 1533 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Option/BT7_098.asset` |
| `BT7-099#1534@base` | `BT7-099` | 1534 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Option/BT7_099.asset` |
| `BT7-099#8822@P0` | `BT7-099` | 8822 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Option/BT7_099_P0.asset` |
| `BT7-100#1535@base` | `BT7-100` | 1535 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Option/BT7_100.asset` |
| `BT7-100#8823@P0` | `BT7-100` | 8823 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Option/BT7_100_P0.asset` |
| `BT7-100#8824@P1` | `BT7-100` | 8824 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Option/BT7_100_P1.asset` |
| `BT7-101#1536@base` | `BT7-101` | 1536 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Option/BT7_101.asset` |
| `BT7-102#1537@base` | `BT7-102` | 1537 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Option/BT7_102.asset` |
| `BT7-102#8825@P1` | `BT7-102` | 8825 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Option/BT7_102_P1.asset` |
| `BT7-104#1539@base` | `BT7-104` | 1539 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Option/BT7_104.asset` |
| `BT7-104#8829@P0` | `BT7-104` | 8829 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Option/BT7_104_P0.asset` |
| `BT7-105#1540@base` | `BT7-105` | 1540 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Option/BT7_105.asset` |
| `BT7-105#3289@P1` | `BT7-105` | 3289 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Option/BT7_105_P1.asset` |
| `BT7-105#8830@P0` | `BT7-105` | 8830 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Option/BT7_105_P0.asset` |
| `BT7-105#8831@P2` | `BT7-105` | 8831 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Option/BT7_105_P2.asset` |
| `BT7-105#8832@P02` | `BT7-105` | 8832 | `P02` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Option/BT7_105_P02.asset` |

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
