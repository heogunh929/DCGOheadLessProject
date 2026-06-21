# C0382_special_digivolution_play - special digivolution/play mechanics card porting 147

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0382_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX8_031` | `DCGO/Assets/Scripts/CardEffect/EX8/Yellow/EX8_031.cs` | `None, OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `EX8_033` | `DCGO/Assets/Scripts/CardEffect/EX8/Yellow/EX8_033.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX8_037` | `DCGO/Assets/Scripts/CardEffect/EX8/Yellow/EX8_037.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 3 |
| `EX8_038` | `DCGO/Assets/Scripts/CardEffect/EX8/Green/EX8_038.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX8_045` | `DCGO/Assets/Scripts/CardEffect/EX8/Green/EX8_045.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX8_048` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_048.cs` | `OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 3 |
| `EX8_052` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_052.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `EX8_054` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_054.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectAttackTarget, SelectJogress` | 2 |
| `EX8_060` | `DCGO/Assets/Scripts/CardEffect/EX8/Purple/EX8_060.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `EX8_063` | `DCGO/Assets/Scripts/CardEffect/EX8/Purple/EX8_063.cs` | `None, OnAllyAttack, OnDiscardHand, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX8-031#4107@base` | `EX8-031` | 4107 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_031.asset` |
| `EX8-031#4108@P1` | `EX8-031` | 4108 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_031_P1.asset` |
| `EX8-033#4111@base` | `EX8-033` | 4111 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_033.asset` |
| `EX8-037#4118@base` | `EX8-037` | 4118 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_037.asset` |
| `EX8-037#4119@P1` | `EX8-037` | 4119 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_037_P1.asset` |
| `EX8-037#9195@P2` | `EX8-037` | 9195 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_037_P2.asset` |
| `EX8-038#4120@base` | `EX8-038` | 4120 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_038.asset` |
| `EX8-038#4121@P1` | `EX8-038` | 4121 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_038_P1.asset` |
| `EX8-045#4136@base` | `EX8-045` | 4136 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_045.asset` |
| `EX8-045#4137@P1` | `EX8-045` | 4137 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_045_P1.asset` |
| `EX8-048#4142@base` | `EX8-048` | 4142 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_048.asset` |
| `EX8-048#4143@P1` | `EX8-048` | 4143 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_048_P1.asset` |
| `EX8-048#9196@P2` | `EX8-048` | 9196 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_048_P2.asset` |
| `EX8-052#4149@base` | `EX8-052` | 4149 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_052.asset` |
| `EX8-052#4150@P1` | `EX8-052` | 4150 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_052_P1.asset` |
| `EX8-054#4152@base` | `EX8-054` | 4152 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_054.asset` |
| `EX8-054#4153@P1` | `EX8-054` | 4153 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_054_P1.asset` |
| `EX8-060#4163@base` | `EX8-060` | 4163 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_060.asset` |
| `EX8-063#4171@base` | `EX8-063` | 4171 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_063.asset` |
| `EX8-063#4172@P1` | `EX8-063` | 4172 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_063_P1.asset` |

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
