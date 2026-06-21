# C0100_zone_security_recovery - zone/security/recovery card porting 94

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0100_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST2_09` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_09.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `ST2_12` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_12.cs` | `OnStartTurn, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `-` | 1 |
| `ST3_09` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_09.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity` | 3 |
| `ST3_12` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_12.cs` | `None, SecuritySkill` | `inherited, security, static_or_continuous, zone_movement` | `-` | 2 |
| `ST4_03` | `DCGO/Assets/Scripts/CardEffect/ST4/Green/ST4_03.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 3 |
| `ST4_10` | `DCGO/Assets/Scripts/CardEffect/ST4/Green/ST4_10.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |
| `ST4_11` | `DCGO/Assets/Scripts/CardEffect/ST4/Green/ST4_11.cs` | `OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `ST4_12` | `DCGO/Assets/Scripts/CardEffect/ST4/Green/ST4_12.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `ST4_14` | `DCGO/Assets/Scripts/CardEffect/ST4/Green/ST4_14.cs` | `OnTappedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 1 |
| `ST5_04` | `DCGO/Assets/Scripts/CardEffect/ST5/Black/ST5_04.cs` | `OnEndTurn` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST2-09#4973@P2` | `ST2-09` | 4973 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_09_P2.asset` |
| `ST2-09#59@base` | `ST2-09` | 59 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_09.asset` |
| `ST2-09#60@P1` | `ST2-09` | 60 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Digimon/ST2_09_P1.asset` |
| `ST2-12#65@base` | `ST2-12` | 65 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Tamer/ST2_12.asset` |
| `ST3-09#4981@P2` | `ST3-09` | 4981 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_09_P2.asset` |
| `ST3-09#93@base` | `ST3-09` | 93 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_09.asset` |
| `ST3-09#94@P1` | `ST3-09` | 94 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_09_P1.asset` |
| `ST3-12#97@base` | `ST3-12` | 97 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Tamer/ST3_12.asset` |
| `ST3-12#98@P1` | `ST3-12` | 98 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Tamer/ST3_12_P1.asset` |
| `ST4-03#109@base` | `ST4-03` | 109 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_03.asset` |
| `ST4-03#110@P1` | `ST4-03` | 110 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_03_P1.asset` |
| `ST4-03#111@P2` | `ST4-03` | 111 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_03_P2.asset` |
| `ST4-10#124@base` | `ST4-10` | 124 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_10.asset` |
| `ST4-10#125@P1` | `ST4-10` | 125 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_10_P1.asset` |
| `ST4-11#126@base` | `ST4-11` | 126 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_11.asset` |
| `ST4-12#127@base` | `ST4-12` | 127 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_12.asset` |
| `ST4-14#130@base` | `ST4-14` | 130 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Tamer/ST4_14.asset` |
| `ST5-04#324@base` | `ST5-04` | 324 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_04.asset` |
| `ST5-06#326@base` | `ST5-06` | 326 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_06.asset` |

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
