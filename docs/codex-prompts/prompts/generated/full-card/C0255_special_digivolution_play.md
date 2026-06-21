# C0255_special_digivolution_play - special digivolution/play mechanics card porting 20

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0255_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT13_040` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_040.cs` | `None, WhenRemoveField` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 6 |
| `BT13_042` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_042.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT13_045` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_045.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT13_049` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_049.cs` | `None, OnEnterFieldAnyone` | `background, inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT13_054` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_054.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT13_055` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_055.cs` | `None, OnDeclaration, OnEndBattle` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT13_060` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_060.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress, SelectBurstDigivolution` | 3 |
| `BT13_062` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_062.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT13_064` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_064.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT13_068` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_068.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT13-040#2690@base` | `BT13-040` | 2690 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_040.asset` |
| `BT13-040#2691@P1` | `BT13-040` | 2691 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_040_P1.asset` |
| `BT13-040#4581@P0` | `BT13-040` | 4581 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_040_P0.asset` |
| `BT13-040#4582@P2` | `BT13-040` | 4582 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_040_P2.asset` |
| `BT13-040#4583@P3` | `BT13-040` | 4583 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_040_P3.asset` |
| `BT13-040#4584@P4` | `BT13-040` | 4584 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_040_P4.asset` |
| `BT13-042#2693@base` | `BT13-042` | 2693 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_042.asset` |
| `BT13-045#2696@base` | `BT13-045` | 2696 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_045.asset` |
| `BT13-045#4587@P0` | `BT13-045` | 4587 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_045_P0.asset` |
| `BT13-049#2701@base` | `BT13-049` | 2701 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_049.asset` |
| `BT13-049#4589@P1` | `BT13-049` | 4589 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_049_P1.asset` |
| `BT13-049#4590@P2` | `BT13-049` | 4590 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_049_P2.asset` |
| `BT13-054#2706@base` | `BT13-054` | 2706 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_054.asset` |
| `BT13-055#2707@base` | `BT13-055` | 2707 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_055.asset` |
| `BT13-060#2715@base` | `BT13-060` | 2715 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_060.asset` |
| `BT13-060#2716@P1` | `BT13-060` | 2716 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_060_P1.asset` |
| `BT13-060#2717@P2` | `BT13-060` | 2717 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_060_P2.asset` |
| `BT13-062#2719@base` | `BT13-062` | 2719 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_062.asset` |
| `BT13-064#2721@base` | `BT13-064` | 2721 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_064.asset` |
| `BT13-064#4595@P0` | `BT13-064` | 4595 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_064_P0.asset` |
| `BT13-068#2725@base` | `BT13-068` | 2725 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_068.asset` |

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
