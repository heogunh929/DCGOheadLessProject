# C0402_special_digivolution_play - special digivolution/play mechanics card porting 167

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0402_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST12_15` | `DCGO/Assets/Scripts/CardEffect/ST12/Red/ST12_15.cs` | `AfterPayCost, None, OnDeclaration, OptionSkill, SecuritySkill` | `background, max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `ST12_16` | `DCGO/Assets/Scripts/CardEffect/ST12/Black/ST12_16.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST13_04` | `DCGO/Assets/Scripts/CardEffect/ST13/Red/ST13_04.cs` | `None, OnEndTurn` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `ST13_05` | `DCGO/Assets/Scripts/CardEffect/ST13/Red/ST13_05.cs` | `None, OnAddDigivolutionCards, OnAllyAttack` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `ST13_06` | `DCGO/Assets/Scripts/CardEffect/ST13/Red/ST13_06.cs` | `None, OnEnterFieldAnyone, OnLoseSecurity` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST13_13` | `DCGO/Assets/Scripts/CardEffect/ST13/Black/ST13_13.cs` | `None, OnEndTurn` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `ST13_14` | `DCGO/Assets/Scripts/CardEffect/ST13/Black/ST13_14.cs` | `None, OnAddDigivolutionCards, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `ST13_15` | `DCGO/Assets/Scripts/CardEffect/ST13/Red/ST13_15.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST13_16` | `DCGO/Assets/Scripts/CardEffect/ST13/Black/ST13_16.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectOrder, SelectSecurity, SelectJogress` | 4 |
| `ST14_02` | `DCGO/Assets/Scripts/CardEffect/ST14/Purple/ST14_02.cs` | `OnAllyAttack, OnDiscardLibrary` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST12-15#2799@base` | `ST12-15` | 2799 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/Option/ST12_15.asset` |
| `ST12-15#4909@P1` | `ST12-15` | 4909 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/Option/ST12_15_P1.asset` |
| `ST12-16#2800@base` | `ST12-16` | 2800 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Black/Option/ST12_16.asset` |
| `ST13-04#2804@base` | `ST13-04` | 2804 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Red/Digimon/ST13_04.asset` |
| `ST13-05#2805@base` | `ST13-05` | 2805 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Red/Digimon/ST13_05.asset` |
| `ST13-06#2806@base` | `ST13-06` | 2806 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Red/Digimon/ST13_06.asset` |
| `ST13-13#2813@base` | `ST13-13` | 2813 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Black/Digimon/ST13_13.asset` |
| `ST13-14#2814@base` | `ST13-14` | 2814 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Black/Digimon/ST13_14.asset` |
| `ST13-15#2815@base` | `ST13-15` | 2815 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Red/Option/ST13_15.asset` |
| `ST13-16#2816@base` | `ST13-16` | 2816 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Black/Option/ST13_16.asset` |
| `ST13-16#4910@P1` | `ST13-16` | 4910 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Black/Option/ST13_16_P1.asset` |
| `ST13-16#4911@P2` | `ST13-16` | 4911 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Black/Option/ST13_16_P2.asset` |
| `ST13-16#4912@P3` | `ST13-16` | 4912 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Black/Option/ST13_16_P3.asset` |
| `ST14-02#2818@base` | `ST14-02` | 2818 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_02.asset` |
| `ST14-02#4915@P1` | `ST14-02` | 4915 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_02_P1.asset` |
| `ST14-02#9031@P2` | `ST14-02` | 9031 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_02_P2.asset` |

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
