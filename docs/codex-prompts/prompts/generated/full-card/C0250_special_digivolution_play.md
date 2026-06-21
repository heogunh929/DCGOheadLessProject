# C0250_special_digivolution_play - special digivolution/play mechanics card porting 15

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0250_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT12_055` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_055.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `BT12_056` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_056.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `BT12_059` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_059.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT12_062` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_062.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT12_063` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_063.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectJogress, SelectDigiXros` | 2 |
| `BT12_065` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_065.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `BT12_066` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_066.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT12_068` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_068.cs` | `None, OnAllyAttack, OnAttackTargetChanged, OnDetermineDoSecurityCheck` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectAttackTarget, SelectJogress` | 3 |
| `BT12_070` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_070.cs` | `None, OnAllyAttack, OnAttackTargetChanged, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectAttackTarget, SelectJogress` | 5 |
| `BT12_073` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_073.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT12-055#2468@base` | `BT12-055` | 2468 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_055.asset` |
| `BT12-055#4509@P0` | `BT12-055` | 4509 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_055_P0.asset` |
| `BT12-056#2469@base` | `BT12-056` | 2469 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_056.asset` |
| `BT12-056#4510@P0` | `BT12-056` | 4510 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_056_P0.asset` |
| `BT12-059#2473@base` | `BT12-059` | 2473 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_059.asset` |
| `BT12-059#4512@P1` | `BT12-059` | 4512 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_059_P1.asset` |
| `BT12-059#4513@P2` | `BT12-059` | 4513 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_059_P2.asset` |
| `BT12-062#2476@base` | `BT12-062` | 2476 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_062.asset` |
| `BT12-062#4514@P1` | `BT12-062` | 4514 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_062_P1.asset` |
| `BT12-063#2477@base` | `BT12-063` | 2477 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_063.asset` |
| `BT12-063#4515@P0` | `BT12-063` | 4515 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_063_P0.asset` |
| `BT12-065#2480@base` | `BT12-065` | 2480 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_065.asset` |
| `BT12-065#4516@P0` | `BT12-065` | 4516 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_065_P0.asset` |
| `BT12-066#2481@base` | `BT12-066` | 2481 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_066.asset` |
| `BT12-068#2483@base` | `BT12-068` | 2483 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_068.asset` |
| `BT12-068#4517@P0` | `BT12-068` | 4517 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_068_P0.asset` |
| `BT12-068#4518@P1` | `BT12-068` | 4518 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_068_P1.asset` |
| `BT12-070#2485@base` | `BT12-070` | 2485 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_070.asset` |
| `BT12-070#2486@P1` | `BT12-070` | 2486 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_070_P1.asset` |
| `BT12-070#4519@P2` | `BT12-070` | 4519 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_070_P2.asset` |
| `BT12-070#4520@P3` | `BT12-070` | 4520 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_070_P3.asset` |
| `BT12-070#4521@P4` | `BT12-070` | 4521 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_070_P4.asset` |
| `BT12-073#2489@base` | `BT12-073` | 2489 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_073.asset` |
| `BT12-073#4524@P1` | `BT12-073` | 4524 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_073_P1.asset` |

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
