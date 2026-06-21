# C0352_special_digivolution_play - special digivolution/play mechanics card porting 117

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0352_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX10_056` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_056.cs` | `None, OnAddDigivolutionCards, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectDigiXros` | 1 |
| `EX10_057` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_057.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEndTurn, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `EX10_058` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_058.cs` | `None, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectDigiXros` | 1 |
| `EX10_059` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_059.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress, SelectDigiXros` | 2 |
| `EX10_060` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_060.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX10_061` | `DCGO/Assets/Scripts/CardEffect/EX10/White/EX10_061.cs` | `BeforePayCost, None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress, SelectDigiXros` | 2 |
| `EX10_062` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_062.cs` | `OnEndTurn, OnLinkCardDiscarded, OnStartMainPhase, SecuritySkill` | `linked, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectAppFusion` | 2 |
| `EX10_063` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_063.cs` | `OnDigivolutionCardDiscarded, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `EX10_064` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_064.cs` | `BeforePayCost, None, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress, SelectDigiXros` | 2 |
| `EX10_065` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_065.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX10-056#7233@base` | `EX10-056` | 7233 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_056.asset` |
| `EX10-057#7234@base` | `EX10-057` | 7234 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_057.asset` |
| `EX10-057#7315@P1` | `EX10-057` | 7315 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_057_P1.asset` |
| `EX10-058#7236@base` | `EX10-058` | 7236 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_058.asset` |
| `EX10-059#7237@base` | `EX10-059` | 7237 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_059.asset` |
| `EX10-059#7316@P1` | `EX10-059` | 7316 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_059_P1.asset` |
| `EX10-060#7239@base` | `EX10-060` | 7239 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_060.asset` |
| `EX10-060#7317@P1` | `EX10-060` | 7317 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_060_P1.asset` |
| `EX10-061#7241@base` | `EX10-061` | 7241 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/White/Digimon/EX10_061.asset` |
| `EX10-061#7318@P1` | `EX10-061` | 7318 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/White/Digimon/EX10_061_P1.asset` |
| `EX10-062#7243@base` | `EX10-062` | 7243 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Tamer/EX10_062.asset` |
| `EX10-062#7319@P1` | `EX10-062` | 7319 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Tamer/EX10_062_P1.asset` |
| `EX10-063#7245@base` | `EX10-063` | 7245 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Tamer/EX10_063.asset` |
| `EX10-063#7320@P1` | `EX10-063` | 7320 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Tamer/EX10_063_P1.asset` |
| `EX10-064#7247@base` | `EX10-064` | 7247 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Tamer/EX10_064.asset` |
| `EX10-064#7321@P1` | `EX10-064` | 7321 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Tamer/EX10_064_P1.asset` |
| `EX10-065#7249@base` | `EX10-065` | 7249 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Tamer/EX10_065.asset` |
| `EX10-065#7322@P1` | `EX10-065` | 7322 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Tamer/EX10_065_P1.asset` |

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
