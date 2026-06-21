# C0389_special_digivolution_play - special digivolution/play mechanics card porting 154

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0389_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 34
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_025` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_025.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectBurstDigivolution` | 2 |
| `P_026` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_026.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous` | `SelectBurstDigivolution` | 2 |
| `P_027` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_027.cs` | `OnDeclaration` | `max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBurstDigivolution` | 2 |
| `P_029` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_029.cs` | `None, OnAllyAttack, OnEndTurn` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `P_030` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_030.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `P_032` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_032.cs` | `OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 3 |
| `P_034` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_034.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `P_035` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_035.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 6 |
| `P_036` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_036.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 7 |
| `P_037` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_037.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 7 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-025#10345@P1` | `P-025` | 10345 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_025_P1.asset` |
| `P-025#6050@base` | `P-025` | 6050 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_025.asset` |
| `P-026#10346@P1` | `P-026` | 10346 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_026_P1.asset` |
| `P-026#6051@base` | `P-026` | 6051 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_026.asset` |
| `P-027#10347@P1` | `P-027` | 10347 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_027_P1.asset` |
| `P-027#6052@base` | `P-027` | 6052 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_027.asset` |
| `P-029#6056@base` | `P-029` | 6056 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_029.asset` |
| `P-029#6057@P1` | `P-029` | 6057 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_029_P1.asset` |
| `P-030#6058@base` | `P-030` | 6058 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_030.asset` |
| `P-030#6059@P1` | `P-030` | 6059 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_030_P1.asset` |
| `P-032#10350@P2` | `P-032` | 10350 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_032_P2.asset` |
| `P-032#6061@base` | `P-032` | 6061 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_032.asset` |
| `P-032#6062@P1` | `P-032` | 6062 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_032_P1.asset` |
| `P-034#6066@base` | `P-034` | 6066 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_034.asset` |
| `P-035#10352@P2` | `P-035` | 10352 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_035_P2.asset` |
| `P-035#10357@P3` | `P-035` | 10357 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_035_P3.asset` |
| `P-035#6067@base` | `P-035` | 6067 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_035.asset` |
| `P-035#6068@P1` | `P-035` | 6068 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_035_P1.asset` |
| `P-035#9211@P4` | `P-035` | 9211 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_035_P4.asset` |
| `P-035#9212@P5` | `P-035` | 9212 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_035_P5.asset` |
| `P-036#10354@P2` | `P-036` | 10354 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_036_P2.asset` |
| `P-036#10355@P3` | `P-036` | 10355 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_036_P3.asset` |
| `P-036#10356@P4` | `P-036` | 10356 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_036_P4.asset` |
| `P-036#6069@base` | `P-036` | 6069 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_036.asset` |
| `P-036#6070@P1` | `P-036` | 6070 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_036_P1.asset` |
| `P-036#9213@P5` | `P-036` | 9213 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_036_P5.asset` |
| `P-036#9214@P6` | `P-036` | 9214 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_036_P6.asset` |
| `P-037#10321@P2` | `P-037` | 10321 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_037_P2.asset` |
| `P-037#10322@P3` | `P-037` | 10322 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_037_P3.asset` |
| `P-037#10323@P4` | `P-037` | 10323 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_037_P4.asset` |
| `P-037#6071@base` | `P-037` | 6071 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_037.asset` |
| `P-037#6072@P1` | `P-037` | 6072 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_037_P1.asset` |
| `P-037#9215@P5` | `P-037` | 9215 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_037_P5.asset` |
| `P-037#9216@P7` | `P-037` | 9216 | `P7` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_037_P7.asset` |

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
