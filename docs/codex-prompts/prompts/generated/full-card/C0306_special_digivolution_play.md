# C0306_special_digivolution_play - special digivolution/play mechanics card porting 71

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0306_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 11
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT23_022` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_022.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `inherited, linked, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress, SelectAppFusion` | 1 |
| `BT23_026` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_026.cs` | `None, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT23_028` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_028.cs` | `None, OnDeclaration, OnEnterFieldAnyone, SecuritySkill, WhenLinked` | `linked, max_count_per_turn, modifier_duration, security, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT23_029` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_029.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT23_030` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_030.cs` | `None, OnAllyAttack, OnDeclaration` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT23_031` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_031.cs` | `BeforePayCost, None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `BT23_032` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_032.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `BT23_036` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_036.cs` | `BeforePayCost, None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `BT23_039` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_039.cs` | `None, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `linked, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT23_040` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_040.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT23-022#7354@base` | `BT23-022` | 7354 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Digimon/BT23_022.asset` |
| `BT23-026#7358@base` | `BT23-026` | 7358 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_026.asset` |
| `BT23-028#7360@base` | `BT23-028` | 7360 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_028.asset` |
| `BT23-029#7361@base` | `BT23-029` | 7361 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_029.asset` |
| `BT23-030#7362@base` | `BT23-030` | 7362 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_030.asset` |
| `BT23-031#7363@base` | `BT23-031` | 7363 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_031.asset` |
| `BT23-032#7364@base` | `BT23-032` | 7364 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_032.asset` |
| `BT23-032#8447@P1` | `BT23-032` | 8447 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_032_P1.asset` |
| `BT23-036#7368@base` | `BT23-036` | 7368 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_036.asset` |
| `BT23-039#7371@base` | `BT23-039` | 7371 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_039.asset` |
| `BT23-040#7372@base` | `BT23-040` | 7372 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_040.asset` |

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
