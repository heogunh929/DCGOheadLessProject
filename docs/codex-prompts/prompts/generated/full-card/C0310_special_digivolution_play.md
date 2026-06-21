# C0310_special_digivolution_play - special digivolution/play mechanics card porting 75

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0310_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 11
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT24_025` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_025.cs` | `None, OnEndTurn, OnUnTappedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT24_026` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_026.cs` | `None, OnAllyAttack, OnDiscardHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT24_027` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_027.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT24_028` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_028.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, OnUnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT24_032` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_032.cs` | `None, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `linked, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT24_036` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_036.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone, SecuritySkill` | `linked, max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT24_037` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_037.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `BT24_038` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_038.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone, WhenLinked` | `inherited, linked, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean` | 1 |
| `BT24_042` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_042.cs` | `None, OnDiscardHand` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT24_044` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_044.cs` | `OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT24-025#7547@base` | `BT24-025` | 7547 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_025.asset` |
| `BT24-026#7548@base` | `BT24-026` | 7548 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_026.asset` |
| `BT24-027#7549@base` | `BT24-027` | 7549 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_027.asset` |
| `BT24-028#7550@base` | `BT24-028` | 7550 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_028.asset` |
| `BT24-032#7556@base` | `BT24-032` | 7556 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_032.asset` |
| `BT24-036#7562@base` | `BT24-036` | 7562 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_036.asset` |
| `BT24-037#7563@base` | `BT24-037` | 7563 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_037.asset` |
| `BT24-038#7564@base` | `BT24-038` | 7564 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_038.asset` |
| `BT24-042#7570@base` | `BT24-042` | 7570 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_042.asset` |
| `BT24-044#7572@base` | `BT24-044` | 7572 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_044.asset` |
| `BT24-044#7573@P1` | `BT24-044` | 7573 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_044_P1.asset` |

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
