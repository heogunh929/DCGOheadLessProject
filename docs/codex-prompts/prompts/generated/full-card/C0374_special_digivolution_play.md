# C0374_special_digivolution_play - special digivolution/play mechanics card porting 139

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0374_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX5_045` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_045.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX5_047` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_047.cs` | `None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX5_048` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_048.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `EX5_049` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_049.cs` | `None, OnDestroyedAnyone, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX5_051` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_051.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `EX5_052` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_052.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `EX5_054` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_054.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 2 |
| `EX5_055` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_055.cs` | `None, OnDestroyedAnyone, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX5_059` | `DCGO/Assets/Scripts/CardEffect/EX5/Purple/EX5_059.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX5_061` | `DCGO/Assets/Scripts/CardEffect/EX5/Purple/EX5_061.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX5-045#3084@base` | `EX5-045` | 3084 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_045.asset` |
| `EX5-047#3086@base` | `EX5-047` | 3086 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_047.asset` |
| `EX5-048#3087@base` | `EX5-048` | 3087 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_048.asset` |
| `EX5-048#9147@P1` | `EX5-048` | 9147 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_048_P1.asset` |
| `EX5-049#3088@base` | `EX5-049` | 3088 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_049.asset` |
| `EX5-051#3090@base` | `EX5-051` | 3090 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_051.asset` |
| `EX5-052#3091@base` | `EX5-052` | 3091 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_052.asset` |
| `EX5-054#3093@base` | `EX5-054` | 3093 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_054.asset` |
| `EX5-054#4232@P1` | `EX5-054` | 4232 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_054_P1.asset` |
| `EX5-055#3094@base` | `EX5-055` | 3094 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_055.asset` |
| `EX5-055#4233@P1` | `EX5-055` | 4233 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_055_P1.asset` |
| `EX5-059#3098@base` | `EX5-059` | 3098 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/Digimon/EX5_059.asset` |
| `EX5-061#3100@base` | `EX5-061` | 3100 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/Digimon/EX5_061.asset` |

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
