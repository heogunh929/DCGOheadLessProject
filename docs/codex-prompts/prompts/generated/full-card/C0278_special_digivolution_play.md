# C0278_special_digivolution_play - special digivolution/play mechanics card porting 43

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0278_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT18_017` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_017.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectBoolean, SelectJogress, SelectDigiXros` | 2 |
| `BT18_018` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_018.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `BT18_019` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_019.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectJogress, SelectDigiXros` | 2 |
| `BT18_022` | `DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_022.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT18_023` | `DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_023.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `BT18_024` | `DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_024.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT18_025` | `DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_025.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT18_026` | `DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_026.cs` | `None, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT18_028` | `DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_028.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress, SelectDigiXros` | 2 |
| `BT18_029` | `DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_029.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress, SelectDigiXros` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT18-017#3858@base` | `BT18-017` | 3858 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_017.asset` |
| `BT18-017#8252@P1` | `BT18-017` | 8252 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_017_P1.asset` |
| `BT18-018#3861@base` | `BT18-018` | 3861 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_018.asset` |
| `BT18-018#3862@P1` | `BT18-018` | 3862 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_018_P1.asset` |
| `BT18-019#3866@base` | `BT18-019` | 3866 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_019.asset` |
| `BT18-019#3867@P1` | `BT18-019` | 3867 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_019_P1.asset` |
| `BT18-022#3875@base` | `BT18-022` | 3875 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_022.asset` |
| `BT18-022#3876@P1` | `BT18-022` | 3876 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_022_P1.asset` |
| `BT18-023#3877@base` | `BT18-023` | 3877 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_023.asset` |
| `BT18-023#8253@P1` | `BT18-023` | 8253 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_023_P1.asset` |
| `BT18-024#3871@base` | `BT18-024` | 3871 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_024.asset` |
| `BT18-025#3873@base` | `BT18-025` | 3873 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_025.asset` |
| `BT18-025#3874@P1` | `BT18-025` | 3874 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_025_P1.asset` |
| `BT18-026#3872@base` | `BT18-026` | 3872 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_026.asset` |
| `BT18-028#3869@base` | `BT18-028` | 3869 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_028.asset` |
| `BT18-028#8254@P1` | `BT18-028` | 8254 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_028_P1.asset` |
| `BT18-029#3870@base` | `BT18-029` | 3870 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_029.asset` |
| `BT18-029#8255@P1` | `BT18-029` | 8255 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_029_P1.asset` |

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
