# C0148_attack_security_timing - attack/security timing card porting 21

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0148_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 14
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT24_085` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_085.cs` | `OnEndTurn, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectAttackTarget` | 3 |
| `BT25_001` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT25_002` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectSecurity` | 1 |
| `BT25_006` | `DCGO/Assets/Scripts/CardEffect/BT25/Purple/BT25_006.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT25_014` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_014.cs` | `None, OnAllyAttack, OnDeclaration` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT25_015` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_015.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT25_030` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_030.cs` | `None, OnAllyAttack, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectBoolean, SelectSecurity` | 1 |
| `BT25_041` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_041.cs` | `None, OnAllyAttack, OnEndAttack` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectSecurity` | 1 |
| `BT25_055` | `DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_055.cs` | `None, OnAllyAttack, OnTappedAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectAttackTarget` | 1 |
| `BT25_057` | `DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_057.cs` | `None, OnEnterFieldAnyone, OptionSkill` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT24-085#7625@base` | `BT24-085` | 7625 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Tamer/BT24_085.asset` |
| `BT24-085#7626@P1` | `BT24-085` | 7626 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Tamer/BT24_085_P1.asset` |
| `BT24-085#7627@P2` | `BT24-085` | 7627 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Tamer/BT24_085_P2.asset` |
| `BT25-001#7963@base` | `BT25-001` | 7963 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/DigiEgg/BT25_001.asset` |
| `BT25-002#7964@base` | `BT25-002` | 7964 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/DigiEgg/BT25_002.asset` |
| `BT25-006#7968@base` | `BT25-006` | 7968 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/DigiEgg/BT25_006.asset` |
| `BT25-014#7976@base` | `BT25-014` | 7976 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_014.asset` |
| `BT25-015#7977@base` | `BT25-015` | 7977 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_015.asset` |
| `BT25-030#7996@base` | `BT25-030` | 7996 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_030.asset` |
| `BT25-041#8007@base` | `BT25-041` | 8007 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_041.asset` |
| `BT25-055#8024@base` | `BT25-055` | 8024 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_055.asset` |
| `BT25-057#8026@base` | `BT25-057` | 8026 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_057.asset` |
| `BT25-057#8027@P1` | `BT25-057` | 8027 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_057_P1.asset` |
| `BT25-057#8028@P2` | `BT25-057` | 8028 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_057_P2.asset` |

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
