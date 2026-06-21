# C0289_special_digivolution_play - special digivolution/play mechanics card porting 54

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0289_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT1_107` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_107.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectJogress` | 3 |
| `BT1_108` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_108.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT1_109` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_109.cs` | `AfterPayCost, None, OptionSkill` | `background, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT1_110` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_110.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `BT1_111` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_111.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `BT1_112` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_112.cs` | `OnEndBattle, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT1_113` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_113.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT20_007` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_007.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectJogress` | 2 |
| `BT20_008` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_008.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectJogress` | 2 |
| `BT20_010` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_010.cs` | `None` | `inherited, static_or_continuous` | `SelectCard, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-107#302@base` | `BT1-107` | 302 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Option/BT1_107.asset` |
| `BT1-107#303@P1` | `BT1-107` | 303 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Option/BT1_107_P1.asset` |
| `BT1-107#304@P2` | `BT1-107` | 304 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Option/BT1_107_P2.asset` |
| `BT1-108#305@base` | `BT1-108` | 305 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_108.asset` |
| `BT1-108#306@P1` | `BT1-108` | 306 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_108_P1.asset` |
| `BT1-108#307@P2` | `BT1-108` | 307 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_108_P2.asset` |
| `BT1-109#308@base` | `BT1-109` | 308 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_109.asset` |
| `BT1-110#1754@P4` | `BT1-110` | 1754 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_110_P4.asset` |
| `BT1-110#309@base` | `BT1-110` | 309 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_110.asset` |
| `BT1-110#310@P1` | `BT1-110` | 310 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_110_P1.asset` |
| `BT1-110#311@P2` | `BT1-110` | 311 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_110_P2.asset` |
| `BT1-110#312@P3` | `BT1-110` | 312 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_110_P3.asset` |
| `BT1-111#313@base` | `BT1-111` | 313 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_111.asset` |
| `BT1-112#314@base` | `BT1-112` | 314 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_112.asset` |
| `BT1-113#315@base` | `BT1-113` | 315 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_113.asset` |
| `BT20-007#5086@base` | `BT20-007` | 5086 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_007.asset` |
| `BT20-007#8326@P1` | `BT20-007` | 8326 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_007_P1.asset` |
| `BT20-008#5087@base` | `BT20-008` | 5087 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_008.asset` |
| `BT20-008#8327@P1` | `BT20-008` | 8327 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_008_P1.asset` |
| `BT20-010#5089@base` | `BT20-010` | 5089 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_010.asset` |

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
