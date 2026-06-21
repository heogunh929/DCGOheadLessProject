# C0245_special_digivolution_play - special digivolution/play mechanics card porting 10

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0245_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT11_074` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_074.cs` | `None, OnAllyAttack, OnUnTappedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `BT11_081` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_081.cs` | `None, OnAddHand, OnDestroyedAnyone, OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectJogress, SelectDigiXros` | 1 |
| `BT11_083` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_083.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 3 |
| `BT11_086` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_086.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectDigiXros` | 3 |
| `BT11_090` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_090.cs` | `OnAddHand, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT11_094` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_094.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `BT11_095` | `DCGO/Assets/Scripts/CardEffect/BT11/White/BT11_095.cs` | `BeforePayCost, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity, SelectDigiXros` | 4 |
| `BT11_096` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_096.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT11_097` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_097.cs` | `None, OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT11_098` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_098.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-074#2348@base` | `BT11-074` | 2348 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_074.asset` |
| `BT11-074#2349@P1` | `BT11-074` | 2349 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_074_P1.asset` |
| `BT11-081#2356@base` | `BT11-081` | 2356 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_081.asset` |
| `BT11-083#2358@base` | `BT11-083` | 2358 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_083.asset` |
| `BT11-083#2359@P1` | `BT11-083` | 2359 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_083_P1.asset` |
| `BT11-083#4429@P0` | `BT11-083` | 4429 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_083_P0.asset` |
| `BT11-086#2362@base` | `BT11-086` | 2362 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_086.asset` |
| `BT11-086#2363@P1` | `BT11-086` | 2363 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_086_P1.asset` |
| `BT11-086#4431@P2` | `BT11-086` | 4431 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_086_P2.asset` |
| `BT11-090#2369@base` | `BT11-090` | 2369 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_090.asset` |
| `BT11-090#2370@P1` | `BT11-090` | 2370 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_090_P1.asset` |
| `BT11-090#4434@P0` | `BT11-090` | 4434 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_090_P0.asset` |
| `BT11-094#2376@base` | `BT11-094` | 2376 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Tamer/BT11_094.asset` |
| `BT11-094#2377@P1` | `BT11-094` | 2377 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Tamer/BT11_094_P1.asset` |
| `BT11-095#2378@base` | `BT11-095` | 2378 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/White/Tamer/BT11_095.asset` |
| `BT11-095#4439@P0` | `BT11-095` | 4439 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/White/Tamer/BT11_095_P0.asset` |
| `BT11-095#8112@P1` | `BT11-095` | 8112 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/White/Tamer/BT11_095_P1.asset` |
| `BT11-095#8113@P2` | `BT11-095` | 8113 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/White/Tamer/BT11_095_P2.asset` |
| `BT11-096#2379@base` | `BT11-096` | 2379 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Option/BT11_096.asset` |
| `BT11-097#2380@base` | `BT11-097` | 2380 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Option/BT11_097.asset` |
| `BT11-098#2381@base` | `BT11-098` | 2381 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Option/BT11_098.asset` |

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
