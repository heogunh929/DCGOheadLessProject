# C0381_special_digivolution_play - special digivolution/play mechanics card porting 146

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0381_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX8_001` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX8_007` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_007.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `EX8_009` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_009.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `EX8_012` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_012.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 3 |
| `EX8_015` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_015.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `EX8_016` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_016.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `EX8_019` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_019.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX8_025` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_025.cs` | `None, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectAttackTarget, SelectJogress` | 2 |
| `EX8_027` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_027.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectAttackTarget, SelectJogress` | 1 |
| `EX8_029` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_029.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX8-001#4049@base` | `EX8-001` | 4049 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/DigiEgg/EX8_001.asset` |
| `EX8-001#4050@P1` | `EX8-001` | 4050 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/DigiEgg/EX8_001_P1.asset` |
| `EX8-007#4061@base` | `EX8-007` | 4061 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_007.asset` |
| `EX8-007#4062@P1` | `EX8-007` | 4062 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_007_P1.asset` |
| `EX8-009#4065@base` | `EX8-009` | 4065 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_009.asset` |
| `EX8-009#4066@P1` | `EX8-009` | 4066 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_009_P1.asset` |
| `EX8-012#4070@base` | `EX8-012` | 4070 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_012.asset` |
| `EX8-012#4071@P1` | `EX8-012` | 4071 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_012_P1.asset` |
| `EX8-012#9188@P2` | `EX8-012` | 9188 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_012_P2.asset` |
| `EX8-015#4076@base` | `EX8-015` | 4076 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_015.asset` |
| `EX8-015#4077@P1` | `EX8-015` | 4077 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_015_P1.asset` |
| `EX8-015#9189@P2` | `EX8-015` | 9189 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_015_P2.asset` |
| `EX8-016#4078@base` | `EX8-016` | 4078 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_016.asset` |
| `EX8-016#4079@P1` | `EX8-016` | 4079 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_016_P1.asset` |
| `EX8-016#9190@P2` | `EX8-016` | 9190 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_016_P2.asset` |
| `EX8-019#4083@base` | `EX8-019` | 4083 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_019.asset` |
| `EX8-019#4084@P1` | `EX8-019` | 4084 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_019_P1.asset` |
| `EX8-025#4093@base` | `EX8-025` | 4093 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_025.asset` |
| `EX8-025#4094@P1` | `EX8-025` | 4094 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_025_P1.asset` |
| `EX8-027#4100@base` | `EX8-027` | 4100 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_027.asset` |
| `EX8-029#4103@base` | `EX8-029` | 4103 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_029.asset` |
| `EX8-029#4104@P1` | `EX8-029` | 4104 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_029_P1.asset` |

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
