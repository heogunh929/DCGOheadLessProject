# C0042_zone_security_recovery - zone/security/recovery card porting 36

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0042_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT24_008` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_008.cs` | `OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectSecurity` | 2 |
| `BT24_012` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_012.cs` | `None, OnLoseSecurity, WhenRemoveField` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |
| `BT24_020` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_020.cs` | `None, OnEnterFieldAnyone, OnUnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT24_022` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_022.cs` | `None, OnEnterFieldAnyone, OnUnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT24_030` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_030.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone, WhenRemoveField` | `max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `BT24_040` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_040.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT24_041` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_041.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `BT24_048` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_048.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectBoolean` | 1 |
| `BT24_049` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_049.cs` | `None, OnDestroyedAnyone, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT24_058` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_058.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT24-008#7526@base` | `BT24-008` | 7526 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_008.asset` |
| `BT24-008#7527@P1` | `BT24-008` | 7527 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_008_P1.asset` |
| `BT24-012#7531@base` | `BT24-012` | 7531 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_012.asset` |
| `BT24-020#7541@base` | `BT24-020` | 7541 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_020.asset` |
| `BT24-020#7542@P1` | `BT24-020` | 7542 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_020_P1.asset` |
| `BT24-022#7544@base` | `BT24-022` | 7544 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_022.asset` |
| `BT24-030#7552@base` | `BT24-030` | 7552 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_030.asset` |
| `BT24-030#7553@P1` | `BT24-030` | 7553 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_030_P1.asset` |
| `BT24-040#7566@base` | `BT24-040` | 7566 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_040.asset` |
| `BT24-040#7567@P1` | `BT24-040` | 7567 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_040_P1.asset` |
| `BT24-041#7568@base` | `BT24-041` | 7568 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_041.asset` |
| `BT24-041#7569@P1` | `BT24-041` | 7569 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_041_P1.asset` |
| `BT24-048#7577@base` | `BT24-048` | 7577 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_048.asset` |
| `BT24-049#7578@base` | `BT24-049` | 7578 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_049.asset` |
| `BT24-058#7589@base` | `BT24-058` | 7589 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_058.asset` |

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
