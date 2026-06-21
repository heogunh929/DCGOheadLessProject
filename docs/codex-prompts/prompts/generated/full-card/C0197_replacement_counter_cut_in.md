# C0197_replacement_counter_cut_in - replacement/counter/cut-in card porting 12

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0197_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT18_039` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_039.cs` | `OnEnterFieldAnyone, OnLoseSecurity, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT18_040` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_040.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT18_042` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_042.cs` | `None, OnAllyAttack, OnCounterTiming, OnEndTurn, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT18_061` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_061.cs` | `OnCounterTiming, OnEndTurn, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT18_070` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_070.cs` | `None, OnAllyAttack, OnAttackTargetChanged, OnCounterTiming, OnDeclaration` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `BT18_071` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_071.cs` | `None, OnAttackTargetChanged, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 4 |
| `BT18_082` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_082.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT18_083` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_083.cs` | `OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean` | 2 |
| `BT18_086` | `DCGO/Assets/Scripts/CardEffect/BT18/White/BT18_086.cs` | `None, SecuritySkill, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT18_102` | `DCGO/Assets/Scripts/CardEffect/BT18/White/BT18_102.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT18-039#3887@base` | `BT18-039` | 3887 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_039.asset` |
| `BT18-040#3901@base` | `BT18-040` | 3901 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_040.asset` |
| `BT18-040#3902@P1` | `BT18-040` | 3902 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_040_P1.asset` |
| `BT18-042#3903@base` | `BT18-042` | 3903 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_042.asset` |
| `BT18-042#3904@P1` | `BT18-042` | 3904 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_042_P1.asset` |
| `BT18-061#3926@base` | `BT18-061` | 3926 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_061.asset` |
| `BT18-070#3932@base` | `BT18-070` | 3932 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_070.asset` |
| `BT18-071#3933@base` | `BT18-071` | 3933 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_071.asset` |
| `BT18-071#3934@P1` | `BT18-071` | 3934 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_071_P1.asset` |
| `BT18-071#8260@P2` | `BT18-071` | 8260 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_071_P2.asset` |
| `BT18-071#8261@P3` | `BT18-071` | 8261 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_071_P3.asset` |
| `BT18-082#3947@base` | `BT18-082` | 3947 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_082.asset` |
| `BT18-082#3948@P1` | `BT18-082` | 3948 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_082_P1.asset` |
| `BT18-083#3945@base` | `BT18-083` | 3945 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_083.asset` |
| `BT18-083#3946@P1` | `BT18-083` | 3946 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_083_P1.asset` |
| `BT18-086#3955@base` | `BT18-086` | 3955 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/White/Digimon/BT18_086.asset` |
| `BT18-102#3974@base` | `BT18-102` | 3974 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/White/Digimon/BT18_102.asset` |
| `BT18-102#3975@P1` | `BT18-102` | 3975 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/White/Digimon/BT18_102_P1.asset` |
| `BT18-102#3976@P2` | `BT18-102` | 3976 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT18/White/Digimon/BT18_102_P2.asset` |
| `BT18-102#8271@P3` | `BT18-102` | 8271 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT18/White/Digimon/BT18_102_P3.asset` |
| `BT18-102#8272@P4` | `BT18-102` | 8272 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT18/White/Digimon/BT18_102_P4.asset` |

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
