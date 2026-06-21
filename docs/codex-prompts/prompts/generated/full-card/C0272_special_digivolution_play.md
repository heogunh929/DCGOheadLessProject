# C0272_special_digivolution_play - special digivolution/play mechanics card porting 37

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0272_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT17_014` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_014.cs` | `None, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT17_015` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_015.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 3 |
| `BT17_017` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_017.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress, SelectDigiXros` | 3 |
| `BT17_019` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_019.cs` | `None, OnEndTurn, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 4 |
| `BT17_021` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_021.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectJogress` | 1 |
| `BT17_022` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_022.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 2 |
| `BT17_023` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_023.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT17_025` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_025.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `BT17_026` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_026.cs` | `OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT17_027` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_027.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectInteger, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT17-014#3554@base` | `BT17-014` | 3554 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_014.asset` |
| `BT17-015#3555@base` | `BT17-015` | 3555 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_015.asset` |
| `BT17-015#4844@P0` | `BT17-015` | 4844 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_015_P0.asset` |
| `BT17-015#8218@P1` | `BT17-015` | 8218 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_015_P1.asset` |
| `BT17-017#3557@base` | `BT17-017` | 3557 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_017.asset` |
| `BT17-017#3558@P1` | `BT17-017` | 3558 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_017_P1.asset` |
| `BT17-017#8220@P2` | `BT17-017` | 8220 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_017_P2.asset` |
| `BT17-019#3562@base` | `BT17-019` | 3562 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_019.asset` |
| `BT17-019#4846@P0` | `BT17-019` | 4846 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_019_P0.asset` |
| `BT17-019#8223@P1` | `BT17-019` | 8223 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_019_P1.asset` |
| `BT17-019#8224@P2` | `BT17-019` | 8224 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_019_P2.asset` |
| `BT17-021#3564@base` | `BT17-021` | 3564 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_021.asset` |
| `BT17-022#3565@base` | `BT17-022` | 3565 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_022.asset` |
| `BT17-022#4847@P1` | `BT17-022` | 4847 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_022_P1.asset` |
| `BT17-023#3566@base` | `BT17-023` | 3566 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_023.asset` |
| `BT17-025#3568@base` | `BT17-025` | 3568 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_025.asset` |
| `BT17-025#4848@P0` | `BT17-025` | 4848 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_025_P0.asset` |
| `BT17-026#3569@base` | `BT17-026` | 3569 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_026.asset` |
| `BT17-027#3570@base` | `BT17-027` | 3570 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_027.asset` |
| `BT17-027#4849@P0` | `BT17-027` | 4849 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_027_P0.asset` |
| `BT17-027#8225@P1` | `BT17-027` | 8225 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_027_P1.asset` |

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
