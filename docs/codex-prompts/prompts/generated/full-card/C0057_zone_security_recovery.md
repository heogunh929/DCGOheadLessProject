# C0057_zone_security_recovery - zone/security/recovery card porting 51

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0057_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT6_029` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_029.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 1 |
| `BT6_031` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_031.cs` | `OnDestroyedAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT6_032` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_032.cs` | `OnLoseSecurity` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT6_033` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_033.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectInteger, SelectSecurity` | 3 |
| `BT6_034` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_034.cs` | `OnLoseSecurity` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 4 |
| `BT6_035` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_035.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectSecurity` | 1 |
| `BT6_036` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_036.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectSecurity` | 2 |
| `BT6_037` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_037.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectSecurity` | 1 |
| `BT6_039` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_039.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectSecurity` | 1 |
| `BT6_040` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_040.cs` | `OnLoseSecurity` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT6-029#1145@base` | `BT6-029` | 1145 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_029.asset` |
| `BT6-031#1148@base` | `BT6-031` | 1148 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_031.asset` |
| `BT6-032#1149@base` | `BT6-032` | 1149 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_032.asset` |
| `BT6-033#1150@base` | `BT6-033` | 1150 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_033.asset` |
| `BT6-033#1151@P1` | `BT6-033` | 1151 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_033_P1.asset` |
| `BT6-033#8688@P0` | `BT6-033` | 8688 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_033_P0.asset` |
| `BT6-034#1152@base` | `BT6-034` | 1152 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_034.asset` |
| `BT6-034#1153@P1` | `BT6-034` | 1153 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_034_P1.asset` |
| `BT6-034#1154@P2` | `BT6-034` | 1154 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_034_P2.asset` |
| `BT6-034#8689@P0` | `BT6-034` | 8689 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_034_P0.asset` |
| `BT6-035#1155@base` | `BT6-035` | 1155 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_035.asset` |
| `BT6-036#1156@base` | `BT6-036` | 1156 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_036.asset` |
| `BT6-036#8690@P0` | `BT6-036` | 8690 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_036_P0.asset` |
| `BT6-037#1157@base` | `BT6-037` | 1157 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_037.asset` |
| `BT6-039#1162@base` | `BT6-039` | 1162 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_039.asset` |
| `BT6-040#1163@base` | `BT6-040` | 1163 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_040.asset` |

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
