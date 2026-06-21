# C0096_zone_security_recovery - zone/security/recovery card porting 90

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0096_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST18_05` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_05.cs` | `OnDetermineDoSecurityCheck, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `ST18_06` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_06.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `ST18_09` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_09.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 1 |
| `ST18_11` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_11.cs` | `OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `ST18_12` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_12.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OnUnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 5 |
| `ST18_13` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_13.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `ST19_03` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_03.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 3 |
| `ST19_04` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_04.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 1 |
| `ST19_05` | `DCGO/Assets/Scripts/CardEffect/ST19/Black/ST19_05.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `ST19_06` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_06.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST18-05#3822@base` | `ST18-05` | 3822 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_05.asset` |
| `ST18-06#3823@base` | `ST18-06` | 3823 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_06.asset` |
| `ST18-09#3826@base` | `ST18-09` | 3826 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_09.asset` |
| `ST18-11#3828@base` | `ST18-11` | 3828 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_11.asset` |
| `ST18-12#3829@base` | `ST18-12` | 3829 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_12.asset` |
| `ST18-12#9042@P1` | `ST18-12` | 9042 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_12_P1.asset` |
| `ST18-12#9043@P2` | `ST18-12` | 9043 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_12_P2.asset` |
| `ST18-12#9044@P3` | `ST18-12` | 9044 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_12_P3.asset` |
| `ST18-12#9045@P4` | `ST18-12` | 9045 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_12_P4.asset` |
| `ST18-13#3830@base` | `ST18-13` | 3830 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_13.asset` |
| `ST19-03#3835@base` | `ST19-03` | 3835 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_03.asset` |
| `ST19-03#9047@P1` | `ST19-03` | 9047 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_03_P1.asset` |
| `ST19-03#9048@P2` | `ST19-03` | 9048 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_03_P2.asset` |
| `ST19-04#3836@base` | `ST19-04` | 3836 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_04.asset` |
| `ST19-05#3837@base` | `ST19-05` | 3837 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Black/Digimon/ST19_05.asset` |
| `ST19-06#3838@base` | `ST19-06` | 3838 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_06.asset` |

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
