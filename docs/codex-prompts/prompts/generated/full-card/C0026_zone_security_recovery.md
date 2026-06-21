# C0026_zone_security_recovery - zone/security/recovery card porting 20

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0026_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 11
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT18_052` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_052.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT18_056` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_056.cs` | `None, OnDestroyedAnyone, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT18_057` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_057.cs` | `BeforePayCost, None` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `BT18_058` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_058.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 1 |
| `BT18_059` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_059.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 1 |
| `BT18_062` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_062.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT18_068` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_068.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectBoolean` | 1 |
| `BT18_075` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_075.cs` | `BeforePayCost, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `BT18_080` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_080.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT18_085` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_085.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectCard` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT18-052#3911@base` | `BT18-052` | 3911 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_052.asset` |
| `BT18-056#3915@base` | `BT18-056` | 3915 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_056.asset` |
| `BT18-056#3916@P1` | `BT18-056` | 3916 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_056_P1.asset` |
| `BT18-057#3906@base` | `BT18-057` | 3906 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_057.asset` |
| `BT18-058#3907@base` | `BT18-058` | 3907 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_058.asset` |
| `BT18-059#3908@base` | `BT18-059` | 3908 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_059.asset` |
| `BT18-062#3919@base` | `BT18-062` | 3919 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_062.asset` |
| `BT18-068#3928@base` | `BT18-068` | 3928 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_068.asset` |
| `BT18-075#3938@base` | `BT18-075` | 3938 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_075.asset` |
| `BT18-080#3949@base` | `BT18-080` | 3949 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_080.asset` |
| `BT18-085#3951@base` | `BT18-085` | 3951 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_085.asset` |

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
