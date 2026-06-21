# C0307_special_digivolution_play - special digivolution/play mechanics card porting 72

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0307_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 14
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT23_044` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_044.cs` | `BeforePayCost, None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT23_047` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_047.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnLoseSecurity, WhenRemoveField` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `BT23_050` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_050.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT23_052` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_052.cs` | `None, OnDeclaration, OnEnterFieldAnyone, SecuritySkill, WhenLinked` | `linked, max_count_per_turn, modifier_duration, security, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT23_053` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_053.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT23_057` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_057.cs` | `BeforePayCost, None, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT23_059` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_059.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT23_065` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_065.cs` | `OnDeclaration, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT23_070` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_070.cs` | `None, OnDetermineDoSecurityCheck, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectAttackTarget, SelectJogress` | 2 |
| `BT23_071` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_071.cs` | `None, OnDestroyedAnyone, OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT23-044#7376@base` | `BT23-044` | 7376 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_044.asset` |
| `BT23-047#7380@base` | `BT23-047` | 7380 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_047.asset` |
| `BT23-047#7381@P1` | `BT23-047` | 7381 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_047_P1.asset` |
| `BT23-050#7384@base` | `BT23-050` | 7384 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_050.asset` |
| `BT23-052#7386@base` | `BT23-052` | 7386 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_052.asset` |
| `BT23-053#7387@base` | `BT23-053` | 7387 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_053.asset` |
| `BT23-057#7391@base` | `BT23-057` | 7391 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_057.asset` |
| `BT23-059#7393@base` | `BT23-059` | 7393 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_059.asset` |
| `BT23-065#7400@base` | `BT23-065` | 7400 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_065.asset` |
| `BT23-065#8448@P1` | `BT23-065` | 8448 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_065_P1.asset` |
| `BT23-070#7406@base` | `BT23-070` | 7406 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_070.asset` |
| `BT23-070#7407@P1` | `BT23-070` | 7407 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_070_P1.asset` |
| `BT23-071#7408@base` | `BT23-071` | 7408 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_071.asset` |
| `BT23-071#7409@P1` | `BT23-071` | 7409 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_071_P1.asset` |

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
