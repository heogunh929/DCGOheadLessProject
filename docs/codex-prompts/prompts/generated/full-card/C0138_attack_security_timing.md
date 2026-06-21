# C0138_attack_security_timing - attack/security timing card porting 11

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0138_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT18_091` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_091.cs` | `OnAttackTargetChanged, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectAttackTarget` | 2 |
| `BT18_094` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_094.cs` | `OnAllyAttack, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT18_099` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_099.cs` | `None, OnAttackTargetChanged, OnStartMainPhase, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectAttackTarget` | 1 |
| `BT19_001` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT19_002` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT19_017` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_017.cs` | `OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT19_023` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_023.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 1 |
| `BT19_024` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_024.cs` | `OnEndAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `BT19_053` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_053.cs` | `None, OnAllyAttack, WhenRemoveField` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 3 |
| `BT19_054` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_054.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT18-091#3956@base` | `BT18-091` | 3956 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Tamer/BT18_091.asset` |
| `BT18-091#3957@P1` | `BT18-091` | 3957 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Tamer/BT18_091_P1.asset` |
| `BT18-094#3962@base` | `BT18-094` | 3962 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Tamer/BT18_094.asset` |
| `BT18-094#3963@P1` | `BT18-094` | 3963 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Tamer/BT18_094_P1.asset` |
| `BT18-099#3961@base` | `BT18-099` | 3961 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Option/BT18_099.asset` |
| `BT19-001#5005@base` | `BT19-001` | 5005 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/DigiEgg/BT19_001.asset` |
| `BT19-002#3977@base` | `BT19-002` | 3977 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/DigiEgg/BT19_002.asset` |
| `BT19-017#3980@base` | `BT19-017` | 3980 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_017.asset` |
| `BT19-017#6757@P1` | `BT19-017` | 6757 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_017_P1.asset` |
| `BT19-023#3983@base` | `BT19-023` | 3983 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_023.asset` |
| `BT19-024#3984@base` | `BT19-024` | 3984 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_024.asset` |
| `BT19-024#8277@P1` | `BT19-024` | 8277 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_024_P1.asset` |
| `BT19-053#4004@base` | `BT19-053` | 4004 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_053.asset` |
| `BT19-053#4005@P1` | `BT19-053` | 4005 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_053_P1.asset` |
| `BT19-053#8284@P2` | `BT19-053` | 8284 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_053_P2.asset` |
| `BT19-054#5040@base` | `BT19-054` | 5040 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_054.asset` |

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
