# C0083_zone_security_recovery - zone/security/recovery card porting 77

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0083_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX7_074` | `DCGO/Assets/Scripts/CardEffect/EX7/Green/EX7_074.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity` | 3 |
| `EX8_010` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_010.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `EX8_011` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_011.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, security, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `EX8_014` | `DCGO/Assets/Scripts/CardEffect/EX8/Red/EX8_014.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectBoolean` | 2 |
| `EX8_017` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_017.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `EX8_023` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_023.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX8_030` | `DCGO/Assets/Scripts/CardEffect/EX8/Yellow/EX8_030.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 4 |
| `EX8_036` | `DCGO/Assets/Scripts/CardEffect/EX8/Yellow/EX8_036.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity` | 2 |
| `EX8_039` | `DCGO/Assets/Scripts/CardEffect/EX8/Green/EX8_039.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `EX8_040` | `DCGO/Assets/Scripts/CardEffect/EX8/Green/EX8_040.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX7-074#3816@base` | `EX7-074` | 3816 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Option/EX7_074.asset` |
| `EX7-074#3817@P1` | `EX7-074` | 3817 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Option/EX7_074_P1.asset` |
| `EX7-074#9186@P2` | `EX7-074` | 9186 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Option/EX7_074_P2.asset` |
| `EX8-010#4067@base` | `EX8-010` | 4067 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_010.asset` |
| `EX8-010#4068@P1` | `EX8-010` | 4068 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_010_P1.asset` |
| `EX8-011#4069@base` | `EX8-011` | 4069 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_011.asset` |
| `EX8-011#9187@P1` | `EX8-011` | 9187 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_011_P1.asset` |
| `EX8-014#4074@base` | `EX8-014` | 4074 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_014.asset` |
| `EX8-014#4075@P1` | `EX8-014` | 4075 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Red/Digimon/EX8_014_P1.asset` |
| `EX8-017#4080@base` | `EX8-017` | 4080 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_017.asset` |
| `EX8-017#4081@P1` | `EX8-017` | 4081 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_017_P1.asset` |
| `EX8-023#4090@base` | `EX8-023` | 4090 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_023.asset` |
| `EX8-023#4091@P1` | `EX8-023` | 4091 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Digimon/EX8_023_P1.asset` |
| `EX8-030#4105@base` | `EX8-030` | 4105 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_030.asset` |
| `EX8-030#4106@P1` | `EX8-030` | 4106 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_030_P1.asset` |
| `EX8-030#9193@P2` | `EX8-030` | 9193 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_030_P2.asset` |
| `EX8-030#9194@P3` | `EX8-030` | 9194 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_030_P3.asset` |
| `EX8-036#4116@base` | `EX8-036` | 4116 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_036.asset` |
| `EX8-036#4117@P1` | `EX8-036` | 4117 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Yellow/Digimon/EX8_036_P1.asset` |
| `EX8-039#4122@base` | `EX8-039` | 4122 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_039.asset` |
| `EX8-040#4123@base` | `EX8-040` | 4123 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_040.asset` |
| `EX8-040#4124@P1` | `EX8-040` | 4124 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_040_P1.asset` |

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
