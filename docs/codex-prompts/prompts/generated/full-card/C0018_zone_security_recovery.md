# C0018_zone_security_recovery - zone/security/recovery card porting 12

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0018_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 27
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT14_068` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_068.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT14_077` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_077.cs` | `OnDiscardLibrary, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `-` | 1 |
| `BT14_082` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_082.cs` | `OnLoseSecurity, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |
| `BT14_083` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_083.cs` | `OnDigivolutionCardDiscarded, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectSecurity` | 3 |
| `BT14_084` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_084.cs` | `OnAddSecurity, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectSecurity` | 3 |
| `BT14_085` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_085.cs` | `OnEnterFieldAnyone, OnTappedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 4 |
| `BT15_001` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_001.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 3 |
| `BT15_002` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_002.cs` | `OnAddHand` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `-` | 3 |
| `BT15_006` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_006.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 3 |
| `BT15_007` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_007.cs` | `OnLoseSecurity, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT14-068#2994@base` | `BT14-068` | 2994 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_068.asset` |
| `BT14-068#2995@P1` | `BT14-068` | 2995 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_068_P1.asset` |
| `BT14-077#3005@base` | `BT14-077` | 3005 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_077.asset` |
| `BT14-082#3011@base` | `BT14-082` | 3011 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Tamer/BT14_082.asset` |
| `BT14-082#3012@P1` | `BT14-082` | 3012 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Tamer/BT14_082_P1.asset` |
| `BT14-082#4692@P0` | `BT14-082` | 4692 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Tamer/BT14_082_P0.asset` |
| `BT14-083#3013@base` | `BT14-083` | 3013 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Tamer/BT14_083.asset` |
| `BT14-083#3014@P1` | `BT14-083` | 3014 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Tamer/BT14_083_P1.asset` |
| `BT14-083#4693@P0` | `BT14-083` | 4693 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Tamer/BT14_083_P0.asset` |
| `BT14-084#3015@base` | `BT14-084` | 3015 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Tamer/BT14_084.asset` |
| `BT14-084#3016@P1` | `BT14-084` | 3016 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Tamer/BT14_084_P1.asset` |
| `BT14-084#4694@P0` | `BT14-084` | 4694 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Tamer/BT14_084_P0.asset` |
| `BT14-085#3017@base` | `BT14-085` | 3017 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Tamer/BT14_085.asset` |
| `BT14-085#3018@P1` | `BT14-085` | 3018 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Tamer/BT14_085_P1.asset` |
| `BT14-085#4695@P0` | `BT14-085` | 4695 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Tamer/BT14_085_P0.asset` |
| `BT14-085#4696@P2` | `BT14-085` | 4696 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Tamer/BT14_085_P2.asset` |
| `BT15-001#3114@base` | `BT15-001` | 3114 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/DigiEgg/BT15_001.asset` |
| `BT15-001#3115@P1` | `BT15-001` | 3115 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/DigiEgg/BT15_001_P1.asset` |
| `BT15-001#4708@P0` | `BT15-001` | 4708 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/DigiEgg/BT15_001_P0.asset` |
| `BT15-002#3116@base` | `BT15-002` | 3116 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/DigiEgg/BT15_002.asset` |
| `BT15-002#3117@P1` | `BT15-002` | 3117 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/DigiEgg/BT15_002_P1.asset` |
| `BT15-002#4709@P0` | `BT15-002` | 4709 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/DigiEgg/BT15_002_P0.asset` |
| `BT15-006#3124@base` | `BT15-006` | 3124 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/DigiEgg/BT15_006.asset` |
| `BT15-006#3125@P1` | `BT15-006` | 3125 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/DigiEgg/BT15_006_P1.asset` |
| `BT15-006#4713@P0` | `BT15-006` | 4713 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/DigiEgg/BT15_006_P0.asset` |
| `BT15-007#3126@base` | `BT15-007` | 3126 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_007.asset` |
| `BT15-007#3127@P1` | `BT15-007` | 3127 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_007_P1.asset` |

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
