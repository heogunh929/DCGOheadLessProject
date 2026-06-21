# C0139_attack_security_timing - attack/security timing card porting 12

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0139_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 27
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT19_072` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_072.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `BT19_082` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_082.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 3 |
| `BT1_001` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT1_003` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 6 |
| `BT1_006` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_006.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |
| `BT1_007` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_007.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `BT1_012` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_012.cs` | `OnBlockAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT1_021` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_021.cs` | `OnAllyAttack, OnEndTurn` | `max_count_per_turn, static_or_continuous` | `-` | 7 |
| `BT1_022` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_022.cs` | `OnBlockAnyone, OnDetermineDoSecurityCheck` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT1_039` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_039.cs` | `OnAllyAttack` | `max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectHand` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-001#133@base` | `BT1-001` | 133 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/DigiEgg/BT1_001.asset` |
| `BT1-003#135@base` | `BT1-003` | 135 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/DigiEgg/BT1_003.asset` |
| `BT1-003#136@P1` | `BT1-003` | 136 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_003_P1.asset` |
| `BT1-003#137@P2` | `BT1-003` | 137 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_003_P2.asset` |
| `BT1-003#4255@P1` | `BT1-003` | 4255 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/DigiEgg/BT1_003_P1.asset` |
| `BT1-003#4256@P2` | `BT1-003` | 4256 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/DigiEgg/BT1_003_P2.asset` |
| `BT1-003#4257@P3` | `BT1-003` | 4257 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/DigiEgg/BT1_003_P3.asset` |
| `BT1-006#140@base` | `BT1-006` | 140 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_006.asset` |
| `BT1-006#4258@P1` | `BT1-006` | 4258 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/DigiEgg/BT1_006_P1.asset` |
| `BT1-007#141@base` | `BT1-007` | 141 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/DigiEgg/BT1_007.asset` |
| `BT1-007#4259@P1` | `BT1-007` | 4259 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/DigiEgg/BT1_007_P1.asset` |
| `BT1-012#150@base` | `BT1-012` | 150 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_012.asset` |
| `BT1-021#162@base` | `BT1-021` | 162 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_021.asset` |
| `BT1-021#4262@P1` | `BT1-021` | 4262 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_021_P1.asset` |
| `BT1-021#4263@P2` | `BT1-021` | 4263 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_021_P2.asset` |
| `BT1-021#4264@P3` | `BT1-021` | 4264 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_021_P3.asset` |
| `BT1-022#163@base` | `BT1-022` | 163 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_022.asset` |
| `BT1-039#193@base` | `BT1-039` | 193 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_039.asset` |
| `BT1-040#194@base` | `BT1-040` | 194 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_040.asset` |
| `BT1-058#216@base` | `BT1-058` | 216 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_058.asset` |
| `BT1-075#246@base` | `BT1-075` | 246 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_075.asset` |
| `BT19-072#4012@base` | `BT19-072` | 4012 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_072.asset` |
| `BT19-072#8288@P1` | `BT19-072` | 8288 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_072_P1.asset` |
| `BT19-072#8289@P2` | `BT19-072` | 8289 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_072_P2.asset` |
| `BT19-082#4019@base` | `BT19-082` | 4019 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Tamer/BT19_082.asset` |
| `BT19-082#4020@P1` | `BT19-082` | 4020 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Tamer/BT19_082_P1.asset` |
| `BT19-082#8293@P2` | `BT19-082` | 8293 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Tamer/BT19_082_P2.asset` |

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
