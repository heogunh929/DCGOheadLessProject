# C0315_special_digivolution_play - special digivolution/play mechanics card porting 80

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0315_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 11
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT25_021` | `DCGO/Assets/Scripts/CardEffect/BT25/Blue/BT25_021.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT25_023` | `DCGO/Assets/Scripts/CardEffect/BT25/Blue/BT25_023.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectJogress` | 1 |
| `BT25_024` | `DCGO/Assets/Scripts/CardEffect/BT25/Blue/BT25_024.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT25_025` | `DCGO/Assets/Scripts/CardEffect/BT25/Blue/BT25_025.cs` | `None, OnLoseSecurity, WhenRemoveField` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT25_026` | `DCGO/Assets/Scripts/CardEffect/BT25/Blue/BT25_026.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `BT25_028` | `DCGO/Assets/Scripts/CardEffect/BT25/Blue/BT25_028.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT25_036` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_036.cs` | `None, OnDeclaration, SecuritySkill, WhenLinked` | `linked, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 1 |
| `BT25_038` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_038.cs` | `None, OnAddSecurity, OnLoseSecurity` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 1 |
| `BT25_039` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_039.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEndTurn, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT25_049` | `DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_049.cs` | `BeforePayCost, None, OnDetermineDoSecurityCheck` | `inherited, linked, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT25-021#7985@base` | `BT25-021` | 7985 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Digimon/BT25_021.asset` |
| `BT25-023#7987@base` | `BT25-023` | 7987 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Digimon/BT25_023.asset` |
| `BT25-024#7988@base` | `BT25-024` | 7988 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Digimon/BT25_024.asset` |
| `BT25-025#7989@base` | `BT25-025` | 7989 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Digimon/BT25_025.asset` |
| `BT25-026#7990@base` | `BT25-026` | 7990 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Digimon/BT25_026.asset` |
| `BT25-028#7992@base` | `BT25-028` | 7992 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Digimon/BT25_028.asset` |
| `BT25-028#7993@P1` | `BT25-028` | 7993 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Digimon/BT25_028_P1.asset` |
| `BT25-036#8002@base` | `BT25-036` | 8002 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_036.asset` |
| `BT25-038#8004@base` | `BT25-038` | 8004 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_038.asset` |
| `BT25-039#8005@base` | `BT25-039` | 8005 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_039.asset` |
| `BT25-049#8018@base` | `BT25-049` | 8018 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_049.asset` |

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
