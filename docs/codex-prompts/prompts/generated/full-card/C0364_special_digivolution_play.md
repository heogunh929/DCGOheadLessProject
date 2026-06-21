# C0364_special_digivolution_play - special digivolution/play mechanics card porting 129

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0364_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX3_024` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_024.cs` | `None, OnStartMainPhase, OnTappedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `EX3_025` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_025.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX3_026` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_026.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX3_027` | `DCGO/Assets/Scripts/CardEffect/EX3/Yellow/EX3_027.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `EX3_033` | `DCGO/Assets/Scripts/CardEffect/EX3/Yellow/EX3_033.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `EX3_034` | `DCGO/Assets/Scripts/CardEffect/EX3/Yellow/EX3_034.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `EX3_035` | `DCGO/Assets/Scripts/CardEffect/EX3/Yellow/EX3_035.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectSecurity, SelectJogress` | 2 |
| `EX3_036` | `DCGO/Assets/Scripts/CardEffect/EX3/Yellow/EX3_036.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX3_037` | `DCGO/Assets/Scripts/CardEffect/EX3/Green/EX3_037.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `EX3_039` | `DCGO/Assets/Scripts/CardEffect/EX3/Green/EX3_039.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX3-024#2199@base` | `EX3-024` | 2199 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_024.asset` |
| `EX3-024#2200@P1` | `EX3-024` | 2200 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_024_P1.asset` |
| `EX3-025#2201@base` | `EX3-025` | 2201 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_025.asset` |
| `EX3-025#2202@P1` | `EX3-025` | 2202 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_025_P1.asset` |
| `EX3-026#2203@base` | `EX3-026` | 2203 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_026.asset` |
| `EX3-026#2204@P1` | `EX3-026` | 2204 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Digimon/EX3_026_P1.asset` |
| `EX3-027#2205@base` | `EX3-027` | 2205 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_027.asset` |
| `EX3-033#2211@base` | `EX3-033` | 2211 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_033.asset` |
| `EX3-034#2212@base` | `EX3-034` | 2212 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_034.asset` |
| `EX3-035#2213@base` | `EX3-035` | 2213 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_035.asset` |
| `EX3-035#2214@P1` | `EX3-035` | 2214 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_035_P1.asset` |
| `EX3-036#2215@base` | `EX3-036` | 2215 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_036.asset` |
| `EX3-036#2216@P1` | `EX3-036` | 2216 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Digimon/EX3_036_P1.asset` |
| `EX3-037#2217@base` | `EX3-037` | 2217 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_037.asset` |
| `EX3-037#2218@P1` | `EX3-037` | 2218 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_037_P1.asset` |
| `EX3-039#2220@base` | `EX3-039` | 2220 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_039.asset` |

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
