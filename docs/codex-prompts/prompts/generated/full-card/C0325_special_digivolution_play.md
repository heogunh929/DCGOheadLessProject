# C0325_special_digivolution_play - special digivolution/play mechanics card porting 90

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0325_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 26
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT4_093` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_093.cs` | `OnDeclaration, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT4_095` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_095.cs` | `BeforePayCost, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectBurstDigivolution` | 3 |
| `BT4_098` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_098.cs` | `OnBlockAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT4_099` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_099.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT4_100` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_100.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT4_101` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_101.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `BT4_102` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_102.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT4_103` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_103.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT4_104` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_104.cs` | `OptionSkill` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 5 |
| `BT4_105` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_105.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT4-093#8544@P0` | `BT4-093` | 8544 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Tamer/BT4_093_P0.asset` |
| `BT4-093#894@base` | `BT4-093` | 894 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Tamer/BT4_093.asset` |
| `BT4-093#895@P1` | `BT4-093` | 895 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Tamer/BT4_093_P1.asset` |
| `BT4-095#8546@P0` | `BT4-095` | 8546 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Tamer/BT4_095_P0.asset` |
| `BT4-095#898@base` | `BT4-095` | 898 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Tamer/BT4_095.asset` |
| `BT4-095#899@P1` | `BT4-095` | 899 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Tamer/BT4_095_P1.asset` |
| `BT4-098#8551@P1` | `BT4-098` | 8551 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Option/BT4_098_P1.asset` |
| `BT4-098#905@base` | `BT4-098` | 905 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Option/BT4_098.asset` |
| `BT4-099#8552@P0` | `BT4-099` | 8552 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Option/BT4_099_P0.asset` |
| `BT4-099#906@base` | `BT4-099` | 906 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Option/BT4_099.asset` |
| `BT4-099#907@P1` | `BT4-099` | 907 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Option/BT4_099_P1.asset` |
| `BT4-100#8553@P0` | `BT4-100` | 8553 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Option/BT4_100_P0.asset` |
| `BT4-100#908@base` | `BT4-100` | 908 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Option/BT4_100.asset` |
| `BT4-101#8554@P0` | `BT4-101` | 8554 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Option/BT4_101_P0.asset` |
| `BT4-101#909@base` | `BT4-101` | 909 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Option/BT4_101.asset` |
| `BT4-102#910@base` | `BT4-102` | 910 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Option/BT4_102.asset` |
| `BT4-103#8555@P0` | `BT4-103` | 8555 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Option/BT4_103_P0.asset` |
| `BT4-103#911@base` | `BT4-103` | 911 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Option/BT4_103.asset` |
| `BT4-104#3288@P1` | `BT4-104` | 3288 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Option/BT4_104_P1.asset` |
| `BT4-104#8556@P0` | `BT4-104` | 8556 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Option/BT4_104_P0.asset` |
| `BT4-104#8557@P2` | `BT4-104` | 8557 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Option/BT4_104_P2.asset` |
| `BT4-104#8558@P02` | `BT4-104` | 8558 | `P02` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Option/BT4_104_P02.asset` |
| `BT4-104#912@base` | `BT4-104` | 912 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Option/BT4_104.asset` |
| `BT4-105#8559@P0` | `BT4-105` | 8559 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Option/BT4_105_P0.asset` |
| `BT4-105#913@base` | `BT4-105` | 913 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Option/BT4_105.asset` |
| `BT4-105#914@P1` | `BT4-105` | 914 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Option/BT4_105_P1.asset` |

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
