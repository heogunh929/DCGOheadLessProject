# C0399_special_digivolution_play - special digivolution/play mechanics card porting 164

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0399_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 14
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `RB1_005` | `DCGO/Assets/Scripts/CardEffect/RB1/Red/RB1_005.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `RB1_008` | `DCGO/Assets/Scripts/CardEffect/RB1/Red/RB1_008.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `RB1_009` | `DCGO/Assets/Scripts/CardEffect/RB1/Red/RB1_009.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `RB1_011` | `DCGO/Assets/Scripts/CardEffect/RB1/Blue/RB1_011.cs` | `OnDiscardHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `RB1_013` | `DCGO/Assets/Scripts/CardEffect/RB1/Blue/RB1_013.cs` | `OnDiscardHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `RB1_015` | `DCGO/Assets/Scripts/CardEffect/RB1/Blue/RB1_015.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `RB1_017` | `DCGO/Assets/Scripts/CardEffect/RB1/Yellow/RB1_017.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `SelectCard, SelectJogress` | 1 |
| `RB1_018` | `DCGO/Assets/Scripts/CardEffect/RB1/Yellow/RB1_018.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `RB1_019` | `DCGO/Assets/Scripts/CardEffect/RB1/Yellow/RB1_019.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectSecurity, SelectJogress` | 2 |
| `RB1_020` | `DCGO/Assets/Scripts/CardEffect/RB1/Green/RB1_020.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `RB1-005#2866@base` | `RB1-005` | 2866 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Digimon/RB1_005.asset` |
| `RB1-005#2867@P1` | `RB1-005` | 2867 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Digimon/RB1_005_P1.asset` |
| `RB1-008#2870@base` | `RB1-008` | 2870 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Digimon/RB1_008.asset` |
| `RB1-009#2871@base` | `RB1-009` | 2871 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Digimon/RB1_009.asset` |
| `RB1-011#2875@base` | `RB1-011` | 2875 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Blue/Digimon/RB1_011.asset` |
| `RB1-011#2876@P1` | `RB1-011` | 2876 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Blue/Digimon/RB1_011_P1.asset` |
| `RB1-013#2878@base` | `RB1-013` | 2878 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Blue/Digimon/RB1_013.asset` |
| `RB1-015#2880@base` | `RB1-015` | 2880 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Blue/Digimon/RB1_015.asset` |
| `RB1-017#2883@base` | `RB1-017` | 2883 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Yellow/Digimon/RB1_017.asset` |
| `RB1-018#2884@base` | `RB1-018` | 2884 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Yellow/Digimon/RB1_018.asset` |
| `RB1-019#2885@base` | `RB1-019` | 2885 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Yellow/Digimon/RB1_019.asset` |
| `RB1-019#2886@P1` | `RB1-019` | 2886 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Yellow/Digimon/RB1_019_P1.asset` |
| `RB1-020#2887@base` | `RB1-020` | 2887 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Green/Digimon/RB1_020.asset` |
| `RB1-020#2888@P1` | `RB1-020` | 2888 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Green/Digimon/RB1_020_P1.asset` |

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
