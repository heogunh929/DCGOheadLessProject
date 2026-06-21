# C0379_special_digivolution_play - special digivolution/play mechanics card porting 144

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0379_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX6_073` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_073.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `EX6_074` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_074.cs` | `OnEndTurn, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `EX7_007` | `DCGO/Assets/Scripts/CardEffect/EX7/Red/EX7_007.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX7_009` | `DCGO/Assets/Scripts/CardEffect/EX7/Red/EX7_009.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX7_014` | `DCGO/Assets/Scripts/CardEffect/EX7/Red/EX7_014.cs` | `OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX7_016` | `DCGO/Assets/Scripts/CardEffect/EX7/Blue/EX7_016.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `EX7_025` | `DCGO/Assets/Scripts/CardEffect/EX7/Yellow/EX7_025.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `EX7_032` | `DCGO/Assets/Scripts/CardEffect/EX7/Green/EX7_032.cs` | `OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX7_037` | `DCGO/Assets/Scripts/CardEffect/EX7/Green/EX7_037.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX7_042` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_042.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX6-073#3522@base` | `EX6-073` | 3522 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_073.asset` |
| `EX6-073#3523@P1` | `EX6-073` | 3523 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_073_P1.asset` |
| `EX6-073#3524@P2` | `EX6-073` | 3524 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_073_P2.asset` |
| `EX6-074#3539@base` | `EX6-074` | 3539 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Tamer/EX6_074.asset` |
| `EX6-074#3540@P1` | `EX6-074` | 3540 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Tamer/EX6_074_P1.asset` |
| `EX6-074#9164@P2` | `EX6-074` | 9164 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Tamer/EX6_074_P2.asset` |
| `EX7-007#3689@base` | `EX7-007` | 3689 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_007.asset` |
| `EX7-009#3692@base` | `EX7-009` | 3692 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_009.asset` |
| `EX7-009#3693@P1` | `EX7-009` | 3693 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_009_P1.asset` |
| `EX7-014#3701@base` | `EX7-014` | 3701 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_014.asset` |
| `EX7-014#3702@P1` | `EX7-014` | 3702 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_014_P1.asset` |
| `EX7-016#3705@base` | `EX7-016` | 3705 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_016.asset` |
| `EX7-016#3706@P1` | `EX7-016` | 3706 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_016_P1.asset` |
| `EX7-025#3722@base` | `EX7-025` | 3722 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_025.asset` |
| `EX7-025#3723@P1` | `EX7-025` | 3723 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_025_P1.asset` |
| `EX7-032#3736@base` | `EX7-032` | 3736 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_032.asset` |
| `EX7-032#3737@P1` | `EX7-032` | 3737 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_032_P1.asset` |
| `EX7-037#3746@base` | `EX7-037` | 3746 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_037.asset` |
| `EX7-037#3747@P1` | `EX7-037` | 3747 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_037_P1.asset` |
| `EX7-042#3755@base` | `EX7-042` | 3755 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_042.asset` |
| `EX7-042#3756@P1` | `EX7-042` | 3756 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_042_P1.asset` |

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
