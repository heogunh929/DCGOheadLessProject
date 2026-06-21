# C0281_special_digivolution_play - special digivolution/play mechanics card porting 46

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0281_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT18_077` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_077.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT18_078` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_078.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectInteger, SelectJogress` | 2 |
| `BT18_079` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_079.cs` | `None, OnDestroyedAnyone, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT18_081` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_081.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 4 |
| `BT18_084` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_084.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 2 |
| `BT18_088` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_088.cs` | `None, OnEndTurn, OnStartMainPhase, OnStartTurn, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectAttackTarget, SelectJogress` | 3 |
| `BT18_092` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_092.cs` | `OnAllyAttack, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT18_095` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_095.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectOrder, SelectSecurity, SelectJogress` | 1 |
| `BT18_096` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_096.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT18_097` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_097.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectOrder, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT18-077#3937@base` | `BT18-077` | 3937 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_077.asset` |
| `BT18-078#3935@base` | `BT18-078` | 3935 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_078.asset` |
| `BT18-078#3936@P1` | `BT18-078` | 3936 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_078_P1.asset` |
| `BT18-079#3940@base` | `BT18-079` | 3940 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_079.asset` |
| `BT18-079#3941@P1` | `BT18-079` | 3941 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_079_P1.asset` |
| `BT18-081#3950@base` | `BT18-081` | 3950 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_081.asset` |
| `BT18-081#8264@P1` | `BT18-081` | 8264 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_081_P1.asset` |
| `BT18-081#8265@P2` | `BT18-081` | 8265 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_081_P2.asset` |
| `BT18-081#8266@P3` | `BT18-081` | 8266 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_081_P3.asset` |
| `BT18-084#3944@base` | `BT18-084` | 3944 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_084.asset` |
| `BT18-084#8267@P1` | `BT18-084` | 8267 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_084_P1.asset` |
| `BT18-088#3953@base` | `BT18-088` | 3953 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Tamer/BT18_088.asset` |
| `BT18-088#3954@P1` | `BT18-088` | 3954 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Tamer/BT18_088_P1.asset` |
| `BT18-088#8268@P2` | `BT18-088` | 8268 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Tamer/BT18_088_P2.asset` |
| `BT18-092#3958@base` | `BT18-092` | 3958 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Tamer/BT18_092.asset` |
| `BT18-092#8269@P1` | `BT18-092` | 8269 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Black/Tamer/BT18_092_P1.asset` |
| `BT18-095#3967@base` | `BT18-095` | 3967 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Option/BT18_095.asset` |
| `BT18-096#3966@base` | `BT18-096` | 3966 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Option/BT18_096.asset` |
| `BT18-097#3968@base` | `BT18-097` | 3968 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Option/BT18_097.asset` |

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
