# C0023_zone_security_recovery - zone/security/recovery card porting 17

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0023_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT16_054` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_054.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `BT16_056` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_056.cs` | `OnAddSecurity, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity` | 1 |
| `BT16_060` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_060.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT16_068` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_068.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT16_075` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_075.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT16_078` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_078.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT16_082` | `DCGO/Assets/Scripts/CardEffect/BT16/White/BT16_082.cs` | `OnMove` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectBoolean` | 9 |
| `BT16_095` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_095.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectSecurity` | 1 |
| `BT17_002` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_002.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `BT17_006` | `DCGO/Assets/Scripts/CardEffect/BT17/Purple/BT17_006.cs` | `OnAddDigivolutionCards` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT16-054#3371@base` | `BT16-054` | 3371 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_054.asset` |
| `BT16-056#3373@base` | `BT16-056` | 3373 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_056.asset` |
| `BT16-060#3377@base` | `BT16-060` | 3377 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_060.asset` |
| `BT16-068#3386@base` | `BT16-068` | 3386 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_068.asset` |
| `BT16-075#3393@base` | `BT16-075` | 3393 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_075.asset` |
| `BT16-075#4807@P0` | `BT16-075` | 4807 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_075_P0.asset` |
| `BT16-078#3397@base` | `BT16-078` | 3397 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_078.asset` |
| `BT16-078#4810@P0` | `BT16-078` | 4810 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_078_P0.asset` |
| `BT16-082#3403@base` | `BT16-082` | 3403 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082.asset` |
| `BT16-082#3404@P1` | `BT16-082` | 3404 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P1.asset` |
| `BT16-082#4812@P0` | `BT16-082` | 4812 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P0.asset` |
| `BT16-082#4813@P2` | `BT16-082` | 4813 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P2.asset` |
| `BT16-082#4814@P3` | `BT16-082` | 4814 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P3.asset` |
| `BT16-082#4815@P4` | `BT16-082` | 4815 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P4.asset` |
| `BT16-082#8209@P5` | `BT16-082` | 8209 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P5.asset` |
| `BT16-082#8210@P6` | `BT16-082` | 8210 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P6.asset` |
| `BT16-082#8211@P7` | `BT16-082` | 8211 | `P7` | `None` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P7.asset` |
| `BT16-095#3423@base` | `BT16-095` | 3423 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Option/BT16_095.asset` |
| `BT17-002#3542@base` | `BT17-002` | 3542 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/DigiEgg/BT17_002.asset` |
| `BT17-002#4834@P0` | `BT17-002` | 4834 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/DigiEgg/BT17_002_P0.asset` |
| `BT17-006#3546@base` | `BT17-006` | 3546 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/DigiEgg/BT17_006.asset` |
| `BT17-006#4838@P0` | `BT17-006` | 4838 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/DigiEgg/BT17_006_P0.asset` |

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
