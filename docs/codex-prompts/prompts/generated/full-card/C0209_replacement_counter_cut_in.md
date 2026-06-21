# C0209_replacement_counter_cut_in - replacement/counter/cut-in card porting 24

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0209_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT25_068` | `DCGO/Assets/Scripts/CardEffect/BT25/Black/BT25_068.cs` | `None, OnCounterTiming, OnTappedAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT25_073` | `DCGO/Assets/Scripts/CardEffect/BT25/Black/BT25_073.cs` | `None, WhenRemoveField` | `inherited, linked, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `BT25_074` | `DCGO/Assets/Scripts/CardEffect/BT25/Black/BT25_074.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT25_084` | `DCGO/Assets/Scripts/CardEffect/BT25/Purple/BT25_084.cs` | `None, OnDiscardHand, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT25_091` | `DCGO/Assets/Scripts/CardEffect/BT25/Black/BT25_091.cs` | `OnEnterFieldAnyone, OnStartTurn, OnUseOption, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 3 |
| `BT25_097` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_097.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectInteger, SelectSecurity, SelectJogress` | 1 |
| `BT25_100` | `DCGO/Assets/Scripts/CardEffect/BT25/Black/BT25_100.cs` | `None, OnCounterTiming, OnDeclaration, OnDetermineDoSecurityCheck, OptionSkill, SecuritySkill` | `counter, inherited, linked, max_count_per_turn, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT25_101` | `DCGO/Assets/Scripts/CardEffect/BT25/Black/BT25_101.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill, WhenRemoveField` | `inherited, linked, max_count_per_turn, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectJogress` | 1 |
| `BT25_104` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_104.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnMove, OnStartTurn, OptionSkill, WhenPermanentWouldBeDeleted` | `background, inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress, SelectBurstDigivolution` | 3 |
| `BT2_045` | `DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs` | `BeforePayCost, WhenDigisorption` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_priority, zone_movement` | `SelectCard, SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-045#441@base` | `BT2-045` | 441 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_045.asset` |
| `BT25-068#8041@base` | `BT25-068` | 8041 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Digimon/BT25_068.asset` |
| `BT25-073#8046@base` | `BT25-073` | 8046 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Digimon/BT25_073.asset` |
| `BT25-074#8047@base` | `BT25-074` | 8047 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Digimon/BT25_074.asset` |
| `BT25-084#8059@base` | `BT25-084` | 8059 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_084.asset` |
| `BT25-084#8060@P1` | `BT25-084` | 8060 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_084_P1.asset` |
| `BT25-091#8073@base` | `BT25-091` | 8073 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Tamer/BT25_091.asset` |
| `BT25-091#8074@P1` | `BT25-091` | 8074 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Tamer/BT25_091_P1.asset` |
| `BT25-091#8075@P2` | `BT25-091` | 8075 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Tamer/BT25_091_P2.asset` |
| `BT25-097#8083@base` | `BT25-097` | 8083 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Option/BT25_097.asset` |
| `BT25-100#8086@base` | `BT25-100` | 8086 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Option/BT25_100.asset` |
| `BT25-101#8087@base` | `BT25-101` | 8087 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Option/BT25_101.asset` |
| `BT25-104#8092@base` | `BT25-104` | 8092 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_104.asset` |
| `BT25-104#8093@P1` | `BT25-104` | 8093 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_104_P1.asset` |
| `BT25-104#8094@P2` | `BT25-104` | 8094 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_104_P2.asset` |

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
