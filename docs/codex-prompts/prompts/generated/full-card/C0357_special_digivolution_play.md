# C0357_special_digivolution_play - special digivolution/play mechanics card porting 122

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0357_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX1_004` | `DCGO/Assets/Scripts/CardEffect/EX1/Red/EX1_004.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 3 |
| `EX1_005` | `DCGO/Assets/Scripts/CardEffect/EX1/Red/EX1_005.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX1_011` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_011.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `SelectCard, SelectJogress` | 2 |
| `EX1_014` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_014.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 3 |
| `EX1_015` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_015.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX1_019` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_019.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 2 |
| `EX1_038` | `DCGO/Assets/Scripts/CardEffect/EX1/Green/EX1_038.cs` | `OnDetermineDoSecurityCheck` | `inherited` | `SelectJogress` | 1 |
| `EX1_041` | `DCGO/Assets/Scripts/CardEffect/EX1/Green/EX1_041.cs` | `OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX1_044` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_044.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 1 |
| `EX1_046` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_046.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX1-004#1264@base` | `EX1-004` | 1264 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_004.asset` |
| `EX1-004#1265@P1` | `EX1-004` | 1265 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_004_P1.asset` |
| `EX1-004#9088@P2` | `EX1-004` | 9088 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_004_P2.asset` |
| `EX1-005#1266@base` | `EX1-005` | 1266 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_005.asset` |
| `EX1-005#1267@P1` | `EX1-005` | 1267 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_005_P1.asset` |
| `EX1-011#1280@base` | `EX1-011` | 1280 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_011.asset` |
| `EX1-011#1281@P1` | `EX1-011` | 1281 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_011_P1.asset` |
| `EX1-014#1286@base` | `EX1-014` | 1286 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_014.asset` |
| `EX1-014#1287@P1` | `EX1-014` | 1287 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_014_P1.asset` |
| `EX1-014#9089@P2` | `EX1-014` | 9089 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_014_P2.asset` |
| `EX1-015#1288@base` | `EX1-015` | 1288 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_015.asset` |
| `EX1-015#1289@P1` | `EX1-015` | 1289 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_015_P1.asset` |
| `EX1-019#1295@base` | `EX1-019` | 1295 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_019.asset` |
| `EX1-019#1296@P1` | `EX1-019` | 1296 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_019_P1.asset` |
| `EX1-038#1326@base` | `EX1-038` | 1326 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_038.asset` |
| `EX1-041#1332@base` | `EX1-041` | 1332 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_041.asset` |
| `EX1-044#1336@base` | `EX1-044` | 1336 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_044.asset` |
| `EX1-046#1338@base` | `EX1-046` | 1338 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_046.asset` |

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
