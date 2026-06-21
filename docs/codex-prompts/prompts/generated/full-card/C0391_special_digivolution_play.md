# C0391_special_digivolution_play - special digivolution/play mechanics card porting 156

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0391_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_087` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_087.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 1 |
| `P_088` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_088.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 4 |
| `P_090` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_090.cs` | `OnEndBattle, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `P_092` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_092.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `P_094` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_094.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectJogress, SelectDigiXros` | 2 |
| `P_095` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_095.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `P_096` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_096.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectInteger, SelectOrder, SelectSecurity, SelectJogress` | 1 |
| `P_099` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_099.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `P_103` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_103.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `P_104` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_104.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-087#6133@base` | `P-087` | 6133 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_087.asset` |
| `P-088#10394@P1` | `P-088` | 10394 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_088_P1.asset` |
| `P-088#10395@P2` | `P-088` | 10395 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_088_P2.asset` |
| `P-088#10396@P3` | `P-088` | 10396 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_088_P3.asset` |
| `P-088#6134@base` | `P-088` | 6134 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_088.asset` |
| `P-090#6136@base` | `P-090` | 6136 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_090.asset` |
| `P-092#6138@base` | `P-092` | 6138 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_092.asset` |
| `P-092#9224@P1` | `P-092` | 9224 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_092_P1.asset` |
| `P-094#6140@base` | `P-094` | 6140 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_094.asset` |
| `P-094#9225@P1` | `P-094` | 9225 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_094_P1.asset` |
| `P-095#6141@base` | `P-095` | 6141 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_095.asset` |
| `P-096#6142@base` | `P-096` | 6142 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_096.asset` |
| `P-099#6145@base` | `P-099` | 6145 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_099.asset` |
| `P-103#10326@P1` | `P-103` | 10326 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_103_P1.asset` |
| `P-103#10327@P2` | `P-103` | 10327 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_103_P2.asset` |
| `P-103#6149@base` | `P-103` | 6149 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_103.asset` |
| `P-103#9226@P3` | `P-103` | 9226 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_103_P3.asset` |
| `P-103#9227@P4` | `P-103` | 9227 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_103_P4.asset` |
| `P-104#10332@P1` | `P-104` | 10332 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_104_P1.asset` |
| `P-104#10333@P2` | `P-104` | 10333 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_104_P2.asset` |
| `P-104#6150@base` | `P-104` | 6150 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_104.asset` |
| `P-104#9228@P3` | `P-104` | 9228 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_104_P3.asset` |
| `P-104#9229@P4` | `P-104` | 9229 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_104_P4.asset` |

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
