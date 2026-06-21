# C0011_zone_security_recovery - zone/security/recovery card porting 5

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0011_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 25
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT11_068` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_068.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectOrder` | 4 |
| `BT11_077` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_077.cs` | `OnDestroyedAnyone, OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT11_079` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_079.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `BT11_084` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_084.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand` | 1 |
| `BT11_085` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_085.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT11_088` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_088.cs` | `OnAddDigivolutionCards, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `BT11_089` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_089.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 4 |
| `BT11_091` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_091.cs` | `BeforePayCost, None, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 3 |
| `BT11_093` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_093.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectSecurity` | 3 |
| `BT12_003` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_003.cs` | `OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-068#2342@base` | `BT11-068` | 2342 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_068.asset` |
| `BT11-068#4417@P0` | `BT11-068` | 4417 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_068_P0.asset` |
| `BT11-068#4418@P1` | `BT11-068` | 4418 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_068_P1.asset` |
| `BT11-068#4419@P2` | `BT11-068` | 4419 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_068_P2.asset` |
| `BT11-077#2352@base` | `BT11-077` | 2352 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_077.asset` |
| `BT11-077#4426@P0` | `BT11-077` | 4426 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_077_P0.asset` |
| `BT11-079#2354@base` | `BT11-079` | 2354 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_079.asset` |
| `BT11-084#2360@base` | `BT11-084` | 2360 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_084.asset` |
| `BT11-085#2361@base` | `BT11-085` | 2361 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_085.asset` |
| `BT11-085#4430@P0` | `BT11-085` | 4430 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_085_P0.asset` |
| `BT11-088#2365@base` | `BT11-088` | 2365 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_088.asset` |
| `BT11-088#2366@P1` | `BT11-088` | 2366 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_088_P1.asset` |
| `BT11-089#2367@base` | `BT11-089` | 2367 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Tamer/BT11_089.asset` |
| `BT11-089#2368@P1` | `BT11-089` | 2368 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Tamer/BT11_089_P1.asset` |
| `BT11-089#4433@P0` | `BT11-089` | 4433 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Tamer/BT11_089_P0.asset` |
| `BT11-089#8109@P2` | `BT11-089` | 8109 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Tamer/BT11_089_P2.asset` |
| `BT11-091#2371@base` | `BT11-091` | 2371 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Tamer/BT11_091.asset` |
| `BT11-091#2372@P1` | `BT11-091` | 2372 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Tamer/BT11_091_P1.asset` |
| `BT11-091#4435@P0` | `BT11-091` | 4435 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Tamer/BT11_091_P0.asset` |
| `BT11-093#2374@base` | `BT11-093` | 2374 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Tamer/BT11_093.asset` |
| `BT11-093#2375@P1` | `BT11-093` | 2375 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Tamer/BT11_093_P1.asset` |
| `BT11-093#4438@P0` | `BT11-093` | 4438 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Tamer/BT11_093_P0.asset` |
| `BT12-003#2401@base` | `BT12-003` | 2401 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/DigiEgg/BT12_003.asset` |
| `BT12-003#4451@P0` | `BT12-003` | 4451 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/DigiEgg/BT12_003_P0.asset` |
| `BT12-003#4452@P1` | `BT12-003` | 4452 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/DigiEgg/BT12_003_P1.asset` |

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
