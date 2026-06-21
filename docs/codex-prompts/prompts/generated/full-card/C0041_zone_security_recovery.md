# C0041_zone_security_recovery - zone/security/recovery card porting 35

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0041_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT23_088` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_088.cs` | `OnEndTurn, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `BT23_090` | `DCGO/Assets/Scripts/CardEffect/BT23/White/BT23_090.cs` | `None, OnEndTurn, OnStartTurn, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 3 |
| `BT23_093` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_093.cs` | `None, OnTappedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 1 |
| `BT23_100` | `DCGO/Assets/Scripts/CardEffect/BT23/White/BT23_100.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity` | 1 |
| `BT24_001` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_001.cs` | `OnLoseSecurity` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT24_002` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_002.cs` | `OnEndTurn` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |
| `BT24_003` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_003.cs` | `OnLoseSecurity` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectSecurity` | 1 |
| `BT24_004` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_004.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT24_005` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_005.cs` | `OnAddDigivolutionCards` | `inherited, max_count_per_turn, static_or_continuous` | `SelectCard` | 1 |
| `BT24_007` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_007.cs` | `OnDiscardHand` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT23-088#7437@base` | `BT23-088` | 7437 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Tamer/BT23_088.asset` |
| `BT23-088#7438@P1` | `BT23-088` | 7438 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Tamer/BT23_088_P1.asset` |
| `BT23-090#7442@base` | `BT23-090` | 7442 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Tamer/BT23_090.asset` |
| `BT23-090#7443@P1` | `BT23-090` | 7443 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Tamer/BT23_090_P1.asset` |
| `BT23-090#7444@P2` | `BT23-090` | 7444 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Tamer/BT23_090_P2.asset` |
| `BT23-093#7447@base` | `BT23-093` | 7447 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Option/BT23_093.asset` |
| `BT23-100#7454@base` | `BT23-100` | 7454 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Option/BT23_100.asset` |
| `BT24-001#7519@base` | `BT24-001` | 7519 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/DigiEgg/BT24_001.asset` |
| `BT24-002#7520@base` | `BT24-002` | 7520 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/DigiEgg/BT24_002.asset` |
| `BT24-003#7521@base` | `BT24-003` | 7521 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/DigiEgg/BT24_003.asset` |
| `BT24-004#7522@base` | `BT24-004` | 7522 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/DigiEgg/BT24_004.asset` |
| `BT24-005#7523@base` | `BT24-005` | 7523 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/DigiEgg/BT24_005.asset` |
| `BT24-007#7525@base` | `BT24-007` | 7525 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/DigiEgg/BT24_007.asset` |

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
