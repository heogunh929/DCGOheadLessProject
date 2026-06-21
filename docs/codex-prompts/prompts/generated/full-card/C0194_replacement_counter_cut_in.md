# C0194_replacement_counter_cut_in - replacement/counter/cut-in card porting 9

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0194_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 26
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT16_061` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_061.cs` | `None, OnAttackTargetChanged, OnCounterTiming, OnDestroyedAnyone, OnEndBattle` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard, SelectAttackTarget, SelectJogress` | 2 |
| `BT16_064` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_064.cs` | `None, OnCounterTiming, OnDestroyedAnyone, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT16_070` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_070.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT16_080` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_080.cs` | `None, OnDestroyedAnyone, OnEndAttack, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT16_086` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_086.cs` | `None, OnDeclaration, OnEndTurn, OnStartTurn, SecuritySkill, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT16_101` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_101.cs` | `None, OnDestroyedAnyone, OnEndBattle, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectAttackTarget, SelectJogress` | 3 |
| `BT16_102` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_102.cs` | `None, OnEnterFieldAnyone, OnLoseSecurity, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 4 |
| `BT17_018` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_018.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `BT17_035` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_035.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 3 |
| `BT17_036` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_036.cs` | `None, OnDiscardSecurity, OnEndAttack, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT16-061#3378@base` | `BT16-061` | 3378 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_061.asset` |
| `BT16-061#4802@P0` | `BT16-061` | 4802 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_061_P0.asset` |
| `BT16-064#3382@base` | `BT16-064` | 3382 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_064.asset` |
| `BT16-064#4803@P0` | `BT16-064` | 4803 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_064_P0.asset` |
| `BT16-070#3388@base` | `BT16-070` | 3388 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_070.asset` |
| `BT16-080#3399@base` | `BT16-080` | 3399 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_080.asset` |
| `BT16-080#3400@P1` | `BT16-080` | 3400 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_080_P1.asset` |
| `BT16-086#3411@base` | `BT16-086` | 3411 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Tamer/BT16_086.asset` |
| `BT16-086#4818@P0` | `BT16-086` | 4818 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Tamer/BT16_086_P0.asset` |
| `BT16-086#4819@P1` | `BT16-086` | 4819 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Tamer/BT16_086_P1.asset` |
| `BT16-101#3429@base` | `BT16-101` | 3429 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_101.asset` |
| `BT16-101#3430@P1` | `BT16-101` | 3430 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_101_P1.asset` |
| `BT16-101#8214@P2` | `BT16-101` | 8214 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_101_P2.asset` |
| `BT16-102#3431@base` | `BT16-102` | 3431 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_102.asset` |
| `BT16-102#3432@P1` | `BT16-102` | 3432 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_102_P1.asset` |
| `BT16-102#3433@P2` | `BT16-102` | 3433 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_102_P2.asset` |
| `BT16-102#8215@P3` | `BT16-102` | 8215 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_102_P3.asset` |
| `BT17-018#3559@base` | `BT17-018` | 3559 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_018.asset` |
| `BT17-018#3560@P1` | `BT17-018` | 3560 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_018_P1.asset` |
| `BT17-018#3561@P2` | `BT17-018` | 3561 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_018_P2.asset` |
| `BT17-018#8221@P3` | `BT17-018` | 8221 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_018_P3.asset` |
| `BT17-018#8222@P4` | `BT17-018` | 8222 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_018_P4.asset` |
| `BT17-035#3580@base` | `BT17-035` | 3580 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_035.asset` |
| `BT17-035#8228@P1` | `BT17-035` | 8228 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_035_P1.asset` |
| `BT17-035#8229@P2` | `BT17-035` | 8229 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_035_P2.asset` |
| `BT17-036#3581@base` | `BT17-036` | 3581 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_036.asset` |

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
