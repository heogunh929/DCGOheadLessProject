# C0257_special_digivolution_play - special digivolution/play mechanics card porting 22

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0257_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 33
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT13_088` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_088.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 5 |
| `BT13_089` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_089.cs` | `OnDestroyedAnyone, OnEndTurn` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 2 |
| `BT13_090` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_090.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT13_091` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_091.cs` | `OnEndAttack, OnEndTurn, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT13_092` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_092.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectJogress, SelectBurstDigivolution` | 3 |
| `BT13_093` | `DCGO/Assets/Scripts/CardEffect/BT13/White/BT13_093.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectJogress` | 4 |
| `BT13_094` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_094.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT13_095` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_095.cs` | `OnEnterFieldAnyone, OnStartTurn, OnTappedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 6 |
| `BT13_097` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_097.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `BT13_098` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_098.cs` | `OnDeclaration, OnDiscardSecurity, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT13-088#2748@base` | `BT13-088` | 2748 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_088.asset` |
| `BT13-088#4605@P0` | `BT13-088` | 4605 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_088_P0.asset` |
| `BT13-088#4606@P1` | `BT13-088` | 4606 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_088_P1.asset` |
| `BT13-088#4607@P2` | `BT13-088` | 4607 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_088_P2.asset` |
| `BT13-088#4608@P3` | `BT13-088` | 4608 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_088_P3.asset` |
| `BT13-089#2749@base` | `BT13-089` | 2749 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_089.asset` |
| `BT13-089#4609@P0` | `BT13-089` | 4609 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_089_P0.asset` |
| `BT13-090#2750@base` | `BT13-090` | 2750 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_090.asset` |
| `BT13-090#2751@P1` | `BT13-090` | 2751 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_090_P1.asset` |
| `BT13-090#4610@P0` | `BT13-090` | 4610 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_090_P0.asset` |
| `BT13-091#2752@base` | `BT13-091` | 2752 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_091.asset` |
| `BT13-091#2753@P1` | `BT13-091` | 2753 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_091_P1.asset` |
| `BT13-092#2754@base` | `BT13-092` | 2754 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_092.asset` |
| `BT13-092#2755@P1` | `BT13-092` | 2755 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_092_P1.asset` |
| `BT13-092#2756@P2` | `BT13-092` | 2756 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_092_P2.asset` |
| `BT13-093#2757@base` | `BT13-093` | 2757 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Digimon/BT13_093.asset` |
| `BT13-093#4611@P0` | `BT13-093` | 4611 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Digimon/BT13_093_P0.asset` |
| `BT13-093#4612@P1` | `BT13-093` | 4612 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Digimon/BT13_093_P1.asset` |
| `BT13-093#8150@P2` | `BT13-093` | 8150 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/White/Digimon/BT13_093_P2.asset` |
| `BT13-094#2758@base` | `BT13-094` | 2758 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Tamer/BT13_094.asset` |
| `BT13-094#4613@P0` | `BT13-094` | 4613 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Tamer/BT13_094_P0.asset` |
| `BT13-095#2759@base` | `BT13-095` | 2759 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Tamer/BT13_095.asset` |
| `BT13-095#2760@P1` | `BT13-095` | 2760 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Tamer/BT13_095_P1.asset` |
| `BT13-095#4614@P0` | `BT13-095` | 4614 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Tamer/BT13_095_P0.asset` |
| `BT13-095#8151@P2` | `BT13-095` | 8151 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Tamer/BT13_095_P2.asset` |
| `BT13-095#8152@P3` | `BT13-095` | 8152 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Tamer/BT13_095_P3.asset` |
| `BT13-095#8153@P4` | `BT13-095` | 8153 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Tamer/BT13_095_P4.asset` |
| `BT13-097#2762@base` | `BT13-097` | 2762 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Tamer/BT13_097.asset` |
| `BT13-097#2763@P1` | `BT13-097` | 2763 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Tamer/BT13_097_P1.asset` |
| `BT13-097#4616@P0` | `BT13-097` | 4616 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Tamer/BT13_097_P0.asset` |
| `BT13-098#2764@base` | `BT13-098` | 2764 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Tamer/BT13_098.asset` |
| `BT13-098#2765@P1` | `BT13-098` | 2765 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Tamer/BT13_098_P1.asset` |
| `BT13-098#4617@P0` | `BT13-098` | 4617 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Tamer/BT13_098_P0.asset` |

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
