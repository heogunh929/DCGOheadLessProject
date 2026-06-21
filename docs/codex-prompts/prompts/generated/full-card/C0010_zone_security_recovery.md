# C0010_zone_security_recovery - zone/security/recovery card porting 4

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0010_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT11_033` | `DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_033.cs` | `OnAddHand, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT11_039` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_039.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT11_045` | `DCGO/Assets/Scripts/CardEffect/BT11/Yellow/BT11_045.cs` | `OnEnterFieldAnyone, OnLoseSecurity` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT11_046` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_046.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT11_047` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_047.cs` | `OnStartTurn` | `max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT11_050` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_050.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT11_052` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_052.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT11_055` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_055.cs` | `OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT11_057` | `DCGO/Assets/Scripts/CardEffect/BT11/Green/BT11_057.cs` | `OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `BT11_060` | `DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_060.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-033#2304@base` | `BT11-033` | 2304 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_033.asset` |
| `BT11-033#2305@P1` | `BT11-033` | 2305 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_033_P1.asset` |
| `BT11-039#2311@base` | `BT11-039` | 2311 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_039.asset` |
| `BT11-045#2318@base` | `BT11-045` | 2318 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_045.asset` |
| `BT11-045#4405@P0` | `BT11-045` | 4405 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_045_P0.asset` |
| `BT11-046#2319@base` | `BT11-046` | 2319 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_046.asset` |
| `BT11-046#4406@P0` | `BT11-046` | 4406 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_046_P0.asset` |
| `BT11-047#2320@base` | `BT11-047` | 2320 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_047.asset` |
| `BT11-050#2323@base` | `BT11-050` | 2323 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_050.asset` |
| `BT11-052#2325@base` | `BT11-052` | 2325 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_052.asset` |
| `BT11-052#4407@P0` | `BT11-052` | 4407 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_052_P0.asset` |
| `BT11-055#2328@base` | `BT11-055` | 2328 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_055.asset` |
| `BT11-055#4410@P0` | `BT11-055` | 4410 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_055_P0.asset` |
| `BT11-057#2331@base` | `BT11-057` | 2331 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_057.asset` |
| `BT11-057#4411@P0` | `BT11-057` | 4411 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Green/Digimon/BT11_057_P0.asset` |
| `BT11-060#2334@base` | `BT11-060` | 2334 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_060.asset` |

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
