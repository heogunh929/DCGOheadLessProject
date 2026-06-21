# C0350_special_digivolution_play - special digivolution/play mechanics card porting 115

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0350_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX10_016` | `DCGO/Assets/Scripts/CardEffect/EX10/Green/EX10_016.cs` | `None, OnAllyAttack, OnDeclaration, WhenLinked` | `linked, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX10_017` | `DCGO/Assets/Scripts/CardEffect/EX10/Green/EX10_017.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnTappedAnyone, WhenLinked` | `inherited, linked, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 1 |
| `EX10_018` | `DCGO/Assets/Scripts/CardEffect/EX10/Green/EX10_018.cs` | `None, OnDestroyedAnyone, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX10_019` | `DCGO/Assets/Scripts/CardEffect/EX10/Green/EX10_019.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone, OnTappedAnyone, WhenLinked` | `inherited, linked, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectSecurity` | 2 |
| `EX10_020` | `DCGO/Assets/Scripts/CardEffect/EX10/Green/EX10_020.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEndTurn, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `EX10_021` | `DCGO/Assets/Scripts/CardEffect/EX10/Green/EX10_021.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX10_022` | `DCGO/Assets/Scripts/CardEffect/EX10/Green/EX10_022.cs` | `None, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX10_024` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_024.cs` | `None, OnAllyAttack, OnDeclaration, SecuritySkill` | `linked, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX10_027` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_027.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX10_029` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_029.cs` | `None, OnDeclaration, SecuritySkill, WhenLinked` | `inherited, linked, max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX10-016#7161@base` | `EX10-016` | 7161 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_016.asset` |
| `EX10-016#7283@P1` | `EX10-016` | 7283 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_016_P1.asset` |
| `EX10-017#7163@base` | `EX10-017` | 7163 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_017.asset` |
| `EX10-018#7164@base` | `EX10-018` | 7164 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_018.asset` |
| `EX10-019#7165@base` | `EX10-019` | 7165 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_019.asset` |
| `EX10-019#7284@P1` | `EX10-019` | 7284 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_019_P1.asset` |
| `EX10-020#7167@base` | `EX10-020` | 7167 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_020.asset` |
| `EX10-020#7285@P1` | `EX10-020` | 7285 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_020_P1.asset` |
| `EX10-021#7169@base` | `EX10-021` | 7169 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_021.asset` |
| `EX10-021#7286@P1` | `EX10-021` | 7286 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_021_P1.asset` |
| `EX10-022#7171@base` | `EX10-022` | 7171 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_022.asset` |
| `EX10-022#7287@P1` | `EX10-022` | 7287 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_022_P1.asset` |
| `EX10-024#7175@base` | `EX10-024` | 7175 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_024.asset` |
| `EX10-024#7289@P1` | `EX10-024` | 7289 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_024_P1.asset` |
| `EX10-027#7181@base` | `EX10-027` | 7181 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_027.asset` |
| `EX10-027#7292@P1` | `EX10-027` | 7292 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_027_P1.asset` |
| `EX10-029#7185@base` | `EX10-029` | 7185 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_029.asset` |
| `EX10-029#7294@P1` | `EX10-029` | 7294 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_029_P1.asset` |

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
