# C0206_replacement_counter_cut_in - replacement/counter/cut-in card porting 21

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0206_replacement_counter_cut_in`
- Kind: `card-porting`
- Category: `replacement-counter-cut-in` / replacement/counter/cut-in
- Dependencies: `L0184_replacement_counter_cut_in`, `L0185_replacement_counter_cut_in`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT23_089` | `DCGO/Assets/Scripts/CardEffect/BT23/White/BT23_089.cs` | `OnStartMainPhase, SecuritySkill, WhenRemoveField` | `max_count_per_turn, optional, replacement_or_cut_in, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 3 |
| `BT23_102` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_102.cs` | `None, OnEnterFieldAnyone, OnLoseSecurity, WhenPermanentWouldBeDeleted, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 3 |
| `BT24_015` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_015.cs` | `None, OnAllyAttack, OnAttackTargetChanged, SecuritySkill` | `inherited, max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `BT24_018` | `DCGO/Assets/Scripts/CardEffect/BT24/Red/BT24_018.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnLoseSecurity, WhenPermanentWouldBeDeleted, WhenRemoveField` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT24_023` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_023.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT24_024` | `DCGO/Assets/Scripts/CardEffect/BT24/Blue/BT24_024.cs` | `None, OnAllyAttack, WhenPermanentWouldBeDeleted` | `max_count_per_turn, optional, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT24_033` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_033.cs` | `None, WhenPermanentWouldBeDeleted` | `inherited, replacement_or_cut_in, static_or_continuous` | `SelectCard` | 1 |
| `BT24_034` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_034.cs` | `None, OnEnterFieldAnyone, OnMove, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, optional, replacement_or_cut_in, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 3 |
| `BT24_035` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_035.cs` | `None, OnEnterFieldAnyone, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, modifier_duration, replacement_or_cut_in, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT24_039` | `DCGO/Assets/Scripts/CardEffect/BT24/Yellow/BT24_039.cs` | `None, OnDestroyedAnyone, SecuritySkill, WhenPermanentWouldBeDeleted` | `inherited, max_count_per_turn, replacement_or_cut_in, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT23-089#7439@base` | `BT23-089` | 7439 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Tamer/BT23_089.asset` |
| `BT23-089#7440@P1` | `BT23-089` | 7440 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Tamer/BT23_089_P1.asset` |
| `BT23-089#7441@P2` | `BT23-089` | 7441 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT23/White/Tamer/BT23_089_P2.asset` |
| `BT23-102#7458@base` | `BT23-102` | 7458 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_102.asset` |
| `BT23-102#7459@P1` | `BT23-102` | 7459 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_102_P1.asset` |
| `BT23-102#7460@P2` | `BT23-102` | 7460 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Digimon/BT23_102_P2.asset` |
| `BT24-015#7535@base` | `BT24-015` | 7535 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_015.asset` |
| `BT24-018#7538@base` | `BT24-018` | 7538 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_018.asset` |
| `BT24-018#7539@P1` | `BT24-018` | 7539 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Red/Digimon/BT24_018_P1.asset` |
| `BT24-023#7545@base` | `BT24-023` | 7545 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_023.asset` |
| `BT24-024#7546@base` | `BT24-024` | 7546 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Blue/Digimon/BT24_024.asset` |
| `BT24-033#7557@base` | `BT24-033` | 7557 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_033.asset` |
| `BT24-034#7558@base` | `BT24-034` | 7558 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_034.asset` |
| `BT24-034#7559@P1` | `BT24-034` | 7559 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_034_P1.asset` |
| `BT24-034#7560@P2` | `BT24-034` | 7560 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_034_P2.asset` |
| `BT24-035#7561@base` | `BT24-035` | 7561 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_035.asset` |
| `BT24-039#7565@base` | `BT24-039` | 7565 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Yellow/Digimon/BT24_039.asset` |

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
