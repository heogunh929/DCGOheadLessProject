# C0171_attack_security_timing - attack/security timing card porting 44

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0171_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX9_006` | `DCGO/Assets/Scripts/CardEffect/EX9/Purple/EX9_006.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 2 |
| `EX9_008` | `DCGO/Assets/Scripts/CardEffect/EX9/Red/EX9_008.cs` | `None, OnAllyAttack, OnDeclaration` | `inherited, static_or_continuous` | `-` | 2 |
| `EX9_015` | `DCGO/Assets/Scripts/CardEffect/EX9/Blue/EX9_015.cs` | `None, OnAllyAttack, OnDeclaration` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX9_027` | `DCGO/Assets/Scripts/CardEffect/EX9/Yellow/EX9_027.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX9_033` | `DCGO/Assets/Scripts/CardEffect/EX9/Yellow/EX9_033.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEndTurn` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `LM_002` | `DCGO/Assets/Scripts/CardEffect/LM/Blue/LM_002.cs` | `OnAllyAttack, OnStartMainPhase` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `LM_003` | `DCGO/Assets/Scripts/CardEffect/LM/Blue/LM_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `LM_007` | `DCGO/Assets/Scripts/CardEffect/LM/Yellow/LM_007.cs` | `OnEndAttack, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `-` | 1 |
| `LM_014` | `DCGO/Assets/Scripts/CardEffect/LM/Black/LM_014.cs` | `OnAttackTargetChanged, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectAttackTarget` | 1 |
| `LM_041` | `DCGO/Assets/Scripts/CardEffect/LM/Blue/LM_041.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX9-006#6841@base` | `EX9-006` | 6841 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/DigiEgg/EX9_006.asset` |
| `EX9-006#6842@P1` | `EX9-006` | 6842 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Purple/DigiEgg/EX9_006_P1.asset` |
| `EX9-008#6846@base` | `EX9-008` | 6846 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_008.asset` |
| `EX9-008#6847@P1` | `EX9-008` | 6847 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_008_P1.asset` |
| `EX9-015#6861@base` | `EX9-015` | 6861 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_015.asset` |
| `EX9-015#6862@P1` | `EX9-015` | 6862 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_015_P1.asset` |
| `EX9-027#6886@base` | `EX9-027` | 6886 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_027.asset` |
| `EX9-027#6887@P1` | `EX9-027` | 6887 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_027_P1.asset` |
| `EX9-033#6897@base` | `EX9-033` | 6897 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_033.asset` |
| `EX9-033#6898@P1` | `EX9-033` | 6898 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_033_P1.asset` |
| `EX9-033#9208@P2` | `EX9-033` | 9208 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_033_P2.asset` |
| `LM-002#3248@base` | `LM-002` | 3248 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Digimon/LM_002.asset` |
| `LM-003#3249@base` | `LM-003` | 3249 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Digimon/LM_003.asset` |
| `LM-007#3253@base` | `LM-007` | 3253 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Yellow/Digimon/LM_007.asset` |
| `LM-014#3260@base` | `LM-014` | 3260 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Black/Digimon/LM_014.asset` |
| `LM-041#5442@base` | `LM-041` | 5442 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Digimon/LM_041.asset` |

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
