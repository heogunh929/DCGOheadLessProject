# C0075_zone_security_recovery - zone/security/recovery card porting 69

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0075_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX3_038` | `DCGO/Assets/Scripts/CardEffect/EX3/Green/EX3_038.cs` | `OnTappedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX3_040` | `DCGO/Assets/Scripts/CardEffect/EX3/Green/EX3_040.cs` | `BeforePayCost, None, OnTappedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `EX3_042` | `DCGO/Assets/Scripts/CardEffect/EX3/Green/EX3_042.cs` | `OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `EX3_045` | `DCGO/Assets/Scripts/CardEffect/EX3/Green/EX3_045.cs` | `OnEndTurn, OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX3_049` | `DCGO/Assets/Scripts/CardEffect/EX3/Black/EX3_049.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `EX3_053` | `DCGO/Assets/Scripts/CardEffect/EX3/Black/EX3_053.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `EX3_054` | `DCGO/Assets/Scripts/CardEffect/EX3/Black/EX3_054.cs` | `BeforePayCost, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX3_059` | `DCGO/Assets/Scripts/CardEffect/EX3/Purple/EX3_059.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `EX3_065` | `DCGO/Assets/Scripts/CardEffect/EX3/White/EX3_065.cs` | `None, OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 3 |
| `EX4_001` | `DCGO/Assets/Scripts/CardEffect/EX4/Blue/EX4_001.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX3-038#2219@base` | `EX3-038` | 2219 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_038.asset` |
| `EX3-038#9127@P1` | `EX3-038` | 9127 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_038_P1.asset` |
| `EX3-040#2221@base` | `EX3-040` | 2221 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_040.asset` |
| `EX3-042#2224@base` | `EX3-042` | 2224 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_042.asset` |
| `EX3-045#2228@base` | `EX3-045` | 2228 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_045.asset` |
| `EX3-045#2229@P1` | `EX3-045` | 2229 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_045_P1.asset` |
| `EX3-049#2233@base` | `EX3-049` | 2233 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Digimon/EX3_049.asset` |
| `EX3-049#2234@P1` | `EX3-049` | 2234 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Digimon/EX3_049_P1.asset` |
| `EX3-053#2238@base` | `EX3-053` | 2238 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Digimon/EX3_053.asset` |
| `EX3-053#2239@P1` | `EX3-053` | 2239 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Digimon/EX3_053_P1.asset` |
| `EX3-054#2240@base` | `EX3-054` | 2240 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Digimon/EX3_054.asset` |
| `EX3-054#2241@P1` | `EX3-054` | 2241 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Digimon/EX3_054_P1.asset` |
| `EX3-059#2246@base` | `EX3-059` | 2246 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_059.asset` |
| `EX3-065#2255@base` | `EX3-065` | 2255 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/White/Tamer/EX3_065.asset` |
| `EX3-065#2256@P1` | `EX3-065` | 2256 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/White/Tamer/EX3_065_P1.asset` |
| `EX3-065#9131@P2` | `EX3-065` | 9131 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX3/White/Tamer/EX3_065_P2.asset` |
| `EX4-001#2544@base` | `EX4-001` | 2544 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Blue/DigiEgg/EX4_001.asset` |

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
