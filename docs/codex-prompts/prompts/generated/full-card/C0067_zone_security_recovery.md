# C0067_zone_security_recovery - zone/security/recovery card porting 61

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0067_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 27
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT9_074` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_074.cs` | `OnDestroyedAnyone, SecuritySkill` | `inherited, max_count_per_turn, security, static_or_continuous, zone_movement` | `-` | 3 |
| `BT9_076` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_076.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT9_085` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_085.cs` | `OnStartMainPhase, OnUnTappedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT9_087` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_087.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT9_088` | `DCGO/Assets/Scripts/CardEffect/BT9/Green/BT9_088.cs` | `OnEndBattle, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 2 |
| `BT9_089` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_089.cs` | `OnEnterFieldAnyone, OnUnTappedAnyone, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `-` | 2 |
| `BT9_091` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_091.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 2 |
| `BT9_092` | `DCGO/Assets/Scripts/CardEffect/BT9/White/BT9_092.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 7 |
| `BT9_112` | `DCGO/Assets/Scripts/CardEffect/BT9/Purple/BT9_112.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 4 |
| `EX10_005` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_005.cs` | `OnDiscardLibrary` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT9-074#1867@base` | `BT9-074` | 1867 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_074.asset` |
| `BT9-074#1868@P1` | `BT9-074` | 1868 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_074_P1.asset` |
| `BT9-074#8988@P0` | `BT9-074` | 8988 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_074_P0.asset` |
| `BT9-076#1870@base` | `BT9-076` | 1870 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_076.asset` |
| `BT9-085#1883@base` | `BT9-085` | 1883 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Tamer/BT9_085.asset` |
| `BT9-085#8999@P0` | `BT9-085` | 8999 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Tamer/BT9_085_P0.asset` |
| `BT9-087#1885@base` | `BT9-087` | 1885 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Tamer/BT9_087.asset` |
| `BT9-087#9000@P0` | `BT9-087` | 9000 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Tamer/BT9_087_P0.asset` |
| `BT9-088#1886@base` | `BT9-088` | 1886 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Tamer/BT9_088.asset` |
| `BT9-088#9001@P0` | `BT9-088` | 9001 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Tamer/BT9_088_P0.asset` |
| `BT9-089#1887@base` | `BT9-089` | 1887 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Tamer/BT9_089.asset` |
| `BT9-089#9002@P0` | `BT9-089` | 9002 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Tamer/BT9_089_P0.asset` |
| `BT9-091#1889@base` | `BT9-091` | 1889 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Tamer/BT9_091.asset` |
| `BT9-091#9004@P0` | `BT9-091` | 9004 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Tamer/BT9_091_P0.asset` |
| `BT9-092#1890@base` | `BT9-092` | 1890 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Tamer/BT9_092.asset` |
| `BT9-092#9005@P0` | `BT9-092` | 9005 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Tamer/BT9_092_P0.asset` |
| `BT9-092#9006@P1` | `BT9-092` | 9006 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Tamer/BT9_092_P1.asset` |
| `BT9-092#9007@P2` | `BT9-092` | 9007 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Tamer/BT9_092_P2.asset` |
| `BT9-092#9008@P3` | `BT9-092` | 9008 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Tamer/BT9_092_P3.asset` |
| `BT9-092#9009@P4` | `BT9-092` | 9009 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Tamer/BT9_092_P4.asset` |
| `BT9-092#9010@P5` | `BT9-092` | 9010 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT9/White/Tamer/BT9_092_P5.asset` |
| `BT9-112#1912@base` | `BT9-112` | 1912 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_112.asset` |
| `BT9-112#1913@P1` | `BT9-112` | 1913 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_112_P1.asset` |
| `BT9-112#6815@P2` | `BT9-112` | 6815 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_112_P2.asset` |
| `BT9-112#6816@P3` | `BT9-112` | 6816 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Purple/Digimon/BT9_112_P3.asset` |
| `EX10-005#7140@base` | `EX10-005` | 7140 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/DigiEgg/EX10_005.asset` |
| `EX10-005#7273@P1` | `EX10-005` | 7273 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/DigiEgg/EX10_005_P1.asset` |

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
