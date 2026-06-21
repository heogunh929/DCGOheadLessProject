# C0287_special_digivolution_play - special digivolution/play mechanics card porting 52

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0287_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT1_011` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_011.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT1_056` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_056.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 1 |
| `BT1_084` | `DCGO/Assets/Scripts/CardEffect/BT1/White/BT1_084.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 6 |
| `BT1_090` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_090.cs` | `OnEndTurn, OptionSkill` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `BT1_091` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_091.cs` | `OptionSkill` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT1_092` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_092.cs` | `OptionSkill` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT1_093` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_093.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT1_094` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_094.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT1_095` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_095.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT1_096` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_096.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-011#149@base` | `BT1-011` | 149 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_011.asset` |
| `BT1-011#4261@P1` | `BT1-011` | 4261 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_011_P1.asset` |
| `BT1-056#213@base` | `BT1-056` | 213 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_056.asset` |
| `BT1-084#263@base` | `BT1-084` | 263 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/White/Digimon/BT1_084.asset` |
| `BT1-084#264@P1` | `BT1-084` | 264 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/White/Digimon/BT1_084_P1.asset` |
| `BT1-084#265@P2` | `BT1-084` | 265 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/White/Digimon/BT1_084_P2.asset` |
| `BT1-084#266@P3` | `BT1-084` | 266 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/White/Digimon/BT1_084_P3.asset` |
| `BT1-084#267@P4` | `BT1-084` | 267 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT1/White/Digimon/BT1_084_P4.asset` |
| `BT1-084#268@P5` | `BT1-084` | 268 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT1/White/Digimon/BT1_084_P5.asset` |
| `BT1-090#283@base` | `BT1-090` | 283 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Option/BT1_090.asset` |
| `BT1-090#4277@P1` | `BT1-090` | 4277 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Option/BT1_090_P1.asset` |
| `BT1-091#284@base` | `BT1-091` | 284 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Option/BT1_091.asset` |
| `BT1-092#285@base` | `BT1-092` | 285 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Option/BT1_092.asset` |
| `BT1-093#286@base` | `BT1-093` | 286 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Option/BT1_093.asset` |
| `BT1-094#287@base` | `BT1-094` | 287 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Option/BT1_094.asset` |
| `BT1-095#288@base` | `BT1-095` | 288 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Option/BT1_095.asset` |
| `BT1-095#289@P1` | `BT1-095` | 289 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Option/BT1_095_P1.asset` |
| `BT1-096#290@base` | `BT1-096` | 290 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Option/BT1_096.asset` |

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
