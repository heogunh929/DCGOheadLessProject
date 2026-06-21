# C0154_attack_security_timing - attack/security timing card porting 27

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0154_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 35
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT5_045` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_045.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 3 |
| `BT5_082` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_082.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectInteger` | 1 |
| `BT5_087` | `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_087.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 5 |
| `BT5_088` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_088.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectCount, SelectSecurity` | 4 |
| `BT5_089` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_089.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 4 |
| `BT5_091` | `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_091.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 3 |
| `BT6_001` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 6 |
| `BT6_003` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity` | 4 |
| `BT6_004` | `DCGO/Assets/Scripts/CardEffect/BT6/Green/BT6_004.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 4 |
| `BT6_008` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_008.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT5-045#1001@base` | `BT5-045` | 1001 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_045.asset` |
| `BT5-045#1002@P1` | `BT5-045` | 1002 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_045_P1.asset` |
| `BT5-045#1003@P2` | `BT5-045` | 1003 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_045_P2.asset` |
| `BT5-082#1047@base` | `BT5-082` | 1047 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_082.asset` |
| `BT5-087#1060@base` | `BT5-087` | 1060 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_087.asset` |
| `BT5-087#1061@P1` | `BT5-087` | 1061 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_087_P1.asset` |
| `BT5-087#1062@P2` | `BT5-087` | 1062 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_087_P2.asset` |
| `BT5-087#8629@P3` | `BT5-087` | 8629 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_087_P3.asset` |
| `BT5-087#8630@P4` | `BT5-087` | 8630 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_087_P4.asset` |
| `BT5-088#1063@base` | `BT5-088` | 1063 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Tamer/BT5_088.asset` |
| `BT5-088#1064@P1` | `BT5-088` | 1064 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Tamer/BT5_088_P1.asset` |
| `BT5-088#8631@P0` | `BT5-088` | 8631 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Tamer/BT5_088_P0.asset` |
| `BT5-088#8632@P2` | `BT5-088` | 8632 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Tamer/BT5_088_P2.asset` |
| `BT5-089#1065@base` | `BT5-089` | 1065 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Tamer/BT5_089.asset` |
| `BT5-089#1066@P1` | `BT5-089` | 1066 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Tamer/BT5_089_P1.asset` |
| `BT5-089#8633@P0` | `BT5-089` | 8633 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Tamer/BT5_089_P0.asset` |
| `BT5-089#8634@P2` | `BT5-089` | 8634 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Tamer/BT5_089_P2.asset` |
| `BT5-091#1069@base` | `BT5-091` | 1069 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_091.asset` |
| `BT5-091#1070@P1` | `BT5-091` | 1070 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_091_P1.asset` |
| `BT5-091#8636@P0` | `BT5-091` | 8636 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Tamer/BT5_091_P0.asset` |
| `BT6-001#1097@base` | `BT6-001` | 1097 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/DigiEgg/BT6_001.asset` |
| `BT6-001#1098@P1` | `BT6-001` | 1098 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_001_P1.asset` |
| `BT6-001#1099@P2` | `BT6-001` | 1099 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_001_P2.asset` |
| `BT6-001#8660@P1` | `BT6-001` | 8660 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/DigiEgg/BT6_001_P1.asset` |
| `BT6-001#8661@P0` | `BT6-001` | 8661 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/DigiEgg/BT6_001_P0.asset` |
| `BT6-001#8662@P2` | `BT6-001` | 8662 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/DigiEgg/BT6_001_P2.asset` |
| `BT6-003#1102@base` | `BT6-003` | 1102 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/DigiEgg/BT6_003.asset` |
| `BT6-003#1103@P1` | `BT6-003` | 1103 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_003_P1.asset` |
| `BT6-003#8665@P1` | `BT6-003` | 8665 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/DigiEgg/BT6_003_P1.asset` |
| `BT6-003#8666@P0` | `BT6-003` | 8666 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/DigiEgg/BT6_003_P0.asset` |
| `BT6-004#1104@base` | `BT6-004` | 1104 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/DigiEgg/BT6_004.asset` |
| `BT6-004#1105@P1` | `BT6-004` | 1105 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_004_P1.asset` |
| `BT6-004#8667@P1` | `BT6-004` | 8667 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/DigiEgg/BT6_004_P1.asset` |
| `BT6-004#8668@P0` | `BT6-004` | 8668 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/DigiEgg/BT6_004_P0.asset` |
| `BT6-008#1113@base` | `BT6-008` | 1113 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_008.asset` |

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
