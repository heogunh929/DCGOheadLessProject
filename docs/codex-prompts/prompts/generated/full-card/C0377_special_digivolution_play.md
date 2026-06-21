# C0377_special_digivolution_play - special digivolution/play mechanics card porting 142

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0377_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX6_034` | `DCGO/Assets/Scripts/CardEffect/EX6/Green/EX6_034.cs` | `None, OnAllyAttack, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `EX6_037` | `DCGO/Assets/Scripts/CardEffect/EX6/Black/EX6_037.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `EX6_038` | `DCGO/Assets/Scripts/CardEffect/EX6/Black/EX6_038.cs` | `None, OnAddDigivolutionCards, OnDeclaration` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX6_041` | `DCGO/Assets/Scripts/CardEffect/EX6/Black/EX6_041.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX6_043` | `DCGO/Assets/Scripts/CardEffect/EX6/Black/EX6_043.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `EX6_044` | `DCGO/Assets/Scripts/CardEffect/EX6/Black/EX6_044.cs` | `None, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX6_051` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_051.cs` | `OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `EX6_054` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_054.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 3 |
| `EX6_056` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_056.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `EX6_058` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_058.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX6-034#3484@base` | `EX6-034` | 3484 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Green/Digimon/EX6_034.asset` |
| `EX6-037#3489@base` | `EX6-037` | 3489 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/Digimon/EX6_037.asset` |
| `EX6-038#3490@base` | `EX6-038` | 3490 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/Digimon/EX6_038.asset` |
| `EX6-038#9159@P1` | `EX6-038` | 9159 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/Digimon/EX6_038_P1.asset` |
| `EX6-041#3493@base` | `EX6-041` | 3493 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/Digimon/EX6_041.asset` |
| `EX6-043#3495@base` | `EX6-043` | 3495 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/Digimon/EX6_043.asset` |
| `EX6-043#3496@P1` | `EX6-043` | 3496 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/Digimon/EX6_043_P1.asset` |
| `EX6-043#9160@P2` | `EX6-043` | 9160 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/Digimon/EX6_043_P2.asset` |
| `EX6-044#3497@base` | `EX6-044` | 3497 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Black/Digimon/EX6_044.asset` |
| `EX6-051#3504@base` | `EX6-051` | 3504 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_051.asset` |
| `EX6-054#3507@base` | `EX6-054` | 3507 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_054.asset` |
| `EX6-054#3508@P1` | `EX6-054` | 3508 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_054_P1.asset` |
| `EX6-054#9162@P2` | `EX6-054` | 9162 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_054_P2.asset` |
| `EX6-056#3510@base` | `EX6-056` | 3510 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_056.asset` |
| `EX6-056#3511@P1` | `EX6-056` | 3511 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_056_P1.asset` |
| `EX6-058#3514@base` | `EX6-058` | 3514 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_058.asset` |
| `EX6-058#3515@P1` | `EX6-058` | 3515 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_058_P1.asset` |

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
