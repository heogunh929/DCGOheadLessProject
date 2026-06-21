# C0329_special_digivolution_play - special digivolution/play mechanics card porting 94

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0329_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 26
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT5_070` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_070.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectBurstDigivolution` | 1 |
| `BT5_072` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_072.cs` | `OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT5_074` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_074.cs` | `OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT5_079` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_079.cs` | `OnAllyAttack, OnDeclaration` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectBurstDigivolution` | 2 |
| `BT5_083` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_083.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 3 |
| `BT5_085` | `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_085.cs` | `BeforePayCost, None` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `BT5_090` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_090.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `BT5_092` | `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_092.cs` | `BeforePayCost, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 6 |
| `BT5_093` | `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_093.cs` | `None, OnStartTurn, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `BT5_094` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_094.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT5-070#1030@base` | `BT5-070` | 1030 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_070.asset` |
| `BT5-072#1035@base` | `BT5-072` | 1035 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_072.asset` |
| `BT5-072#8617@P0` | `BT5-072` | 8617 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_072_P0.asset` |
| `BT5-072#8618@P1` | `BT5-072` | 8618 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_072_P1.asset` |
| `BT5-074#1037@base` | `BT5-074` | 1037 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_074.asset` |
| `BT5-079#1042@base` | `BT5-079` | 1042 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_079.asset` |
| `BT5-079#8619@P0` | `BT5-079` | 8619 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_079_P0.asset` |
| `BT5-083#1048@base` | `BT5-083` | 1048 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_083.asset` |
| `BT5-083#1049@P1` | `BT5-083` | 1049 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_083_P1.asset` |
| `BT5-083#8621@P0` | `BT5-083` | 8621 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_083_P0.asset` |
| `BT5-085#1053@base` | `BT5-085` | 1053 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_085.asset` |
| `BT5-085#1054@P1` | `BT5-085` | 1054 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_085_P1.asset` |
| `BT5-085#8623@P2` | `BT5-085` | 8623 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_085_P2.asset` |
| `BT5-090#1067@base` | `BT5-090` | 1067 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Tamer/BT5_090.asset` |
| `BT5-090#1068@P1` | `BT5-090` | 1068 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Tamer/BT5_090_P1.asset` |
| `BT5-090#8635@P0` | `BT5-090` | 8635 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Tamer/BT5_090_P0.asset` |
| `BT5-092#1071@base` | `BT5-092` | 1071 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_092.asset` |
| `BT5-092#1072@P1` | `BT5-092` | 1072 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_092_P1.asset` |
| `BT5-092#8637@P0` | `BT5-092` | 8637 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_092_P0.asset` |
| `BT5-092#8638@P2` | `BT5-092` | 8638 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_092_P2.asset` |
| `BT5-092#8639@P3` | `BT5-092` | 8639 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_092_P3.asset` |
| `BT5-092#8640@P4` | `BT5-092` | 8640 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_092_P4.asset` |
| `BT5-093#1073@base` | `BT5-093` | 1073 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_093.asset` |
| `BT5-093#1074@P1` | `BT5-093` | 1074 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_093_P1.asset` |
| `BT5-093#8641@P0` | `BT5-093` | 8641 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_093_P0.asset` |
| `BT5-094#1075@base` | `BT5-094` | 1075 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Option/BT5_094.asset` |

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
