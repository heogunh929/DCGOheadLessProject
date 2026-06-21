# C0263_special_digivolution_play - special digivolution/play mechanics card porting 28

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0263_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT15_040` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_040.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT15_041` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_041.cs` | `None, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT15_042` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_042.cs` | `OnEnterFieldAnyone, OnLoseSecurity` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 5 |
| `BT15_045` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_045.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT15_048` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_048.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT15_051` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_051.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 1 |
| `BT15_052` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_052.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT15_054` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_054.cs` | `None, OnEnterFieldAnyone, OnMove` | `max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT15_056` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_056.cs` | `None, OnStartMainPhase, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `BT15_057` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_057.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT15-040#3168@base` | `BT15-040` | 3168 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_040.asset` |
| `BT15-041#3169@base` | `BT15-041` | 3169 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_041.asset` |
| `BT15-042#3170@base` | `BT15-042` | 3170 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_042.asset` |
| `BT15-042#4730@P0` | `BT15-042` | 4730 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_042_P0.asset` |
| `BT15-042#4731@P1` | `BT15-042` | 4731 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_042_P1.asset` |
| `BT15-042#4732@P2` | `BT15-042` | 4732 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_042_P2.asset` |
| `BT15-042#4733@P3` | `BT15-042` | 4733 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_042_P3.asset` |
| `BT15-045#3174@base` | `BT15-045` | 3174 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_045.asset` |
| `BT15-048#3177@base` | `BT15-048` | 3177 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_048.asset` |
| `BT15-051#3182@base` | `BT15-051` | 3182 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_051.asset` |
| `BT15-052#3183@base` | `BT15-052` | 3183 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_052.asset` |
| `BT15-052#4734@P0` | `BT15-052` | 4734 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_052_P0.asset` |
| `BT15-054#3185@base` | `BT15-054` | 3185 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_054.asset` |
| `BT15-054#4739@P0` | `BT15-054` | 4739 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_054_P0.asset` |
| `BT15-056#3187@base` | `BT15-056` | 3187 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_056.asset` |
| `BT15-056#3188@P1` | `BT15-056` | 3188 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_056_P1.asset` |
| `BT15-057#3189@base` | `BT15-057` | 3189 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_057.asset` |
| `BT15-057#4741@P0` | `BT15-057` | 4741 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_057_P0.asset` |

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
