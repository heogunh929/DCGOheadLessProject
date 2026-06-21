# C0268_special_digivolution_play - special digivolution/play mechanics card porting 33

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0268_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT16_041` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_041.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT16_043` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_043.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT16_049` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_049.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT16_051` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_051.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectBoolean, SelectJogress` | 2 |
| `BT16_052` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_052.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 1 |
| `BT16_055` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_055.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT16_062` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_062.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT16_063` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_063.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT16_065` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_065.cs` | `BeforePayCost, None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT16_066` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_066.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT16-041#3355@base` | `BT16-041` | 3355 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_041.asset` |
| `BT16-041#4796@P0` | `BT16-041` | 4796 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_041_P0.asset` |
| `BT16-043#3357@base` | `BT16-043` | 3357 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_043.asset` |
| `BT16-049#3365@base` | `BT16-049` | 3365 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_049.asset` |
| `BT16-051#3367@base` | `BT16-051` | 3367 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_051.asset` |
| `BT16-051#3368@P1` | `BT16-051` | 3368 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_051_P1.asset` |
| `BT16-052#3369@base` | `BT16-052` | 3369 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_052.asset` |
| `BT16-055#3372@base` | `BT16-055` | 3372 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_055.asset` |
| `BT16-055#4801@P0` | `BT16-055` | 4801 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_055_P0.asset` |
| `BT16-062#3379@base` | `BT16-062` | 3379 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_062.asset` |
| `BT16-063#3380@base` | `BT16-063` | 3380 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_063.asset` |
| `BT16-063#3381@P1` | `BT16-063` | 3381 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_063_P1.asset` |
| `BT16-065#3383@base` | `BT16-065` | 3383 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_065.asset` |
| `BT16-065#4804@P0` | `BT16-065` | 4804 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_065_P0.asset` |
| `BT16-066#3384@base` | `BT16-066` | 3384 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_066.asset` |
| `BT16-066#4805@P0` | `BT16-066` | 4805 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_066_P0.asset` |

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
