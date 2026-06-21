# C0290_special_digivolution_play - special digivolution/play mechanics card porting 55

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0290_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT20_011` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_011.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT20_012` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_012.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT20_013` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_013.cs` | `None, OnDeclaration` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 1 |
| `BT20_014` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_014.cs` | `OnAllyAttack, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT20_015` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_015.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT20_018` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_018.cs` | `OnAllyAttack, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT20_019` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_019.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `BT20_020` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_020.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |
| `BT20_022` | `DCGO/Assets/Scripts/CardEffect/BT20/Blue/BT20_022.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT20_023` | `DCGO/Assets/Scripts/CardEffect/BT20/Blue/BT20_023.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT20-011#5090@base` | `BT20-011` | 5090 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_011.asset` |
| `BT20-012#5091@base` | `BT20-012` | 5091 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_012.asset` |
| `BT20-013#5092@base` | `BT20-013` | 5092 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_013.asset` |
| `BT20-014#5093@base` | `BT20-014` | 5093 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_014.asset` |
| `BT20-015#5094@base` | `BT20-015` | 5094 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_015.asset` |
| `BT20-018#5097@base` | `BT20-018` | 5097 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_018.asset` |
| `BT20-019#5098@base` | `BT20-019` | 5098 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_019.asset` |
| `BT20-020#5099@base` | `BT20-020` | 5099 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_020.asset` |
| `BT20-020#5202@P1` | `BT20-020` | 5202 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_020_P1.asset` |
| `BT20-020#5203@P2` | `BT20-020` | 5203 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_020_P2.asset` |
| `BT20-022#5101@base` | `BT20-022` | 5101 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_022.asset` |
| `BT20-023#5102@base` | `BT20-023` | 5102 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/Digimon/BT20_023.asset` |

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
