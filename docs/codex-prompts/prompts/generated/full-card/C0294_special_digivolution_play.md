# C0294_special_digivolution_play - special digivolution/play mechanics card porting 59

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0294_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT20_085` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_085.cs` | `OnEndTurn, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT20_087` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_087.cs` | `None, OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT20_091` | `DCGO/Assets/Scripts/CardEffect/BT20/White/BT20_091.cs` | `OnEnterFieldAnyone, SecuritySkill, WhenRemoveField` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 3 |
| `BT20_093` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_093.cs` | `None, OptionSkill, SecuritySkill, WhenRemoveField` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT20_094` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_094.cs` | `None, OnLoseSecurity, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT20_095` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_095.cs` | `OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT20_097` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_097.cs` | `OptionSkill, SecuritySkill, WhenRemoveField` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT20_098` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_098.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `BT20_099` | `DCGO/Assets/Scripts/CardEffect/BT20/White/BT20_099.cs` | `None, OnEndTurn, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT20_102` | `DCGO/Assets/Scripts/CardEffect/BT20/Blue/BT20_102.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT20-085#5164@base` | `BT20-085` | 5164 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Tamer/BT20_085.asset` |
| `BT20-085#5242@P1` | `BT20-085` | 5242 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Tamer/BT20_085_P1.asset` |
| `BT20-087#5166@base` | `BT20-087` | 5166 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Tamer/BT20_087.asset` |
| `BT20-087#5246@P1` | `BT20-087` | 5246 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Tamer/BT20_087_P1.asset` |
| `BT20-091#5170@base` | `BT20-091` | 5170 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Tamer/BT20_091.asset` |
| `BT20-091#5254@P1` | `BT20-091` | 5254 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Tamer/BT20_091_P1.asset` |
| `BT20-091#8363@P2` | `BT20-091` | 8363 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Tamer/BT20_091_P2.asset` |
| `BT20-093#5172@base` | `BT20-093` | 5172 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Option/BT20_093.asset` |
| `BT20-094#5173@base` | `BT20-094` | 5173 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Option/BT20_094.asset` |
| `BT20-095#5174@base` | `BT20-095` | 5174 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Option/BT20_095.asset` |
| `BT20-097#5176@base` | `BT20-097` | 5176 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Option/BT20_097.asset` |
| `BT20-098#5177@base` | `BT20-098` | 5177 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Option/BT20_098.asset` |
| `BT20-099#5178@base` | `BT20-099` | 5178 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Option/BT20_099.asset` |
| `BT20-102#5181@base` | `BT20-102` | 5181 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_102.asset` |
| `BT20-102#5261@P1` | `BT20-102` | 5261 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_102_P1.asset` |
| `BT20-102#5262@P2` | `BT20-102` | 5262 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_102_P2.asset` |
| `BT20-102#8367@P3` | `BT20-102` | 8367 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_102_P3.asset` |
| `BT20-102#8368@P4` | `BT20-102` | 8368 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_102_P4.asset` |

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
