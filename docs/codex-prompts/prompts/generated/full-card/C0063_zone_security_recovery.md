# C0063_zone_security_recovery - zone/security/recovery card porting 57

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0063_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 31
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT7_080` | `DCGO/Assets/Scripts/CardEffect/BT7/White/BT7_080.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 4 |
| `BT7_081` | `DCGO/Assets/Scripts/CardEffect/BT7/White/BT7_081.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 6 |
| `BT7_088` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_088.cs` | `None, OnEnterFieldAnyone, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 4 |
| `BT7_090` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_090.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 3 |
| `BT7_091` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_091.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone, SecuritySkill` | `inherited, max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 3 |
| `BT8_003` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_003.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |
| `BT8_006` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_006.cs` | `OnDiscardLibrary` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT8_009` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_009.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 3 |
| `BT8_019` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_019.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT8_021` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_021.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT7-080#1502@base` | `BT7-080` | 1502 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_080.asset` |
| `BT7-080#1503@P1` | `BT7-080` | 1503 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_080_P1.asset` |
| `BT7-080#1504@P2` | `BT7-080` | 1504 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_080_P2.asset` |
| `BT7-080#1505@P3` | `BT7-080` | 1505 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_080_P3.asset` |
| `BT7-081#1506@base` | `BT7-081` | 1506 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_081.asset` |
| `BT7-081#1507@P1` | `BT7-081` | 1507 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_081_P1.asset` |
| `BT7-081#1508@P2` | `BT7-081` | 1508 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_081_P2.asset` |
| `BT7-081#1509@P3` | `BT7-081` | 1509 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_081_P3.asset` |
| `BT7-081#1510@P4` | `BT7-081` | 1510 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_081_P4.asset` |
| `BT7-081#8807@P5` | `BT7-081` | 8807 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT7/White/Digimon/BT7_081_P5.asset` |
| `BT7-088#1520@base` | `BT7-088` | 1520 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Tamer/BT7_088.asset` |
| `BT7-088#1521@P1` | `BT7-088` | 1521 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Tamer/BT7_088_P1.asset` |
| `BT7-088#8813@P0` | `BT7-088` | 8813 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Tamer/BT7_088_P0.asset` |
| `BT7-088#8814@P2` | `BT7-088` | 8814 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Tamer/BT7_088_P2.asset` |
| `BT7-090#1524@base` | `BT7-090` | 1524 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Tamer/BT7_090.asset` |
| `BT7-090#8816@P0` | `BT7-090` | 8816 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Tamer/BT7_090_P0.asset` |
| `BT7-090#8817@P1` | `BT7-090` | 8817 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Tamer/BT7_090_P1.asset` |
| `BT7-091#1525@base` | `BT7-091` | 1525 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Tamer/BT7_091.asset` |
| `BT7-091#1526@P1` | `BT7-091` | 1526 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Tamer/BT7_091_P1.asset` |
| `BT7-091#8818@P0` | `BT7-091` | 8818 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/Tamer/BT7_091_P0.asset` |
| `BT8-003#1557@base` | `BT8-003` | 1557 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/DigiEgg/BT8_003.asset` |
| `BT8-003#8842@P0` | `BT8-003` | 8842 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/DigiEgg/BT8_003_P0.asset` |
| `BT8-006#1560@base` | `BT8-006` | 1560 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/DigiEgg/BT8_006.asset` |
| `BT8-006#8845@P0` | `BT8-006` | 8845 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/DigiEgg/BT8_006_P0.asset` |
| `BT8-009#1566@base` | `BT8-009` | 1566 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_009.asset` |
| `BT8-009#1567@P1` | `BT8-009` | 1567 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_009_P1.asset` |
| `BT8-009#8847@P0` | `BT8-009` | 8847 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_009_P0.asset` |
| `BT8-019#1581@base` | `BT8-019` | 1581 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Digimon/BT8_019.asset` |
| `BT8-021#1583@base` | `BT8-021` | 1583 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_021.asset` |
| `BT8-021#8855@P0` | `BT8-021` | 8855 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_021_P0.asset` |
| `BT8-021#8856@P1` | `BT8-021` | 8856 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_021_P1.asset` |

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
