# C0043_zone_security_recovery - zone/security/recovery card porting 37

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0043_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT24_064` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_064.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT24_076` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_076.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT24_098` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_098.cs` | `OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectInteger, SelectSecurity` | 1 |
| `BT24_099` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_099.cs` | `None, OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 1 |
| `BT24_100` | `DCGO/Assets/Scripts/CardEffect/BT24/White/BT24_100.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity` | 2 |
| `BT24_102` | `DCGO/Assets/Scripts/CardEffect/BT24/White/BT24_102.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 3 |
| `BT25_002` | `DCGO/Assets/Scripts/CardEffect/BT25/Blue/BT25_002.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT25_005` | `DCGO/Assets/Scripts/CardEffect/BT25/Black/BT25_005.cs` | `OnAddDigivolutionCards` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |
| `BT25_008` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_008.cs` | `None` | `inherited, skippable, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `BT25_009` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_009.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT24-064#7595@base` | `BT24-064` | 7595 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_064.asset` |
| `BT24-076#7609@base` | `BT24-076` | 7609 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_076.asset` |
| `BT24-098#7641@base` | `BT24-098` | 7641 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Option/BT24_098.asset` |
| `BT24-099#7642@base` | `BT24-099` | 7642 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Option/BT24_099.asset` |
| `BT24-100#7643@base` | `BT24-100` | 7643 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/White/Option/BT24_100.asset` |
| `BT24-100#7644@P1` | `BT24-100` | 7644 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/White/Option/BT24_100_P1.asset` |
| `BT24-102#7648@base` | `BT24-102` | 7648 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/White/Tamer/BT24_102.asset` |
| `BT24-102#7649@P1` | `BT24-102` | 7649 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/White/Tamer/BT24_102_P1.asset` |
| `BT24-102#7650@P2` | `BT24-102` | 7650 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT24/White/Tamer/BT24_102_P2.asset` |
| `BT25-002#7964@base` | `BT25-002` | 7964 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/DigiEgg/BT25_002.asset` |
| `BT25-005#7967@base` | `BT25-005` | 7967 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/DigiEgg/BT25_005.asset` |
| `BT25-008#7970@base` | `BT25-008` | 7970 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_008.asset` |
| `BT25-009#7971@base` | `BT25-009` | 7971 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_009.asset` |

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
