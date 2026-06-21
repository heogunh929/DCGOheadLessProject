# C0061_zone_security_recovery - zone/security/recovery card porting 55

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0061_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT7_038` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_038.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity` | 2 |
| `BT7_041` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_041.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity` | 2 |
| `BT7_042` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_042.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 4 |
| `BT7_043` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_043.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 1 |
| `BT7_044` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_044.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 4 |
| `BT7_052` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_052.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 1 |
| `BT7_054` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_054.cs` | `OnDestroyedAnyone, OnEndBattle` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 4 |
| `BT7_055` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_055.cs` | `None, OnEnterFieldAnyone, WhenUntapAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT7_060` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_060.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 1 |
| `BT7_061` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_061.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT7-038#1434@base` | `BT7-038` | 1434 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_038.asset` |
| `BT7-038#8774@P0` | `BT7-038` | 8774 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_038_P0.asset` |
| `BT7-041#1438@base` | `BT7-041` | 1438 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_041.asset` |
| `BT7-041#1439@P1` | `BT7-041` | 1439 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_041_P1.asset` |
| `BT7-042#1440@base` | `BT7-042` | 1440 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_042.asset` |
| `BT7-042#1441@P1` | `BT7-042` | 1441 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_042_P1.asset` |
| `BT7-042#8776@P0` | `BT7-042` | 8776 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_042_P0.asset` |
| `BT7-042#8777@P2` | `BT7-042` | 8777 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_042_P2.asset` |
| `BT7-043#1442@base` | `BT7-043` | 1442 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_043.asset` |
| `BT7-044#1443@base` | `BT7-044` | 1443 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_044.asset` |
| `BT7-044#1444@P1` | `BT7-044` | 1444 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_044_P1.asset` |
| `BT7-044#1445@P2` | `BT7-044` | 1445 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_044_P2.asset` |
| `BT7-044#8778@P0` | `BT7-044` | 8778 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_044_P0.asset` |
| `BT7-052#1458@base` | `BT7-052` | 1458 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_052.asset` |
| `BT7-054#1460@base` | `BT7-054` | 1460 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_054.asset` |
| `BT7-054#1461@P1` | `BT7-054` | 1461 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_054_P1.asset` |
| `BT7-054#8783@P0` | `BT7-054` | 8783 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_054_P0.asset` |
| `BT7-054#8784@P2` | `BT7-054` | 8784 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_054_P2.asset` |
| `BT7-055#1462@base` | `BT7-055` | 1462 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_055.asset` |
| `BT7-060#1468@base` | `BT7-060` | 1468 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_060.asset` |
| `BT7-061#1469@base` | `BT7-061` | 1469 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_061.asset` |

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
