# C0333_special_digivolution_play - special digivolution/play mechanics card porting 98

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0333_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT6_085` | `DCGO/Assets/Scripts/CardEffect/BT6/White/BT6_085.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT6_086` | `DCGO/Assets/Scripts/CardEffect/BT6/White/BT6_086.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT6_087` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_087.cs` | `OnDeclaration, OnEndTurn, OnMove, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `BT6_088` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_088.cs` | `OnDeclaration, OnEndTurn, OnMove, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `BT6_092` | `DCGO/Assets/Scripts/CardEffect/BT6/White/BT6_092.cs` | `None, OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `inherited, max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT6_093` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_093.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `BT6_094` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_094.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT6_095` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_095.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectJogress` | 2 |
| `BT6_096` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_096.cs` | `OnAllyAttack, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT6_097` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_097.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT6-085#1225@base` | `BT6-085` | 1225 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_085.asset` |
| `BT6-085#1226@P1` | `BT6-085` | 1226 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_085_P1.asset` |
| `BT6-086#1227@base` | `BT6-086` | 1227 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_086.asset` |
| `BT6-086#1228@P1` | `BT6-086` | 1228 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Digimon/BT6_086_P1.asset` |
| `BT6-087#1229@base` | `BT6-087` | 1229 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Tamer/BT6_087.asset` |
| `BT6-087#8716@P0` | `BT6-087` | 8716 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Tamer/BT6_087_P0.asset` |
| `BT6-087#8717@P1` | `BT6-087` | 8717 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Tamer/BT6_087_P1.asset` |
| `BT6-087#8718@P2` | `BT6-087` | 8718 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Tamer/BT6_087_P2.asset` |
| `BT6-088#1230@base` | `BT6-088` | 1230 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Tamer/BT6_088.asset` |
| `BT6-088#8719@P0` | `BT6-088` | 8719 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Tamer/BT6_088_P0.asset` |
| `BT6-088#8720@P1` | `BT6-088` | 8720 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Tamer/BT6_088_P1.asset` |
| `BT6-088#8721@P2` | `BT6-088` | 8721 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Tamer/BT6_088_P2.asset` |
| `BT6-092#1234@base` | `BT6-092` | 1234 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Tamer/BT6_092.asset` |
| `BT6-092#8726@P0` | `BT6-092` | 8726 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/White/Tamer/BT6_092_P0.asset` |
| `BT6-093#1235@base` | `BT6-093` | 1235 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Option/BT6_093.asset` |
| `BT6-093#1236@P1` | `BT6-093` | 1236 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Option/BT6_093_P1.asset` |
| `BT6-094#1237@base` | `BT6-094` | 1237 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Option/BT6_094.asset` |
| `BT6-094#8727@P0` | `BT6-094` | 8727 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Option/BT6_094_P0.asset` |
| `BT6-095#1238@base` | `BT6-095` | 1238 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Option/BT6_095.asset` |
| `BT6-095#8728@P0` | `BT6-095` | 8728 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Option/BT6_095_P0.asset` |
| `BT6-096#1239@base` | `BT6-096` | 1239 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Option/BT6_096.asset` |
| `BT6-097#1240@base` | `BT6-097` | 1240 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Option/BT6_097.asset` |
| `BT6-097#8729@P0` | `BT6-097` | 8729 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Option/BT6_097_P0.asset` |
| `BT6-097#8730@P1` | `BT6-097` | 8730 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Option/BT6_097_P1.asset` |

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
