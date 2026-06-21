# C0172_attack_security_timing - attack/security timing card porting 45

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0172_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_007` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_007.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `P_011` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_011.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `P_041` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_041.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous` | `-` | 4 |
| `P_047` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_047.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |
| `P_049` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_049.cs` | `OnBlockAnyone, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 1 |
| `P_050` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_050.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `P_052` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_052.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `P_053` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_053.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `P_058` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_058.cs` | `None` | `static_or_continuous` | `SelectAttackTarget` | 2 |
| `P_065` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_065.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-007#10336@P1` | `P-007` | 10336 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_007_P1.asset` |
| `P-007#6016@base` | `P-007` | 6016 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_007.asset` |
| `P-011#10338@P1` | `P-011` | 10338 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_011_P1.asset` |
| `P-011#6029@base` | `P-011` | 6029 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_011.asset` |
| `P-041#6079@base` | `P-041` | 6079 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_041.asset` |
| `P-041#6080@P1` | `P-041` | 6080 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_041_P1.asset` |
| `P-041#6081@P2` | `P-041` | 6081 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_041_P2.asset` |
| `P-041#9223@P3` | `P-041` | 9223 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_041_P3.asset` |
| `P-047#10358@P1` | `P-047` | 10358 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_047_P1.asset` |
| `P-047#6089@base` | `P-047` | 6089 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_047.asset` |
| `P-049#6091@base` | `P-049` | 6091 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_049.asset` |
| `P-050#10365@P1` | `P-050` | 10365 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_050_P1.asset` |
| `P-050#6092@base` | `P-050` | 6092 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_050.asset` |
| `P-052#6094@base` | `P-052` | 6094 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_052.asset` |
| `P-053#6095@base` | `P-053` | 6095 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_053.asset` |
| `P-058#2868@base` | `P-058` | 2868 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Digimon/RB1_006.asset` |
| `P-058#6100@base` | `P-058` | 6100 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_058.asset` |
| `P-065#10374@P1` | `P-065` | 10374 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_065_P1.asset` |
| `P-065#6107@base` | `P-065` | 6107 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_065.asset` |

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
