# C0165_attack_security_timing - attack/security timing card porting 38

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0165_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX2_042` | `DCGO/Assets/Scripts/CardEffect/EX2/Purple/EX2_042.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 1 |
| `EX2_073` | `DCGO/Assets/Scripts/CardEffect/EX2/Red/EX2_073.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity` | 5 |
| `EX3_003` | `DCGO/Assets/Scripts/CardEffect/EX3/Red/EX3_003.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous` | `SelectCard` | 1 |
| `EX3_006` | `DCGO/Assets/Scripts/CardEffect/EX3/Red/EX3_006.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX3_012` | `DCGO/Assets/Scripts/CardEffect/EX3/Red/EX3_012.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectSecurity` | 2 |
| `EX3_022` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_022.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `EX4_018` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_018.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `EX4_019` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_019.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `EX4_029` | `DCGO/Assets/Scripts/CardEffect/EX4/Yellow/EX4_029.cs` | `None, OnAllyAttack, OnEndAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `EX4_031` | `DCGO/Assets/Scripts/CardEffect/EX4/Yellow/EX4_031.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX2-042#1984@base` | `EX2-042` | 1984 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_042.asset` |
| `EX2-073#2026@base` | `EX2-073` | 2026 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_073.asset` |
| `EX2-073#2027@P1` | `EX2-073` | 2027 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_073_P1.asset` |
| `EX2-073#2028@P2` | `EX2-073` | 2028 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_073_P2.asset` |
| `EX2-073#9124@P3` | `EX2-073` | 9124 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_073_P3.asset` |
| `EX2-073#9125@P4` | `EX2-073` | 9125 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_073_P4.asset` |
| `EX3-003#2173@base` | `EX3-003` | 2173 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_003.asset` |
| `EX3-006#2176@base` | `EX3-006` | 2176 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_006.asset` |
| `EX3-009#2179@base` | `EX3-009` | 2179 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_009.asset` |
| `EX3-012#2183@base` | `EX3-012` | 2183 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_012.asset` |
| `EX3-012#2184@P1` | `EX3-012` | 2184 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_012_P1.asset` |
| `EX3-022#2197@base` | `EX3-022` | 2197 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_022.asset` |
| `EX4-018#2564@base` | `EX4-018` | 2564 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_018.asset` |
| `EX4-019#2565@base` | `EX4-019` | 2565 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_019.asset` |
| `EX4-029#2578@base` | `EX4-029` | 2578 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_029.asset` |
| `EX4-031#2581@base` | `EX4-031` | 2581 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_031.asset` |

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
