# C0408_special_digivolution_play - special digivolution/play mechanics card porting 173

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0408_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST4_13` | `DCGO/Assets/Scripts/CardEffect/ST4/Green/ST4_13.cs` | `OnDeclaration, OnDetermineDoSecurityCheck` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 2 |
| `ST4_15` | `DCGO/Assets/Scripts/CardEffect/ST4/Green/ST4_15.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST4_16` | `DCGO/Assets/Scripts/CardEffect/ST4/Green/ST4_16.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST5_13` | `DCGO/Assets/Scripts/CardEffect/ST5/Black/ST5_13.cs` | `None, OnDeclaration` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectBurstDigivolution` | 1 |
| `ST5_15` | `DCGO/Assets/Scripts/CardEffect/ST5/Black/ST5_15.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST5_16` | `DCGO/Assets/Scripts/CardEffect/ST5/Black/ST5_16.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `ST6_13` | `DCGO/Assets/Scripts/CardEffect/ST6/Purple/ST6_13.cs` | `None, OnDeclaration` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectBurstDigivolution` | 1 |
| `ST6_15` | `DCGO/Assets/Scripts/CardEffect/ST6/Purple/ST6_15.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `ST6_16` | `DCGO/Assets/Scripts/CardEffect/ST6/Purple/ST6_16.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `ST7_03` | `DCGO/Assets/Scripts/CardEffect/ST7/Red/ST7_03.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 7 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST4-13#128@base` | `ST4-13` | 128 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_13.asset` |
| `ST4-13#129@P1` | `ST4-13` | 129 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Digimon/ST4_13_P1.asset` |
| `ST4-15#131@base` | `ST4-15` | 131 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Option/ST4_15.asset` |
| `ST4-16#132@base` | `ST4-16` | 132 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST4/Green/Option/ST4_16.asset` |
| `ST5-13#335@base` | `ST5-13` | 335 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_13.asset` |
| `ST5-15#337@base` | `ST5-15` | 337 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Option/ST5_15.asset` |
| `ST5-16#338@base` | `ST5-16` | 338 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Option/ST5_16.asset` |
| `ST6-13#355@base` | `ST6-13` | 355 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Digimon/ST6_13.asset` |
| `ST6-15#358@base` | `ST6-15` | 358 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Option/ST6_15.asset` |
| `ST6-15#9079@P1` | `ST6-15` | 9079 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Option/ST6_15_P1.asset` |
| `ST6-16#359@base` | `ST6-16` | 359 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST6/Purple/Option/ST6_16.asset` |
| `ST7-03#566@base` | `ST7-03` | 566 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_03.asset` |
| `ST7-03#567@P1` | `ST7-03` | 567 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_03_P1.asset` |
| `ST7-03#568@P2` | `ST7-03` | 568 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_03_P2.asset` |
| `ST7-03#569@P3` | `ST7-03` | 569 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_03_P3.asset` |
| `ST7-03#570@P4` | `ST7-03` | 570 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_03_P4.asset` |
| `ST7-03#9080@P5` | `ST7-03` | 9080 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_03_P5.asset` |
| `ST7-03#9081@P6` | `ST7-03` | 9081 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_03_P6.asset` |

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
