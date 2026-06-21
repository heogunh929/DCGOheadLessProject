# C0076_zone_security_recovery - zone/security/recovery card porting 70

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0076_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX4_002` | `DCGO/Assets/Scripts/CardEffect/EX4/Green/EX4_002.cs` | `OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `EX4_003` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_003.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `EX4_012` | `DCGO/Assets/Scripts/CardEffect/EX4/Red/EX4_012.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX4_015` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_015.cs` | `OnAddHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `-` | 1 |
| `EX4_017` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_017.cs` | `OnAddHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `EX4_022` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_022.cs` | `OnAddHand, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX4_023` | `DCGO/Assets/Scripts/CardEffect/EX4/Yellow/EX4_023.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 2 |
| `EX4_041` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_041.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 1 |
| `EX4_043` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_043.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `EX4_044` | `DCGO/Assets/Scripts/CardEffect/EX4/Black/EX4_044.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX4-002#2545@base` | `EX4-002` | 2545 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/DigiEgg/EX4_002.asset` |
| `EX4-002#3295@P1` | `EX4-002` | 3295 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/DigiEgg/EX4_002_P1.asset` |
| `EX4-002#4240@P0` | `EX4-002` | 4240 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Green/DigiEgg/EX4_002_P0.asset` |
| `EX4-003#2546@base` | `EX4-003` | 2546 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/DigiEgg/EX4_003.asset` |
| `EX4-003#9136@P1` | `EX4-003` | 9136 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/DigiEgg/EX4_003_P1.asset` |
| `EX4-012#2556@base` | `EX4-012` | 2556 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_012.asset` |
| `EX4-012#2557@P1` | `EX4-012` | 2557 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_012_P1.asset` |
| `EX4-015#2561@base` | `EX4-015` | 2561 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_015.asset` |
| `EX4-017#2563@base` | `EX4-017` | 2563 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_017.asset` |
| `EX4-022#2569@base` | `EX4-022` | 2569 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_022.asset` |
| `EX4-022#2570@P1` | `EX4-022` | 2570 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/Digimon/EX4_022_P1.asset` |
| `EX4-023#2571@base` | `EX4-023` | 2571 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_023.asset` |
| `EX4-023#4241@P1` | `EX4-023` | 4241 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Yellow/Digimon/EX4_023_P1.asset` |
| `EX4-041#2593@base` | `EX4-041` | 2593 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_041.asset` |
| `EX4-043#2595@base` | `EX4-043` | 2595 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_043.asset` |
| `EX4-044#2596@base` | `EX4-044` | 2596 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Black/Digimon/EX4_044.asset` |

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
