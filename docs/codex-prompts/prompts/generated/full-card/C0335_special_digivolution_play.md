# C0335_special_digivolution_play - special digivolution/play mechanics card porting 100

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0335_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT6_108` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_108.cs` | `OnDiscardHand, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT6_109` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_109.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT6_110` | `DCGO/Assets/Scripts/CardEffect/BT6/White/BT6_110.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT7_003` | `DCGO/Assets/Scripts/CardEffect/BT7/Yellow/BT7_003.cs` | `OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 2 |
| `BT7_008` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_008.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 3 |
| `BT7_009` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_009.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `SelectCard, SelectJogress` | 1 |
| `BT7_011` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_011.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT7_017` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_017.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 3 |
| `BT7_019` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_019.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 3 |
| `BT7_022` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_022.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT6-108#1251@base` | `BT6-108` | 1251 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Option/BT6_108.asset` |
| `BT6-108#8739@P0` | `BT6-108` | 8739 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Option/BT6_108_P0.asset` |
| `BT6-109#1252@base` | `BT6-109` | 1252 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Option/BT6_109.asset` |
| `BT6-109#1253@P1` | `BT6-109` | 1253 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Option/BT6_109_P1.asset` |
| `BT6-109#8740@P0` | `BT6-109` | 8740 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Option/BT6_109_P0.asset` |
| `BT6-110#1254@base` | `BT6-110` | 1254 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Option/BT6_110.asset` |
| `BT6-110#8741@P0` | `BT6-110` | 8741 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Option/BT6_110_P0.asset` |
| `BT7-003#1381@base` | `BT7-003` | 1381 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/DigiEgg/BT7_003.asset` |
| `BT7-003#8747@P0` | `BT7-003` | 8747 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Yellow/DigiEgg/BT7_003_P0.asset` |
| `BT7-008#1388@base` | `BT7-008` | 1388 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_008.asset` |
| `BT7-008#1389@P1` | `BT7-008` | 1389 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_008_P1.asset` |
| `BT7-008#8756@P0` | `BT7-008` | 8756 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_008_P0.asset` |
| `BT7-009#1390@base` | `BT7-009` | 1390 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_009.asset` |
| `BT7-011#1392@base` | `BT7-011` | 1392 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_011.asset` |
| `BT7-011#8757@P0` | `BT7-011` | 8757 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_011_P0.asset` |
| `BT7-017#1400@base` | `BT7-017` | 1400 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_017.asset` |
| `BT7-017#8763@P0` | `BT7-017` | 8763 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_017_P0.asset` |
| `BT7-017#8764@P1` | `BT7-017` | 8764 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_017_P1.asset` |
| `BT7-019#1403@base` | `BT7-019` | 1403 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_019.asset` |
| `BT7-019#1404@P1` | `BT7-019` | 1404 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_019_P1.asset` |
| `BT7-019#8767@P0` | `BT7-019` | 8767 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_019_P0.asset` |
| `BT7-022#1409@base` | `BT7-022` | 1409 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/Digimon/BT7_022.asset` |

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
