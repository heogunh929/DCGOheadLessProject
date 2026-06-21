# C0229_replacement_counter_cut_in - replacement/counter/cut-in card porting 44

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0229_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_140` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_140.cs` | `None, OnEndBattle, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, static_or_continuous, zone_movement` | `-` | 2 |
| `P_141` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_141.cs` | `None, OnCounterTiming, OnTappedAnyone` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous` | `SelectJogress` | 1 |
| `P_146` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_146.cs` | `None, OptionSkill, SecuritySkill, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `P_153` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_153.cs` | `None, OnEndAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `P_154` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_154.cs` | `None, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `P_165` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_165.cs` | `OnEnterFieldAnyone, SecuritySkill, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `-` | 4 |
| `P_173` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_173.cs` | `None, OnCounterTiming, OnDestroyedAnyone, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `P_178` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_178.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 4 |
| `P_184` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_184.cs` | `None, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 1 |
| `P_189` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_189.cs` | `None, OnLoseSecurity, SecuritySkill` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-140#10294@base` | `P-140` | 10294 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_140.asset` |
| `P-140#9260@P1` | `P-140` | 9260 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_140_P1.asset` |
| `P-141#10295@base` | `P-141` | 10295 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_141.asset` |
| `P-146#10303@base` | `P-146` | 10303 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_146.asset` |
| `P-146#10304@P1` | `P-146` | 10304 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_146_P1.asset` |
| `P-146#9264@P2` | `P-146` | 9264 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_146_P2.asset` |
| `P-153#10452@base` | `P-153` | 10452 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_153.asset` |
| `P-153#10453@P1` | `P-153` | 10453 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_153_P1.asset` |
| `P-154#10454@base` | `P-154` | 10454 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_154.asset` |
| `P-154#10455@P1` | `P-154` | 10455 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_154_P1.asset` |
| `P-165#10448@base` | `P-165` | 10448 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_165.asset` |
| `P-165#10449@P1` | `P-165` | 10449 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_165_P1.asset` |
| `P-165#9277@P2` | `P-165` | 9277 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_165_P2.asset` |
| `P-165#9278@P3` | `P-165` | 9278 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_165_P3.asset` |
| `P-173#5186@base` | `P-173` | 5186 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_173.asset` |
| `P-178#5299@base` | `P-178` | 5299 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_178.asset` |
| `P-178#5300@P1` | `P-178` | 5300 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_178_P1.asset` |
| `P-178#9291@P2` | `P-178` | 9291 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_178_P2.asset` |
| `P-178#9292@P3` | `P-178` | 9292 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_178_P3.asset` |
| `P-184#5436@base` | `P-184` | 5436 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_184.asset` |
| `P-189#6979@base` | `P-189` | 6979 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_189.asset` |
| `P-189#6980@P1` | `P-189` | 6980 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_189_P1.asset` |
| `P-189#9304@P2` | `P-189` | 9304 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_189_P2.asset` |

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
