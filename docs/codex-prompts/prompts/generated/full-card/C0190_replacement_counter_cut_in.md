# C0190_replacement_counter_cut_in - replacement/counter/cut-in card porting 5

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0190_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 27
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT13_069` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_069.cs` | `None, OnAllyAttack, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT13_075` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_075.cs` | `OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 4 |
| `BT14_014` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_014.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 5 |
| `BT14_020` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_020.cs` | `OnStartMainPhase, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT14_021` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_021.cs` | `WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in` | `-` | 2 |
| `BT14_026` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_026.cs` | `OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 4 |
| `BT14_035` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_035.cs` | `WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in` | `-` | 1 |
| `BT14_037` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_037.cs` | `OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 4 |
| `BT14_038` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_038.cs` | `None, OnDestroyedAnyone, SecuritySkill` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `BT14_039` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_039.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT13-069#2726@base` | `BT13-069` | 2726 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_069.asset` |
| `BT13-075#2732@base` | `BT13-075` | 2732 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_075.asset` |
| `BT13-075#2733@P1` | `BT13-075` | 2733 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_075_P1.asset` |
| `BT13-075#4599@P2` | `BT13-075` | 4599 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_075_P2.asset` |
| `BT13-075#4600@P3` | `BT13-075` | 4600 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_075_P3.asset` |
| `BT14-014#2932@base` | `BT14-014` | 2932 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_014.asset` |
| `BT14-014#2933@P1` | `BT14-014` | 2933 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_014_P1.asset` |
| `BT14-014#4640@P2` | `BT14-014` | 4640 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_014_P2.asset` |
| `BT14-014#4641@P3` | `BT14-014` | 4641 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_014_P3.asset` |
| `BT14-014#8172@P4` | `BT14-014` | 8172 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_014_P4.asset` |
| `BT14-020#2939@base` | `BT14-020` | 2939 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_020.asset` |
| `BT14-020#2940@P1` | `BT14-020` | 2940 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_020_P1.asset` |
| `BT14-021#2941@base` | `BT14-021` | 2941 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_021.asset` |
| `BT14-025#2945@base` | `BT14-025` | 2945 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_025.asset` |
| `BT14-026#2946@base` | `BT14-026` | 2946 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_026.asset` |
| `BT14-026#2947@P1` | `BT14-026` | 2947 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_026_P1.asset` |
| `BT14-026#4645@P2` | `BT14-026` | 4645 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_026_P2.asset` |
| `BT14-026#4646@P3` | `BT14-026` | 4646 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_026_P3.asset` |
| `BT14-035#2957@base` | `BT14-035` | 2957 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_035.asset` |
| `BT14-037#2959@base` | `BT14-037` | 2959 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_037.asset` |
| `BT14-037#2960@P1` | `BT14-037` | 2960 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_037_P1.asset` |
| `BT14-037#4652@P2` | `BT14-037` | 4652 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_037_P2.asset` |
| `BT14-037#4653@P3` | `BT14-037` | 4653 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_037_P3.asset` |
| `BT14-038#2961@base` | `BT14-038` | 2961 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_038.asset` |
| `BT14-038#4654@P0` | `BT14-038` | 4654 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_038_P0.asset` |
| `BT14-039#2962@base` | `BT14-039` | 2962 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_039.asset` |
| `BT14-039#4655@P0` | `BT14-039` | 4655 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_039_P0.asset` |

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
