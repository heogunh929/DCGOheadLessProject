# C0242_special_digivolution_play - special digivolution/play mechanics card porting 7

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0242_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT10_102` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_102.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT10_103` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_103.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT10_104` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_104.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress, SelectDigiXros` | 2 |
| `BT10_105` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_105.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectOrder, SelectSecurity, SelectJogress` | 2 |
| `BT10_106` | `DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_106.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT10_107` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_107.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT10_108` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_108.cs` | `OnDiscardLibrary, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT10_109` | `DCGO/Assets/Scripts/CardEffect/BT10/White/BT10_109.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT10_110` | `DCGO/Assets/Scripts/CardEffect/BT10/White/BT10_110.cs` | `None, OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT11_015` | `DCGO/Assets/Scripts/CardEffect/BT11/Red/BT11_015.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress, SelectDigiXros` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT10-102#2158@base` | `BT10-102` | 2158 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Option/BT10_102.asset` |
| `BT10-103#2159@base` | `BT10-103` | 2159 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Option/BT10_103.asset` |
| `BT10-103#4359@P0` | `BT10-103` | 4359 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Option/BT10_103_P0.asset` |
| `BT10-103#4360@P1` | `BT10-103` | 4360 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Option/BT10_103_P1.asset` |
| `BT10-104#2160@base` | `BT10-104` | 2160 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Option/BT10_104.asset` |
| `BT10-104#4361@P0` | `BT10-104` | 4361 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Option/BT10_104_P0.asset` |
| `BT10-105#2161@base` | `BT10-105` | 2161 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Option/BT10_105.asset` |
| `BT10-105#4362@P1` | `BT10-105` | 4362 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Option/BT10_105_P1.asset` |
| `BT10-106#2162@base` | `BT10-106` | 2162 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Option/BT10_106.asset` |
| `BT10-106#4363@P0` | `BT10-106` | 4363 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Black/Option/BT10_106_P0.asset` |
| `BT10-107#2163@base` | `BT10-107` | 2163 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Option/BT10_107.asset` |
| `BT10-108#2164@base` | `BT10-108` | 2164 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Option/BT10_108.asset` |
| `BT10-108#4364@P0` | `BT10-108` | 4364 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Option/BT10_108_P0.asset` |
| `BT10-109#2165@base` | `BT10-109` | 2165 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Option/BT10_109.asset` |
| `BT10-109#4365@P0` | `BT10-109` | 4365 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Option/BT10_109_P0.asset` |
| `BT10-109#4366@P1` | `BT10-109` | 4366 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Option/BT10_109_P1.asset` |
| `BT10-110#2166@base` | `BT10-110` | 2166 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Option/BT10_110.asset` |
| `BT10-110#4367@P0` | `BT10-110` | 4367 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Option/BT10_110_P0.asset` |
| `BT11-015#2282@base` | `BT11-015` | 2282 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_015.asset` |
| `BT11-015#4376@P0` | `BT11-015` | 4376 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_015_P0.asset` |
| `BT11-015#4377@P1` | `BT11-015` | 4377 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_015_P1.asset` |
| `BT11-015#4378@P2` | `BT11-015` | 4378 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_015_P2.asset` |
| `BT11-015#4379@P3` | `BT11-015` | 4379 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT11/Red/Digimon/BT11_015_P3.asset` |

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
