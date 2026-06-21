# C0157_attack_security_timing - attack/security timing card porting 30

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0157_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT7_037` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_037.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity` | 1 |
| `BT7_045` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_045.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 3 |
| `BT7_051` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_051.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `BT7_065` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_065.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 3 |
| `BT7_076` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_076.cs` | `OnAllyAttack, OnDiscardHand` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `BT7_086` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_086.cs` | `OnAllyAttack, OnEnterFieldAnyone, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 3 |
| `BT8_001` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT8_008` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_008.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 5 |
| `BT8_010` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_010.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT8_018` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_018.cs` | `None` | `static_or_continuous` | `SelectAttackTarget` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT7-037#1433@base` | `BT7-037` | 1433 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_037.asset` |
| `BT7-045#1446@base` | `BT7-045` | 1446 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_045.asset` |
| `BT7-045#1447@P1` | `BT7-045` | 1447 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_045_P1.asset` |
| `BT7-045#1448@P2` | `BT7-045` | 1448 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_045_P2.asset` |
| `BT7-051#1457@base` | `BT7-051` | 1457 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_051.asset` |
| `BT7-051#8781@P0` | `BT7-051` | 8781 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_051_P0.asset` |
| `BT7-065#1478@base` | `BT7-065` | 1478 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_065.asset` |
| `BT7-065#1479@P1` | `BT7-065` | 1479 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_065_P1.asset` |
| `BT7-065#8799@P2` | `BT7-065` | 8799 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_065_P2.asset` |
| `BT7-076#1496@base` | `BT7-076` | 1496 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_076.asset` |
| `BT7-086#1516@base` | `BT7-086` | 1516 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Tamer/BT7_086.asset` |
| `BT7-086#1517@P1` | `BT7-086` | 1517 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Tamer/BT7_086_P1.asset` |
| `BT7-086#8810@P0` | `BT7-086` | 8810 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Tamer/BT7_086_P0.asset` |
| `BT8-001#1555@base` | `BT8-001` | 1555 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/DigiEgg/BT8_001.asset` |
| `BT8-001#8840@P0` | `BT8-001` | 8840 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/DigiEgg/BT8_001_P0.asset` |
| `BT8-008#1562@base` | `BT8-008` | 1562 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_008.asset` |
| `BT8-008#1563@P1` | `BT8-008` | 1563 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_008_P1.asset` |
| `BT8-008#1564@P2` | `BT8-008` | 1564 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_008_P2.asset` |
| `BT8-008#1565@P3` | `BT8-008` | 1565 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_008_P3.asset` |
| `BT8-008#8846@P0` | `BT8-008` | 8846 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_008_P0.asset` |
| `BT8-010#1568@base` | `BT8-010` | 1568 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_010.asset` |
| `BT8-018#1580@base` | `BT8-018` | 1580 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_018.asset` |
| `BT8-018#8854@P0` | `BT8-018` | 8854 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_018_P0.asset` |

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
