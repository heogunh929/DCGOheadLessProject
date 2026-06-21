# C0286_special_digivolution_play - special digivolution/play mechanics card porting 51

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0286_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT19_091` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_091.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `BT19_093` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_093.cs` | `None, OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT19_094` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_094.cs` | `OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT19_095` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_095.cs` | `None, OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT19_097` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_097.cs` | `OnDiscardLibrary, OnStartTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `BT19_098` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_098.cs` | `None, OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `BT19_099` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_099.cs` | `None, OptionSkill, SecuritySkill, WhenRemoveField` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT19_100` | `DCGO/Assets/Scripts/CardEffect/BT19/White/BT19_100.cs` | `OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT19_101` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_101.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `BT19_102` | `DCGO/Assets/Scripts/CardEffect/BT19/White/BT19_102.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT19-091#5071@base` | `BT19-091` | 5071 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Option/BT19_091.asset` |
| `BT19-093#5072@base` | `BT19-093` | 5072 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Option/BT19_093.asset` |
| `BT19-094#4026@base` | `BT19-094` | 4026 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Option/BT19_094.asset` |
| `BT19-095#5073@base` | `BT19-095` | 5073 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Option/BT19_095.asset` |
| `BT19-097#5074@base` | `BT19-097` | 5074 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Option/BT19_097.asset` |
| `BT19-098#5075@base` | `BT19-098` | 5075 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Option/BT19_098.asset` |
| `BT19-099#4025@base` | `BT19-099` | 4025 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Option/BT19_099.asset` |
| `BT19-100#5076@base` | `BT19-100` | 5076 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/White/Option/BT19_100.asset` |
| `BT19-101#4027@base` | `BT19-101` | 4027 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_101.asset` |
| `BT19-101#4028@P1` | `BT19-101` | 4028 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_101_P1.asset` |
| `BT19-101#4029@P2` | `BT19-101` | 4029 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_101_P2.asset` |
| `BT19-102#5077@base` | `BT19-102` | 5077 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/White/Digimon/BT19_102.asset` |
| `BT19-102#5078@P1` | `BT19-102` | 5078 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/White/Digimon/BT19_102_P1.asset` |

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
