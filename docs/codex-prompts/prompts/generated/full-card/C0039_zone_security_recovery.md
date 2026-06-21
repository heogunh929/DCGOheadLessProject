# C0039_zone_security_recovery - zone/security/recovery card porting 33

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0039_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT22_094` | `DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_094.cs` | `BeforePayCost, None, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 3 |
| `BT22_097` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_097.cs` | `None, OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 1 |
| `BT22_099` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_099.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity` | 1 |
| `BT22_100` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_100.cs` | `None, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 1 |
| `BT23_004` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_004.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT23_005` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_005.cs` | `BeforePayCost, None` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `BT23_011` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_011.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `BT23_012` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_012.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `BT23_019` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_019.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `-` | 1 |
| `BT23_023` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_023.cs` | `None, WhenRemoveField` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT22-094#7115@base` | `BT22-094` | 7115 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Tamer/BT22_094.asset` |
| `BT22-094#7116@P1` | `BT22-094` | 7116 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Tamer/BT22_094_P1.asset` |
| `BT22-094#7117@P2` | `BT22-094` | 7117 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Tamer/BT22_094_P2.asset` |
| `BT22-097#7121@base` | `BT22-097` | 7121 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Option/BT22_097.asset` |
| `BT22-099#7123@base` | `BT22-099` | 7123 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Option/BT22_099.asset` |
| `BT22-100#7124@base` | `BT22-100` | 7124 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Option/BT22_100.asset` |
| `BT23-004#7335@base` | `BT23-004` | 7335 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/DigiEgg/BT23_004.asset` |
| `BT23-005#7336@base` | `BT23-005` | 7336 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_005.asset` |
| `BT23-005#8446@P1` | `BT23-005` | 8446 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_005_P1.asset` |
| `BT23-011#7342@base` | `BT23-011` | 7342 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_011.asset` |
| `BT23-012#7343@base` | `BT23-012` | 7343 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_012.asset` |
| `BT23-019#7351@base` | `BT23-019` | 7351 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Digimon/BT23_019.asset` |
| `BT23-023#7355@base` | `BT23-023` | 7355 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Digimon/BT23_023.asset` |

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
