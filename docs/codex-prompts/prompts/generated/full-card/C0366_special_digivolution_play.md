# C0366_special_digivolution_play - special digivolution/play mechanics card porting 131

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0366_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX3_061` | `DCGO/Assets/Scripts/CardEffect/EX3/Purple/EX3_061.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectAttackTarget, SelectJogress` | 2 |
| `EX3_062` | `DCGO/Assets/Scripts/CardEffect/EX3/Purple/EX3_062.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectJogress` | 4 |
| `EX3_063` | `DCGO/Assets/Scripts/CardEffect/EX3/Purple/EX3_063.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX3_064` | `DCGO/Assets/Scripts/CardEffect/EX3/Purple/EX3_064.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX3_066` | `DCGO/Assets/Scripts/CardEffect/EX3/Red/EX3_066.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `EX3_067` | `DCGO/Assets/Scripts/CardEffect/EX3/Blue/EX3_067.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `EX3_068` | `DCGO/Assets/Scripts/CardEffect/EX3/Yellow/EX3_068.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX3_069` | `DCGO/Assets/Scripts/CardEffect/EX3/Yellow/EX3_069.cs` | `OnDeclaration, OnEndTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `EX3_070` | `DCGO/Assets/Scripts/CardEffect/EX3/Green/EX3_070.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 1 |
| `EX3_071` | `DCGO/Assets/Scripts/CardEffect/EX3/Black/EX3_071.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX3-061#2248@base` | `EX3-061` | 2248 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_061.asset` |
| `EX3-061#2249@P1` | `EX3-061` | 2249 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_061_P1.asset` |
| `EX3-062#2250@base` | `EX3-062` | 2250 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_062.asset` |
| `EX3-062#9128@P1` | `EX3-062` | 9128 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_062_P1.asset` |
| `EX3-062#9129@P2` | `EX3-062` | 9129 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_062_P2.asset` |
| `EX3-062#9130@P3` | `EX3-062` | 9130 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_062_P3.asset` |
| `EX3-063#2251@base` | `EX3-063` | 2251 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_063.asset` |
| `EX3-063#2252@P1` | `EX3-063` | 2252 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_063_P1.asset` |
| `EX3-064#2253@base` | `EX3-064` | 2253 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_064.asset` |
| `EX3-064#2254@P1` | `EX3-064` | 2254 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Purple/Digimon/EX3_064_P1.asset` |
| `EX3-066#2257@base` | `EX3-066` | 2257 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Red/Option/EX3_066.asset` |
| `EX3-067#2258@base` | `EX3-067` | 2258 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Option/EX3_067.asset` |
| `EX3-067#9132@P1` | `EX3-067` | 9132 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Option/EX3_067_P1.asset` |
| `EX3-067#9133@P2` | `EX3-067` | 9133 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Blue/Option/EX3_067_P2.asset` |
| `EX3-068#2259@base` | `EX3-068` | 2259 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Option/EX3_068.asset` |
| `EX3-069#2260@base` | `EX3-069` | 2260 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Yellow/Option/EX3_069.asset` |
| `EX3-070#2261@base` | `EX3-070` | 2261 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Green/Option/EX3_070.asset` |
| `EX3-071#2262@base` | `EX3-071` | 2262 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX3/Black/Option/EX3_071.asset` |

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
