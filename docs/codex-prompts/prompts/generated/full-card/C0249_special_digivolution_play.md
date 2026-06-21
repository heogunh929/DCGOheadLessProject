# C0249_special_digivolution_play - special digivolution/play mechanics card porting 14

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0249_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 35
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT12_034` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_034.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 4 |
| `BT12_037` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_037.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectJogress, SelectDigiXros` | 2 |
| `BT12_038` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_038.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 4 |
| `BT12_042` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_042.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `BT12_043` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_043.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 7 |
| `BT12_047` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_047.cs` | `OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 5 |
| `BT12_048` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_048.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectDigiXros` | 2 |
| `BT12_050` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_050.cs` | `BeforePayCost, OnDetermineDoSecurityCheck` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 5 |
| `BT12_051` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_051.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress, SelectDigiXros` | 2 |
| `BT12_054` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_054.cs` | `OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT12-034#2444@base` | `BT12-034` | 2444 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_034.asset` |
| `BT12-034#4492@P1` | `BT12-034` | 4492 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_034_P1.asset` |
| `BT12-034#4493@P2` | `BT12-034` | 4493 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_034_P2.asset` |
| `BT12-034#8123@P3` | `BT12-034` | 8123 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_034_P3.asset` |
| `BT12-037#2447@base` | `BT12-037` | 2447 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_037.asset` |
| `BT12-037#4495@P0` | `BT12-037` | 4495 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_037_P0.asset` |
| `BT12-038#2448@base` | `BT12-038` | 2448 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_038.asset` |
| `BT12-038#4496@P1` | `BT12-038` | 4496 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_038_P1.asset` |
| `BT12-038#8124@P2` | `BT12-038` | 8124 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_038_P2.asset` |
| `BT12-038#8125@P3` | `BT12-038` | 8125 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_038_P3.asset` |
| `BT12-042#2453@base` | `BT12-042` | 2453 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_042.asset` |
| `BT12-042#4497@P0` | `BT12-042` | 4497 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_042_P0.asset` |
| `BT12-042#4498@P1` | `BT12-042` | 4498 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_042_P1.asset` |
| `BT12-043#2454@base` | `BT12-043` | 2454 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_043.asset` |
| `BT12-043#2455@P1` | `BT12-043` | 2455 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_043_P1.asset` |
| `BT12-043#4499@P2` | `BT12-043` | 4499 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_043_P2.asset` |
| `BT12-043#4500@P3` | `BT12-043` | 4500 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_043_P3.asset` |
| `BT12-043#4501@P4` | `BT12-043` | 4501 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_043_P4.asset` |
| `BT12-043#4502@P5` | `BT12-043` | 4502 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_043_P5.asset` |
| `BT12-043#8126@P6` | `BT12-043` | 8126 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_043_P6.asset` |
| `BT12-047#2459@base` | `BT12-047` | 2459 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_047.asset` |
| `BT12-047#4504@P0` | `BT12-047` | 4504 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_047_P0.asset` |
| `BT12-047#4505@P1` | `BT12-047` | 4505 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_047_P1.asset` |
| `BT12-047#8127@P2` | `BT12-047` | 8127 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_047_P2.asset` |
| `BT12-047#8128@P3` | `BT12-047` | 8128 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_047_P3.asset` |
| `BT12-048#2460@base` | `BT12-048` | 2460 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_048.asset` |
| `BT12-048#4506@P0` | `BT12-048` | 4506 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_048_P0.asset` |
| `BT12-050#2462@base` | `BT12-050` | 2462 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_050.asset` |
| `BT12-050#4507@P0` | `BT12-050` | 4507 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_050_P0.asset` |
| `BT12-050#4508@P1` | `BT12-050` | 4508 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_050_P1.asset` |
| `BT12-050#8129@P2` | `BT12-050` | 8129 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_050_P2.asset` |
| `BT12-050#8130@P3` | `BT12-050` | 8130 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_050_P3.asset` |
| `BT12-051#2463@base` | `BT12-051` | 2463 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_051.asset` |
| `BT12-051#2464@P1` | `BT12-051` | 2464 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_051_P1.asset` |
| `BT12-054#2467@base` | `BT12-054` | 2467 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_054.asset` |

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
