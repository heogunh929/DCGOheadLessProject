# C0179_attack_security_timing - attack/security timing card porting 52

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0179_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 25
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST1_09` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_09.cs` | `OnBlockAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 6 |
| `ST20_04` | `DCGO/Assets/Scripts/CardEffect/ST20/Red/ST20_04.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget` | 2 |
| `ST20_05` | `DCGO/Assets/Scripts/CardEffect/ST20/Yellow/ST20_05.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, security, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `ST20_06` | `DCGO/Assets/Scripts/CardEffect/ST20/Yellow/ST20_06.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 2 |
| `ST20_09` | `DCGO/Assets/Scripts/CardEffect/ST20/Green/ST20_09.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 2 |
| `ST21_03` | `DCGO/Assets/Scripts/CardEffect/ST21/Blue/ST21_03.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, security, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `ST21_04` | `DCGO/Assets/Scripts/CardEffect/ST21/Blue/ST21_04.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 2 |
| `ST21_05` | `DCGO/Assets/Scripts/CardEffect/ST21/Yellow/ST21_05.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `ST21_06` | `DCGO/Assets/Scripts/CardEffect/ST21/Yellow/ST21_06.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget` | 2 |
| `ST21_09` | `DCGO/Assets/Scripts/CardEffect/ST21/Green/ST21_09.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST1-09#23@base` | `ST1-09` | 23 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_09.asset` |
| `ST1-09#24@P1` | `ST1-09` | 24 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_09_P1.asset` |
| `ST1-09#25@P2` | `ST1-09` | 25 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_09_P2.asset` |
| `ST1-09#4896@P3` | `ST1-09` | 4896 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_09_P3.asset` |
| `ST1-09#4897@P4` | `ST1-09` | 4897 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_09_P4.asset` |
| `ST1-09#4898@P5` | `ST1-09` | 4898 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_09_P5.asset` |
| `ST20-04#5266@base` | `ST20-04` | 5266 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Red/Digimon/ST20_04.asset` |
| `ST20-04#9054@P1` | `ST20-04` | 9054 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Red/Digimon/ST20_04_P1.asset` |
| `ST20-05#5267@base` | `ST20-05` | 5267 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Yellow/Digimon/ST20_05.asset` |
| `ST20-05#9055@P1` | `ST20-05` | 9055 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Yellow/Digimon/ST20_05_P1.asset` |
| `ST20-06#5268@base` | `ST20-06` | 5268 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Yellow/Digimon/ST20_06.asset` |
| `ST20-06#9056@P1` | `ST20-06` | 9056 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Yellow/Digimon/ST20_06_P1.asset` |
| `ST20-09#5271@base` | `ST20-09` | 5271 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Green/Digimon/ST20_09.asset` |
| `ST20-09#9059@P1` | `ST20-09` | 9059 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Green/Digimon/ST20_09_P1.asset` |
| `ST21-03#5281@base` | `ST21-03` | 5281 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Blue/Digimon/ST21_03.asset` |
| `ST21-03#9065@P1` | `ST21-03` | 9065 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Blue/Digimon/ST21_03_P1.asset` |
| `ST21-04#5282@base` | `ST21-04` | 5282 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Blue/Digimon/ST21_04.asset` |
| `ST21-04#9066@P1` | `ST21-04` | 9066 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Blue/Digimon/ST21_04_P1.asset` |
| `ST21-05#5283@base` | `ST21-05` | 5283 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Yellow/Digimon/ST21_05.asset` |
| `ST21-05#9067@P1` | `ST21-05` | 9067 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Yellow/Digimon/ST21_05_P1.asset` |
| `ST21-06#5284@base` | `ST21-06` | 5284 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Yellow/Digimon/ST21_06.asset` |
| `ST21-06#9068@P1` | `ST21-06` | 9068 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Yellow/Digimon/ST21_06_P1.asset` |
| `ST21-09#5287@base` | `ST21-09` | 5287 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Green/Digimon/ST21_09.asset` |
| `ST21-09#9071@P1` | `ST21-09` | 9071 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Green/Digimon/ST21_09_P1.asset` |
| `ST21-09#9072@P2` | `ST21-09` | 9072 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Green/Digimon/ST21_09_P2.asset` |

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
