# C0404_special_digivolution_play - special digivolution/play mechanics card porting 169

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0404_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST17_03` | `DCGO/Assets/Scripts/CardEffect/ST17/Green/ST17_03.cs` | `None, OnDeclaration` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `ST17_04` | `DCGO/Assets/Scripts/CardEffect/ST17/Green/ST17_04.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `ST17_05` | `DCGO/Assets/Scripts/CardEffect/ST17/Green/ST17_05.cs` | `None, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `ST17_07` | `DCGO/Assets/Scripts/CardEffect/ST17/Green/ST17_07.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `ST17_09` | `DCGO/Assets/Scripts/CardEffect/ST17/Green/ST17_09.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 3 |
| `ST17_10` | `DCGO/Assets/Scripts/CardEffect/ST17/Green/ST17_10.cs` | `OnDeclaration, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectSecurity, SelectJogress` | 3 |
| `ST17_11` | `DCGO/Assets/Scripts/CardEffect/ST17/Green/ST17_11.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `ST18_15` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_15.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST19_15` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_15.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST1_13` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_13.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST1-13#34@base` | `ST1-13` | 34 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Option/ST1_13.asset` |
| `ST1-13#35@P1` | `ST1-13` | 35 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Option/ST1_13_P1.asset` |
| `ST1-13#36@P2` | `ST1-13` | 36 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Option/ST1_13_P2.asset` |
| `ST17-03#3270@base` | `ST17-03` | 3270 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_03.asset` |
| `ST17-03#3271@P1` | `ST17-03` | 3271 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_03_P1.asset` |
| `ST17-04#3272@base` | `ST17-04` | 3272 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_04.asset` |
| `ST17-04#3273@P1` | `ST17-04` | 3273 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_04_P1.asset` |
| `ST17-04#4961@P0` | `ST17-04` | 4961 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_04_P0.asset` |
| `ST17-05#3274@base` | `ST17-05` | 3274 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_05.asset` |
| `ST17-07#3277@base` | `ST17-07` | 3277 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_07.asset` |
| `ST17-07#3278@P1` | `ST17-07` | 3278 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_07_P1.asset` |
| `ST17-09#3281@base` | `ST17-09` | 3281 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_09.asset` |
| `ST17-09#3282@P1` | `ST17-09` | 3282 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_09_P1.asset` |
| `ST17-09#4965@P0` | `ST17-09` | 4965 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Digimon/ST17_09_P0.asset` |
| `ST17-10#3283@base` | `ST17-10` | 3283 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Tamer/ST17_10.asset` |
| `ST17-10#4966@P0` | `ST17-10` | 4966 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Tamer/ST17_10_P0.asset` |
| `ST17-10#4967@P1` | `ST17-10` | 4967 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Tamer/ST17_10_P1.asset` |
| `ST17-11#3284@base` | `ST17-11` | 3284 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Option/ST17_11.asset` |
| `ST17-11#4968@P0` | `ST17-11` | 4968 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Option/ST17_11_P0.asset` |
| `ST17-11#4969@P1` | `ST17-11` | 4969 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Option/ST17_11_P1.asset` |
| `ST17-11#4970@P2` | `ST17-11` | 4970 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST17/Green/Option/ST17_11_P2.asset` |
| `ST18-15#3832@base` | `ST18-15` | 3832 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Option/ST18_15.asset` |
| `ST19-15#3847@base` | `ST19-15` | 3847 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Option/ST19_15.asset` |

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
