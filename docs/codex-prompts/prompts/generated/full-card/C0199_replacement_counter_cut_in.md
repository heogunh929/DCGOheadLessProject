# C0199_replacement_counter_cut_in - replacement/counter/cut-in card porting 14

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0199_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT19_048` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_048.cs` | `None, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |
| `BT19_050` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_050.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `BT19_057` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_057.cs` | `None, OnAllyAttack, OnCounterTiming, OnDestroyedAnyone` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT19_061` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_061.cs` | `None, OnCounterTiming, OnDestroyedAnyone, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress, SelectDigiXros` | 1 |
| `BT19_062` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_062.cs` | `None, OnAllyAttack, OnCounterTiming, OnEndTurn` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `BT19_063` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_063.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 1 |
| `BT19_064` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_064.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT19_073` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_073.cs` | `None, OnAllyAttack, OnCounterTiming, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT19_074` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_074.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `BT19_075` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_075.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT19-048#3994@base` | `BT19-048` | 3994 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_048.asset` |
| `BT19-048#8281@P1` | `BT19-048` | 8281 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_048_P1.asset` |
| `BT19-050#5036@base` | `BT19-050` | 5036 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_050.asset` |
| `BT19-050#5037@P1` | `BT19-050` | 5037 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_050_P1.asset` |
| `BT19-050#5038@P2` | `BT19-050` | 5038 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_050_P2.asset` |
| `BT19-057#5042@base` | `BT19-057` | 5042 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_057.asset` |
| `BT19-061#5044@base` | `BT19-061` | 5044 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_061.asset` |
| `BT19-062#5045@base` | `BT19-062` | 5045 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_062.asset` |
| `BT19-063#4007@base` | `BT19-063` | 4007 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_063.asset` |
| `BT19-064#5046@base` | `BT19-064` | 5046 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_064.asset` |
| `BT19-064#5047@P1` | `BT19-064` | 5047 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_064_P1.asset` |
| `BT19-064#5048@P2` | `BT19-064` | 5048 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_064_P2.asset` |
| `BT19-073#4013@base` | `BT19-073` | 4013 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_073.asset` |
| `BT19-073#4014@P1` | `BT19-073` | 4014 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_073_P1.asset` |
| `BT19-074#5052@base` | `BT19-074` | 5052 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_074.asset` |
| `BT19-074#5053@P1` | `BT19-074` | 5053 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_074_P1.asset` |
| `BT19-074#5054@P2` | `BT19-074` | 5054 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_074_P2.asset` |
| `BT19-074#8290@P3` | `BT19-074` | 8290 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_074_P3.asset` |
| `BT19-075#4015@base` | `BT19-075` | 4015 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_075.asset` |
| `BT19-075#4016@P1` | `BT19-075` | 4016 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_075_P1.asset` |

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
