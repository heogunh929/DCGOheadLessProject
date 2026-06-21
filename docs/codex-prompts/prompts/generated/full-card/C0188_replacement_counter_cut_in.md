# C0188_replacement_counter_cut_in - replacement/counter/cut-in card porting 3

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0188_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT11_018` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_018.cs` | `None, OnEndAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress, SelectDigiXros` | 2 |
| `BT11_019` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_019.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress, SelectDigiXros` | 6 |
| `BT11_030` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_030.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress, SelectDigiXros` | 2 |
| `BT11_040` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_040.cs` | `OnDestroyedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT11_041` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_041.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT11_043` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_043.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT11_062` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_062.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted, WhenReturntoHandAnyone, WhenReturntoLibraryAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectOrder, SelectJogress` | 2 |
| `BT11_064` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_064.cs` | `None, WhenPermanentWouldBeDeleted, WhenReturntoHandAnyone, WhenReturntoLibraryAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT11_082` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_082.cs` | `None, OnDestroyedAnyone, OnDigivolutionCardDiscarded, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT11_111` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_111.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-018#2287@base` | `BT11-018` | 2287 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_018.asset` |
| `BT11-018#4380@P0` | `BT11-018` | 4380 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_018_P0.asset` |
| `BT11-019#2288@base` | `BT11-019` | 2288 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_019.asset` |
| `BT11-019#2289@P1` | `BT11-019` | 2289 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_019_P1.asset` |
| `BT11-019#4381@P2` | `BT11-019` | 4381 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_019_P2.asset` |
| `BT11-019#4382@P3` | `BT11-019` | 4382 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_019_P3.asset` |
| `BT11-019#4383@P4` | `BT11-019` | 4383 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_019_P4.asset` |
| `BT11-019#8106@P5` | `BT11-019` | 8106 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_019_P5.asset` |
| `BT11-030#2300@base` | `BT11-030` | 2300 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_030.asset` |
| `BT11-030#4393@P0` | `BT11-030` | 4393 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_030_P0.asset` |
| `BT11-040#2312@base` | `BT11-040` | 2312 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_040.asset` |
| `BT11-040#4400@P0` | `BT11-040` | 4400 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_040_P0.asset` |
| `BT11-041#2313@base` | `BT11-041` | 2313 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_041.asset` |
| `BT11-043#2316@base` | `BT11-043` | 2316 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_043.asset` |
| `BT11-043#4402@P0` | `BT11-043` | 4402 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_043_P0.asset` |
| `BT11-043#4403@P1` | `BT11-043` | 4403 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_043_P1.asset` |
| `BT11-062#2336@base` | `BT11-062` | 2336 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_062.asset` |
| `BT11-062#4414@P0` | `BT11-062` | 4414 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_062_P0.asset` |
| `BT11-064#2338@base` | `BT11-064` | 2338 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_064.asset` |
| `BT11-082#2357@base` | `BT11-082` | 2357 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_082.asset` |
| `BT11-082#4428@P0` | `BT11-082` | 4428 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_082_P0.asset` |
| `BT11-111#2394@base` | `BT11-111` | 2394 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_111.asset` |
| `BT11-111#2395@P1` | `BT11-111` | 2395 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_111_P1.asset` |

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
