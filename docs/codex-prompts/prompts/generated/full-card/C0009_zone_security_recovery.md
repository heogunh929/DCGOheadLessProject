# C0009_zone_security_recovery - zone/security/recovery card porting 3

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0009_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT11_003` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_003.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `BT11_004` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_004.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT11_005` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_005.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `BT11_007` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_007.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 3 |
| `BT11_011` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_011.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 3 |
| `BT11_016` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_016.cs` | `None, OnDestroyedAnyone, OnLoseSecurity` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT11_024` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_024.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 1 |
| `BT11_027` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_027.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 3 |
| `BT11_028` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_028.cs` | `OnAddHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `BT11_032` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_032.cs` | `OnEnterFieldAnyone, OnUnTappedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-003#2270@base` | `BT11-003` | 2270 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/DigiEgg/BT11_003.asset` |
| `BT11-003#4368@P0` | `BT11-003` | 4368 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/DigiEgg/BT11_003_P0.asset` |
| `BT11-004#2271@base` | `BT11-004` | 2271 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/DigiEgg/BT11_004.asset` |
| `BT11-005#2272@base` | `BT11-005` | 2272 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/DigiEgg/BT11_005.asset` |
| `BT11-005#4369@P0` | `BT11-005` | 4369 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/DigiEgg/BT11_005_P0.asset` |
| `BT11-005#4370@P1` | `BT11-005` | 4370 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/DigiEgg/BT11_005_P1.asset` |
| `BT11-007#2274@base` | `BT11-007` | 2274 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_007.asset` |
| `BT11-007#4371@P0` | `BT11-007` | 4371 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_007_P0.asset` |
| `BT11-007#8105@P1` | `BT11-007` | 8105 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_007_P1.asset` |
| `BT11-011#2278@base` | `BT11-011` | 2278 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_011.asset` |
| `BT11-011#4372@P0` | `BT11-011` | 4372 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_011_P0.asset` |
| `BT11-013#2280@base` | `BT11-013` | 2280 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_013.asset` |
| `BT11-016#2283@base` | `BT11-016` | 2283 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_016.asset` |
| `BT11-016#2284@P1` | `BT11-016` | 2284 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_016_P1.asset` |
| `BT11-024#2294@base` | `BT11-024` | 2294 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_024.asset` |
| `BT11-027#2297@base` | `BT11-027` | 2297 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_027.asset` |
| `BT11-027#4389@P0` | `BT11-027` | 4389 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_027_P0.asset` |
| `BT11-027#4390@P1` | `BT11-027` | 4390 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_027_P1.asset` |
| `BT11-028#2298@base` | `BT11-028` | 2298 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_028.asset` |
| `BT11-028#4391@P0` | `BT11-028` | 4391 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_028_P0.asset` |
| `BT11-032#2302@base` | `BT11-032` | 2302 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_032.asset` |
| `BT11-032#2303@P1` | `BT11-032` | 2303 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_032_P1.asset` |
| `BT11-032#8107@P2` | `BT11-032` | 8107 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_032_P2.asset` |

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
