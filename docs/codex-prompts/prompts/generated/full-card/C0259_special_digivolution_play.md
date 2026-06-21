# C0259_special_digivolution_play - special digivolution/play mechanics card porting 24

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0259_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT14_007` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_007.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `BT14_012` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_012.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `SelectJogress` | 3 |
| `BT14_013` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_013.cs` | `OnEndTurn, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectAttackTarget, SelectJogress` | 2 |
| `BT14_018` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_018.cs` | `BeforePayCost, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectJogress` | 2 |
| `BT14_032` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_032.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT14_048` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_048.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `BT14_052` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_052.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT14_058` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_058.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 3 |
| `BT14_063` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_063.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT14_064` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_064.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT14-007#2924@base` | `BT14-007` | 2924 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_007.asset` |
| `BT14-007#2925@P1` | `BT14-007` | 2925 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_007_P1.asset` |
| `BT14-012#2930@base` | `BT14-012` | 2930 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_012.asset` |
| `BT14-012#4638@P0` | `BT14-012` | 4638 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_012_P0.asset` |
| `BT14-012#8171@P1` | `BT14-012` | 8171 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_012_P1.asset` |
| `BT14-013#2931@base` | `BT14-013` | 2931 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_013.asset` |
| `BT14-013#4639@P0` | `BT14-013` | 4639 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_013_P0.asset` |
| `BT14-018#2937@base` | `BT14-018` | 2937 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_018.asset` |
| `BT14-018#4643@P0` | `BT14-018` | 4643 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_018_P0.asset` |
| `BT14-032#2953@base` | `BT14-032` | 2953 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_032.asset` |
| `BT14-048#2972@base` | `BT14-048` | 2972 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_048.asset` |
| `BT14-048#4665@P0` | `BT14-048` | 4665 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_048_P0.asset` |
| `BT14-052#2977@base` | `BT14-052` | 2977 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_052.asset` |
| `BT14-058#2984@base` | `BT14-058` | 2984 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_058.asset` |
| `BT14-058#4670@P0` | `BT14-058` | 4670 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_058_P0.asset` |
| `BT14-058#8176@P1` | `BT14-058` | 8176 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_058_P1.asset` |
| `BT14-063#2989@base` | `BT14-063` | 2989 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_063.asset` |
| `BT14-063#4673@P0` | `BT14-063` | 4673 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_063_P0.asset` |
| `BT14-064#2990@base` | `BT14-064` | 2990 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_064.asset` |
| `BT14-064#4674@P0` | `BT14-064` | 4674 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_064_P0.asset` |
| `BT14-064#4675@P1` | `BT14-064` | 4675 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_064_P1.asset` |
| `BT14-064#4676@P2` | `BT14-064` | 4676 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_064_P2.asset` |
| `BT14-064#4677@P3` | `BT14-064` | 4677 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_064_P3.asset` |

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
