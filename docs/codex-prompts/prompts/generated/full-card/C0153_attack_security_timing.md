# C0153_attack_security_timing - attack/security timing card porting 26

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0153_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 27
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT4_090` | `DCGO/Assets/Scripts/CardEffect/BT4/White/BT4_090.cs` | `OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectAttackTarget` | 6 |
| `BT4_092` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_092.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 3 |
| `BT4_114` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_114.cs` | `OnAllyAttack, OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `BT5_003` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_003.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 4 |
| `BT5_005` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_005.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT5_016` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_016.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT5_017` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_017.cs` | `None, OnEnterFieldAnyone` | `inherited, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectAttackTarget` | 2 |
| `BT5_018` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_018.cs` | `OnAllyAttack` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `BT5_031` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_031.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT5_032` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_032.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectCount` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT4-090#8542@P0` | `BT4-090` | 8542 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/White/Digimon/BT4_090_P0.asset` |
| `BT4-090#885@base` | `BT4-090` | 885 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/White/Digimon/BT4_090.asset` |
| `BT4-090#886@P1` | `BT4-090` | 886 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/White/Digimon/BT4_090_P1.asset` |
| `BT4-090#887@P2` | `BT4-090` | 887 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/White/Digimon/BT4_090_P2.asset` |
| `BT4-090#888@P3` | `BT4-090` | 888 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT4/White/Digimon/BT4_090_P3.asset` |
| `BT4-090#889@P4` | `BT4-090` | 889 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT4/White/Digimon/BT4_090_P4.asset` |
| `BT4-092#8543@P0` | `BT4-092` | 8543 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Tamer/BT4_092_P0.asset` |
| `BT4-092#892@base` | `BT4-092` | 892 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Tamer/BT4_092.asset` |
| `BT4-092#893@P1` | `BT4-092` | 893 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Tamer/BT4_092_P1.asset` |
| `BT4-114#924@base` | `BT4-114` | 924 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_114.asset` |
| `BT4-114#925@P1` | `BT4-114` | 925 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_114_P1.asset` |
| `BT5-003#8571@P0` | `BT5-003` | 8571 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/DigiEgg/BT5_003_P0.asset` |
| `BT5-003#8572@P1` | `BT5-003` | 8572 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/DigiEgg/BT5_003_P1.asset` |
| `BT5-003#931@base` | `BT5-003` | 931 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/DigiEgg/BT5_003.asset` |
| `BT5-003#932@P1` | `BT5-003` | 932 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_003_P1.asset` |
| `BT5-005#8574@P0` | `BT5-005` | 8574 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/DigiEgg/BT5_005_P0.asset` |
| `BT5-005#934@base` | `BT5-005` | 934 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/DigiEgg/BT5_005.asset` |
| `BT5-016#8580@P0` | `BT5-016` | 8580 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_016_P0.asset` |
| `BT5-016#956@base` | `BT5-016` | 956 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_016.asset` |
| `BT5-017#8581@P0` | `BT5-017` | 8581 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_017_P0.asset` |
| `BT5-017#957@base` | `BT5-017` | 957 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_017.asset` |
| `BT5-018#958@base` | `BT5-018` | 958 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_018.asset` |
| `BT5-031#8589@P0` | `BT5-031` | 8589 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_031_P0.asset` |
| `BT5-031#978@base` | `BT5-031` | 978 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_031.asset` |
| `BT5-032#979@base` | `BT5-032` | 979 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_032.asset` |
| `BT5-032#980@P1` | `BT5-032` | 980 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_032_P1.asset` |
| `BT5-032#981@P2` | `BT5-032` | 981 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_032_P2.asset` |

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
