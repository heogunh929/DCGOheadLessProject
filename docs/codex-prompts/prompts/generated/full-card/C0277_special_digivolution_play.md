# C0277_special_digivolution_play - special digivolution/play mechanics card porting 42

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0277_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT17_098` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_098.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT17_099` | `DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_099.cs` | `OnDestroyedAnyone, OnPermamemtReturnedToHand, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 3 |
| `BT17_101` | `DCGO/Assets/Scripts/CardEffect/BT17/Purple/BT17_101.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 3 |
| `BT17_102` | `DCGO/Assets/Scripts/CardEffect/BT17/White/BT17_102.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 6 |
| `BT18_007` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_007.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT18_011` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_011.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT18_012` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_012.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT18_013` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_013.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT18_014` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_014.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT18_015` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_015.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT17-098#3667@base` | `BT17-098` | 3667 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Option/BT17_098.asset` |
| `BT17-098#4888@P0` | `BT17-098` | 4888 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Option/BT17_098_P0.asset` |
| `BT17-098#4889@P1` | `BT17-098` | 4889 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Option/BT17_098_P1.asset` |
| `BT17-099#3668@base` | `BT17-099` | 3668 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Option/BT17_099.asset` |
| `BT17-099#4890@P1` | `BT17-099` | 4890 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Option/BT17_099_P1.asset` |
| `BT17-099#8247@P2` | `BT17-099` | 8247 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Yellow/Option/BT17_099_P2.asset` |
| `BT17-101#3670@base` | `BT17-101` | 3670 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_101.asset` |
| `BT17-101#3671@P1` | `BT17-101` | 3671 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_101_P1.asset` |
| `BT17-101#3672@P2` | `BT17-101` | 3672 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_101_P2.asset` |
| `BT17-102#3673@base` | `BT17-102` | 3673 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_102.asset` |
| `BT17-102#3674@P1` | `BT17-102` | 3674 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_102_P1.asset` |
| `BT17-102#3675@P2` | `BT17-102` | 3675 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_102_P2.asset` |
| `BT17-102#3676@P3` | `BT17-102` | 3676 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_102_P3.asset` |
| `BT17-102#8248@P4` | `BT17-102` | 8248 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_102_P4.asset` |
| `BT17-102#8249@P5` | `BT17-102` | 8249 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT17/White/Digimon/BT17_102_P5.asset` |
| `BT18-007#3853@base` | `BT18-007` | 3853 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_007.asset` |
| `BT18-011#3857@base` | `BT18-011` | 3857 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_011.asset` |
| `BT18-011#8251@P1` | `BT18-011` | 8251 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_011_P1.asset` |
| `BT18-012#3864@base` | `BT18-012` | 3864 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_012.asset` |
| `BT18-013#3860@base` | `BT18-013` | 3860 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_013.asset` |
| `BT18-014#3863@base` | `BT18-014` | 3863 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_014.asset` |
| `BT18-015#3865@base` | `BT18-015` | 3865 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_015.asset` |

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
