# C0055_zone_security_recovery - zone/security/recovery card porting 49

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0055_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT5_034` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_034.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT5_035` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_035.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `BT5_036` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_036.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 4 |
| `BT5_037` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_037.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `BT5_039` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_039.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT5_042` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_042.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 4 |
| `BT5_043` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_043.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 2 |
| `BT5_049` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_049.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT5_055` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_055.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT5_065` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_065.cs` | `None, SecuritySkill` | `inherited, modifier_duration, security, static_or_continuous, zone_movement` | `-` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT5-034#983@base` | `BT5-034` | 983 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_034.asset` |
| `BT5-034#984@P1` | `BT5-034` | 984 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_034_P1.asset` |
| `BT5-035#985@base` | `BT5-035` | 985 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_035.asset` |
| `BT5-035#986@P1` | `BT5-035` | 986 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_035_P1.asset` |
| `BT5-036#8592@P0` | `BT5-036` | 8592 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_036_P0.asset` |
| `BT5-036#8593@P1` | `BT5-036` | 8593 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_036_P1.asset` |
| `BT5-036#8594@P2` | `BT5-036` | 8594 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_036_P2.asset` |
| `BT5-036#987@base` | `BT5-036` | 987 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_036.asset` |
| `BT5-037#988@base` | `BT5-037` | 988 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_037.asset` |
| `BT5-039#8595@P0` | `BT5-039` | 8595 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_039_P0.asset` |
| `BT5-039#991@base` | `BT5-039` | 991 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_039.asset` |
| `BT5-042#994@base` | `BT5-042` | 994 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_042.asset` |
| `BT5-042#995@P1` | `BT5-042` | 995 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_042_P1.asset` |
| `BT5-042#996@P2` | `BT5-042` | 996 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_042_P2.asset` |
| `BT5-042#997@P3` | `BT5-042` | 997 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_042_P3.asset` |
| `BT5-043#8597@P0` | `BT5-043` | 8597 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_043_P0.asset` |
| `BT5-043#998@base` | `BT5-043` | 998 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_043.asset` |
| `BT5-049#1007@base` | `BT5-049` | 1007 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_049.asset` |
| `BT5-049#8603@P0` | `BT5-049` | 8603 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_049_P0.asset` |
| `BT5-055#1013@base` | `BT5-055` | 1013 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_055.asset` |
| `BT5-055#8605@P0` | `BT5-055` | 8605 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_055_P0.asset` |
| `BT5-065#1024@base` | `BT5-065` | 1024 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_065.asset` |
| `BT5-065#8610@P0` | `BT5-065` | 8610 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_065_P0.asset` |
| `BT5-065#8611@P1` | `BT5-065` | 8611 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_065_P1.asset` |

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
