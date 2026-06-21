# C0181_attack_security_timing - attack/security timing card porting 54

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0181_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 27
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST2_11` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_11.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous` | `-` | 3 |
| `ST3_05` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_05.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity` | 3 |
| `ST3_08` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_08.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 7 |
| `ST3_11` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_11.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `ST4_04` | `DCGO/Assets/Scripts/CardEffect/ST4/Green/ST4_04.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `ST6_03` | `DCGO/Assets/Scripts/CardEffect/ST6/Purple/ST6_03.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 2 |
| `ST7_02` | `DCGO/Assets/Scripts/CardEffect/ST7/Red/ST7_02.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `ST7_08` | `DCGO/Assets/Scripts/CardEffect/ST7/Red/ST7_08.cs` | `OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `ST7_09` | `DCGO/Assets/Scripts/CardEffect/ST7/Red/ST7_09.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |
| `ST8_05` | `DCGO/Assets/Scripts/CardEffect/ST8/Blue/ST8_05.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT3-033#641@base` | `BT3-033` | 641 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_033.asset` |
| `BT3-035#643@base` | `BT3-035` | 643 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_035.asset` |
| `BT3-035#644@P1` | `BT3-035` | 644 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_035_P1.asset` |
| `BT8-037#1603@base` | `BT8-037` | 1603 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_037.asset` |
| `ST2-11#62@base` | `ST2-11` | 62 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_11.asset` |
| `ST2-11#63@P1` | `ST2-11` | 63 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_11_P1.asset` |
| `ST2-11#64@P2` | `ST2-11` | 64 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_11_P2.asset` |
| `ST3-05#83@base` | `ST3-05` | 83 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_05.asset` |
| `ST3-05#84@P1` | `ST3-05` | 84 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_05_P1.asset` |
| `ST3-05#85@P2` | `ST3-05` | 85 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_05_P2.asset` |
| `ST3-08#90@base` | `ST3-08` | 90 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_08.asset` |
| `ST3-08#91@P1` | `ST3-08` | 91 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_08_P1.asset` |
| `ST3-08#92@P2` | `ST3-08` | 92 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_08_P2.asset` |
| `ST3-11#96@base` | `ST3-11` | 96 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_11.asset` |
| `ST4-04#112@base` | `ST4-04` | 112 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_04.asset` |
| `ST4-04#113@P1` | `ST4-04` | 113 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_04_P1.asset` |
| `ST4-06#115@base` | `ST4-06` | 115 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_06.asset` |
| `ST6-03#342@base` | `ST6-03` | 342 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_03.asset` |
| `ST6-06#345@base` | `ST6-06` | 345 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_06.asset` |
| `ST7-02#564@base` | `ST7-02` | 564 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_02.asset` |
| `ST7-02#565@P1` | `ST7-02` | 565 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_02_P1.asset` |
| `ST7-08#580@base` | `ST7-08` | 580 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_08.asset` |
| `ST7-08#581@P1` | `ST7-08` | 581 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_08_P1.asset` |
| `ST7-09#4993@P1` | `ST7-09` | 4993 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_09_P1.asset` |
| `ST7-09#4994@P2` | `ST7-09` | 4994 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_09_P2.asset` |
| `ST7-09#582@base` | `ST7-09` | 582 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_09.asset` |
| `ST8-05#1719@base` | `ST8-05` | 1719 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_05.asset` |

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
