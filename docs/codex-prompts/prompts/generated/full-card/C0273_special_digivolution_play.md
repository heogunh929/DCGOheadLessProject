# C0273_special_digivolution_play - special digivolution/play mechanics card porting 38

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0273_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT17_028` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_028.cs` | `None, OnAddHand, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress, SelectDigiXros` | 3 |
| `BT17_030` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_030.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT17_031` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_031.cs` | `OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT17_032` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_032.cs` | `OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT17_034` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_034.cs` | `None, OnDiscardSecurity, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT17_040` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_040.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `BT17_042` | `DCGO/Assets/Scripts/CardEffect/BT17/Green/BT17_042.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT17_043` | `DCGO/Assets/Scripts/CardEffect/BT17/Green/BT17_043.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT17_044` | `DCGO/Assets/Scripts/CardEffect/BT17/Green/BT17_044.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT17_045` | `DCGO/Assets/Scripts/CardEffect/BT17/Green/BT17_045.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT17-028#3571@base` | `BT17-028` | 3571 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_028.asset` |
| `BT17-028#3572@P1` | `BT17-028` | 3572 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_028_P1.asset` |
| `BT17-028#8226@P2` | `BT17-028` | 8226 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_028_P2.asset` |
| `BT17-030#3574@base` | `BT17-030` | 3574 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_030.asset` |
| `BT17-030#3575@P1` | `BT17-030` | 3575 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_030_P1.asset` |
| `BT17-031#3576@base` | `BT17-031` | 3576 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_031.asset` |
| `BT17-031#4850@P1` | `BT17-031` | 4850 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_031_P1.asset` |
| `BT17-031#8227@P2` | `BT17-031` | 8227 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_031_P2.asset` |
| `BT17-032#3577@base` | `BT17-032` | 3577 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_032.asset` |
| `BT17-034#3579@base` | `BT17-034` | 3579 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_034.asset` |
| `BT17-040#3585@base` | `BT17-040` | 3585 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_040.asset` |
| `BT17-040#3586@P1` | `BT17-040` | 3586 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_040_P1.asset` |
| `BT17-042#3590@base` | `BT17-042` | 3590 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_042.asset` |
| `BT17-043#3591@base` | `BT17-043` | 3591 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_043.asset` |
| `BT17-043#4853@P1` | `BT17-043` | 4853 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_043_P1.asset` |
| `BT17-044#3592@base` | `BT17-044` | 3592 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_044.asset` |
| `BT17-044#4854@P0` | `BT17-044` | 4854 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_044_P0.asset` |
| `BT17-045#3593@base` | `BT17-045` | 3593 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_045.asset` |

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
