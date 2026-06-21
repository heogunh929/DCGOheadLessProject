# C0390_special_digivolution_play - special digivolution/play mechanics card porting 155

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0390_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 33
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_038` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_038.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 7 |
| `P_039` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_039.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 6 |
| `P_040` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_040.cs` | `OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 6 |
| `P_043` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_043.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `P_059` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_059.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 2 |
| `P_060` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_060.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `P_061` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_061.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `P_062` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_062.cs` | `OnAllyAttack, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `P_063` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_063.cs` | `OnAllyAttack, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `P_064` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_064.cs` | `OnAllyAttack, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-038#10318@P2` | `P-038` | 10318 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_038_P2.asset` |
| `P-038#10319@P3` | `P-038` | 10319 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_038_P3.asset` |
| `P-038#10320@P4` | `P-038` | 10320 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_038_P4.asset` |
| `P-038#6073@base` | `P-038` | 6073 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_038.asset` |
| `P-038#6074@P1` | `P-038` | 6074 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_038_P1.asset` |
| `P-038#9217@P5` | `P-038` | 9217 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_038_P5.asset` |
| `P-038#9218@P6` | `P-038` | 9218 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_038_P6.asset` |
| `P-039#10351@P2` | `P-039` | 10351 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_039_P2.asset` |
| `P-039#10353@P3` | `P-039` | 10353 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_039_P3.asset` |
| `P-039#6075@base` | `P-039` | 6075 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_039.asset` |
| `P-039#6076@P1` | `P-039` | 6076 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_039_P1.asset` |
| `P-039#9219@P4` | `P-039` | 9219 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_039_P4.asset` |
| `P-039#9220@P5` | `P-039` | 9220 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Option/P_039_P5.asset` |
| `P-040#10362@P2` | `P-040` | 10362 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_040_P2.asset` |
| `P-040#10363@P3` | `P-040` | 10363 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_040_P3.asset` |
| `P-040#6077@base` | `P-040` | 6077 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_040.asset` |
| `P-040#6078@P1` | `P-040` | 6078 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_040_P1.asset` |
| `P-040#9221@P4` | `P-040` | 9221 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_040_P4.asset` |
| `P-040#9222@P5` | `P-040` | 9222 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Option/P_040_P5.asset` |
| `P-043#6083@base` | `P-043` | 6083 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_043.asset` |
| `P-043#6084@P1` | `P-043` | 6084 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_043_P1.asset` |
| `P-059#10364@P1` | `P-059` | 10364 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_059_P1.asset` |
| `P-059#6101@base` | `P-059` | 6101 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_059.asset` |
| `P-060#10370@P1` | `P-060` | 10370 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_060_P1.asset` |
| `P-060#6102@base` | `P-060` | 6102 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_060.asset` |
| `P-061#10368@P1` | `P-061` | 10368 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_061_P1.asset` |
| `P-061#6103@base` | `P-061` | 6103 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_061.asset` |
| `P-062#10377@P1` | `P-062` | 10377 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Tamer/P_062_P1.asset` |
| `P-062#6104@base` | `P-062` | 6104 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Tamer/P_062.asset` |
| `P-063#10373@P1` | `P-063` | 10373 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Tamer/P_063_P1.asset` |
| `P-063#6105@base` | `P-063` | 6105 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Tamer/P_063.asset` |
| `P-064#10369@P1` | `P-064` | 10369 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Tamer/P_064_P1.asset` |
| `P-064#6106@base` | `P-064` | 6106 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Tamer/P_064.asset` |

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
