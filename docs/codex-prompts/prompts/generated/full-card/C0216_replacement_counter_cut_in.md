# C0216_replacement_counter_cut_in - replacement/counter/cut-in card porting 31

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0216_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX11_031` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_031.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX11_044` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_044.cs` | `None, OnAllyAttack, OnDigivolutionCardDiscarded, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `EX11_045` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_045.cs` | `None, OnAddDigivolutionCards, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectAssembly` | 2 |
| `EX11_050` | `DCGO/Assets/Scripts/CardEffect/EX11/Purple/EX11_050.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX1_027` | `DCGO/Assets/Scripts/CardEffect/EX1/Yellow/EX1_027.cs` | `OnEndBattle, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |
| `EX1_037` | `DCGO/Assets/Scripts/CardEffect/EX1/Green/EX1_037.cs` | `OnEndBattle, OnStartTurn` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX1_065` | `DCGO/Assets/Scripts/CardEffect/EX1/White/EX1_065.cs` | `None, OnEndBattle, SecuritySkill` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `EX1_073` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_073.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectOrder` | 5 |
| `EX2_048` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_048.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 1 |
| `EX2_054` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_054.cs` | `None, OnEnterFieldAnyone, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX1-027#1310@base` | `EX1-027` | 1310 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_027.asset` |
| `EX1-027#9090@P1` | `EX1-027` | 9090 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_027_P1.asset` |
| `EX1-037#1324@base` | `EX1-037` | 1324 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_037.asset` |
| `EX1-037#1325@P1` | `EX1-037` | 1325 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_037_P1.asset` |
| `EX1-065#1365@base` | `EX1-065` | 1365 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Digimon/EX1_065.asset` |
| `EX1-065#1366@P1` | `EX1-065` | 1366 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Digimon/EX1_065_P1.asset` |
| `EX1-073#1376@base` | `EX1-073` | 1376 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_073.asset` |
| `EX1-073#1377@P1` | `EX1-073` | 1377 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_073_P1.asset` |
| `EX1-073#1378@P2` | `EX1-073` | 1378 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_073_P2.asset` |
| `EX1-073#6820@P3` | `EX1-073` | 6820 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_073_P3.asset` |
| `EX1-073#6821@P4` | `EX1-073` | 6821 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_073_P4.asset` |
| `EX11-031#7720@base` | `EX11-031` | 7720 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_031.asset` |
| `EX11-031#7721@P1` | `EX11-031` | 7721 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_031_P1.asset` |
| `EX11-044#7747@base` | `EX11-044` | 7747 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_044.asset` |
| `EX11-044#7748@P1` | `EX11-044` | 7748 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_044_P1.asset` |
| `EX11-044#7749@P2` | `EX11-044` | 7749 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_044_P2.asset` |
| `EX11-045#7750@base` | `EX11-045` | 7750 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_045.asset` |
| `EX11-045#7751@P1` | `EX11-045` | 7751 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_045_P1.asset` |
| `EX11-050#7760@base` | `EX11-050` | 7760 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_050.asset` |
| `EX11-050#7761@P1` | `EX11-050` | 7761 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_050_P1.asset` |
| `EX2-048#1994@base` | `EX2-048` | 1994 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_048.asset` |
| `EX2-054#2000@base` | `EX2-054` | 2000 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_054.asset` |

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
