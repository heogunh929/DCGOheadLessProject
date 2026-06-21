# C0160_attack_security_timing - attack/security timing card porting 33

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0160_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX10_002` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_002.cs` | `OnAttackTargetChanged` | `inherited, max_count_per_turn, static_or_continuous` | `SelectAttackTarget` | 2 |
| `EX10_003` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `EX10_008` | `DCGO/Assets/Scripts/CardEffect/EX10/Red/EX10_008.cs` | `None, OnAttackTargetChanged, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget` | 1 |
| `EX10_009` | `DCGO/Assets/Scripts/CardEffect/EX10/Red/EX10_009.cs` | `OnAllyAttack, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectAttackTarget` | 2 |
| `EX10_040` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_040.cs` | `OnAllyAttack, OnStartMainPhase` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `EX10_046` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_046.cs` | `OnAllyAttack, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |
| `EX10_049` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_049.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX11_002` | `DCGO/Assets/Scripts/CardEffect/EX11/Blue/EX11_002.cs` | `None` | `inherited, static_or_continuous` | `SelectAttackTarget` | 2 |
| `EX11_013` | `DCGO/Assets/Scripts/CardEffect/EX11/Blue/EX11_013.cs` | `OnEndAttack, OnEnterFieldAnyone, OnMove` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `EX11_024` | `DCGO/Assets/Scripts/CardEffect/EX11/Yellow/EX11_024.cs` | `OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX10-002#7134@base` | `EX10-002` | 7134 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/DigiEgg/EX10_002.asset` |
| `EX10-002#7270@P1` | `EX10-002` | 7270 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/DigiEgg/EX10_002_P1.asset` |
| `EX10-003#7136@base` | `EX10-003` | 7136 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/DigiEgg/EX10_003.asset` |
| `EX10-003#7271@P1` | `EX10-003` | 7271 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/DigiEgg/EX10_003_P1.asset` |
| `EX10-008#7146@base` | `EX10-008` | 7146 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_008.asset` |
| `EX10-009#7147@base` | `EX10-009` | 7147 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_009.asset` |
| `EX10-009#7276@P1` | `EX10-009` | 7276 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_009_P1.asset` |
| `EX10-040#7205@base` | `EX10-040` | 7205 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_040.asset` |
| `EX10-040#7303@P1` | `EX10-040` | 7303 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_040_P1.asset` |
| `EX10-046#7217@base` | `EX10-046` | 7217 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_046.asset` |
| `EX10-046#7309@P1` | `EX10-046` | 7309 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_046_P1.asset` |
| `EX10-049#7222@base` | `EX10-049` | 7222 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_049.asset` |
| `EX10-049#7311@P1` | `EX10-049` | 7311 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_049_P1.asset` |
| `EX11-002#7659@base` | `EX11-002` | 7659 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/DigiEgg/EX11_002.asset` |
| `EX11-002#7660@P1` | `EX11-002` | 7660 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/DigiEgg/EX11_002_P1.asset` |
| `EX11-013#7681@base` | `EX11-013` | 7681 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_013.asset` |
| `EX11-013#7682@P1` | `EX11-013` | 7682 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_013_P1.asset` |
| `EX11-024#7704@base` | `EX11-024` | 7704 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_024.asset` |
| `EX11-024#7705@P1` | `EX11-024` | 7705 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_024_P1.asset` |
| `EX11-024#7706@P2` | `EX11-024` | 7706 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_024_P2.asset` |

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
