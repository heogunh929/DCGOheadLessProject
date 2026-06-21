# C0241_special_digivolution_play - special digivolution/play mechanics card porting 6

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0241_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT10_092` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_092.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone, SecuritySkill` | `inherited, max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 4 |
| `BT10_093` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_093.cs` | `BeforePayCost, None, OnAddDigivolutionCards, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectDigiXros` | 4 |
| `BT10_094` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_094.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT10_095` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_095.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 2 |
| `BT10_096` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_096.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectSecurity, SelectJogress` | 1 |
| `BT10_097` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_097.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `BT10_098` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_098.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT10_099` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_099.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT10_100` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_100.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `BT10_101` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_101.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT10-092#2146@base` | `BT10-092` | 2146 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Tamer/BT10_092.asset` |
| `BT10-092#2147@P1` | `BT10-092` | 2147 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Tamer/BT10_092_P1.asset` |
| `BT10-092#4349@P0` | `BT10-092` | 4349 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Tamer/BT10_092_P0.asset` |
| `BT10-092#4350@P2` | `BT10-092` | 4350 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Tamer/BT10_092_P2.asset` |
| `BT10-093#2148@base` | `BT10-093` | 2148 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093.asset` |
| `BT10-093#2149@P1` | `BT10-093` | 2149 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093_P1.asset` |
| `BT10-093#4351@P0` | `BT10-093` | 4351 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093_P0.asset` |
| `BT10-093#8103@P2` | `BT10-093` | 8103 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093_P2.asset` |
| `BT10-094#2150@base` | `BT10-094` | 2150 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Option/BT10_094.asset` |
| `BT10-094#4352@P0` | `BT10-094` | 4352 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Option/BT10_094_P0.asset` |
| `BT10-095#2151@base` | `BT10-095` | 2151 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Option/BT10_095.asset` |
| `BT10-095#4353@P0` | `BT10-095` | 4353 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Option/BT10_095_P0.asset` |
| `BT10-096#2152@base` | `BT10-096` | 2152 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Option/BT10_096.asset` |
| `BT10-097#2153@base` | `BT10-097` | 2153 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Option/BT10_097.asset` |
| `BT10-097#4354@P0` | `BT10-097` | 4354 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Option/BT10_097_P0.asset` |
| `BT10-097#4355@P1` | `BT10-097` | 4355 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Option/BT10_097_P1.asset` |
| `BT10-098#2154@base` | `BT10-098` | 2154 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Option/BT10_098.asset` |
| `BT10-099#2155@base` | `BT10-099` | 2155 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Option/BT10_099.asset` |
| `BT10-100#2156@base` | `BT10-100` | 2156 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Option/BT10_100.asset` |
| `BT10-100#4356@P1` | `BT10-100` | 4356 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Option/BT10_100_P1.asset` |
| `BT10-101#2157@base` | `BT10-101` | 2157 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Option/BT10_101.asset` |
| `BT10-101#4357@P0` | `BT10-101` | 4357 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Option/BT10_101_P0.asset` |
| `BT10-101#4358@P1` | `BT10-101` | 4358 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Option/BT10_101_P1.asset` |

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
