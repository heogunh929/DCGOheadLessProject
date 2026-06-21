# C0133_attack_security_timing - attack/security timing card porting 6

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0133_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT14_015` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_015.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT14_016` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_016.cs` | `OnAllyAttack` | `inherited` | `-` | 1 |
| `BT14_019` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_019.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT14_022` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_022.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT14_023` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_023.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT14_029` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_029.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `BT14_031` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_031.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT14_036` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_036.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT14_053` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_053.cs` | `OnAllyAttack, OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT14_054` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_054.cs` | `OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT14-015#2934@base` | `BT14-015` | 2934 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_015.asset` |
| `BT14-016#2935@base` | `BT14-016` | 2935 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_016.asset` |
| `BT14-019#2938@base` | `BT14-019` | 2938 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_019.asset` |
| `BT14-022#2942@base` | `BT14-022` | 2942 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_022.asset` |
| `BT14-023#2943@base` | `BT14-023` | 2943 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_023.asset` |
| `BT14-023#4644@P0` | `BT14-023` | 4644 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_023_P0.asset` |
| `BT14-024#2944@base` | `BT14-024` | 2944 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_024.asset` |
| `BT14-029#2950@base` | `BT14-029` | 2950 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_029.asset` |
| `BT14-029#4648@P0` | `BT14-029` | 4648 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_029_P0.asset` |
| `BT14-031#2952@base` | `BT14-031` | 2952 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_031.asset` |
| `BT14-036#2958@base` | `BT14-036` | 2958 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_036.asset` |
| `BT14-036#4651@P0` | `BT14-036` | 4651 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_036_P0.asset` |
| `BT14-053#2978@base` | `BT14-053` | 2978 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_053.asset` |
| `BT14-053#4668@P0` | `BT14-053` | 4668 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_053_P0.asset` |
| `BT14-054#2979@base` | `BT14-054` | 2979 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_054.asset` |
| `BT14-054#4669@P0` | `BT14-054` | 4669 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_054_P0.asset` |

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
