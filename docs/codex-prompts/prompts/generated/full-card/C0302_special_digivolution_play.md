# C0302_special_digivolution_play - special digivolution/play mechanics card porting 67

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0302_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT22_031` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_031.cs` | `None, OnEnterFieldAnyone` | `background, inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT22_033` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_033.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `linked, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `BT22_035` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_035.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `inherited, linked, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean` | 1 |
| `BT22_037` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_037.cs` | `None, OnAllyAttack, OnDiscardSecurity, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT22_039` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_039.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `BT22_042` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_042.cs` | `None, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT22_045` | `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_045.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectSecurity, SelectJogress` | 1 |
| `BT22_050` | `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_050.cs` | `None, OnDeclaration, OnEnterFieldAnyone, SecuritySkill, WhenLinked` | `linked, max_count_per_turn, security, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT22_055` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_055.cs` | `None, OnDeclaration, OnEnterFieldAnyone` | `inherited, linked, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 1 |
| `BT22_058` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_058.cs` | `None, OnDeclaration, WhenLinked` | `linked, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT22-031#7028@base` | `BT22-031` | 7028 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_031.asset` |
| `BT22-033#7030@base` | `BT22-033` | 7030 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_033.asset` |
| `BT22-035#7032@base` | `BT22-035` | 7032 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_035.asset` |
| `BT22-037#7034@base` | `BT22-037` | 7034 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_037.asset` |
| `BT22-039#7036@base` | `BT22-039` | 7036 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_039.asset` |
| `BT22-039#7037@P1` | `BT22-039` | 7037 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_039_P1.asset` |
| `BT22-042#7040@base` | `BT22-042` | 7040 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_042.asset` |
| `BT22-042#8431@P1` | `BT22-042` | 8431 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_042_P1.asset` |
| `BT22-045#7045@base` | `BT22-045` | 7045 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_045.asset` |
| `BT22-050#7052@base` | `BT22-050` | 7052 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_050.asset` |
| `BT22-055#7059@base` | `BT22-055` | 7059 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_055.asset` |
| `BT22-058#7064@base` | `BT22-058` | 7064 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_058.asset` |

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
