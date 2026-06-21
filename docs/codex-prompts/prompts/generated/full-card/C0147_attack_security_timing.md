# C0147_attack_security_timing - attack/security timing card porting 20

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0147_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT24_031` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_031.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectBoolean, SelectSecurity` | 2 |
| `BT24_043` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_043.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT24_047` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_047.cs` | `OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget` | 1 |
| `BT24_051` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_051.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget` | 2 |
| `BT24_059` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_059.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT24_061` | `DCGO/Assets/Scripts/CardEffect/BT24/Black/BT24_061.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT24_068` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_068.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 1 |
| `BT24_069` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_069.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, OnMove` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `BT24_070` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_070.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT24_073` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_073.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT24-031#7554@base` | `BT24-031` | 7554 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_031.asset` |
| `BT24-031#7555@P1` | `BT24-031` | 7555 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_031_P1.asset` |
| `BT24-043#7571@base` | `BT24-043` | 7571 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_043.asset` |
| `BT24-047#7576@base` | `BT24-047` | 7576 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_047.asset` |
| `BT24-051#7580@base` | `BT24-051` | 7580 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_051.asset` |
| `BT24-051#7581@P1` | `BT24-051` | 7581 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Digimon/BT24_051_P1.asset` |
| `BT24-059#7590@base` | `BT24-059` | 7590 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_059.asset` |
| `BT24-061#7592@base` | `BT24-061` | 7592 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Black/Digimon/BT24_061.asset` |
| `BT24-068#7601@base` | `BT24-068` | 7601 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_068.asset` |
| `BT24-069#7602@base` | `BT24-069` | 7602 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_069.asset` |
| `BT24-070#7603@base` | `BT24-070` | 7603 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_070.asset` |
| `BT24-073#7606@base` | `BT24-073` | 7606 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Digimon/BT24_073.asset` |

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
