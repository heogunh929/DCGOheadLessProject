# C0149_attack_security_timing - attack/security timing card porting 22

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0149_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT25_065` | `DCGO/Assets/Scripts/CardEffect/BT25/Black/BT25_065.cs` | `None, OnAllyAttack, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT25_082` | `DCGO/Assets/Scripts/CardEffect/BT25/Purple/BT25_082.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean` | 1 |
| `BT25_086` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_086.cs` | `OnEndTurn, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget` | 2 |
| `BT2_007` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_007.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT2_012` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_012.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `BT2_013` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_013.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 4 |
| `BT2_015` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_015.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `BT2_019` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_019.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT2_020` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_020.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 4 |
| `BT2_025` | `DCGO/Assets/Scripts/CardEffect/BT2/Blue/BT2_025.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-007#370@base` | `BT2-007` | 370 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/DigiEgg/BT2_007.asset` |
| `BT2-012#377@base` | `BT2-012` | 377 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_012.asset` |
| `BT2-012#378@P1` | `BT2-012` | 378 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_012_P1.asset` |
| `BT2-013#379@base` | `BT2-013` | 379 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_013.asset` |
| `BT2-013#380@P1` | `BT2-013` | 380 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_013_P1.asset` |
| `BT2-015#382@base` | `BT2-015` | 382 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_015.asset` |
| `BT2-015#383@P1` | `BT2-015` | 383 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_015_P1.asset` |
| `BT2-019#390@base` | `BT2-019` | 390 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_019.asset` |
| `BT2-020#391@base` | `BT2-020` | 391 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_020.asset` |
| `BT2-020#392@P1` | `BT2-020` | 392 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_020_P1.asset` |
| `BT2-020#393@P2` | `BT2-020` | 393 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_020_P2.asset` |
| `BT2-020#394@P3` | `BT2-020` | 394 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_020_P3.asset` |
| `BT2-025#401@base` | `BT2-025` | 401 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_025.asset` |
| `BT2-025#402@P1` | `BT2-025` | 402 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_025_P1.asset` |
| `BT25-065#8038@base` | `BT25-065` | 8038 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Digimon/BT25_065.asset` |
| `BT25-082#8057@base` | `BT25-082` | 8057 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_082.asset` |
| `BT25-086#8064@base` | `BT25-086` | 8064 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Tamer/BT25_086.asset` |
| `BT25-086#8065@P1` | `BT25-086` | 8065 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Tamer/BT25_086_P1.asset` |
| `BT3-012#608@base` | `BT3-012` | 608 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_012.asset` |
| `BT4-028#6778@P0` | `BT4-028` | 6778 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_028_P0.asset` |
| `BT4-028#801@base` | `BT4-028` | 801 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_028.asset` |
| `BT8-011#1569@base` | `BT8-011` | 1569 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_011.asset` |

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
