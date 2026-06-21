# C0332_special_digivolution_play - special digivolution/play mechanics card porting 97

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0332_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 31
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT6_015` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_015.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 4 |
| `BT6_016` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_016.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 3 |
| `BT6_019` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_019.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `BT6_028` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_028.cs` | `OnDeclaration` | `max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectBurstDigivolution` | 2 |
| `BT6_042` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_042.cs` | `OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT6_047` | `DCGO/Assets/Scripts/CardEffect/BT6/Green/BT6_047.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous` | `SelectCard, SelectJogress` | 3 |
| `BT6_075` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_075.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT6_079` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_079.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT6_082` | `DCGO/Assets/Scripts/CardEffect/BT6/White/BT6_082.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectJogress` | 5 |
| `BT6_084` | `DCGO/Assets/Scripts/CardEffect/BT6/White/BT6_084.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectJogress` | 6 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT6-015#1124@base` | `BT6-015` | 1124 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_015.asset` |
| `BT6-015#1125@P1` | `BT6-015` | 1125 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_015_P1.asset` |
| `BT6-015#8679@P0` | `BT6-015` | 8679 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_015_P0.asset` |
| `BT6-015#8680@P2` | `BT6-015` | 8680 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_015_P2.asset` |
| `BT6-016#1126@base` | `BT6-016` | 1126 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_016.asset` |
| `BT6-016#1127@P1` | `BT6-016` | 1127 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_016_P1.asset` |
| `BT6-016#1128@P2` | `BT6-016` | 1128 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_016_P2.asset` |
| `BT6-019#1133@base` | `BT6-019` | 1133 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_019.asset` |
| `BT6-019#1134@P1` | `BT6-019` | 1134 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_019_P1.asset` |
| `BT6-019#8683@P0` | `BT6-019` | 8683 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_019_P0.asset` |
| `BT6-028#1144@base` | `BT6-028` | 1144 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_028.asset` |
| `BT6-028#8687@P0` | `BT6-028` | 8687 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_028_P0.asset` |
| `BT6-042#1165@base` | `BT6-042` | 1165 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_042.asset` |
| `BT6-042#8692@P0` | `BT6-042` | 8692 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_042_P0.asset` |
| `BT6-047#1171@base` | `BT6-047` | 1171 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_047.asset` |
| `BT6-047#1172@P1` | `BT6-047` | 1172 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_047_P1.asset` |
| `BT6-047#8694@P0` | `BT6-047` | 8694 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_047_P0.asset` |
| `BT6-075#1209@base` | `BT6-075` | 1209 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_075.asset` |
| `BT6-075#8709@P0` | `BT6-075` | 8709 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_075_P0.asset` |
| `BT6-079#1215@base` | `BT6-079` | 1215 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_079.asset` |
| `BT6-082#1219@base` | `BT6-082` | 1219 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_082.asset` |
| `BT6-082#1220@P1` | `BT6-082` | 1220 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_082_P1.asset` |
| `BT6-082#8713@P0` | `BT6-082` | 8713 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_082_P0.asset` |
| `BT6-082#8714@P2` | `BT6-082` | 8714 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_082_P2.asset` |
| `BT6-082#8715@P3` | `BT6-082` | 8715 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_082_P3.asset` |
| `BT6-084#1223@base` | `BT6-084` | 1223 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_084.asset` |
| `BT6-084#1224@P1` | `BT6-084` | 1224 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_084_P1.asset` |
| `BT6-084#6782@P0` | `BT6-084` | 6782 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_084_P0.asset` |
| `BT6-084#6783@P2` | `BT6-084` | 6783 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_084_P2.asset` |
| `BT6-084#6784@P3` | `BT6-084` | 6784 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_084_P3.asset` |
| `BT6-084#6785@P4` | `BT6-084` | 6785 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_084_P4.asset` |

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
