# C0201_replacement_counter_cut_in - replacement/counter/cut-in card porting 16

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0201_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT20_077` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_077.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 3 |
| `BT20_078` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_078.cs` | `None, OnCounterTiming, OnDestroyedAnyone, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT20_080` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_080.cs` | `None, OnAddDigivolutionCards, OnDestroyedAnyone, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `BT20_081` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_081.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT20_082` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_082.cs` | `None, OnEndTurn, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT20_089` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_089.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT20_100` | `DCGO/Assets/Scripts/CardEffect/BT20/White/BT20_100.cs` | `OptionSkill, SecuritySkill, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT20_101` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_101.cs` | `None, OnCounterTiming, OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone, OnTappedAnyone` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 5 |
| `BT21_022` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_022.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 4 |
| `BT21_035` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_035.cs` | `None, OnAttackTargetChanged, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectAttackTarget, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT20-077#5156@base` | `BT20-077` | 5156 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_077.asset` |
| `BT20-077#5231@P1` | `BT20-077` | 5231 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_077_P1.asset` |
| `BT20-077#5232@P2` | `BT20-077` | 5232 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_077_P2.asset` |
| `BT20-078#5157@base` | `BT20-078` | 5157 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_078.asset` |
| `BT20-080#5159@base` | `BT20-080` | 5159 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_080.asset` |
| `BT20-081#5160@base` | `BT20-081` | 5160 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_081.asset` |
| `BT20-081#5235@P1` | `BT20-081` | 5235 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_081_P1.asset` |
| `BT20-081#5236@P2` | `BT20-081` | 5236 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_081_P2.asset` |
| `BT20-082#5161@base` | `BT20-082` | 5161 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_082.asset` |
| `BT20-082#5237@P1` | `BT20-082` | 5237 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_082_P1.asset` |
| `BT20-089#5168@base` | `BT20-089` | 5168 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Tamer/BT20_089.asset` |
| `BT20-089#5250@P1` | `BT20-089` | 5250 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Tamer/BT20_089_P1.asset` |
| `BT20-100#5179@base` | `BT20-100` | 5179 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Option/BT20_100.asset` |
| `BT20-100#8364@P1` | `BT20-100` | 8364 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Option/BT20_100_P1.asset` |
| `BT20-101#5180@base` | `BT20-101` | 5180 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_101.asset` |
| `BT20-101#5259@P1` | `BT20-101` | 5259 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_101_P1.asset` |
| `BT20-101#5260@P2` | `BT20-101` | 5260 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_101_P2.asset` |
| `BT20-101#8365@P3` | `BT20-101` | 8365 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_101_P3.asset` |
| `BT20-101#8366@P4` | `BT20-101` | 8366 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_101_P4.asset` |
| `BT21-022#5330@base` | `BT21-022` | 5330 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_022.asset` |
| `BT21-022#8380@P1` | `BT21-022` | 8380 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_022_P1.asset` |
| `BT21-022#8381@P2` | `BT21-022` | 8381 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_022_P2.asset` |
| `BT21-022#8382@P3` | `BT21-022` | 8382 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_022_P3.asset` |
| `BT21-035#5346@base` | `BT21-035` | 5346 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_035.asset` |

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
