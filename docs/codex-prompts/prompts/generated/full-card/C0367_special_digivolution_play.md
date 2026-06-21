# C0367_special_digivolution_play - special digivolution/play mechanics card porting 132

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0367_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX3_072` | `DCGO/Assets/Scripts/CardEffect/EX3/Purple/EX3_072.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX3_073` | `DCGO/Assets/Scripts/CardEffect/EX3/Purple/EX3_073.cs` | `None, OnDestroyedAnyone, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `EX3_074` | `DCGO/Assets/Scripts/CardEffect/EX3/Green/EX3_074.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 4 |
| `EX4_005` | `DCGO/Assets/Scripts/CardEffect/EX4/Red/EX4_005.cs` | `None, OnStartMainPhase, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `EX4_006` | `DCGO/Assets/Scripts/CardEffect/EX4/Red/EX4_006.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectJogress` | 2 |
| `EX4_007` | `DCGO/Assets/Scripts/CardEffect/EX4/Red/EX4_007.cs` | `None, OnStartMainPhase, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `EX4_008` | `DCGO/Assets/Scripts/CardEffect/EX4/Red/EX4_008.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX4_009` | `DCGO/Assets/Scripts/CardEffect/EX4/Red/EX4_009.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX4_010` | `DCGO/Assets/Scripts/CardEffect/EX4/Red/EX4_010.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX4_011` | `DCGO/Assets/Scripts/CardEffect/EX4/Red/EX4_011.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX3-072#2263@base` | `EX3-072` | 2263 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Option/EX3_072.asset` |
| `EX3-073#2264@base` | `EX3-073` | 2264 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_073.asset` |
| `EX3-073#2265@P1` | `EX3-073` | 2265 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_073_P1.asset` |
| `EX3-074#2266@base` | `EX3-074` | 2266 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_074.asset` |
| `EX3-074#2267@P1` | `EX3-074` | 2267 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_074_P1.asset` |
| `EX3-074#9134@P2` | `EX3-074` | 9134 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_074_P2.asset` |
| `EX3-074#9135@P3` | `EX3-074` | 9135 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Digimon/EX3_074_P3.asset` |
| `EX4-005#2548@base` | `EX4-005` | 2548 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_005.asset` |
| `EX4-005#9137@P1` | `EX4-005` | 9137 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_005_P1.asset` |
| `EX4-005#9138@P2` | `EX4-005` | 9138 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_005_P2.asset` |
| `EX4-006#2549@base` | `EX4-006` | 2549 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_006.asset` |
| `EX4-006#9139@P1` | `EX4-006` | 9139 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_006_P1.asset` |
| `EX4-007#2550@base` | `EX4-007` | 2550 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_007.asset` |
| `EX4-008#2551@base` | `EX4-008` | 2551 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_008.asset` |
| `EX4-009#2552@base` | `EX4-009` | 2552 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_009.asset` |
| `EX4-010#2553@base` | `EX4-010` | 2553 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_010.asset` |
| `EX4-011#2554@base` | `EX4-011` | 2554 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_011.asset` |
| `EX4-011#2555@P1` | `EX4-011` | 2555 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX4/Red/Digimon/EX4_011_P1.asset` |

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
