# C0143_attack_security_timing - attack/security timing card porting 16

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0143_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT21_081` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_081.cs` | `OnEndTurn, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget` | 3 |
| `BT21_083` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_083.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectAttackTarget` | 2 |
| `BT21_102` | `DCGO/Assets/Scripts/CardEffect/BT21/White/BT21_102.cs` | `OnAllyAttack, OnDeclaration, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 5 |
| `BT22_010` | `DCGO/Assets/Scripts/CardEffect/BT22/Red/BT22_010.cs` | `None, OnDeclaration` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectAttackTarget` | 2 |
| `BT22_011` | `DCGO/Assets/Scripts/CardEffect/BT22/Red/BT22_011.cs` | `None, OnAllyAttack, OnDeclaration` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectAttackTarget` | 1 |
| `BT22_029` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_029.cs` | `OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `BT22_032` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_032.cs` | `OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `BT22_034` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_034.cs` | `None, OnAllyAttack, OnDiscardSecurity, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectSecurity` | 1 |
| `BT22_060` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_060.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 1 |
| `BT22_074` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_074.cs` | `None, OnDeclaration, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectAttackTarget` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT21-081#5399@base` | `BT21-081` | 5399 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_081.asset` |
| `BT21-081#5400@P1` | `BT21-081` | 5400 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_081_P1.asset` |
| `BT21-081#8413@P2` | `BT21-081` | 8413 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_081_P2.asset` |
| `BT21-083#5403@base` | `BT21-083` | 5403 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_083.asset` |
| `BT21-083#5404@P1` | `BT21-083` | 5404 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_083_P1.asset` |
| `BT21-102#5431@base` | `BT21-102` | 5431 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/White/Tamer/BT21_102.asset` |
| `BT21-102#5432@P1` | `BT21-102` | 5432 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/White/Tamer/BT21_102_P1.asset` |
| `BT21-102#5433@P2` | `BT21-102` | 5433 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/White/Tamer/BT21_102_P2.asset` |
| `BT21-102#8421@P3` | `BT21-102` | 8421 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT21/White/Tamer/BT21_102_P3.asset` |
| `BT21-102#8422@P4` | `BT21-102` | 8422 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT21/White/Tamer/BT21_102_P4.asset` |
| `BT22-010#6999@base` | `BT22-010` | 6999 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Digimon/BT22_010.asset` |
| `BT22-010#8424@P1` | `BT22-010` | 8424 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Digimon/BT22_010_P1.asset` |
| `BT22-011#7001@base` | `BT22-011` | 7001 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Digimon/BT22_011.asset` |
| `BT22-029#7025@base` | `BT22-029` | 7025 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_029.asset` |
| `BT22-029#7026@P1` | `BT22-029` | 7026 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_029_P1.asset` |
| `BT22-032#7029@base` | `BT22-032` | 7029 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_032.asset` |
| `BT22-034#7031@base` | `BT22-034` | 7031 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_034.asset` |
| `BT22-060#7066@base` | `BT22-060` | 7066 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_060.asset` |
| `BT22-074#7081@base` | `BT22-074` | 7081 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_074.asset` |

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
