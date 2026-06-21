# C0231_replacement_counter_cut_in - replacement/counter/cut-in card porting 46

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0231_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST17_06` | `DCGO/Assets/Scripts/CardEffect/ST17/Green/ST17_06.cs` | `None, OnTappedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `ST17_08` | `DCGO/Assets/Scripts/CardEffect/ST17/Green/ST17_08.cs` | `None, OnCounterTiming, OnEndAttack, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 7 |
| `ST17_12` | `DCGO/Assets/Scripts/CardEffect/ST17/Green/ST17_12.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `ST17_13` | `DCGO/Assets/Scripts/CardEffect/ST17/Blue/ST17_13.cs` | `None, OnEndBattle, OnEnterFieldAnyone, SecuritySkill, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST18_08` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_08.cs` | `None, OnEndTurn, SecuritySkill` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean` | 1 |
| `ST19_02` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_02.cs` | `WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in` | `-` | 1 |
| `ST19_07` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_07.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, static_or_continuous` | `-` | 1 |
| `ST19_08` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_08.cs` | `None, OnEndTurn, SecuritySkill` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity` | 1 |
| `ST19_10` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_10.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, static_or_continuous` | `SelectJogress, SelectDigiXros` | 1 |
| `ST19_11` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_11.cs` | `OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST17-06#3275@base` | `ST17-06` | 3275 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_06.asset` |
| `ST17-06#3276@P1` | `ST17-06` | 3276 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_06_P1.asset` |
| `ST17-06#9036@P2` | `ST17-06` | 9036 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_06_P2.asset` |
| `ST17-06#9037@P3` | `ST17-06` | 9037 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_06_P3.asset` |
| `ST17-08#3279@base` | `ST17-08` | 3279 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_08.asset` |
| `ST17-08#3280@P1` | `ST17-08` | 3280 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_08_P1.asset` |
| `ST17-08#4962@P2` | `ST17-08` | 4962 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_08_P2.asset` |
| `ST17-08#4963@P3` | `ST17-08` | 4963 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_08_P3.asset` |
| `ST17-08#4964@P4` | `ST17-08` | 4964 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_08_P4.asset` |
| `ST17-08#9038@P5` | `ST17-08` | 9038 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_08_P5.asset` |
| `ST17-08#9039@P6` | `ST17-08` | 9039 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_08_P6.asset` |
| `ST17-12#3285@base` | `ST17-12` | 3285 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Option/ST17_12.asset` |
| `ST17-12#4971@P0` | `ST17-12` | 4971 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Option/ST17_12_P0.asset` |
| `ST17-13#3286@base` | `ST17-13` | 3286 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Black/Digimon/ST17_13.asset` |
| `ST18-08#3825@base` | `ST18-08` | 3825 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_08.asset` |
| `ST19-02#3834@base` | `ST19-02` | 3834 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_02.asset` |
| `ST19-07#3839@base` | `ST19-07` | 3839 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_07.asset` |
| `ST19-08#3840@base` | `ST19-08` | 3840 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_08.asset` |
| `ST19-10#3842@base` | `ST19-10` | 3842 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_10.asset` |
| `ST19-11#3843@base` | `ST19-11` | 3843 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_11.asset` |

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
