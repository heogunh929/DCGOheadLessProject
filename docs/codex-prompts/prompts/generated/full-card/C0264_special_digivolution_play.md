# C0264_special_digivolution_play - special digivolution/play mechanics card porting 29

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0264_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT15_059` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_059.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT15_060` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_060.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress, SelectDigiXros` | 2 |
| `BT15_065` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_065.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT15_066` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_066.cs` | `None, OnEndTurn` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT15_079` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_079.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT15_081` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_081.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT15_083` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_083.cs` | `OnAddHand, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `BT15_086` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_086.cs` | `None, OnDeclaration, OnEndTurn, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 4 |
| `BT15_087` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_087.cs` | `None, OnAllyAttack, OnDeclaration, OnEndTurn, OnStartTurn, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 4 |
| `BT15_088` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_088.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT15-059#3191@base` | `BT15-059` | 3191 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_059.asset` |
| `BT15-060#3192@base` | `BT15-060` | 3192 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_060.asset` |
| `BT15-060#4742@P0` | `BT15-060` | 4742 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_060_P0.asset` |
| `BT15-065#3198@base` | `BT15-065` | 3198 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_065.asset` |
| `BT15-065#4745@P0` | `BT15-065` | 4745 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_065_P0.asset` |
| `BT15-066#3199@base` | `BT15-066` | 3199 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_066.asset` |
| `BT15-066#4746@P0` | `BT15-066` | 4746 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_066_P0.asset` |
| `BT15-079#3213@base` | `BT15-079` | 3213 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_079.asset` |
| `BT15-079#4754@P0` | `BT15-079` | 4754 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_079_P0.asset` |
| `BT15-081#3215@base` | `BT15-081` | 3215 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_081.asset` |
| `BT15-081#3216@P1` | `BT15-081` | 3216 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_081_P1.asset` |
| `BT15-083#3219@base` | `BT15-083` | 3219 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Tamer/BT15_083.asset` |
| `BT15-083#3220@P1` | `BT15-083` | 3220 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Tamer/BT15_083_P1.asset` |
| `BT15-083#4758@P0` | `BT15-083` | 4758 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Tamer/BT15_083_P0.asset` |
| `BT15-086#3225@base` | `BT15-086` | 3225 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Tamer/BT15_086.asset` |
| `BT15-086#3226@P1` | `BT15-086` | 3226 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Tamer/BT15_086_P1.asset` |
| `BT15-086#4762@P0` | `BT15-086` | 4762 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Tamer/BT15_086_P0.asset` |
| `BT15-086#4763@P2` | `BT15-086` | 4763 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Tamer/BT15_086_P2.asset` |
| `BT15-087#3227@base` | `BT15-087` | 3227 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Tamer/BT15_087.asset` |
| `BT15-087#3228@P1` | `BT15-087` | 3228 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Tamer/BT15_087_P1.asset` |
| `BT15-087#4764@P0` | `BT15-087` | 4764 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Tamer/BT15_087_P0.asset` |
| `BT15-087#4765@P2` | `BT15-087` | 4765 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Tamer/BT15_087_P2.asset` |
| `BT15-088#3229@base` | `BT15-088` | 3229 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Option/BT15_088.asset` |
| `BT15-088#4766@P0` | `BT15-088` | 4766 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Option/BT15_088_P0.asset` |

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
