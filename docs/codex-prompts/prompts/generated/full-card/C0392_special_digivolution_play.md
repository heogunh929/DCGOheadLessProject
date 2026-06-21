# C0392_special_digivolution_play - special digivolution/play mechanics card porting 157

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0392_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 35
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_105` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_105.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 6 |
| `P_106` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_106.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `P_107` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_107.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `P_108` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_108.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `P_110` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_110.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 3 |
| `P_112` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_112.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `P_115` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_115.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 2 |
| `P_116` | `DCGO/Assets/Scripts/CardEffect/P/White/P_116.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `P_118` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_118.cs` | `OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 4 |
| `P_119` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_119.cs` | `OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-105#10324@P1` | `P-105` | 10324 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_105_P1.asset` |
| `P-105#10325@P2` | `P-105` | 10325 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_105_P2.asset` |
| `P-105#6151@base` | `P-105` | 6151 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_105.asset` |
| `P-105#9230@P3` | `P-105` | 9230 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_105_P3.asset` |
| `P-105#9231@P4` | `P-105` | 9231 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_105_P4.asset` |
| `P-105#9232@P5` | `P-105` | 9232 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_105_P5.asset` |
| `P-106#10330@P1` | `P-106` | 10330 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_106_P1.asset` |
| `P-106#10331@P2` | `P-106` | 10331 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_106_P2.asset` |
| `P-106#6152@base` | `P-106` | 6152 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_106.asset` |
| `P-106#9233@P3` | `P-106` | 9233 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_106_P3.asset` |
| `P-106#9234@P4` | `P-106` | 9234 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_106_P4.asset` |
| `P-107#10334@P1` | `P-107` | 10334 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_107_P1.asset` |
| `P-107#10335@P2` | `P-107` | 10335 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_107_P2.asset` |
| `P-107#6153@base` | `P-107` | 6153 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_107.asset` |
| `P-107#9235@P3` | `P-107` | 9235 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_107_P3.asset` |
| `P-107#9236@P4` | `P-107` | 9236 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_107_P4.asset` |
| `P-108#10328@P1` | `P-108` | 10328 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_108_P1.asset` |
| `P-108#10329@P2` | `P-108` | 10329 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_108_P2.asset` |
| `P-108#6154@base` | `P-108` | 6154 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_108.asset` |
| `P-108#9237@P3` | `P-108` | 9237 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_108_P3.asset` |
| `P-108#9238@P4` | `P-108` | 9238 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_108_P4.asset` |
| `P-110#6156@base` | `P-110` | 6156 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_110.asset` |
| `P-110#9239@P1` | `P-110` | 9239 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_110_P1.asset` |
| `P-110#9240@P2` | `P-110` | 9240 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_110_P2.asset` |
| `P-112#6158@base` | `P-112` | 6158 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_112.asset` |
| `P-115#6161@base` | `P-115` | 6161 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_115.asset` |
| `P-115#9241@P1` | `P-115` | 9241 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_115_P1.asset` |
| `P-116#6162@base` | `P-116` | 6162 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_116.asset` |
| `P-118#10413@P1` | `P-118` | 10413 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_118_P1.asset` |
| `P-118#10414@P2` | `P-118` | 10414 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_118_P2.asset` |
| `P-118#6164@base` | `P-118` | 6164 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_118.asset` |
| `P-118#9243@P3` | `P-118` | 9243 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_118_P3.asset` |
| `P-119#10415@P1` | `P-119` | 10415 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_119_P1.asset` |
| `P-119#10416@P2` | `P-119` | 10416 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_119_P2.asset` |
| `P-119#6165@base` | `P-119` | 6165 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_119.asset` |

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
