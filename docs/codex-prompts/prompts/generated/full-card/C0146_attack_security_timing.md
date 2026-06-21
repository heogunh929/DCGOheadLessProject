# C0146_attack_security_timing - attack/security timing card porting 19

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0146_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT23_078` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_078.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget` | 2 |
| `BT23_086` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_086.cs` | `OnEndTurn, OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectAttackTarget` | 2 |
| `BT23_091` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_091.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT23_092` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_092.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT23_094` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_094.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT23_095` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_095.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT23_096` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_096.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT24_011` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_011.cs` | `None, OnAllyAttack` | `inherited, static_or_continuous` | `-` | 1 |
| `BT24_017` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_017.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT24_029` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_029.cs` | `None, OnAllyAttack, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT23-078#7416@base` | `BT23-078` | 7416 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Tamer/BT23_078.asset` |
| `BT23-078#7417@P1` | `BT23-078` | 7417 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Tamer/BT23_078_P1.asset` |
| `BT23-086#7434@base` | `BT23-086` | 7434 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Tamer/BT23_086.asset` |
| `BT23-086#7435@P1` | `BT23-086` | 7435 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Tamer/BT23_086_P1.asset` |
| `BT23-091#7445@base` | `BT23-091` | 7445 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Option/BT23_091.asset` |
| `BT23-092#7446@base` | `BT23-092` | 7446 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Option/BT23_092.asset` |
| `BT23-094#7448@base` | `BT23-094` | 7448 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Option/BT23_094.asset` |
| `BT23-095#7449@base` | `BT23-095` | 7449 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Option/BT23_095.asset` |
| `BT23-096#7450@base` | `BT23-096` | 7450 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Option/BT23_096.asset` |
| `BT24-011#7530@base` | `BT24-011` | 7530 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_011.asset` |
| `BT24-017#7537@base` | `BT24-017` | 7537 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_017.asset` |
| `BT24-029#7551@base` | `BT24-029` | 7551 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_029.asset` |

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
