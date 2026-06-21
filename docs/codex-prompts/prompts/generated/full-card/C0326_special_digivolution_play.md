# C0326_special_digivolution_play - special digivolution/play mechanics card porting 91

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0326_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT4_106` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_106.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT4_107` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_107.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress, SelectBurstDigivolution` | 2 |
| `BT4_108` | `DCGO/Assets/Scripts/CardEffect/BT4/Green/BT4_108.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT4_109` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_109.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT4_110` | `DCGO/Assets/Scripts/CardEffect/BT4/Black/BT4_110.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT4_111` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_111.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `BT4_112` | `DCGO/Assets/Scripts/CardEffect/BT4/Purple/BT4_112.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT4_115` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_115.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `BT5_001` | `DCGO/Assets/Scripts/CardEffect/BT5/Red/BT5_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `SelectJogress` | 5 |
| `BT5_002` | `DCGO/Assets/Scripts/CardEffect/BT5/Blue/BT5_002.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT4-106#915@base` | `BT4-106` | 915 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Option/BT4_106.asset` |
| `BT4-107#8560@P0` | `BT4-107` | 8560 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Option/BT4_107_P0.asset` |
| `BT4-107#916@base` | `BT4-107` | 916 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Option/BT4_107.asset` |
| `BT4-108#917@base` | `BT4-108` | 917 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Green/Option/BT4_108.asset` |
| `BT4-109#8561@P1` | `BT4-109` | 8561 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Option/BT4_109_P1.asset` |
| `BT4-109#8562@P2` | `BT4-109` | 8562 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Option/BT4_109_P2.asset` |
| `BT4-109#918@base` | `BT4-109` | 918 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Option/BT4_109.asset` |
| `BT4-110#8563@P0` | `BT4-110` | 8563 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Option/BT4_110_P0.asset` |
| `BT4-110#919@base` | `BT4-110` | 919 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Black/Option/BT4_110.asset` |
| `BT4-111#8564@P1` | `BT4-111` | 8564 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Option/BT4_111_P1.asset` |
| `BT4-111#920@base` | `BT4-111` | 920 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Option/BT4_111.asset` |
| `BT4-112#8565@P0` | `BT4-112` | 8565 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Option/BT4_112_P0.asset` |
| `BT4-112#921@base` | `BT4-112` | 921 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Purple/Option/BT4_112.asset` |
| `BT4-115#8566@P2` | `BT4-115` | 8566 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_115_P2.asset` |
| `BT4-115#926@base` | `BT4-115` | 926 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_115.asset` |
| `BT4-115#927@P1` | `BT4-115` | 927 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_115_P1.asset` |
| `BT5-001#8567@P0` | `BT5-001` | 8567 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/DigiEgg/BT5_001_P0.asset` |
| `BT5-001#8568@P1` | `BT5-001` | 8568 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/DigiEgg/BT5_001_P1.asset` |
| `BT5-001#8569@P2` | `BT5-001` | 8569 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/DigiEgg/BT5_001_P2.asset` |
| `BT5-001#928@base` | `BT5-001` | 928 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/DigiEgg/BT5_001.asset` |
| `BT5-001#929@P1` | `BT5-001` | 929 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Red/Digimon/BT5_001_P1.asset` |
| `BT5-002#8570@P0` | `BT5-002` | 8570 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/DigiEgg/BT5_002_P0.asset` |
| `BT5-002#930@base` | `BT5-002` | 930 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Blue/DigiEgg/BT5_002.asset` |

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
