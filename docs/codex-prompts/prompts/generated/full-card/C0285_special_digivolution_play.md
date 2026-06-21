# C0285_special_digivolution_play - special digivolution/play mechanics card porting 50

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0285_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT19_078` | `DCGO/Assets/Scripts/CardEffect/BT19/White/BT19_078.cs` | `OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 1 |
| `BT19_079` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_079.cs` | `BeforePayCost, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectSecurity, SelectDigiXros` | 2 |
| `BT19_080` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_080.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `BT19_081` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_081.cs` | `BeforePayCost, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectDigiXros` | 2 |
| `BT19_083` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_083.cs` | `OnEnterFieldAnyone, OnUseOption, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `BT19_085` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_085.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT19_086` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_086.cs` | `OnDeclaration, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT19_087` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_087.cs` | `BeforePayCost, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectSecurity, SelectDigiXros` | 2 |
| `BT19_088` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_088.cs` | `OnDeclaration, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT19_090` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_090.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT19-078#5057@base` | `BT19-078` | 5057 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/White/Digimon/BT19_078.asset` |
| `BT19-079#5058@base` | `BT19-079` | 5058 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Tamer/BT19_079.asset` |
| `BT19-079#5060@P1` | `BT19-079` | 5060 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Tamer/BT19_079_P1.asset` |
| `BT19-080#5061@base` | `BT19-080` | 5061 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Tamer/BT19_080.asset` |
| `BT19-080#8292@P1` | `BT19-080` | 8292 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Tamer/BT19_080_P1.asset` |
| `BT19-081#5062@base` | `BT19-081` | 5062 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Tamer/BT19_081.asset` |
| `BT19-081#5064@P1` | `BT19-081` | 5064 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Tamer/BT19_081_P1.asset` |
| `BT19-083#5065@base` | `BT19-083` | 5065 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Tamer/BT19_083.asset` |
| `BT19-083#8294@P1` | `BT19-083` | 8294 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Tamer/BT19_083_P1.asset` |
| `BT19-083#8295@P2` | `BT19-083` | 8295 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Tamer/BT19_083_P2.asset` |
| `BT19-085#5066@base` | `BT19-085` | 5066 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Tamer/BT19_085.asset` |
| `BT19-085#8297@P1` | `BT19-085` | 8297 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Tamer/BT19_085_P1.asset` |
| `BT19-086#5067@base` | `BT19-086` | 5067 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Tamer/BT19_086.asset` |
| `BT19-086#8298@P1` | `BT19-086` | 8298 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Tamer/BT19_086_P1.asset` |
| `BT19-087#4017@base` | `BT19-087` | 4017 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Tamer/BT19_087.asset` |
| `BT19-087#4018@P1` | `BT19-087` | 4018 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Tamer/BT19_087_P1.asset` |
| `BT19-088#5068@base` | `BT19-088` | 5068 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Tamer/BT19_088.asset` |
| `BT19-088#8299@P1` | `BT19-088` | 8299 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Tamer/BT19_088_P1.asset` |
| `BT19-090#5070@base` | `BT19-090` | 5070 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Option/BT19_090.asset` |

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
