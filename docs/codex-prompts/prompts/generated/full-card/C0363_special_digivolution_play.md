# C0363_special_digivolution_play - special digivolution/play mechanics card porting 128

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0363_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX2_071` | `DCGO/Assets/Scripts/CardEffect/EX2/Purple/EX2_071.cs` | `OnDiscardLibrary, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `EX2_072` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_072.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX3_001` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_001.cs` | `OnUnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `SelectJogress` | 1 |
| `EX3_004` | `DCGO/Assets/Scripts/CardEffect/EX3/Red/EX3_004.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectJogress` | 1 |
| `EX3_005` | `DCGO/Assets/Scripts/CardEffect/EX3/Red/EX3_005.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX3_007` | `DCGO/Assets/Scripts/CardEffect/EX3/Red/EX3_007.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `EX3_008` | `DCGO/Assets/Scripts/CardEffect/EX3/Red/EX3_008.cs` | `OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress` | 1 |
| `EX3_010` | `DCGO/Assets/Scripts/CardEffect/EX3/Red/EX3_010.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 4 |
| `EX3_011` | `DCGO/Assets/Scripts/CardEffect/EX3/Red/EX3_011.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |
| `EX3_014` | `DCGO/Assets/Scripts/CardEffect/EX3/Red/EX3_014.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectJogress, SelectDigiXros` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX2-071#2023@base` | `EX2-071` | 2023 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Option/EX2_071.asset` |
| `EX2-071#2024@P1` | `EX2-071` | 2024 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Option/EX2_071_P1.asset` |
| `EX2-071#6822@P0` | `EX2-071` | 6822 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Option/EX2_071_P0.asset` |
| `EX2-071#6823@P2` | `EX2-071` | 6823 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Option/EX2_071_P2.asset` |
| `EX2-072#2025@base` | `EX2-072` | 2025 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Option/EX2_072.asset` |
| `EX2-072#9123@P1` | `EX2-072` | 9123 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Option/EX2_072_P1.asset` |
| `EX3-001#2171@base` | `EX3-001` | 2171 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/DigiEgg/EX3_001.asset` |
| `EX3-004#2174@base` | `EX3-004` | 2174 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_004.asset` |
| `EX3-005#2175@base` | `EX3-005` | 2175 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_005.asset` |
| `EX3-007#2177@base` | `EX3-007` | 2177 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_007.asset` |
| `EX3-008#2178@base` | `EX3-008` | 2178 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_008.asset` |
| `EX3-010#2180@base` | `EX3-010` | 2180 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_010.asset` |
| `EX3-010#2181@P1` | `EX3-010` | 2181 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_010_P1.asset` |
| `EX3-010#6829@P2` | `EX3-010` | 6829 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_010_P2.asset` |
| `EX3-010#6830@P3` | `EX3-010` | 6830 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_010_P3.asset` |
| `EX3-011#2182@base` | `EX3-011` | 2182 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_011.asset` |
| `EX3-014#2187@base` | `EX3-014` | 2187 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_014.asset` |
| `EX3-014#2188@P1` | `EX3-014` | 2188 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Digimon/EX3_014_P1.asset` |

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
