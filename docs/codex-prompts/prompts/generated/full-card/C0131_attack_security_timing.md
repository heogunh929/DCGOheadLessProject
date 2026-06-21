# C0131_attack_security_timing - attack/security timing card porting 4

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0131_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT12_041` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_041.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT12_057` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_057.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |
| `BT12_071` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_071.cs` | `OnAllyAttack, OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT12_077` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_077.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `BT12_083` | `DCGO/Assets/Scripts/CardEffect/BT12/Purple/BT12_083.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 2 |
| `BT13_005` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_005.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT13_021` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_021.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT13_026` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_026.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT13_027` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_027.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `BT13_029` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_029.cs` | `OnAddHand, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectAttackTarget` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT12-041#2451@base` | `BT12-041` | 2451 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_041.asset` |
| `BT12-041#2452@P1` | `BT12-041` | 2452 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_041_P1.asset` |
| `BT12-057#2470@base` | `BT12-057` | 2470 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_057.asset` |
| `BT12-057#2471@P1` | `BT12-057` | 2471 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_057_P1.asset` |
| `BT12-057#4511@P2` | `BT12-057` | 4511 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_057_P2.asset` |
| `BT12-071#2487@base` | `BT12-071` | 2487 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_071.asset` |
| `BT12-071#4522@P0` | `BT12-071` | 4522 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Digimon/BT12_071_P0.asset` |
| `BT12-077#2493@base` | `BT12-077` | 2493 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_077.asset` |
| `BT12-077#2494@P1` | `BT12-077` | 2494 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_077_P1.asset` |
| `BT12-077#4528@P0` | `BT12-077` | 4528 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_077_P0.asset` |
| `BT12-083#2501@base` | `BT12-083` | 2501 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_083.asset` |
| `BT12-083#2502@P1` | `BT12-083` | 2502 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Purple/Digimon/BT12_083_P1.asset` |
| `BT13-005#2647@base` | `BT13-005` | 2647 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/DigiEgg/BT13_005.asset` |
| `BT13-005#4557@P0` | `BT13-005` | 4557 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Black/DigiEgg/BT13_005_P0.asset` |
| `BT13-021#2668@base` | `BT13-021` | 2668 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_021.asset` |
| `BT13-026#2673@base` | `BT13-026` | 2673 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_026.asset` |
| `BT13-027#2674@base` | `BT13-027` | 2674 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_027.asset` |
| `BT13-029#2676@base` | `BT13-029` | 2676 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_029.asset` |
| `BT13-029#4574@P0` | `BT13-029` | 4574 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_029_P0.asset` |

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
