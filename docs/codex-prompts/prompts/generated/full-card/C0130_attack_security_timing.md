# C0130_attack_security_timing - attack/security timing card porting 3

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0130_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 30
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT11_076` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_076.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |
| `BT11_087` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_087.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, OnMove` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT11_092` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_092.cs` | `OnAllyAttack, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 5 |
| `BT12_002` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 5 |
| `BT12_008` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_008.cs` | `None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT12_014` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_014.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT12_018` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_018.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 6 |
| `BT12_019` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_019.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT12_035` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_035.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT12_039` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_039.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-076#2351@base` | `BT11-076` | 2351 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_076.asset` |
| `BT11-076#4424@P0` | `BT11-076` | 4424 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_076_P0.asset` |
| `BT11-076#4425@P1` | `BT11-076` | 4425 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_076_P1.asset` |
| `BT11-087#2364@base` | `BT11-087` | 2364 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_087.asset` |
| `BT11-087#4432@P0` | `BT11-087` | 4432 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_087_P0.asset` |
| `BT11-092#2373@base` | `BT11-092` | 2373 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Tamer/BT11_092.asset` |
| `BT11-092#4436@P0` | `BT11-092` | 4436 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Tamer/BT11_092_P0.asset` |
| `BT11-092#4437@P1` | `BT11-092` | 4437 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Tamer/BT11_092_P1.asset` |
| `BT11-092#8110@P2` | `BT11-092` | 8110 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Tamer/BT11_092_P2.asset` |
| `BT11-092#8111@P3` | `BT11-092` | 8111 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Tamer/BT11_092_P3.asset` |
| `BT12-002#2400@base` | `BT12-002` | 2400 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/DigiEgg/BT12_002.asset` |
| `BT12-002#4449@P0` | `BT12-002` | 4449 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/DigiEgg/BT12_002_P0.asset` |
| `BT12-002#4450@P1` | `BT12-002` | 4450 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/DigiEgg/BT12_002_P1.asset` |
| `BT12-002#8115@P2` | `BT12-002` | 8115 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/DigiEgg/BT12_002_P2.asset` |
| `BT12-002#8116@P3` | `BT12-002` | 8116 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/DigiEgg/BT12_002_P3.asset` |
| `BT12-008#2407@base` | `BT12-008` | 2407 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_008.asset` |
| `BT12-014#2417@base` | `BT12-014` | 2417 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_014.asset` |
| `BT12-014#2418@P1` | `BT12-014` | 2418 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_014_P1.asset` |
| `BT12-018#2424@base` | `BT12-018` | 2424 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_018.asset` |
| `BT12-018#2425@P1` | `BT12-018` | 2425 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_018_P1.asset` |
| `BT12-018#2426@P2` | `BT12-018` | 2426 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_018_P2.asset` |
| `BT12-018#4475@P3` | `BT12-018` | 4475 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_018_P3.asset` |
| `BT12-018#4476@P4` | `BT12-018` | 4476 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_018_P4.asset` |
| `BT12-018#4477@P5` | `BT12-018` | 4477 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_018_P5.asset` |
| `BT12-019#2427@base` | `BT12-019` | 2427 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_019.asset` |
| `BT12-023#2431@base` | `BT12-023` | 2431 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_023.asset` |
| `BT12-035#2445@base` | `BT12-035` | 2445 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_035.asset` |
| `BT12-035#4494@P0` | `BT12-035` | 4494 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_035_P0.asset` |
| `BT12-039#2449@base` | `BT12-039` | 2449 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_039.asset` |
| `BT12-040#2450@base` | `BT12-040` | 2450 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_040.asset` |

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
