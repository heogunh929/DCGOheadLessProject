# C0007_zone_security_recovery - zone/security/recovery card porting 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0007_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `AD1_019` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_019.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 1 |
| `AD1_022` | `DCGO/Assets/Scripts/CardEffect/AD1/Green/AD1_022.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT10_006` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_006.cs` | `OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT10_014` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_014.cs` | `None, OnEnterFieldAnyone` | `inherited, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `BT10_018` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_018.cs` | `OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT10_020` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_020.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `-` | 2 |
| `BT10_025` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_025.cs` | `None, OnDeclaration` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT10_028` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_028.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `BT10_030` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_030.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT10_042` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_042.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 7 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `AD1-019#7862@base` | `AD1-019` | 7862 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_019.asset` |
| `AD1-022#7864@base` | `AD1-022` | 7864 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Green/AD1_022.asset` |
| `BT10-006#2038@base` | `BT10-006` | 2038 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/DigiEgg/BT10_006.asset` |
| `BT10-006#4287@P0` | `BT10-006` | 4287 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/DigiEgg/BT10_006_P0.asset` |
| `BT10-014#2048@base` | `BT10-014` | 2048 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_014.asset` |
| `BT10-014#4298@P0` | `BT10-014` | 4298 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_014_P0.asset` |
| `BT10-018#2053@base` | `BT10-018` | 2053 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_018.asset` |
| `BT10-018#4301@P1` | `BT10-018` | 4301 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_018_P1.asset` |
| `BT10-020#2055@base` | `BT10-020` | 2055 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_020.asset` |
| `BT10-020#4304@P0` | `BT10-020` | 4304 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_020_P0.asset` |
| `BT10-025#2062@base` | `BT10-025` | 2062 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_025.asset` |
| `BT10-028#2066@base` | `BT10-028` | 2066 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_028.asset` |
| `BT10-028#4308@P0` | `BT10-028` | 4308 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_028_P0.asset` |
| `BT10-030#2068@base` | `BT10-030` | 2068 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_030.asset` |
| `BT10-042#2081@base` | `BT10-042` | 2081 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042.asset` |
| `BT10-042#2082@P1` | `BT10-042` | 2082 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042_P1.asset` |
| `BT10-042#4313@P2` | `BT10-042` | 4313 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042_P2.asset` |
| `BT10-042#4314@P3` | `BT10-042` | 4314 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042_P3.asset` |
| `BT10-042#4315@P4` | `BT10-042` | 4315 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042_P4.asset` |
| `BT10-042#4316@P5` | `BT10-042` | 4316 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042_P5.asset` |
| `BT10-042#8097@P6` | `BT10-042` | 8097 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042_P6.asset` |

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
