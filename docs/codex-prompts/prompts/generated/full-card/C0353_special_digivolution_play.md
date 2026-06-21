# C0353_special_digivolution_play - special digivolution/play mechanics card porting 118

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0353_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX10_066` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_066.cs` | `OnEndTurn, OnStartTurn, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX10_068` | `DCGO/Assets/Scripts/CardEffect/EX10/White/EX10_068.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `EX10_069` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_069.cs` | `OnTappedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `EX10_070` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_070.cs` | `None, OnLinkCardDiscarded, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX10_071` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_071.cs` | `None, OnEndTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `EX10_072` | `DCGO/Assets/Scripts/CardEffect/EX10/White/EX10_072.cs` | `None, OnEndTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `EX10_073` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_073.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OnLinkCardDiscarded` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |
| `EX11_001` | `DCGO/Assets/Scripts/CardEffect/EX11/Red/EX11_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `EX11_006` | `DCGO/Assets/Scripts/CardEffect/EX11/White/EX11_006.cs` | `OnAllyAttack` | `inherited, linked, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `EX11_007` | `DCGO/Assets/Scripts/CardEffect/EX11/Red/EX11_007.cs` | `None, OnEnterFieldAnyone, OnMove` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX10-066#7251@base` | `EX10-066` | 7251 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Tamer/EX10_066.asset` |
| `EX10-066#7323@P1` | `EX10-066` | 7323 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Tamer/EX10_066_P1.asset` |
| `EX10-068#7255@base` | `EX10-068` | 7255 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/White/Tamer/EX10_068.asset` |
| `EX10-068#7325@P1` | `EX10-068` | 7325 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/White/Tamer/EX10_068_P1.asset` |
| `EX10-069#7257@base` | `EX10-069` | 7257 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Option/EX10_069.asset` |
| `EX10-069#7326@P1` | `EX10-069` | 7326 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Option/EX10_069_P1.asset` |
| `EX10-070#7259@base` | `EX10-070` | 7259 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Option/EX10_070.asset` |
| `EX10-070#7327@P1` | `EX10-070` | 7327 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Option/EX10_070_P1.asset` |
| `EX10-071#7261@base` | `EX10-071` | 7261 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Option/EX10_071.asset` |
| `EX10-071#7328@P1` | `EX10-071` | 7328 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Option/EX10_071_P1.asset` |
| `EX10-072#7263@base` | `EX10-072` | 7263 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/White/Option/EX10_072.asset` |
| `EX10-072#7329@P1` | `EX10-072` | 7329 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/White/Option/EX10_072_P1.asset` |
| `EX10-073#7265@base` | `EX10-073` | 7265 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_073.asset` |
| `EX10-073#7330@P1` | `EX10-073` | 7330 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_073_P1.asset` |
| `EX11-001#7657@base` | `EX11-001` | 7657 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/DigiEgg/EX11_001.asset` |
| `EX11-001#7658@P1` | `EX11-001` | 7658 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/DigiEgg/EX11_001_P1.asset` |
| `EX11-006#7667@base` | `EX11-006` | 7667 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/White/DigiEgg/EX11_006.asset` |
| `EX11-006#7668@P1` | `EX11-006` | 7668 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/White/DigiEgg/EX11_006_P1.asset` |
| `EX11-007#7669@base` | `EX11-007` | 7669 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_007.asset` |
| `EX11-007#7670@P1` | `EX11-007` | 7670 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Digimon/EX11_007_P1.asset` |

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
