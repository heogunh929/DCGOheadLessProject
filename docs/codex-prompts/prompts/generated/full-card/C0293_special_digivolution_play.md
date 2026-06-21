# C0293_special_digivolution_play - special digivolution/play mechanics card porting 58

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0293_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT20_061` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_061.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT20_064` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_064.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT20_066` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_066.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT20_068` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_068.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT20_070` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_070.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT20_071` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_071.cs` | `None, OnAddDigivolutionCards, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT20_073` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_073.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT20_074` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_074.cs` | `None, OnEnterFieldAnyone, WhenReturntoHandAnyone, WhenReturntoLibraryAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT20_083` | `DCGO/Assets/Scripts/CardEffect/BT20/White/BT20_083.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 5 |
| `BT20_084` | `DCGO/Assets/Scripts/CardEffect/BT20/White/BT20_084.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT20-061#5140@base` | `BT20-061` | 5140 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_061.asset` |
| `BT20-064#5143@base` | `BT20-064` | 5143 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_064.asset` |
| `BT20-066#5145@base` | `BT20-066` | 5145 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_066.asset` |
| `BT20-068#5147@base` | `BT20-068` | 5147 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_068.asset` |
| `BT20-068#8347@P1` | `BT20-068` | 8347 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_068_P1.asset` |
| `BT20-070#5149@base` | `BT20-070` | 5149 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_070.asset` |
| `BT20-071#5150@base` | `BT20-071` | 5150 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_071.asset` |
| `BT20-073#5152@base` | `BT20-073` | 5152 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_073.asset` |
| `BT20-074#5153@base` | `BT20-074` | 5153 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_074.asset` |
| `BT20-074#8352@P1` | `BT20-074` | 8352 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_074_P1.asset` |
| `BT20-083#5162@base` | `BT20-083` | 5162 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Digimon/BT20_083.asset` |
| `BT20-083#8357@P1` | `BT20-083` | 8357 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Digimon/BT20_083_P1.asset` |
| `BT20-083#8358@P2` | `BT20-083` | 8358 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Digimon/BT20_083_P2.asset` |
| `BT20-083#8359@P3` | `BT20-083` | 8359 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Digimon/BT20_083_P3.asset` |
| `BT20-083#8360@P4` | `BT20-083` | 8360 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Digimon/BT20_083_P4.asset` |
| `BT20-084#5163@base` | `BT20-084` | 5163 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Digimon/BT20_084.asset` |

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
