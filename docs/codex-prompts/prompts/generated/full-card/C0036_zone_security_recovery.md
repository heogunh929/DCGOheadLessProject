# C0036_zone_security_recovery - zone/security/recovery card porting 30

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0036_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 14
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT21_088` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_088.cs` | `BeforePayCost, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `BT21_090` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_090.cs` | `None, OnAddDigivolutionCards, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity` | 3 |
| `BT21_091` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_091.cs` | `None, OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity` | 1 |
| `BT21_092` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_092.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity` | 1 |
| `BT21_093` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_093.cs` | `BeforePayCost, None, OnLoseSecurity, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |
| `BT21_095` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_095.cs` | `None, OnEndTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |
| `BT21_099` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_099.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity` | 1 |
| `BT22_001` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_001.cs` | `OnAddDigivolutionCards` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT22_002` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_002.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT22_004` | `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_004.cs` | `OnAddDigivolutionCards` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT21-088#5413@base` | `BT21-088` | 5413 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Tamer/BT21_088.asset` |
| `BT21-088#5414@P1` | `BT21-088` | 5414 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Tamer/BT21_088_P1.asset` |
| `BT21-090#5417@base` | `BT21-090` | 5417 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Option/BT21_090.asset` |
| `BT21-090#8414@P1` | `BT21-090` | 8414 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Option/BT21_090_P1.asset` |
| `BT21-090#8415@P2` | `BT21-090` | 8415 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Option/BT21_090_P2.asset` |
| `BT21-091#5418@base` | `BT21-091` | 5418 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Option/BT21_091.asset` |
| `BT21-092#5419@base` | `BT21-092` | 5419 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Option/BT21_092.asset` |
| `BT21-093#5420@base` | `BT21-093` | 5420 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Option/BT21_093.asset` |
| `BT21-095#5422@base` | `BT21-095` | 5422 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Option/BT21_095.asset` |
| `BT21-095#8416@P1` | `BT21-095` | 8416 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Option/BT21_095_P1.asset` |
| `BT21-099#5426@base` | `BT21-099` | 5426 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Option/BT21_099.asset` |
| `BT22-001#6989@base` | `BT22-001` | 6989 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/DigiEgg/BT22_001.asset` |
| `BT22-002#6990@base` | `BT22-002` | 6990 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/DigiEgg/BT22_002.asset` |
| `BT22-004#6992@base` | `BT22-004` | 6992 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/DigiEgg/BT22_004.asset` |

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
