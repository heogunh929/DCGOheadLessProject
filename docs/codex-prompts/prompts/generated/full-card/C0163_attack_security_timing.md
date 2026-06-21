# C0163_attack_security_timing - attack/security timing card porting 36

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0163_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX1_028` | `DCGO/Assets/Scripts/CardEffect/EX1/Yellow/EX1_028.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectSecurity` | 1 |
| `EX1_029` | `DCGO/Assets/Scripts/CardEffect/EX1/Yellow/EX1_029.cs` | `OnAddSecurity, OnAllyAttack` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |
| `EX1_030` | `DCGO/Assets/Scripts/CardEffect/EX1/Yellow/EX1_030.cs` | `OnAddSecurity, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX1_032` | `DCGO/Assets/Scripts/CardEffect/EX1/Yellow/EX1_032.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity` | 1 |
| `EX1_035` | `DCGO/Assets/Scripts/CardEffect/EX1/Green/EX1_035.cs` | `OnAllyAttack` | `max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |
| `EX1_040` | `DCGO/Assets/Scripts/CardEffect/EX1/Green/EX1_040.cs` | `OnAllyAttack, OnEndBattle` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 2 |
| `EX1_042` | `DCGO/Assets/Scripts/CardEffect/EX1/Green/EX1_042.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `EX1_047` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_047.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `EX1_050` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_050.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `EX1_059` | `DCGO/Assets/Scripts/CardEffect/EX1/Purple/EX1_059.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX1-028#1311@base` | `EX1-028` | 1311 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_028.asset` |
| `EX1-029#1312@base` | `EX1-029` | 1312 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_029.asset` |
| `EX1-029#1313@P1` | `EX1-029` | 1313 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_029_P1.asset` |
| `EX1-030#1314@base` | `EX1-030` | 1314 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_030.asset` |
| `EX1-030#1315@P1` | `EX1-030` | 1315 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_030_P1.asset` |
| `EX1-032#1317@base` | `EX1-032` | 1317 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_032.asset` |
| `EX1-035#1322@base` | `EX1-035` | 1322 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_035.asset` |
| `EX1-040#1330@base` | `EX1-040` | 1330 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_040.asset` |
| `EX1-040#1331@P1` | `EX1-040` | 1331 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_040_P1.asset` |
| `EX1-042#1333@base` | `EX1-042` | 1333 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_042.asset` |
| `EX1-047#1339@base` | `EX1-047` | 1339 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_047.asset` |
| `EX1-050#1346@base` | `EX1-050` | 1346 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_050.asset` |
| `EX1-050#1347@P1` | `EX1-050` | 1347 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_050_P1.asset` |
| `EX1-050#1348@P2` | `EX1-050` | 1348 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_050_P2.asset` |
| `EX1-059#1357@base` | `EX1-059` | 1357 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_059.asset` |
| `EX1-059#1358@P1` | `EX1-059` | 1358 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_059_P1.asset` |

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
