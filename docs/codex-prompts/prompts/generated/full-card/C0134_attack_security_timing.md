# C0134_attack_security_timing - attack/security timing card porting 7

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0134_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT14_072` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_072.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 1 |
| `BT14_075` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_075.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT14_080` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_080.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `BT14_088` | `DCGO/Assets/Scripts/CardEffect/BT14/White/BT14_088.cs` | `OnAllyAttack, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 3 |
| `BT15_003` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectBoolean, SelectSecurity` | 3 |
| `BT15_004` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_004.cs` | `OnEndTurn` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectAttackTarget` | 3 |
| `BT15_008` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_008.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT15_010` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_010.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT15_015` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_015.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectAttackTarget` | 1 |
| `BT15_029` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_029.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT14-072#3000@base` | `BT14-072` | 3000 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_072.asset` |
| `BT14-075#3003@base` | `BT14-075` | 3003 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_075.asset` |
| `BT14-075#4681@P0` | `BT14-075` | 4681 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_075_P0.asset` |
| `BT14-080#3008@base` | `BT14-080` | 3008 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_080.asset` |
| `BT14-080#4688@P0` | `BT14-080` | 4688 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_080_P0.asset` |
| `BT14-088#3023@base` | `BT14-088` | 3023 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/White/Tamer/BT14_088.asset` |
| `BT14-088#4700@P0` | `BT14-088` | 4700 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/White/Tamer/BT14_088_P0.asset` |
| `BT14-088#4701@P1` | `BT14-088` | 4701 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/White/Tamer/BT14_088_P1.asset` |
| `BT15-003#3118@base` | `BT15-003` | 3118 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/DigiEgg/BT15_003.asset` |
| `BT15-003#3119@P1` | `BT15-003` | 3119 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/DigiEgg/BT15_003_P1.asset` |
| `BT15-003#4710@P0` | `BT15-003` | 4710 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/DigiEgg/BT15_003_P0.asset` |
| `BT15-004#3120@base` | `BT15-004` | 3120 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/DigiEgg/BT15_004.asset` |
| `BT15-004#3121@P1` | `BT15-004` | 3121 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/DigiEgg/BT15_004_P1.asset` |
| `BT15-004#4711@P0` | `BT15-004` | 4711 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/DigiEgg/BT15_004_P0.asset` |
| `BT15-008#3128@base` | `BT15-008` | 3128 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_008.asset` |
| `BT15-010#3130@base` | `BT15-010` | 3130 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_010.asset` |
| `BT15-015#3137@base` | `BT15-015` | 3137 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_015.asset` |
| `BT15-029#3154@base` | `BT15-029` | 3154 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_029.asset` |
| `BT15-029#4724@P0` | `BT15-029` | 4724 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_029_P0.asset` |

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
