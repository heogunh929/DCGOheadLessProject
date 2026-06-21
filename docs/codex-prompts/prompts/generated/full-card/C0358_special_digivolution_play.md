# C0358_special_digivolution_play - special digivolution/play mechanics card porting 123

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0358_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX1_051` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_051.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `EX1_052` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_052.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `EX1_053` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_053.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX1_056` | `DCGO/Assets/Scripts/CardEffect/EX1/Purple/EX1_056.cs` | `None, OnDestroyedAnyone` | `inherited, modifier_duration, static_or_continuous` | `SelectJogress` | 1 |
| `EX1_061` | `DCGO/Assets/Scripts/CardEffect/EX1/Purple/EX1_061.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectCard, SelectAttackTarget, SelectJogress` | 2 |
| `EX1_062` | `DCGO/Assets/Scripts/CardEffect/EX1/Purple/EX1_062.cs` | `None, OnDestroyedAnyone, OnEndAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX1_067` | `DCGO/Assets/Scripts/CardEffect/EX1/Red/EX1_067.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX1_068` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_068.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous` | `SelectJogress` | 1 |
| `EX1_069` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_069.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity, SelectJogress` | 1 |
| `EX1_070` | `DCGO/Assets/Scripts/CardEffect/EX1/Purple/EX1_070.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX1-051#1349@base` | `EX1-051` | 1349 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_051.asset` |
| `EX1-052#1350@base` | `EX1-052` | 1350 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_052.asset` |
| `EX1-052#9092@P1` | `EX1-052` | 9092 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_052_P1.asset` |
| `EX1-053#1351@base` | `EX1-053` | 1351 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_053.asset` |
| `EX1-053#6819@P1` | `EX1-053` | 6819 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_053_P1.asset` |
| `EX1-056#1354@base` | `EX1-056` | 1354 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_056.asset` |
| `EX1-061#1360@base` | `EX1-061` | 1360 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_061.asset` |
| `EX1-061#9094@P1` | `EX1-061` | 9094 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_061_P1.asset` |
| `EX1-062#1361@base` | `EX1-062` | 1361 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_062.asset` |
| `EX1-067#1369@base` | `EX1-067` | 1369 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Option/EX1_067.asset` |
| `EX1-068#1370@base` | `EX1-068` | 1370 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Option/EX1_068.asset` |
| `EX1-069#1371@base` | `EX1-069` | 1371 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Option/EX1_069.asset` |
| `EX1-070#1372@base` | `EX1-070` | 1372 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Option/EX1_070.asset` |

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
