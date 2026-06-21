# C0403_special_digivolution_play - special digivolution/play mechanics card porting 168

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0403_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 25
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST14_07` | `DCGO/Assets/Scripts/CardEffect/ST14/Purple/ST14_07.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `ST14_09` | `DCGO/Assets/Scripts/CardEffect/ST14/Purple/ST14_09.cs` | `None, OnAllyAttack, OnDiscardLibrary` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `ST14_12` | `DCGO/Assets/Scripts/CardEffect/ST14/Purple/ST14_12.cs` | `OnDeclaration, OnDiscardLibrary, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `ST15_02` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_02.cs` | `None, OnAttackTargetChanged, OnStartMainPhase` | `inherited, max_count_per_turn, static_or_continuous` | `SelectAttackTarget, SelectJogress` | 3 |
| `ST15_15` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_15.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `ST15_16` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_16.cs` | `OnStartMainPhase, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `ST16_03` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_03.cs` | `None, OnAllyAttack, OnStartMainPhase` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand, SelectJogress` | 3 |
| `ST16_15` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_15.cs` | `None, OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `ST16_16` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_16.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `ST17_02` | `DCGO/Assets/Scripts/CardEffect/ST17/Green/ST17_02.cs` | `None, OnDeclaration` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST14-07#2823@base` | `ST14-07` | 2823 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_07.asset` |
| `ST14-07#4920@P0` | `ST14-07` | 4920 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_07_P0.asset` |
| `ST14-07#4921@P1` | `ST14-07` | 4921 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_07_P1.asset` |
| `ST14-09#2826@base` | `ST14-09` | 2826 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_09.asset` |
| `ST14-09#9032@P1` | `ST14-09` | 9032 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_09_P1.asset` |
| `ST14-12#2829@base` | `ST14-12` | 2829 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Option/ST14_12.asset` |
| `ST14-12#4925@P0` | `ST14-12` | 4925 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Option/ST14_12_P0.asset` |
| `ST14-12#9033@P1` | `ST14-12` | 9033 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Option/ST14_12_P1.asset` |
| `ST15-02#2831@base` | `ST15-02` | 2831 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_02.asset` |
| `ST15-02#4927@P0` | `ST15-02` | 4927 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_02_P0.asset` |
| `ST15-02#4928@P1` | `ST15-02` | 4928 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_02_P1.asset` |
| `ST15-15#2844@base` | `ST15-15` | 2844 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Option/ST15_15.asset` |
| `ST15-15#4941@P0` | `ST15-15` | 4941 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Option/ST15_15_P0.asset` |
| `ST15-16#2845@base` | `ST15-16` | 2845 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Option/ST15_16.asset` |
| `ST15-16#4942@P0` | `ST15-16` | 4942 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Option/ST15_16_P0.asset` |
| `ST16-03#2848@base` | `ST16-03` | 2848 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_03.asset` |
| `ST16-03#4945@P0` | `ST16-03` | 4945 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_03_P0.asset` |
| `ST16-03#4946@P1` | `ST16-03` | 4946 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_03_P1.asset` |
| `ST16-15#2860@base` | `ST16-15` | 2860 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Option/ST16_15.asset` |
| `ST16-15#4958@P0` | `ST16-15` | 4958 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Option/ST16_15_P0.asset` |
| `ST16-16#2861@base` | `ST16-16` | 2861 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Option/ST16_16.asset` |
| `ST16-16#4959@P0` | `ST16-16` | 4959 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Option/ST16_16_P0.asset` |
| `ST17-02#3268@base` | `ST17-02` | 3268 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_02.asset` |
| `ST17-02#3269@P1` | `ST17-02` | 3269 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_02_P1.asset` |
| `ST17-02#9035@P2` | `ST17-02` | 9035 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_02_P2.asset` |

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
