# C0309_special_digivolution_play - special digivolution/play mechanics card porting 74

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0309_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 14
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT23_098` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_098.cs` | `OnTappedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT23_099` | `DCGO/Assets/Scripts/CardEffect/BT23/White/BT23_099.cs` | `None, OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT23_101` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_101.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT24_006` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_006.cs` | `WhenLinked` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `BT24_009` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_009.cs` | `None, OnDiscardHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectJogress` | 1 |
| `BT24_010` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_010.cs` | `None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT24_013` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_013.cs` | `None, OnAllyAttack, OnDiscardHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT24_014` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_014.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT24_016` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_016.cs` | `OnAllyAttack, OnDeclaration, OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT24_021` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_021.cs` | `None, OnDiscardHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT23-098#7452@base` | `BT23-098` | 7452 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Option/BT23_098.asset` |
| `BT23-099#7453@base` | `BT23-099` | 7453 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Option/BT23_099.asset` |
| `BT23-101#7455@base` | `BT23-101` | 7455 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_101.asset` |
| `BT23-101#7456@P1` | `BT23-101` | 7456 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_101_P1.asset` |
| `BT23-101#7457@P2` | `BT23-101` | 7457 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_101_P2.asset` |
| `BT24-006#7524@base` | `BT24-006` | 7524 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/DigiEgg/BT24_006.asset` |
| `BT24-009#7528@base` | `BT24-009` | 7528 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_009.asset` |
| `BT24-010#7529@base` | `BT24-010` | 7529 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_010.asset` |
| `BT24-013#7532@base` | `BT24-013` | 7532 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_013.asset` |
| `BT24-014#7533@base` | `BT24-014` | 7533 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_014.asset` |
| `BT24-014#7534@P1` | `BT24-014` | 7534 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_014_P1.asset` |
| `BT24-016#7536@base` | `BT24-016` | 7536 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_016.asset` |
| `BT24-016#8449@P1` | `BT24-016` | 8449 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_016_P1.asset` |
| `BT24-021#7543@base` | `BT24-021` | 7543 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_021.asset` |

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
