# C0230_replacement_counter_cut_in - replacement/counter/cut-in card porting 45

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0230_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_191` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_191.cs` | `None, OnCounterTiming, OnEndTurn, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 3 |
| `P_194` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_194.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, static_or_continuous` | `-` | 2 |
| `P_214` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_214.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `RB1_012` | `DCGO/Assets/Scripts/CardEffect/RB1/Blue/RB1_012.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, static_or_continuous` | `SelectJogress` | 1 |
| `RB1_016` | `DCGO/Assets/Scripts/CardEffect/RB1/Blue/RB1_016.cs` | `OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectOrder` | 2 |
| `ST12_12` | `DCGO/Assets/Scripts/CardEffect/ST12/White/ST12_12.cs` | `OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectJogress` | 3 |
| `ST15_08` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_08.cs` | `None, OnAttackTargetChanged, SecuritySkill` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectAttackTarget, SelectJogress` | 1 |
| `ST15_12` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_12.cs` | `None, OnCounterTiming, OnLoseSecurity` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `-` | 2 |
| `ST16_08` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_08.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, SecuritySkill` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 1 |
| `ST16_12` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_12.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-191#6983@base` | `P-191` | 6983 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_191.asset` |
| `P-191#6984@P1` | `P-191` | 6984 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_191_P1.asset` |
| `P-191#9306@P3` | `P-191` | 9306 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_191_P3.asset` |
| `P-194#7130@base` | `P-194` | 7130 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_194.asset` |
| `P-194#9309@P1` | `P-194` | 9309 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_194_P1.asset` |
| `P-214#7507@base` | `P-214` | 7507 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_214.asset` |
| `P-214#7508@P1` | `P-214` | 7508 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_214_P1.asset` |
| `RB1-012#2877@base` | `RB1-012` | 2877 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Blue/Digimon/RB1_012.asset` |
| `RB1-016#2881@base` | `RB1-016` | 2881 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Blue/Digimon/RB1_016.asset` |
| `RB1-016#2882@P1` | `RB1-016` | 2882 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Blue/Digimon/RB1_016_P1.asset` |
| `ST12-12#2796@base` | `ST12-12` | 2796 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/White/Digimon/ST12_12.asset` |
| `ST12-12#4906@P1` | `ST12-12` | 4906 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST12/White/Digimon/ST12_12_P1.asset` |
| `ST12-12#4907@P2` | `ST12-12` | 4907 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST12/White/Digimon/ST12_12_P2.asset` |
| `ST15-08#2837@base` | `ST15-08` | 2837 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_08.asset` |
| `ST15-12#2841@base` | `ST15-12` | 2841 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_12.asset` |
| `ST15-12#4938@P1` | `ST15-12` | 4938 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_12_P1.asset` |
| `ST16-08#2853@base` | `ST16-08` | 2853 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_08.asset` |
| `ST16-12#2857@base` | `ST16-12` | 2857 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_12.asset` |
| `ST16-12#4955@P1` | `ST16-12` | 4955 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_12_P1.asset` |

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
