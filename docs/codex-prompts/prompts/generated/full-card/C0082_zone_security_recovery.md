# C0082_zone_security_recovery - zone/security/recovery card porting 76

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0082_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX7_051` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_051.cs` | `None, OnDestroyedAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean` | 1 |
| `EX7_053` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_053.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 2 |
| `EX7_056` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_056.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX7_057` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_057.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX7_060` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_060.cs` | `None, OnDeclaration, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `EX7_062` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_062.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 4 |
| `EX7_063` | `DCGO/Assets/Scripts/CardEffect/EX7/Yellow/EX7_063.cs` | `OnDestroyedAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |
| `EX7_064` | `DCGO/Assets/Scripts/CardEffect/EX7/Green/EX7_064.cs` | `OnEndTurn, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |
| `EX7_065` | `DCGO/Assets/Scripts/CardEffect/EX7/Purple/EX7_065.cs` | `OnDeclaration, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |
| `EX7_070` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_070.cs` | `None, OnDigivolutionCardDiscarded, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX7-051#3771@base` | `EX7-051` | 3771 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_051.asset` |
| `EX7-053#3774@base` | `EX7-053` | 3774 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_053.asset` |
| `EX7-053#3775@P1` | `EX7-053` | 3775 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_053_P1.asset` |
| `EX7-056#3780@base` | `EX7-056` | 3780 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_056.asset` |
| `EX7-056#3781@P1` | `EX7-056` | 3781 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_056_P1.asset` |
| `EX7-057#3782@base` | `EX7-057` | 3782 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_057.asset` |
| `EX7-057#3783@P1` | `EX7-057` | 3783 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_057_P1.asset` |
| `EX7-060#3788@base` | `EX7-060` | 3788 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_060.asset` |
| `EX7-062#3791@base` | `EX7-062` | 3791 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_062.asset` |
| `EX7-062#3792@P1` | `EX7-062` | 3792 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_062_P1.asset` |
| `EX7-062#3793@P2` | `EX7-062` | 3793 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_062_P2.asset` |
| `EX7-062#9183@P3` | `EX7-062` | 9183 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Digimon/EX7_062_P3.asset` |
| `EX7-063#3794@base` | `EX7-063` | 3794 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Tamer/EX7_063.asset` |
| `EX7-063#3795@P1` | `EX7-063` | 3795 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Tamer/EX7_063_P1.asset` |
| `EX7-064#3796@base` | `EX7-064` | 3796 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Tamer/EX7_064.asset` |
| `EX7-064#3797@P1` | `EX7-064` | 3797 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Tamer/EX7_064_P1.asset` |
| `EX7-064#9184@P2` | `EX7-064` | 9184 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Tamer/EX7_064_P2.asset` |
| `EX7-065#3798@base` | `EX7-065` | 3798 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Tamer/EX7_065.asset` |
| `EX7-065#3799@P1` | `EX7-065` | 3799 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Tamer/EX7_065_P1.asset` |
| `EX7-065#9185@P2` | `EX7-065` | 9185 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Purple/Tamer/EX7_065_P2.asset` |
| `EX7-070#3808@base` | `EX7-070` | 3808 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Option/EX7_070.asset` |
| `EX7-070#3809@P1` | `EX7-070` | 3809 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Option/EX7_070_P1.asset` |

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
