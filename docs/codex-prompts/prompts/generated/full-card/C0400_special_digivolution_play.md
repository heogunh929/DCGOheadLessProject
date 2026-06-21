# C0400_special_digivolution_play - special digivolution/play mechanics card porting 165

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0400_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `RB1_021` | `DCGO/Assets/Scripts/CardEffect/RB1/Green/RB1_021.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 1 |
| `RB1_022` | `DCGO/Assets/Scripts/CardEffect/RB1/Green/RB1_022.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `RB1_023` | `DCGO/Assets/Scripts/CardEffect/RB1/Green/RB1_023.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `RB1_024` | `DCGO/Assets/Scripts/CardEffect/RB1/Green/RB1_024.cs` | `OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `RB1_029` | `DCGO/Assets/Scripts/CardEffect/RB1/Purple/RB1_029.cs` | `None, OnDestroyedAnyone, OnEndAttack` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `RB1_030` | `DCGO/Assets/Scripts/CardEffect/RB1/Purple/RB1_030.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `RB1_031` | `DCGO/Assets/Scripts/CardEffect/RB1/Purple/RB1_031.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `RB1_032` | `DCGO/Assets/Scripts/CardEffect/RB1/Red/RB1_032.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `RB1_036` | `DCGO/Assets/Scripts/CardEffect/RB1/Red/RB1_036.cs` | `None, OnDestroyedAnyone, OnEndTurn` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `ST10_04` | `DCGO/Assets/Scripts/CardEffect/ST10/Yellow/ST10_04.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `RB1-021#2889@base` | `RB1-021` | 2889 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Green/Digimon/RB1_021.asset` |
| `RB1-022#2890@base` | `RB1-022` | 2890 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Green/Digimon/RB1_022.asset` |
| `RB1-023#2891@base` | `RB1-023` | 2891 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Green/Digimon/RB1_023.asset` |
| `RB1-024#2892@base` | `RB1-024` | 2892 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Green/Digimon/RB1_024.asset` |
| `RB1-029#2899@base` | `RB1-029` | 2899 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Purple/Digimon/RB1_029.asset` |
| `RB1-030#2900@base` | `RB1-030` | 2900 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Purple/Digimon/RB1_030.asset` |
| `RB1-031#2901@base` | `RB1-031` | 2901 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Purple/Digimon/RB1_031.asset` |
| `RB1-031#2902@P1` | `RB1-031` | 2902 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Purple/Digimon/RB1_031_P1.asset` |
| `RB1-032#2903@base` | `RB1-032` | 2903 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Tamer/RB1_032.asset` |
| `RB1-032#2904@P1` | `RB1-032` | 2904 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Tamer/RB1_032_P1.asset` |
| `RB1-036#2910@base` | `RB1-036` | 2910 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Digimon/RB1_036.asset` |
| `RB1-036#2911@P1` | `RB1-036` | 2911 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Digimon/RB1_036_P1.asset` |
| `ST10-04#1759@base` | `ST10-04` | 1759 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Digimon/ST10_04.asset` |
| `ST10-04#4902@P1` | `ST10-04` | 4902 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Digimon/ST10_04_P1.asset` |
| `ST10-04#9025@P2` | `ST10-04` | 9025 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Digimon/ST10_04_P2.asset` |

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
