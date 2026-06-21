# C0054_zone_security_recovery - zone/security/recovery card porting 48

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0054_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 26
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT4_089` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_089.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 1 |
| `BT4_091` | `DCGO/Assets/Scripts/CardEffect/BT4/White/BT4_091.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT4_094` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_094.cs` | `None, OnDestroyedAnyone, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectSecurity` | 3 |
| `BT4_096` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_096.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 5 |
| `BT4_097` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_097.cs` | `OnLoseSecurity, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 4 |
| `BT4_113` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_113.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT5_011` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_011.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT5_021` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_021.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 4 |
| `BT5_025` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_025.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectCount` | 1 |
| `BT5_028` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_028.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT4-089#884@base` | `BT4-089` | 884 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_089.asset` |
| `BT4-091#890@base` | `BT4-091` | 890 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/White/Digimon/BT4_091.asset` |
| `BT4-091#891@P1` | `BT4-091` | 891 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/White/Digimon/BT4_091_P1.asset` |
| `BT4-094#8545@P0` | `BT4-094` | 8545 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Tamer/BT4_094_P0.asset` |
| `BT4-094#896@base` | `BT4-094` | 896 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Tamer/BT4_094.asset` |
| `BT4-094#897@P1` | `BT4-094` | 897 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Tamer/BT4_094_P1.asset` |
| `BT4-096#8547@P0` | `BT4-096` | 8547 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Tamer/BT4_096_P0.asset` |
| `BT4-096#8548@P3` | `BT4-096` | 8548 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Tamer/BT4_096_P3.asset` |
| `BT4-096#900@base` | `BT4-096` | 900 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Tamer/BT4_096.asset` |
| `BT4-096#901@P1` | `BT4-096` | 901 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Tamer/BT4_096_P1.asset` |
| `BT4-096#902@P2` | `BT4-096` | 902 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Tamer/BT4_096_P2.asset` |
| `BT4-097#8549@P0` | `BT4-097` | 8549 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Tamer/BT4_097_P0.asset` |
| `BT4-097#8550@P2` | `BT4-097` | 8550 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Tamer/BT4_097_P2.asset` |
| `BT4-097#903@base` | `BT4-097` | 903 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Tamer/BT4_097.asset` |
| `BT4-097#904@P1` | `BT4-097` | 904 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Tamer/BT4_097_P1.asset` |
| `BT4-113#922@base` | `BT4-113` | 922 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_113.asset` |
| `BT4-113#923@P1` | `BT4-113` | 923 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_113_P1.asset` |
| `BT5-011#948@base` | `BT5-011` | 948 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_011.asset` |
| `BT5-011#949@P1` | `BT5-011` | 949 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_011_P1.asset` |
| `BT5-021#8584@P1` | `BT5-021` | 8584 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_021_P1.asset` |
| `BT5-021#8585@P2` | `BT5-021` | 8585 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_021_P2.asset` |
| `BT5-021#965@base` | `BT5-021` | 965 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_021.asset` |
| `BT5-025#970@base` | `BT5-025` | 970 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_025.asset` |
| `BT5-028#8586@P0` | `BT5-028` | 8586 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_028_P0.asset` |
| `BT5-028#975@base` | `BT5-028` | 975 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_028.asset` |
| `BT5-033#982@base` | `BT5-033` | 982 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_033.asset` |

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
