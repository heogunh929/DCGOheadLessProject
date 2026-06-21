# C0282_special_digivolution_play - special digivolution/play mechanics card porting 47

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0282_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT18_100` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_100.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT18_101` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_101.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT19_003` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_003.cs` | `OnEndTurn` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT19_007` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_007.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT19_008` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_008.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT19_009` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_009.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT19_010` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_010.cs` | `None, WhenRemoveField` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 2 |
| `BT19_012` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_012.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress, SelectDigiXros` | 1 |
| `BT19_013` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_013.cs` | `None, OnDestroyedAnyone, WhenRemoveField` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 1 |
| `BT19_019` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_019.cs` | `OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT18-100#3973@base` | `BT18-100` | 3973 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Option/BT18_100.asset` |
| `BT18-100#8270@P1` | `BT18-100` | 8270 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Option/BT18_100_P1.asset` |
| `BT18-101#3970@base` | `BT18-101` | 3970 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_101.asset` |
| `BT18-101#3971@P1` | `BT18-101` | 3971 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_101_P1.asset` |
| `BT18-101#3972@P2` | `BT18-101` | 3972 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_101_P2.asset` |
| `BT19-003#5006@base` | `BT19-003` | 5006 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/DigiEgg/BT19_003.asset` |
| `BT19-007#5008@base` | `BT19-007` | 5008 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_007.asset` |
| `BT19-008#5009@base` | `BT19-008` | 5009 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_008.asset` |
| `BT19-008#8273@P1` | `BT19-008` | 8273 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_008_P1.asset` |
| `BT19-009#5010@base` | `BT19-009` | 5010 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_009.asset` |
| `BT19-009#8274@P1` | `BT19-009` | 8274 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_009_P1.asset` |
| `BT19-010#5011@base` | `BT19-010` | 5011 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_010.asset` |
| `BT19-010#8275@P1` | `BT19-010` | 8275 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_010_P1.asset` |
| `BT19-012#5015@base` | `BT19-012` | 5015 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_012.asset` |
| `BT19-013#5016@base` | `BT19-013` | 5016 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_013.asset` |
| `BT19-019#3981@base` | `BT19-019` | 3981 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_019.asset` |
| `BT19-019#8276@P1` | `BT19-019` | 8276 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_019_P1.asset` |

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
