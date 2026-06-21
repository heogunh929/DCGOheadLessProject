# C0397_special_digivolution_play - special digivolution/play mechanics card porting 162

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0397_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_204` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_204.cs` | `OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `P_205` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_205.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `P_213` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_213.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `P_217` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_217.cs` | `OnEnterFieldAnyone, SecuritySkill, WhenLinked` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 2 |
| `P_218` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_218.cs` | `OnEnterFieldAnyone, SecuritySkill, WhenLinked` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 2 |
| `P_219` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_219.cs` | `BeforePayCost, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `P_220` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_220.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectAssembly` | 1 |
| `P_221` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_221.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `P_222` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_222.cs` | `BeforePayCost, None, OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `P_223` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_223.cs` | `BeforePayCost, None, OnEnterFieldAnyone, OnUseOption` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-204#7478@base` | `P-204` | 7478 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_204.asset` |
| `P-204#7479@P1` | `P-204` | 7479 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_204_P1.asset` |
| `P-205#7480@base` | `P-205` | 7480 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_205.asset` |
| `P-205#7481@P1` | `P-205` | 7481 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_205_P1.asset` |
| `P-213#7506@base` | `P-213` | 7506 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_213.asset` |
| `P-217#7513@base` | `P-217` | 7513 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Tamer/P_217.asset` |
| `P-217#7514@P1` | `P-217` | 7514 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Tamer/P_217_P1.asset` |
| `P-218#7515@base` | `P-218` | 7515 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_218.asset` |
| `P-218#7516@P1` | `P-218` | 7516 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_218_P1.asset` |
| `P-219#7517@base` | `P-219` | 7517 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_219.asset` |
| `P-219#7518@P1` | `P-219` | 7518 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_219_P1.asset` |
| `P-220#7651@base` | `P-220` | 7651 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_220.asset` |
| `P-221#7652@base` | `P-221` | 7652 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_221.asset` |
| `P-222#7653@base` | `P-222` | 7653 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_222.asset` |
| `P-223#7654@base` | `P-223` | 7654 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_223.asset` |

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
