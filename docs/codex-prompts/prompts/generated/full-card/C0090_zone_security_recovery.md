# C0090_zone_security_recovery - zone/security/recovery card porting 84

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0090_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_102` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_102.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `P_122` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_122.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 3 |
| `P_123` | `DCGO/Assets/Scripts/CardEffect/P/White/P_123.cs` | `OnMove` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectBoolean` | 3 |
| `P_130` | `DCGO/Assets/Scripts/CardEffect/P/White/P_130.cs` | `OnEnterFieldAnyone, OnMove, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `-` | 2 |
| `P_131` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_131.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `P_151` | `DCGO/Assets/Scripts/CardEffect/P/White/P_151.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 5 |
| `P_156` | `DCGO/Assets/Scripts/CardEffect/P/White/P_156.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity` | 4 |
| `P_162` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_162.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `P_163` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_163.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `P_165_token` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_165_token.cs` | `OnDestroyedAnyone, OnEndTurn` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 0 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-102#6148@base` | `P-102` | 6148 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_102.asset` |
| `P-122#10427@P1` | `P-122` | 10427 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_122_P1.asset` |
| `P-122#10428@P2` | `P-122` | 10428 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_122_P2.asset` |
| `P-122#6168@base` | `P-122` | 6168 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_122.asset` |
| `P-123#10423@P2` | `P-123` | 10423 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Digimon/P_123_P2.asset` |
| `P-123#10424@P3` | `P-123` | 10424 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Digimon/P_123_P3.asset` |
| `P-123#6169@base` | `P-123` | 6169 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Digimon/P_123.asset` |
| `P-130#10431@P2` | `P-130` | 10431 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Tamer/P_130_P2.asset` |
| `P-130#6176@base` | `P-130` | 6176 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Tamer/P_130.asset` |
| `P-131#10285@base` | `P-131` | 10285 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_131.asset` |
| `P-131#9244@P2` | `P-131` | 9244 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_131_P2.asset` |
| `P-151#10314@base` | `P-151` | 10314 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_151.asset` |
| `P-151#10315@P1` | `P-151` | 10315 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_151_P1.asset` |
| `P-151#10433@P3` | `P-151` | 10433 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_151_P3.asset` |
| `P-151#9268@P4` | `P-151` | 9268 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_151_P4.asset` |
| `P-151#9269@P5` | `P-151` | 9269 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_151_P5.asset` |
| `P-156#10458@base` | `P-156` | 10458 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_156.asset` |
| `P-156#10459@P1` | `P-156` | 10459 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_156_P1.asset` |
| `P-156#10460@P2` | `P-156` | 10460 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_156_P2.asset` |
| `P-156#9270@P3` | `P-156` | 9270 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_156_P3.asset` |
| `P-162#10434@base` | `P-162` | 10434 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_162.asset` |
| `P-162#9273@P1` | `P-162` | 9273 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_162_P1.asset` |
| `P-163#10444@base` | `P-163` | 10444 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_163.asset` |
| `P-163#9274@P1` | `P-163` | 9274 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_163_P1.asset` |

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
