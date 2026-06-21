# C0396_special_digivolution_play - special digivolution/play mechanics card porting 161

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0396_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_183` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_183.cs` | `None, OnAttackTargetChanged, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `P_185` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_185.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `P_186` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_186.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `P_187` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_187.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `P_190` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_190.cs` | `None, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `linked, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `-` | 3 |
| `P_193` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_193.cs` | `OnEndTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 3 |
| `P_195` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_195.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 3 |
| `P_201` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_201.cs` | `None, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `P_202` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_202.cs` | `BeforePayCost, None, OnDeclaration, OnDetermineDoSecurityCheck` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `P_203` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_203.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-183#5435@base` | `P-183` | 5435 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_183.asset` |
| `P-185#5437@base` | `P-185` | 5437 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_185.asset` |
| `P-186#5438@base` | `P-186` | 5438 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_186.asset` |
| `P-186#9301@P1` | `P-186` | 9301 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_186_P1.asset` |
| `P-187#5439@base` | `P-187` | 5439 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_187.asset` |
| `P-187#9302@P1` | `P-187` | 9302 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_187_P1.asset` |
| `P-190#6981@base` | `P-190` | 6981 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_190.asset` |
| `P-190#6982@P1` | `P-190` | 6982 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_190_P1.asset` |
| `P-190#9305@P2` | `P-190` | 9305 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_190_P2.asset` |
| `P-193#6987@base` | `P-193` | 6987 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_193.asset` |
| `P-193#6988@P1` | `P-193` | 6988 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_193_P1.asset` |
| `P-193#9308@P2` | `P-193` | 9308 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_193_P2.asset` |
| `P-195#7131@base` | `P-195` | 7131 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_195.asset` |
| `P-195#9310@P1` | `P-195` | 9310 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_195_P1.asset` |
| `P-195#9311@P2` | `P-195` | 9311 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_195_P2.asset` |
| `P-201#7472@base` | `P-201` | 7472 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_201.asset` |
| `P-201#7473@P1` | `P-201` | 7473 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_201_P1.asset` |
| `P-202#7474@base` | `P-202` | 7474 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_202.asset` |
| `P-202#7475@P1` | `P-202` | 7475 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_202_P1.asset` |
| `P-203#7476@base` | `P-203` | 7476 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_203.asset` |
| `P-203#7477@P1` | `P-203` | 7477 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_203_P1.asset` |

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
