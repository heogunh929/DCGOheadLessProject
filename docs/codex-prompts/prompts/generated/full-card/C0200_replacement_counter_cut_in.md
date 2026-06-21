# C0200_replacement_counter_cut_in - replacement/counter/cut-in card porting 15

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0200_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 25
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT20_016` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_016.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `BT20_017_token` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_017_token.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, static_or_continuous` | `-` | 0 |
| `BT20_021` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_021.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 5 |
| `BT20_027` | `DCGO/Assets/Scripts/CardEffect/BT20/Blue/BT20_027.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnLoseSecurity, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT20_030` | `DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_030.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT20_031` | `DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_031.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT20_045` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_045.cs` | `None, OnAllyAttack, OnCounterTiming, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnTappedAnyone, WhenPermanentWouldBeDeleted` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 3 |
| `BT20_056` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_056.cs` | `OnEnterFieldAnyone, OnLoseSecurity, WhenPermanentWouldBeDeleted, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 6 |
| `BT20_060` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_060.cs` | `None, OnCounterTiming, OnEnterFieldAnyone, OnLoseSecurity` | `counter, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `BT20_076` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_076.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectBoolean, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT20-016#5095@base` | `BT20-016` | 5095 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_016.asset` |
| `BT20-016#8328@P1` | `BT20-016` | 8328 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_016_P1.asset` |
| `BT20-021#5100@base` | `BT20-021` | 5100 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_021.asset` |
| `BT20-021#5204@P1` | `BT20-021` | 5204 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_021_P1.asset` |
| `BT20-021#5205@P2` | `BT20-021` | 5205 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_021_P2.asset` |
| `BT20-021#8330@P3` | `BT20-021` | 8330 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_021_P3.asset` |
| `BT20-021#8331@P4` | `BT20-021` | 8331 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_021_P4.asset` |
| `BT20-027#5106@base` | `BT20-027` | 5106 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_027.asset` |
| `BT20-030#5109@base` | `BT20-030` | 5109 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_030.asset` |
| `BT20-031#5110@base` | `BT20-031` | 5110 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_031.asset` |
| `BT20-045#5124@base` | `BT20-045` | 5124 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_045.asset` |
| `BT20-045#5215@P1` | `BT20-045` | 5215 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_045_P1.asset` |
| `BT20-045#5216@P2` | `BT20-045` | 5216 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_045_P2.asset` |
| `BT20-056#5135@base` | `BT20-056` | 5135 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_056.asset` |
| `BT20-056#8340@P1` | `BT20-056` | 8340 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_056_P1.asset` |
| `BT20-056#8341@P2` | `BT20-056` | 8341 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_056_P2.asset` |
| `BT20-056#8342@P3` | `BT20-056` | 8342 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_056_P3.asset` |
| `BT20-056#8343@P4` | `BT20-056` | 8343 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_056_P4.asset` |
| `BT20-056#8344@P5` | `BT20-056` | 8344 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_056_P5.asset` |
| `BT20-060#5139@base` | `BT20-060` | 5139 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_060.asset` |
| `BT20-060#5223@P1` | `BT20-060` | 5223 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_060_P1.asset` |
| `BT20-060#5224@P2` | `BT20-060` | 5224 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_060_P2.asset` |
| `BT20-060#8345@P3` | `BT20-060` | 8345 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_060_P3.asset` |
| `BT20-060#8346@P4` | `BT20-060` | 8346 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_060_P4.asset` |
| `BT20-076#5155@base` | `BT20-076` | 5155 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_076.asset` |

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
