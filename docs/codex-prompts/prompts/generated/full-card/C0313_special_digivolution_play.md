# C0313_special_digivolution_play - special digivolution/play mechanics card porting 78

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0313_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT24_084` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_084.cs` | `OnLoseSecurity, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT24_086` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_086.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT24_087` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_087.cs` | `OnStartMainPhase, SecuritySkill, WhenLinked` | `linked, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectAppFusion` | 1 |
| `BT24_088` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_088.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `BT24_089` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_089.cs` | `OnTappedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT24_090` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_090.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectInteger, SelectSecurity, SelectJogress` | 2 |
| `BT24_091` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_091.cs` | `None, OnAllyAttack, OnDeclaration, OptionSkill, SecuritySkill` | `linked, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT24_092` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_092.cs` | `None, OnAllyAttack, OnDeclaration, OptionSkill, SecuritySkill` | `linked, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT24_093` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_093.cs` | `OnLoseSecurity, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT24_094` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_094.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectInteger, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT24-084#7622@base` | `BT24-084` | 7622 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Tamer/BT24_084.asset` |
| `BT24-084#7623@P1` | `BT24-084` | 7623 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Tamer/BT24_084_P1.asset` |
| `BT24-084#7624@P2` | `BT24-084` | 7624 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Tamer/BT24_084_P2.asset` |
| `BT24-086#7628@base` | `BT24-086` | 7628 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Tamer/BT24_086.asset` |
| `BT24-087#7629@base` | `BT24-087` | 7629 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Tamer/BT24_087.asset` |
| `BT24-088#7630@base` | `BT24-088` | 7630 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Tamer/BT24_088.asset` |
| `BT24-088#7631@P1` | `BT24-088` | 7631 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Tamer/BT24_088_P1.asset` |
| `BT24-089#7632@base` | `BT24-089` | 7632 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Option/BT24_089.asset` |
| `BT24-090#7633@base` | `BT24-090` | 7633 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Option/BT24_090.asset` |
| `BT24-090#8451@P1` | `BT24-090` | 8451 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Option/BT24_090_P1.asset` |
| `BT24-091#7634@base` | `BT24-091` | 7634 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Option/BT24_091.asset` |
| `BT24-092#7635@base` | `BT24-092` | 7635 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Option/BT24_092.asset` |
| `BT24-093#7636@base` | `BT24-093` | 7636 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Option/BT24_093.asset` |
| `BT24-093#8452@P1` | `BT24-093` | 8452 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Option/BT24_093_P1.asset` |
| `BT24-094#7637@base` | `BT24-094` | 7637 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Option/BT24_094.asset` |
| `BT24-094#8453@P1` | `BT24-094` | 8453 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Option/BT24_094_P1.asset` |

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
