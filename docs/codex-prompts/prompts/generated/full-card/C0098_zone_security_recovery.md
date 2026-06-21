# C0098_zone_security_recovery - zone/security/recovery card porting 92

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0098_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 14
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST20_13` | `DCGO/Assets/Scripts/CardEffect/ST20/Black/ST20_13.cs` | `BeforePayCost, None, SecuritySkill` | `inherited, max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `ST20_14` | `DCGO/Assets/Scripts/CardEffect/ST20/Black/ST20_14.cs` | `None, OptionSkill, SecuritySkill, WhenRemoveField` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 1 |
| `ST21_01` | `DCGO/Assets/Scripts/CardEffect/ST21/Purple/ST21_01.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `ST21_02` | `DCGO/Assets/Scripts/CardEffect/ST21/Blue/ST21_02.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 2 |
| `ST21_07` | `DCGO/Assets/Scripts/CardEffect/ST21/Green/ST21_07.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 2 |
| `ST21_08` | `DCGO/Assets/Scripts/CardEffect/ST21/Green/ST21_08.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `ST21_12` | `DCGO/Assets/Scripts/CardEffect/ST21/Blue/ST21_12.cs` | `BeforePayCost, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `ST21_13` | `DCGO/Assets/Scripts/CardEffect/ST21/Purple/ST21_13.cs` | `BeforePayCost, None, SecuritySkill` | `inherited, max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `ST21_14` | `DCGO/Assets/Scripts/CardEffect/ST21/Purple/ST21_14.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity` | 1 |
| `ST22_14` | `DCGO/Assets/Scripts/CardEffect/ST22/Purple/ST22_14.cs` | `BeforePayCost, None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST20-13#5276@base` | `ST20-13` | 5276 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Black/Tamer/ST20_13.asset` |
| `ST20-14#5277@base` | `ST20-14` | 5277 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Black/Option/ST20_14.asset` |
| `ST21-01#5279@base` | `ST21-01` | 5279 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Purple/DigiEgg/ST21_01.asset` |
| `ST21-01#9063@P1` | `ST21-01` | 9063 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Purple/DigiEgg/ST21_01_P1.asset` |
| `ST21-02#5280@base` | `ST21-02` | 5280 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Blue/Digimon/ST21_02.asset` |
| `ST21-02#9064@P1` | `ST21-02` | 9064 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Blue/Digimon/ST21_02_P1.asset` |
| `ST21-07#5285@base` | `ST21-07` | 5285 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Green/Digimon/ST21_07.asset` |
| `ST21-07#9069@P1` | `ST21-07` | 9069 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Green/Digimon/ST21_07_P1.asset` |
| `ST21-08#5286@base` | `ST21-08` | 5286 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Green/Digimon/ST21_08.asset` |
| `ST21-08#9070@P1` | `ST21-08` | 9070 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Green/Digimon/ST21_08_P1.asset` |
| `ST21-12#5291@base` | `ST21-12` | 5291 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Blue/Tamer/ST21_12.asset` |
| `ST21-13#5292@base` | `ST21-13` | 5292 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Purple/Tamer/ST21_13.asset` |
| `ST21-14#5293@base` | `ST21-14` | 5293 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST21/Purple/Option/ST21_14.asset` |
| `ST22-14#7505@base` | `ST22-14` | 7505 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Purple/Digimon/ST22_14.asset` |

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
