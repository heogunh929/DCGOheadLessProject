# C0166_attack_security_timing - attack/security timing card porting 39

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0166_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX4_057` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_057.cs` | `None, OnAllyAttack, OnEndAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `EX4_059` | `DCGO/Assets/Scripts/CardEffect/EX4/Purple/EX4_059.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `EX5_025` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_025.cs` | `None, OnAllyAttack, OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 3 |
| `EX5_028` | `DCGO/Assets/Scripts/CardEffect/EX5/Yellow/EX5_028.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 1 |
| `EX5_031` | `DCGO/Assets/Scripts/CardEffect/EX5/Yellow/EX5_031.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectSecurity` | 1 |
| `EX5_074` | `DCGO/Assets/Scripts/CardEffect/EX5/Yellow/EX5_074.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 2 |
| `EX6_002` | `DCGO/Assets/Scripts/CardEffect/EX6/Blue/EX6_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 2 |
| `EX6_003` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 2 |
| `EX6_009` | `DCGO/Assets/Scripts/CardEffect/EX6/Red/EX6_009.cs` | `None, OnAddDigivolutionCards, OnAttackTargetChanged, OnDeclaration` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget` | 1 |
| `EX6_014` | `DCGO/Assets/Scripts/CardEffect/EX6/Blue/EX6_014.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX4-057#2614@base` | `EX4-057` | 2614 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_057.asset` |
| `EX4-057#3299@P1` | `EX4-057` | 3299 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_057_P1.asset` |
| `EX4-059#2617@base` | `EX4-059` | 2617 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Purple/Digimon/EX4_059.asset` |
| `EX5-025#3064@base` | `EX5-025` | 3064 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_025.asset` |
| `EX5-025#4216@P1` | `EX5-025` | 4216 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_025_P1.asset` |
| `EX5-025#9146@P2` | `EX5-025` | 9146 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_025_P2.asset` |
| `EX5-028#3067@base` | `EX5-028` | 3067 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_028.asset` |
| `EX5-031#3070@base` | `EX5-031` | 3070 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_031.asset` |
| `EX5-074#3113@base` | `EX5-074` | 3113 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_074.asset` |
| `EX5-074#4239@P1` | `EX5-074` | 4239 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_074_P1.asset` |
| `EX6-002#3437@base` | `EX6-002` | 3437 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Blue/DigiEgg/EX6_002.asset` |
| `EX6-002#3438@P1` | `EX6-002` | 3438 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Blue/DigiEgg/EX6_002_P1.asset` |
| `EX6-003#3439@base` | `EX6-003` | 3439 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/DigiEgg/EX6_003.asset` |
| `EX6-003#3440@P1` | `EX6-003` | 3440 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/DigiEgg/EX6_003_P1.asset` |
| `EX6-009#3447@base` | `EX6-009` | 3447 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Red/Digimon/EX6_009.asset` |
| `EX6-014#3454@base` | `EX6-014` | 3454 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Blue/Digimon/EX6_014.asset` |

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
