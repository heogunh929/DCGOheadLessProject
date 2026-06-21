# C0093_zone_security_recovery - zone/security/recovery card porting 87

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0093_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_236` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_236.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity` | 2 |
| `RB1_010` | `DCGO/Assets/Scripts/CardEffect/RB1/Red/RB1_010.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 3 |
| `RB1_027` | `DCGO/Assets/Scripts/CardEffect/RB1/Black/RB1_027.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectBoolean, SelectSecurity` | 2 |
| `RB1_028` | `DCGO/Assets/Scripts/CardEffect/RB1/Purple/RB1_028.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `RB1_034` | `DCGO/Assets/Scripts/CardEffect/RB1/Green/RB1_034.cs` | `BeforePayCost, OnEndTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `RB1_035` | `DCGO/Assets/Scripts/CardEffect/RB1/White/RB1_035.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 1 |
| `ST10_05` | `DCGO/Assets/Scripts/CardEffect/ST10/Yellow/ST10_05.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `ST10_08` | `DCGO/Assets/Scripts/CardEffect/ST10/Purple/ST10_08.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `ST10_09` | `DCGO/Assets/Scripts/CardEffect/ST10/Purple/ST10_09.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `ST10_11` | `DCGO/Assets/Scripts/CardEffect/ST10/Purple/ST10_11.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-018#6042@base` | `P-018` | 6042 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_018.asset` |
| `P-018#6043@P1` | `P-018` | 6043 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_018_P1.asset` |
| `P-236#7904@base` | `P-236` | 7904 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_236.asset` |
| `P-236#7905@P1` | `P-236` | 7905 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_236_P1.asset` |
| `RB1-010#2872@base` | `RB1-010` | 2872 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Digimon/RB1_010.asset` |
| `RB1-010#2873@P1` | `RB1-010` | 2873 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Digimon/RB1_010_P1.asset` |
| `RB1-010#2874@P2` | `RB1-010` | 2874 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Digimon/RB1_010_P2.asset` |
| `RB1-027#2896@base` | `RB1-027` | 2896 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Black/Digimon/RB1_027.asset` |
| `RB1-027#2897@P1` | `RB1-027` | 2897 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Black/Digimon/RB1_027_P1.asset` |
| `RB1-028#2898@base` | `RB1-028` | 2898 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Purple/Digimon/RB1_028.asset` |
| `RB1-034#2907@base` | `RB1-034` | 2907 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Green/Tamer/RB1_034.asset` |
| `RB1-034#2908@P1` | `RB1-034` | 2908 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Green/Tamer/RB1_034_P1.asset` |
| `RB1-035#2909@base` | `RB1-035` | 2909 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/White/Tamer/RB1_035.asset` |
| `ST10-05#1760@base` | `ST10-05` | 1760 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Digimon/ST10_05.asset` |
| `ST10-05#1761@P1` | `ST10-05` | 1761 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Yellow/Digimon/ST10_05_P1.asset` |
| `ST10-08#1766@base` | `ST10-08` | 1766 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Purple/Digimon/ST10_08.asset` |
| `ST10-09#1767@base` | `ST10-09` | 1767 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Purple/Digimon/ST10_09.asset` |
| `ST10-11#1769@base` | `ST10-11` | 1769 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST10/Purple/Digimon/ST10_11.asset` |

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
