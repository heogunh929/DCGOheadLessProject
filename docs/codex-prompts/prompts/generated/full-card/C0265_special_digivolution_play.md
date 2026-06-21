# C0265_special_digivolution_play - special digivolution/play mechanics card porting 30

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0265_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT15_089` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_089.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT15_090` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_090.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT15_091` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_091.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectOrder, SelectSecurity, SelectJogress` | 2 |
| `BT15_092` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_092.cs` | `OnDiscardSecurity, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT15_093` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_093.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 1 |
| `BT15_094` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_094.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT15_095` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_095.cs` | `OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT15_096` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_096.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `BT15_098` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_098.cs` | `OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT15_099` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_099.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT15-089#3230@base` | `BT15-089` | 3230 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Option/BT15_089.asset` |
| `BT15-090#3231@base` | `BT15-090` | 3231 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Option/BT15_090.asset` |
| `BT15-091#3232@base` | `BT15-091` | 3232 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Option/BT15_091.asset` |
| `BT15-091#4767@P0` | `BT15-091` | 4767 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Option/BT15_091_P0.asset` |
| `BT15-092#3233@base` | `BT15-092` | 3233 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Option/BT15_092.asset` |
| `BT15-092#4768@P0` | `BT15-092` | 4768 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Option/BT15_092_P0.asset` |
| `BT15-093#3234@base` | `BT15-093` | 3234 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Option/BT15_093.asset` |
| `BT15-094#3235@base` | `BT15-094` | 3235 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Option/BT15_094.asset` |
| `BT15-095#3236@base` | `BT15-095` | 3236 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Option/BT15_095.asset` |
| `BT15-095#4769@P0` | `BT15-095` | 4769 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Option/BT15_095_P0.asset` |
| `BT15-096#3237@base` | `BT15-096` | 3237 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Option/BT15_096.asset` |
| `BT15-096#4770@P0` | `BT15-096` | 4770 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Option/BT15_096_P0.asset` |
| `BT15-098#3239@base` | `BT15-098` | 3239 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Option/BT15_098.asset` |
| `BT15-098#4771@P0` | `BT15-098` | 4771 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Option/BT15_098_P0.asset` |
| `BT15-099#3240@base` | `BT15-099` | 3240 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Option/BT15_099.asset` |

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
