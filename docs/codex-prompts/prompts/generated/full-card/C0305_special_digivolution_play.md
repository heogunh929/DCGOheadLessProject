# C0305_special_digivolution_play - special digivolution/play mechanics card porting 70

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0305_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT22_101` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_101.cs` | `OnDestroyedAnyone, OnStartTurn, OnUnTappedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `BT23_006` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_006.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT23_007` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_007.cs` | `None, OnDeclaration, OnDetermineDoSecurityCheck, SecuritySkill` | `inherited, linked, security, static_or_continuous, zone_movement` | `-` | 1 |
| `BT23_008` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_008.cs` | `None, OnAllyAttack, OnDeclaration` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT23_009` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_009.cs` | `None, OnDeclaration, OnEndTurn, WhenLinked` | `linked, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget` | 1 |
| `BT23_010` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_010.cs` | `None, OnAllyAttack, SecuritySkill` | `inherited, security, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT23_013` | `DCGO/Assets/Scripts/CardEffect/BT23/Red/BT23_013.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectAttackTarget, SelectJogress` | 2 |
| `BT23_016` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_016.cs` | `None, OnDeclaration, WhenLinked` | `linked, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT23_018` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_018.cs` | `None, OnDeclaration` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT23_021` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_021.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `linked, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress, SelectAppFusion` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT22-101#7125@base` | `BT22-101` | 7125 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Tamer/BT22_101.asset` |
| `BT22-101#7126@P1` | `BT22-101` | 7126 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Tamer/BT22_101_P1.asset` |
| `BT22-101#7127@P2` | `BT22-101` | 7127 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Tamer/BT22_101_P2.asset` |
| `BT23-006#7337@base` | `BT23-006` | 7337 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_006.asset` |
| `BT23-007#7338@base` | `BT23-007` | 7338 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_007.asset` |
| `BT23-008#7339@base` | `BT23-008` | 7339 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_008.asset` |
| `BT23-009#7340@base` | `BT23-009` | 7340 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_009.asset` |
| `BT23-010#7341@base` | `BT23-010` | 7341 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_010.asset` |
| `BT23-013#7344@base` | `BT23-013` | 7344 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_013.asset` |
| `BT23-013#7345@P1` | `BT23-013` | 7345 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Red/Digimon/BT23_013_P1.asset` |
| `BT23-016#7348@base` | `BT23-016` | 7348 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Digimon/BT23_016.asset` |
| `BT23-018#7350@base` | `BT23-018` | 7350 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Digimon/BT23_018.asset` |
| `BT23-021#7353@base` | `BT23-021` | 7353 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Digimon/BT23_021.asset` |

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
