# C0168_attack_security_timing - attack/security timing card porting 41

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0168_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 26
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX7_019` | `DCGO/Assets/Scripts/CardEffect/EX7/Blue/EX7_019.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `EX7_020` | `DCGO/Assets/Scripts/CardEffect/EX7/Blue/EX7_020.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX7_022` | `DCGO/Assets/Scripts/CardEffect/EX7/Blue/EX7_022.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectAttackTarget` | 2 |
| `EX7_028` | `DCGO/Assets/Scripts/CardEffect/EX7/Yellow/EX7_028.cs` | `None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `EX7_030` | `DCGO/Assets/Scripts/CardEffect/EX7/Yellow/EX7_030.cs` | `OnAllyAttack, OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 4 |
| `EX7_034` | `DCGO/Assets/Scripts/CardEffect/EX7/Green/EX7_034.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 4 |
| `EX7_036` | `DCGO/Assets/Scripts/CardEffect/EX7/Green/EX7_036.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 4 |
| `EX7_049` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_049.cs` | `OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX7_052` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_052.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX7_054` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_054.cs` | `OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX7-019#3711@base` | `EX7-019` | 3711 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_019.asset` |
| `EX7-019#3712@P1` | `EX7-019` | 3712 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_019_P1.asset` |
| `EX7-020#3713@base` | `EX7-020` | 3713 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_020.asset` |
| `EX7-020#3714@P1` | `EX7-020` | 3714 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_020_P1.asset` |
| `EX7-022#3716@base` | `EX7-022` | 3716 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_022.asset` |
| `EX7-022#3717@P1` | `EX7-022` | 3717 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_022_P1.asset` |
| `EX7-028#3727@base` | `EX7-028` | 3727 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_028.asset` |
| `EX7-028#3728@P1` | `EX7-028` | 3728 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_028_P1.asset` |
| `EX7-030#3731@base` | `EX7-030` | 3731 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_030.asset` |
| `EX7-030#3732@P1` | `EX7-030` | 3732 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_030_P1.asset` |
| `EX7-030#3733@P2` | `EX7-030` | 3733 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_030_P2.asset` |
| `EX7-030#9176@P3` | `EX7-030` | 9176 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_030_P3.asset` |
| `EX7-034#3740@base` | `EX7-034` | 3740 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_034.asset` |
| `EX7-034#9177@P1` | `EX7-034` | 9177 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_034_P1.asset` |
| `EX7-034#9178@P2` | `EX7-034` | 9178 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_034_P2.asset` |
| `EX7-034#9179@P3` | `EX7-034` | 9179 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_034_P3.asset` |
| `EX7-036#3743@base` | `EX7-036` | 3743 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_036.asset` |
| `EX7-036#3744@P1` | `EX7-036` | 3744 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_036_P1.asset` |
| `EX7-036#3745@P2` | `EX7-036` | 3745 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_036_P2.asset` |
| `EX7-036#9180@P3` | `EX7-036` | 9180 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_036_P3.asset` |
| `EX7-049#3768@base` | `EX7-049` | 3768 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_049.asset` |
| `EX7-049#3769@P1` | `EX7-049` | 3769 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_049_P1.asset` |
| `EX7-052#3772@base` | `EX7-052` | 3772 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_052.asset` |
| `EX7-052#3773@P1` | `EX7-052` | 3773 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_052_P1.asset` |
| `EX7-054#3776@base` | `EX7-054` | 3776 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_054.asset` |
| `EX7-054#3777@P1` | `EX7-054` | 3777 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_054_P1.asset` |

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
