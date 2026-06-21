# C0240_special_digivolution_play - special digivolution/play mechanics card porting 5

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0240_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 30
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT10_078` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_078.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT10_081` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_081.cs` | `OnAllyAttack, OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectCount, SelectJogress` | 2 |
| `BT10_083` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_083.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `BT10_085` | `DCGO/Assets/Scripts/CardEffect/BT10/White/BT10_085.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress` | 5 |
| `BT10_086` | `DCGO/Assets/Scripts/CardEffect/BT10/White/BT10_086.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectOrder, SelectSecurity, SelectJogress` | 3 |
| `BT10_087` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_087.cs` | `BeforePayCost, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectDigiXros` | 4 |
| `BT10_088` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_088.cs` | `BeforePayCost, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectSecurity, SelectDigiXros` | 3 |
| `BT10_089` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_089.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 3 |
| `BT10_090` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_090.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 3 |
| `BT10_091` | `DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_091.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT10-078#2124@base` | `BT10-078` | 2124 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_078.asset` |
| `BT10-078#4337@P0` | `BT10-078` | 4337 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_078_P0.asset` |
| `BT10-081#2127@base` | `BT10-081` | 2127 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_081.asset` |
| `BT10-081#4339@P1` | `BT10-081` | 4339 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_081_P1.asset` |
| `BT10-083#2130@base` | `BT10-083` | 2130 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_083.asset` |
| `BT10-083#2131@P1` | `BT10-083` | 2131 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_083_P1.asset` |
| `BT10-085#2133@base` | `BT10-085` | 2133 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Digimon/BT10_085.asset` |
| `BT10-085#2134@P1` | `BT10-085` | 2134 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Digimon/BT10_085_P1.asset` |
| `BT10-085#4341@P2` | `BT10-085` | 4341 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Digimon/BT10_085_P2.asset` |
| `BT10-085#4342@P3` | `BT10-085` | 4342 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Digimon/BT10_085_P3.asset` |
| `BT10-085#8100@P0` | `BT10-085` | 8100 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Digimon/BT10_085_P0.asset` |
| `BT10-086#2135@base` | `BT10-086` | 2135 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Digimon/BT10_086.asset` |
| `BT10-086#2136@P1` | `BT10-086` | 2136 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Digimon/BT10_086_P1.asset` |
| `BT10-086#8101@P2` | `BT10-086` | 8101 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/White/Digimon/BT10_086_P2.asset` |
| `BT10-087#2137@base` | `BT10-087` | 2137 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Tamer/BT10_087.asset` |
| `BT10-087#2138@P1` | `BT10-087` | 2138 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Tamer/BT10_087_P1.asset` |
| `BT10-087#4343@P0` | `BT10-087` | 4343 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Tamer/BT10_087_P0.asset` |
| `BT10-087#8102@P2` | `BT10-087` | 8102 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Red/Tamer/BT10_087_P2.asset` |
| `BT10-088#2139@base` | `BT10-088` | 2139 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Tamer/BT10_088.asset` |
| `BT10-088#2140@P1` | `BT10-088` | 2140 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Tamer/BT10_088_P1.asset` |
| `BT10-088#4344@P0` | `BT10-088` | 4344 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Tamer/BT10_088_P0.asset` |
| `BT10-089#2141@base` | `BT10-089` | 2141 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Tamer/BT10_089.asset` |
| `BT10-089#2142@P1` | `BT10-089` | 2142 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Tamer/BT10_089_P1.asset` |
| `BT10-089#4345@P0` | `BT10-089` | 4345 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Tamer/BT10_089_P0.asset` |
| `BT10-090#2143@base` | `BT10-090` | 2143 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Tamer/BT10_090.asset` |
| `BT10-090#2144@P1` | `BT10-090` | 2144 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Tamer/BT10_090_P1.asset` |
| `BT10-090#4346@P0` | `BT10-090` | 4346 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Tamer/BT10_090_P0.asset` |
| `BT10-091#2145@base` | `BT10-091` | 2145 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Tamer/BT10_091.asset` |
| `BT10-091#4347@P0` | `BT10-091` | 4347 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Tamer/BT10_091_P0.asset` |
| `BT10-091#4348@P1` | `BT10-091` | 4348 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT10/Green/Tamer/BT10_091_P1.asset` |

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
