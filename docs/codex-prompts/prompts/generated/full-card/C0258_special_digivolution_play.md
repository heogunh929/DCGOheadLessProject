# C0258_special_digivolution_play - special digivolution/play mechanics card porting 23

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0258_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 29
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT13_101` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_101.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 3 |
| `BT13_103` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_103.cs` | `BeforePayCost, None, OnEndTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT13_104` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_104.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT13_105` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_105.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT13_106` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_106.cs` | `OnDiscardSecurity, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT13_107` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_107.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT13_108` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_108.cs` | `OnTappedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT13_109` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_109.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT13_110` | `DCGO/Assets/Scripts/CardEffect/BT13/White/BT13_110.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 5 |
| `BT13_112` | `DCGO/Assets/Scripts/CardEffect/BT13/White/BT13_112.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 6 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT13-101#2769@base` | `BT13-101` | 2769 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Tamer/BT13_101.asset` |
| `BT13-101#2770@P1` | `BT13-101` | 2770 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Tamer/BT13_101_P1.asset` |
| `BT13-101#4621@P0` | `BT13-101` | 4621 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Tamer/BT13_101_P0.asset` |
| `BT13-103#2773@base` | `BT13-103` | 2773 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Tamer/BT13_103.asset` |
| `BT13-103#4623@P0` | `BT13-103` | 4623 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Tamer/BT13_103_P0.asset` |
| `BT13-104#2774@base` | `BT13-104` | 2774 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Option/BT13_104.asset` |
| `BT13-104#4624@P0` | `BT13-104` | 4624 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Option/BT13_104_P0.asset` |
| `BT13-104#8154@P1` | `BT13-104` | 8154 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Option/BT13_104_P1.asset` |
| `BT13-105#2775@base` | `BT13-105` | 2775 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Option/BT13_105.asset` |
| `BT13-105#4625@P0` | `BT13-105` | 4625 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Option/BT13_105_P0.asset` |
| `BT13-106#2776@base` | `BT13-106` | 2776 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Option/BT13_106.asset` |
| `BT13-106#4626@P0` | `BT13-106` | 4626 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Option/BT13_106_P0.asset` |
| `BT13-107#2777@base` | `BT13-107` | 2777 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Option/BT13_107.asset` |
| `BT13-107#4627@P0` | `BT13-107` | 4627 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Option/BT13_107_P0.asset` |
| `BT13-108#2778@base` | `BT13-108` | 2778 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Option/BT13_108.asset` |
| `BT13-108#4628@P0` | `BT13-108` | 4628 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Option/BT13_108_P0.asset` |
| `BT13-109#2779@base` | `BT13-109` | 2779 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Option/BT13_109.asset` |
| `BT13-109#4629@P0` | `BT13-109` | 4629 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Option/BT13_109_P0.asset` |
| `BT13-110#2780@base` | `BT13-110` | 2780 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Option/BT13_110.asset` |
| `BT13-110#4630@P0` | `BT13-110` | 4630 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Option/BT13_110_P0.asset` |
| `BT13-110#4631@P1` | `BT13-110` | 4631 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Option/BT13_110_P1.asset` |
| `BT13-110#8155@P2` | `BT13-110` | 8155 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Option/BT13_110_P2.asset` |
| `BT13-110#8156@P3` | `BT13-110` | 8156 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Option/BT13_110_P3.asset` |
| `BT13-112#2783@base` | `BT13-112` | 2783 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Digimon/BT13_112.asset` |
| `BT13-112#2784@P1` | `BT13-112` | 2784 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Digimon/BT13_112_P1.asset` |
| `BT13-112#8160@P2` | `BT13-112` | 8160 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Digimon/BT13_112_P2.asset` |
| `BT13-112#8161@P3` | `BT13-112` | 8161 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Digimon/BT13_112_P3.asset` |
| `BT13-112#8162@P4` | `BT13-112` | 8162 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Digimon/BT13_112_P4.asset` |
| `BT13-112#8163@P5` | `BT13-112` | 8163 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Digimon/BT13_112_P5.asset` |

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
