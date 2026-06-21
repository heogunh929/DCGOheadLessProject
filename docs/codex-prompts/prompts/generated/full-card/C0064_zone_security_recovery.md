# C0064_zone_security_recovery - zone/security/recovery card porting 58

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0064_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT8_022` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_022.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT8_028` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_028.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT8_029` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_029.cs` | `None, OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT8_033` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_033.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 3 |
| `BT8_035` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_035.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT8_040` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_040.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand` | 1 |
| `BT8_049` | `DCGO/Assets/Scripts/CardEffect/BT8/Green/BT8_049.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous` | `SelectCard` | 2 |
| `BT8_050` | `DCGO/Assets/Scripts/CardEffect/BT8/Green/BT8_050.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT8_055` | `DCGO/Assets/Scripts/CardEffect/BT8/Green/BT8_055.cs` | `OnEnterFieldAnyone, OnUnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT8_057` | `DCGO/Assets/Scripts/CardEffect/BT8/Green/BT8_057.cs` | `None, OnUnTappedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT8-022#1584@base` | `BT8-022` | 1584 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_022.asset` |
| `BT8-028#1591@base` | `BT8-028` | 1591 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_028.asset` |
| `BT8-029#1592@base` | `BT8-029` | 1592 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_029.asset` |
| `BT8-033#1598@base` | `BT8-033` | 1598 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_033.asset` |
| `BT8-033#1599@P1` | `BT8-033` | 1599 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_033_P1.asset` |
| `BT8-033#8862@P0` | `BT8-033` | 8862 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_033_P0.asset` |
| `BT8-035#1601@base` | `BT8-035` | 1601 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_035.asset` |
| `BT8-040#1608@base` | `BT8-040` | 1608 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_040.asset` |
| `BT8-049#1620@base` | `BT8-049` | 1620 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_049.asset` |
| `BT8-049#8873@P0` | `BT8-049` | 8873 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_049_P0.asset` |
| `BT8-050#1621@base` | `BT8-050` | 1621 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_050.asset` |
| `BT8-055#1630@base` | `BT8-055` | 1630 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_055.asset` |
| `BT8-055#8875@P0` | `BT8-055` | 8875 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_055_P0.asset` |
| `BT8-057#1632@base` | `BT8-057` | 1632 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_057.asset` |
| `BT8-057#1633@P1` | `BT8-057` | 1633 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Digimon/BT8_057_P1.asset` |

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
