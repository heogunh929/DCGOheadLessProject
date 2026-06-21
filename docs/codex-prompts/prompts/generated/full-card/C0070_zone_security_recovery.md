# C0070_zone_security_recovery - zone/security/recovery card porting 64

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0070_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX11_051` | `DCGO/Assets/Scripts/CardEffect/EX11/Purple/EX11_051.cs` | `OnDestroyedAnyone, OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `EX11_054` | `DCGO/Assets/Scripts/CardEffect/EX11/Red/EX11_054.cs` | `None, OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX11_057` | `DCGO/Assets/Scripts/CardEffect/EX11/Blue/EX11_057.cs` | `OnDigivolutionCardDiscarded, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectSecurity` | 1 |
| `EX11_058` | `DCGO/Assets/Scripts/CardEffect/EX11/Blue/EX11_058.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `EX11_060` | `DCGO/Assets/Scripts/CardEffect/EX11/Yellow/EX11_060.cs` | `OnDestroyedAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |
| `EX11_061` | `DCGO/Assets/Scripts/CardEffect/EX11/Yellow/EX11_061.cs` | `OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 1 |
| `EX11_062` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_062.cs` | `None, OnStartTurn, OnTappedAnyone, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX11_065` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_065.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectInteger, SelectSecurity` | 2 |
| `EX1_007` | `DCGO/Assets/Scripts/CardEffect/EX1/Red/EX1_007.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 4 |
| `EX1_012` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_012.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX1-007#1270@base` | `EX1-007` | 1270 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_007.asset` |
| `EX1-007#1271@P1` | `EX1-007` | 1271 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_007_P1.asset` |
| `EX1-007#1272@P2` | `EX1-007` | 1272 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_007_P2.asset` |
| `EX1-007#1273@P3` | `EX1-007` | 1273 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_007_P3.asset` |
| `EX1-012#1282@base` | `EX1-012` | 1282 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_012.asset` |
| `EX1-012#1283@P1` | `EX1-012` | 1283 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_012_P1.asset` |
| `EX11-051#7762@base` | `EX11-051` | 7762 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_051.asset` |
| `EX11-051#7763@P1` | `EX11-051` | 7763 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_051_P1.asset` |
| `EX11-051#7764@P2` | `EX11-051` | 7764 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_051_P2.asset` |
| `EX11-054#7768@base` | `EX11-054` | 7768 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Tamer/EX11_054.asset` |
| `EX11-054#7769@P1` | `EX11-054` | 7769 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Tamer/EX11_054_P1.asset` |
| `EX11-057#7773@base` | `EX11-057` | 7773 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Tamer/EX11_057.asset` |
| `EX11-058#7774@base` | `EX11-058` | 7774 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Tamer/EX11_058.asset` |
| `EX11-058#7775@P1` | `EX11-058` | 7775 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Tamer/EX11_058_P1.asset` |
| `EX11-060#7778@base` | `EX11-060` | 7778 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Tamer/EX11_060.asset` |
| `EX11-060#7779@P1` | `EX11-060` | 7779 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Tamer/EX11_060_P1.asset` |
| `EX11-061#7780@base` | `EX11-061` | 7780 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Tamer/EX11_061.asset` |
| `EX11-062#7781@base` | `EX11-062` | 7781 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Tamer/EX11_062.asset` |
| `EX11-062#7782@P1` | `EX11-062` | 7782 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Tamer/EX11_062_P1.asset` |
| `EX11-065#7785@base` | `EX11-065` | 7785 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Tamer/EX11_065.asset` |
| `EX11-065#7786@P1` | `EX11-065` | 7786 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Tamer/EX11_065_P1.asset` |

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
