# C0323_special_digivolution_play - special digivolution/play mechanics card porting 88

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0323_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 29
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT4_004` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_004.cs` | `None` | `inherited, static_or_continuous` | `SelectBurstDigivolution` | 2 |
| `BT4_008` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_008.cs` | `OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectBurstDigivolution` | 7 |
| `BT4_012` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_012.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 1 |
| `BT4_017` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_017.cs` | `None, OnAllyAttack, OnDeclaration` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBurstDigivolution` | 6 |
| `BT4_019` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_019.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 3 |
| `BT4_026` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_026.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous` | `SelectBurstDigivolution` | 1 |
| `BT4_031` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_031.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT4_032` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_032.cs` | `None, OnDeclaration` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 2 |
| `BT4_033` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_033.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 3 |
| `BT4_046` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_046.cs` | `None, OnDeclaration` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectBurstDigivolution` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT4-004#762@base` | `BT4-004` | 762 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/DigiEgg/BT4_004.asset` |
| `BT4-004#8497@P0` | `BT4-004` | 8497 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/DigiEgg/BT4_004_P0.asset` |
| `BT4-008#770@base` | `BT4-008` | 770 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_008.asset` |
| `BT4-012#780@base` | `BT4-012` | 780 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_012.asset` |
| `BT4-017#787@base` | `BT4-017` | 787 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_017.asset` |
| `BT4-017#788@P1` | `BT4-017` | 788 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_017_P1.asset` |
| `BT4-017#8506@P2` | `BT4-017` | 8506 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_017_P2.asset` |
| `BT4-017#8507@P3` | `BT4-017` | 8507 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_017_P3.asset` |
| `BT4-017#8508@P4` | `BT4-017` | 8508 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_017_P4.asset` |
| `BT4-017#8509@P5` | `BT4-017` | 8509 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_017_P5.asset` |
| `BT4-019#790@base` | `BT4-019` | 790 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_019.asset` |
| `BT4-019#791@P1` | `BT4-019` | 791 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_019_P1.asset` |
| `BT4-019#8511@P0` | `BT4-019` | 8511 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_019_P0.asset` |
| `BT4-021#793@base` | `BT4-021` | 793 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_021.asset` |
| `BT4-026#799@base` | `BT4-026` | 799 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_026.asset` |
| `BT4-031#6779@P0` | `BT4-031` | 6779 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_031_P0.asset` |
| `BT4-031#805@base` | `BT4-031` | 805 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_031.asset` |
| `BT4-032#806@base` | `BT4-032` | 806 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_032.asset` |
| `BT4-032#8513@P0` | `BT4-032` | 8513 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_032_P0.asset` |
| `BT4-033#807@base` | `BT4-033` | 807 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_033.asset` |
| `BT4-033#808@P1` | `BT4-033` | 808 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_033_P1.asset` |
| `BT4-033#8514@P0` | `BT4-033` | 8514 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_033_P0.asset` |
| `BT4-046#824@base` | `BT4-046` | 824 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_046.asset` |
| `BT4-046#8518@P0` | `BT4-046` | 8518 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_046_P0.asset` |
| `BT4-052#832@base` | `BT4-052` | 832 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Digimon/BT4_052.asset` |
| `BT4-064#847@base` | `BT4-064` | 847 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Digimon/BT4_064.asset` |
| `BT4-077#864@base` | `BT4-077` | 864 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_077.asset` |
| `BT7-031#1422@base` | `BT7-031` | 1422 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_031.asset` |
| `BT7-031#1423@P1` | `BT7-031` | 1423 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_031_P1.asset` |

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
