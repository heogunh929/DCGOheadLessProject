# C0056_zone_security_recovery - zone/security/recovery card porting 50

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0056_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 35
- Source effect count: 9
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT5_078` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_078.cs` | `OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT5_081` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_081.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `BT5_084` | `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_084.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 4 |
| `BT6_002` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_002.cs` | `OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 4 |
| `BT6_005` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_005.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `SelectCard` | 6 |
| `BT6_006` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_006.cs` | `OnDiscardHand` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 6 |
| `BT6_014` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_014.cs` | `OnEnterFieldAnyone` | `inherited, trigger_when_digivolving, zone_movement` | `-` | 7 |
| `BT6_017` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_017.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 3 |
| `BT6_026` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_026.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT5-078#1041@base` | `BT5-078` | 1041 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_078.asset` |
| `BT5-081#1044@base` | `BT5-081` | 1044 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_081.asset` |
| `BT5-081#1045@P1` | `BT5-081` | 1045 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_081_P1.asset` |
| `BT5-081#1046@P2` | `BT5-081` | 1046 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_081_P2.asset` |
| `BT5-084#1050@base` | `BT5-084` | 1050 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_084.asset` |
| `BT5-084#1051@P1` | `BT5-084` | 1051 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_084_P1.asset` |
| `BT5-084#1052@P2` | `BT5-084` | 1052 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_084_P2.asset` |
| `BT5-084#8622@P0` | `BT5-084` | 8622 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/White/Digimon/BT5_084_P0.asset` |
| `BT6-002#1100@base` | `BT6-002` | 1100 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/DigiEgg/BT6_002.asset` |
| `BT6-002#1101@P1` | `BT6-002` | 1101 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_002_P1.asset` |
| `BT6-002#8663@P1` | `BT6-002` | 8663 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/DigiEgg/BT6_002_P1.asset` |
| `BT6-002#8664@P0` | `BT6-002` | 8664 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/DigiEgg/BT6_002_P0.asset` |
| `BT6-005#1106@base` | `BT6-005` | 1106 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/DigiEgg/BT6_005.asset` |
| `BT6-005#1107@P1` | `BT6-005` | 1107 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_005_P1.asset` |
| `BT6-005#1108@P2` | `BT6-005` | 1108 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_005_P2.asset` |
| `BT6-005#8669@P1` | `BT6-005` | 8669 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/DigiEgg/BT6_005_P1.asset` |
| `BT6-005#8670@P0` | `BT6-005` | 8670 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/DigiEgg/BT6_005_P0.asset` |
| `BT6-005#8671@P2` | `BT6-005` | 8671 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/DigiEgg/BT6_005_P2.asset` |
| `BT6-006#1109@base` | `BT6-006` | 1109 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/DigiEgg/BT6_006.asset` |
| `BT6-006#1110@P1` | `BT6-006` | 1110 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_006_P1.asset` |
| `BT6-006#8672@P1` | `BT6-006` | 8672 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/DigiEgg/BT6_006_P1.asset` |
| `BT6-006#8673@P0` | `BT6-006` | 8673 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/DigiEgg/BT6_006_P0.asset` |
| `BT6-006#8674@P2` | `BT6-006` | 8674 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/DigiEgg/BT6_006_P2.asset` |
| `BT6-006#8675@P3` | `BT6-006` | 8675 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/DigiEgg/BT6_006_P3.asset` |
| `BT6-014#1123@base` | `BT6-014` | 1123 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_014.asset` |
| `BT6-017#1129@base` | `BT6-017` | 1129 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_017.asset` |
| `BT6-017#1130@P1` | `BT6-017` | 1130 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_017_P1.asset` |
| `BT6-017#8681@P0` | `BT6-017` | 8681 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_017_P0.asset` |
| `BT6-026#1142@base` | `BT6-026` | 1142 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_026.asset` |
| `BT8-013#1572@base` | `BT8-013` | 1572 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_013.asset` |
| `BT8-013#1573@P1` | `BT8-013` | 1573 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_013_P1.asset` |
| `BT8-013#1574@P2` | `BT8-013` | 1574 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_013_P2.asset` |
| `BT8-013#1575@P3` | `BT8-013` | 1575 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_013_P3.asset` |
| `BT8-013#6794@P4` | `BT8-013` | 6794 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_013_P4.asset` |
| `BT8-013#6795@P5` | `BT8-013` | 6795 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_013_P5.asset` |

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
