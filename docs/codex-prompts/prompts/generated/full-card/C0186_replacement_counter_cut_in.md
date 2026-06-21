# C0186_replacement_counter_cut_in - replacement/counter/cut-in card porting 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0186_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `AD1_005` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_005.cs` | `None, OnAllyAttack, OnCounterTiming, OnEnterFieldAnyone` | `counter, inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger` | 2 |
| `AD1_012` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_012.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectAttackTarget, SelectJogress, SelectAssembly` | 2 |
| `AD1_014` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_014.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, OnTappedAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `AD1_017` | `DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_017.cs` | `None, OnEnterFieldAnyone, OnLoseSecurity, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity` | 2 |
| `AD1_018` | `DCGO/Assets/Scripts/CardEffect/AD1/Purple/AD1_018.cs` | `None, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, modifier_duration, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `AD1_023` | `DCGO/Assets/Scripts/CardEffect/AD1/Black/AD1_023.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectInteger, SelectSecurity, SelectJogress` | 2 |
| `BT10_009` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_009.cs` | `None, OnEndAttack, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectJogress, SelectDigiXros` | 2 |
| `BT10_012` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_012.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress, SelectDigiXros` | 2 |
| `BT10_013` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_013.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, static_or_continuous` | `SelectJogress, SelectDigiXros` | 2 |
| `BT10_015` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_015.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress, SelectDigiXros` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `AD1-005#7824@base` | `AD1-005` | 7824 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005.asset` |
| `AD1-005#7825@P1` | `AD1-005` | 7825 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005_P1.asset` |
| `AD1-012#7836@base` | `AD1-012` | 7836 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012.asset` |
| `AD1-012#7837@P1` | `AD1-012` | 7837 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012_P1.asset` |
| `AD1-014#7839@base` | `AD1-014` | 7839 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014.asset` |
| `AD1-014#7840@P1` | `AD1-014` | 7840 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014_P1.asset` |
| `AD1-017#7845@base` | `AD1-017` | 7845 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_017.asset` |
| `AD1-017#7846@P1` | `AD1-017` | 7846 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_017_P1.asset` |
| `AD1-018#7847@base` | `AD1-018` | 7847 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Purple/Digimon/AD1_018.asset` |
| `AD1-018#7848@P1` | `AD1-018` | 7848 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Purple/Digimon/AD1_018_P1.asset` |
| `AD1-023#7855@base` | `AD1-023` | 7855 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023.asset` |
| `AD1-023#7856@P1` | `AD1-023` | 7856 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023_P1.asset` |
| `BT10-009#2041@base` | `BT10-009` | 2041 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_009.asset` |
| `BT10-009#4292@P0` | `BT10-009` | 4292 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_009_P0.asset` |
| `BT10-012#2045@base` | `BT10-012` | 2045 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_012.asset` |
| `BT10-012#4297@P0` | `BT10-012` | 4297 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_012_P0.asset` |
| `BT10-013#2046@base` | `BT10-013` | 2046 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_013.asset` |
| `BT10-013#2047@P1` | `BT10-013` | 2047 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_013_P1.asset` |
| `BT10-015#2049@base` | `BT10-015` | 2049 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_015.asset` |
| `BT10-015#4299@P0` | `BT10-015` | 4299 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_015_P0.asset` |

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
