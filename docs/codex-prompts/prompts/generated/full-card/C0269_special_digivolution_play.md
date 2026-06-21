# C0269_special_digivolution_play - special digivolution/play mechanics card porting 34

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0269_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT16_067` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_067.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT16_069` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_069.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT16_071` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_071.cs` | `None, OnAllyAttack, OnEndAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectBoolean, SelectJogress` | 1 |
| `BT16_072` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_072.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT16_073` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_073.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT16_076` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_076.cs` | `None, OnDestroyedAnyone, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 4 |
| `BT16_077` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_077.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectJogress` | 3 |
| `BT16_079` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_079.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `BT16_083` | `DCGO/Assets/Scripts/CardEffect/BT16/White/BT16_083.cs` | `OnDestroyedAnyone, OnEndTurn` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT16_084` | `DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_084.cs` | `OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT16-067#3385@base` | `BT16-067` | 3385 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_067.asset` |
| `BT16-069#3387@base` | `BT16-069` | 3387 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_069.asset` |
| `BT16-071#3389@base` | `BT16-071` | 3389 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_071.asset` |
| `BT16-072#3390@base` | `BT16-072` | 3390 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_072.asset` |
| `BT16-073#3391@base` | `BT16-073` | 3391 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_073.asset` |
| `BT16-076#3394@base` | `BT16-076` | 3394 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_076.asset` |
| `BT16-076#4808@P0` | `BT16-076` | 4808 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_076_P0.asset` |
| `BT16-076#4809@P1` | `BT16-076` | 4809 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_076_P1.asset` |
| `BT16-076#8207@P2` | `BT16-076` | 8207 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_076_P2.asset` |
| `BT16-077#3395@base` | `BT16-077` | 3395 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_077.asset` |
| `BT16-077#3396@P1` | `BT16-077` | 3396 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_077_P1.asset` |
| `BT16-077#8208@P2` | `BT16-077` | 8208 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_077_P2.asset` |
| `BT16-079#3398@base` | `BT16-079` | 3398 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_079.asset` |
| `BT16-079#4811@P0` | `BT16-079` | 4811 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_079_P0.asset` |
| `BT16-083#3405@base` | `BT16-083` | 3405 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_083.asset` |
| `BT16-083#3406@P1` | `BT16-083` | 3406 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_083_P1.asset` |
| `BT16-084#3407@base` | `BT16-084` | 3407 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Tamer/BT16_084.asset` |
| `BT16-084#3408@P1` | `BT16-084` | 3408 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Tamer/BT16_084_P1.asset` |
| `BT16-084#4816@P0` | `BT16-084` | 4816 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Tamer/BT16_084_P0.asset` |

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
