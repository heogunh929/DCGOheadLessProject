# C0270_special_digivolution_play - special digivolution/play mechanics card porting 35

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0270_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 27
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT16_085` | `DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_085.cs` | `OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 5 |
| `BT16_087` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_087.cs` | `None, OnDeclaration, OnDetermineDoSecurityCheck, OnEndTurn, OnStartTurn, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 4 |
| `BT16_088` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_088.cs` | `OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 3 |
| `BT16_089` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_089.cs` | `BeforePayCost, OnDestroyedAnyone, OnEndTurn, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT16_090` | `DCGO/Assets/Scripts/CardEffect/BT16/White/BT16_090.cs` | `OnDeclaration, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT16_091` | `DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_091.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `BT16_092` | `DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_092.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT16_093` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_093.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT16_094` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_094.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT16_096` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_096.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT16-085#3409@base` | `BT16-085` | 3409 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Tamer/BT16_085.asset` |
| `BT16-085#3410@P1` | `BT16-085` | 3410 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Tamer/BT16_085_P1.asset` |
| `BT16-085#4817@P0` | `BT16-085` | 4817 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Tamer/BT16_085_P0.asset` |
| `BT16-085#8212@P2` | `BT16-085` | 8212 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Tamer/BT16_085_P2.asset` |
| `BT16-085#8213@P3` | `BT16-085` | 8213 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Tamer/BT16_085_P3.asset` |
| `BT16-087#3412@base` | `BT16-087` | 3412 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Tamer/BT16_087.asset` |
| `BT16-087#3413@P1` | `BT16-087` | 3413 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Tamer/BT16_087_P1.asset` |
| `BT16-087#4820@P0` | `BT16-087` | 4820 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Tamer/BT16_087_P0.asset` |
| `BT16-087#4821@P2` | `BT16-087` | 4821 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Tamer/BT16_087_P2.asset` |
| `BT16-088#3414@base` | `BT16-088` | 3414 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Tamer/BT16_088.asset` |
| `BT16-088#3415@P1` | `BT16-088` | 3415 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Tamer/BT16_088_P1.asset` |
| `BT16-088#4822@P0` | `BT16-088` | 4822 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Tamer/BT16_088_P0.asset` |
| `BT16-089#3416@base` | `BT16-089` | 3416 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Tamer/BT16_089.asset` |
| `BT16-089#4823@P0` | `BT16-089` | 4823 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Tamer/BT16_089_P0.asset` |
| `BT16-090#3417@base` | `BT16-090` | 3417 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Tamer/BT16_090.asset` |
| `BT16-090#3418@P1` | `BT16-090` | 3418 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Tamer/BT16_090_P1.asset` |
| `BT16-090#4824@P0` | `BT16-090` | 4824 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Tamer/BT16_090_P0.asset` |
| `BT16-091#3419@base` | `BT16-091` | 3419 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Option/BT16_091.asset` |
| `BT16-091#4825@P0` | `BT16-091` | 4825 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Option/BT16_091_P0.asset` |
| `BT16-092#3420@base` | `BT16-092` | 3420 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Option/BT16_092.asset` |
| `BT16-092#4826@P0` | `BT16-092` | 4826 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Option/BT16_092_P0.asset` |
| `BT16-093#3421@base` | `BT16-093` | 3421 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Option/BT16_093.asset` |
| `BT16-093#4827@P0` | `BT16-093` | 4827 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Option/BT16_093_P0.asset` |
| `BT16-094#3422@base` | `BT16-094` | 3422 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Option/BT16_094.asset` |
| `BT16-096#3424@base` | `BT16-096` | 3424 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Option/BT16_096.asset` |
| `BT16-096#4828@P0` | `BT16-096` | 4828 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Option/BT16_096_P0.asset` |
| `BT16-096#4829@P1` | `BT16-096` | 4829 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Option/BT16_096_P1.asset` |

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
