# C0167_attack_security_timing - attack/security timing card porting 40

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0167_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX6_016` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_016.cs` | `OnAllyAttack, OnStartMainPhase` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `EX6_017` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_017.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `EX6_045` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_045.cs` | `OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `EX6_048` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_048.cs` | `OnAllyAttack, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `EX6_050` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_050.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 1 |
| `EX7_002` | `DCGO/Assets/Scripts/CardEffect/EX7/Blue/EX7_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX7_006` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_006.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 2 |
| `EX7_010` | `DCGO/Assets/Scripts/CardEffect/EX7/Red/EX7_010.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |
| `EX7_013` | `DCGO/Assets/Scripts/CardEffect/EX7/Red/EX7_013.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectAttackTarget` | 3 |
| `EX7_017` | `DCGO/Assets/Scripts/CardEffect/EX7/Blue/EX7_017.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX6-016#3457@base` | `EX6-016` | 3457 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_016.asset` |
| `EX6-017#3458@base` | `EX6-017` | 3458 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Digimon/EX6_017.asset` |
| `EX6-045#3498@base` | `EX6-045` | 3498 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_045.asset` |
| `EX6-048#3501@base` | `EX6-048` | 3501 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_048.asset` |
| `EX6-050#3503@base` | `EX6-050` | 3503 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_050.asset` |
| `EX7-002#3679@base` | `EX7-002` | 3679 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/DigiEgg/EX7_002.asset` |
| `EX7-002#3680@P1` | `EX7-002` | 3680 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/DigiEgg/EX7_002_P1.asset` |
| `EX7-006#3687@base` | `EX7-006` | 3687 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/DigiEgg/EX7_006.asset` |
| `EX7-006#3688@P1` | `EX7-006` | 3688 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/DigiEgg/EX7_006_P1.asset` |
| `EX7-010#3694@base` | `EX7-010` | 3694 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_010.asset` |
| `EX7-013#3699@base` | `EX7-013` | 3699 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_013.asset` |
| `EX7-013#3700@P1` | `EX7-013` | 3700 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_013_P1.asset` |
| `EX7-013#9165@P2` | `EX7-013` | 9165 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_013_P2.asset` |
| `EX7-017#3707@base` | `EX7-017` | 3707 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_017.asset` |
| `EX7-017#3708@P1` | `EX7-017` | 3708 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_017_P1.asset` |

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
