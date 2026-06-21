# C0089_zone_security_recovery - zone/security/recovery card porting 83

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0089_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_079` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_079.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 3 |
| `P_080` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_080.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 3 |
| `P_081` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_081.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 3 |
| `P_082` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_082.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `P_083` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_083.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `P_084` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_084.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `P_085` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_085.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `-` | 1 |
| `P_086` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_086.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `P_098` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_098.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `P_100` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_100.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-079#10390@P2` | `P-079` | 10390 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_079_P2.asset` |
| `P-079#6122@base` | `P-079` | 6122 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_079.asset` |
| `P-079#6123@P1` | `P-079` | 6123 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_079_P1.asset` |
| `P-080#10393@P2` | `P-080` | 10393 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_080_P2.asset` |
| `P-080#6124@base` | `P-080` | 6124 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_080.asset` |
| `P-080#6125@P1` | `P-080` | 6125 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_080_P1.asset` |
| `P-081#10397@P2` | `P-081` | 10397 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_081_P2.asset` |
| `P-081#6126@base` | `P-081` | 6126 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_081.asset` |
| `P-081#6127@P1` | `P-081` | 6127 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_081_P1.asset` |
| `P-082#6128@base` | `P-082` | 6128 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_082.asset` |
| `P-083#6129@base` | `P-083` | 6129 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_083.asset` |
| `P-084#6130@base` | `P-084` | 6130 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_084.asset` |
| `P-085#6131@base` | `P-085` | 6131 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_085.asset` |
| `P-086#6132@base` | `P-086` | 6132 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_086.asset` |
| `P-098#6144@base` | `P-098` | 6144 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_098.asset` |
| `P-100#6146@base` | `P-100` | 6146 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_100.asset` |

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
