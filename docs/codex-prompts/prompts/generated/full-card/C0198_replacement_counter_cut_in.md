# C0198_replacement_counter_cut_in - replacement/counter/cut-in card porting 13

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0198_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT19_011` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_011.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `BT19_014` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_014.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 2 |
| `BT19_018` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_018.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, static_or_continuous` | `-` | 1 |
| `BT19_025` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_025.cs` | `None, OnAllyAttack, OnEndAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress, SelectDigiXros` | 1 |
| `BT19_029` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_029.cs` | `OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectSecurity` | 1 |
| `BT19_031` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_031.cs` | `None, OnAllyAttack, OnDestroyedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT19_032` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_032.cs` | `OnDestroyedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT19_036` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_036.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectSecurity, SelectJogress` | 1 |
| `BT19_037` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_037.cs` | `OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 4 |
| `BT19_043` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_043.cs` | `None, OnEndTurn, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT19-011#5012@base` | `BT19-011` | 5012 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_011.asset` |
| `BT19-011#5013@P1` | `BT19-011` | 5013 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_011_P1.asset` |
| `BT19-011#5014@P2` | `BT19-011` | 5014 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_011_P2.asset` |
| `BT19-014#5017@base` | `BT19-014` | 5017 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_014.asset` |
| `BT19-014#6828@P1` | `BT19-014` | 6828 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_014_P1.asset` |
| `BT19-018#3982@base` | `BT19-018` | 3982 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_018.asset` |
| `BT19-025#5022@base` | `BT19-025` | 5022 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_025.asset` |
| `BT19-029#3989@base` | `BT19-029` | 3989 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_029.asset` |
| `BT19-031#5025@base` | `BT19-031` | 5025 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_031.asset` |
| `BT19-032#3990@base` | `BT19-032` | 3990 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_032.asset` |
| `BT19-036#3992@base` | `BT19-036` | 3992 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_036.asset` |
| `BT19-037#5029@base` | `BT19-037` | 5029 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_037.asset` |
| `BT19-037#5030@P1` | `BT19-037` | 5030 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_037_P1.asset` |
| `BT19-037#5031@P2` | `BT19-037` | 5031 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_037_P2.asset` |
| `BT19-037#8280@P3` | `BT19-037` | 8280 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_037_P3.asset` |
| `BT19-043#3999@base` | `BT19-043` | 3999 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_043.asset` |
| `BT19-043#4000@P1` | `BT19-043` | 4000 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_043_P1.asset` |

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
