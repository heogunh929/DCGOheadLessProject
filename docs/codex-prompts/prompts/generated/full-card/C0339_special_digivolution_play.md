# C0339_special_digivolution_play - special digivolution/play mechanics card porting 104

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0339_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 29
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT7_106` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_106.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT7_107` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_107.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT7_108` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_108.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT7_109` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_109.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `BT7_110` | `DCGO/Assets/Scripts/CardEffect/BT7/White/BT7_110.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 6 |
| `BT7_111` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_111.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `BT7_112` | `DCGO/Assets/Scripts/CardEffect/BT7/White/BT7_112.cs` | `BeforePayCost, None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectOrder, SelectJogress` | 4 |
| `BT8_015` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_015.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT8_016` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_016.cs` | `None` | `inherited, modifier_duration, static_or_continuous` | `SelectJogress` | 1 |
| `BT8_020` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_020.cs` | `OnEndTurn` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT7-106#1541@base` | `BT7-106` | 1541 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Option/BT7_106.asset` |
| `BT7-106#1542@P1` | `BT7-106` | 1542 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Option/BT7_106_P1.asset` |
| `BT7-107#1543@base` | `BT7-107` | 1543 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Option/BT7_107.asset` |
| `BT7-107#8833@P0` | `BT7-107` | 8833 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Option/BT7_107_P0.asset` |
| `BT7-108#1544@base` | `BT7-108` | 1544 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Option/BT7_108.asset` |
| `BT7-109#1545@base` | `BT7-109` | 1545 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Option/BT7_109.asset` |
| `BT7-109#8834@P1` | `BT7-109` | 8834 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Option/BT7_109_P1.asset` |
| `BT7-109#8835@P2` | `BT7-109` | 8835 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Option/BT7_109_P2.asset` |
| `BT7-110#1546@base` | `BT7-110` | 1546 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Option/BT7_110.asset` |
| `BT7-110#1547@P1` | `BT7-110` | 1547 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Option/BT7_110_P1.asset` |
| `BT7-110#1548@P2` | `BT7-110` | 1548 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Option/BT7_110_P2.asset` |
| `BT7-110#1549@P3` | `BT7-110` | 1549 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Option/BT7_110_P3.asset` |
| `BT7-110#1550@P4` | `BT7-110` | 1550 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Option/BT7_110_P4.asset` |
| `BT7-110#8836@P0` | `BT7-110` | 8836 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Option/BT7_110_P0.asset` |
| `BT7-111#1551@base` | `BT7-111` | 1551 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_111.asset` |
| `BT7-111#1552@P1` | `BT7-111` | 1552 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_111_P1.asset` |
| `BT7-111#8837@P2` | `BT7-111` | 8837 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_111_P2.asset` |
| `BT7-112#1553@base` | `BT7-112` | 1553 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_112.asset` |
| `BT7-112#1554@P1` | `BT7-112` | 1554 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_112_P1.asset` |
| `BT7-112#8838@P2` | `BT7-112` | 8838 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_112_P2.asset` |
| `BT7-112#8839@P3` | `BT7-112` | 8839 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_112_P3.asset` |
| `BT8-015#1577@base` | `BT8-015` | 1577 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_015.asset` |
| `BT8-015#8852@P0` | `BT8-015` | 8852 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_015_P0.asset` |
| `BT8-015#8853@P1` | `BT8-015` | 8853 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_015_P1.asset` |
| `BT8-016#1578@base` | `BT8-016` | 1578 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_016.asset` |
| `BT8-020#1582@base` | `BT8-020` | 1582 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_020.asset` |
| `ST10-02#1757@base` | `ST10-02` | 1757 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Digimon/ST10_02.asset` |
| `ST9-08#1746@base` | `ST9-08` | 1746 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Green/Digimon/ST9_08.asset` |
| `ST9-08#5001@P1` | `ST9-08` | 5001 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Green/Digimon/ST9_08_P1.asset` |

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
