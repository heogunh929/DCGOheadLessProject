# C0046_zone_security_recovery - zone/security/recovery card porting 40

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0046_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT25_078` | `DCGO/Assets/Scripts/CardEffect/BT25/Purple/BT25_078.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectBoolean` | 1 |
| `BT25_079` | `DCGO/Assets/Scripts/CardEffect/BT25/Purple/BT25_079.cs` | `None, OnDestroyedAnyone` | `inherited, static_or_continuous, zone_movement` | `-` | 1 |
| `BT25_080` | `DCGO/Assets/Scripts/CardEffect/BT25/Purple/BT25_080.cs` | `None, OnDiscardHand` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `BT25_081` | `DCGO/Assets/Scripts/CardEffect/BT25/Purple/BT25_081.cs` | `OnDestroyedAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT25_083` | `DCGO/Assets/Scripts/CardEffect/BT25/Purple/BT25_083.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger` | 1 |
| `BT25_085` | `DCGO/Assets/Scripts/CardEffect/BT25/Purple/BT25_085.cs` | `None, OptionSkill` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger` | 3 |
| `BT25_087` | `DCGO/Assets/Scripts/CardEffect/BT25/Blue/BT25_087.cs` | `BeforePayCost, OnAddHand, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectInteger, SelectSecurity` | 1 |
| `BT25_092` | `DCGO/Assets/Scripts/CardEffect/BT25/Purple/BT25_092.cs` | `OnDeclaration, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity` | 3 |
| `BT2_001` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_001.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `-` | 4 |
| `BT2_008` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_008.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-001#360@base` | `BT2-001` | 360 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/DigiEgg/BT2_001.asset` |
| `BT2-001#361@P1` | `BT2-001` | 361 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_001_P1.asset` |
| `BT2-001#8300@P1` | `BT2-001` | 8300 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/DigiEgg/BT2_001_P1.asset` |
| `BT2-008#371@base` | `BT2-008` | 371 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_008.asset` |
| `BT2-009#372@base` | `BT2-009` | 372 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_009.asset` |
| `BT25-078#8053@base` | `BT25-078` | 8053 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_078.asset` |
| `BT25-079#8054@base` | `BT25-079` | 8054 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_079.asset` |
| `BT25-080#8055@base` | `BT25-080` | 8055 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_080.asset` |
| `BT25-081#8056@base` | `BT25-081` | 8056 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_081.asset` |
| `BT25-083#8058@base` | `BT25-083` | 8058 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_083.asset` |
| `BT25-085#8061@base` | `BT25-085` | 8061 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_085.asset` |
| `BT25-085#8062@P1` | `BT25-085` | 8062 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_085_P1.asset` |
| `BT25-085#8063@P2` | `BT25-085` | 8063 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Digimon/BT25_085_P2.asset` |
| `BT25-087#8067@base` | `BT25-087` | 8067 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Tamer/BT25_087.asset` |
| `BT25-092#8076@base` | `BT25-092` | 8076 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Tamer/BT25_092.asset` |
| `BT25-092#8077@P1` | `BT25-092` | 8077 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Tamer/BT25_092_P1.asset` |
| `BT25-092#8078@P2` | `BT25-092` | 8078 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Purple/Tamer/BT25_092_P2.asset` |

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
