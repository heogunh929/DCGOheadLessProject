# C0159_attack_security_timing - attack/security timing card porting 32

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0159_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT8_112` | `DCGO/Assets/Scripts/CardEffect/BT8/White/BT8_112.cs` | `BeforePayCost, None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectOrder` | 5 |
| `BT9_006` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_006.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 4 |
| `BT9_025` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_025.cs` | `OnEndAttack` | `max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectHand` | 2 |
| `BT9_061` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_061.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT9_065` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_065.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT9_071` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_071.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT9_073` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_073.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |
| `BT9_077` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_077.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 1 |
| `BT9_079` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_079.cs` | `OnEndAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT9_084` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_084.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT8-112#1706@base` | `BT8-112` | 1706 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Digimon/BT8_112.asset` |
| `BT8-112#1708@P2` | `BT8-112` | 1708 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Digimon/BT8_112_P2.asset` |
| `BT8-112#1709@P4` | `BT8-112` | 1709 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Digimon/BT8_112_P4.asset` |
| `BT8-112#3434@P1` | `BT8-112` | 3434 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Digimon/BT8_112_P1.asset` |
| `BT8-112#8935@P3` | `BT8-112` | 8935 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Digimon/BT8_112_P3.asset` |
| `BT9-006#1786@base` | `BT9-006` | 1786 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/DigiEgg/BT9_006.asset` |
| `BT9-006#1787@P1` | `BT9-006` | 1787 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_006_P1.asset` |
| `BT9-006#8947@P1` | `BT9-006` | 8947 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/DigiEgg/BT9_006_P1.asset` |
| `BT9-006#8948@P0` | `BT9-006` | 8948 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/DigiEgg/BT9_006_P0.asset` |
| `BT9-025#1811@base` | `BT9-025` | 1811 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_025.asset` |
| `BT9-025#8962@P1` | `BT9-025` | 8962 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_025_P1.asset` |
| `BT9-061#1851@base` | `BT9-061` | 1851 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_061.asset` |
| `BT9-065#1856@base` | `BT9-065` | 1856 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_065.asset` |
| `BT9-065#8985@P0` | `BT9-065` | 8985 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_065_P0.asset` |
| `BT9-071#1864@base` | `BT9-071` | 1864 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_071.asset` |
| `BT9-071#8987@P0` | `BT9-071` | 8987 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_071_P0.asset` |
| `BT9-073#1866@base` | `BT9-073` | 1866 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_073.asset` |
| `BT9-077#1871@base` | `BT9-077` | 1871 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_077.asset` |
| `BT9-079#1873@base` | `BT9-079` | 1873 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_079.asset` |
| `BT9-079#8991@P0` | `BT9-079` | 8991 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_079_P0.asset` |
| `BT9-084#1882@base` | `BT9-084` | 1882 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Tamer/BT9_084.asset` |
| `BT9-084#8998@P0` | `BT9-084` | 8998 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Tamer/BT9_084_P0.asset` |

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
