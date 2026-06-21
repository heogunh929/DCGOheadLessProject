# C0252_special_digivolution_play - special digivolution/play mechanics card porting 17

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0252_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT12_099` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_099.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `BT12_100` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_100.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `BT12_101` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_101.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT12_102` | `DCGO/Assets/Scripts/CardEffect/BT12/Blue/BT12_102.cs` | `BeforePayCost, None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT12_103` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_103.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT12_104` | `DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_104.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT12_105` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_105.cs` | `OnDestroyedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT12_106` | `DCGO/Assets/Scripts/CardEffect/BT12/Green/BT12_106.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 2 |
| `BT12_107` | `DCGO/Assets/Scripts/CardEffect/BT12/Black/BT12_107.cs` | `OnStartMainPhase, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `BT12_108` | `DCGO/Assets/Scripts/CardEffect/BT12/Red/BT12_108.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT12-099#2526@base` | `BT12-099` | 2526 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Option/BT12_099.asset` |
| `BT12-100#2527@base` | `BT12-100` | 2527 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Option/BT12_100.asset` |
| `BT12-100#4546@P0` | `BT12-100` | 4546 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Red/Option/BT12_100_P0.asset` |
| `BT12-101#2528@base` | `BT12-101` | 2528 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Option/BT12_101.asset` |
| `BT12-101#4547@P0` | `BT12-101` | 4547 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Option/BT12_101_P0.asset` |
| `BT12-102#2529@base` | `BT12-102` | 2529 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Blue/Option/BT12_102.asset` |
| `BT12-103#2530@base` | `BT12-103` | 2530 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Option/BT12_103.asset` |
| `BT12-104#2531@base` | `BT12-104` | 2531 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Option/BT12_104.asset` |
| `BT12-104#4548@P0` | `BT12-104` | 4548 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Option/BT12_104_P0.asset` |
| `BT12-104#8137@P1` | `BT12-104` | 8137 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Yellow/Option/BT12_104_P1.asset` |
| `BT12-105#2532@base` | `BT12-105` | 2532 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Option/BT12_105.asset` |
| `BT12-106#2533@base` | `BT12-106` | 2533 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Option/BT12_106.asset` |
| `BT12-106#4549@P0` | `BT12-106` | 4549 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Option/BT12_106_P0.asset` |
| `BT12-107#2534@base` | `BT12-107` | 2534 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Option/BT12_107.asset` |
| `BT12-108#2535@base` | `BT12-108` | 2535 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Option/BT12_108.asset` |
| `BT12-108#4550@P0` | `BT12-108` | 4550 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Black/Option/BT12_108_P0.asset` |

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
