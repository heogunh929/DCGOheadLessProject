# C0169_attack_security_timing - attack/security timing card porting 42

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0169_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX7_073` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_073.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 3 |
| `EX8_002` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `EX8_003` | `DCGO/Assets/Scripts/CardEffect/EX8/Yellow/EX8_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX8_004` | `DCGO/Assets/Scripts/CardEffect/EX8/Green/EX8_004.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectAttackTarget` | 2 |
| `EX8_006` | `DCGO/Assets/Scripts/CardEffect/EX8/Purple/EX8_006.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX8_018` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_018.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `EX8_020` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_020.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `EX8_021` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_021.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX8_022` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_022.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX8_024` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_024.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX7-073#3813@base` | `EX7-073` | 3813 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_073.asset` |
| `EX7-073#3814@P1` | `EX7-073` | 3814 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_073_P1.asset` |
| `EX7-073#3815@P2` | `EX7-073` | 3815 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_073_P2.asset` |
| `EX8-002#4051@base` | `EX8-002` | 4051 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/DigiEgg/EX8_002.asset` |
| `EX8-002#4052@P1` | `EX8-002` | 4052 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/DigiEgg/EX8_002_P1.asset` |
| `EX8-003#4053@base` | `EX8-003` | 4053 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/DigiEgg/EX8_003.asset` |
| `EX8-003#4054@P1` | `EX8-003` | 4054 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/DigiEgg/EX8_003_P1.asset` |
| `EX8-004#4055@base` | `EX8-004` | 4055 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/DigiEgg/EX8_004.asset` |
| `EX8-004#4056@P1` | `EX8-004` | 4056 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/DigiEgg/EX8_004_P1.asset` |
| `EX8-006#4059@base` | `EX8-006` | 4059 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/DigiEgg/EX8_006.asset` |
| `EX8-006#4060@P1` | `EX8-006` | 4060 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/DigiEgg/EX8_006_P1.asset` |
| `EX8-018#4082@base` | `EX8-018` | 4082 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_018.asset` |
| `EX8-020#4085@base` | `EX8-020` | 4085 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_020.asset` |
| `EX8-020#4086@P1` | `EX8-020` | 4086 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_020_P1.asset` |
| `EX8-021#4087@base` | `EX8-021` | 4087 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_021.asset` |
| `EX8-021#4088@P1` | `EX8-021` | 4088 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_021_P1.asset` |
| `EX8-022#4089@base` | `EX8-022` | 4089 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_022.asset` |
| `EX8-022#9191@P1` | `EX8-022` | 9191 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_022_P1.asset` |
| `EX8-024#4092@base` | `EX8-024` | 4092 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_024.asset` |

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
