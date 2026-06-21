# C0239_special_digivolution_play - special digivolution/play mechanics card porting 4

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0239_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT10_058` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_058.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT10_060` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_060.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous` | `SelectJogress` | 3 |
| `BT10_061` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_061.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 1 |
| `BT10_063` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_063.cs` | `None` | `static_or_continuous` | `SelectJogress, SelectDigiXros` | 1 |
| `BT10_067` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_067.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT10_068` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_068.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 3 |
| `BT10_069` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_069.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT10_073` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_073.cs` | `OnDestroyedAnyone, OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT10_075` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_075.cs` | `OnDestroyedAnyone, OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT10_077` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_077.cs` | `None, OnAddHand, OnDestroyedAnyone, OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectDigiXros` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT10-058#2101@base` | `BT10-058` | 2101 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_058.asset` |
| `BT10-060#2103@base` | `BT10-060` | 2103 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_060.asset` |
| `BT10-060#2104@P1` | `BT10-060` | 2104 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_060_P1.asset` |
| `BT10-060#4329@P0` | `BT10-060` | 4329 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_060_P0.asset` |
| `BT10-061#2105@base` | `BT10-061` | 2105 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_061.asset` |
| `BT10-063#2107@base` | `BT10-063` | 2107 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_063.asset` |
| `BT10-067#2111@base` | `BT10-067` | 2111 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_067.asset` |
| `BT10-067#4332@P0` | `BT10-067` | 4332 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_067_P0.asset` |
| `BT10-068#2112@base` | `BT10-068` | 2112 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_068.asset` |
| `BT10-068#2113@P1` | `BT10-068` | 2113 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_068_P1.asset` |
| `BT10-068#4333@P2` | `BT10-068` | 4333 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_068_P2.asset` |
| `BT10-069#2114@base` | `BT10-069` | 2114 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_069.asset` |
| `BT10-069#2115@P1` | `BT10-069` | 2115 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_069_P1.asset` |
| `BT10-073#2119@base` | `BT10-073` | 2119 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_073.asset` |
| `BT10-073#4335@P0` | `BT10-073` | 4335 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_073_P0.asset` |
| `BT10-075#2121@base` | `BT10-075` | 2121 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_075.asset` |
| `BT10-075#4336@P0` | `BT10-075` | 4336 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_075_P0.asset` |
| `BT10-077#2123@base` | `BT10-077` | 2123 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_077.asset` |

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
