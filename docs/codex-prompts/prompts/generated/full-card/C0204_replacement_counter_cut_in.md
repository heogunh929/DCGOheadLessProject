# C0204_replacement_counter_cut_in - replacement/counter/cut-in card porting 19

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0204_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT22_067` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_067.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `BT22_072` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_072.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT22_073` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_073.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 4 |
| `BT22_075` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_075.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted, WhenRemoveField` | `inherited, linked, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectBoolean` | 1 |
| `BT22_076` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_076.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |
| `BT22_080` | `DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_080.cs` | `BeforePayCost, None, OnEnterFieldAnyone, OnSecurityCheck` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `BT22_095` | `DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_095.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone, SecuritySkill, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT23_024` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_024.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenLinked, WhenPermanentWouldBeDeleted` | `inherited, linked, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress, SelectAppFusion` | 1 |
| `BT23_027` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_027.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectJogress` | 1 |
| `BT23_033` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_033.cs` | `None, OnDeclaration, OnEnterFieldAnyone, WhenLinked, WhenPermanentWouldBeDeleted` | `inherited, linked, max_count_per_turn, modifier_duration, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT22-067#7074@base` | `BT22-067` | 7074 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_067.asset` |
| `BT22-072#7079@base` | `BT22-072` | 7079 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_072.asset` |
| `BT22-073#7080@base` | `BT22-073` | 7080 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_073.asset` |
| `BT22-073#8440@P1` | `BT22-073` | 8440 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_073_P1.asset` |
| `BT22-073#8441@P2` | `BT22-073` | 8441 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_073_P2.asset` |
| `BT22-073#8442@P3` | `BT22-073` | 8442 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_073_P3.asset` |
| `BT22-075#7082@base` | `BT22-075` | 7082 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_075.asset` |
| `BT22-076#7083@base` | `BT22-076` | 7083 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_076.asset` |
| `BT22-080#7089@base` | `BT22-080` | 7089 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Digimon/BT22_080.asset` |
| `BT22-080#8444@P1` | `BT22-080` | 8444 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Digimon/BT22_080_P1.asset` |
| `BT22-095#7118@base` | `BT22-095` | 7118 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Tamer/BT22_095.asset` |
| `BT22-095#7119@P1` | `BT22-095` | 7119 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Tamer/BT22_095_P1.asset` |
| `BT23-024#7356@base` | `BT23-024` | 7356 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Digimon/BT23_024.asset` |
| `BT23-027#7359@base` | `BT23-027` | 7359 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_027.asset` |
| `BT23-033#7365@base` | `BT23-033` | 7365 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_033.asset` |

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
