# C0311_special_digivolution_play - special digivolution/play mechanics card porting 76

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0311_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT24_045` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_045.cs` | `None, OnAllyAttack, OnDiscardHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT24_046` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_046.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT24_053` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_053.cs` | `None, OnDeclaration` | `inherited, linked, static_or_continuous` | `-` | 1 |
| `BT24_054` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_054.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT24_056` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_056.cs` | `None, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `inherited, linked, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT24_057` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_057.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone, SecuritySkill` | `linked, max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT24_065` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_065.cs` | `None, OnEndTurn, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `BT24_066` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_066.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT24_067` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_067.cs` | `None, OnDeclaration, OnDestroyedAnyone, WhenLinked` | `inherited, linked, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT24_071` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_071.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone` | `linked, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT24-045#7574@base` | `BT24-045` | 7574 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_045.asset` |
| `BT24-046#7575@base` | `BT24-046` | 7575 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_046.asset` |
| `BT24-053#7583@base` | `BT24-053` | 7583 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_053.asset` |
| `BT24-054#7584@base` | `BT24-054` | 7584 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_054.asset` |
| `BT24-054#7585@P1` | `BT24-054` | 7585 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_054_P1.asset` |
| `BT24-056#7587@base` | `BT24-056` | 7587 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_056.asset` |
| `BT24-057#7588@base` | `BT24-057` | 7588 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_057.asset` |
| `BT24-065#7596@base` | `BT24-065` | 7596 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_065.asset` |
| `BT24-065#7597@P1` | `BT24-065` | 7597 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_065_P1.asset` |
| `BT24-066#7598@base` | `BT24-066` | 7598 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_066.asset` |
| `BT24-067#7599@base` | `BT24-067` | 7599 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_067.asset` |
| `BT24-067#7600@P1` | `BT24-067` | 7600 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_067_P1.asset` |
| `BT24-071#7604@base` | `BT24-071` | 7604 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_071.asset` |

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
