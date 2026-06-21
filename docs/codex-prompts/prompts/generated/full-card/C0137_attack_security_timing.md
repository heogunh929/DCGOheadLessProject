# C0137_attack_security_timing - attack/security timing card porting 10

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0137_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT17_060` | `DCGO/Assets/Scripts/CardEffect/BT17/Black/BT17_060.cs` | `BeforePayCost, None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget` | 2 |
| `BT17_070` | `DCGO/Assets/Scripts/CardEffect/BT17/Purple/BT17_070.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean` | 2 |
| `BT18_001` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT18_003` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT18_016` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_016.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 1 |
| `BT18_027` | `DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_027.cs` | `OnAllyAttack` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `BT18_032` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_032.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT18_035` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_035.cs` | `OnAllyAttack, SecuritySkill` | `inherited, max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT18_069` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_069.cs` | `None, OnEndTurn` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget` | 1 |
| `BT18_089` | `DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_089.cs` | `OnAllyAttack, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT17-060#3609@base` | `BT17-060` | 3609 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Black/Digimon/BT17_060.asset` |
| `BT17-060#3610@P1` | `BT17-060` | 3610 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Black/Digimon/BT17_060_P1.asset` |
| `BT17-070#3620@base` | `BT17-070` | 3620 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_070.asset` |
| `BT17-070#4860@P0` | `BT17-070` | 4860 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_070_P0.asset` |
| `BT18-001#3852@base` | `BT18-001` | 3852 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/DigiEgg/BT18_001.asset` |
| `BT18-003#3856@base` | `BT18-003` | 3856 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/DigiEgg/BT18_003.asset` |
| `BT18-016#3868@base` | `BT18-016` | 3868 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_016.asset` |
| `BT18-027#3878@base` | `BT18-027` | 3878 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_027.asset` |
| `BT18-032#3886@base` | `BT18-032` | 3886 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_032.asset` |
| `BT18-035#3890@base` | `BT18-035` | 3890 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_035.asset` |
| `BT18-069#3920@base` | `BT18-069` | 3920 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_069.asset` |
| `BT18-089#3942@base` | `BT18-089` | 3942 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Tamer/BT18_089.asset` |
| `BT18-089#3943@P1` | `BT18-089` | 3943 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Tamer/BT18_089_P1.asset` |

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
