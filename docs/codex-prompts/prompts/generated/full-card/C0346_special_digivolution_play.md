# C0346_special_digivolution_play - special digivolution/play mechanics card porting 111

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0346_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 26
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT9_064` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_064.cs` | `OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `BT9_067` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_067.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectJogress` | 2 |
| `BT9_068` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_068.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT9_070` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_070.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 1 |
| `BT9_075` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_075.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT9_078` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_078.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT9_080` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_080.cs` | `OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `BT9_081` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_081.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT9_082` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_082.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `BT9_083` | `DCGO/Assets/Scripts/CardEffect/BT9/White/BT9_083.cs` | `None, OnEnterFieldAnyone, OnStartTurn` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT9-064#1854@base` | `BT9-064` | 1854 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_064.asset` |
| `BT9-064#1855@P1` | `BT9-064` | 1855 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_064_P1.asset` |
| `BT9-064#8984@P0` | `BT9-064` | 8984 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_064_P0.asset` |
| `BT9-067#1859@base` | `BT9-067` | 1859 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_067.asset` |
| `BT9-067#6812@P0` | `BT9-067` | 6812 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_067_P0.asset` |
| `BT9-068#1860@base` | `BT9-068` | 1860 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_068.asset` |
| `BT9-068#1861@P1` | `BT9-068` | 1861 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_068_P1.asset` |
| `BT9-070#1863@base` | `BT9-070` | 1863 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_070.asset` |
| `BT9-075#1869@base` | `BT9-075` | 1869 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_075.asset` |
| `BT9-075#8989@P0` | `BT9-075` | 8989 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_075_P0.asset` |
| `BT9-078#1872@base` | `BT9-078` | 1872 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_078.asset` |
| `BT9-078#8990@P0` | `BT9-078` | 8990 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_078_P0.asset` |
| `BT9-080#1874@base` | `BT9-080` | 1874 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_080.asset` |
| `BT9-080#1875@P1` | `BT9-080` | 1875 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_080_P1.asset` |
| `BT9-080#8992@P0` | `BT9-080` | 8992 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_080_P0.asset` |
| `BT9-081#1876@base` | `BT9-081` | 1876 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_081.asset` |
| `BT9-081#1877@P1` | `BT9-081` | 1877 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_081_P1.asset` |
| `BT9-082#1878@base` | `BT9-082` | 1878 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_082.asset` |
| `BT9-082#1879@P1` | `BT9-082` | 1879 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_082_P1.asset` |
| `BT9-082#8993@P2` | `BT9-082` | 8993 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_082_P2.asset` |
| `BT9-082#8994@P3` | `BT9-082` | 8994 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_082_P3.asset` |
| `BT9-082#8995@P4` | `BT9-082` | 8995 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_082_P4.asset` |
| `BT9-083#1880@base` | `BT9-083` | 1880 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Digimon/BT9_083.asset` |
| `BT9-083#1881@P1` | `BT9-083` | 1881 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Digimon/BT9_083_P1.asset` |
| `BT9-083#8996@P2` | `BT9-083` | 8996 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Digimon/BT9_083_P2.asset` |
| `BT9-083#8997@P3` | `BT9-083` | 8997 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Digimon/BT9_083_P3.asset` |

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
