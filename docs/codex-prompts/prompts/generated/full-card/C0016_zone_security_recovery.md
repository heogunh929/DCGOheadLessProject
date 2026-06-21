# C0016_zone_security_recovery - zone/security/recovery card porting 10

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0016_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 29
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT13_100` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_100.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 4 |
| `BT13_102` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_102.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectSecurity` | 3 |
| `BT14_001` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_001.cs` | `OnLoseSecurity` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 5 |
| `BT14_003` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_003.cs` | `OnAddSecurity` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 3 |
| `BT14_006` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_006.cs` | `OnDiscardHand` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 4 |
| `BT14_017` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_017.cs` | `None, OnEnterFieldAnyone` | `inherited, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `BT14_027` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_027.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `-` | 1 |
| `BT14_030` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_030.cs` | `OnEnterFieldAnyone, OnPermamemtReturnedToHand` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT14_033` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_033.cs` | `OnAddSecurity, OnStartMainPhase` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 3 |
| `BT14_034` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_034.cs` | `OnDestroyedAnyone, SecuritySkill` | `inherited, max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT13-100#2767@base` | `BT13-100` | 2767 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Tamer/BT13_100.asset` |
| `BT13-100#2768@P1` | `BT13-100` | 2768 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Tamer/BT13_100_P1.asset` |
| `BT13-100#4619@P0` | `BT13-100` | 4619 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Tamer/BT13_100_P0.asset` |
| `BT13-100#4620@P2` | `BT13-100` | 4620 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Green/Tamer/BT13_100_P2.asset` |
| `BT13-102#2771@base` | `BT13-102` | 2771 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Tamer/BT13_102.asset` |
| `BT13-102#2772@P1` | `BT13-102` | 2772 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Tamer/BT13_102_P1.asset` |
| `BT13-102#4622@P0` | `BT13-102` | 4622 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Tamer/BT13_102_P0.asset` |
| `BT14-001#2912@base` | `BT14-001` | 2912 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/DigiEgg/BT14_001.asset` |
| `BT14-001#2913@P1` | `BT14-001` | 2913 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/DigiEgg/BT14_001_P1.asset` |
| `BT14-001#4632@P0` | `BT14-001` | 4632 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/DigiEgg/BT14_001_P0.asset` |
| `BT14-001#8164@P2` | `BT14-001` | 8164 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/DigiEgg/BT14_001_P2.asset` |
| `BT14-001#8165@P3` | `BT14-001` | 8165 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/DigiEgg/BT14_001_P3.asset` |
| `BT14-003#2916@base` | `BT14-003` | 2916 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/DigiEgg/BT14_003.asset` |
| `BT14-003#2917@P1` | `BT14-003` | 2917 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/DigiEgg/BT14_003_P1.asset` |
| `BT14-003#4634@P0` | `BT14-003` | 4634 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/DigiEgg/BT14_003_P0.asset` |
| `BT14-006#2922@base` | `BT14-006` | 2922 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/DigiEgg/BT14_006.asset` |
| `BT14-006#2923@P1` | `BT14-006` | 2923 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/DigiEgg/BT14_006_P1.asset` |
| `BT14-006#4637@P0` | `BT14-006` | 4637 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/DigiEgg/BT14_006_P0.asset` |
| `BT14-006#8166@P2` | `BT14-006` | 8166 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/DigiEgg/BT14_006_P2.asset` |
| `BT14-017#2936@base` | `BT14-017` | 2936 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_017.asset` |
| `BT14-017#4642@P0` | `BT14-017` | 4642 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_017_P0.asset` |
| `BT14-027#2948@base` | `BT14-027` | 2948 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_027.asset` |
| `BT14-030#2951@base` | `BT14-030` | 2951 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_030.asset` |
| `BT14-030#4649@P0` | `BT14-030` | 4649 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_030_P0.asset` |
| `BT14-033#2954@base` | `BT14-033` | 2954 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_033.asset` |
| `BT14-033#2955@P1` | `BT14-033` | 2955 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_033_P1.asset` |
| `BT14-033#8173@P2` | `BT14-033` | 8173 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_033_P2.asset` |
| `BT14-034#2956@base` | `BT14-034` | 2956 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_034.asset` |
| `BT14-034#4650@P0` | `BT14-034` | 4650 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_034_P0.asset` |

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
