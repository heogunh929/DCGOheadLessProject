# C0151_attack_security_timing - attack/security timing card porting 24

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0151_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 31
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT3_002` | `DCGO/Assets/Scripts/CardEffect/BT3/Blue/BT3_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 6 |
| `BT3_003` | `DCGO/Assets/Scripts/CardEffect/BT3/Yellow/BT3_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity` | 6 |
| `BT3_004` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_004.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 6 |
| `BT3_005` | `DCGO/Assets/Scripts/CardEffect/BT3/Black/BT3_005.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT3_017` | `DCGO/Assets/Scripts/CardEffect/BT3/Red/BT3_017.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT3_039` | `DCGO/Assets/Scripts/CardEffect/BT3/Yellow/BT3_039.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 4 |
| `BT3_041` | `DCGO/Assets/Scripts/CardEffect/BT3/Yellow/BT3_041.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `BT3_042` | `DCGO/Assets/Scripts/CardEffect/BT3/Yellow/BT3_042.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT3_058` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_058.cs` | `OnAllyAttack, OnDetermineDoSecurityCheck` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT3_064` | `DCGO/Assets/Scripts/CardEffect/BT3/Black/BT3_064.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT3-002#588@base` | `BT3-002` | 588 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/DigiEgg/BT3_002.asset` |
| `BT3-002#589@P1` | `BT3-002` | 589 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_002_P1.asset` |
| `BT3-002#590@P2` | `BT3-002` | 590 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_002_P2.asset` |
| `BT3-002#8454@P1` | `BT3-002` | 8454 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/DigiEgg/BT3_002_P1.asset` |
| `BT3-002#8455@P2` | `BT3-002` | 8455 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/DigiEgg/BT3_002_P2.asset` |
| `BT3-002#8456@P3` | `BT3-002` | 8456 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/DigiEgg/BT3_002_P3.asset` |
| `BT3-003#591@base` | `BT3-003` | 591 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/DigiEgg/BT3_003.asset` |
| `BT3-003#592@P1` | `BT3-003` | 592 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_003_P1.asset` |
| `BT3-003#593@P2` | `BT3-003` | 593 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_003_P2.asset` |
| `BT3-003#8457@P1` | `BT3-003` | 8457 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/DigiEgg/BT3_003_P1.asset` |
| `BT3-003#8458@P2` | `BT3-003` | 8458 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/DigiEgg/BT3_003_P2.asset` |
| `BT3-003#8459@P3` | `BT3-003` | 8459 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/DigiEgg/BT3_003_P3.asset` |
| `BT3-004#594@base` | `BT3-004` | 594 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/DigiEgg/BT3_004.asset` |
| `BT3-004#595@P1` | `BT3-004` | 595 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_004_P1.asset` |
| `BT3-004#596@P2` | `BT3-004` | 596 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_004_P2.asset` |
| `BT3-004#8460@P1` | `BT3-004` | 8460 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/DigiEgg/BT3_004_P1.asset` |
| `BT3-004#8461@P2` | `BT3-004` | 8461 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/DigiEgg/BT3_004_P2.asset` |
| `BT3-004#8462@P3` | `BT3-004` | 8462 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/DigiEgg/BT3_004_P3.asset` |
| `BT3-005#597@base` | `BT3-005` | 597 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/DigiEgg/BT3_005.asset` |
| `BT3-017#613@base` | `BT3-017` | 613 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_017.asset` |
| `BT3-039#650@base` | `BT3-039` | 650 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_039.asset` |
| `BT3-039#651@P1` | `BT3-039` | 651 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_039_P1.asset` |
| `BT3-039#8473@P2` | `BT3-039` | 8473 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_039_P2.asset` |
| `BT3-039#8474@P3` | `BT3-039` | 8474 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_039_P3.asset` |
| `BT3-041#653@base` | `BT3-041` | 653 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_041.asset` |
| `BT3-042#654@base` | `BT3-042` | 654 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_042.asset` |
| `BT3-058#681@base` | `BT3-058` | 681 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_058.asset` |
| `BT3-058#8479@P1` | `BT3-058` | 8479 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_058_P1.asset` |
| `BT3-064#689@base` | `BT3-064` | 689 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_064.asset` |
| `BT3-069#696@base` | `BT3-069` | 696 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_069.asset` |
| `BT4-001#759@base` | `BT4-001` | 759 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/DigiEgg/BT4_001.asset` |

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
