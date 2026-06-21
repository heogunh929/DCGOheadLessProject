# C0101_zone_security_recovery - zone/security/recovery card porting 95

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0101_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST5_09` | `DCGO/Assets/Scripts/CardEffect/ST5/Black/ST5_09.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `ST5_12` | `DCGO/Assets/Scripts/CardEffect/ST5/Black/ST5_12.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `ST5_14` | `DCGO/Assets/Scripts/CardEffect/ST5/Black/ST5_14.cs` | `OnTappedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `ST6_01` | `DCGO/Assets/Scripts/CardEffect/ST6/Purple/ST6_01.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 4 |
| `ST6_04` | `DCGO/Assets/Scripts/CardEffect/ST6/Purple/ST6_04.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `ST6_10` | `DCGO/Assets/Scripts/CardEffect/ST6/Purple/ST6_10.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `ST6_11` | `DCGO/Assets/Scripts/CardEffect/ST6/Purple/ST6_11.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `-` | 1 |
| `ST6_12` | `DCGO/Assets/Scripts/CardEffect/ST6/Purple/ST6_12.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `ST6_14` | `DCGO/Assets/Scripts/CardEffect/ST6/Purple/ST6_14.cs` | `OnDestroyedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 2 |
| `ST7_04` | `DCGO/Assets/Scripts/CardEffect/ST7/Red/ST7_04.cs` | `None` | `inherited, modifier_duration, static_or_continuous, zone_movement` | `-` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT11-037#2309@base` | `BT11-037` | 2309 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Yellow/Digimon/BT11_037.asset` |
| `ST14-04#2820@base` | `ST14-04` | 2820 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_04.asset` |
| `ST14-04#4917@P0` | `ST14-04` | 4917 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_04_P0.asset` |
| `ST5-09#330@base` | `ST5-09` | 330 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_09.asset` |
| `ST5-12#333@base` | `ST5-12` | 333 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_12.asset` |
| `ST5-12#334@P1` | `ST5-12` | 334 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_12_P1.asset` |
| `ST5-14#336@base` | `ST5-14` | 336 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Tamer/ST5_14.asset` |
| `ST6-01#339@base` | `ST6-01` | 339 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/DigiEgg/ST6_01.asset` |
| `ST6-01#340@P1` | `ST6-01` | 340 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_01_P1.asset` |
| `ST6-01#4987@P1` | `ST6-01` | 4987 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/DigiEgg/ST6_01_P1.asset` |
| `ST6-01#4988@P2` | `ST6-01` | 4988 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/DigiEgg/ST6_01_P2.asset` |
| `ST6-04#343@base` | `ST6-04` | 343 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_04.asset` |
| `ST6-10#352@base` | `ST6-10` | 352 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_10.asset` |
| `ST6-11#353@base` | `ST6-11` | 353 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_11.asset` |
| `ST6-12#354@base` | `ST6-12` | 354 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_12.asset` |
| `ST6-14#356@base` | `ST6-14` | 356 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Tamer/ST6_14.asset` |
| `ST6-14#357@P1` | `ST6-14` | 357 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Tamer/ST6_14_P1.asset` |
| `ST7-04#571@base` | `ST7-04` | 571 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_04.asset` |

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
