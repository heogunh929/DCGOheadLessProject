# C0049_zone_security_recovery - zone/security/recovery card porting 43

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0049_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 35
- Source effect count: 8
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT2_085` | `DCGO/Assets/Scripts/CardEffect/BT2/Blue/BT2_085.cs` | `OnDigivolutionCardDiscarded, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 3 |
| `BT2_087` | `DCGO/Assets/Scripts/CardEffect/BT2/Yellow/BT2_087.cs` | `OnStartTurn, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectSecurity` | 3 |
| `BT2_089` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_089.cs` | `None, OnStartTurn, SecuritySkill` | `inherited, modifier_duration, security, static_or_continuous, zone_movement` | `-` | 3 |
| `BT2_090` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_090.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 4 |
| `BT3_006` | `DCGO/Assets/Scripts/CardEffect/BT3/Purple/BT3_006.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 6 |
| `BT3_011` | `DCGO/Assets/Scripts/CardEffect/BT3/Red/BT3_011.cs` | `SecuritySkill` | `security, zone_movement` | `-` | 14 |
| `BT3_014` | `DCGO/Assets/Scripts/CardEffect/BT3/Red/BT3_014.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT3_015` | `DCGO/Assets/Scripts/CardEffect/BT3/Red/BT3_015.cs` | `OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-085#523@base` | `BT2-085` | 523 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Tamer/BT2_085.asset` |
| `BT2-085#524@P1` | `BT2-085` | 524 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Tamer/BT2_085_P1.asset` |
| `BT2-085#525@P2` | `BT2-085` | 525 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Tamer/BT2_085_P2.asset` |
| `BT2-087#527@base` | `BT2-087` | 527 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Tamer/BT2_087.asset` |
| `BT2-087#528@P1` | `BT2-087` | 528 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Tamer/BT2_087_P1.asset` |
| `BT2-087#529@P2` | `BT2-087` | 529 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Tamer/BT2_087_P2.asset` |
| `BT2-089#531@base` | `BT2-089` | 531 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Tamer/BT2_089.asset` |
| `BT2-089#532@P1` | `BT2-089` | 532 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Tamer/BT2_089_P1.asset` |
| `BT2-089#533@P2` | `BT2-089` | 533 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Tamer/BT2_089_P2.asset` |
| `BT2-090#534@base` | `BT2-090` | 534 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Tamer/BT2_090.asset` |
| `BT2-090#535@P1` | `BT2-090` | 535 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Tamer/BT2_090_P1.asset` |
| `BT2-090#536@P2` | `BT2-090` | 536 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Tamer/BT2_090_P2.asset` |
| `BT2-090#8323@P3` | `BT2-090` | 8323 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Tamer/BT2_090_P3.asset` |
| `BT3-006#598@base` | `BT3-006` | 598 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/DigiEgg/BT3_006.asset` |
| `BT3-006#599@P1` | `BT3-006` | 599 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_006_P1.asset` |
| `BT3-006#600@P2` | `BT3-006` | 600 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_006_P2.asset` |
| `BT3-006#8463@P1` | `BT3-006` | 8463 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/DigiEgg/BT3_006_P1.asset` |
| `BT3-006#8464@P2` | `BT3-006` | 8464 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/DigiEgg/BT3_006_P2.asset` |
| `BT3-006#8465@P3` | `BT3-006` | 8465 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/DigiEgg/BT3_006_P3.asset` |
| `BT3-011#606@base` | `BT3-011` | 606 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_011.asset` |
| `BT3-011#607@P1` | `BT3-011` | 607 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_011_P1.asset` |
| `BT3-014#610@base` | `BT3-014` | 610 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_014.asset` |
| `BT3-015#611@base` | `BT3-015` | 611 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_015.asset` |
| `BT3-024#626@base` | `BT3-024` | 626 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_024.asset` |
| `BT3-024#627@P1` | `BT3-024` | 627 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_024_P1.asset` |
| `BT3-024#628@P2` | `BT3-024` | 628 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_024_P2.asset` |
| `BT3-036#645@base` | `BT3-036` | 645 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_036.asset` |
| `BT3-036#646@P1` | `BT3-036` | 646 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_036_P1.asset` |
| `BT3-049#665@base` | `BT3-049` | 665 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_049.asset` |
| `BT3-049#666@P1` | `BT3-049` | 666 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_049_P1.asset` |
| `BT3-065#690@base` | `BT3-065` | 690 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_065.asset` |
| `BT3-065#691@P1` | `BT3-065` | 691 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_065_P1.asset` |
| `BT3-082#714@base` | `BT3-082` | 714 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_082.asset` |
| `BT3-082#715@P1` | `BT3-082` | 715 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_082_P1.asset` |
| `BT6-058#1187@base` | `BT6-058` | 1187 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_058.asset` |

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
