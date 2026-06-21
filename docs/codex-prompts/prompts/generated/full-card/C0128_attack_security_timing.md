# C0128_attack_security_timing - attack/security timing card porting 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0128_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `AD1_007` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_007.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectInteger, SelectOrder, SelectAttackTarget` | 2 |
| `BT10_002` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `BT10_003` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 6 |
| `BT10_023` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_023.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand` | 3 |
| `BT10_027` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_027.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT10_035` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_035.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT10_038` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_038.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT10_040` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_040.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT10_054` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_054.cs` | `OnAllyAttack, OnEndBattle, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `BT10_059` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_059.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectOrder` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `AD1-007#7828@base` | `AD1-007` | 7828 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007.asset` |
| `AD1-007#7829@P1` | `AD1-007` | 7829 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007_P1.asset` |
| `BT10-002#2032@base` | `BT10-002` | 2032 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/DigiEgg/BT10_002.asset` |
| `BT10-002#4281@P0` | `BT10-002` | 4281 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/DigiEgg/BT10_002_P0.asset` |
| `BT10-003#2033@base` | `BT10-003` | 2033 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/DigiEgg/BT10_003.asset` |
| `BT10-003#2034@P1` | `BT10-003` | 2034 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_003_P1.asset` |
| `BT10-003#2035@P2` | `BT10-003` | 2035 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_003_P2.asset` |
| `BT10-003#4282@P0` | `BT10-003` | 4282 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/DigiEgg/BT10_003_P0.asset` |
| `BT10-003#4283@P1` | `BT10-003` | 4283 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/DigiEgg/BT10_003_P1.asset` |
| `BT10-003#4284@P2` | `BT10-003` | 4284 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/DigiEgg/BT10_003_P2.asset` |
| `BT10-023#2058@base` | `BT10-023` | 2058 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_023.asset` |
| `BT10-023#2059@P1` | `BT10-023` | 2059 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_023_P1.asset` |
| `BT10-023#4306@P0` | `BT10-023` | 4306 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_023_P0.asset` |
| `BT10-027#2065@base` | `BT10-027` | 2065 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_027.asset` |
| `BT10-035#2073@base` | `BT10-035` | 2073 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_035.asset` |
| `BT10-038#2076@base` | `BT10-038` | 2076 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_038.asset` |
| `BT10-038#4310@P0` | `BT10-038` | 4310 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_038_P0.asset` |
| `BT10-040#2078@base` | `BT10-040` | 2078 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_040.asset` |
| `BT10-040#4312@P0` | `BT10-040` | 4312 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_040_P0.asset` |
| `BT10-054#2095@base` | `BT10-054` | 2095 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_054.asset` |
| `BT10-054#2096@P1` | `BT10-054` | 2096 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_054_P1.asset` |
| `BT10-054#4325@P0` | `BT10-054` | 4325 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_054_P0.asset` |
| `BT10-059#2102@base` | `BT10-059` | 2102 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_059.asset` |
| `BT10-059#4328@P0` | `BT10-059` | 4328 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_059_P0.asset` |

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
