# C0087_zone_security_recovery - zone/security/recovery card porting 81

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0087_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `LM_032` | `DCGO/Assets/Scripts/CardEffect/LM/Purple/LM_032.cs` | `OnStartTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 4 |
| `LM_042` | `DCGO/Assets/Scripts/CardEffect/LM/Green/LM_042.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `P_001` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_001.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 3 |
| `P_002` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_002.cs` | `OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `P_003` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_003.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `P_005` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_005.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectSecurity` | 3 |
| `P_006` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_006.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |
| `P_015` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_015.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `P_017` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_017.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `-` | 2 |
| `P_020` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_020.cs` | `OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `LM-032#4042@base` | `LM-032` | 4042 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Option/LM_032.asset` |
| `LM-032#7889@P1` | `LM-032` | 7889 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Option/LM_032_P1.asset` |
| `LM-032#7890@P2` | `LM-032` | 7890 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Option/LM_032_P2.asset` |
| `LM-032#7891@P3` | `LM-032` | 7891 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Option/LM_032_P3.asset` |
| `LM-042#5443@base` | `LM-042` | 5443 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Digimon/LM_042.asset` |
| `LM-042#9210@P1` | `LM-042` | 9210 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Digimon/LM_042_P1.asset` |
| `P-001#6000@base` | `P-001` | 6000 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_001.asset` |
| `P-001#6001@P1` | `P-001` | 6001 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_001_P1.asset` |
| `P-001#6002@P2` | `P-001` | 6002 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_001_P2.asset` |
| `P-002#6003@base` | `P-002` | 6003 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_002.asset` |
| `P-002#6004@P1` | `P-002` | 6004 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_002_P1.asset` |
| `P-003#6005@base` | `P-003` | 6005 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_003.asset` |
| `P-003#6006@P1` | `P-003` | 6006 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_003_P1.asset` |
| `P-005#6011@base` | `P-005` | 6011 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_005.asset` |
| `P-005#6012@P1` | `P-005` | 6012 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_005_P1.asset` |
| `P-005#6013@P2` | `P-005` | 6013 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_005_P2.asset` |
| `P-006#6014@base` | `P-006` | 6014 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_006.asset` |
| `P-006#6015@P1` | `P-006` | 6015 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_006_P1.asset` |
| `P-015#6035@base` | `P-015` | 6035 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_015.asset` |
| `P-017#10343@P1` | `P-017` | 10343 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_017_P1.asset` |
| `P-017#6041@base` | `P-017` | 6041 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_017.asset` |
| `P-020#10344@P1` | `P-020` | 10344 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_020_P1.asset` |
| `P-020#6045@base` | `P-020` | 6045 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_020.asset` |

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
