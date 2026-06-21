# C0238_special_digivolution_play - special digivolution/play mechanics card porting 3

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0238_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT10_021` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_021.cs` | `OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT10_029` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_029.cs` | `None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `SelectJogress` | 1 |
| `BT10_031` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_031.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 1 |
| `BT10_032` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_032.cs` | `OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT10_034` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_034.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT10_036` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_036.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 1 |
| `BT10_039` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_039.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT10_041` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_041.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `BT10_049` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_049.cs` | `None, OnDestroyedAnyone, OnDetermineDoSecurityCheck` | `inherited, static_or_continuous` | `SelectJogress` | 1 |
| `BT10_050` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_050.cs` | `None, OnDetermineDoSecurityCheck` | `inherited, static_or_continuous` | `SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT10-021#2056@base` | `BT10-021` | 2056 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_021.asset` |
| `BT10-021#4305@P0` | `BT10-021` | 4305 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_021_P0.asset` |
| `BT10-029#2067@base` | `BT10-029` | 2067 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_029.asset` |
| `BT10-031#2069@base` | `BT10-031` | 2069 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_031.asset` |
| `BT10-032#2070@base` | `BT10-032` | 2070 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_032.asset` |
| `BT10-032#4309@P0` | `BT10-032` | 4309 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_032_P0.asset` |
| `BT10-034#2072@base` | `BT10-034` | 2072 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_034.asset` |
| `BT10-036#2074@base` | `BT10-036` | 2074 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_036.asset` |
| `BT10-039#2077@base` | `BT10-039` | 2077 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_039.asset` |
| `BT10-039#4311@P0` | `BT10-039` | 4311 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_039_P0.asset` |
| `BT10-041#2079@base` | `BT10-041` | 2079 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_041.asset` |
| `BT10-041#2080@P1` | `BT10-041` | 2080 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_041_P1.asset` |
| `BT10-049#2090@base` | `BT10-049` | 2090 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_049.asset` |
| `BT10-050#2091@base` | `BT10-050` | 2091 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_050.asset` |
| `BT10-050#4322@P1` | `BT10-050` | 4322 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_050_P1.asset` |

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
