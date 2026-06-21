# C0062_zone_security_recovery - zone/security/recovery card porting 56

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0062_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT7_066` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_066.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `BT7_068` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_068.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 3 |
| `BT7_069` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_069.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 3 |
| `BT7_070` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_070.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `BT7_071` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_071.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 3 |
| `BT7_074` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_074.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `BT7_075` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_075.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `BT7_077` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_077.cs` | `OnDiscardHand, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `BT7_078` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_078.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 4 |
| `BT7_079` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_079.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT7-066#1480@base` | `BT7-066` | 1480 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_066.asset` |
| `BT7-066#8800@P0` | `BT7-066` | 8800 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_066_P0.asset` |
| `BT7-068#1482@base` | `BT7-068` | 1482 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_068.asset` |
| `BT7-068#1483@P1` | `BT7-068` | 1483 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_068_P1.asset` |
| `BT7-068#8801@P0` | `BT7-068` | 8801 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_068_P0.asset` |
| `BT7-069#1484@base` | `BT7-069` | 1484 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_069.asset` |
| `BT7-069#1485@P1` | `BT7-069` | 1485 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_069_P1.asset` |
| `BT7-069#1486@P2` | `BT7-069` | 1486 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_069_P2.asset` |
| `BT7-070#1487@base` | `BT7-070` | 1487 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_070.asset` |
| `BT7-071#1488@base` | `BT7-071` | 1488 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_071.asset` |
| `BT7-071#1489@P1` | `BT7-071` | 1489 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_071_P1.asset` |
| `BT7-071#1490@P2` | `BT7-071` | 1490 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_071_P2.asset` |
| `BT7-074#1494@base` | `BT7-074` | 1494 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_074.asset` |
| `BT7-075#1495@base` | `BT7-075` | 1495 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_075.asset` |
| `BT7-075#8803@P0` | `BT7-075` | 8803 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_075_P0.asset` |
| `BT7-077#1497@base` | `BT7-077` | 1497 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_077.asset` |
| `BT7-077#8804@P0` | `BT7-077` | 8804 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_077_P0.asset` |
| `BT7-078#1498@base` | `BT7-078` | 1498 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_078.asset` |
| `BT7-078#1499@P1` | `BT7-078` | 1499 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_078_P1.asset` |
| `BT7-078#8805@P0` | `BT7-078` | 8805 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_078_P0.asset` |
| `BT7-078#8806@P2` | `BT7-078` | 8806 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_078_P2.asset` |
| `BT7-079#1500@base` | `BT7-079` | 1500 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_079.asset` |
| `BT7-079#1501@P1` | `BT7-079` | 1501 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Digimon/BT7_079_P1.asset` |

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
