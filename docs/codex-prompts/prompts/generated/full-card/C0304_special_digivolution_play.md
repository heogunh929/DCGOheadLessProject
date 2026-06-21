# C0304_special_digivolution_play - special digivolution/play mechanics card porting 69

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0304_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT22_084` | `DCGO/Assets/Scripts/CardEffect/BT22/Red/BT22_084.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase, OnStartTurn, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 3 |
| `BT22_085` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_085.cs` | `OnAllyAttack, OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT22_086` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_086.cs` | `OnAddDigivolutionCards, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT22_087` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_087.cs` | `OnStartMainPhase, SecuritySkill, WhenLinked` | `linked, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectAppFusion` | 1 |
| `BT22_088` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_088.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `BT22_089` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_089.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `BT22_090` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_090.cs` | `OnEndTurn, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT22_091` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_091.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT22_096` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_096.cs` | `OnTappedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT22_098` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_098.cs` | `OnTappedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT22-084#7096@base` | `BT22-084` | 7096 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Tamer/BT22_084.asset` |
| `BT22-084#7097@P1` | `BT22-084` | 7097 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Tamer/BT22_084_P1.asset` |
| `BT22-084#7098@P2` | `BT22-084` | 7098 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Tamer/BT22_084_P2.asset` |
| `BT22-085#7099@base` | `BT22-085` | 7099 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Tamer/BT22_085.asset` |
| `BT22-085#8445@P1` | `BT22-085` | 8445 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Tamer/BT22_085_P1.asset` |
| `BT22-086#7100@base` | `BT22-086` | 7100 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Tamer/BT22_086.asset` |
| `BT22-087#7101@base` | `BT22-087` | 7101 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Tamer/BT22_087.asset` |
| `BT22-088#7102@base` | `BT22-088` | 7102 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Tamer/BT22_088.asset` |
| `BT22-089#7103@base` | `BT22-089` | 7103 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Tamer/BT22_089.asset` |
| `BT22-089#7104@P1` | `BT22-089` | 7104 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Tamer/BT22_089_P1.asset` |
| `BT22-090#7105@base` | `BT22-090` | 7105 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Tamer/BT22_090.asset` |
| `BT22-090#7106@P1` | `BT22-090` | 7106 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Tamer/BT22_090_P1.asset` |
| `BT22-091#7107@base` | `BT22-091` | 7107 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Tamer/BT22_091.asset` |
| `BT22-091#7108@P1` | `BT22-091` | 7108 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Tamer/BT22_091_P1.asset` |
| `BT22-091#7109@P2` | `BT22-091` | 7109 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Tamer/BT22_091_P2.asset` |
| `BT22-096#7120@base` | `BT22-096` | 7120 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Option/BT22_096.asset` |
| `BT22-098#7122@base` | `BT22-098` | 7122 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Option/BT22_098.asset` |

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
