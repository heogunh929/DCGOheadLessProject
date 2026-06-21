# C0344_special_digivolution_play - special digivolution/play mechanics card porting 109

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0344_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT9_016` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_016.cs` | `None, OnEndAttack, OnLoseSecurity` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT9_017` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_017.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT9_020` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_020.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectOrder, SelectJogress` | 4 |
| `BT9_023` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_023.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 2 |
| `BT9_028` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_028.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT9_029` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_029.cs` | `OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT9_030` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_030.cs` | `OnAllyAttack` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT9_031` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_031.cs` | `None, OnEnterFieldAnyone, OnUnTappedAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 2 |
| `BT9_034` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_034.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT9_036` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_036.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT9-016#1798@base` | `BT9-016` | 1798 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_016.asset` |
| `BT9-016#1799@P1` | `BT9-016` | 1799 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_016_P1.asset` |
| `BT9-016#8956@P2` | `BT9-016` | 8956 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_016_P2.asset` |
| `BT9-017#1800@base` | `BT9-017` | 1800 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_017.asset` |
| `BT9-017#1801@P1` | `BT9-017` | 1801 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Digimon/BT9_017_P1.asset` |
| `BT9-020#1804@base` | `BT9-020` | 1804 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_020.asset` |
| `BT9-020#1805@P1` | `BT9-020` | 1805 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_020_P1.asset` |
| `BT9-020#8958@P0` | `BT9-020` | 8958 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_020_P0.asset` |
| `BT9-020#8959@P2` | `BT9-020` | 8959 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_020_P2.asset` |
| `BT9-023#1809@base` | `BT9-023` | 1809 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_023.asset` |
| `BT9-023#8961@P1` | `BT9-023` | 8961 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_023_P1.asset` |
| `BT9-028#1814@base` | `BT9-028` | 1814 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_028.asset` |
| `BT9-028#8963@P0` | `BT9-028` | 8963 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_028_P0.asset` |
| `BT9-029#1815@base` | `BT9-029` | 1815 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_029.asset` |
| `BT9-029#8964@P0` | `BT9-029` | 8964 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_029_P0.asset` |
| `BT9-030#1816@base` | `BT9-030` | 1816 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_030.asset` |
| `BT9-031#1817@base` | `BT9-031` | 1817 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_031.asset` |
| `BT9-031#1818@P1` | `BT9-031` | 1818 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Digimon/BT9_031_P1.asset` |
| `BT9-034#1821@base` | `BT9-034` | 1821 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_034.asset` |
| `BT9-036#1823@base` | `BT9-036` | 1823 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_036.asset` |

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
