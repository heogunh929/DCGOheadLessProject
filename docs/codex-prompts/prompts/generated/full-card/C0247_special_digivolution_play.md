# C0247_special_digivolution_play - special digivolution/play mechanics card porting 12

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0247_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 31
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT11_109` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_109.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT11_110` | `DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_110.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT11_112` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_112.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone, OnUnTappedAnyone, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT12_007` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_007.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT12_010` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_010.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT12_011` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_011.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress, SelectDigiXros` | 5 |
| `BT12_012` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_012.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 4 |
| `BT12_013` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_013.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 3 |
| `BT12_015` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_015.cs` | `OnDeclaration, OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 4 |
| `BT12_016` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_016.cs` | `OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-109#2392@base` | `BT11-109` | 2392 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Option/BT11_109.asset` |
| `BT11-109#4446@P0` | `BT11-109` | 4446 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Option/BT11_109_P0.asset` |
| `BT11-110#2393@base` | `BT11-110` | 2393 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Purple/Option/BT11_110.asset` |
| `BT11-112#2396@base` | `BT11-112` | 2396 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_112.asset` |
| `BT11-112#2397@P1` | `BT11-112` | 2397 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_112_P1.asset` |
| `BT11-112#8114@P2` | `BT11-112` | 8114 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Tamer/BT11_112_P2.asset` |
| `BT12-007#2405@base` | `BT12-007` | 2405 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_007.asset` |
| `BT12-007#2406@P1` | `BT12-007` | 2406 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_007_P1.asset` |
| `BT12-010#2409@base` | `BT12-010` | 2409 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_010.asset` |
| `BT12-010#2410@P1` | `BT12-010` | 2410 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_010_P1.asset` |
| `BT12-011#2411@base` | `BT12-011` | 2411 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_011.asset` |
| `BT12-011#4457@P0` | `BT12-011` | 4457 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_011_P0.asset` |
| `BT12-011#4458@P1` | `BT12-011` | 4458 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_011_P1.asset` |
| `BT12-011#4459@P2` | `BT12-011` | 4459 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_011_P2.asset` |
| `BT12-011#4460@P3` | `BT12-011` | 4460 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_011_P3.asset` |
| `BT12-012#2415@base` | `BT12-012` | 2415 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_012.asset` |
| `BT12-012#4461@P0` | `BT12-012` | 4461 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_012_P0.asset` |
| `BT12-012#4462@P1` | `BT12-012` | 4462 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_012_P1.asset` |
| `BT12-012#4463@P2` | `BT12-012` | 4463 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_012_P2.asset` |
| `BT12-013#2416@base` | `BT12-013` | 2416 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_013.asset` |
| `BT12-013#4464@P0` | `BT12-013` | 4464 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_013_P0.asset` |
| `BT12-013#4465@P1` | `BT12-013` | 4465 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_013_P1.asset` |
| `BT12-015#2419@base` | `BT12-015` | 2419 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_015.asset` |
| `BT12-015#4466@P0` | `BT12-015` | 4466 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_015_P0.asset` |
| `BT12-015#4467@P1` | `BT12-015` | 4467 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_015_P1.asset` |
| `BT12-015#4468@P2` | `BT12-015` | 4468 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_015_P2.asset` |
| `BT12-016#2420@base` | `BT12-016` | 2420 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_016.asset` |
| `BT12-016#2421@P1` | `BT12-016` | 2421 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_016_P1.asset` |
| `BT12-016#4469@P0` | `BT12-016` | 4469 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_016_P0.asset` |
| `BT12-016#4470@P2` | `BT12-016` | 4470 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Digimon/BT12_016_P2.asset` |
| `BT12-016#8117@P2` | `BT12-016` | 8117 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_009_P2.asset` |

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
