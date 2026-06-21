# C0014_zone_security_recovery - zone/security/recovery card porting 8

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0014_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT13_003` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_003.cs` | `OnLoseSecurity` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT13_006` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_006.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `BT13_007` | `DCGO/Assets/Scripts/CardEffect/BT13/White/BT13_007.cs` | `BeforePayCost, None, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectOrder` | 3 |
| `BT13_011` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_011.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT13_014` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_014.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `BT13_030` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_030.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `BT13_044` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_044.cs` | `None, OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `BT13_048` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_048.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT13_051` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_051.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT13_056` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_056.cs` | `None, OnDeclaration, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT13-003#2645@base` | `BT13-003` | 2645 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/DigiEgg/BT13_003.asset` |
| `BT13-003#4555@P0` | `BT13-003` | 4555 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/DigiEgg/BT13_003_P0.asset` |
| `BT13-006#2648@base` | `BT13-006` | 2648 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/DigiEgg/BT13_006.asset` |
| `BT13-006#4558@P0` | `BT13-006` | 4558 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/DigiEgg/BT13_006_P0.asset` |
| `BT13-007#2649@base` | `BT13-007` | 2649 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/DigiEgg/BT13_007.asset` |
| `BT13-007#2650@P1` | `BT13-007` | 2650 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/DigiEgg/BT13_007_P1.asset` |
| `BT13-007#8139@P2` | `BT13-007` | 8139 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/DigiEgg/BT13_007_P2.asset` |
| `BT13-011#2654@base` | `BT13-011` | 2654 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_011.asset` |
| `BT13-014#2657@base` | `BT13-014` | 2657 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_014.asset` |
| `BT13-014#4562@P0` | `BT13-014` | 4562 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_014_P0.asset` |
| `BT13-030#2677@base` | `BT13-030` | 2677 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_030.asset` |
| `BT13-030#2678@P1` | `BT13-030` | 2678 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_030_P1.asset` |
| `BT13-030#4575@P0` | `BT13-030` | 4575 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_030_P0.asset` |
| `BT13-044#2695@base` | `BT13-044` | 2695 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_044.asset` |
| `BT13-044#4586@P0` | `BT13-044` | 4586 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_044_P0.asset` |
| `BT13-048#2700@base` | `BT13-048` | 2700 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_048.asset` |
| `BT13-048#4588@P0` | `BT13-048` | 4588 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_048_P0.asset` |
| `BT13-051#2703@base` | `BT13-051` | 2703 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_051.asset` |
| `BT13-056#2708@base` | `BT13-056` | 2708 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_056.asset` |
| `BT13-056#2709@P1` | `BT13-056` | 2709 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_056_P1.asset` |
| `BT13-056#4592@P0` | `BT13-056` | 4592 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_056_P0.asset` |

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
