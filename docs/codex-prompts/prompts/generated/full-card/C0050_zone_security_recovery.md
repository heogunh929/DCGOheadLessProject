# C0050_zone_security_recovery - zone/security/recovery card porting 44

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0050_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 28
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT3_018` | `DCGO/Assets/Scripts/CardEffect/BT3/Red/BT3_018.cs` | `OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT3_025` | `DCGO/Assets/Scripts/CardEffect/BT3/Blue/BT3_025.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT3_029` | `DCGO/Assets/Scripts/CardEffect/BT3/Blue/BT3_029.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT3_030` | `DCGO/Assets/Scripts/CardEffect/BT3/Blue/BT3_030.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT3_034` | `DCGO/Assets/Scripts/CardEffect/BT3/Yellow/BT3_034.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 2 |
| `BT3_043` | `DCGO/Assets/Scripts/CardEffect/BT3/Yellow/BT3_043.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT3_046` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_046.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 13 |
| `BT3_047` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_047.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous` | `SelectCard` | 2 |
| `BT3_051` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_051.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT3_071` | `DCGO/Assets/Scripts/CardEffect/BT3/Black/BT3_071.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT3-018#614@base` | `BT3-018` | 614 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_018.asset` |
| `BT3-018#615@P1` | `BT3-018` | 615 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_018_P1.asset` |
| `BT3-025#629@base` | `BT3-025` | 629 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_025.asset` |
| `BT3-025#630@P1` | `BT3-025` | 630 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_025_P1.asset` |
| `BT3-029#634@base` | `BT3-029` | 634 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_029.asset` |
| `BT3-030#635@base` | `BT3-030` | 635 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_030.asset` |
| `BT3-030#636@P1` | `BT3-030` | 636 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_030_P1.asset` |
| `BT3-034#642@base` | `BT3-034` | 642 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_034.asset` |
| `BT3-034#8472@P1` | `BT3-034` | 8472 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_034_P1.asset` |
| `BT3-043#655@base` | `BT3-043` | 655 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_043.asset` |
| `BT3-043#656@P1` | `BT3-043` | 656 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_043_P1.asset` |
| `BT3-046#659@base` | `BT3-046` | 659 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_046.asset` |
| `BT3-046#660@P1` | `BT3-046` | 660 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_046_P1.asset` |
| `BT3-046#6768@P2` | `BT3-046` | 6768 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_046_P2.asset` |
| `BT3-046#6769@P3` | `BT3-046` | 6769 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_046_P3.asset` |
| `BT3-046#6770@P4` | `BT3-046` | 6770 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_046_P4.asset` |
| `BT3-046#6771@P5` | `BT3-046` | 6771 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_046_P5.asset` |
| `BT3-046#8475@P6` | `BT3-046` | 8475 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_046_P6.asset` |
| `BT3-047#661@base` | `BT3-047` | 661 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_047.asset` |
| `BT3-047#662@P1` | `BT3-047` | 662 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_047_P1.asset` |
| `BT3-051#670@base` | `BT3-051` | 670 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_051.asset` |
| `BT3-061#685@base` | `BT3-061` | 685 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_061.asset` |
| `BT3-061#686@P1` | `BT3-061` | 686 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_061_P1.asset` |
| `BT3-071#698@base` | `BT3-071` | 698 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_071.asset` |
| `BT3-077#708@base` | `BT3-077` | 708 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_077.asset` |
| `BT3-077#709@P1` | `BT3-077` | 709 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_077_P1.asset` |
| `BT6-021#1136@base` | `BT6-021` | 1136 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_021.asset` |
| `BT6-021#1137@P1` | `BT6-021` | 1137 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_021_P1.asset` |

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
