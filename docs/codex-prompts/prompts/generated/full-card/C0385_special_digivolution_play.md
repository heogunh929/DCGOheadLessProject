# C0385_special_digivolution_play - special digivolution/play mechanics card porting 150

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0385_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX9_071` | `DCGO/Assets/Scripts/CardEffect/EX9/Blue/EX9_071.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `LM_004` | `DCGO/Assets/Scripts/CardEffect/LM/Blue/LM_004.cs` | `OnDiscardHand, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 1 |
| `LM_006` | `DCGO/Assets/Scripts/CardEffect/LM/Blue/LM_006.cs` | `OnDeclaration, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `LM_015` | `DCGO/Assets/Scripts/CardEffect/LM/Black/LM_015.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `LM_016` | `DCGO/Assets/Scripts/CardEffect/LM/Purple/LM_016.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 4 |
| `LM_020` | `DCGO/Assets/Scripts/CardEffect/LM/Yellow/LM_020.cs` | `OnEnterFieldAnyone, OnStartTurn` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 2 |
| `LM_033` | `DCGO/Assets/Scripts/CardEffect/LM/Red/LM_033.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `LM_034` | `DCGO/Assets/Scripts/CardEffect/LM/Blue/LM_034.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `LM_035` | `DCGO/Assets/Scripts/CardEffect/LM/Yellow/LM_035.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `LM_036` | `DCGO/Assets/Scripts/CardEffect/LM/Green/LM_036.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX9-071#6968@base` | `EX9-071` | 6968 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Option/EX9_071.asset` |
| `EX9-071#6969@P1` | `EX9-071` | 6969 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Option/EX9_071_P1.asset` |
| `LM-004#3250@base` | `LM-004` | 3250 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Digimon/LM_004.asset` |
| `LM-006#3252@base` | `LM-006` | 3252 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Digimon/LM_006.asset` |
| `LM-015#3261@base` | `LM-015` | 3261 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Black/Digimon/LM_015.asset` |
| `LM-016#3262@base` | `LM-016` | 3262 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Digimon/LM_016.asset` |
| `LM-016#7865@P1` | `LM-016` | 7865 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Digimon/LM_016_P1.asset` |
| `LM-016#7866@P2` | `LM-016` | 7866 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Digimon/LM_016_P2.asset` |
| `LM-016#7867@P3` | `LM-016` | 7867 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Digimon/LM_016_P3.asset` |
| `LM-020#3266@base` | `LM-020` | 3266 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Yellow/Digimon/LM_020.asset` |
| `LM-020#6827@P1` | `LM-020` | 6827 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/LM/Yellow/Digimon/LM_020_P1.asset` |
| `LM-033#4043@base` | `LM-033` | 4043 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Red/Option/LM_033.asset` |
| `LM-034#4044@base` | `LM-034` | 4044 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Option/LM_034.asset` |
| `LM-035#4045@base` | `LM-035` | 4045 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Yellow/Option/LM_035.asset` |
| `LM-036#4046@base` | `LM-036` | 4046 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Option/LM_036.asset` |

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
