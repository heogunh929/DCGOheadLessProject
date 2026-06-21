# C0069_zone_security_recovery - zone/security/recovery card porting 63

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0069_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX11_008` | `DCGO/Assets/Scripts/CardEffect/EX11/Red/EX11_008.cs` | `OnEnterFieldAnyone, OnLoseSecurity, OnMove` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX11_010` | `DCGO/Assets/Scripts/CardEffect/EX11/Red/EX11_010.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX11_016` | `DCGO/Assets/Scripts/CardEffect/EX11/Blue/EX11_016.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity` | 2 |
| `EX11_025` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_025.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 2 |
| `EX11_026` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_026.cs` | `OnEndBattle, OnEnterFieldAnyone, OnMove` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX11_030` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_030.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 2 |
| `EX11_035` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_035.cs` | `None, OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 3 |
| `EX11_038` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_038.cs` | `OnDigivolutionCardDiscarded, OnEnterFieldAnyone, OnMove` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectBoolean, SelectInteger` | 2 |
| `EX11_043` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_043.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OnSecurityCheck` | `max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `EX11_048` | `DCGO/Assets/Scripts/CardEffect/EX11/Purple/EX11_048.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone, OnMove` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX11-008#7671@base` | `EX11-008` | 7671 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_008.asset` |
| `EX11-008#7672@P1` | `EX11-008` | 7672 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_008_P1.asset` |
| `EX11-010#7675@base` | `EX11-010` | 7675 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_010.asset` |
| `EX11-010#7676@P1` | `EX11-010` | 7676 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_010_P1.asset` |
| `EX11-016#7687@base` | `EX11-016` | 7687 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_016.asset` |
| `EX11-016#7688@P1` | `EX11-016` | 7688 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_016_P1.asset` |
| `EX11-025#7707@base` | `EX11-025` | 7707 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_025.asset` |
| `EX11-025#7708@P1` | `EX11-025` | 7708 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_025_P1.asset` |
| `EX11-026#7709@base` | `EX11-026` | 7709 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_026.asset` |
| `EX11-026#7710@P1` | `EX11-026` | 7710 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_026_P1.asset` |
| `EX11-030#7718@base` | `EX11-030` | 7718 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_030.asset` |
| `EX11-030#7719@P1` | `EX11-030` | 7719 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_030_P1.asset` |
| `EX11-035#7728@base` | `EX11-035` | 7728 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_035.asset` |
| `EX11-035#7729@P1` | `EX11-035` | 7729 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_035_P1.asset` |
| `EX11-035#7730@P2` | `EX11-035` | 7730 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_035_P2.asset` |
| `EX11-038#7735@base` | `EX11-038` | 7735 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_038.asset` |
| `EX11-038#7736@P1` | `EX11-038` | 7736 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_038_P1.asset` |
| `EX11-043#7745@base` | `EX11-043` | 7745 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_043.asset` |
| `EX11-043#7746@P1` | `EX11-043` | 7746 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_043_P1.asset` |
| `EX11-048#7756@base` | `EX11-048` | 7756 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_048.asset` |
| `EX11-048#7757@P1` | `EX11-048` | 7757 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_048_P1.asset` |

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
