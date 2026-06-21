# C0256_special_digivolution_play - special digivolution/play mechanics card porting 21

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0256_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT13_070` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_070.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT13_073` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_073.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `SelectJogress` | 2 |
| `BT13_074` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_074.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT13_076` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_076.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT13_080` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_080.cs` | `BeforePayCost, None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectOrder, SelectJogress` | 1 |
| `BT13_083` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_083.cs` | `BeforePayCost, None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectOrder, SelectJogress` | 1 |
| `BT13_084` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_084.cs` | `OnDiscardHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT13_085` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_085.cs` | `OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT13_086` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_086.cs` | `BeforePayCost, None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT13_087` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_087.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT13-070#2727@base` | `BT13-070` | 2727 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_070.asset` |
| `BT13-073#2730@base` | `BT13-073` | 2730 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_073.asset` |
| `BT13-073#4597@P0` | `BT13-073` | 4597 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_073_P0.asset` |
| `BT13-074#2731@base` | `BT13-074` | 2731 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_074.asset` |
| `BT13-074#4598@P0` | `BT13-074` | 4598 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_074_P0.asset` |
| `BT13-076#2734@base` | `BT13-076` | 2734 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_076.asset` |
| `BT13-076#4601@P0` | `BT13-076` | 4601 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_076_P0.asset` |
| `BT13-080#2739@base` | `BT13-080` | 2739 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_080.asset` |
| `BT13-083#2742@base` | `BT13-083` | 2742 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_083.asset` |
| `BT13-084#2743@base` | `BT13-084` | 2743 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_084.asset` |
| `BT13-085#2744@base` | `BT13-085` | 2744 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_085.asset` |
| `BT13-086#2745@base` | `BT13-086` | 2745 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_086.asset` |
| `BT13-086#4603@P0` | `BT13-086` | 4603 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_086_P0.asset` |
| `BT13-087#2746@base` | `BT13-087` | 2746 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_087.asset` |
| `BT13-087#2747@P1` | `BT13-087` | 2747 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_087_P1.asset` |
| `BT13-087#4604@P0` | `BT13-087` | 4604 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_087_P0.asset` |
| `BT13-087#8149@P2` | `BT13-087` | 8149 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_087_P2.asset` |

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
