# C0176_attack_security_timing - attack/security timing card porting 49

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0176_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST15_01` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_01.cs` | `OnAttackTargetChanged` | `inherited, max_count_per_turn, static_or_continuous` | `SelectAttackTarget` | 2 |
| `ST15_05` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_05.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `ST15_14` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_14.cs` | `OnAttackTargetChanged, OnStartTurn, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget` | 2 |
| `ST16_01` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_01.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `ST16_05` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_05.cs` | `OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `ST16_11` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_11.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 3 |
| `ST17_01` | `DCGO/Assets/Scripts/CardEffect/ST17/Green/ST17_01.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `ST18_01` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_01.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `ST18_03` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_03.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `ST18_10` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_10.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST15-01#2830@base` | `ST15-01` | 2830 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/DigiEgg/ST15_01.asset` |
| `ST15-01#4926@P0` | `ST15-01` | 4926 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/DigiEgg/ST15_01_P0.asset` |
| `ST15-05#2834@base` | `ST15-05` | 2834 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_05.asset` |
| `ST15-05#4931@P0` | `ST15-05` | 4931 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_05_P0.asset` |
| `ST15-14#2843@base` | `ST15-14` | 2843 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Tamer/ST15_14.asset` |
| `ST15-14#4940@P0` | `ST15-14` | 4940 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Tamer/ST15_14_P0.asset` |
| `ST16-01#2846@base` | `ST16-01` | 2846 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/DigiEgg/ST16_01.asset` |
| `ST16-01#4943@P0` | `ST16-01` | 4943 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/DigiEgg/ST16_01_P0.asset` |
| `ST16-05#2850@base` | `ST16-05` | 2850 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_05.asset` |
| `ST16-05#4948@P0` | `ST16-05` | 4948 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_05_P0.asset` |
| `ST16-11#2856@base` | `ST16-11` | 2856 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_11.asset` |
| `ST16-11#4953@P0` | `ST16-11` | 4953 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_11_P0.asset` |
| `ST16-11#4954@P1` | `ST16-11` | 4954 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_11_P1.asset` |
| `ST17-01#3267@base` | `ST17-01` | 3267 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/DigiEgg/ST17_01.asset` |
| `ST17-01#4960@P0` | `ST17-01` | 4960 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/DigiEgg/ST17_01_P0.asset` |
| `ST18-01#3818@base` | `ST18-01` | 3818 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/DigiEgg/ST18_01.asset` |
| `ST18-03#3820@base` | `ST18-03` | 3820 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_03.asset` |
| `ST18-10#3827@base` | `ST18-10` | 3827 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_10.asset` |

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
