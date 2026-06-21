# C0262_special_digivolution_play - special digivolution/play mechanics card porting 27

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0262_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT14_101` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_101.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity, SelectAttackTarget, SelectJogress` | 5 |
| `BT14_102` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_102.cs` | `None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 2 |
| `BT15_020` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_020.cs` | `None, OnAllyAttack, OnStartMainPhase` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT15_021` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_021.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT15_024` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_024.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 2 |
| `BT15_031` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_031.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT15_032` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_032.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT15_034` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_034.cs` | `OnLoseSecurity, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT15_035` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_035.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT15_039` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_039.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT14-101#3036@base` | `BT14-101` | 3036 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_101.asset` |
| `BT14-101#3037@P1` | `BT14-101` | 3037 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_101_P1.asset` |
| `BT14-101#8180@P2` | `BT14-101` | 8180 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_101_P2.asset` |
| `BT14-101#8181@P3` | `BT14-101` | 8181 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_101_P3.asset` |
| `BT14-101#8182@P4` | `BT14-101` | 8182 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_101_P4.asset` |
| `BT14-102#3038@base` | `BT14-102` | 3038 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_102.asset` |
| `BT14-102#3039@P1` | `BT14-102` | 3039 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_102_P1.asset` |
| `BT15-020#3142@base` | `BT15-020` | 3142 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_020.asset` |
| `BT15-020#3143@P1` | `BT15-020` | 3143 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_020_P1.asset` |
| `BT15-021#3144@base` | `BT15-021` | 3144 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_021.asset` |
| `BT15-021#4722@P0` | `BT15-021` | 4722 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_021_P0.asset` |
| `BT15-024#3147@base` | `BT15-024` | 3147 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_024.asset` |
| `BT15-024#8183@P1` | `BT15-024` | 8183 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_024_P1.asset` |
| `BT15-031#3156@base` | `BT15-031` | 3156 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_031.asset` |
| `BT15-031#4726@P0` | `BT15-031` | 4726 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_031_P0.asset` |
| `BT15-032#3157@base` | `BT15-032` | 3157 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_032.asset` |
| `BT15-032#4727@P0` | `BT15-032` | 4727 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_032_P0.asset` |
| `BT15-034#3159@base` | `BT15-034` | 3159 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_034.asset` |
| `BT15-034#4728@P0` | `BT15-034` | 4728 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_034_P0.asset` |
| `BT15-035#3160@base` | `BT15-035` | 3160 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_035.asset` |
| `BT15-039#3167@base` | `BT15-039` | 3167 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_039.asset` |
| `BT15-039#4729@P0` | `BT15-039` | 4729 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_039_P0.asset` |

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
