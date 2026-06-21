# C0079_zone_security_recovery - zone/security/recovery card porting 73

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0079_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 14
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX5_063` | `DCGO/Assets/Scripts/CardEffect/EX5/Purple/EX5_063.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `EX5_064` | `DCGO/Assets/Scripts/CardEffect/EX5/Red/EX5_064.cs` | `OnDeclaration, OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `EX6_004` | `DCGO/Assets/Scripts/CardEffect/EX6/Green/EX6_004.cs` | `OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `EX6_005` | `DCGO/Assets/Scripts/CardEffect/EX6/Black/EX6_005.cs` | `OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `EX6_008` | `DCGO/Assets/Scripts/CardEffect/EX6/Red/EX6_008.cs` | `None, OnAddDigivolutionCards, OnDeclaration` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `EX6_013` | `DCGO/Assets/Scripts/CardEffect/EX6/Blue/EX6_013.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `-` | 1 |
| `EX6_015` | `DCGO/Assets/Scripts/CardEffect/EX6/Blue/EX6_015.cs` | `OnAddDigivolutionCards, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX6_036` | `DCGO/Assets/Scripts/CardEffect/EX6/Black/EX6_036.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `EX6_039` | `DCGO/Assets/Scripts/CardEffect/EX6/Black/EX6_039.cs` | `BeforePayCost, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `EX6_040` | `DCGO/Assets/Scripts/CardEffect/EX6/Black/EX6_040.cs` | `None, OnAddDigivolutionCards, OnDeclaration` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX5-063#3102@base` | `EX5-063` | 3102 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/Digimon/EX5_063.asset` |
| `EX5-063#4235@P1` | `EX5-063` | 4235 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/Digimon/EX5_063_P1.asset` |
| `EX5-063#9148@P2` | `EX5-063` | 9148 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/Digimon/EX5_063_P2.asset` |
| `EX5-064#3103@base` | `EX5-064` | 3103 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Red/Tamer/EX5_064.asset` |
| `EX6-004#3441@base` | `EX6-004` | 3441 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Green/DigiEgg/EX6_004.asset` |
| `EX6-005#3442@base` | `EX6-005` | 3442 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/DigiEgg/EX6_005.asset` |
| `EX6-005#3443@P1` | `EX6-005` | 3443 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/DigiEgg/EX6_005_P1.asset` |
| `EX6-008#3446@base` | `EX6-008` | 3446 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Red/Digimon/EX6_008.asset` |
| `EX6-013#3453@base` | `EX6-013` | 3453 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Blue/Digimon/EX6_013.asset` |
| `EX6-015#3455@base` | `EX6-015` | 3455 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Blue/Digimon/EX6_015.asset` |
| `EX6-015#3456@P1` | `EX6-015` | 3456 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Blue/Digimon/EX6_015_P1.asset` |
| `EX6-036#3488@base` | `EX6-036` | 3488 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/Digimon/EX6_036.asset` |
| `EX6-039#3491@base` | `EX6-039` | 3491 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/Digimon/EX6_039.asset` |
| `EX6-040#3492@base` | `EX6-040` | 3492 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/Digimon/EX6_040.asset` |

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
