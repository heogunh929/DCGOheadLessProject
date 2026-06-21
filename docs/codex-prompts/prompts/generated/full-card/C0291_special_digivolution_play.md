# C0291_special_digivolution_play - special digivolution/play mechanics card porting 56

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0291_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT20_024` | `DCGO/Assets/Scripts/CardEffect/BT20/Blue/BT20_024.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT20_025` | `DCGO/Assets/Scripts/CardEffect/BT20/Blue/BT20_025.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT20_026` | `DCGO/Assets/Scripts/CardEffect/BT20/Blue/BT20_026.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `BT20_028` | `DCGO/Assets/Scripts/CardEffect/BT20/Blue/BT20_028.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT20_029` | `DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_029.cs` | `BeforePayCost, None, OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT20_032` | `DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_032.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT20_035` | `DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_035.cs` | `None, OnAddDigivolutionCards, OnDestroyedAnyone, OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `BT20_036` | `DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_036.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `BT20_037` | `DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_037.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT20_038` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_038.cs` | `BeforePayCost, None, OnDetermineDoSecurityCheck` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT20-024#5103@base` | `BT20-024` | 5103 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_024.asset` |
| `BT20-025#5104@base` | `BT20-025` | 5104 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_025.asset` |
| `BT20-026#5105@base` | `BT20-026` | 5105 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_026.asset` |
| `BT20-026#8332@P1` | `BT20-026` | 8332 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_026_P1.asset` |
| `BT20-028#5107@base` | `BT20-028` | 5107 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_028.asset` |
| `BT20-028#5208@P1` | `BT20-028` | 5208 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_028_P1.asset` |
| `BT20-029#5108@base` | `BT20-029` | 5108 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_029.asset` |
| `BT20-029#8333@P1` | `BT20-029` | 8333 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_029_P1.asset` |
| `BT20-032#5111@base` | `BT20-032` | 5111 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_032.asset` |
| `BT20-032#8334@P1` | `BT20-032` | 8334 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_032_P1.asset` |
| `BT20-035#5114@base` | `BT20-035` | 5114 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_035.asset` |
| `BT20-036#5115@base` | `BT20-036` | 5115 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_036.asset` |
| `BT20-037#5116@base` | `BT20-037` | 5116 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_037.asset` |
| `BT20-037#5212@P1` | `BT20-037` | 5212 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_037_P1.asset` |
| `BT20-038#5117@base` | `BT20-038` | 5117 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_038.asset` |

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
