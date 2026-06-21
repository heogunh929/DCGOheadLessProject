# C0318_special_digivolution_play - special digivolution/play mechanics card porting 83

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0318_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT25_103` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_103.cs` | `None, WhenRemoveField` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectJogress` | 3 |
| `BT2_006` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_006.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 2 |
| `BT2_053` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_053.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `BT2_059` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_059.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT2_062` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_062.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT2_086` | `DCGO/Assets/Scripts/CardEffect/BT2/Blue/BT2_086.cs` | `OnAllyAttack, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT2_088` | `DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_088.cs` | `BeforePayCost, None, OnDetermineDoSecurityCheck, SecuritySkill` | `inherited, max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT2_091` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_091.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT2_092` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_092.cs` | `OptionSkill` | `max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT2_093` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_093.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-006#369@base` | `BT2-006` | 369 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_006.asset` |
| `BT2-006#8304@P1` | `BT2-006` | 8304 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/DigiEgg/BT2_006_P1.asset` |
| `BT2-053#456@base` | `BT2-053` | 456 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_053.asset` |
| `BT2-053#457@P1` | `BT2-053` | 457 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_053_P1.asset` |
| `BT2-053#458@P2` | `BT2-053` | 458 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_053_P2.asset` |
| `BT2-059#470@base` | `BT2-059` | 470 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_059.asset` |
| `BT2-062#474@base` | `BT2-062` | 474 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_062.asset` |
| `BT2-086#526@base` | `BT2-086` | 526 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Tamer/BT2_086.asset` |
| `BT2-086#8321@P1` | `BT2-086` | 8321 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Tamer/BT2_086_P1.asset` |
| `BT2-088#530@base` | `BT2-088` | 530 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Tamer/BT2_088.asset` |
| `BT2-088#8322@P1` | `BT2-088` | 8322 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Tamer/BT2_088_P1.asset` |
| `BT2-091#537@base` | `BT2-091` | 537 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Option/BT2_091.asset` |
| `BT2-092#538@base` | `BT2-092` | 538 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Option/BT2_092.asset` |
| `BT2-093#539@base` | `BT2-093` | 539 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Option/BT2_093.asset` |
| `BT25-103#8089@base` | `BT25-103` | 8089 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_103.asset` |
| `BT25-103#8090@P1` | `BT25-103` | 8090 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_103_P1.asset` |
| `BT25-103#8091@P2` | `BT25-103` | 8091 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_103_P2.asset` |

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
