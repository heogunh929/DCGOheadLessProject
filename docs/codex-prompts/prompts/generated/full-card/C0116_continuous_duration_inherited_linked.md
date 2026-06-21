# C0116_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 12

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0116_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 30
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT5_006` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_006.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT5_022` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_022.cs` | `OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 6 |
| `BT5_030` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_030.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT5_038` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_038.cs` | `None` | `inherited, static_or_continuous` | `-` | 3 |
| `BT5_044` | `DCGO/Assets/Scripts/CardEffect/BT5/Yellow/BT5_044.cs` | `None, OnMove` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 5 |
| `BT5_053` | `DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_053.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT5_062` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_062.cs` | `None, OnEndBattle` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous` | `-` | 2 |
| `BT5_068` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_068.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT5_069` | `DCGO/Assets/Scripts/CardEffect/BT5/Black/BT5_069.cs` | `None` | `inherited, static_or_continuous` | `-` | 3 |
| `BT5_071` | `DCGO/Assets/Scripts/CardEffect/BT5/Purple/BT5_071.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous` | `-` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT5-006#8575@P0` | `BT5-006` | 8575 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/DigiEgg/BT5_006_P0.asset` |
| `BT5-006#935@base` | `BT5-006` | 935 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/DigiEgg/BT5_006.asset` |
| `BT5-022#6780@P0` | `BT5-022` | 6780 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_022_P0.asset` |
| `BT5-022#966@base` | `BT5-022` | 966 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_022.asset` |
| `BT5-030#8588@P0` | `BT5-030` | 8588 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_030_P0.asset` |
| `BT5-030#977@base` | `BT5-030` | 977 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/Digimon/BT5_030.asset` |
| `BT5-038#989@base` | `BT5-038` | 989 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_038.asset` |
| `BT5-038#990@P1` | `BT5-038` | 990 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_038_P1.asset` |
| `BT5-041#993@base` | `BT5-041` | 993 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_041.asset` |
| `BT5-044#1000@P1` | `BT5-044` | 1000 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_044_P1.asset` |
| `BT5-044#8598@P0` | `BT5-044` | 8598 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_044_P0.asset` |
| `BT5-044#8599@P2` | `BT5-044` | 8599 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_044_P2.asset` |
| `BT5-044#8600@P3` | `BT5-044` | 8600 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_044_P3.asset` |
| `BT5-044#999@base` | `BT5-044` | 999 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_044.asset` |
| `BT5-053#1011@base` | `BT5-053` | 1011 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_053.asset` |
| `BT5-062#1020@base` | `BT5-062` | 1020 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_062.asset` |
| `BT5-062#1021@P1` | `BT5-062` | 1021 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_062_P1.asset` |
| `BT5-068#1028@base` | `BT5-068` | 1028 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_068.asset` |
| `BT5-069#1029@base` | `BT5-069` | 1029 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_069.asset` |
| `BT5-069#8614@P0` | `BT5-069` | 8614 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_069_P0.asset` |
| `BT5-069#8615@P1` | `BT5-069` | 8615 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_069_P1.asset` |
| `BT5-071#1031@base` | `BT5-071` | 1031 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_071.asset` |
| `BT5-071#1032@P1` | `BT5-071` | 1032 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_071_P1.asset` |
| `BT5-071#1033@P2` | `BT5-071` | 1033 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_071_P2.asset` |
| `BT5-071#1034@P3` | `BT5-071` | 1034 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_071_P3.asset` |
| `BT5-071#8616@P0` | `BT5-071` | 8616 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Purple/Digimon/BT5_071_P0.asset` |
| `P-004#6007@base` | `P-004` | 6007 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_004.asset` |
| `P-004#6008@P1` | `P-004` | 6008 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_004_P1.asset` |
| `P-004#6009@P2` | `P-004` | 6009 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_004_P2.asset` |
| `P-004#6010@P3` | `P-004` | 6010 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_004_P3.asset` |

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
