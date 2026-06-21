# C0227_replacement_counter_cut_in - replacement/counter/cut-in card porting 42

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0227_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `LM_024` | `DCGO/Assets/Scripts/CardEffect/LM/Green/LM_024.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `LM_025` | `DCGO/Assets/Scripts/CardEffect/LM/Black/LM_025.cs` | `OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `LM_026` | `DCGO/Assets/Scripts/CardEffect/LM/Purple/LM_026.cs` | `None, OnCounterTiming, OnEnterFieldAnyone, WhenRemoveField` | `counter, inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 1 |
| `LM_043` | `DCGO/Assets/Scripts/CardEffect/LM/Black/LM_043.cs` | `None, OnCounterTiming, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `LM_044` | `DCGO/Assets/Scripts/CardEffect/LM/Purple/LM_044.cs` | `None, OnCounterTiming, OnDestroyedAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `P_045` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_045.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, static_or_continuous` | `SelectJogress` | 1 |
| `P_056` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_056.cs` | `BeforePayCost, OnEnterFieldAnyone, WhenDigisorption` | `max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_priority, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `P_066` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_066.cs` | `OnEndBattle, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |
| `P_067` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_067.cs` | `OnEndBattle, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `-` | 3 |
| `P_068` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_068.cs` | `OnEndBattle, SecuritySkill` | `max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `LM-024#4034@base` | `LM-024` | 4034 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Digimon/LM_024.asset` |
| `LM-025#4035@base` | `LM-025` | 4035 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Black/Digimon/LM_025.asset` |
| `LM-026#4036@base` | `LM-026` | 4036 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Digimon/LM_026.asset` |
| `LM-043#5444@base` | `LM-043` | 5444 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Black/Digimon/LM_043.asset` |
| `LM-044#5445@base` | `LM-044` | 5445 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Digimon/LM_044.asset` |
| `P-045#6086@base` | `P-045` | 6086 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_045.asset` |
| `P-056#6098@base` | `P-056` | 6098 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_056.asset` |
| `P-066#10375@P1` | `P-066` | 10375 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_066_P1.asset` |
| `P-066#10376@P2` | `P-066` | 10376 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_066_P2.asset` |
| `P-066#6108@base` | `P-066` | 6108 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_066.asset` |
| `P-067#10366@P1` | `P-067` | 10366 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_067_P1.asset` |
| `P-067#10367@P2` | `P-067` | 10367 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_067_P2.asset` |
| `P-067#6109@base` | `P-067` | 6109 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_067.asset` |
| `P-068#10378@P1` | `P-068` | 10378 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_068_P1.asset` |
| `P-068#10379@P2` | `P-068` | 10379 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_068_P2.asset` |
| `P-068#6110@base` | `P-068` | 6110 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_068.asset` |

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
