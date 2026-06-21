# C0048_zone_security_recovery - zone/security/recovery card porting 42

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0048_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 32
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT2_041` | `DCGO/Assets/Scripts/CardEffect/BT2/Yellow/BT2_041.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `BT2_044` | `DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_044.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |
| `BT2_066` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_066.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectCount` | 5 |
| `BT2_068` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_068.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 6 |
| `BT2_069` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_069.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 4 |
| `BT2_070` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_070.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous` | `-` | 4 |
| `BT2_071` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_071.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT2_077` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_077.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 3 |
| `BT2_080` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_080.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT2_083` | `DCGO/Assets/Scripts/CardEffect/BT2/White/BT2_083.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-041#435@base` | `BT2-041` | 435 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_041.asset` |
| `BT2-041#436@P1` | `BT2-041` | 436 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_041_P1.asset` |
| `BT2-041#8312@P2` | `BT2-041` | 8312 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_041_P2.asset` |
| `BT2-044#439@base` | `BT2-044` | 439 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_044.asset` |
| `BT2-044#440@P1` | `BT2-044` | 440 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_044_P1.asset` |
| `BT2-066#481@base` | `BT2-066` | 481 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_066.asset` |
| `BT2-066#482@P1` | `BT2-066` | 482 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_066_P1.asset` |
| `BT2-066#483@P2` | `BT2-066` | 483 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_066_P2.asset` |
| `BT2-066#484@P3` | `BT2-066` | 484 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_066_P3.asset` |
| `BT2-066#485@P4` | `BT2-066` | 485 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_066_P4.asset` |
| `BT2-068#489@base` | `BT2-068` | 489 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_068.asset` |
| `BT2-068#490@P1` | `BT2-068` | 490 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_068_P1.asset` |
| `BT2-068#491@P2` | `BT2-068` | 491 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_068_P2.asset` |
| `BT2-068#8314@P3` | `BT2-068` | 8314 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_068_P3.asset` |
| `BT2-068#8315@P4` | `BT2-068` | 8315 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_068_P4.asset` |
| `BT2-068#8316@P0` | `BT2-068` | 8316 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_068_P0.asset` |
| `BT2-069#492@base` | `BT2-069` | 492 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_069.asset` |
| `BT2-069#493@P1` | `BT2-069` | 493 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_069_P1.asset` |
| `BT2-069#8317@P2` | `BT2-069` | 8317 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_069_P2.asset` |
| `BT2-070#494@base` | `BT2-070` | 494 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_070.asset` |
| `BT2-070#495@P1` | `BT2-070` | 495 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_070_P1.asset` |
| `BT2-070#8318@P2` | `BT2-070` | 8318 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_070_P2.asset` |
| `BT2-070#8319@P3` | `BT2-070` | 8319 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_070_P3.asset` |
| `BT2-071#496@base` | `BT2-071` | 496 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_071.asset` |
| `BT2-076#505@base` | `BT2-076` | 505 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_076.asset` |
| `BT2-077#506@base` | `BT2-077` | 506 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_077.asset` |
| `BT2-077#507@P1` | `BT2-077` | 507 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_077_P1.asset` |
| `BT2-077#508@P2` | `BT2-077` | 508 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_077_P2.asset` |
| `BT2-080#512@base` | `BT2-080` | 512 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_080.asset` |
| `BT2-080#513@P1` | `BT2-080` | 513 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_080_P1.asset` |
| `BT2-083#518@base` | `BT2-083` | 518 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/White/Digimon/BT2_083.asset` |
| `BT2-083#519@P1` | `BT2-083` | 519 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/White/Digimon/BT2_083_P1.asset` |

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
