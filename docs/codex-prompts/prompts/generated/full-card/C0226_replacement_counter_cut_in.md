# C0226_replacement_counter_cut_in - replacement/counter/cut-in card porting 41

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0226_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX9_073` | `DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_073.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress, SelectAssembly` | 3 |
| `EX9_074` | `DCGO/Assets/Scripts/CardEffect/EX9/White/EX9_074.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectAssembly` | 2 |
| `LM_001` | `DCGO/Assets/Scripts/CardEffect/LM/Red/LM_001.cs` | `None, OnCounterTiming, OnDestroyedAnyone, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `LM_005` | `DCGO/Assets/Scripts/CardEffect/LM/Blue/LM_005.cs` | `OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 1 |
| `LM_013` | `DCGO/Assets/Scripts/CardEffect/LM/Green/LM_013.cs` | `OnAllyAttack, OnCounterTiming, OnEndTurn, OnEnterFieldAnyone` | `counter, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `LM_017` | `DCGO/Assets/Scripts/CardEffect/LM/Purple/LM_017.cs` | `None, OnAddDigivolutionCards, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `LM_019` | `DCGO/Assets/Scripts/CardEffect/LM/White/LM_019.cs` | `OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `LM_021` | `DCGO/Assets/Scripts/CardEffect/LM/Red/LM_021.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `LM_022` | `DCGO/Assets/Scripts/CardEffect/LM/Blue/LM_022.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `LM_023` | `DCGO/Assets/Scripts/CardEffect/LM/Yellow/LM_023.cs` | `None, OnAddSecurity, OnCounterTiming, OnEnterFieldAnyone, OnUseOption` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX9-073#6972@base` | `EX9-073` | 6972 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_073.asset` |
| `EX9-073#6973@P1` | `EX9-073` | 6973 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_073_P1.asset` |
| `EX9-073#6974@P2` | `EX9-073` | 6974 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_073_P2.asset` |
| `EX9-074#6975@base` | `EX9-074` | 6975 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/White/Digimon/EX9_074.asset` |
| `EX9-074#6976@P1` | `EX9-074` | 6976 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/White/Digimon/EX9_074_P1.asset` |
| `LM-001#3247@base` | `LM-001` | 3247 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Red/Digimon/LM_001.asset` |
| `LM-005#3251@base` | `LM-005` | 3251 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Digimon/LM_005.asset` |
| `LM-013#3259@base` | `LM-013` | 3259 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Digimon/LM_013.asset` |
| `LM-017#3263@base` | `LM-017` | 3263 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Digimon/LM_017.asset` |
| `LM-017#7868@P1` | `LM-017` | 7868 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Digimon/LM_017_P1.asset` |
| `LM-019#3265@base` | `LM-019` | 3265 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/White/Digimon/LM_019.asset` |
| `LM-021#4031@base` | `LM-021` | 4031 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Red/Digimon/LM_021.asset` |
| `LM-021#7869@P1` | `LM-021` | 7869 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/LM/Red/Digimon/LM_021_P1.asset` |
| `LM-021#7870@P2` | `LM-021` | 7870 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/LM/Red/Digimon/LM_021_P2.asset` |
| `LM-022#4032@base` | `LM-022` | 4032 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Digimon/LM_022.asset` |
| `LM-022#7871@P1` | `LM-022` | 7871 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Digimon/LM_022_P1.asset` |
| `LM-022#7872@P2` | `LM-022` | 7872 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Digimon/LM_022_P2.asset` |
| `LM-023#4033@base` | `LM-023` | 4033 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Yellow/Digimon/LM_023.asset` |

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
