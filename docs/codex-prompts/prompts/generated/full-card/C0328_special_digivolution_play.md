# C0328_special_digivolution_play - special digivolution/play mechanics card porting 93

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0328_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT5_029` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_029.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 2 |
| `BT5_046` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_046.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous` | `SelectCard, SelectBurstDigivolution` | 3 |
| `BT5_047` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_047.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT5_050` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_050.cs` | `OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, static_or_continuous` | `SelectBurstDigivolution` | 2 |
| `BT5_056` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_056.cs` | `OnDeclaration, OnUseDigiburst` | `max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 1 |
| `BT5_057` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_057.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectBurstDigivolution` | 2 |
| `BT5_059` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_059.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT5_060` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_060.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT5_063` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_063.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT5_067` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_067.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT5-029#8587@P0` | `BT5-029` | 8587 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_029_P0.asset` |
| `BT5-029#976@base` | `BT5-029` | 976 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_029.asset` |
| `BT5-046#1004@base` | `BT5-046` | 1004 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_046.asset` |
| `BT5-046#8601@P0` | `BT5-046` | 8601 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_046_P0.asset` |
| `BT5-046#8602@P1` | `BT5-046` | 8602 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_046_P1.asset` |
| `BT5-047#1005@base` | `BT5-047` | 1005 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_047.asset` |
| `BT5-050#1008@base` | `BT5-050` | 1008 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_050.asset` |
| `BT5-050#8604@P1` | `BT5-050` | 8604 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_050_P1.asset` |
| `BT5-056#1014@base` | `BT5-056` | 1014 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_056.asset` |
| `BT5-057#1015@base` | `BT5-057` | 1015 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_057.asset` |
| `BT5-057#8606@P0` | `BT5-057` | 8606 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_057_P0.asset` |
| `BT5-059#1017@base` | `BT5-059` | 1017 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_059.asset` |
| `BT5-060#1018@base` | `BT5-060` | 1018 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_060.asset` |
| `BT5-060#8608@P0` | `BT5-060` | 8608 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_060_P0.asset` |
| `BT5-063#1022@base` | `BT5-063` | 1022 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_063.asset` |
| `BT5-067#1027@base` | `BT5-067` | 1027 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_067.asset` |
| `BT5-067#8613@P0` | `BT5-067` | 8613 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_067_P0.asset` |

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
