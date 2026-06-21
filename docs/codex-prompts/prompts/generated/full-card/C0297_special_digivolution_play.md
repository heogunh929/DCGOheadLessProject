# C0297_special_digivolution_play - special digivolution/play mechanics card porting 62

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0297_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT21_043` | `DCGO/Assets/Scripts/CardEffect/BT21/Yellow/BT21_043.cs` | `None, OnDeclaration, OnEnterFieldAnyone, SecuritySkill, WhenLinked` | `linked, max_count_per_turn, modifier_duration, security, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT21_044` | `DCGO/Assets/Scripts/CardEffect/BT21/Yellow/BT21_044.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 3 |
| `BT21_045` | `DCGO/Assets/Scripts/CardEffect/BT21/Yellow/BT21_045.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT21_046` | `DCGO/Assets/Scripts/CardEffect/BT21/Green/BT21_046.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 1 |
| `BT21_047` | `DCGO/Assets/Scripts/CardEffect/BT21/Green/BT21_047.cs` | `None, OnDeclaration, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, linked, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `BT21_053` | `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_053.cs` | `None, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `linked, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT21_054` | `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_054.cs` | `None, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `linked, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT21_057` | `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_057.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `BT21_058` | `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_058.cs` | `OnDigivolutionCardReturnToDeckBottom, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT21_059` | `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_059.cs` | `None, OnDeclaration, WhenLinked` | `inherited, linked, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress, SelectAppFusion` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT21-043#5354@base` | `BT21-043` | 5354 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_043.asset` |
| `BT21-044#5355@base` | `BT21-044` | 5355 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_044.asset` |
| `BT21-044#8397@P1` | `BT21-044` | 8397 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_044_P1.asset` |
| `BT21-044#8398@P2` | `BT21-044` | 8398 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_044_P2.asset` |
| `BT21-045#5356@base` | `BT21-045` | 5356 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_045.asset` |
| `BT21-046#5357@base` | `BT21-046` | 5357 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Green/Digimon/BT21_046.asset` |
| `BT21-047#5358@base` | `BT21-047` | 5358 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Green/Digimon/BT21_047.asset` |
| `BT21-053#5367@base` | `BT21-053` | 5367 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_053.asset` |
| `BT21-054#5368@base` | `BT21-054` | 5368 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_054.asset` |
| `BT21-057#5372@base` | `BT21-057` | 5372 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_057.asset` |
| `BT21-058#5373@base` | `BT21-058` | 5373 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_058.asset` |
| `BT21-059#5374@base` | `BT21-059` | 5374 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_059.asset` |

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
