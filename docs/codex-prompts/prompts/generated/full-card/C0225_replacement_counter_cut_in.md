# C0225_replacement_counter_cut_in - replacement/counter/cut-in card porting 40

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0225_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX9_058` | `DCGO/Assets/Scripts/CardEffect/EX9/Purple/EX9_058.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX9_059` | `DCGO/Assets/Scripts/CardEffect/EX9/Purple/EX9_059.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX9_060` | `DCGO/Assets/Scripts/CardEffect/EX9/Purple/EX9_060.cs` | `None, OnAllyAttack, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX9_061` | `DCGO/Assets/Scripts/CardEffect/EX9/Purple/EX9_061.cs` | `None, OnAllyAttack, OnDeclaration, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX9_063` | `DCGO/Assets/Scripts/CardEffect/EX9/Purple/EX9_063.cs` | `BeforePayCost, None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX9_064` | `DCGO/Assets/Scripts/CardEffect/EX9/Purple/EX9_064.cs` | `BeforePayCost, None, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 3 |
| `EX9_065` | `DCGO/Assets/Scripts/CardEffect/EX9/Purple/EX9_065.cs` | `None, OnCounterTiming, OnDestroyedAnyone, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `EX9_068` | `DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_068.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `EX9_069` | `DCGO/Assets/Scripts/CardEffect/EX9/White/EX9_069.cs` | `None, OnAddDigivolutionCards, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `EX9_070` | `DCGO/Assets/Scripts/CardEffect/EX9/Red/EX9_070.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX9-058#6943@base` | `EX9-058` | 6943 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_058.asset` |
| `EX9-058#6944@P1` | `EX9-058` | 6944 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_058_P1.asset` |
| `EX9-059#6945@base` | `EX9-059` | 6945 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_059.asset` |
| `EX9-059#6946@P1` | `EX9-059` | 6946 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_059_P1.asset` |
| `EX9-060#6947@base` | `EX9-060` | 6947 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_060.asset` |
| `EX9-060#6948@P1` | `EX9-060` | 6948 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_060_P1.asset` |
| `EX9-061#6949@base` | `EX9-061` | 6949 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_061.asset` |
| `EX9-061#6950@P1` | `EX9-061` | 6950 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_061_P1.asset` |
| `EX9-063#6952@base` | `EX9-063` | 6952 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_063.asset` |
| `EX9-064#6953@base` | `EX9-064` | 6953 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_064.asset` |
| `EX9-064#6954@P1` | `EX9-064` | 6954 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_064_P1.asset` |
| `EX9-064#6955@P2` | `EX9-064` | 6955 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_064_P2.asset` |
| `EX9-065#6956@base` | `EX9-065` | 6956 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/Digimon/EX9_065.asset` |
| `EX9-068#6962@base` | `EX9-068` | 6962 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Tamer/EX9_068.asset` |
| `EX9-068#6963@P1` | `EX9-068` | 6963 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Tamer/EX9_068_P1.asset` |
| `EX9-069#6964@base` | `EX9-069` | 6964 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/White/Tamer/EX9_069.asset` |
| `EX9-069#6965@P1` | `EX9-069` | 6965 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/White/Tamer/EX9_069_P1.asset` |
| `EX9-070#6966@base` | `EX9-070` | 6966 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Option/EX9_070.asset` |
| `EX9-070#6967@P1` | `EX9-070` | 6967 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Option/EX9_070_P1.asset` |

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
