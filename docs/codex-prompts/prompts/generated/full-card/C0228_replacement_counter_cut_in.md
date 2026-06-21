# C0228_replacement_counter_cut_in - replacement/counter/cut-in card porting 43

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0228_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 26
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_069` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_069.cs` | `OnEndBattle, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |
| `P_070` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_070.cs` | `OnEndBattle, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectCard, SelectBoolean` | 3 |
| `P_071` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_071.cs` | `OnEndBattle, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `P_072` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_072.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted, WhenReturntoHandAnyone, WhenReturntoLibraryAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `P_073` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_073.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `P_109` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_109.cs` | `None, OnCounterTiming, OnTappedAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 3 |
| `P_113` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_113.cs` | `None, OnCounterTiming, OnEndBattle, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 4 |
| `P_114` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_114.cs` | `OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `P_120` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_120.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, static_or_continuous` | `-` | 3 |
| `P_137` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_137.cs` | `None, OnAllyAttack, OnAttackTargetChanged, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-069#10371@P1` | `P-069` | 10371 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_069_P1.asset` |
| `P-069#10372@P2` | `P-069` | 10372 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_069_P2.asset` |
| `P-069#6111@base` | `P-069` | 6111 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_069.asset` |
| `P-070#10381@P1` | `P-070` | 10381 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_070_P1.asset` |
| `P-070#10382@P2` | `P-070` | 10382 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_070_P2.asset` |
| `P-070#6112@base` | `P-070` | 6112 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_070.asset` |
| `P-071#10386@P1` | `P-071` | 10386 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_071_P1.asset` |
| `P-071#6113@base` | `P-071` | 6113 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_071.asset` |
| `P-072#10391@P1` | `P-072` | 10391 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_072_P1.asset` |
| `P-072#6114@base` | `P-072` | 6114 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_072.asset` |
| `P-073#10383@P1` | `P-073` | 10383 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_073_P1.asset` |
| `P-073#6115@base` | `P-073` | 6115 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_073.asset` |
| `P-109#10404@P1` | `P-109` | 10404 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_109_P1.asset` |
| `P-109#10405@P2` | `P-109` | 10405 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_109_P2.asset` |
| `P-109#6155@base` | `P-109` | 6155 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_109.asset` |
| `P-113#10410@P1` | `P-113` | 10410 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_113_P1.asset` |
| `P-113#10411@P2` | `P-113` | 10411 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_113_P2.asset` |
| `P-113#10412@P3` | `P-113` | 10412 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_113_P3.asset` |
| `P-113#6159@base` | `P-113` | 6159 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_113.asset` |
| `P-114#10406@P1` | `P-114` | 10406 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_114_P1.asset` |
| `P-114#6160@base` | `P-114` | 6160 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_114.asset` |
| `P-120#10425@P1` | `P-120` | 10425 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_120_P1.asset` |
| `P-120#10426@P2` | `P-120` | 10426 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_120_P2.asset` |
| `P-120#6166@base` | `P-120` | 6166 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_120.asset` |
| `P-137#10291@base` | `P-137` | 10291 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_137.asset` |
| `P-137#9258@P1` | `P-137` | 9258 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_137_P1.asset` |

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
