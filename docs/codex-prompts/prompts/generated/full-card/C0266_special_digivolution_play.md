# C0266_special_digivolution_play - special digivolution/play mechanics card porting 31

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0266_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT15_100` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_100.cs` | `OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT15_102` | `DCGO/Assets/Scripts/CardEffect/BT15/White/BT15_102.cs` | `BeforePayCost, None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectDigiXros` | 3 |
| `BT16_007` | `DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_007.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT16_008` | `DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_008.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT16_010` | `DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_010.cs` | `None, OnDestroyedAnyone, OnEndTurn` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT16_011` | `DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_011.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone, OnReturnCardsToHandFromTrash` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT16_012` | `DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_012.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT16_014` | `DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_014.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT16_015` | `DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_015.cs` | `AfterEffectsActivate, None, OnDestroyedAnyone, OnDigivolutionCardDiscarded, OnEndAttack, OnEnterFieldAnyone, OnStartTurn` | `background, inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_priority, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT16_016` | `DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_016.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT15-100#3241@base` | `BT15-100` | 3241 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Option/BT15_100.asset` |
| `BT15-100#4772@P0` | `BT15-100` | 4772 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Option/BT15_100_P0.asset` |
| `BT15-102#3244@base` | `BT15-102` | 3244 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/White/Digimon/BT15_102.asset` |
| `BT15-102#3245@P1` | `BT15-102` | 3245 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/White/Digimon/BT15_102_P1.asset` |
| `BT15-102#3246@P2` | `BT15-102` | 3246 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/White/Digimon/BT15_102_P2.asset` |
| `BT16-007#3312@base` | `BT16-007` | 3312 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_007.asset` |
| `BT16-008#3313@base` | `BT16-008` | 3313 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_008.asset` |
| `BT16-010#3315@base` | `BT16-010` | 3315 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_010.asset` |
| `BT16-011#3316@base` | `BT16-011` | 3316 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_011.asset` |
| `BT16-011#4779@P0` | `BT16-011` | 4779 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_011_P0.asset` |
| `BT16-012#3317@base` | `BT16-012` | 3317 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_012.asset` |
| `BT16-012#3318@P1` | `BT16-012` | 3318 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_012_P1.asset` |
| `BT16-014#3322@base` | `BT16-014` | 3322 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_014.asset` |
| `BT16-014#4780@P0` | `BT16-014` | 4780 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_014_P0.asset` |
| `BT16-015#3323@base` | `BT16-015` | 3323 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_015.asset` |
| `BT16-015#4781@P0` | `BT16-015` | 4781 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/Digimon/BT16_015_P0.asset` |
| `BT16-016#3324@base` | `BT16-016` | 3324 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_016.asset` |

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
