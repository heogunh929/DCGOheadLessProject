# C0191_replacement_counter_cut_in - replacement/counter/cut-in card porting 6

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0191_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 27
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT14_049` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_049.cs` | `OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 4 |
| `BT14_056` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_056.cs` | `OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT14_060` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_060.cs` | `None, OnAllyAttack, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `BT14_096` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_096.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT15_012` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_012.cs` | `None, OnEnterFieldAnyone, OnStartTurn, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress, SelectDigiXros` | 2 |
| `BT15_014` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_014.cs` | `OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 3 |
| `BT15_026` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_026.cs` | `None, OnAddHand, OnCounterTiming, OnEnterFieldAnyone` | `counter, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 4 |
| `BT15_033` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_033.cs` | `WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in` | `-` | 1 |
| `BT15_037` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_037.cs` | `OnDiscardSecurity, OnLoseSecurity, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `BT15_038` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_038.cs` | `OnCounterTiming, OnEnterFieldAnyone, OnLoseSecurity` | `counter, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT14-049#2973@base` | `BT14-049` | 2973 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_049.asset` |
| `BT14-049#2974@P1` | `BT14-049` | 2974 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_049_P1.asset` |
| `BT14-049#4666@P2` | `BT14-049` | 4666 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_049_P2.asset` |
| `BT14-049#4667@P3` | `BT14-049` | 4667 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_049_P3.asset` |
| `BT14-056#2981@base` | `BT14-056` | 2981 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_056.asset` |
| `BT14-056#2982@P1` | `BT14-056` | 2982 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_056_P1.asset` |
| `BT14-060#2986@base` | `BT14-060` | 2986 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_060.asset` |
| `BT14-060#4671@P0` | `BT14-060` | 4671 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_060_P0.asset` |
| `BT14-060#4672@P1` | `BT14-060` | 4672 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_060_P1.asset` |
| `BT14-096#3031@base` | `BT14-096` | 3031 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Option/BT14_096.asset` |
| `BT14-096#4705@P0` | `BT14-096` | 4705 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Option/BT14_096_P0.asset` |
| `BT15-012#3132@base` | `BT15-012` | 3132 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_012.asset` |
| `BT15-012#4714@P0` | `BT15-012` | 4714 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_012_P0.asset` |
| `BT15-014#3134@base` | `BT15-014` | 3134 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_014.asset` |
| `BT15-014#3135@P1` | `BT15-014` | 3135 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_014_P1.asset` |
| `BT15-014#3136@P2` | `BT15-014` | 3136 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_014_P2.asset` |
| `BT15-026#3149@base` | `BT15-026` | 3149 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_026.asset` |
| `BT15-026#3150@P1` | `BT15-026` | 3150 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_026_P1.asset` |
| `BT15-026#3151@P2` | `BT15-026` | 3151 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_026_P2.asset` |
| `BT15-026#8184@P3` | `BT15-026` | 8184 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_026_P3.asset` |
| `BT15-033#3158@base` | `BT15-033` | 3158 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_033.asset` |
| `BT15-037#3162@base` | `BT15-037` | 3162 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_037.asset` |
| `BT15-037#3163@P1` | `BT15-037` | 3163 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_037_P1.asset` |
| `BT15-038#3164@base` | `BT15-038` | 3164 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_038.asset` |
| `BT15-038#3165@P1` | `BT15-038` | 3165 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_038_P1.asset` |
| `BT15-038#3166@P2` | `BT15-038` | 3166 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_038_P2.asset` |
| `BT15-038#8185@P3` | `BT15-038` | 8185 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_038_P3.asset` |

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
