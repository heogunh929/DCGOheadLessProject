# C0299_special_digivolution_play - special digivolution/play mechanics card porting 64

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0299_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT21_075` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_075.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT21_076` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_076.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectSecurity, SelectJogress` | 1 |
| `BT21_077` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_077.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT21_079` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_079.cs` | `None, OnDestroyedAnyone, OnEndAttack` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT21_084` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_084.cs` | `OnStartTurn, SecuritySkill, WhenLinked` | `linked, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectAppFusion` | 2 |
| `BT21_086` | `DCGO/Assets/Scripts/CardEffect/BT21/Yellow/BT21_086.cs` | `OnEnterFieldAnyone, OnStartMainPhase, OnTappedAnyone, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT21_087` | `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_087.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT21_089` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_089.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT21_094` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_094.cs` | `OptionSkill, SecuritySkill, WhenTopCardTrashed` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT21_096` | `DCGO/Assets/Scripts/CardEffect/BT21/Yellow/BT21_096.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT21-075#5392@base` | `BT21-075` | 5392 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_075.asset` |
| `BT21-076#5393@base` | `BT21-076` | 5393 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_076.asset` |
| `BT21-077#5394@base` | `BT21-077` | 5394 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_077.asset` |
| `BT21-077#8410@P1` | `BT21-077` | 8410 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_077_P1.asset` |
| `BT21-079#5396@base` | `BT21-079` | 5396 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_079.asset` |
| `BT21-084#5405@base` | `BT21-084` | 5405 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_084.asset` |
| `BT21-084#5406@P1` | `BT21-084` | 5406 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_084_P1.asset` |
| `BT21-086#5409@base` | `BT21-086` | 5409 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Tamer/BT21_086.asset` |
| `BT21-086#5410@P1` | `BT21-086` | 5410 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Tamer/BT21_086_P1.asset` |
| `BT21-087#5411@base` | `BT21-087` | 5411 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Tamer/BT21_087.asset` |
| `BT21-087#5412@P1` | `BT21-087` | 5412 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Tamer/BT21_087_P1.asset` |
| `BT21-089#5415@base` | `BT21-089` | 5415 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Tamer/BT21_089.asset` |
| `BT21-089#5416@P1` | `BT21-089` | 5416 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Tamer/BT21_089_P1.asset` |
| `BT21-094#5421@base` | `BT21-094` | 5421 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Option/BT21_094.asset` |
| `BT21-096#5423@base` | `BT21-096` | 5423 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Option/BT21_096.asset` |

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
