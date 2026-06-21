# C0034_zone_security_recovery - zone/security/recovery card porting 28

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0034_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 11
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT20_092` | `DCGO/Assets/Scripts/CardEffect/BT20/White/BT20_092.cs` | `OnEnterFieldAnyone, OnStartMainPhase, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 1 |
| `BT20_096` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_096.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 1 |
| `BT21_001` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_001.cs` | `OnLoseSecurity` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT21_003` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_003.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT21_007` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_007.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT21_008` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_008.cs` | `OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 2 |
| `BT21_011` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_011.cs` | `BeforePayCost, None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `BT21_012` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_012.cs` | `None, OnDeclaration` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 1 |
| `BT21_015` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_015.cs` | `None, OnEnterFieldAnyone, SecuritySkill` | `inherited, max_count_per_turn, security, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT21_024` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_024.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT20-092#5171@base` | `BT20-092` | 5171 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/White/Tamer/BT20_092.asset` |
| `BT20-096#5175@base` | `BT20-096` | 5175 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Option/BT20_096.asset` |
| `BT21-001#5307@base` | `BT21-001` | 5307 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/DigiEgg/BT21_001.asset` |
| `BT21-003#5309@base` | `BT21-003` | 5309 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/DigiEgg/BT21_003.asset` |
| `BT21-007#5313@base` | `BT21-007` | 5313 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_007.asset` |
| `BT21-008#5314@base` | `BT21-008` | 5314 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_008.asset` |
| `BT21-008#5315@P1` | `BT21-008` | 5315 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_008_P1.asset` |
| `BT21-011#5319@base` | `BT21-011` | 5319 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_011.asset` |
| `BT21-012#5320@base` | `BT21-012` | 5320 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_012.asset` |
| `BT21-015#5323@base` | `BT21-015` | 5323 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_015.asset` |
| `BT21-024#5333@base` | `BT21-024` | 5333 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_024.asset` |

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
