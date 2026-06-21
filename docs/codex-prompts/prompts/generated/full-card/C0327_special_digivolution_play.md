# C0327_special_digivolution_play - special digivolution/play mechanics card porting 92

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0327_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 30
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT5_004` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_004.cs` | `OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 2 |
| `BT5_007` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_007.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 5 |
| `BT5_008` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_008.cs` | `None` | `inherited, modifier_duration, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT5_009` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_009.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT5_010` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_010.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 5 |
| `BT5_014` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_014.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `BT5_015` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_015.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT5_019` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_019.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT5_020` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_020.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 6 |
| `BT5_024` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_024.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT5-004#8573@P0` | `BT5-004` | 8573 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/DigiEgg/BT5_004_P0.asset` |
| `BT5-004#933@base` | `BT5-004` | 933 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/DigiEgg/BT5_004.asset` |
| `BT5-007#936@base` | `BT5-007` | 936 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_007.asset` |
| `BT5-007#937@P1` | `BT5-007` | 937 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_007_P1.asset` |
| `BT5-007#938@P2` | `BT5-007` | 938 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_007_P2.asset` |
| `BT5-007#939@P3` | `BT5-007` | 939 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_007_P3.asset` |
| `BT5-007#940@P4` | `BT5-007` | 940 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_007_P4.asset` |
| `BT5-008#941@base` | `BT5-008` | 941 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_008.asset` |
| `BT5-009#8576@P0` | `BT5-009` | 8576 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_009_P0.asset` |
| `BT5-009#942@base` | `BT5-009` | 942 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_009.asset` |
| `BT5-009#943@P1` | `BT5-009` | 943 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_009_P1.asset` |
| `BT5-010#8577@P0` | `BT5-010` | 8577 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_010_P0.asset` |
| `BT5-010#944@base` | `BT5-010` | 944 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_010.asset` |
| `BT5-010#945@P1` | `BT5-010` | 945 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_010_P1.asset` |
| `BT5-010#946@P2` | `BT5-010` | 946 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_010_P2.asset` |
| `BT5-010#947@P3` | `BT5-010` | 947 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_010_P3.asset` |
| `BT5-014#8578@P0` | `BT5-014` | 8578 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_014_P0.asset` |
| `BT5-014#954@base` | `BT5-014` | 954 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_014.asset` |
| `BT5-015#8579@P0` | `BT5-015` | 8579 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_015_P0.asset` |
| `BT5-015#955@base` | `BT5-015` | 955 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_015.asset` |
| `BT5-019#959@base` | `BT5-019` | 959 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_019.asset` |
| `BT5-019#960@P1` | `BT5-019` | 960 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_019_P1.asset` |
| `BT5-020#8582@P0` | `BT5-020` | 8582 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_020_P0.asset` |
| `BT5-020#8583@P4` | `BT5-020` | 8583 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_020_P4.asset` |
| `BT5-020#961@base` | `BT5-020` | 961 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_020.asset` |
| `BT5-020#962@P1` | `BT5-020` | 962 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_020_P1.asset` |
| `BT5-020#963@P2` | `BT5-020` | 963 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_020_P2.asset` |
| `BT5-020#964@P3` | `BT5-020` | 964 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_020_P3.asset` |
| `BT5-024#968@base` | `BT5-024` | 968 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_024.asset` |
| `BT5-024#969@P1` | `BT5-024` | 969 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_024_P1.asset` |

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
