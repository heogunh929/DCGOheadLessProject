# C0222_replacement_counter_cut_in - replacement/counter/cut-in card porting 37

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0222_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX8_061` | `DCGO/Assets/Scripts/CardEffect/EX8/Purple/EX8_061.cs` | `None, OnAllyAttack, OnDestroyedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `EX8_062` | `DCGO/Assets/Scripts/CardEffect/EX8/Purple/EX8_062.cs` | `None, OnCounterTiming, OnDestroyedAnyone, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `EX8_071` | `DCGO/Assets/Scripts/CardEffect/EX8/Purple/EX8_071.cs` | `None, OptionSkill, SecuritySkill, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |
| `EX9_007` | `DCGO/Assets/Scripts/CardEffect/EX9/Red/EX9_007.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `EX9_009` | `DCGO/Assets/Scripts/CardEffect/EX9/Red/EX9_009.cs` | `None, OnAllyAttack, OnDeclaration` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous` | `-` | 2 |
| `EX9_010` | `DCGO/Assets/Scripts/CardEffect/EX9/Red/EX9_010.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX9_011` | `DCGO/Assets/Scripts/CardEffect/EX9/Red/EX9_011.cs` | `BeforePayCost, None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 3 |
| `EX9_013` | `DCGO/Assets/Scripts/CardEffect/EX9/Red/EX9_013.cs` | `None, OnAllyAttack, OnCounterTiming, OnEndTurn, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `EX9_014` | `DCGO/Assets/Scripts/CardEffect/EX9/Blue/EX9_014.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX9_017` | `DCGO/Assets/Scripts/CardEffect/EX9/Blue/EX9_017.cs` | `None, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX8-061#4164@base` | `EX8-061` | 4164 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_061.asset` |
| `EX8-061#4165@P1` | `EX8-061` | 4165 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_061_P1.asset` |
| `EX8-062#4166@base` | `EX8-062` | 4166 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_062.asset` |
| `EX8-062#4167@P1` | `EX8-062` | 4167 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_062_P1.asset` |
| `EX8-062#4169@P2` | `EX8-062` | 4169 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Digimon/EX8_062_P2.asset` |
| `EX8-071#4187@base` | `EX8-071` | 4187 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Option/EX8_071.asset` |
| `EX8-071#4188@P1` | `EX8-071` | 4188 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Purple/Option/EX8_071_P1.asset` |
| `EX9-007#6843@base` | `EX9-007` | 6843 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_007.asset` |
| `EX9-007#6844@P1` | `EX9-007` | 6844 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_007_P1.asset` |
| `EX9-007#6845@P2` | `EX9-007` | 6845 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_007_P2.asset` |
| `EX9-009#6848@base` | `EX9-009` | 6848 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_009.asset` |
| `EX9-009#6849@P1` | `EX9-009` | 6849 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_009_P1.asset` |
| `EX9-010#6850@base` | `EX9-010` | 6850 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_010.asset` |
| `EX9-010#6851@P1` | `EX9-010` | 6851 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_010_P1.asset` |
| `EX9-011#6852@base` | `EX9-011` | 6852 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_011.asset` |
| `EX9-011#6853@P1` | `EX9-011` | 6853 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_011_P1.asset` |
| `EX9-011#6854@P2` | `EX9-011` | 6854 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_011_P2.asset` |
| `EX9-013#6857@base` | `EX9-013` | 6857 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_013.asset` |
| `EX9-013#6858@P1` | `EX9-013` | 6858 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Red/Digimon/EX9_013_P1.asset` |
| `EX9-014#6859@base` | `EX9-014` | 6859 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_014.asset` |
| `EX9-014#6860@P1` | `EX9-014` | 6860 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_014_P1.asset` |
| `EX9-017#6865@base` | `EX9-017` | 6865 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_017.asset` |
| `EX9-017#6866@P1` | `EX9-017` | 6866 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX9/Blue/Digimon/EX9_017_P1.asset` |

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
