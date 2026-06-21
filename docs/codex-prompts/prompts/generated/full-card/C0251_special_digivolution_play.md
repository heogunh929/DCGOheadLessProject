# C0251_special_digivolution_play - special digivolution/play mechanics card porting 16

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0251_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 31
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT12_074` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_074.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent, SelectDigiXros` | 3 |
| `BT12_075` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_075.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectDigiXros` | 2 |
| `BT12_078` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_078.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 1 |
| `BT12_081` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_081.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectInteger, SelectJogress` | 3 |
| `BT12_082` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_082.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT12_085` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_085.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 4 |
| `BT12_089` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_089.cs` | `OnDeclaration, OnStartTurn, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectSecurity, SelectJogress` | 4 |
| `BT12_090` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_090.cs` | `OnAllyAttack, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 3 |
| `BT12_092` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_092.cs` | `OnStartMainPhase, OnTappedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `BT12_095` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_095.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT12-074#2490@base` | `BT12-074` | 2490 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_074.asset` |
| `BT12-074#4525@P0` | `BT12-074` | 4525 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_074_P0.asset` |
| `BT12-074#4526@P1` | `BT12-074` | 4526 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_074_P1.asset` |
| `BT12-075#2491@base` | `BT12-075` | 2491 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_075.asset` |
| `BT12-075#4527@P0` | `BT12-075` | 4527 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_075_P0.asset` |
| `BT12-078#2495@base` | `BT12-078` | 2495 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_078.asset` |
| `BT12-081#2498@base` | `BT12-081` | 2498 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_081.asset` |
| `BT12-081#2499@P1` | `BT12-081` | 2499 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_081_P1.asset` |
| `BT12-081#4529@P0` | `BT12-081` | 4529 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_081_P0.asset` |
| `BT12-082#2500@base` | `BT12-082` | 2500 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_082.asset` |
| `BT12-082#4530@P0` | `BT12-082` | 4530 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_082_P0.asset` |
| `BT12-085#2504@base` | `BT12-085` | 2504 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_085.asset` |
| `BT12-085#2505@P1` | `BT12-085` | 2505 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_085_P1.asset` |
| `BT12-085#4532@P2` | `BT12-085` | 4532 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_085_P2.asset` |
| `BT12-085#8131@P3` | `BT12-085` | 8131 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_085_P3.asset` |
| `BT12-089#2511@base` | `BT12-089` | 2511 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_089.asset` |
| `BT12-089#2512@P1` | `BT12-089` | 2512 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_089_P1.asset` |
| `BT12-089#4536@P0` | `BT12-089` | 4536 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_089_P0.asset` |
| `BT12-089#8132@P2` | `BT12-089` | 8132 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Tamer/BT12_089_P2.asset` |
| `BT12-090#2513@base` | `BT12-090` | 2513 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Tamer/BT12_090.asset` |
| `BT12-090#2514@P1` | `BT12-090` | 2514 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Tamer/BT12_090_P1.asset` |
| `BT12-090#4537@P0` | `BT12-090` | 4537 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Tamer/BT12_090_P0.asset` |
| `BT12-092#2516@base` | `BT12-092` | 2516 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Tamer/BT12_092.asset` |
| `BT12-092#2517@P1` | `BT12-092` | 2517 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Tamer/BT12_092_P1.asset` |
| `BT12-092#4539@P0` | `BT12-092` | 4539 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Tamer/BT12_092_P0.asset` |
| `BT12-092#8133@P2` | `BT12-092` | 8133 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Tamer/BT12_092_P2.asset` |
| `BT12-092#8134@P3` | `BT12-092` | 8134 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Tamer/BT12_092_P3.asset` |
| `BT12-095#2520@base` | `BT12-095` | 2520 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Tamer/BT12_095.asset` |
| `BT12-095#2521@P1` | `BT12-095` | 2521 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Tamer/BT12_095_P1.asset` |
| `BT12-095#4542@P0` | `BT12-095` | 4542 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Tamer/BT12_095_P0.asset` |
| `BT12-095#8135@P2` | `BT12-095` | 8135 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Tamer/BT12_095_P2.asset` |

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
