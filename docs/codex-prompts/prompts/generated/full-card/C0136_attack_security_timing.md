# C0136_attack_security_timing - attack/security timing card porting 9

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0136_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT16_059` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_059.cs` | `None, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT16_074` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_074.cs` | `None, OnEndAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity` | 2 |
| `BT16_081` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_081.cs` | `OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT17_001` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT17_016` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_016.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `BT17_020` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_020.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 1 |
| `BT17_029` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_029.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT17_033` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_033.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT17_037` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_037.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |
| `BT17_058` | `DCGO/Assets/Scripts/CardEffect/BT17/Black/BT17_058.cs` | `OnDetermineDoSecurityCheck, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT16-059#3376@base` | `BT16-059` | 3376 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_059.asset` |
| `BT16-074#3392@base` | `BT16-074` | 3392 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_074.asset` |
| `BT16-074#4806@P0` | `BT16-074` | 4806 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_074_P0.asset` |
| `BT16-081#3401@base` | `BT16-081` | 3401 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_081.asset` |
| `BT16-081#3402@P1` | `BT16-081` | 3402 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_081_P1.asset` |
| `BT17-001#3541@base` | `BT17-001` | 3541 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/DigiEgg/BT17_001.asset` |
| `BT17-001#4833@P0` | `BT17-001` | 4833 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/DigiEgg/BT17_001_P0.asset` |
| `BT17-016#3556@base` | `BT17-016` | 3556 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_016.asset` |
| `BT17-016#4845@P0` | `BT17-016` | 4845 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_016_P0.asset` |
| `BT17-016#8219@P1` | `BT17-016` | 8219 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_016_P1.asset` |
| `BT17-020#3563@base` | `BT17-020` | 3563 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_020.asset` |
| `BT17-029#3573@base` | `BT17-029` | 3573 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_029.asset` |
| `BT17-033#3578@base` | `BT17-033` | 3578 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_033.asset` |
| `BT17-037#3582@base` | `BT17-037` | 3582 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_037.asset` |
| `BT17-058#3607@base` | `BT17-058` | 3607 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Black/Digimon/BT17_058.asset` |

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
