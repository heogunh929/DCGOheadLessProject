# C0383_special_digivolution_play - special digivolution/play mechanics card porting 148

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0383_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 25
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX8_064` | `DCGO/Assets/Scripts/CardEffect/EX8/Purple/EX8_064.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX8_065` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_065.cs` | `OnAllyAttack, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `EX8_072` | `DCGO/Assets/Scripts/CardEffect/EX8/Purple/EX8_072.cs` | `OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX8_073` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_073.cs` | `None, OnAllyAttack, OnEndAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `EX9_005` | `DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_005.cs` | `None, OnAllyAttack, OnDeclaration` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `EX9_012` | `DCGO/Assets/Scripts/CardEffect/EX9/Red/EX9_012.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX9_018` | `DCGO/Assets/Scripts/CardEffect/EX9/Blue/EX9_018.cs` | `BeforePayCost, None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 3 |
| `EX9_019` | `DCGO/Assets/Scripts/CardEffect/EX9/Blue/EX9_019.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX9_021` | `DCGO/Assets/Scripts/CardEffect/EX9/Blue/EX9_021.cs` | `None, OnEndAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `EX9_024` | `DCGO/Assets/Scripts/CardEffect/EX9/Yellow/EX9_024.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX8-064#4173@base` | `EX8-064` | 4173 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_064.asset` |
| `EX8-064#4174@P1` | `EX8-064` | 4174 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_064_P1.asset` |
| `EX8-065#4175@base` | `EX8-065` | 4175 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Tamer/EX8_065.asset` |
| `EX8-065#4176@P1` | `EX8-065` | 4176 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Tamer/EX8_065_P1.asset` |
| `EX8-065#9200@P2` | `EX8-065` | 9200 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Tamer/EX8_065_P2.asset` |
| `EX8-072#4189@base` | `EX8-072` | 4189 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Option/EX8_072.asset` |
| `EX8-073#4190@base` | `EX8-073` | 4190 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_073.asset` |
| `EX8-073#4191@P1` | `EX8-073` | 4191 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_073_P1.asset` |
| `EX8-073#4193@P2` | `EX8-073` | 4193 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_073_P2.asset` |
| `EX8-073#9203@P3` | `EX8-073` | 9203 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_073_P3.asset` |
| `EX8-073#9204@P4` | `EX8-073` | 9204 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_073_P4.asset` |
| `EX9-005#6839@base` | `EX9-005` | 6839 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/DigiEgg/EX9_005.asset` |
| `EX9-005#6840@P1` | `EX9-005` | 6840 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/DigiEgg/EX9_005_P1.asset` |
| `EX9-012#6855@base` | `EX9-012` | 6855 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_012.asset` |
| `EX9-012#6856@P1` | `EX9-012` | 6856 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_012_P1.asset` |
| `EX9-018#6867@base` | `EX9-018` | 6867 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_018.asset` |
| `EX9-018#6868@P1` | `EX9-018` | 6868 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_018_P1.asset` |
| `EX9-018#6869@P2` | `EX9-018` | 6869 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_018_P2.asset` |
| `EX9-019#6870@base` | `EX9-019` | 6870 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_019.asset` |
| `EX9-019#6871@P1` | `EX9-019` | 6871 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_019_P1.asset` |
| `EX9-021#6874@base` | `EX9-021` | 6874 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_021.asset` |
| `EX9-021#6875@P1` | `EX9-021` | 6875 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_021_P1.asset` |
| `EX9-021#9206@P2` | `EX9-021` | 9206 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_021_P2.asset` |
| `EX9-024#6880@base` | `EX9-024` | 6880 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_024.asset` |
| `EX9-024#6881@P1` | `EX9-024` | 6881 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_024_P1.asset` |

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
