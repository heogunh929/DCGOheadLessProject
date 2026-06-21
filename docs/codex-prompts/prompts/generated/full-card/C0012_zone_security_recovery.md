# C0012_zone_security_recovery - zone/security/recovery card porting 6

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0012_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT12_004` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_004.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `BT12_005` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_005.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `BT12_006` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_006.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT12_009` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_009.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 2 |
| `BT12_026` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_026.cs` | `OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `BT12_027` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_027.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT12_032` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_032.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `BT12_045` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_045.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT12_064` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_064.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT12_080` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_080.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectOrder` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT12-004#2402@base` | `BT12-004` | 2402 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/DigiEgg/BT12_004.asset` |
| `BT12-004#4453@P0` | `BT12-004` | 4453 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/DigiEgg/BT12_004_P0.asset` |
| `BT12-005#2403@base` | `BT12-005` | 2403 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/DigiEgg/BT12_005.asset` |
| `BT12-005#4454@P0` | `BT12-005` | 4454 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/DigiEgg/BT12_005_P0.asset` |
| `BT12-006#2404@base` | `BT12-006` | 2404 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/DigiEgg/BT12_006.asset` |
| `BT12-006#4455@P0` | `BT12-006` | 4455 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/DigiEgg/BT12_006_P0.asset` |
| `BT12-009#2408@base` | `BT12-009` | 2408 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_009.asset` |
| `BT12-009#4456@P1` | `BT12-009` | 4456 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_009_P1.asset` |
| `BT12-026#2434@base` | `BT12-026` | 2434 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_026.asset` |
| `BT12-026#4483@P0` | `BT12-026` | 4483 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_026_P0.asset` |
| `BT12-027#2435@base` | `BT12-027` | 2435 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_027.asset` |
| `BT12-032#2442@base` | `BT12-032` | 2442 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_032.asset` |
| `BT12-032#4491@P0` | `BT12-032` | 4491 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Digimon/BT12_032_P0.asset` |
| `BT12-045#2457@base` | `BT12-045` | 2457 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_045.asset` |
| `BT12-064#2478@base` | `BT12-064` | 2478 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_064.asset` |
| `BT12-064#2479@P1` | `BT12-064` | 2479 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_064_P1.asset` |
| `BT12-080#2497@base` | `BT12-080` | 2497 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_080.asset` |

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
