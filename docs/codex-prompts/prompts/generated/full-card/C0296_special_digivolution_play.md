# C0296_special_digivolution_play - special digivolution/play mechanics card porting 61

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0296_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT21_020` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_020.cs` | `BeforePayCost, None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 1 |
| `BT21_021` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_021.cs` | `None, OnDestroyedAnyone, OnEndAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress, SelectDigiXros` | 3 |
| `BT21_023` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_023.cs` | `None, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `inherited, linked, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean` | 2 |
| `BT21_027` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_027.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 1 |
| `BT21_030` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_030.cs` | `BeforePayCost, None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 1 |
| `BT21_032` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_032.cs` | `None` | `inherited, static_or_continuous` | `SelectCard, SelectJogress` | 1 |
| `BT21_039` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_039.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `BT21_040` | `DCGO/Assets/Scripts/CardEffect/BT21/Yellow/BT21_040.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `BT21_041` | `DCGO/Assets/Scripts/CardEffect/BT21/Yellow/BT21_041.cs` | `None, OnDeclaration, SecuritySkill` | `inherited, linked, security, static_or_continuous, zone_movement` | `-` | 1 |
| `BT21_042` | `DCGO/Assets/Scripts/CardEffect/BT21/Yellow/BT21_042.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT21-020#5328@base` | `BT21-020` | 5328 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_020.asset` |
| `BT21-021#5329@base` | `BT21-021` | 5329 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_021.asset` |
| `BT21-021#8378@P1` | `BT21-021` | 8378 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_021_P1.asset` |
| `BT21-021#8379@P2` | `BT21-021` | 8379 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_021_P2.asset` |
| `BT21-023#5331@base` | `BT21-023` | 5331 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_023.asset` |
| `BT21-023#5332@P1` | `BT21-023` | 5332 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_023_P1.asset` |
| `BT21-027#5336@base` | `BT21-027` | 5336 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_027.asset` |
| `BT21-030#5341@base` | `BT21-030` | 5341 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_030.asset` |
| `BT21-032#5343@base` | `BT21-032` | 5343 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_032.asset` |
| `BT21-039#5350@base` | `BT21-039` | 5350 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_039.asset` |
| `BT21-040#5351@base` | `BT21-040` | 5351 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_040.asset` |
| `BT21-040#8393@P1` | `BT21-040` | 8393 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_040_P1.asset` |
| `BT21-040#8394@P2` | `BT21-040` | 8394 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_040_P2.asset` |
| `BT21-041#5352@base` | `BT21-041` | 5352 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_041.asset` |
| `BT21-042#5353@base` | `BT21-042` | 5353 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_042.asset` |
| `BT21-042#8395@P1` | `BT21-042` | 8395 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_042_P1.asset` |
| `BT21-042#8396@P2` | `BT21-042` | 8396 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Yellow/Digimon/BT21_042_P2.asset` |

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
