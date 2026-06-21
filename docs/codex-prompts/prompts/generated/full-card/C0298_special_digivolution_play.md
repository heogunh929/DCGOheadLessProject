# C0298_special_digivolution_play - special digivolution/play mechanics card porting 63

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0298_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT21_060` | `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_060.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `BT21_061` | `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_061.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `BT21_064` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_064.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectJogress` | 1 |
| `BT21_066` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_066.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectDigiXros` | 2 |
| `BT21_067` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_067.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, SecuritySkill` | `inherited, max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT21_068` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_068.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT21_069` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_069.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `BT21_070` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_070.cs` | `None, OnDeclaration, OnEnterFieldAnyone, SecuritySkill, WhenLinked` | `linked, max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `BT21_071` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_071.cs` | `None, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `linked, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean` | 1 |
| `BT21_074` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_074.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `linked, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT21-060#5375@base` | `BT21-060` | 5375 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_060.asset` |
| `BT21-060#8400@P1` | `BT21-060` | 8400 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_060_P1.asset` |
| `BT21-060#8401@P2` | `BT21-060` | 8401 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_060_P2.asset` |
| `BT21-061#5376@base` | `BT21-061` | 5376 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_061.asset` |
| `BT21-061#8402@P1` | `BT21-061` | 8402 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_061_P1.asset` |
| `BT21-064#5381@base` | `BT21-064` | 5381 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_064.asset` |
| `BT21-066#5383@base` | `BT21-066` | 5383 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_066.asset` |
| `BT21-066#8407@P1` | `BT21-066` | 8407 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_066_P1.asset` |
| `BT21-067#5384@base` | `BT21-067` | 5384 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_067.asset` |
| `BT21-068#5385@base` | `BT21-068` | 5385 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_068.asset` |
| `BT21-069#5386@base` | `BT21-069` | 5386 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_069.asset` |
| `BT21-069#8408@P1` | `BT21-069` | 8408 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_069_P1.asset` |
| `BT21-070#5387@base` | `BT21-070` | 5387 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_070.asset` |
| `BT21-071#5388@base` | `BT21-071` | 5388 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_071.asset` |
| `BT21-074#5391@base` | `BT21-074` | 5391 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_074.asset` |

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
