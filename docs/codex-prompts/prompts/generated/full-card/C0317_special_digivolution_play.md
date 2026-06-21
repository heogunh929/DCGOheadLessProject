# C0317_special_digivolution_play - special digivolution/play mechanics card porting 82

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0317_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT25_088` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_088.cs` | `BeforePayCost, None, OnLoseSecurity, OnStartTurn, SecuritySkill` | `linked, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectInteger, SelectSecurity` | 2 |
| `BT25_089` | `DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_089.cs` | `None, OnDeclaration, OnEndTurn, OnStartMainPhase, SecuritySkill` | `linked, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectSecurity, SelectAppFusion` | 1 |
| `BT25_090` | `DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_090.cs` | `BeforePayCost, None, OnStartTurn, OnTappedAnyone, SecuritySkill` | `linked, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectInteger, SelectSecurity` | 2 |
| `BT25_093` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_093.cs` | `None, OnAllyAttack, OnDeclaration, OptionSkill, SecuritySkill` | `linked, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT25_094` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_094.cs` | `None, OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectInteger, SelectSecurity, SelectJogress` | 1 |
| `BT25_095` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_095.cs` | `None, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectInteger, SelectSecurity, SelectJogress` | 1 |
| `BT25_096` | `DCGO/Assets/Scripts/CardEffect/BT25/Blue/BT25_096.cs` | `BeforePayCost, None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 1 |
| `BT25_098` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_098.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `BT25_099` | `DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_099.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectInteger, SelectSecurity, SelectJogress` | 1 |
| `BT25_102` | `DCGO/Assets/Scripts/CardEffect/BT25/Black/BT25_102.cs` | `None, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectInteger, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT25-088#8068@base` | `BT25-088` | 8068 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Tamer/BT25_088.asset` |
| `BT25-088#8069@P1` | `BT25-088` | 8069 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Tamer/BT25_088_P1.asset` |
| `BT25-089#8070@base` | `BT25-089` | 8070 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Tamer/BT25_089.asset` |
| `BT25-090#8071@base` | `BT25-090` | 8071 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Tamer/BT25_090.asset` |
| `BT25-090#8072@P1` | `BT25-090` | 8072 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Tamer/BT25_090_P1.asset` |
| `BT25-093#8079@base` | `BT25-093` | 8079 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Option/BT25_093.asset` |
| `BT25-094#8080@base` | `BT25-094` | 8080 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Option/BT25_094.asset` |
| `BT25-095#8081@base` | `BT25-095` | 8081 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Option/BT25_095.asset` |
| `BT25-096#8082@base` | `BT25-096` | 8082 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Option/BT25_096.asset` |
| `BT25-098#8084@base` | `BT25-098` | 8084 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Option/BT25_098.asset` |
| `BT25-099#8085@base` | `BT25-099` | 8085 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Option/BT25_099.asset` |
| `BT25-102#8088@base` | `BT25-102` | 8088 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Black/Option/BT25_102.asset` |

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
