# C0180_attack_security_timing - attack/security timing card porting 53

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0180_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 14
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST22_13` | `DCGO/Assets/Scripts/CardEffect/ST22/Green/ST22_13.cs` | `OnAllyAttack, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `ST23_01` | `DCGO/Assets/Scripts/CardEffect/ST23/Green/ST23_01.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `ST23_04` | `DCGO/Assets/Scripts/CardEffect/ST23/Yellow/ST23_04.cs` | `None, OnAllyAttack, OnEndAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `ST23_08` | `DCGO/Assets/Scripts/CardEffect/ST23/Green/ST23_08.cs` | `None, OnAllyAttack, OnEndAttack` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `ST24_01` | `DCGO/Assets/Scripts/CardEffect/ST24/Yellow/ST24_01.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `ST24_02` | `DCGO/Assets/Scripts/CardEffect/ST24/Blue/ST24_02.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `ST24_03` | `DCGO/Assets/Scripts/CardEffect/ST24/Blue/ST24_03.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `ST24_12` | `DCGO/Assets/Scripts/CardEffect/ST24/Purple/ST24_12.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `ST2_03` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_03.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `ST2_06` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_06.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT3-023#625@base` | `BT3-023` | 625 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_023.asset` |
| `BT3-026#631@base` | `BT3-026` | 631 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_026.asset` |
| `BT8-025#1587@base` | `BT8-025` | 1587 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_025.asset` |
| `ST2-03#48@base` | `ST2-03` | 48 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_03.asset` |
| `ST2-06#51@base` | `ST2-06` | 51 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_06.asset` |
| `ST2-06#52@P1` | `ST2-06` | 52 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_06_P1.asset` |
| `ST22-13#7504@base` | `ST22-13` | 7504 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Green/Digimon/ST22_13.asset` |
| `ST23-01#7933@base` | `ST23-01` | 7933 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Green/DigiEgg/ST23_01.asset` |
| `ST23-04#7939@base` | `ST23-04` | 7939 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Yellow/Digimon/ST23_04.asset` |
| `ST23-08#7947@base` | `ST23-08` | 7947 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Green/Digimon/ST23_08.asset` |
| `ST24-01#7910@base` | `ST24-01` | 7910 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Yellow/DigiEgg/ST24_01.asset` |
| `ST24-02#7911@base` | `ST24-02` | 7911 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Blue/Digimon/ST24_02.asset` |
| `ST24-03#7912@base` | `ST24-03` | 7912 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Blue/Digimon/ST24_03.asset` |
| `ST24-12#7921@base` | `ST24-12` | 7921 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Purple/Digimon/ST24_12.asset` |

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
