# C0145_attack_security_timing - attack/security timing card porting 18

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0145_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 9
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT23_041` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_041.cs` | `None, OnAllyAttack, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT23_046` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_046.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget` | 1 |
| `BT23_048` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_048.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 1 |
| `BT23_051` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_051.cs` | `None, OnAllyAttack, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT23_056` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_056.cs` | `None, OnAttackTargetChanged, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 1 |
| `BT23_057_token` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_057_token.cs` | `None, OnAllyAttack` | `inherited, static_or_continuous` | `-` | 0 |
| `BT23_060` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_060.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |
| `BT23_062` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_062.cs` | `None, OnAllyAttack, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT23_063` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_063.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT23_069` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_069.cs` | `OnAllyAttack, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT23-041#7373@base` | `BT23-041` | 7373 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_041.asset` |
| `BT23-046#7379@base` | `BT23-046` | 7379 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_046.asset` |
| `BT23-048#7382@base` | `BT23-048` | 7382 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_048.asset` |
| `BT23-051#7385@base` | `BT23-051` | 7385 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_051.asset` |
| `BT23-056#7390@base` | `BT23-056` | 7390 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_056.asset` |
| `BT23-060#7394@base` | `BT23-060` | 7394 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_060.asset` |
| `BT23-062#7397@base` | `BT23-062` | 7397 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_062.asset` |
| `BT23-063#7398@base` | `BT23-063` | 7398 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_063.asset` |
| `BT23-069#7405@base` | `BT23-069` | 7405 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_069.asset` |

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
