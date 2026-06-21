# C0401_special_digivolution_play - special digivolution/play mechanics card porting 166

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0401_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST10_06` | `DCGO/Assets/Scripts/CardEffect/ST10/Yellow/ST10_06.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `ST10_14` | `DCGO/Assets/Scripts/CardEffect/ST10/Yellow/ST10_14.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 6 |
| `ST10_15` | `DCGO/Assets/Scripts/CardEffect/ST10/Purple/ST10_15.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `ST12_04` | `DCGO/Assets/Scripts/CardEffect/ST12/Red/ST12_04.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `ST12_06` | `DCGO/Assets/Scripts/CardEffect/ST12/Red/ST12_06.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 1 |
| `ST12_08` | `DCGO/Assets/Scripts/CardEffect/ST12/Red/ST12_08.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectAttackTarget, SelectJogress` | 1 |
| `ST12_10` | `DCGO/Assets/Scripts/CardEffect/ST12/Red/ST12_10.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `ST12_11` | `DCGO/Assets/Scripts/CardEffect/ST12/Black/ST12_11.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `ST12_13` | `DCGO/Assets/Scripts/CardEffect/ST12/White/ST12_13.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `ST12_14` | `DCGO/Assets/Scripts/CardEffect/ST12/Red/ST12_14.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST10-06#1762@base` | `ST10-06` | 1762 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Digimon/ST10_06.asset` |
| `ST10-06#1763@P1` | `ST10-06` | 1763 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Digimon/ST10_06_P1.asset` |
| `ST10-06#1764@P2` | `ST10-06` | 1764 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Digimon/ST10_06_P2.asset` |
| `ST10-06#9026@P3` | `ST10-06` | 9026 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Digimon/ST10_06_P3.asset` |
| `ST10-14#1773@base` | `ST10-14` | 1773 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Option/ST10_14.asset` |
| `ST10-14#4903@P1` | `ST10-14` | 4903 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Option/ST10_14_P1.asset` |
| `ST10-14#4904@P2` | `ST10-14` | 4904 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Option/ST10_14_P2.asset` |
| `ST10-14#9027@P3` | `ST10-14` | 9027 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Option/ST10_14_P3.asset` |
| `ST10-14#9028@P4` | `ST10-14` | 9028 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Option/ST10_14_P4.asset` |
| `ST10-14#9029@P5` | `ST10-14` | 9029 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Option/ST10_14_P5.asset` |
| `ST10-15#1774@base` | `ST10-15` | 1774 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Purple/Option/ST10_15.asset` |
| `ST12-04#2788@base` | `ST12-04` | 2788 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/Digimon/ST12_04.asset` |
| `ST12-06#2790@base` | `ST12-06` | 2790 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/Digimon/ST12_06.asset` |
| `ST12-08#2792@base` | `ST12-08` | 2792 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/Digimon/ST12_08.asset` |
| `ST12-10#2794@base` | `ST12-10` | 2794 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/Digimon/ST12_10.asset` |
| `ST12-11#2795@base` | `ST12-11` | 2795 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Black/Digimon/ST12_11.asset` |
| `ST12-13#2797@base` | `ST12-13` | 2797 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/White/Digimon/ST12_13.asset` |
| `ST12-13#4908@P1` | `ST12-13` | 4908 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST12/White/Digimon/ST12_13_P1.asset` |
| `ST12-14#2798@base` | `ST12-14` | 2798 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST12/Red/Option/ST12_14.asset` |

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
