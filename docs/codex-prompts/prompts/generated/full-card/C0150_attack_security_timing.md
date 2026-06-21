# C0150_attack_security_timing - attack/security timing card porting 23

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0150_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT2_033` | `DCGO/Assets/Scripts/CardEffect/BT2/Yellow/BT2_033.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `BT2_035` | `DCGO/Assets/Scripts/CardEffect/BT2/Yellow/BT2_035.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 4 |
| `BT2_039` | `DCGO/Assets/Scripts/CardEffect/BT2/Yellow/BT2_039.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |
| `BT2_049` | `DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_049.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `BT2_051` | `DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_051.cs` | `None, OnEndBattle` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget` | 3 |
| `BT2_078` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_078.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT2_081` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_081.cs` | `OnAllyAttack` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT2_084` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_084.cs` | `OnAllyAttack, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 3 |
| `BT2_112` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_112.cs` | `None, OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 3 |
| `BT3_001` | `DCGO/Assets/Scripts/CardEffect/BT3/Red/BT3_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-033#418@base` | `BT2-033` | 418 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_033.asset` |
| `BT2-033#419@P1` | `BT2-033` | 419 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_033_P1.asset` |
| `BT2-035#422@base` | `BT2-035` | 422 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_035.asset` |
| `BT2-035#423@P1` | `BT2-035` | 423 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_035_P1.asset` |
| `BT2-035#424@P2` | `BT2-035` | 424 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_035_P2.asset` |
| `BT2-035#425@P3` | `BT2-035` | 425 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_035_P3.asset` |
| `BT2-039#433@base` | `BT2-039` | 433 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_039.asset` |
| `BT2-039#8311@P1` | `BT2-039` | 8311 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_039_P1.asset` |
| `BT2-049#447@base` | `BT2-049` | 447 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_049.asset` |
| `BT2-049#448@P1` | `BT2-049` | 448 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_049_P1.asset` |
| `BT2-051#450@base` | `BT2-051` | 450 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_051.asset` |
| `BT2-051#451@P1` | `BT2-051` | 451 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_051_P1.asset` |
| `BT2-051#452@P2` | `BT2-051` | 452 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_051_P2.asset` |
| `BT2-078#509@base` | `BT2-078` | 509 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_078.asset` |
| `BT2-078#510@P1` | `BT2-078` | 510 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_078_P1.asset` |
| `BT2-081#514@base` | `BT2-081` | 514 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_081.asset` |
| `BT2-081#515@P1` | `BT2-081` | 515 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_081_P1.asset` |
| `BT2-084#520@base` | `BT2-084` | 520 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Tamer/BT2_084.asset` |
| `BT2-084#521@P1` | `BT2-084` | 521 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Tamer/BT2_084_P1.asset` |
| `BT2-084#522@P2` | `BT2-084` | 522 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Tamer/BT2_084_P2.asset` |
| `BT2-112#561@base` | `BT2-112` | 561 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_112.asset` |
| `BT2-112#562@P1` | `BT2-112` | 562 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_112_P1.asset` |
| `BT2-112#8324@P2` | `BT2-112` | 8324 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_112_P2.asset` |
| `BT3-001#587@base` | `BT3-001` | 587 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/DigiEgg/BT3_001.asset` |

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
