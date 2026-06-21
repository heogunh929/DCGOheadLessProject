# C0394_special_digivolution_play - special digivolution/play mechanics card porting 159

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0394_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_136` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_136.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 3 |
| `P_138` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_138.cs` | `OnEnterFieldAnyone, OnUnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `P_139` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_139.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `P_142` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_142.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `P_144` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_144.cs` | `None, OnAttackTargetChanged` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 3 |
| `P_145` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_145.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `P_147` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_147.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 3 |
| `P_150` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_150.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `P_152` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_152.cs` | `None, OnAllyAttack` | `max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 2 |
| `P_155` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_155.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-136#10290@base` | `P-136` | 10290 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_136.asset` |
| `P-136#9256@P1` | `P-136` | 9256 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_136_P1.asset` |
| `P-136#9257@P2` | `P-136` | 9257 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_136_P2.asset` |
| `P-138#10292@base` | `P-138` | 10292 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_138.asset` |
| `P-138#9259@P1` | `P-138` | 9259 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_138_P1.asset` |
| `P-139#10293@base` | `P-139` | 10293 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_139.asset` |
| `P-142#10296@base` | `P-142` | 10296 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_142.asset` |
| `P-144#10299@base` | `P-144` | 10299 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_144.asset` |
| `P-144#10300@P1` | `P-144` | 10300 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_144_P1.asset` |
| `P-144#9262@P2` | `P-144` | 9262 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_144_P2.asset` |
| `P-145#10301@base` | `P-145` | 10301 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_145.asset` |
| `P-145#10302@P1` | `P-145` | 10302 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_145_P1.asset` |
| `P-145#9263@P2` | `P-145` | 9263 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_145_P2.asset` |
| `P-147#10305@base` | `P-147` | 10305 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_147.asset` |
| `P-147#10306@P1` | `P-147` | 10306 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_147_P1.asset` |
| `P-147#10307@P2` | `P-147` | 10307 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_147_P2.asset` |
| `P-150#10312@base` | `P-150` | 10312 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_150.asset` |
| `P-150#10313@P1` | `P-150` | 10313 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_150_P1.asset` |
| `P-150#9267@P2` | `P-150` | 9267 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_150_P2.asset` |
| `P-152#10450@base` | `P-152` | 10450 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_152.asset` |
| `P-152#10451@P1` | `P-152` | 10451 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_152_P1.asset` |
| `P-155#10456@base` | `P-155` | 10456 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_155.asset` |
| `P-155#10457@P1` | `P-155` | 10457 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_155_P1.asset` |

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
