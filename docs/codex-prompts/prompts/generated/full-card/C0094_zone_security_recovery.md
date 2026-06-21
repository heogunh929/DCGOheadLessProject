# C0094_zone_security_recovery - zone/security/recovery card porting 88

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0094_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST10_12` | `DCGO/Assets/Scripts/CardEffect/ST10/Purple/ST10_12.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 2 |
| `ST10_13` | `DCGO/Assets/Scripts/CardEffect/ST10/Purple/ST10_13.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `ST13_01` | `DCGO/Assets/Scripts/CardEffect/ST13/Black/ST13_01.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `ST13_09` | `DCGO/Assets/Scripts/CardEffect/ST13/Black/ST13_09.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `ST13_11` | `DCGO/Assets/Scripts/CardEffect/ST13/Black/ST13_11.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `ST14_03` | `DCGO/Assets/Scripts/CardEffect/ST14/Purple/ST14_03.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `-` | 2 |
| `ST14_05` | `DCGO/Assets/Scripts/CardEffect/ST14/Purple/ST14_05.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `-` | 2 |
| `ST14_06` | `DCGO/Assets/Scripts/CardEffect/ST14/Purple/ST14_06.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `ST14_08` | `DCGO/Assets/Scripts/CardEffect/ST14/Purple/ST14_08.cs` | `OnDiscardLibrary, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 3 |
| `ST14_10` | `DCGO/Assets/Scripts/CardEffect/ST14/Purple/ST14_10.cs` | `OnDiscardLibrary, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST10-12#1770@base` | `ST10-12` | 1770 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Purple/Digimon/ST10_12.asset` |
| `ST10-12#1771@P1` | `ST10-12` | 1771 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Purple/Digimon/ST10_12_P1.asset` |
| `ST10-13#1772@base` | `ST10-13` | 1772 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Purple/Digimon/ST10_13.asset` |
| `ST13-01#2801@base` | `ST13-01` | 2801 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Black/DigiEgg/ST13_01.asset` |
| `ST13-09#2809@base` | `ST13-09` | 2809 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Black/Digimon/ST13_09.asset` |
| `ST13-11#2811@base` | `ST13-11` | 2811 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST13/Black/Digimon/ST13_11.asset` |
| `ST14-03#2819@base` | `ST14-03` | 2819 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_03.asset` |
| `ST14-03#4916@P0` | `ST14-03` | 4916 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_03_P0.asset` |
| `ST14-05#2821@base` | `ST14-05` | 2821 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_05.asset` |
| `ST14-05#4918@P0` | `ST14-05` | 4918 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_05_P0.asset` |
| `ST14-06#2822@base` | `ST14-06` | 2822 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_06.asset` |
| `ST14-06#4919@P0` | `ST14-06` | 4919 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_06_P0.asset` |
| `ST14-08#2824@base` | `ST14-08` | 2824 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_08.asset` |
| `ST14-08#2825@P1` | `ST14-08` | 2825 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_08_P1.asset` |
| `ST14-08#4922@P2` | `ST14-08` | 4922 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_08_P2.asset` |
| `ST14-10#2827@base` | `ST14-10` | 2827 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_10.asset` |
| `ST14-10#4923@P1` | `ST14-10` | 4923 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Digimon/ST14_10_P1.asset` |

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
