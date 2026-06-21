# C0071_zone_security_recovery - zone/security/recovery card porting 65

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0071_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX1_022` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_022.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX1_023` | `DCGO/Assets/Scripts/CardEffect/EX1/Yellow/EX1_023.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `EX1_024` | `DCGO/Assets/Scripts/CardEffect/EX1/Yellow/EX1_024.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 3 |
| `EX1_031` | `DCGO/Assets/Scripts/CardEffect/EX1/Yellow/EX1_031.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 1 |
| `EX1_034` | `DCGO/Assets/Scripts/CardEffect/EX1/Green/EX1_034.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX1_045` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_045.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 1 |
| `EX1_048` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_048.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 4 |
| `EX1_049` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_049.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |
| `EX1_054` | `DCGO/Assets/Scripts/CardEffect/EX1/Black/EX1_054.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `EX1_055` | `DCGO/Assets/Scripts/CardEffect/EX1/Purple/EX1_055.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX1-022#1300@base` | `EX1-022` | 1300 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_022.asset` |
| `EX1-022#1301@P1` | `EX1-022` | 1301 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_022_P1.asset` |
| `EX1-023#1302@base` | `EX1-023` | 1302 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_023.asset` |
| `EX1-024#1303@base` | `EX1-024` | 1303 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_024.asset` |
| `EX1-024#1304@P1` | `EX1-024` | 1304 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_024_P1.asset` |
| `EX1-024#1305@P2` | `EX1-024` | 1305 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_024_P2.asset` |
| `EX1-031#1316@base` | `EX1-031` | 1316 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_031.asset` |
| `EX1-034#1320@base` | `EX1-034` | 1320 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_034.asset` |
| `EX1-034#1321@P1` | `EX1-034` | 1321 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Green/Digimon/EX1_034_P1.asset` |
| `EX1-045#1337@base` | `EX1-045` | 1337 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_045.asset` |
| `EX1-048#1340@base` | `EX1-048` | 1340 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_048.asset` |
| `EX1-048#1341@P1` | `EX1-048` | 1341 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_048_P1.asset` |
| `EX1-048#1342@P2` | `EX1-048` | 1342 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_048_P2.asset` |
| `EX1-048#1343@P3` | `EX1-048` | 1343 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_048_P3.asset` |
| `EX1-049#1344@base` | `EX1-049` | 1344 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_049.asset` |
| `EX1-049#1345@P1` | `EX1-049` | 1345 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_049_P1.asset` |
| `EX1-054#1352@base` | `EX1-054` | 1352 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Black/Digimon/EX1_054.asset` |
| `EX1-055#1353@base` | `EX1-055` | 1353 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Purple/Digimon/EX1_055.asset` |

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
