# C0308_special_digivolution_play - special digivolution/play mechanics card porting 73

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0308_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT23_072` | `DCGO/Assets/Scripts/CardEffect/BT23/White/BT23_072.cs` | `OnDeclaration, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT23_074` | `DCGO/Assets/Scripts/CardEffect/BT23/White/BT23_074.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT23_075` | `DCGO/Assets/Scripts/CardEffect/BT23/White/BT23_075.cs` | `None, OnEndTurn, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT23_076` | `DCGO/Assets/Scripts/CardEffect/BT23/White/BT23_076.cs` | `OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT23_077` | `DCGO/Assets/Scripts/CardEffect/BT23/White/BT23_077.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT23_079` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_079.cs` | `OnStartMainPhase, SecuritySkill, WhenLinked` | `linked, max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectAppFusion` | 1 |
| `BT23_082` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_082.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `BT23_084` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_084.cs` | `OnAllyAttack, OnEndTurn, OnStartMainPhase, SecuritySkill` | `background, inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT23_087` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_087.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `BT23_097` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_097.cs` | `OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT23-072#7410@base` | `BT23-072` | 7410 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Digimon/BT23_072.asset` |
| `BT23-074#7412@base` | `BT23-074` | 7412 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Digimon/BT23_074.asset` |
| `BT23-075#7413@base` | `BT23-075` | 7413 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Digimon/BT23_075.asset` |
| `BT23-076#7414@base` | `BT23-076` | 7414 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Digimon/BT23_076.asset` |
| `BT23-077#7415@base` | `BT23-077` | 7415 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Digimon/BT23_077.asset` |
| `BT23-079#7418@base` | `BT23-079` | 7418 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Tamer/BT23_079.asset` |
| `BT23-082#7424@base` | `BT23-082` | 7424 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Tamer/BT23_082.asset` |
| `BT23-082#7425@P1` | `BT23-082` | 7425 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Tamer/BT23_082_P1.asset` |
| `BT23-084#7428@base` | `BT23-084` | 7428 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Tamer/BT23_084.asset` |
| `BT23-084#7429@P1` | `BT23-084` | 7429 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Tamer/BT23_084_P1.asset` |
| `BT23-084#7430@P2` | `BT23-084` | 7430 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Tamer/BT23_084_P2.asset` |
| `BT23-087#7436@base` | `BT23-087` | 7436 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Tamer/BT23_087.asset` |
| `BT23-097#7451@base` | `BT23-097` | 7451 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Option/BT23_097.asset` |

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
