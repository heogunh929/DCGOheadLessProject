# C0022_zone_security_recovery - zone/security/recovery card porting 16

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0022_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT15_082` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_082.cs` | `OnReturnCardsToHandFromTrash, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 4 |
| `BT15_084` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_084.cs` | `OnDiscardSecurity, OnLoseSecurity, OnStartTurn, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 4 |
| `BT15_097` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_097.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 1 |
| `BT16_006` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_006.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 3 |
| `BT16_020` | `DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_020.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 1 |
| `BT16_029` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_029.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `BT16_037` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_037.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT16_042` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_042.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT16_047` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_047.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT16_048` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_048.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT15-082#3217@base` | `BT15-082` | 3217 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Tamer/BT15_082.asset` |
| `BT15-082#3218@P1` | `BT15-082` | 3218 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Tamer/BT15_082_P1.asset` |
| `BT15-082#4756@P0` | `BT15-082` | 4756 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Tamer/BT15_082_P0.asset` |
| `BT15-082#4757@P2` | `BT15-082` | 4757 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Tamer/BT15_082_P2.asset` |
| `BT15-084#3221@base` | `BT15-084` | 3221 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Tamer/BT15_084.asset` |
| `BT15-084#3222@P1` | `BT15-084` | 3222 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Tamer/BT15_084_P1.asset` |
| `BT15-084#4759@P0` | `BT15-084` | 4759 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Tamer/BT15_084_P0.asset` |
| `BT15-084#4760@P2` | `BT15-084` | 4760 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Tamer/BT15_084_P2.asset` |
| `BT15-097#3238@base` | `BT15-097` | 3238 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Option/BT15_097.asset` |
| `BT16-006#3310@base` | `BT16-006` | 3310 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/DigiEgg/BT16_006.asset` |
| `BT16-006#3311@P1` | `BT16-006` | 3311 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/DigiEgg/BT16_006_P1.asset` |
| `BT16-006#4778@P0` | `BT16-006` | 4778 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/DigiEgg/BT16_006_P0.asset` |
| `BT16-020#3328@base` | `BT16-020` | 3328 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_020.asset` |
| `BT16-029#3342@base` | `BT16-029` | 3342 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_029.asset` |
| `BT16-037#3351@base` | `BT16-037` | 3351 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_037.asset` |
| `BT16-042#3356@base` | `BT16-042` | 3356 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_042.asset` |
| `BT16-047#3362@base` | `BT16-047` | 3362 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_047.asset` |
| `BT16-047#4800@P0` | `BT16-047` | 4800 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_047_P0.asset` |
| `BT16-048#3363@base` | `BT16-048` | 3363 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_048.asset` |
| `BT16-048#3364@P1` | `BT16-048` | 3364 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_048_P1.asset` |

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
