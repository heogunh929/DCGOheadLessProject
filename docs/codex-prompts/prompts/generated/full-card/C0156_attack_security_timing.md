# C0156_attack_security_timing - attack/security timing card porting 29

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0156_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT6_071` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_071.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT6_078` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_078.cs` | `OnAllyAttack, OnDestroyedAnyone, OnDiscardHand` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `BT6_083` | `DCGO/Assets/Scripts/CardEffect/BT6/White/BT6_083.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT6_089` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_089.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |
| `BT7_004` | `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_004.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `SelectCard` | 4 |
| `BT7_006` | `DCGO/Assets/Scripts/CardEffect/BT7/Purple/BT7_006.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous` | `SelectCard` | 2 |
| `BT7_016` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_016.cs` | `OnBlockAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `BT7_025` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_025.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT7_029` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_029.cs` | `OnAddHand, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT7_032` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_032.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT6-071#1205@base` | `BT6-071` | 1205 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_071.asset` |
| `BT6-078#1213@base` | `BT6-078` | 1213 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_078.asset` |
| `BT6-078#1214@P1` | `BT6-078` | 1214 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_078_P1.asset` |
| `BT6-083#1221@base` | `BT6-083` | 1221 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_083.asset` |
| `BT6-083#1222@P1` | `BT6-083` | 1222 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_083_P1.asset` |
| `BT6-089#1231@base` | `BT6-089` | 1231 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Tamer/BT6_089.asset` |
| `BT6-089#8722@P0` | `BT6-089` | 8722 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Tamer/BT6_089_P0.asset` |
| `BT6-089#8723@P1` | `BT6-089` | 8723 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Tamer/BT6_089_P1.asset` |
| `BT7-004#1382@base` | `BT7-004` | 1382 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/DigiEgg/BT7_004.asset` |
| `BT7-004#1383@P1` | `BT7-004` | 1383 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_004_P1.asset` |
| `BT7-004#8748@P0` | `BT7-004` | 8748 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/DigiEgg/BT7_004_P0.asset` |
| `BT7-004#8749@P1` | `BT7-004` | 8749 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Green/DigiEgg/BT7_004_P1.asset` |
| `BT7-006#1386@base` | `BT7-006` | 1386 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/DigiEgg/BT7_006.asset` |
| `BT7-006#8755@P0` | `BT7-006` | 8755 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Purple/DigiEgg/BT7_006_P0.asset` |
| `BT7-016#1398@base` | `BT7-016` | 1398 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_016.asset` |
| `BT7-016#1399@P1` | `BT7-016` | 1399 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_016_P1.asset` |
| `BT7-025#1413@base` | `BT7-025` | 1413 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_025.asset` |
| `BT7-025#8769@P0` | `BT7-025` | 8769 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_025_P0.asset` |
| `BT7-029#1418@base` | `BT7-029` | 1418 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_029.asset` |
| `BT7-029#1419@P1` | `BT7-029` | 1419 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_029_P1.asset` |
| `BT7-032#1424@base` | `BT7-032` | 1424 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_032.asset` |
| `BT7-032#6788@P1` | `BT7-032` | 6788 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/Digimon/BT7_032_P1.asset` |

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
