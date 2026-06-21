# C0202_replacement_counter_cut_in - replacement/counter/cut-in card porting 17

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0202_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT21_036` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_036.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT21_037` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_037.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT21_038` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_038.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 1 |
| `BT21_051` | `DCGO/Assets/Scripts/CardEffect/BT21/Green/BT21_051.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `BT21_052` | `DCGO/Assets/Scripts/CardEffect/BT21/Green/BT21_052.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnTappedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT21_062` | `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_062.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 5 |
| `BT21_073` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_073.cs` | `None, OnDeclaration, OnEnterFieldAnyone, OnStartMainPhase, WhenLinked, WhenRemoveField` | `inherited, linked, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectAttackTarget` | 1 |
| `BT22_019` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_019.cs` | `BeforePayCost, None, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT22_022` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_022.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT22_025` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_025.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT21-036#5347@base` | `BT21-036` | 5347 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_036.asset` |
| `BT21-036#8390@P1` | `BT21-036` | 8390 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_036_P1.asset` |
| `BT21-037#5348@base` | `BT21-037` | 5348 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_037.asset` |
| `BT21-037#8391@P1` | `BT21-037` | 8391 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_037_P1.asset` |
| `BT21-037#8392@P2` | `BT21-037` | 8392 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_037_P2.asset` |
| `BT21-038#5349@base` | `BT21-038` | 5349 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_038.asset` |
| `BT21-051#5362@base` | `BT21-051` | 5362 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Green/Digimon/BT21_051.asset` |
| `BT21-051#5363@P1` | `BT21-051` | 5363 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Green/Digimon/BT21_051_P1.asset` |
| `BT21-051#5364@P2` | `BT21-051` | 5364 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Green/Digimon/BT21_051_P2.asset` |
| `BT21-052#5365@base` | `BT21-052` | 5365 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Green/Digimon/BT21_052.asset` |
| `BT21-052#5366@P1` | `BT21-052` | 5366 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Green/Digimon/BT21_052_P1.asset` |
| `BT21-062#5377@base` | `BT21-062` | 5377 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_062.asset` |
| `BT21-062#5378@P1` | `BT21-062` | 5378 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_062_P1.asset` |
| `BT21-062#5379@P2` | `BT21-062` | 5379 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_062_P2.asset` |
| `BT21-062#8403@P3` | `BT21-062` | 8403 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_062_P3.asset` |
| `BT21-062#8404@P4` | `BT21-062` | 8404 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_062_P4.asset` |
| `BT21-073#5390@base` | `BT21-073` | 5390 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_073.asset` |
| `BT22-019#7012@base` | `BT22-019` | 7012 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_019.asset` |
| `BT22-022#7015@base` | `BT22-022` | 7015 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_022.asset` |
| `BT22-022#8427@P1` | `BT22-022` | 8427 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_022_P1.asset` |
| `BT22-025#7019@base` | `BT22-025` | 7019 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_025.asset` |
| `BT22-025#7020@P1` | `BT22-025` | 7020 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_025_P1.asset` |

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
