# C0019_zone_security_recovery - zone/security/recovery card porting 13

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0019_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT15_009` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_009.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT15_011` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_011.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 1 |
| `BT15_013` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_013.cs` | `OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity` | 2 |
| `BT15_016` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_016.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT15_017` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_017.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 5 |
| `BT15_018` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_018.cs` | `OnEndTurn` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT15_019` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_019.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT15_022` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_022.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT15_023` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_023.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT15_027` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_027.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT15-009#3129@base` | `BT15-009` | 3129 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_009.asset` |
| `BT15-011#3131@base` | `BT15-011` | 3131 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_011.asset` |
| `BT15-013#3133@base` | `BT15-013` | 3133 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_013.asset` |
| `BT15-013#4715@P0` | `BT15-013` | 4715 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_013_P0.asset` |
| `BT15-016#3138@base` | `BT15-016` | 3138 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_016.asset` |
| `BT15-016#4716@P0` | `BT15-016` | 4716 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_016_P0.asset` |
| `BT15-017#3139@base` | `BT15-017` | 3139 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_017.asset` |
| `BT15-017#4717@P0` | `BT15-017` | 4717 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_017_P0.asset` |
| `BT15-017#4718@P1` | `BT15-017` | 4718 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_017_P1.asset` |
| `BT15-017#4719@P2` | `BT15-017` | 4719 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_017_P2.asset` |
| `BT15-017#4720@P3` | `BT15-017` | 4720 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_017_P3.asset` |
| `BT15-018#3140@base` | `BT15-018` | 3140 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_018.asset` |
| `BT15-018#4721@P0` | `BT15-018` | 4721 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_018_P0.asset` |
| `BT15-019#3141@base` | `BT15-019` | 3141 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_019.asset` |
| `BT15-022#3145@base` | `BT15-022` | 3145 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_022.asset` |
| `BT15-023#3146@base` | `BT15-023` | 3146 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_023.asset` |
| `BT15-027#3152@base` | `BT15-027` | 3152 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_027.asset` |

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
