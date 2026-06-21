# C0347_special_digivolution_play - special digivolution/play mechanics card porting 112

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0347_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT9_086` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_086.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectJogress` | 3 |
| `BT9_090` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_090.cs` | `BeforePayCost, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 2 |
| `BT9_093` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_093.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT9_094` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_094.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT9_095` | `DCGO/Assets/Scripts/CardEffect/BT9/Red/BT9_095.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `BT9_096` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_096.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT9_097` | `DCGO/Assets/Scripts/CardEffect/BT9/Blue/BT9_097.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT9_098` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_098.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT9_099` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_099.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT9_100` | `DCGO/Assets/Scripts/CardEffect/BT9/Green/BT9_100.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT9-086#1884@base` | `BT9-086` | 1884 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Tamer/BT9_086.asset` |
| `BT9-086#6813@P0` | `BT9-086` | 6813 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Tamer/BT9_086_P0.asset` |
| `BT9-086#6814@P1` | `BT9-086` | 6814 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Tamer/BT9_086_P1.asset` |
| `BT9-090#1888@base` | `BT9-090` | 1888 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Tamer/BT9_090.asset` |
| `BT9-090#9003@P0` | `BT9-090` | 9003 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Tamer/BT9_090_P0.asset` |
| `BT9-093#1891@base` | `BT9-093` | 1891 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Option/BT9_093.asset` |
| `BT9-094#1892@base` | `BT9-094` | 1892 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Option/BT9_094.asset` |
| `BT9-094#9011@P0` | `BT9-094` | 9011 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Option/BT9_094_P0.asset` |
| `BT9-095#1893@base` | `BT9-095` | 1893 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Option/BT9_095.asset` |
| `BT9-095#9012@P0` | `BT9-095` | 9012 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Red/Option/BT9_095_P0.asset` |
| `BT9-096#1894@base` | `BT9-096` | 1894 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Option/BT9_096.asset` |
| `BT9-097#1895@base` | `BT9-097` | 1895 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Option/BT9_097.asset` |
| `BT9-097#9013@P0` | `BT9-097` | 9013 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Blue/Option/BT9_097_P0.asset` |
| `BT9-098#1896@base` | `BT9-098` | 1896 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Option/BT9_098.asset` |
| `BT9-099#1897@base` | `BT9-099` | 1897 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Option/BT9_099.asset` |
| `BT9-099#9014@P0` | `BT9-099` | 9014 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Option/BT9_099_P0.asset` |
| `BT9-100#1898@base` | `BT9-100` | 1898 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Option/BT9_100.asset` |
| `BT9-100#9015@P0` | `BT9-100` | 9015 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Option/BT9_100_P0.asset` |

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
