# C0051_zone_security_recovery - zone/security/recovery card porting 45

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0051_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 29
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT3_073` | `DCGO/Assets/Scripts/CardEffect/BT3/Black/BT3_073.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |
| `BT3_084` | `DCGO/Assets/Scripts/CardEffect/BT3/Purple/BT3_084.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT3_088` | `DCGO/Assets/Scripts/CardEffect/BT3/Purple/BT3_088.cs` | `OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 4 |
| `BT3_090` | `DCGO/Assets/Scripts/CardEffect/BT3/Purple/BT3_090.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity` | 2 |
| `BT3_091` | `DCGO/Assets/Scripts/CardEffect/BT3/Purple/BT3_091.cs` | `OnEnterFieldAnyone, OnUseOption` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 6 |
| `BT3_093` | `DCGO/Assets/Scripts/CardEffect/BT3/Blue/BT3_093.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 4 |
| `BT3_094` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_094.cs` | `OnEndBattle, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 2 |
| `BT3_095` | `DCGO/Assets/Scripts/CardEffect/BT3/Black/BT3_095.cs` | `OnStartTurn, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `-` | 1 |
| `BT3_096` | `DCGO/Assets/Scripts/CardEffect/BT3/Purple/BT3_096.cs` | `OnUseOption, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 2 |
| `BT4_006` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_006.cs` | `OnDestroyedAnyone` | `inherited, zone_movement` | `-` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT3-073#700@base` | `BT3-073` | 700 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_073.asset` |
| `BT3-073#701@P1` | `BT3-073` | 701 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_073_P1.asset` |
| `BT3-084#717@base` | `BT3-084` | 717 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_084.asset` |
| `BT3-088#721@base` | `BT3-088` | 721 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_088.asset` |
| `BT3-088#722@P1` | `BT3-088` | 722 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_088_P1.asset` |
| `BT3-088#8485@P2` | `BT3-088` | 8485 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_088_P2.asset` |
| `BT3-088#8486@P3` | `BT3-088` | 8486 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_088_P3.asset` |
| `BT3-090#724@base` | `BT3-090` | 724 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_090.asset` |
| `BT3-090#725@P1` | `BT3-090` | 725 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_090_P1.asset` |
| `BT3-091#726@base` | `BT3-091` | 726 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_091.asset` |
| `BT3-091#727@P1` | `BT3-091` | 727 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_091_P1.asset` |
| `BT3-091#728@P2` | `BT3-091` | 728 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_091_P2.asset` |
| `BT3-091#729@P3` | `BT3-091` | 729 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_091_P3.asset` |
| `BT3-091#730@P4` | `BT3-091` | 730 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_091_P4.asset` |
| `BT3-091#8487@P5` | `BT3-091` | 8487 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_091_P5.asset` |
| `BT3-093#732@base` | `BT3-093` | 732 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Tamer/BT3_093.asset` |
| `BT3-093#733@P1` | `BT3-093` | 733 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Tamer/BT3_093_P1.asset` |
| `BT3-093#8489@P2` | `BT3-093` | 8489 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Tamer/BT3_093_P2.asset` |
| `BT3-093#8490@P3` | `BT3-093` | 8490 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Tamer/BT3_093_P3.asset` |
| `BT3-094#734@base` | `BT3-094` | 734 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Tamer/BT3_094.asset` |
| `BT3-094#735@P1` | `BT3-094` | 735 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Tamer/BT3_094_P1.asset` |
| `BT3-095#736@base` | `BT3-095` | 736 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Tamer/BT3_095.asset` |
| `BT3-096#737@base` | `BT3-096` | 737 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Tamer/BT3_096.asset` |
| `BT3-096#8491@P1` | `BT3-096` | 8491 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Tamer/BT3_096_P1.asset` |
| `BT4-006#767@base` | `BT4-006` | 767 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/DigiEgg/BT4_006.asset` |
| `BT4-006#768@P1` | `BT4-006` | 768 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_006_P1.asset` |
| `BT4-006#8502@P0` | `BT4-006` | 8502 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/DigiEgg/BT4_006_P0.asset` |
| `BT4-006#8503@P1` | `BT4-006` | 8503 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/DigiEgg/BT4_006_P1.asset` |
| `BT4-006#8504@P2` | `BT4-006` | 8504 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/DigiEgg/BT4_006_P2.asset` |

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
