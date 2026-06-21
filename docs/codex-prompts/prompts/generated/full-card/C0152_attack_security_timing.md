# C0152_attack_security_timing - attack/security timing card porting 25

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0152_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT3_112` | `DCGO/Assets/Scripts/CardEffect/BT3/White/BT3_112.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 3 |
| `BT4_002` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT4_003` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT4_027` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_027.cs` | `None, OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT4_034` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_034.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT4_044` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_044.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT4_048` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_048.cs` | `OnAllyAttack` | `max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |
| `BT4_057` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_057.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT4_075` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_075.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget` | 2 |
| `BT4_078` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_078.cs` | `OnAllyAttack` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT3-112#756@base` | `BT3-112` | 756 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/White/Digimon/BT3_112.asset` |
| `BT3-112#757@P1` | `BT3-112` | 757 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/White/Digimon/BT3_112_P1.asset` |
| `BT3-112#758@P2` | `BT3-112` | 758 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/White/Digimon/BT3_112_P2.asset` |
| `BT4-002#760@base` | `BT4-002` | 760 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/DigiEgg/BT4_002.asset` |
| `BT4-002#8495@P0` | `BT4-002` | 8495 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/DigiEgg/BT4_002_P0.asset` |
| `BT4-003#761@base` | `BT4-003` | 761 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/DigiEgg/BT4_003.asset` |
| `BT4-003#8496@P0` | `BT4-003` | 8496 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/DigiEgg/BT4_003_P0.asset` |
| `BT4-027#6777@P0` | `BT4-027` | 6777 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_027_P0.asset` |
| `BT4-027#800@base` | `BT4-027` | 800 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_027.asset` |
| `BT4-034#809@base` | `BT4-034` | 809 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_034.asset` |
| `BT4-044#822@base` | `BT4-044` | 822 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_044.asset` |
| `BT4-048#826@base` | `BT4-048` | 826 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_048.asset` |
| `BT4-048#827@P1` | `BT4-048` | 827 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_048_P1.asset` |
| `BT4-048#828@P2` | `BT4-048` | 828 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_048_P2.asset` |
| `BT4-057#837@base` | `BT4-057` | 837 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_057.asset` |
| `BT4-075#861@base` | `BT4-075` | 861 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_075.asset` |
| `BT4-075#862@P1` | `BT4-075` | 862 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_075_P1.asset` |
| `BT4-078#8536@P0` | `BT4-078` | 8536 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_078_P0.asset` |
| `BT4-078#865@base` | `BT4-078` | 865 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_078.asset` |
| `BT4-078#866@P1` | `BT4-078` | 866 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_078_P1.asset` |

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
