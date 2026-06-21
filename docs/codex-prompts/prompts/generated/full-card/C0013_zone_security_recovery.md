# C0013_zone_security_recovery - zone/security/recovery card porting 7

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0013_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT12_086` | `DCGO/Assets/Scripts/CardEffect/BT12/White/BT12_086.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT12_087` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_087.cs` | `BeforePayCost, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 3 |
| `BT12_088` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_088.cs` | `None, OnSecurityCheck, OnStartTurn, SecuritySkill` | `inherited, max_count_per_turn, security, static_or_continuous, zone_movement` | `-` | 3 |
| `BT12_091` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_091.cs` | `BeforePayCost, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `BT12_093` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_093.cs` | `BeforePayCost, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `BT12_094` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_094.cs` | `BeforePayCost, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `BT12_096` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_096.cs` | `BeforePayCost, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 4 |
| `BT12_097` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_097.cs` | `BeforePayCost, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `BT12_098` | `DCGO/Assets/Scripts/CardEffect/BT12/White/BT12_098.cs` | `OnDeclaration, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `BT13_001` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_001.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT12-086#2506@base` | `BT12-086` | 2506 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/White/Digimon/BT12_086.asset` |
| `BT12-086#4533@P0` | `BT12-086` | 4533 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/White/Digimon/BT12_086_P0.asset` |
| `BT12-087#2507@base` | `BT12-087` | 2507 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_087.asset` |
| `BT12-087#2508@P1` | `BT12-087` | 2508 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_087_P1.asset` |
| `BT12-087#4534@P0` | `BT12-087` | 4534 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_087_P0.asset` |
| `BT12-088#2509@base` | `BT12-088` | 2509 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_088.asset` |
| `BT12-088#2510@P1` | `BT12-088` | 2510 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_088_P1.asset` |
| `BT12-088#4535@P0` | `BT12-088` | 4535 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_088_P0.asset` |
| `BT12-091#2515@base` | `BT12-091` | 2515 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Tamer/BT12_091.asset` |
| `BT12-091#4538@P0` | `BT12-091` | 4538 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Tamer/BT12_091_P0.asset` |
| `BT12-093#2518@base` | `BT12-093` | 2518 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Tamer/BT12_093.asset` |
| `BT12-093#4540@P0` | `BT12-093` | 4540 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Tamer/BT12_093_P0.asset` |
| `BT12-094#2519@base` | `BT12-094` | 2519 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Tamer/BT12_094.asset` |
| `BT12-094#4541@P0` | `BT12-094` | 4541 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Tamer/BT12_094_P0.asset` |
| `BT12-096#2522@base` | `BT12-096` | 2522 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Tamer/BT12_096.asset` |
| `BT12-096#2523@P1` | `BT12-096` | 2523 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Tamer/BT12_096_P1.asset` |
| `BT12-096#4543@P0` | `BT12-096` | 4543 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Tamer/BT12_096_P0.asset` |
| `BT12-096#8136@P2` | `BT12-096` | 8136 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Tamer/BT12_096_P2.asset` |
| `BT12-097#2524@base` | `BT12-097` | 2524 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Tamer/BT12_097.asset` |
| `BT12-097#4544@P0` | `BT12-097` | 4544 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Tamer/BT12_097_P0.asset` |
| `BT12-098#2525@base` | `BT12-098` | 2525 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/White/Tamer/BT12_098.asset` |
| `BT12-098#4545@P0` | `BT12-098` | 4545 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/White/Tamer/BT12_098_P0.asset` |
| `BT13-001#2643@base` | `BT13-001` | 2643 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/DigiEgg/BT13_001.asset` |
| `BT13-001#4553@P0` | `BT13-001` | 4553 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/DigiEgg/BT13_001_P0.asset` |

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
