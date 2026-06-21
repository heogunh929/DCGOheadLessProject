# C0053_zone_security_recovery - zone/security/recovery card porting 47

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0053_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 25
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT4_055` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_055.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT4_058` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_058.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT4_060` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_060.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `BT4_061` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_061.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT4_074` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_074.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 4 |
| `BT4_079` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_079.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 2 |
| `BT4_083` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_083.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `BT4_084` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_084.cs` | `OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 4 |
| `BT4_087` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_087.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 3 |
| `BT4_088` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_088.cs` | `OnDestroyedAnyone, OnLoseSecurity` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT4-055#835@base` | `BT4-055` | 835 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_055.asset` |
| `BT4-058#838@base` | `BT4-058` | 838 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_058.asset` |
| `BT4-058#8524@P0` | `BT4-058` | 8524 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_058_P0.asset` |
| `BT4-060#841@base` | `BT4-060` | 841 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_060.asset` |
| `BT4-060#8525@P0` | `BT4-060` | 8525 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_060_P0.asset` |
| `BT4-061#842@base` | `BT4-061` | 842 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_061.asset` |
| `BT4-061#8526@P0` | `BT4-061` | 8526 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_061_P0.asset` |
| `BT4-074#8533@P0` | `BT4-074` | 8533 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_074_P0.asset` |
| `BT4-074#8534@P2` | `BT4-074` | 8534 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_074_P2.asset` |
| `BT4-074#859@base` | `BT4-074` | 859 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_074.asset` |
| `BT4-074#860@P1` | `BT4-074` | 860 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_074_P1.asset` |
| `BT4-079#8537@P1` | `BT4-079` | 8537 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_079_P1.asset` |
| `BT4-079#867@base` | `BT4-079` | 867 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_079.asset` |
| `BT4-083#871@base` | `BT4-083` | 871 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_083.asset` |
| `BT4-084#872@base` | `BT4-084` | 872 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_084.asset` |
| `BT4-084#873@P1` | `BT4-084` | 873 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_084_P1.asset` |
| `BT4-084#874@P2` | `BT4-084` | 874 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_084_P2.asset` |
| `BT4-084#875@P3` | `BT4-084` | 875 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_084_P3.asset` |
| `BT4-087#8541@P0` | `BT4-087` | 8541 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_087_P0.asset` |
| `BT4-087#878@base` | `BT4-087` | 878 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_087.asset` |
| `BT4-087#879@P1` | `BT4-087` | 879 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_087_P1.asset` |
| `BT4-088#880@base` | `BT4-088` | 880 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_088.asset` |
| `BT4-088#881@P1` | `BT4-088` | 881 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_088_P1.asset` |
| `BT4-088#882@P2` | `BT4-088` | 882 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_088_P2.asset` |
| `BT4-088#883@P3` | `BT4-088` | 883 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_088_P3.asset` |

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
