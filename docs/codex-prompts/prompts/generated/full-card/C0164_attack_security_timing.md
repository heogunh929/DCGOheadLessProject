# C0164_attack_security_timing - attack/security timing card porting 37

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0164_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX1_063` | `DCGO/Assets/Scripts/CardEffect/EX1/Purple/EX1_063.cs` | `OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `EX2_010` | `DCGO/Assets/Scripts/CardEffect/EX2/Red/EX2_010.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |
| `EX2_011` | `DCGO/Assets/Scripts/CardEffect/EX2/Red/EX2_011.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |
| `EX2_013` | `DCGO/Assets/Scripts/CardEffect/EX2/Blue/EX2_013.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `EX2_014` | `DCGO/Assets/Scripts/CardEffect/EX2/Blue/EX2_014.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `EX2_028` | `DCGO/Assets/Scripts/CardEffect/EX2/Green/EX2_028.cs` | `None, OnEndAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `EX2_029` | `DCGO/Assets/Scripts/CardEffect/EX2/Green/EX2_029.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX2_032` | `DCGO/Assets/Scripts/CardEffect/EX2/Black/EX2_032.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `EX2_038` | `DCGO/Assets/Scripts/CardEffect/EX2/Black/EX2_038.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectInteger, SelectSecurity` | 2 |
| `EX2_040` | `DCGO/Assets/Scripts/CardEffect/EX2/Purple/EX2_040.cs` | `OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX1-063#1362@base` | `EX1-063` | 1362 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_063.asset` |
| `EX1-063#1363@P1` | `EX1-063` | 1363 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_063_P1.asset` |
| `EX2-010#1931@base` | `EX2-010` | 1931 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_010.asset` |
| `EX2-010#1932@P1` | `EX2-010` | 1932 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_010_P1.asset` |
| `EX2-010#1933@P2` | `EX2-010` | 1933 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_010_P2.asset` |
| `EX2-011#1934@base` | `EX2-011` | 1934 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_011.asset` |
| `EX2-011#1935@P1` | `EX2-011` | 1935 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_011_P1.asset` |
| `EX2-011#1936@P2` | `EX2-011` | 1936 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_011_P2.asset` |
| `EX2-013#1939@base` | `EX2-013` | 1939 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Digimon/EX2_013.asset` |
| `EX2-014#1940@base` | `EX2-014` | 1940 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Digimon/EX2_014.asset` |
| `EX2-028#1963@base` | `EX2-028` | 1963 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Digimon/EX2_028.asset` |
| `EX2-029#1964@base` | `EX2-029` | 1964 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Digimon/EX2_029.asset` |
| `EX2-029#1965@P1` | `EX2-029` | 1965 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Digimon/EX2_029_P1.asset` |
| `EX2-032#1969@base` | `EX2-032` | 1969 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Digimon/EX2_032.asset` |
| `EX2-038#1976@base` | `EX2-038` | 1976 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Digimon/EX2_038.asset` |
| `EX2-038#1977@P1` | `EX2-038` | 1977 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Digimon/EX2_038_P1.asset` |
| `EX2-040#1981@base` | `EX2-040` | 1981 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_040.asset` |

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
