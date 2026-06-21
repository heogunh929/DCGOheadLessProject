# C0360_special_digivolution_play - special digivolution/play mechanics card porting 125

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0360_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX2_022` | `DCGO/Assets/Scripts/CardEffect/EX2/Yellow/EX2_022.cs` | `None, OnAllyAttack` | `max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 2 |
| `EX2_023` | `DCGO/Assets/Scripts/CardEffect/EX2/Yellow/EX2_023.cs` | `OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX2_024` | `DCGO/Assets/Scripts/CardEffect/EX2/Yellow/EX2_024.cs` | `OnEnterFieldAnyone, OnUseOption` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `EX2_033` | `DCGO/Assets/Scripts/CardEffect/EX2/Black/EX2_033.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX2_035` | `DCGO/Assets/Scripts/CardEffect/EX2/Black/EX2_035.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX2_039` | `DCGO/Assets/Scripts/CardEffect/EX2/Purple/EX2_039.cs` | `None, OnDiscardLibrary, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectCount, SelectOrder, SelectJogress` | 3 |
| `EX2_041` | `DCGO/Assets/Scripts/CardEffect/EX2/Purple/EX2_041.cs` | `None, OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `EX2_044` | `DCGO/Assets/Scripts/CardEffect/EX2/Purple/EX2_044.cs` | `OnAllyAttack, OnDiscardLibrary, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 5 |
| `EX2_045` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_045.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `EX2_046` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_046.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX2-022#1951@base` | `EX2-022` | 1951 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_022.asset` |
| `EX2-022#1952@P1` | `EX2-022` | 1952 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_022_P1.asset` |
| `EX2-023#1953@base` | `EX2-023` | 1953 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_023.asset` |
| `EX2-023#1954@P1` | `EX2-023` | 1954 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_023_P1.asset` |
| `EX2-024#1955@base` | `EX2-024` | 1955 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_024.asset` |
| `EX2-024#1956@P1` | `EX2-024` | 1956 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_024_P1.asset` |
| `EX2-024#9110@P2` | `EX2-024` | 9110 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_024_P2.asset` |
| `EX2-033#1970@base` | `EX2-033` | 1970 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Digimon/EX2_033.asset` |
| `EX2-035#1972@base` | `EX2-035` | 1972 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Digimon/EX2_035.asset` |
| `EX2-035#1973@P1` | `EX2-035` | 1973 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Digimon/EX2_035_P1.asset` |
| `EX2-039#1978@base` | `EX2-039` | 1978 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_039.asset` |
| `EX2-039#1979@P1` | `EX2-039` | 1979 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_039_P1.asset` |
| `EX2-039#1980@P2` | `EX2-039` | 1980 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_039_P2.asset` |
| `EX2-041#1982@base` | `EX2-041` | 1982 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_041.asset` |
| `EX2-041#1983@P1` | `EX2-041` | 1983 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_041_P1.asset` |
| `EX2-044#1987@base` | `EX2-044` | 1987 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_044.asset` |
| `EX2-044#1988@P1` | `EX2-044` | 1988 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_044_P1.asset` |
| `EX2-044#9111@P2` | `EX2-044` | 9111 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_044_P2.asset` |
| `EX2-044#9112@P3` | `EX2-044` | 9112 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_044_P3.asset` |
| `EX2-044#9113@P4` | `EX2-044` | 9113 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_044_P4.asset` |
| `EX2-045#1989@base` | `EX2-045` | 1989 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_045.asset` |
| `EX2-045#1990@P1` | `EX2-045` | 1990 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_045_P1.asset` |
| `EX2-045#1991@P2` | `EX2-045` | 1991 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_045_P2.asset` |
| `EX2-046#1992@base` | `EX2-046` | 1992 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_046.asset` |

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
