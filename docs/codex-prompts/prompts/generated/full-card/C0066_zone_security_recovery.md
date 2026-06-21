# C0066_zone_security_recovery - zone/security/recovery card porting 60

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0066_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 31
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT8_094` | `DCGO/Assets/Scripts/CardEffect/BT8/White/BT8_094.cs` | `OnDestroyedAnyone, OnMove, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 5 |
| `BT9_002` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_002.cs` | `OnAddHand` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 4 |
| `BT9_003` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_003.cs` | `OnAddSecurity` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 4 |
| `BT9_018` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_018.cs` | `OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT9_021` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_021.cs` | `OnAddHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |
| `BT9_033` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_033.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 4 |
| `BT9_058` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_058.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 4 |
| `BT9_066` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_066.cs` | `OnAddDigivolutionCards, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `BT9_069` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_069.cs` | `OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT9_072` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_072.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT14-009#2927@base` | `BT14-009` | 2927 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_009.asset` |
| `BT8-094#1687@base` | `BT8-094` | 1687 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Tamer/BT8_094.asset` |
| `BT8-094#6802@P0` | `BT8-094` | 6802 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Tamer/BT8_094_P0.asset` |
| `BT8-094#6803@P1` | `BT8-094` | 6803 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Tamer/BT8_094_P1.asset` |
| `BT8-094#6804@P2` | `BT8-094` | 6804 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Tamer/BT8_094_P2.asset` |
| `BT8-094#8920@P3` | `BT8-094` | 8920 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Tamer/BT8_094_P3.asset` |
| `BT9-002#1777@base` | `BT9-002` | 1777 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/DigiEgg/BT9_002.asset` |
| `BT9-002#1778@P1` | `BT9-002` | 1778 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_002_P1.asset` |
| `BT9-002#8938@P1` | `BT9-002` | 8938 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/DigiEgg/BT9_002_P1.asset` |
| `BT9-002#8939@P0` | `BT9-002` | 8939 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/DigiEgg/BT9_002_P0.asset` |
| `BT9-003#1779@base` | `BT9-003` | 1779 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/DigiEgg/BT9_003.asset` |
| `BT9-003#1780@P1` | `BT9-003` | 1780 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_003_P1.asset` |
| `BT9-003#8940@P1` | `BT9-003` | 8940 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/DigiEgg/BT9_003_P1.asset` |
| `BT9-003#8941@P0` | `BT9-003` | 8941 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/DigiEgg/BT9_003_P0.asset` |
| `BT9-018#1802@base` | `BT9-018` | 1802 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_018.asset` |
| `BT9-018#8957@P0` | `BT9-018` | 8957 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_018_P0.asset` |
| `BT9-021#1806@base` | `BT9-021` | 1806 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_021.asset` |
| `BT9-021#1807@P1` | `BT9-021` | 1807 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_021_P1.asset` |
| `BT9-021#8960@P0` | `BT9-021` | 8960 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_021_P0.asset` |
| `BT9-033#1820@base` | `BT9-033` | 1820 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_033.asset` |
| `BT9-033#8965@P1` | `BT9-033` | 8965 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_033_P1.asset` |
| `BT9-047#1835@base` | `BT9-047` | 1835 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_047.asset` |
| `BT9-058#1847@base` | `BT9-058` | 1847 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_058.asset` |
| `BT9-058#1848@P1` | `BT9-058` | 1848 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_058_P1.asset` |
| `BT9-058#8982@P0` | `BT9-058` | 8982 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_058_P0.asset` |
| `BT9-058#8983@P2` | `BT9-058` | 8983 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_058_P2.asset` |
| `BT9-066#1857@base` | `BT9-066` | 1857 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_066.asset` |
| `BT9-066#1858@P1` | `BT9-066` | 1858 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_066_P1.asset` |
| `BT9-066#8986@P0` | `BT9-066` | 8986 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_066_P0.asset` |
| `BT9-069#1862@base` | `BT9-069` | 1862 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_069.asset` |
| `BT9-072#1865@base` | `BT9-072` | 1865 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_072.asset` |

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
