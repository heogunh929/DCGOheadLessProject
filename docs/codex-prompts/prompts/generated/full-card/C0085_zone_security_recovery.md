# C0085_zone_security_recovery - zone/security/recovery card porting 79

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0085_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX8_070` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_070.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX9_002` | `DCGO/Assets/Scripts/CardEffect/EX9/Blue/EX9_002.cs` | `OnAddDigivolutionCards` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 2 |
| `EX9_003` | `DCGO/Assets/Scripts/CardEffect/EX9/Yellow/EX9_003.cs` | `BeforePayCost` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `EX9_004` | `DCGO/Assets/Scripts/CardEffect/EX9/Green/EX9_004.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 2 |
| `EX9_026` | `DCGO/Assets/Scripts/CardEffect/EX9/Yellow/EX9_026.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `EX9_036` | `DCGO/Assets/Scripts/CardEffect/EX9/Green/EX9_036.cs` | `BeforePayCost, None` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `EX9_040` | `DCGO/Assets/Scripts/CardEffect/EX9/Green/EX9_040.cs` | `None, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX9_042` | `DCGO/Assets/Scripts/CardEffect/EX9/Green/EX9_042.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX9_048` | `DCGO/Assets/Scripts/CardEffect/EX9/Black/EX9_048.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 2 |
| `EX9_072` | `DCGO/Assets/Scripts/CardEffect/EX9/White/EX9_072.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX8-070#4185@base` | `EX8-070` | 4185 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Option/EX8_070.asset` |
| `EX8-070#4186@P1` | `EX8-070` | 4186 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Option/EX8_070_P1.asset` |
| `EX9-002#6833@base` | `EX9-002` | 6833 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/DigiEgg/EX9_002.asset` |
| `EX9-002#6834@P1` | `EX9-002` | 6834 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/DigiEgg/EX9_002_P1.asset` |
| `EX9-003#6835@base` | `EX9-003` | 6835 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/DigiEgg/EX9_003.asset` |
| `EX9-003#6836@P1` | `EX9-003` | 6836 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/DigiEgg/EX9_003_P1.asset` |
| `EX9-004#6837@base` | `EX9-004` | 6837 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/DigiEgg/EX9_004.asset` |
| `EX9-004#6838@P1` | `EX9-004` | 6838 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/DigiEgg/EX9_004_P1.asset` |
| `EX9-026#6884@base` | `EX9-026` | 6884 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_026.asset` |
| `EX9-026#6885@P1` | `EX9-026` | 6885 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Yellow/Digimon/EX9_026_P1.asset` |
| `EX9-036#6904@base` | `EX9-036` | 6904 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_036.asset` |
| `EX9-036#6905@P1` | `EX9-036` | 6905 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_036_P1.asset` |
| `EX9-040#6912@base` | `EX9-040` | 6912 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_040.asset` |
| `EX9-040#6913@P1` | `EX9-040` | 6913 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_040_P1.asset` |
| `EX9-042#6915@base` | `EX9-042` | 6915 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_042.asset` |
| `EX9-042#6916@P1` | `EX9-042` | 6916 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Green/Digimon/EX9_042_P1.asset` |
| `EX9-048#6927@base` | `EX9-048` | 6927 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_048.asset` |
| `EX9-048#6928@P1` | `EX9-048` | 6928 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Black/Digimon/EX9_048_P1.asset` |
| `EX9-072#6970@base` | `EX9-072` | 6970 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/White/Option/EX9_072.asset` |
| `EX9-072#6971@P1` | `EX9-072` | 6971 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/White/Option/EX9_072_P1.asset` |

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
