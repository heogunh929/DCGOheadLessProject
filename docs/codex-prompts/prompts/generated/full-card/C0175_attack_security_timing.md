# C0175_attack_security_timing - attack/security timing card porting 48

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0175_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_209` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_209.cs` | `None, OnAllyAttack, OnDiscardHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 4 |
| `P_238` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_238.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `RB1_002` | `DCGO/Assets/Scripts/CardEffect/RB1/Blue/RB1_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `RB1_014` | `DCGO/Assets/Scripts/CardEffect/RB1/Blue/RB1_014.cs` | `OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectOrder` | 1 |
| `RB1_025` | `DCGO/Assets/Scripts/CardEffect/RB1/Green/RB1_025.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 2 |
| `RB1_033` | `DCGO/Assets/Scripts/CardEffect/RB1/Blue/RB1_033.cs` | `OnAllyAttack, OnUnTappedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 2 |
| `ST10_01` | `DCGO/Assets/Scripts/CardEffect/ST10/Purple/ST10_01.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 2 |
| `ST13_02` | `DCGO/Assets/Scripts/CardEffect/ST13/Red/ST13_02.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `ST13_03` | `DCGO/Assets/Scripts/CardEffect/ST13/Red/ST13_03.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `ST14_01` | `DCGO/Assets/Scripts/CardEffect/ST14/Purple/ST14_01.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-209#7487@base` | `P-209` | 7487 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_209.asset` |
| `P-209#9312@P1` | `P-209` | 9312 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_209_P1.asset` |
| `P-209#9313@P2` | `P-209` | 9313 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_209_P2.asset` |
| `P-209#9314@P3` | `P-209` | 9314 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_209_P3.asset` |
| `P-238#7908@base` | `P-238` | 7908 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_238.asset` |
| `P-238#7909@P1` | `P-238` | 7909 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_238_P1.asset` |
| `RB1-002#2863@base` | `RB1-002` | 2863 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Blue/DigiEgg/RB1_002.asset` |
| `RB1-014#2879@base` | `RB1-014` | 2879 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Blue/Digimon/RB1_014.asset` |
| `RB1-025#2893@base` | `RB1-025` | 2893 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Green/Digimon/RB1_025.asset` |
| `RB1-025#2894@P1` | `RB1-025` | 2894 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Green/Digimon/RB1_025_P1.asset` |
| `RB1-033#2905@base` | `RB1-033` | 2905 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Blue/Tamer/RB1_033.asset` |
| `RB1-033#2906@P1` | `RB1-033` | 2906 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Blue/Tamer/RB1_033_P1.asset` |
| `ST10-01#1756@base` | `ST10-01` | 1756 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Purple/DigiEgg/ST10_01.asset` |
| `ST10-01#4901@P1` | `ST10-01` | 4901 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Purple/DigiEgg/ST10_01_P1.asset` |
| `ST13-02#2802@base` | `ST13-02` | 2802 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Red/Digimon/ST13_02.asset` |
| `ST13-03#2803@base` | `ST13-03` | 2803 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Red/Digimon/ST13_03.asset` |
| `ST14-01#2817@base` | `ST14-01` | 2817 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/DigiEgg/ST14_01.asset` |
| `ST14-01#4913@P0` | `ST14-01` | 4913 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/DigiEgg/ST14_01_P0.asset` |
| `ST14-01#4914@P1` | `ST14-01` | 4914 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/DigiEgg/ST14_01_P1.asset` |

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
