# C0008_zone_security_recovery - zone/security/recovery card porting 2

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0008_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT10_044` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_044.cs` | `OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 3 |
| `BT10_046` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_046.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 4 |
| `BT10_048` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_048.cs` | `OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 3 |
| `BT10_053` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_053.cs` | `OnDeclaration, OnTappedAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `BT10_057` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_057.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `BT10_071` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_071.cs` | `OnDestroyedAnyone` | `inherited, zone_movement` | `-` | 1 |
| `BT10_076` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_076.cs` | `OnDestroyedAnyone, OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `BT10_080` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_080.cs` | `OnDestroyedAnyone, OnDiscardHand, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT10_112` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_112.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity` | 3 |
| `BT11_001` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_001.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT10-044#2084@base` | `BT10-044` | 2084 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_044.asset` |
| `BT10-044#2085@P1` | `BT10-044` | 2085 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_044_P1.asset` |
| `BT10-044#4317@P0` | `BT10-044` | 4317 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_044_P0.asset` |
| `BT10-046#2087@base` | `BT10-046` | 2087 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_046.asset` |
| `BT10-046#4318@P0` | `BT10-046` | 4318 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_046_P0.asset` |
| `BT10-046#4319@P1` | `BT10-046` | 4319 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_046_P1.asset` |
| `BT10-046#8098@P2` | `BT10-046` | 8098 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_046_P2.asset` |
| `BT10-048#2089@base` | `BT10-048` | 2089 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_048.asset` |
| `BT10-048#4320@P1` | `BT10-048` | 4320 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_048_P1.asset` |
| `BT10-048#4321@P2` | `BT10-048` | 4321 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_048_P2.asset` |
| `BT10-053#2094@base` | `BT10-053` | 2094 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_053.asset` |
| `BT10-053#4324@P0` | `BT10-053` | 4324 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_053_P0.asset` |
| `BT10-057#2099@base` | `BT10-057` | 2099 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_057.asset` |
| `BT10-057#2100@P1` | `BT10-057` | 2100 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_057_P1.asset` |
| `BT10-057#8099@P2` | `BT10-057` | 8099 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_057_P2.asset` |
| `BT10-071#2117@base` | `BT10-071` | 2117 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_071.asset` |
| `BT10-076#2122@base` | `BT10-076` | 2122 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_076.asset` |
| `BT10-080#2126@base` | `BT10-080` | 2126 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_080.asset` |
| `BT10-080#4338@P0` | `BT10-080` | 4338 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_080_P0.asset` |
| `BT10-112#2169@base` | `BT10-112` | 2169 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_112.asset` |
| `BT10-112#2170@P1` | `BT10-112` | 2170 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_112_P1.asset` |
| `BT10-112#8104@base` | `BT10-112` | 8104 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_112_P2_J.asset` |
| `BT11-001#2268@base` | `BT11-001` | 2268 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/DigiEgg/BT11_001.asset` |

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
