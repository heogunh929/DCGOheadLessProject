# C0183_simultaneous_trigger_priority - simultaneous trigger/priority card porting 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0183_simultaneous_trigger_priority`
- Kind: `card-porting`
- Category: `simultaneous-trigger-priority` / simultaneous trigger/priority
- Dependencies: none
- Card identity count: 28
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT12_044` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_044.cs` | `AfterEffectsActivate, None, OnEnterFieldAnyone, OnStartTurn, OnUseOption, OptionSkill, SecuritySkill, WhenRemoveField` | `background, max_count_per_turn, modifier_duration, security, static_or_continuous, trigger_priority, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT13_050` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_050.cs` | `None, OnDeclaration, OnEnterFieldAnyone` | `background, inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT14_044` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_044.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase, OnTappedAnyone` | `background, inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT14_046` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_046.cs` | `BeforePayCost, None, OnEnterFieldAnyone` | `background, inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 5 |
| `BT21_056` | `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_056.cs` | `None, OnEnterFieldAnyone` | `background, inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT4_011` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_011.cs` | `BeforePayCost, None` | `background, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 8 |
| `BT7_089` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_089.cs` | `None, OnDetermineDoSecurityCheck, OnEndTurn, SecuritySkill` | `background, inherited, max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 3 |
| `EX1_033` | `DCGO/Assets/Scripts/CardEffect/EX1/Green/EX1_033.cs` | `AfterPayCost, None, OnAllyAttack` | `background, inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 3 |
| `EX5_029` | `DCGO/Assets/Scripts/CardEffect/EX5/Yellow/EX5_029.cs` | `AfterPayCost, None, OnAllyAttack` | `background, inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |
| `P_093` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_093.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `background, inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT12-044#2456@base` | `BT12-044` | 2456 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_044.asset` |
| `BT12-044#4503@P0` | `BT12-044` | 4503 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_044_P0.asset` |
| `BT13-050#2702@base` | `BT13-050` | 2702 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_050.asset` |
| `BT14-044#2967@base` | `BT14-044` | 2967 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_044.asset` |
| `BT14-044#2968@P1` | `BT14-044` | 2968 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_044_P1.asset` |
| `BT14-046#2970@base` | `BT14-046` | 2970 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_046.asset` |
| `BT14-046#4661@P0` | `BT14-046` | 4661 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_046_P0.asset` |
| `BT14-046#4662@P1` | `BT14-046` | 4662 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_046_P1.asset` |
| `BT14-046#4663@P2` | `BT14-046` | 4663 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_046_P2.asset` |
| `BT14-046#4664@P3` | `BT14-046` | 4664 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_046_P3.asset` |
| `BT21-056#5370@base` | `BT21-056` | 5370 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_056.asset` |
| `BT21-056#5371@P1` | `BT21-056` | 5371 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_056_P1.asset` |
| `BT4-011#6773@P0` | `BT4-011` | 6773 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_011_P0.asset` |
| `BT4-011#6774@P6` | `BT4-011` | 6774 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_011_P6.asset` |
| `BT4-011#774@base` | `BT4-011` | 774 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_011.asset` |
| `BT4-011#775@P1` | `BT4-011` | 775 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_011_P1.asset` |
| `BT4-011#776@P2` | `BT4-011` | 776 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_011_P2.asset` |
| `BT4-011#777@P3` | `BT4-011` | 777 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_011_P3.asset` |
| `BT4-011#778@P4` | `BT4-011` | 778 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_011_P4.asset` |
| `BT4-011#779@P5` | `BT4-011` | 779 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_011_P5.asset` |
| `BT7-089#1522@base` | `BT7-089` | 1522 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Tamer/BT7_089.asset` |
| `BT7-089#1523@P1` | `BT7-089` | 1523 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Tamer/BT7_089_P1.asset` |
| `BT7-089#8815@P0` | `BT7-089` | 8815 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Tamer/BT7_089_P0.asset` |
| `EX1-033#1318@base` | `EX1-033` | 1318 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_033.asset` |
| `EX1-033#1319@P1` | `EX1-033` | 1319 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_033_P1.asset` |
| `EX1-033#9091@P2` | `EX1-033` | 9091 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_033_P2.asset` |
| `EX5-029#3068@base` | `EX5-029` | 3068 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_029.asset` |
| `P-093#6139@base` | `P-093` | 6139 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_093.asset` |

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
