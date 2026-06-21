# C0320_special_digivolution_play - special digivolution/play mechanics card porting 85

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0320_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT2_104` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_104.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT2_105` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_105.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT2_106` | `DCGO/Assets/Scripts/CardEffect/BT2/Black/BT2_106.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT2_107` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_107.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT2_108` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_108.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT2_109` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_109.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT2_110` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_110.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT2_111` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_111.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT3_008` | `DCGO/Assets/Scripts/CardEffect/BT3/Red/BT3_008.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `BT3_019` | `DCGO/Assets/Scripts/CardEffect/BT3/Red/BT3_019.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-104#551@base` | `BT2-104` | 551 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Option/BT2_104.asset` |
| `BT2-105#552@base` | `BT2-105` | 552 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Option/BT2_105.asset` |
| `BT2-106#553@base` | `BT2-106` | 553 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Option/BT2_106.asset` |
| `BT2-107#554@base` | `BT2-107` | 554 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Option/BT2_107.asset` |
| `BT2-108#1755@P1` | `BT2-108` | 1755 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Option/BT2_108_P1.asset` |
| `BT2-108#555@base` | `BT2-108` | 555 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Option/BT2_108.asset` |
| `BT2-109#556@base` | `BT2-109` | 556 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Option/BT2_109.asset` |
| `BT2-110#557@base` | `BT2-110` | 557 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Option/BT2_110.asset` |
| `BT2-111#558@base` | `BT2-111` | 558 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_111.asset` |
| `BT2-111#559@P1` | `BT2-111` | 559 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_111_P1.asset` |
| `BT2-111#560@P2` | `BT2-111` | 560 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_111_P2.asset` |
| `BT3-008#602@base` | `BT3-008` | 602 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_008.asset` |
| `BT3-008#8466@P1` | `BT3-008` | 8466 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_008_P1.asset` |
| `BT3-019#616@base` | `BT3-019` | 616 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_019.asset` |
| `BT3-019#617@P1` | `BT3-019` | 617 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_019_P1.asset` |
| `BT3-062#687@base` | `BT3-062` | 687 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_062.asset` |

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
