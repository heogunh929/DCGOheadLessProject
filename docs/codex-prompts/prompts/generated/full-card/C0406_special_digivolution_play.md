# C0406_special_digivolution_play - special digivolution/play mechanics card porting 171

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0406_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST22_07` | `DCGO/Assets/Scripts/CardEffect/ST22/Yellow/ST22_07.cs` | `OnAllyAttack, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `ST22_08` | `DCGO/Assets/Scripts/CardEffect/ST22/Red/ST22_08.cs` | `None, OnDeclaration, OnEndTurn, OptionSkill, SecuritySkill` | `linked, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 1 |
| `ST22_09` | `DCGO/Assets/Scripts/CardEffect/ST22/Blue/ST22_09.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `inherited, linked, max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `ST22_11` | `DCGO/Assets/Scripts/CardEffect/ST22/Black/ST22_11.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `inherited, linked, max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `ST22_12` | `DCGO/Assets/Scripts/CardEffect/ST22/Red/ST22_12.cs` | `None, OnAllyAttack, OnDeclaration, WhenLinked` | `inherited, linked, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean` | 1 |
| `ST24_04` | `DCGO/Assets/Scripts/CardEffect/ST24/Yellow/ST24_04.cs` | `None, OnEnterFieldAnyone, OnMove` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `ST24_05` | `DCGO/Assets/Scripts/CardEffect/ST24/Yellow/ST24_05.cs` | `None` | `inherited, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectJogress` | 1 |
| `ST24_07` | `DCGO/Assets/Scripts/CardEffect/ST24/Yellow/ST24_07.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OptionSkill` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 3 |
| `ST24_11` | `DCGO/Assets/Scripts/CardEffect/ST24/Green/ST24_11.cs` | `None, OnDigivolutionCardDiscarded, OnTappedAnyone` | `max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST24_13` | `DCGO/Assets/Scripts/CardEffect/ST24/Yellow/ST24_13.cs` | `None, OnDigivolutionCardDiscarded, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST22-07#7498@base` | `ST22-07` | 7498 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Yellow/Tamer/ST22_07.asset` |
| `ST22-08#7499@base` | `ST22-08` | 7499 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Red/Option/ST22_08.asset` |
| `ST22-09#7500@base` | `ST22-09` | 7500 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Blue/Option/ST22_09.asset` |
| `ST22-11#7502@base` | `ST22-11` | 7502 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Black/Option/ST22_11.asset` |
| `ST22-12#7503@base` | `ST22-12` | 7503 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST22/Red/Digimon/ST22_12.asset` |
| `ST24-04#7913@base` | `ST24-04` | 7913 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Yellow/Digimon/ST24_04.asset` |
| `ST24-05#7914@base` | `ST24-05` | 7914 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Yellow/Digimon/ST24_05.asset` |
| `ST24-07#7916@base` | `ST24-07` | 7916 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Yellow/Digimon/ST24_07.asset` |
| `ST24-07#7931@P2` | `ST24-07` | 7931 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Yellow/Digimon/ST24_07_P2.asset` |
| `ST24-07#7932@P3` | `ST24-07` | 7932 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Yellow/Digimon/ST24_07_P3.asset` |
| `ST24-11#7920@base` | `ST24-11` | 7920 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Green/Digimon/ST24_11.asset` |
| `ST24-13#7922@base` | `ST24-13` | 7922 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Yellow/Tamer/ST24_13.asset` |

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
