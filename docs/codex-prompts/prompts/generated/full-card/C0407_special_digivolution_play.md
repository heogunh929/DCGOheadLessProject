# C0407_special_digivolution_play - special digivolution/play mechanics card porting 172

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0407_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST24_14` | `DCGO/Assets/Scripts/CardEffect/ST24/Green/ST24_14.cs` | `None, OnDigivolutionCardDiscarded, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 1 |
| `ST24_15` | `DCGO/Assets/Scripts/CardEffect/ST24/White/ST24_15.cs` | `None, OnStartMainPhase, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 1 |
| `ST2_13` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_13.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectJogress` | 4 |
| `ST2_14` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_14.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST2_15` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_15.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `ST2_16` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_16.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `ST3_13` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_13.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `ST3_14` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_14.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `ST3_15` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_15.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `ST3_16` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_16.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST2-13#66@base` | `ST2-13` | 66 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Option/ST2_13.asset` |
| `ST2-13#67@P1` | `ST2-13` | 67 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Option/ST2_13_P1.asset` |
| `ST2-13#68@P2` | `ST2-13` | 68 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Option/ST2_13_P2.asset` |
| `ST2-13#69@P3` | `ST2-13` | 69 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Option/ST2_13_P3.asset` |
| `ST2-14#70@base` | `ST2-14` | 70 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Option/ST2_14.asset` |
| `ST2-15#3287@P1` | `ST2-15` | 3287 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Option/ST2_15_P1.asset` |
| `ST2-15#4974@P2` | `ST2-15` | 4974 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Option/ST2_15_P2.asset` |
| `ST2-15#71@base` | `ST2-15` | 71 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Option/ST2_15.asset` |
| `ST2-16#4975@P3` | `ST2-16` | 4975 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Option/ST2_16_P3.asset` |
| `ST2-16#72@base` | `ST2-16` | 72 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Option/ST2_16.asset` |
| `ST2-16#73@P1` | `ST2-16` | 73 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Option/ST2_16_P1.asset` |
| `ST2-16#74@P2` | `ST2-16` | 74 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST2/Blue/Option/ST2_16_P2.asset` |
| `ST24-14#7923@base` | `ST24-14` | 7923 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Green/Tamer/ST24_14.asset` |
| `ST24-15#7924@base` | `ST24-15` | 7924 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/White/Option/ST24_15.asset` |
| `ST3-13#100@P1` | `ST3-13` | 100 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Option/ST3_13_P1.asset` |
| `ST3-13#101@P2` | `ST3-13` | 101 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Option/ST3_13_P2.asset` |
| `ST3-13#99@base` | `ST3-13` | 99 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Option/ST3_13.asset` |
| `ST3-14#102@base` | `ST3-14` | 102 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Option/ST3_14.asset` |
| `ST3-14#103@P1` | `ST3-14` | 103 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Option/ST3_14_P1.asset` |
| `ST3-14#104@P2` | `ST3-14` | 104 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Option/ST3_14_P2.asset` |
| `ST3-15#105@base` | `ST3-15` | 105 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Option/ST3_15.asset` |
| `ST3-15#4982@P1` | `ST3-15` | 4982 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Option/ST3_15_P1.asset` |
| `ST3-16#106@base` | `ST3-16` | 106 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST3/Yellow/Option/ST3_16.asset` |

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
