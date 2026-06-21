# C0140_attack_security_timing - attack/security timing card porting 13

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0140_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT1_040` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_040.cs` | `OnAllyAttack, OnEndTurn` | `max_count_per_turn, static_or_continuous` | `-` | 0 |
| `BT1_041` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_041.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `-` | 1 |
| `BT1_044` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_044.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `BT1_046` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_046.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT1_054` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_054.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT1_066` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_066.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |
| `BT1_076` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_076.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 4 |
| `BT1_078` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_078.cs` | `OnAllyAttack` | `max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard` | 3 |
| `BT1_079` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_079.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT1_081` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_081.cs` | `OnDetermineDoSecurityCheck, OnEndAttack` | `inherited, max_count_per_turn, optional, static_or_continuous` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-041#195@base` | `BT1-041` | 195 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_041.asset` |
| `BT1-044#198@base` | `BT1-044` | 198 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_044.asset` |
| `BT1-044#199@P1` | `BT1-044` | 199 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_044_P1.asset` |
| `BT1-046#202@base` | `BT1-046` | 202 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_046.asset` |
| `BT1-054#211@base` | `BT1-054` | 211 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_054.asset` |
| `BT1-066#231@base` | `BT1-066` | 231 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_066.asset` |
| `BT1-066#232@P1` | `BT1-066` | 232 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_066_P1.asset` |
| `BT1-066#233@P2` | `BT1-066` | 233 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_066_P2.asset` |
| `BT1-076#247@base` | `BT1-076` | 247 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_076.asset` |
| `BT1-076#248@P1` | `BT1-076` | 248 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_076_P1.asset` |
| `BT1-076#249@P2` | `BT1-076` | 249 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_076_P2.asset` |
| `BT1-076#4273@P3` | `BT1-076` | 4273 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_076_P3.asset` |
| `BT1-078#253@base` | `BT1-078` | 253 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_078.asset` |
| `BT1-078#254@P1` | `BT1-078` | 254 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_078_P1.asset` |
| `BT1-079#255@base` | `BT1-079` | 255 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_079.asset` |
| `BT1-079#256@P1` | `BT1-079` | 256 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_079_P1.asset` |
| `BT1-081#258@base` | `BT1-081` | 258 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_081.asset` |
| `BT7-049#1455@base` | `BT7-049` | 1455 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_049.asset` |

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
