# C0237_special_digivolution_play - special digivolution/play mechanics card porting 2

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0237_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 30
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `AD1_015` | `DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_015.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectJogress` | 2 |
| `AD1_016` | `DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_016.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectJogress` | 2 |
| `AD1_020` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_020.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectInteger, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `AD1_021` | `DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_021.cs` | `None, OnEndTurn, OnTappedAnyone, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `AD1_024` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_024.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectJogress` | 3 |
| `AD1_025` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_025.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, OnLeaveFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress, SelectAssembly` | 2 |
| `BT10_008` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_008.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 5 |
| `BT10_011` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_011.cs` | `None, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `SelectJogress` | 6 |
| `BT10_016` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_016.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectAttackTarget, SelectJogress` | 3 |
| `BT10_019` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_019.cs` | `OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectBoolean, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `AD1-015#7841@base` | `AD1-015` | 7841 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_015.asset` |
| `AD1-015#7842@P1` | `AD1-015` | 7842 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_015_P1.asset` |
| `AD1-016#7843@base` | `AD1-016` | 7843 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016.asset` |
| `AD1-016#7844@P1` | `AD1-016` | 7844 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016_P1.asset` |
| `AD1-020#7850@base` | `AD1-020` | 7850 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_020.asset` |
| `AD1-020#7851@P1` | `AD1-020` | 7851 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_020_P1.asset` |
| `AD1-021#7852@base` | `AD1-021` | 7852 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021.asset` |
| `AD1-021#7853@P1` | `AD1-021` | 7853 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021_P1.asset` |
| `AD1-024#7857@base` | `AD1-024` | 7857 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024.asset` |
| `AD1-024#7858@P1` | `AD1-024` | 7858 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024_P1.asset` |
| `AD1-024#7859@P2` | `AD1-024` | 7859 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024_P2.asset` |
| `AD1-025#7860@base` | `AD1-025` | 7860 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025.asset` |
| `AD1-025#7861@P1` | `AD1-025` | 7861 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025_P1.asset` |
| `BT10-008#2040@base` | `BT10-008` | 2040 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_008.asset` |
| `BT10-008#4288@P0` | `BT10-008` | 4288 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_008_P0.asset` |
| `BT10-008#4289@P1` | `BT10-008` | 4289 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_008_P1.asset` |
| `BT10-008#4290@P2` | `BT10-008` | 4290 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_008_P2.asset` |
| `BT10-008#4291@P3` | `BT10-008` | 4291 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_008_P3.asset` |
| `BT10-011#2043@base` | `BT10-011` | 2043 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_011.asset` |
| `BT10-011#2044@P1` | `BT10-011` | 2044 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_011_P1.asset` |
| `BT10-011#4293@P0` | `BT10-011` | 4293 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_011_P0.asset` |
| `BT10-011#4294@P2` | `BT10-011` | 4294 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_011_P2.asset` |
| `BT10-011#4295@P3` | `BT10-011` | 4295 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_011_P3.asset` |
| `BT10-011#4296@P4` | `BT10-011` | 4296 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_011_P4.asset` |
| `BT10-016#2050@base` | `BT10-016` | 2050 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_016.asset` |
| `BT10-016#2051@P1` | `BT10-016` | 2051 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_016_P1.asset` |
| `BT10-016#4300@P2` | `BT10-016` | 4300 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_016_P2.asset` |
| `BT10-019#2054@base` | `BT10-019` | 2054 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_019.asset` |
| `BT10-019#4302@P0` | `BT10-019` | 4302 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_019_P0.asset` |
| `BT10-019#4303@P1` | `BT10-019` | 4303 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_019_P1.asset` |

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
