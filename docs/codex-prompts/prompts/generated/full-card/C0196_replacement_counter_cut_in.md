# C0196_replacement_counter_cut_in - replacement/counter/cut-in card porting 11

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0196_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 34
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT17_073` | `DCGO/Assets/Scripts/CardEffect/BT17/Purple/BT17_073.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT17_077` | `DCGO/Assets/Scripts/CardEffect/BT17/White/BT17_077.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 7 |
| `BT17_078` | `DCGO/Assets/Scripts/CardEffect/BT17/White/BT17_078.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectJogress` | 6 |
| `BT17_084` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_084.cs` | `OnEndTurn, OnStartTurn, SecuritySkill, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectAttackTarget` | 3 |
| `BT17_086` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_086.cs` | `None, OnDeclaration, OnEndTurn, OnStartMainPhase, SecuritySkill, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT17_092` | `DCGO/Assets/Scripts/CardEffect/BT17/White/BT17_092.cs` | `None, OnEnterFieldAnyone, SecuritySkill, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT17_097` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_097.cs` | `OptionSkill, SecuritySkill, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 6 |
| `BT17_100` | `DCGO/Assets/Scripts/CardEffect/BT17/Black/BT17_100.cs` | `OnEndTurn, OnStartTurn, OptionSkill, SecuritySkill, WhenRemoveField` | `inherited, max_count_per_turn, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT18_030` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_030.cs` | `OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `BT18_036` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_036.cs` | `OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT17-073#3623@base` | `BT17-073` | 3623 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_073.asset` |
| `BT17-073#3624@P1` | `BT17-073` | 3624 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_073_P1.asset` |
| `BT17-077#3629@base` | `BT17-077` | 3629 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_077.asset` |
| `BT17-077#3630@P1` | `BT17-077` | 3630 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_077_P1.asset` |
| `BT17-077#3631@P2` | `BT17-077` | 3631 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_077_P2.asset` |
| `BT17-077#8231@P3` | `BT17-077` | 8231 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_077_P3.asset` |
| `BT17-077#8232@P4` | `BT17-077` | 8232 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_077_P4.asset` |
| `BT17-077#8233@P5` | `BT17-077` | 8233 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_077_P5.asset` |
| `BT17-077#8234@P6` | `BT17-077` | 8234 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_077_P6.asset` |
| `BT17-078#3632@base` | `BT17-078` | 3632 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_078.asset` |
| `BT17-078#3633@P1` | `BT17-078` | 3633 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_078_P1.asset` |
| `BT17-078#3634@P2` | `BT17-078` | 3634 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_078_P2.asset` |
| `BT17-078#8235@P3` | `BT17-078` | 8235 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_078_P3.asset` |
| `BT17-078#8236@P4` | `BT17-078` | 8236 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_078_P4.asset` |
| `BT17-078#8237@P5` | `BT17-078` | 8237 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_078_P5.asset` |
| `BT17-084#3645@base` | `BT17-084` | 3645 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Tamer/BT17_084.asset` |
| `BT17-084#3646@P1` | `BT17-084` | 3646 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Tamer/BT17_084_P1.asset` |
| `BT17-084#4869@P0` | `BT17-084` | 4869 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Tamer/BT17_084_P0.asset` |
| `BT17-086#3649@base` | `BT17-086` | 3649 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_086.asset` |
| `BT17-086#3650@P1` | `BT17-086` | 3650 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_086_P1.asset` |
| `BT17-086#4873@P0` | `BT17-086` | 4873 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_086_P0.asset` |
| `BT17-092#3659@base` | `BT17-092` | 3659 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Tamer/BT17_092.asset` |
| `BT17-092#3660@P1` | `BT17-092` | 3660 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Tamer/BT17_092_P1.asset` |
| `BT17-092#4881@P0` | `BT17-092` | 4881 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Tamer/BT17_092_P0.asset` |
| `BT17-097#3666@base` | `BT17-097` | 3666 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Option/BT17_097.asset` |
| `BT17-097#4887@P0` | `BT17-097` | 4887 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Option/BT17_097_P0.asset` |
| `BT17-097#8243@P1` | `BT17-097` | 8243 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Option/BT17_097_P1.asset` |
| `BT17-097#8244@P2` | `BT17-097` | 8244 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Option/BT17_097_P2.asset` |
| `BT17-097#8245@P3` | `BT17-097` | 8245 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Option/BT17_097_P3.asset` |
| `BT17-097#8246@P4` | `BT17-097` | 8246 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Option/BT17_097_P4.asset` |
| `BT17-100#3669@base` | `BT17-100` | 3669 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Black/Option/BT17_100.asset` |
| `BT17-100#4891@P0` | `BT17-100` | 4891 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Black/Option/BT17_100_P0.asset` |
| `BT18-030#3882@base` | `BT18-030` | 3882 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_030.asset` |
| `BT18-036#3891@base` | `BT18-036` | 3891 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_036.asset` |

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
