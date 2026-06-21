# C0223_replacement_counter_cut_in - replacement/counter/cut-in card porting 38

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0223_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX9_020` | `DCGO/Assets/Scripts/CardEffect/EX9/Blue/EX9_020.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone, WhenRemoveField` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `EX9_023` | `DCGO/Assets/Scripts/CardEffect/EX9/Yellow/EX9_023.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX9_025` | `DCGO/Assets/Scripts/CardEffect/EX9/Yellow/EX9_025.cs` | `None, OnAllyAttack, OnDeclaration, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX9_028` | `DCGO/Assets/Scripts/CardEffect/EX9/Yellow/EX9_028.cs` | `None, OnEndTurn` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectBoolean, SelectSecurity` | 2 |
| `EX9_029` | `DCGO/Assets/Scripts/CardEffect/EX9/Yellow/EX9_029.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `EX9_030` | `DCGO/Assets/Scripts/CardEffect/EX9/Yellow/EX9_030.cs` | `BeforePayCost, None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 3 |
| `EX9_032` | `DCGO/Assets/Scripts/CardEffect/EX9/Yellow/EX9_032.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX9_035` | `DCGO/Assets/Scripts/CardEffect/EX9/Green/EX9_035.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX9_037` | `DCGO/Assets/Scripts/CardEffect/EX9/Green/EX9_037.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX9_038` | `DCGO/Assets/Scripts/CardEffect/EX9/Green/EX9_038.cs` | `None, OnAllyAttack, OnDeclaration, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX9-020#6872@base` | `EX9-020` | 6872 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_020.asset` |
| `EX9-020#6873@P1` | `EX9-020` | 6873 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_020_P1.asset` |
| `EX9-023#6878@base` | `EX9-023` | 6878 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_023.asset` |
| `EX9-023#6879@P1` | `EX9-023` | 6879 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_023_P1.asset` |
| `EX9-025#6882@base` | `EX9-025` | 6882 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_025.asset` |
| `EX9-025#6883@P1` | `EX9-025` | 6883 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_025_P1.asset` |
| `EX9-028#6888@base` | `EX9-028` | 6888 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_028.asset` |
| `EX9-028#6889@P1` | `EX9-028` | 6889 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_028_P1.asset` |
| `EX9-029#6890@base` | `EX9-029` | 6890 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_029.asset` |
| `EX9-029#6891@P1` | `EX9-029` | 6891 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_029_P1.asset` |
| `EX9-030#6892@base` | `EX9-030` | 6892 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_030.asset` |
| `EX9-030#6893@P1` | `EX9-030` | 6893 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_030_P1.asset` |
| `EX9-030#6894@P2` | `EX9-030` | 6894 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_030_P2.asset` |
| `EX9-032#6896@base` | `EX9-032` | 6896 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_032.asset` |
| `EX9-032#9207@P1` | `EX9-032` | 9207 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_032_P1.asset` |
| `EX9-035#6902@base` | `EX9-035` | 6902 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_035.asset` |
| `EX9-035#6903@P1` | `EX9-035` | 6903 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_035_P1.asset` |
| `EX9-037#6906@base` | `EX9-037` | 6906 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_037.asset` |
| `EX9-037#6907@P1` | `EX9-037` | 6907 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_037_P1.asset` |
| `EX9-038#6908@base` | `EX9-038` | 6908 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_038.asset` |
| `EX9-038#6909@P1` | `EX9-038` | 6909 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_038_P1.asset` |

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
