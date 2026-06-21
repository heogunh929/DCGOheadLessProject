# C0132_attack_security_timing - attack/security timing card porting 5

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0132_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT13_032` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_032.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `BT13_034` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_034.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 3 |
| `BT13_036` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_036.cs` | `OnAllyAttack, OnLoseSecurity` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT13_037` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_037.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT13_038` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_038.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT13_046` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_046.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `BT13_077` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_077.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 2 |
| `BT13_111` | `DCGO/Assets/Scripts/CardEffect/BT13/Red/BT13_111.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 5 |
| `BT14_005` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_005.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 3 |
| `BT14_008` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_008.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT13-032#2680@base` | `BT13-032` | 2680 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_032.asset` |
| `BT13-032#4577@P0` | `BT13-032` | 4577 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_032_P0.asset` |
| `BT13-034#2684@base` | `BT13-034` | 2684 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_034.asset` |
| `BT13-034#4578@P0` | `BT13-034` | 4578 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_034_P0.asset` |
| `BT13-034#4579@P1` | `BT13-034` | 4579 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_034_P1.asset` |
| `BT13-036#2686@base` | `BT13-036` | 2686 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_036.asset` |
| `BT13-037#2687@base` | `BT13-037` | 2687 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_037.asset` |
| `BT13-038#2688@base` | `BT13-038` | 2688 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_038.asset` |
| `BT13-046#2697@base` | `BT13-046` | 2697 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_046.asset` |
| `BT13-046#2698@P1` | `BT13-046` | 2698 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Digimon/BT13_046_P1.asset` |
| `BT13-077#2735@base` | `BT13-077` | 2735 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_077.asset` |
| `BT13-077#2736@P1` | `BT13-077` | 2736 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_077_P1.asset` |
| `BT13-111#2781@base` | `BT13-111` | 2781 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_111.asset` |
| `BT13-111#2782@P1` | `BT13-111` | 2782 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_111_P1.asset` |
| `BT13-111#8157@P2` | `BT13-111` | 8157 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_111_P2.asset` |
| `BT13-111#8158@P3` | `BT13-111` | 8158 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_111_P3.asset` |
| `BT13-111#8159@P4` | `BT13-111` | 8159 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Red/Digimon/BT13_111_P4.asset` |
| `BT14-005#2920@base` | `BT14-005` | 2920 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/DigiEgg/BT14_005.asset` |
| `BT14-005#2921@P1` | `BT14-005` | 2921 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/DigiEgg/BT14_005_P1.asset` |
| `BT14-005#4636@P0` | `BT14-005` | 4636 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/DigiEgg/BT14_005_P0.asset` |
| `BT14-008#2926@base` | `BT14-008` | 2926 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_008.asset` |

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
