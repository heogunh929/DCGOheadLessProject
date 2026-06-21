# C0074_zone_security_recovery - zone/security/recovery card porting 68

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0074_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX3_015` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_015.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `EX3_017` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_017.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `EX3_019` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_019.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `EX3_021` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_021.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `EX3_023` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_023.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `EX3_028` | `DCGO/Assets/Scripts/CardEffect/EX3/Yellow/EX3_028.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `EX3_029` | `DCGO/Assets/Scripts/CardEffect/EX3/Yellow/EX3_029.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `EX3_030` | `DCGO/Assets/Scripts/CardEffect/EX3/Yellow/EX3_030.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `EX3_031` | `DCGO/Assets/Scripts/CardEffect/EX3/Yellow/EX3_031.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX3_032` | `DCGO/Assets/Scripts/CardEffect/EX3/Yellow/EX3_032.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX3-015#2189@base` | `EX3-015` | 2189 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_015.asset` |
| `EX3-017#2191@base` | `EX3-017` | 2191 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_017.asset` |
| `EX3-019#2193@base` | `EX3-019` | 2193 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_019.asset` |
| `EX3-021#2196@base` | `EX3-021` | 2196 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_021.asset` |
| `EX3-023#2198@base` | `EX3-023` | 2198 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_023.asset` |
| `EX3-023#6824@P1` | `EX3-023` | 6824 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_023_P1.asset` |
| `EX3-028#2206@base` | `EX3-028` | 2206 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_028.asset` |
| `EX3-029#2207@base` | `EX3-029` | 2207 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_029.asset` |
| `EX3-030#2208@base` | `EX3-030` | 2208 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_030.asset` |
| `EX3-031#2209@base` | `EX3-031` | 2209 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_031.asset` |
| `EX3-031#6825@P1` | `EX3-031` | 6825 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_031_P1.asset` |
| `EX3-032#2210@base` | `EX3-032` | 2210 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_032.asset` |

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
