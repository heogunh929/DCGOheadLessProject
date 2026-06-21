# C0312_special_digivolution_play - special digivolution/play mechanics card porting 77

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0312_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT24_072` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_072.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT24_074` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_074.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT24_075` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_075.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT24_077` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_077.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone, WhenLinked` | `inherited, linked, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean` | 1 |
| `BT24_078` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_078.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT24_079` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_079.cs` | `None, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity` | 2 |
| `BT24_080` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_080.cs` | `None, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT24_081` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_081.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress, SelectAssembly` | 2 |
| `BT24_082` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_082.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `BT24_083` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_083.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT24-072#7605@base` | `BT24-072` | 7605 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_072.asset` |
| `BT24-074#7607@base` | `BT24-074` | 7607 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_074.asset` |
| `BT24-075#7608@base` | `BT24-075` | 7608 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_075.asset` |
| `BT24-075#8450@P1` | `BT24-075` | 8450 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_075_P1.asset` |
| `BT24-077#7610@base` | `BT24-077` | 7610 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_077.asset` |
| `BT24-078#7611@base` | `BT24-078` | 7611 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_078.asset` |
| `BT24-078#7612@P1` | `BT24-078` | 7612 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_078_P1.asset` |
| `BT24-079#7613@base` | `BT24-079` | 7613 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_079.asset` |
| `BT24-079#7614@P1` | `BT24-079` | 7614 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_079_P1.asset` |
| `BT24-080#7615@base` | `BT24-080` | 7615 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_080.asset` |
| `BT24-081#7616@base` | `BT24-081` | 7616 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_081.asset` |
| `BT24-081#7617@P1` | `BT24-081` | 7617 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_081_P1.asset` |
| `BT24-082#7618@base` | `BT24-082` | 7618 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Tamer/BT24_082.asset` |
| `BT24-083#7619@base` | `BT24-083` | 7619 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Tamer/BT24_083.asset` |
| `BT24-083#7620@P1` | `BT24-083` | 7620 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Tamer/BT24_083_P1.asset` |
| `BT24-083#7621@P2` | `BT24-083` | 7621 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Tamer/BT24_083_P2.asset` |

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
