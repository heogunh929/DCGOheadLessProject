# C0314_special_digivolution_play - special digivolution/play mechanics card porting 79

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0314_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 11
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT24_095` | `DCGO/Assets/Scripts/CardEffect/BT24/Green/BT24_095.cs` | `None, OnAllyAttack, OnDeclaration, OptionSkill, SecuritySkill` | `linked, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT24_096` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_096.cs` | `OnEnterFieldAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT24_097` | `DCGO/Assets/Scripts/CardEffect/BT24/Purple/BT24_097.cs` | `None, OnAllyAttack, OnDeclaration, OptionSkill, SecuritySkill` | `linked, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT25_007` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_007.cs` | `None, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `linked, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT25_010` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_010.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT25_011` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_011.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT25_013` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_013.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT25_016` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_016.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT25_017` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_017.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `BT25_018` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_018.cs` | `None, OnAllyAttack, OnEndTurn` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT24-095#7638@base` | `BT24-095` | 7638 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Green/Option/BT24_095.asset` |
| `BT24-096#7639@base` | `BT24-096` | 7639 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Option/BT24_096.asset` |
| `BT24-097#7640@base` | `BT24-097` | 7640 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT24/Purple/Option/BT24_097.asset` |
| `BT25-007#7969@base` | `BT25-007` | 7969 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_007.asset` |
| `BT25-010#7972@base` | `BT25-010` | 7972 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_010.asset` |
| `BT25-011#7973@base` | `BT25-011` | 7973 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_011.asset` |
| `BT25-013#7975@base` | `BT25-013` | 7975 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_013.asset` |
| `BT25-016#7978@base` | `BT25-016` | 7978 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_016.asset` |
| `BT25-017#7979@base` | `BT25-017` | 7979 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_017.asset` |
| `BT25-018#7980@base` | `BT25-018` | 7980 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_018.asset` |
| `BT25-018#7981@P1` | `BT25-018` | 7981 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_018_P1.asset` |

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
