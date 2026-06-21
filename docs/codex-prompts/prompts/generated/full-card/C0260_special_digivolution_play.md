# C0260_special_digivolution_play - special digivolution/play mechanics card porting 25

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0260_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 31
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT14_066` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_066.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 3 |
| `BT14_071` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_071.cs` | `OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 2 |
| `BT14_074` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_074.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectJogress` | 2 |
| `BT14_076` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_076.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT14_078` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_078.cs` | `OnDestroyedAnyone, OnEndTurn` | `max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 6 |
| `BT14_079` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_079.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT14_081` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_081.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 6 |
| `BT14_086` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_086.cs` | `None, OnDeclaration, OnEndTurn, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 4 |
| `BT14_087` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_087.cs` | `None, OnAllyAttack, OnDeclaration, OnEndTurn, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT14_089` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_089.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT14-066#2992@base` | `BT14-066` | 2992 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_066.asset` |
| `BT14-066#4678@P0` | `BT14-066` | 4678 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_066_P0.asset` |
| `BT14-066#8177@P1` | `BT14-066` | 8177 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_066_P1.asset` |
| `BT14-071#2998@base` | `BT14-071` | 2998 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_071.asset` |
| `BT14-071#2999@P1` | `BT14-071` | 2999 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_071_P1.asset` |
| `BT14-074#3002@base` | `BT14-074` | 3002 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_074.asset` |
| `BT14-074#4680@P1` | `BT14-074` | 4680 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_074_P1.asset` |
| `BT14-076#3004@base` | `BT14-076` | 3004 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_076.asset` |
| `BT14-076#4682@P0` | `BT14-076` | 4682 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_076_P0.asset` |
| `BT14-078#3006@base` | `BT14-078` | 3006 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_078.asset` |
| `BT14-078#4683@P0` | `BT14-078` | 4683 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_078_P0.asset` |
| `BT14-078#4684@P1` | `BT14-078` | 4684 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_078_P1.asset` |
| `BT14-078#4685@P2` | `BT14-078` | 4685 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_078_P2.asset` |
| `BT14-078#4686@P3` | `BT14-078` | 4686 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_078_P3.asset` |
| `BT14-078#8178@P4` | `BT14-078` | 8178 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_078_P4.asset` |
| `BT14-079#3007@base` | `BT14-079` | 3007 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_079.asset` |
| `BT14-079#4687@P0` | `BT14-079` | 4687 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_079_P0.asset` |
| `BT14-081#3009@base` | `BT14-081` | 3009 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_081.asset` |
| `BT14-081#3010@P1` | `BT14-081` | 3010 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_081_P1.asset` |
| `BT14-081#4689@P2` | `BT14-081` | 4689 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_081_P2.asset` |
| `BT14-081#4690@P3` | `BT14-081` | 4690 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_081_P3.asset` |
| `BT14-081#4691@P4` | `BT14-081` | 4691 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_081_P4.asset` |
| `BT14-081#8179@P5` | `BT14-081` | 8179 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_081_P5.asset` |
| `BT14-086#3019@base` | `BT14-086` | 3019 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Tamer/BT14_086.asset` |
| `BT14-086#3020@P1` | `BT14-086` | 3020 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Tamer/BT14_086_P1.asset` |
| `BT14-086#4697@P0` | `BT14-086` | 4697 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Tamer/BT14_086_P0.asset` |
| `BT14-086#4698@P2` | `BT14-086` | 4698 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Tamer/BT14_086_P2.asset` |
| `BT14-087#3021@base` | `BT14-087` | 3021 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Tamer/BT14_087.asset` |
| `BT14-087#3022@P1` | `BT14-087` | 3022 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Tamer/BT14_087_P1.asset` |
| `BT14-087#4699@P0` | `BT14-087` | 4699 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Tamer/BT14_087_P0.asset` |
| `BT14-089#3024@base` | `BT14-089` | 3024 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Option/BT14_089.asset` |

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
