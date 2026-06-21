# C0373_special_digivolution_play - special digivolution/play mechanics card porting 138

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0373_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX5_026` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_026.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 4 |
| `EX5_027` | `DCGO/Assets/Scripts/CardEffect/EX5/Yellow/EX5_027.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX5_030` | `DCGO/Assets/Scripts/CardEffect/EX5/Yellow/EX5_030.cs` | `None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX5_032` | `DCGO/Assets/Scripts/CardEffect/EX5/Yellow/EX5_032.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX5_034` | `DCGO/Assets/Scripts/CardEffect/EX5/Yellow/EX5_034.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX5_037` | `DCGO/Assets/Scripts/CardEffect/EX5/Green/EX5_037.cs` | `OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `EX5_038` | `DCGO/Assets/Scripts/CardEffect/EX5/Green/EX5_038.cs` | `OnDetermineDoSecurityCheck, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `EX5_040` | `DCGO/Assets/Scripts/CardEffect/EX5/Green/EX5_040.cs` | `OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `EX5_043` | `DCGO/Assets/Scripts/CardEffect/EX5/Green/EX5_043.cs` | `None, OnDeclaration, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX5_044` | `DCGO/Assets/Scripts/CardEffect/EX5/Black/EX5_044.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX5-026#3065@base` | `EX5-026` | 3065 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_026.asset` |
| `EX5-026#4217@P1` | `EX5-026` | 4217 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_026_P1.asset` |
| `EX5-026#4220@P2` | `EX5-026` | 4220 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_026_P2.asset` |
| `EX5-026#4223@P3` | `EX5-026` | 4223 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_026_P3.asset` |
| `EX5-027#3066@base` | `EX5-027` | 3066 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_027.asset` |
| `EX5-030#3069@base` | `EX5-030` | 3069 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_030.asset` |
| `EX5-032#3071@base` | `EX5-032` | 3071 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_032.asset` |
| `EX5-034#3073@base` | `EX5-034` | 3073 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_034.asset` |
| `EX5-034#4227@P1` | `EX5-034` | 4227 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Yellow/Digimon/EX5_034_P1.asset` |
| `EX5-037#3076@base` | `EX5-037` | 3076 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_037.asset` |
| `EX5-038#3077@base` | `EX5-038` | 3077 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_038.asset` |
| `EX5-040#3079@base` | `EX5-040` | 3079 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_040.asset` |
| `EX5-043#3082@base` | `EX5-043` | 3082 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_043.asset` |
| `EX5-043#4230@P1` | `EX5-043` | 4230 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_043_P1.asset` |
| `EX5-044#3083@base` | `EX5-044` | 3083 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Black/Digimon/EX5_044.asset` |

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
