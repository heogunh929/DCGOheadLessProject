# C0144_attack_security_timing - attack/security timing card porting 17

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0144_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 11
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT22_102` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_102.cs` | `OnAllyAttack, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 2 |
| `BT23_001` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT23_002` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT23_003` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_003.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectAttackTarget` | 1 |
| `BT23_014` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_014.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT23_015` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_015.cs` | `BeforePayCost, None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |
| `BT23_017` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_017.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 1 |
| `BT23_020` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_020.cs` | `None, OnAllyAttack, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT23_034` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_034.cs` | `BeforePayCost, None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |
| `BT23_037` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_037.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT22-102#7128@base` | `BT22-102` | 7128 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Tamer/BT22_102.asset` |
| `BT22-102#7129@P1` | `BT22-102` | 7129 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Tamer/BT22_102_P1.asset` |
| `BT23-001#7332@base` | `BT23-001` | 7332 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/DigiEgg/BT23_001.asset` |
| `BT23-002#7333@base` | `BT23-002` | 7333 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/DigiEgg/BT23_002.asset` |
| `BT23-003#7334@base` | `BT23-003` | 7334 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/DigiEgg/BT23_003.asset` |
| `BT23-014#7346@base` | `BT23-014` | 7346 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_014.asset` |
| `BT23-015#7347@base` | `BT23-015` | 7347 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_015.asset` |
| `BT23-017#7349@base` | `BT23-017` | 7349 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Digimon/BT23_017.asset` |
| `BT23-020#7352@base` | `BT23-020` | 7352 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Digimon/BT23_020.asset` |
| `BT23-034#7366@base` | `BT23-034` | 7366 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_034.asset` |
| `BT23-037#7369@base` | `BT23-037` | 7369 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_037.asset` |

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
