# C0077_zone_security_recovery - zone/security/recovery card porting 71

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0077_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX4_045` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_045.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `EX4_046` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_046.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `EX4_052` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_052.cs` | `OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 2 |
| `EX5_001` | `DCGO/Assets/Scripts/CardEffect/EX5/Red/EX5_001.cs` | `OnAddDigivolutionCards` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 5 |
| `EX5_002` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_002.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 2 |
| `EX5_005` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_005.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX5_006` | `DCGO/Assets/Scripts/CardEffect/EX5/Purple/EX5_006.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `EX5_008` | `DCGO/Assets/Scripts/CardEffect/EX5/Red/EX5_008.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `EX5_012` | `DCGO/Assets/Scripts/CardEffect/EX5/Red/EX5_012.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 4 |
| `EX5_014` | `DCGO/Assets/Scripts/CardEffect/EX5/Red/EX5_014.cs` | `None, OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX4-045#2597@base` | `EX4-045` | 2597 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_045.asset` |
| `EX4-046#2598@base` | `EX4-046` | 2598 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_046.asset` |
| `EX4-052#2608@base` | `EX4-052` | 2608 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_052.asset` |
| `EX4-052#4252@P1` | `EX4-052` | 4252 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_052_P1.asset` |
| `EX5-001#3040@base` | `EX5-001` | 3040 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/DigiEgg/EX5_001.asset` |
| `EX5-001#4199@P1` | `EX5-001` | 4199 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/DigiEgg/EX5_001_P1.asset` |
| `EX5-001#9143@P2` | `EX5-001` | 9143 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/DigiEgg/EX5_001_P2.asset` |
| `EX5-001#9144@P3` | `EX5-001` | 9144 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/DigiEgg/EX5_001_P3.asset` |
| `EX5-001#9145@P4` | `EX5-001` | 9145 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/DigiEgg/EX5_001_P4.asset` |
| `EX5-002#3041@base` | `EX5-002` | 3041 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/DigiEgg/EX5_002.asset` |
| `EX5-002#4200@P1` | `EX5-002` | 4200 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/DigiEgg/EX5_002_P1.asset` |
| `EX5-005#3044@base` | `EX5-005` | 3044 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/DigiEgg/EX5_005.asset` |
| `EX5-005#4203@P1` | `EX5-005` | 4203 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/DigiEgg/EX5_005_P1.asset` |
| `EX5-006#3045@base` | `EX5-006` | 3045 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/DigiEgg/EX5_006.asset` |
| `EX5-006#4204@P1` | `EX5-006` | 4204 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/DigiEgg/EX5_006_P1.asset` |
| `EX5-008#3047@base` | `EX5-008` | 3047 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_008.asset` |
| `EX5-012#3051@base` | `EX5-012` | 3051 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_012.asset` |
| `EX5-012#4206@P1` | `EX5-012` | 4206 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_012_P1.asset` |
| `EX5-012#4207@P2` | `EX5-012` | 4207 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_012_P2.asset` |
| `EX5-012#4208@P3` | `EX5-012` | 4208 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_012_P3.asset` |
| `EX5-014#3053@base` | `EX5-014` | 3053 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_014.asset` |
| `EX5-014#4210@P1` | `EX5-014` | 4210 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Digimon/EX5_014_P1.asset` |

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
