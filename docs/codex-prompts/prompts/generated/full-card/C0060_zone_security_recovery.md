# C0060_zone_security_recovery - zone/security/recovery card porting 54

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0060_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 28
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT7_014` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_014.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity` | 5 |
| `BT7_015` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_015.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectOrder` | 2 |
| `BT7_018` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_018.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 4 |
| `BT7_021` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_021.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `BT7_024` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_024.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `BT7_026` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_026.cs` | `OnEnterFieldAnyone, OnUnTappedAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT7_027` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_027.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `BT7_030` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_030.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 4 |
| `BT7_033` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_033.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |
| `BT7_035` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_035.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT7-014#1396@base` | `BT7-014` | 1396 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_014.asset` |
| `BT7-014#8758@P0` | `BT7-014` | 8758 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_014_P0.asset` |
| `BT7-014#8759@P1` | `BT7-014` | 8759 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_014_P1.asset` |
| `BT7-014#8760@P2` | `BT7-014` | 8760 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_014_P2.asset` |
| `BT7-014#8761@P3` | `BT7-014` | 8761 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_014_P3.asset` |
| `BT7-015#1397@base` | `BT7-015` | 1397 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_015.asset` |
| `BT7-015#8762@P0` | `BT7-015` | 8762 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_015_P0.asset` |
| `BT7-018#1401@base` | `BT7-018` | 1401 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_018.asset` |
| `BT7-018#1402@P1` | `BT7-018` | 1402 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_018_P1.asset` |
| `BT7-018#8765@P0` | `BT7-018` | 8765 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_018_P0.asset` |
| `BT7-018#8766@P2` | `BT7-018` | 8766 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_018_P2.asset` |
| `BT7-021#1406@base` | `BT7-021` | 1406 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_021.asset` |
| `BT7-021#1407@P1` | `BT7-021` | 1407 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_021_P1.asset` |
| `BT7-021#1408@P2` | `BT7-021` | 1408 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_021_P2.asset` |
| `BT7-024#1412@base` | `BT7-024` | 1412 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_024.asset` |
| `BT7-024#8768@P0` | `BT7-024` | 8768 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_024_P0.asset` |
| `BT7-026#1414@base` | `BT7-026` | 1414 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_026.asset` |
| `BT7-026#1415@P1` | `BT7-026` | 1415 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_026_P1.asset` |
| `BT7-027#1416@base` | `BT7-027` | 1416 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_027.asset` |
| `BT7-030#1420@base` | `BT7-030` | 1420 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_030.asset` |
| `BT7-030#1421@P1` | `BT7-030` | 1421 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_030_P1.asset` |
| `BT7-030#8770@P0` | `BT7-030` | 8770 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_030_P0.asset` |
| `BT7-030#8771@P2` | `BT7-030` | 8771 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_030_P2.asset` |
| `BT7-033#1425@base` | `BT7-033` | 1425 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_033.asset` |
| `BT7-033#6789@P1` | `BT7-033` | 6789 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_033_P1.asset` |
| `BT7-035#1427@base` | `BT7-035` | 1427 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_035.asset` |
| `BT7-035#1428@P1` | `BT7-035` | 1428 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_035_P1.asset` |
| `BT7-035#1429@P2` | `BT7-035` | 1429 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_035_P2.asset` |

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
