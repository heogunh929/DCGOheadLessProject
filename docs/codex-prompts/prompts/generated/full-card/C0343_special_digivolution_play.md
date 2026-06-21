# C0343_special_digivolution_play - special digivolution/play mechanics card porting 108

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0343_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 26
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT8_108` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_108.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectJogress` | 4 |
| `BT8_109` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_109.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT8_110` | `DCGO/Assets/Scripts/CardEffect/BT8/White/BT8_110.cs` | `None, OptionSkill, SecuritySkill, WhenTopCardTrashed` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_priority, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT9_001` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_001.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 4 |
| `BT9_008` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_008.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectOrder, SelectJogress` | 4 |
| `BT9_009` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_009.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT9_011` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_011.cs` | `None` | `inherited, modifier_duration, static_or_continuous` | `SelectJogress` | 1 |
| `BT9_013` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_013.cs` | `None, OnEnterFieldAnyone` | `inherited, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectAttackTarget, SelectJogress` | 2 |
| `BT9_014` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_014.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT9_015` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_015.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT8-108#1701@base` | `BT8-108` | 1701 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Option/BT8_108.asset` |
| `BT8-108#3292@P1` | `BT8-108` | 3292 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Option/BT8_108_P1.asset` |
| `BT8-108#6808@P2` | `BT8-108` | 6808 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Option/BT8_108_P2.asset` |
| `BT8-108#8934@P3` | `BT8-108` | 8934 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Option/BT8_108_P3.asset` |
| `BT8-109#1702@base` | `BT8-109` | 1702 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Option/BT8_109.asset` |
| `BT8-109#6809@P0` | `BT8-109` | 6809 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Option/BT8_109_P0.asset` |
| `BT8-109#6810@P1` | `BT8-109` | 6810 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Option/BT8_109_P1.asset` |
| `BT8-110#1703@base` | `BT8-110` | 1703 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Option/BT8_110.asset` |
| `BT8-110#6811@P1` | `BT8-110` | 6811 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Option/BT8_110_P1.asset` |
| `BT9-001#1775@base` | `BT9-001` | 1775 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/DigiEgg/BT9_001.asset` |
| `BT9-001#1776@P1` | `BT9-001` | 1776 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_001_P1.asset` |
| `BT9-001#8936@P1` | `BT9-001` | 8936 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/DigiEgg/BT9_001_P1.asset` |
| `BT9-001#8937@P0` | `BT9-001` | 8937 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/DigiEgg/BT9_001_P0.asset` |
| `BT9-008#1789@base` | `BT9-008` | 1789 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_008.asset` |
| `BT9-008#1790@P1` | `BT9-008` | 1790 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_008_P1.asset` |
| `BT9-008#8949@P0` | `BT9-008` | 8949 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_008_P0.asset` |
| `BT9-008#8950@P2` | `BT9-008` | 8950 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_008_P2.asset` |
| `BT9-009#1791@base` | `BT9-009` | 1791 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_009.asset` |
| `BT9-009#8951@P0` | `BT9-009` | 8951 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_009_P0.asset` |
| `BT9-011#1793@base` | `BT9-011` | 1793 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_011.asset` |
| `BT9-013#1795@base` | `BT9-013` | 1795 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_013.asset` |
| `BT9-013#8953@P0` | `BT9-013` | 8953 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_013_P0.asset` |
| `BT9-014#1796@base` | `BT9-014` | 1796 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_014.asset` |
| `BT9-014#8954@P0` | `BT9-014` | 8954 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_014_P0.asset` |
| `BT9-015#1797@base` | `BT9-015` | 1797 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_015.asset` |
| `BT9-015#8955@P0` | `BT9-015` | 8955 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_015_P0.asset` |

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
