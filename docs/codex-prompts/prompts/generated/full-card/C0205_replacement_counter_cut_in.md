# C0205_replacement_counter_cut_in - replacement/counter/cut-in card porting 20

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0205_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT23_035` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_035.cs` | `None, OnEnterFieldAnyone, OnLoseSecurity, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectSecurity` | 1 |
| `BT23_043` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_043.cs` | `None, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT23_045` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_045.cs` | `None, OnCounterTiming, OnEnterFieldAnyone, OnTappedAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity` | 2 |
| `BT23_054` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_054.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT23_055` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_055.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT23_058` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_058.cs` | `None, OnTappedAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT23_066` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_066.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT23_067` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_067.cs` | `BeforePayCost, None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT23_073` | `DCGO/Assets/Scripts/CardEffect/BT23/White/BT23_073.cs` | `BeforePayCost, None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 1 |
| `BT23_080` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_080.cs` | `OnStartMainPhase, SecuritySkill, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT23-035#7367@base` | `BT23-035` | 7367 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_035.asset` |
| `BT23-043#7375@base` | `BT23-043` | 7375 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_043.asset` |
| `BT23-045#7377@base` | `BT23-045` | 7377 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_045.asset` |
| `BT23-045#7378@P1` | `BT23-045` | 7378 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_045_P1.asset` |
| `BT23-054#7388@base` | `BT23-054` | 7388 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_054.asset` |
| `BT23-055#7389@base` | `BT23-055` | 7389 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_055.asset` |
| `BT23-058#7392@base` | `BT23-058` | 7392 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_058.asset` |
| `BT23-066#7401@base` | `BT23-066` | 7401 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_066.asset` |
| `BT23-067#7402@base` | `BT23-067` | 7402 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_067.asset` |
| `BT23-073#7411@base` | `BT23-073` | 7411 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Digimon/BT23_073.asset` |
| `BT23-080#7419@base` | `BT23-080` | 7419 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Tamer/BT23_080.asset` |
| `BT23-080#7420@P1` | `BT23-080` | 7420 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Tamer/BT23_080_P1.asset` |

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
