# C0203_replacement_counter_cut_in - replacement/counter/cut-in card porting 18

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0203_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT22_036` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_036.cs` | `OnDeclaration, OnEndTurn, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT22_038` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_038.cs` | `BeforePayCost, None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 1 |
| `BT22_041` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_041.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `BT22_049` | `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_049.cs` | `None, OnDetermineDoSecurityCheck, OnEndTurn` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectBoolean, SelectSecurity` | 1 |
| `BT22_052` | `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_052.cs` | `None, OnCounterTiming, OnEnterFieldAnyone, WhenRemoveField` | `counter, max_count_per_turn, modifier_duration, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT22_053` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_053.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT22_057` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_057.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT22_061` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_061.cs` | `BeforePayCost, None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectAttackTarget, SelectJogress` | 1 |
| `BT22_062` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_062.cs` | `None, OnCounterTiming, OnEndTurn, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `BT22_066` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_066.cs` | `None, OnCounterTiming, OnEnterFieldAnyone, OnTappedAnyone` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT22-036#7033@base` | `BT22-036` | 7033 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_036.asset` |
| `BT22-036#8430@P1` | `BT22-036` | 8430 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_036_P1.asset` |
| `BT22-038#7035@base` | `BT22-038` | 7035 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_038.asset` |
| `BT22-041#7039@base` | `BT22-041` | 7039 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_041.asset` |
| `BT22-049#7051@base` | `BT22-049` | 7051 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_049.asset` |
| `BT22-052#7054@base` | `BT22-052` | 7054 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_052.asset` |
| `BT22-052#7055@P1` | `BT22-052` | 7055 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_052_P1.asset` |
| `BT22-053#7056@base` | `BT22-053` | 7056 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_053.asset` |
| `BT22-057#7062@base` | `BT22-057` | 7062 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_057.asset` |
| `BT22-057#8435@P1` | `BT22-057` | 8435 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_057_P1.asset` |
| `BT22-061#7067@base` | `BT22-061` | 7067 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_061.asset` |
| `BT22-062#7068@base` | `BT22-062` | 7068 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_062.asset` |
| `BT22-066#7073@base` | `BT22-066` | 7073 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_066.asset` |

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
