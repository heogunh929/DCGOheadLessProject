# C0015_zone_security_recovery - zone/security/recovery card porting 9

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0015_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT13_057` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_057.cs` | `OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `BT13_061` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_061.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `SelectCard` | 1 |
| `BT13_071` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_071.cs` | `None, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `BT13_072` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_072.cs` | `OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 1 |
| `BT13_078` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_078.cs` | `OnDestroyedAnyone, OnEndTurn` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 2 |
| `BT13_079` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_079.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT13_081` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_081.cs` | `OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT13_082` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_082.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `BT13_096` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_096.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `BT13_099` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_099.cs` | `OnEndTurn, OnTappedAnyone, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT13-057#2710@base` | `BT13-057` | 2710 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_057.asset` |
| `BT13-057#4593@P0` | `BT13-057` | 4593 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_057_P0.asset` |
| `BT13-057#8148@P1` | `BT13-057` | 8148 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_057_P1.asset` |
| `BT13-061#2718@base` | `BT13-061` | 2718 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_061.asset` |
| `BT13-071#2728@base` | `BT13-071` | 2728 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_071.asset` |
| `BT13-071#4596@P0` | `BT13-071` | 4596 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_071_P0.asset` |
| `BT13-072#2729@base` | `BT13-072` | 2729 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_072.asset` |
| `BT13-078#2737@base` | `BT13-078` | 2737 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_078.asset` |
| `BT13-078#4602@P1` | `BT13-078` | 4602 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_078_P1.asset` |
| `BT13-079#2738@base` | `BT13-079` | 2738 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_079.asset` |
| `BT13-081#2740@base` | `BT13-081` | 2740 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_081.asset` |
| `BT13-082#2741@base` | `BT13-082` | 2741 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_082.asset` |
| `BT13-096#2761@base` | `BT13-096` | 2761 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Tamer/BT13_096.asset` |
| `BT13-096#4615@P0` | `BT13-096` | 4615 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Tamer/BT13_096_P0.asset` |
| `BT13-099#2766@base` | `BT13-099` | 2766 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Tamer/BT13_099.asset` |
| `BT13-099#4618@P0` | `BT13-099` | 4618 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Tamer/BT13_099_P0.asset` |

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
