# C0276_special_digivolution_play - special digivolution/play mechanics card porting 41

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0276_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 33
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT17_085` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_085.cs` | `OnDeclaration, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectSecurity, SelectJogress` | 5 |
| `BT17_087` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_087.cs` | `OnEnterFieldAnyone, OnTappedAnyone, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `BT17_088` | `DCGO/Assets/Scripts/CardEffect/BT17/Green/BT17_088.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT17_089` | `DCGO/Assets/Scripts/CardEffect/BT17/Green/BT17_089.cs` | `OnTappedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `BT17_090` | `DCGO/Assets/Scripts/CardEffect/BT17/Purple/BT17_090.cs` | `OnAddDigivolutionCards, OnEndTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT17_091` | `DCGO/Assets/Scripts/CardEffect/BT17/Purple/BT17_091.cs` | `None, OnAllyAttack, OnDeclaration, OnEndTurn, OnStartTurn, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT17_093` | `DCGO/Assets/Scripts/CardEffect/BT17/White/BT17_093.cs` | `OnEndTurn, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 3 |
| `BT17_094` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_094.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 3 |
| `BT17_095` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_095.cs` | `OptionSkill, SecuritySkill, WhenRemoveField` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 4 |
| `BT17_096` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_096.cs` | `OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT17-085#3647@base` | `BT17-085` | 3647 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_085.asset` |
| `BT17-085#3648@P1` | `BT17-085` | 3648 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_085_P1.asset` |
| `BT17-085#4870@P0` | `BT17-085` | 4870 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_085_P0.asset` |
| `BT17-085#4871@P2` | `BT17-085` | 4871 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_085_P2.asset` |
| `BT17-085#4872@P3` | `BT17-085` | 4872 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_085_P3.asset` |
| `BT17-087#3651@base` | `BT17-087` | 3651 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_087.asset` |
| `BT17-087#3652@P1` | `BT17-087` | 3652 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_087_P1.asset` |
| `BT17-087#4874@P0` | `BT17-087` | 4874 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_087_P0.asset` |
| `BT17-087#8240@P2` | `BT17-087` | 8240 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Tamer/BT17_087_P2.asset` |
| `BT17-088#3653@base` | `BT17-088` | 3653 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Tamer/BT17_088.asset` |
| `BT17-088#3654@P1` | `BT17-088` | 3654 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Tamer/BT17_088_P1.asset` |
| `BT17-088#4875@P0` | `BT17-088` | 4875 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Tamer/BT17_088_P0.asset` |
| `BT17-089#3655@base` | `BT17-089` | 3655 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Tamer/BT17_089.asset` |
| `BT17-089#3656@P1` | `BT17-089` | 3656 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Tamer/BT17_089_P1.asset` |
| `BT17-089#4876@P0` | `BT17-089` | 4876 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Tamer/BT17_089_P0.asset` |
| `BT17-090#3657@base` | `BT17-090` | 3657 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Tamer/BT17_090.asset` |
| `BT17-090#4877@P0` | `BT17-090` | 4877 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Tamer/BT17_090_P0.asset` |
| `BT17-090#4878@P1` | `BT17-090` | 4878 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Tamer/BT17_090_P1.asset` |
| `BT17-091#3658@base` | `BT17-091` | 3658 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Tamer/BT17_091.asset` |
| `BT17-091#4879@P0` | `BT17-091` | 4879 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Tamer/BT17_091_P0.asset` |
| `BT17-091#4880@P1` | `BT17-091` | 4880 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Tamer/BT17_091_P1.asset` |
| `BT17-093#3661@base` | `BT17-093` | 3661 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Tamer/BT17_093.asset` |
| `BT17-093#3662@P1` | `BT17-093` | 3662 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Tamer/BT17_093_P1.asset` |
| `BT17-093#4882@P0` | `BT17-093` | 4882 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Tamer/BT17_093_P0.asset` |
| `BT17-094#3663@base` | `BT17-094` | 3663 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Option/BT17_094.asset` |
| `BT17-094#4883@P0` | `BT17-094` | 4883 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Option/BT17_094_P0.asset` |
| `BT17-094#4884@P1` | `BT17-094` | 4884 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Option/BT17_094_P1.asset` |
| `BT17-095#3664@base` | `BT17-095` | 3664 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Option/BT17_095.asset` |
| `BT17-095#4885@P0` | `BT17-095` | 4885 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Option/BT17_095_P0.asset` |
| `BT17-095#8241@P1` | `BT17-095` | 8241 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Option/BT17_095_P1.asset` |
| `BT17-095#8242@P2` | `BT17-095` | 8242 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Option/BT17_095_P2.asset` |
| `BT17-096#3665@base` | `BT17-096` | 3665 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Option/BT17_096.asset` |
| `BT17-096#4886@P0` | `BT17-096` | 4886 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Option/BT17_096_P0.asset` |

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
