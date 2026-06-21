# C0359_special_digivolution_play - special digivolution/play mechanics card porting 124

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0359_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX1_071` | `DCGO/Assets/Scripts/CardEffect/EX1/White/EX1_071.cs` | `AfterPayCost, BeforePayCost, None, OptionSkill, SecuritySkill` | `background, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `EX1_072` | `DCGO/Assets/Scripts/CardEffect/EX1/White/EX1_072.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `EX2_001` | `DCGO/Assets/Scripts/CardEffect/EX2/Red/EX2_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `SelectJogress` | 4 |
| `EX2_007` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_007.cs` | `BeforePayCost, None, OnDeclaration` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `EX2_008` | `DCGO/Assets/Scripts/CardEffect/EX2/Red/EX2_008.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `EX2_009` | `DCGO/Assets/Scripts/CardEffect/EX2/Red/EX2_009.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 4 |
| `EX2_012` | `DCGO/Assets/Scripts/CardEffect/EX2/Red/EX2_012.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `EX2_019` | `DCGO/Assets/Scripts/CardEffect/EX2/Yellow/EX2_019.cs` | `OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `EX2_020` | `DCGO/Assets/Scripts/CardEffect/EX2/Yellow/EX2_020.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectSecurity, SelectJogress` | 1 |
| `EX2_021` | `DCGO/Assets/Scripts/CardEffect/EX2/Yellow/EX2_021.cs` | `OnEnterFieldAnyone, OnUseOption` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX1-071#1373@base` | `EX1-071` | 1373 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Option/EX1_071.asset` |
| `EX1-071#1374@P1` | `EX1-071` | 1374 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Option/EX1_071_P1.asset` |
| `EX1-072#1375@base` | `EX1-072` | 1375 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Option/EX1_072.asset` |
| `EX1-072#9099@P1` | `EX1-072` | 9099 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/White/Option/EX1_072_P1.asset` |
| `EX2-001#1914@base` | `EX2-001` | 1914 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/DigiEgg/EX2_001.asset` |
| `EX2-001#1915@P1` | `EX2-001` | 1915 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/DigiEgg/EX2_001_P1.asset` |
| `EX2-001#9103@P2` | `EX2-001` | 9103 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/DigiEgg/EX2_001_P2.asset` |
| `EX2-001#9104@P3` | `EX2-001` | 9104 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/DigiEgg/EX2_001_P3.asset` |
| `EX2-007#1924@base` | `EX2-007` | 1924 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/DigiEgg/EX2_007.asset` |
| `EX2-007#9108@P1` | `EX2-007` | 9108 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/DigiEgg/EX2_007_P1.asset` |
| `EX2-008#1925@base` | `EX2-008` | 1925 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_008.asset` |
| `EX2-008#1926@P1` | `EX2-008` | 1926 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_008_P1.asset` |
| `EX2-009#1927@base` | `EX2-009` | 1927 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_009.asset` |
| `EX2-009#1928@P1` | `EX2-009` | 1928 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_009_P1.asset` |
| `EX2-009#1929@P2` | `EX2-009` | 1929 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_009_P2.asset` |
| `EX2-009#1930@P3` | `EX2-009` | 1930 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_009_P3.asset` |
| `EX2-012#1937@base` | `EX2-012` | 1937 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_012.asset` |
| `EX2-012#1938@P1` | `EX2-012` | 1938 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Digimon/EX2_012_P1.asset` |
| `EX2-019#1947@base` | `EX2-019` | 1947 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_019.asset` |
| `EX2-019#1948@P1` | `EX2-019` | 1948 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_019_P1.asset` |
| `EX2-019#9109@P2` | `EX2-019` | 9109 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_019_P2.asset` |
| `EX2-020#1949@base` | `EX2-020` | 1949 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_020.asset` |
| `EX2-021#1950@base` | `EX2-021` | 1950 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Yellow/Digimon/EX2_021.asset` |

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
