# C0324_special_digivolution_play - special digivolution/play mechanics card porting 89

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0324_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT4_049` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_049.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectBurstDigivolution` | 2 |
| `BT4_051` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_051.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectBurstDigivolution` | 1 |
| `BT4_059` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_059.cs` | `OnAllyAttack, OnDeclaration` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 2 |
| `BT4_062` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_062.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectOrder, SelectBurstDigivolution` | 3 |
| `BT4_063` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_063.cs` | `OnDestroyedAnyone` | `max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT4_068` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_068.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 2 |
| `BT4_071` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_071.cs` | `OnDestroyedAnyone` | `max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT4_072` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_072.cs` | `None, OnDeclaration` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 2 |
| `BT4_081` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_081.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 1 |
| `BT4_086` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_086.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT4-049#829@base` | `BT4-049` | 829 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_049.asset` |
| `BT4-049#8520@P0` | `BT4-049` | 8520 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_049_P0.asset` |
| `BT4-051#831@base` | `BT4-051` | 831 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_051.asset` |
| `BT4-059#839@base` | `BT4-059` | 839 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_059.asset` |
| `BT4-059#840@P1` | `BT4-059` | 840 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_059_P1.asset` |
| `BT4-062#843@base` | `BT4-062` | 843 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_062.asset` |
| `BT4-062#844@P1` | `BT4-062` | 844 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_062_P1.asset` |
| `BT4-062#8527@P2` | `BT4-062` | 8527 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_062_P2.asset` |
| `BT4-063#845@base` | `BT4-063` | 845 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_063.asset` |
| `BT4-063#846@P1` | `BT4-063` | 846 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_063_P1.asset` |
| `BT4-063#8528@P0` | `BT4-063` | 8528 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_063_P0.asset` |
| `BT4-068#852@base` | `BT4-068` | 852 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_068.asset` |
| `BT4-068#8531@P0` | `BT4-068` | 8531 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_068_P0.asset` |
| `BT4-071#855@base` | `BT4-071` | 855 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_071.asset` |
| `BT4-072#856@base` | `BT4-072` | 856 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_072.asset` |
| `BT4-072#857@P1` | `BT4-072` | 857 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_072_P1.asset` |
| `BT4-081#869@base` | `BT4-081` | 869 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_081.asset` |
| `BT4-086#8540@P0` | `BT4-086` | 8540 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_086_P0.asset` |
| `BT4-086#877@base` | `BT4-086` | 877 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_086.asset` |

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
