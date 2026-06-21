# C0068_zone_security_recovery - zone/security/recovery card porting 62

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0068_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX10_025` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_025.cs` | `OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX10_026` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_026.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX10_028` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_028.cs` | `OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX10_037` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_037.cs` | `None, OnDiscardLibrary, OnStartMainPhase` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX10_039` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_039.cs` | `OnDestroyedAnyone, OnDigivolutionCardDiscarded, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean` | 2 |
| `EX10_048` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_048.cs` | `BeforePayCost, None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `EX10_054` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_054.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `EX10_067` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_067.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `EX11_003` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_003.cs` | `OnAddSecurity` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |
| `EX11_005` | `DCGO/Assets/Scripts/CardEffect/EX11/Purple/EX11_005.cs` | `OnStartMainPhase` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX10-025#7177@base` | `EX10-025` | 7177 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_025.asset` |
| `EX10-025#7290@P1` | `EX10-025` | 7290 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_025_P1.asset` |
| `EX10-026#7179@base` | `EX10-026` | 7179 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_026.asset` |
| `EX10-026#7291@P1` | `EX10-026` | 7291 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_026_P1.asset` |
| `EX10-028#7183@base` | `EX10-028` | 7183 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_028.asset` |
| `EX10-028#7293@P1` | `EX10-028` | 7293 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_028_P1.asset` |
| `EX10-037#7200@base` | `EX10-037` | 7200 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_037.asset` |
| `EX10-037#7301@P1` | `EX10-037` | 7301 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_037_P1.asset` |
| `EX10-039#7203@base` | `EX10-039` | 7203 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_039.asset` |
| `EX10-039#7302@P1` | `EX10-039` | 7302 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_039_P1.asset` |
| `EX10-048#7221@base` | `EX10-048` | 7221 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_048.asset` |
| `EX10-054#7231@base` | `EX10-054` | 7231 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_054.asset` |
| `EX10-067#7253@base` | `EX10-067` | 7253 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Tamer/EX10_067.asset` |
| `EX10-067#7324@P1` | `EX10-067` | 7324 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Tamer/EX10_067_P1.asset` |
| `EX11-003#7661@base` | `EX11-003` | 7661 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/DigiEgg/EX11_003.asset` |
| `EX11-003#7662@P1` | `EX11-003` | 7662 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/DigiEgg/EX11_003_P1.asset` |
| `EX11-005#7665@base` | `EX11-005` | 7665 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/DigiEgg/EX11_005.asset` |
| `EX11-005#7666@P1` | `EX11-005` | 7666 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/DigiEgg/EX11_005_P1.asset` |

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
