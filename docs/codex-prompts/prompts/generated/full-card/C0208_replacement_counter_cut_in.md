# C0208_replacement_counter_cut_in - replacement/counter/cut-in card porting 23

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0208_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT25_031` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_031.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT25_032` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_032.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT25_033` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_033.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT25_034` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_034.cs` | `None, OnDestroyedAnyone, OnDiscardSecurity, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `BT25_035` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_035.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT25_037` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_037.cs` | `None, WhenPermanentWouldBeDeleted` | `replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT25_043` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_043.cs` | `None, OptionSkill, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectInteger, SelectSecurity` | 3 |
| `BT25_045` | `DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_045.cs` | `None, OnDeclaration, WhenLinked, WhenWouldLink` | `linked, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT25_056` | `DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_056.cs` | `None, OnDeclaration, WhenLinked, WhenPermanentWouldBeDeleted` | `inherited, linked, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger` | 1 |
| `BT25_066` | `DCGO/Assets/Scripts/CardEffect/BT25/Black/BT25_066.cs` | `None, WhenRemoveField` | `inherited, linked, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT25-031#7997@base` | `BT25-031` | 7997 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_031.asset` |
| `BT25-032#7998@base` | `BT25-032` | 7998 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_032.asset` |
| `BT25-033#7999@base` | `BT25-033` | 7999 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_033.asset` |
| `BT25-034#8000@base` | `BT25-034` | 8000 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_034.asset` |
| `BT25-035#8001@base` | `BT25-035` | 8001 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_035.asset` |
| `BT25-037#8003@base` | `BT25-037` | 8003 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_037.asset` |
| `BT25-043#8009@base` | `BT25-043` | 8009 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_043.asset` |
| `BT25-043#8010@P1` | `BT25-043` | 8010 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_043_P1.asset` |
| `BT25-043#8011@P2` | `BT25-043` | 8011 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_043_P2.asset` |
| `BT25-045#8014@base` | `BT25-045` | 8014 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_045.asset` |
| `BT25-056#8025@base` | `BT25-056` | 8025 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_056.asset` |
| `BT25-066#8039@base` | `BT25-066` | 8039 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Digimon/BT25_066.asset` |

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
