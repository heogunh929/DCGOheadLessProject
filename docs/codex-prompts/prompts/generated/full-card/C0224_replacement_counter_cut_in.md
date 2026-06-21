# C0224_replacement_counter_cut_in - replacement/counter/cut-in card porting 39

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0224_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX9_039` | `DCGO/Assets/Scripts/CardEffect/EX9/Green/EX9_039.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectAttackTarget` | 2 |
| `EX9_043` | `DCGO/Assets/Scripts/CardEffect/EX9/Green/EX9_043.cs` | `BeforePayCost, None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `EX9_047` | `DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_047.cs` | `None, OnCounterTiming, OnDestroyedAnyone` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress, SelectAssembly` | 2 |
| `EX9_049` | `DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_049.cs` | `None, OnEndTurn` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectBoolean` | 2 |
| `EX9_050` | `DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_050.cs` | `None, OnEndTurn` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectBoolean` | 2 |
| `EX9_051` | `DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_051.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX9_052` | `DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_052.cs` | `None, OnDestroyedAnyone, OnEndTurn` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean` | 2 |
| `EX9_053` | `DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_053.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `EX9_056` | `DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_056.cs` | `None, OnCounterTiming, OnEnterFieldAnyone, WhenRemoveField` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `EX9_057` | `DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_057.cs` | `None, OnAllyAttack, OnCounterTiming, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnMove` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectBoolean, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX9-039#6910@base` | `EX9-039` | 6910 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_039.asset` |
| `EX9-039#6911@P1` | `EX9-039` | 6911 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_039_P1.asset` |
| `EX9-043#6917@base` | `EX9-043` | 6917 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_043.asset` |
| `EX9-043#6918@P1` | `EX9-043` | 6918 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_043_P1.asset` |
| `EX9-043#6919@P2` | `EX9-043` | 6919 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_043_P2.asset` |
| `EX9-047#6925@base` | `EX9-047` | 6925 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_047.asset` |
| `EX9-047#6926@P1` | `EX9-047` | 6926 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_047_P1.asset` |
| `EX9-049#6929@base` | `EX9-049` | 6929 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_049.asset` |
| `EX9-049#6930@P1` | `EX9-049` | 6930 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_049_P1.asset` |
| `EX9-050#6931@base` | `EX9-050` | 6931 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_050.asset` |
| `EX9-050#6932@P1` | `EX9-050` | 6932 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_050_P1.asset` |
| `EX9-051#6933@base` | `EX9-051` | 6933 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_051.asset` |
| `EX9-051#6934@P1` | `EX9-051` | 6934 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_051_P1.asset` |
| `EX9-052#6935@base` | `EX9-052` | 6935 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_052.asset` |
| `EX9-052#6936@P1` | `EX9-052` | 6936 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_052_P1.asset` |
| `EX9-053#6937@base` | `EX9-053` | 6937 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_053.asset` |
| `EX9-056#6940@base` | `EX9-056` | 6940 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_056.asset` |
| `EX9-057#6941@base` | `EX9-057` | 6941 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_057.asset` |
| `EX9-057#6942@P1` | `EX9-057` | 6942 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_057_P1.asset` |

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
