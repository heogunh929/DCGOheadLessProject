# C0395_special_digivolution_play - special digivolution/play mechanics card porting 160

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0395_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_158` | `DCGO/Assets/Scripts/CardEffect/P/White/P_158.cs` | `OnDeclaration, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `P_159` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_159.cs` | `None, OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `P_160` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_160.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 2 |
| `P_161` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_161.cs` | `None, OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `P_171` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_171.cs` | `BeforePayCost, None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `P_172` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_172.cs` | `BeforePayCost, None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `P_174` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_174.cs` | `BeforePayCost, None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `P_177` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_177.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 4 |
| `P_179` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_179.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 4 |
| `P_182` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_182.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-158#10463@base` | `P-158` | 10463 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Tamer/P_158.asset` |
| `P-158#10464@P1` | `P-158` | 10464 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Tamer/P_158_P1.asset` |
| `P-159#10432@P1` | `P-159` | 10432 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_159_P1.asset` |
| `P-159#10465@base` | `P-159` | 10465 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_159.asset` |
| `P-160#10447@base` | `P-160` | 10447 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_160.asset` |
| `P-160#9271@P1` | `P-160` | 9271 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_160_P1.asset` |
| `P-161#10441@base` | `P-161` | 10441 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_161.asset` |
| `P-161#9272@P1` | `P-161` | 9272 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_161_P1.asset` |
| `P-171#5184@base` | `P-171` | 5184 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_171.asset` |
| `P-172#5185@base` | `P-172` | 5185 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_172.asset` |
| `P-174#5187@base` | `P-174` | 5187 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_174.asset` |
| `P-177#5297@base` | `P-177` | 5297 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/DigiEgg/P_177.asset` |
| `P-177#5298@P1` | `P-177` | 5298 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/DigiEgg/P_177_P1.asset` |
| `P-177#9289@P2` | `P-177` | 9289 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/DigiEgg/P_177_P2.asset` |
| `P-177#9290@P3` | `P-177` | 9290 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/DigiEgg/P_177_P3.asset` |
| `P-179#5301@base` | `P-179` | 5301 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_179.asset` |
| `P-179#5302@P1` | `P-179` | 5302 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_179_P1.asset` |
| `P-179#9293@P2` | `P-179` | 9293 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_179_P2.asset` |
| `P-179#9294@P3` | `P-179` | 9294 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_179_P3.asset` |
| `P-182#5434@base` | `P-182` | 5434 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_182.asset` |
| `P-182#9299@P1` | `P-182` | 9299 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_182_P1.asset` |
| `P-182#9300@P2` | `P-182` | 9300 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_182_P2.asset` |

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
