# C0065_zone_security_recovery - zone/security/recovery card porting 59

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0065_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 35
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT8_058` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_058.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 3 |
| `BT8_059` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_059.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 5 |
| `BT8_066` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_066.cs` | `None, OnAddDigivolutionCards` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |
| `BT8_070` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_070.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `BT8_071` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_071.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 7 |
| `BT8_072` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_072.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT8_074` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_074.cs` | `OnDiscardLibrary` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT8_079` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_079.cs` | `OnDiscardLibrary, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 4 |
| `BT8_088` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_088.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 5 |
| `BT8_090` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_090.cs` | `OnAddSecurity, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT8-058#1634@base` | `BT8-058` | 1634 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_058.asset` |
| `BT8-058#1635@P1` | `BT8-058` | 1635 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_058_P1.asset` |
| `BT8-058#8876@P0` | `BT8-058` | 8876 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_058_P0.asset` |
| `BT8-059#1636@base` | `BT8-059` | 1636 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_059.asset` |
| `BT8-059#8877@P1` | `BT8-059` | 8877 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_059_P1.asset` |
| `BT8-059#8878@P2` | `BT8-059` | 8878 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_059_P2.asset` |
| `BT8-059#8879@P3` | `BT8-059` | 8879 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_059_P3.asset` |
| `BT8-059#8880@P4` | `BT8-059` | 8880 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_059_P4.asset` |
| `BT8-066#1645@base` | `BT8-066` | 1645 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_066.asset` |
| `BT8-070#1651@base` | `BT8-070` | 1651 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_070.asset` |
| `BT8-070#1652@P1` | `BT8-070` | 1652 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_070_P1.asset` |
| `BT8-070#6801@P2` | `BT8-070` | 6801 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_070_P2.asset` |
| `BT8-071#1653@base` | `BT8-071` | 1653 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_071.asset` |
| `BT8-071#8886@P1` | `BT8-071` | 8886 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_071_P1.asset` |
| `BT8-071#8887@P2` | `BT8-071` | 8887 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_071_P2.asset` |
| `BT8-071#8888@P3` | `BT8-071` | 8888 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_071_P3.asset` |
| `BT8-072#1654@base` | `BT8-072` | 1654 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_072.asset` |
| `BT8-072#8889@P0` | `BT8-072` | 8889 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_072_P0.asset` |
| `BT8-074#1656@base` | `BT8-074` | 1656 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_074.asset` |
| `BT8-079#1661@base` | `BT8-079` | 1661 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_079.asset` |
| `BT8-079#1662@P1` | `BT8-079` | 1662 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_079_P1.asset` |
| `BT8-079#8892@P0` | `BT8-079` | 8892 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_079_P0.asset` |
| `BT8-079#8893@P2` | `BT8-079` | 8893 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_079_P2.asset` |
| `BT8-088#1677@base` | `BT8-088` | 1677 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Tamer/BT8_088.asset` |
| `BT8-088#1678@P1` | `BT8-088` | 1678 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Tamer/BT8_088_P1.asset` |
| `BT8-088#8905@P0` | `BT8-088` | 8905 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Tamer/BT8_088_P0.asset` |
| `BT8-088#8906@P2` | `BT8-088` | 8906 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Tamer/BT8_088_P2.asset` |
| `BT8-088#8907@P3` | `BT8-088` | 8907 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Tamer/BT8_088_P3.asset` |
| `BT8-090#1681@base` | `BT8-090` | 1681 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Tamer/BT8_090.asset` |
| `BT8-090#1682@P1` | `BT8-090` | 1682 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Tamer/BT8_090_P1.asset` |
| `BT8-090#8910@P0` | `BT8-090` | 8910 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Tamer/BT8_090_P0.asset` |
| `BT8-090#8911@P2` | `BT8-090` | 8911 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Tamer/BT8_090_P2.asset` |
| `ST12-03#2787@base` | `ST12-03` | 2787 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/Digimon/ST12_03.asset` |
| `ST12-03#4905@P1` | `ST12-03` | 4905 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/Digimon/ST12_03_P1.asset` |
| `ST13-08#2808@base` | `ST13-08` | 2808 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Black/Digimon/ST13_08.asset` |

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
