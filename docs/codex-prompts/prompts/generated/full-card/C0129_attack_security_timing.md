# C0129_attack_security_timing - attack/security timing card porting 2

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0129_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT10_070` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_070.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT10_072` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_072.cs` | `OnAllyAttack, OnDestroyedAnyone, OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT10_082` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_082.cs` | `None, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectBoolean` | 3 |
| `BT11_002` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT11_008` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_008.cs` | `OnAttackTargetChanged` | `inherited, max_count_per_turn, static_or_continuous` | `SelectAttackTarget` | 1 |
| `BT11_010` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_010.cs` | `OnAllyAttack, OnAttackTargetChanged` | `inherited, max_count_per_turn, static_or_continuous` | `SelectAttackTarget` | 1 |
| `BT11_014` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_014.cs` | `OnAllyAttack, OnAttackTargetChanged` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectAttackTarget` | 2 |
| `BT11_017` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_017.cs` | `OnAllyAttack, OnAttackTargetChanged, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectAttackTarget` | 2 |
| `BT11_025` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_025.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT11_056` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_056.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectOrder` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT10-070#2116@base` | `BT10-070` | 2116 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_070.asset` |
| `BT10-070#4334@P0` | `BT10-070` | 4334 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Digimon/BT10_070_P0.asset` |
| `BT10-072#2118@base` | `BT10-072` | 2118 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_072.asset` |
| `BT10-082#2128@base` | `BT10-082` | 2128 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_082.asset` |
| `BT10-082#2129@P1` | `BT10-082` | 2129 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_082_P1.asset` |
| `BT10-082#4340@P0` | `BT10-082` | 4340 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_082_P0.asset` |
| `BT11-002#2269@base` | `BT11-002` | 2269 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/DigiEgg/BT11_002.asset` |
| `BT11-008#2275@base` | `BT11-008` | 2275 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_008.asset` |
| `BT11-010#2277@base` | `BT11-010` | 2277 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_010.asset` |
| `BT11-014#2281@base` | `BT11-014` | 2281 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_014.asset` |
| `BT11-014#4375@P0` | `BT11-014` | 4375 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_014_P0.asset` |
| `BT11-017#2285@base` | `BT11-017` | 2285 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_017.asset` |
| `BT11-017#2286@P1` | `BT11-017` | 2286 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_017_P1.asset` |
| `BT11-025#2295@base` | `BT11-025` | 2295 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_025.asset` |
| `BT11-056#2329@base` | `BT11-056` | 2329 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_056.asset` |
| `BT11-056#2330@P1` | `BT11-056` | 2330 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_056_P1.asset` |

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
