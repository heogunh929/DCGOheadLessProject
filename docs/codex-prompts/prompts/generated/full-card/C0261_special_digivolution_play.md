# C0261_special_digivolution_play - special digivolution/play mechanics card porting 26

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0261_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT14_090` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_090.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectOrder, SelectSecurity, SelectJogress` | 2 |
| `BT14_091` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_091.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT14_092` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_092.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT14_093` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_093.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT14_094` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_094.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 1 |
| `BT14_095` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_095.cs` | `OnTappedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT14_097` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_097.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT14_098` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_098.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT14_099` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_099.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT14_100` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_100.cs` | `OnDiscardHand, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT14-090#3025@base` | `BT14-090` | 3025 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Option/BT14_090.asset` |
| `BT14-090#4702@P0` | `BT14-090` | 4702 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Option/BT14_090_P0.asset` |
| `BT14-091#3026@base` | `BT14-091` | 3026 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Option/BT14_091.asset` |
| `BT14-091#4703@P0` | `BT14-091` | 4703 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Option/BT14_091_P0.asset` |
| `BT14-092#3027@base` | `BT14-092` | 3027 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Option/BT14_092.asset` |
| `BT14-093#3028@base` | `BT14-093` | 3028 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Option/BT14_093.asset` |
| `BT14-093#4704@P0` | `BT14-093` | 4704 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Option/BT14_093_P0.asset` |
| `BT14-094#3029@base` | `BT14-094` | 3029 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Option/BT14_094.asset` |
| `BT14-095#3030@base` | `BT14-095` | 3030 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Option/BT14_095.asset` |
| `BT14-097#3032@base` | `BT14-097` | 3032 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Option/BT14_097.asset` |
| `BT14-097#4706@P0` | `BT14-097` | 4706 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Option/BT14_097_P0.asset` |
| `BT14-098#3033@base` | `BT14-098` | 3033 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Option/BT14_098.asset` |
| `BT14-099#3034@base` | `BT14-099` | 3034 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Option/BT14_099.asset` |
| `BT14-099#4707@P0` | `BT14-099` | 4707 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Option/BT14_099_P0.asset` |
| `BT14-100#3035@base` | `BT14-100` | 3035 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Option/BT14_100.asset` |

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
