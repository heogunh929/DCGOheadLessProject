# C0253_special_digivolution_play - special digivolution/play mechanics card porting 18

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0253_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 25
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT12_109` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_109.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT12_110` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_110.cs` | `OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT12_111` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_111.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 3 |
| `BT12_112` | `DCGO/Assets/Scripts/CardEffect/BT12/White/BT12_112.cs` | `BeforePayCost, None, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectSecurity, SelectJogress, SelectDigiXros` | 4 |
| `BT13_008` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_008.cs` | `None, OnDeclaration, OnTappedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT13_009` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_009.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT13_010` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_010.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT13_012` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_012.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT13_013` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_013.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT13_015` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_015.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT12-109#2536@base` | `BT12-109` | 2536 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Option/BT12_109.asset` |
| `BT12-109#4551@P0` | `BT12-109` | 4551 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Option/BT12_109_P0.asset` |
| `BT12-110#2537@base` | `BT12-110` | 2537 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Option/BT12_110.asset` |
| `BT12-110#4552@P0` | `BT12-110` | 4552 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Option/BT12_110_P0.asset` |
| `BT12-111#2538@base` | `BT12-111` | 2538 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_111.asset` |
| `BT12-111#2539@P1` | `BT12-111` | 2539 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_111_P1.asset` |
| `BT12-111#2540@P2` | `BT12-111` | 2540 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_111_P2.asset` |
| `BT12-112#2541@base` | `BT12-112` | 2541 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/White/Digimon/BT12_112.asset` |
| `BT12-112#2542@P1` | `BT12-112` | 2542 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/White/Digimon/BT12_112_P1.asset` |
| `BT12-112#2543@P2` | `BT12-112` | 2543 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/White/Digimon/BT12_112_P2.asset` |
| `BT12-112#8138@P3` | `BT12-112` | 8138 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/White/Digimon/BT12_112_P3.asset` |
| `BT13-008#2651@base` | `BT13-008` | 2651 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_008.asset` |
| `BT13-008#4559@P1` | `BT13-008` | 4559 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_008_P1.asset` |
| `BT13-008#8140@P2` | `BT13-008` | 8140 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_008_P2.asset` |
| `BT13-009#2652@base` | `BT13-009` | 2652 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_009.asset` |
| `BT13-010#2653@base` | `BT13-010` | 2653 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_010.asset` |
| `BT13-010#4560@P0` | `BT13-010` | 4560 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_010_P0.asset` |
| `BT13-012#2655@base` | `BT13-012` | 2655 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_012.asset` |
| `BT13-012#4561@P1` | `BT13-012` | 4561 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_012_P1.asset` |
| `BT13-012#8141@P2` | `BT13-012` | 8141 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_012_P2.asset` |
| `BT13-013#2656@base` | `BT13-013` | 2656 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_013.asset` |
| `BT13-015#2658@base` | `BT13-015` | 2658 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_015.asset` |
| `BT13-015#4563@P1` | `BT13-015` | 4563 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_015_P1.asset` |
| `BT13-015#8142@P2` | `BT13-015` | 8142 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_015_P2.asset` |
| `BT13-015#8143@P3` | `BT13-015` | 8143 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_015_P3.asset` |

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
