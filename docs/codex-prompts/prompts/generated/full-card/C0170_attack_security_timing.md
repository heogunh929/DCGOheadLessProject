# C0170_attack_security_timing - attack/security timing card porting 43

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0170_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX8_032` | `DCGO/Assets/Scripts/CardEffect/EX8/Yellow/EX8_032.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX8_034` | `DCGO/Assets/Scripts/CardEffect/EX8/Yellow/EX8_034.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `EX8_050` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_050.cs` | `None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX8_056` | `DCGO/Assets/Scripts/CardEffect/EX8/Purple/EX8_056.cs` | `None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX8_057` | `DCGO/Assets/Scripts/CardEffect/EX8/Purple/EX8_057.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 1 |
| `EX8_058` | `DCGO/Assets/Scripts/CardEffect/EX8/Purple/EX8_058.cs` | `None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX8_059` | `DCGO/Assets/Scripts/CardEffect/EX8/Purple/EX8_059.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX8_069` | `DCGO/Assets/Scripts/CardEffect/EX8/Green/EX8_069.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |
| `EX8_074` | `DCGO/Assets/Scripts/CardEffect/EX8/Green/EX8_074.cs` | `BeforePayCost, None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 4 |
| `EX9_001` | `DCGO/Assets/Scripts/CardEffect/EX9/Red/EX9_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX8-032#4109@base` | `EX8-032` | 4109 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_032.asset` |
| `EX8-032#4110@P1` | `EX8-032` | 4110 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_032_P1.asset` |
| `EX8-034#4112@base` | `EX8-034` | 4112 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_034.asset` |
| `EX8-034#4113@P1` | `EX8-034` | 4113 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_034_P1.asset` |
| `EX8-050#4146@base` | `EX8-050` | 4146 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_050.asset` |
| `EX8-050#4147@P1` | `EX8-050` | 4147 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_050_P1.asset` |
| `EX8-056#4156@base` | `EX8-056` | 4156 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_056.asset` |
| `EX8-056#4157@P1` | `EX8-056` | 4157 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_056_P1.asset` |
| `EX8-057#4158@base` | `EX8-057` | 4158 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_057.asset` |
| `EX8-058#4159@base` | `EX8-058` | 4159 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_058.asset` |
| `EX8-058#4160@P1` | `EX8-058` | 4160 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_058_P1.asset` |
| `EX8-059#4161@base` | `EX8-059` | 4161 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_059.asset` |
| `EX8-059#4162@P1` | `EX8-059` | 4162 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_059_P1.asset` |
| `EX8-069#4183@base` | `EX8-069` | 4183 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Option/EX8_069.asset` |
| `EX8-069#4184@P1` | `EX8-069` | 4184 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Option/EX8_069_P1.asset` |
| `EX8-074#4195@base` | `EX8-074` | 4195 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_074.asset` |
| `EX8-074#4196@P1` | `EX8-074` | 4196 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_074_P1.asset` |
| `EX8-074#4198@P2` | `EX8-074` | 4198 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_074_P2.asset` |
| `EX8-074#9205@P3` | `EX8-074` | 9205 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_074_P3.asset` |
| `EX9-001#6831@base` | `EX9-001` | 6831 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/DigiEgg/EX9_001.asset` |
| `EX9-001#6832@P1` | `EX9-001` | 6832 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/DigiEgg/EX9_001_P1.asset` |

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
