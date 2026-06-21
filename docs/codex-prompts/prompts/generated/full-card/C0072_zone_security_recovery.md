# C0072_zone_security_recovery - zone/security/recovery card porting 66

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0072_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX1_058` | `DCGO/Assets/Scripts/CardEffect/EX1/Purple/EX1_058.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `EX1_060` | `DCGO/Assets/Scripts/CardEffect/EX1/Purple/EX1_060.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |
| `EX1_064` | `DCGO/Assets/Scripts/CardEffect/EX1/Purple/EX1_064.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `EX1_066` | `DCGO/Assets/Scripts/CardEffect/EX1/White/EX1_066.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 6 |
| `EX2_003` | `DCGO/Assets/Scripts/CardEffect/EX2/Yellow/EX2_003.cs` | `OnUseOption` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `EX2_004` | `DCGO/Assets/Scripts/CardEffect/EX2/Green/EX2_004.cs` | `OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `EX2_006` | `DCGO/Assets/Scripts/CardEffect/EX2/Purple/EX2_006.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `-` | 3 |
| `EX2_016` | `DCGO/Assets/Scripts/CardEffect/EX2/Blue/EX2_016.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `EX2_017` | `DCGO/Assets/Scripts/CardEffect/EX2/Blue/EX2_017.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX2_018` | `DCGO/Assets/Scripts/CardEffect/EX2/Blue/EX2_018.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX1-058#1356@base` | `EX1-058` | 1356 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_058.asset` |
| `EX1-060#1359@base` | `EX1-060` | 1359 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_060.asset` |
| `EX1-060#9093@P1` | `EX1-060` | 9093 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_060_P1.asset` |
| `EX1-064#1364@base` | `EX1-064` | 1364 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_064.asset` |
| `EX1-066#1367@base` | `EX1-066` | 1367 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Tamer/EX1_066.asset` |
| `EX1-066#1368@P1` | `EX1-066` | 1368 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Tamer/EX1_066_P1.asset` |
| `EX1-066#9095@P2` | `EX1-066` | 9095 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Tamer/EX1_066_P2.asset` |
| `EX1-066#9096@P3` | `EX1-066` | 9096 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Tamer/EX1_066_P3.asset` |
| `EX1-066#9097@P4` | `EX1-066` | 9097 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Tamer/EX1_066_P4.asset` |
| `EX1-066#9098@P5` | `EX1-066` | 9098 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Tamer/EX1_066_P5.asset` |
| `EX2-003#1917@base` | `EX2-003` | 1917 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/DigiEgg/EX2_003.asset` |
| `EX2-003#1918@P1` | `EX2-003` | 1918 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_003_P1.asset` |
| `EX2-003#9105@P1` | `EX2-003` | 9105 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/DigiEgg/EX2_003_P1.asset` |
| `EX2-004#1919@base` | `EX2-004` | 1919 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/DigiEgg/EX2_004.asset` |
| `EX2-004#1920@P1` | `EX2-004` | 1920 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Digimon/EX2_004_P1.asset` |
| `EX2-004#9106@P1` | `EX2-004` | 9106 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/DigiEgg/EX2_004_P1.asset` |
| `EX2-006#1922@base` | `EX2-006` | 1922 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/DigiEgg/EX2_006.asset` |
| `EX2-006#1923@P1` | `EX2-006` | 1923 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_006_P1.asset` |
| `EX2-006#9107@P1` | `EX2-006` | 9107 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/DigiEgg/EX2_006_P1.asset` |
| `EX2-016#1942@base` | `EX2-016` | 1942 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Digimon/EX2_016.asset` |
| `EX2-017#1943@base` | `EX2-017` | 1943 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Digimon/EX2_017.asset` |
| `EX2-017#1944@P1` | `EX2-017` | 1944 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Digimon/EX2_017_P1.asset` |
| `EX2-018#1945@base` | `EX2-018` | 1945 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Digimon/EX2_018.asset` |
| `EX2-018#1946@P1` | `EX2-018` | 1946 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Digimon/EX2_018_P1.asset` |

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
