# C0161_attack_security_timing - attack/security timing card porting 34

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0161_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX11_034` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_034.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectInteger, SelectSecurity` | 2 |
| `EX11_049` | `DCGO/Assets/Scripts/CardEffect/EX11/Purple/EX11_049.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 2 |
| `EX11_052` | `DCGO/Assets/Scripts/CardEffect/EX11/Purple/EX11_052.cs` | `None, OnEndAttack, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `EX11_063` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_063.cs` | `OnEndTurn, OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectAttackTarget` | 1 |
| `EX11_064` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_064.cs` | `OnAllyAttack, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectSecurity` | 1 |
| `EX11_068` | `DCGO/Assets/Scripts/CardEffect/EX11/Purple/EX11_068.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 2 |
| `EX11_069` | `DCGO/Assets/Scripts/CardEffect/EX11/Purple/EX11_069.cs` | `OnAllyAttack, OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 2 |
| `EX1_002` | `DCGO/Assets/Scripts/CardEffect/EX1/Red/EX1_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `EX1_003` | `DCGO/Assets/Scripts/CardEffect/EX1/Red/EX1_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `EX1_006` | `DCGO/Assets/Scripts/CardEffect/EX1/Red/EX1_006.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX1-002#1261@base` | `EX1-002` | 1261 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_002.asset` |
| `EX1-002#1262@P1` | `EX1-002` | 1262 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_002_P1.asset` |
| `EX1-003#1263@base` | `EX1-003` | 1263 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_003.asset` |
| `EX1-006#1268@base` | `EX1-006` | 1268 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_006.asset` |
| `EX1-006#1269@P1` | `EX1-006` | 1269 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_006_P1.asset` |
| `EX11-034#7726@base` | `EX11-034` | 7726 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_034.asset` |
| `EX11-034#7727@P1` | `EX11-034` | 7727 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_034_P1.asset` |
| `EX11-049#7758@base` | `EX11-049` | 7758 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_049.asset` |
| `EX11-049#7759@P1` | `EX11-049` | 7759 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_049_P1.asset` |
| `EX11-052#7765@base` | `EX11-052` | 7765 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_052.asset` |
| `EX11-052#7766@P1` | `EX11-052` | 7766 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_052_P1.asset` |
| `EX11-063#7783@base` | `EX11-063` | 7783 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Tamer/EX11_063.asset` |
| `EX11-064#7784@base` | `EX11-064` | 7784 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Tamer/EX11_064.asset` |
| `EX11-068#7791@base` | `EX11-068` | 7791 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Tamer/EX11_068.asset` |
| `EX11-068#7792@P1` | `EX11-068` | 7792 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Tamer/EX11_068_P1.asset` |
| `EX11-069#7793@base` | `EX11-069` | 7793 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Tamer/EX11_069.asset` |
| `EX11-069#7794@P1` | `EX11-069` | 7794 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Tamer/EX11_069_P1.asset` |

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
