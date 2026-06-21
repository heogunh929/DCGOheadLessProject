# C0232_replacement_counter_cut_in - replacement/counter/cut-in card porting 47

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0232_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 14
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST19_13` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_13.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `ST20_11` | `DCGO/Assets/Scripts/CardEffect/ST20/Black/ST20_11.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `ST21_11` | `DCGO/Assets/Scripts/CardEffect/ST21/Purple/ST21_11.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `ST22_02` | `DCGO/Assets/Scripts/CardEffect/ST22/Yellow/ST22_02.cs` | `OnEnterFieldAnyone, OnMove, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 3 |
| `ST22_03` | `DCGO/Assets/Scripts/CardEffect/ST22/Yellow/ST22_03.cs` | `OnEnterFieldAnyone, OnMove, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `ST22_05` | `DCGO/Assets/Scripts/CardEffect/ST22/Yellow/ST22_05.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 1 |
| `ST22_10` | `DCGO/Assets/Scripts/CardEffect/ST22/Yellow/ST22_10.cs` | `OnDiscardSecurity, OptionSkill, SecuritySkill, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST23_02` | `DCGO/Assets/Scripts/CardEffect/ST23/Yellow/ST23_02.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, static_or_continuous` | `SelectCard` | 1 |
| `ST23_03` | `DCGO/Assets/Scripts/CardEffect/ST23/Yellow/ST23_03.cs` | `BeforePayCost, None, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |
| `ST23_05` | `DCGO/Assets/Scripts/CardEffect/ST23/Yellow/ST23_05.cs` | `None, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST19-13#3845@base` | `ST19-13` | 3845 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_13.asset` |
| `ST20-11#5274@base` | `ST20-11` | 5274 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Black/Digimon/ST20_11.asset` |
| `ST20-11#9062@P1` | `ST20-11` | 9062 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Black/Digimon/ST20_11_P1.asset` |
| `ST21-11#5290@base` | `ST21-11` | 5290 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Purple/Digimon/ST21_11.asset` |
| `ST21-11#9074@P1` | `ST21-11` | 9074 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Purple/Digimon/ST21_11_P1.asset` |
| `ST22-02#7492@base` | `ST22-02` | 7492 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Digimon/ST22_02.asset` |
| `ST22-02#7493@P2` | `ST22-02` | 7493 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Digimon/ST22_02_P2.asset` |
| `ST22-02#9075@P1` | `ST22-02` | 9075 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Digimon/ST22_02_P1.asset` |
| `ST22-03#7494@base` | `ST22-03` | 7494 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Digimon/ST22_03.asset` |
| `ST22-05#7496@base` | `ST22-05` | 7496 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Digimon/ST22_05.asset` |
| `ST22-10#7501@base` | `ST22-10` | 7501 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Option/ST22_10.asset` |
| `ST23-02#7935@base` | `ST23-02` | 7935 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Yellow/Digimon/ST23_02.asset` |
| `ST23-03#7937@base` | `ST23-03` | 7937 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Yellow/Digimon/ST23_03.asset` |
| `ST23-05#7941@base` | `ST23-05` | 7941 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Yellow/Digimon/ST23_05.asset` |

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
