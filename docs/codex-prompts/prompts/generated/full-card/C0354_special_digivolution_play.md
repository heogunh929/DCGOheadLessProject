# C0354_special_digivolution_play - special digivolution/play mechanics card porting 119

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0354_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX11_009` | `DCGO/Assets/Scripts/CardEffect/EX11/Red/EX11_009.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX11_011` | `DCGO/Assets/Scripts/CardEffect/EX11/Red/EX11_011.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX11_014` | `DCGO/Assets/Scripts/CardEffect/EX11/Blue/EX11_014.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `EX11_015` | `DCGO/Assets/Scripts/CardEffect/EX11/Blue/EX11_015.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX11_020` | `DCGO/Assets/Scripts/CardEffect/EX11/Yellow/EX11_020.cs` | `None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX11_021` | `DCGO/Assets/Scripts/CardEffect/EX11/Yellow/EX11_021.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX11_028` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_028.cs` | `OnEndBattle, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX11_029` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_029.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnMove, WhenLinked` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectInteger, SelectSecurity, SelectJogress` | 2 |
| `EX11_032` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_032.cs` | `OnDeclaration, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 3 |
| `EX11_033` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_033.cs` | `None, OnEndBattle, OnEnterFieldAnyone, OnMove, WhenLinked` | `inherited, linked, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX11-009#7673@base` | `EX11-009` | 7673 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_009.asset` |
| `EX11-009#7674@P1` | `EX11-009` | 7674 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_009_P1.asset` |
| `EX11-011#7677@base` | `EX11-011` | 7677 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_011.asset` |
| `EX11-011#7678@P1` | `EX11-011` | 7678 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_011_P1.asset` |
| `EX11-014#7683@base` | `EX11-014` | 7683 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_014.asset` |
| `EX11-014#7684@P1` | `EX11-014` | 7684 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_014_P1.asset` |
| `EX11-015#7685@base` | `EX11-015` | 7685 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_015.asset` |
| `EX11-015#7686@P1` | `EX11-015` | 7686 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Blue/Digimon/EX11_015_P1.asset` |
| `EX11-020#7696@base` | `EX11-020` | 7696 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_020.asset` |
| `EX11-020#7697@P1` | `EX11-020` | 7697 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_020_P1.asset` |
| `EX11-021#7698@base` | `EX11-021` | 7698 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_021.asset` |
| `EX11-021#7699@P1` | `EX11-021` | 7699 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Digimon/EX11_021_P1.asset` |
| `EX11-028#7714@base` | `EX11-028` | 7714 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_028.asset` |
| `EX11-028#7715@P1` | `EX11-028` | 7715 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_028_P1.asset` |
| `EX11-029#7716@base` | `EX11-029` | 7716 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_029.asset` |
| `EX11-029#7717@P1` | `EX11-029` | 7717 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_029_P1.asset` |
| `EX11-032#7722@base` | `EX11-032` | 7722 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_032.asset` |
| `EX11-032#7723@P1` | `EX11-032` | 7723 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_032_P1.asset` |
| `EX11-032#9101@P2` | `EX11-032` | 9101 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_032_P2.asset` |
| `EX11-033#7724@base` | `EX11-033` | 7724 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_033.asset` |
| `EX11-033#7725@P1` | `EX11-033` | 7725 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_033_P1.asset` |

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
