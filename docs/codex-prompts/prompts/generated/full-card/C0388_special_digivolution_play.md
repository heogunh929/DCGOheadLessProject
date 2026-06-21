# C0388_special_digivolution_play - special digivolution/play mechanics card porting 153

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0388_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 26
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `LM_061` | `DCGO/Assets/Scripts/CardEffect/LM/Black/LM_061.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `LM_062` | `DCGO/Assets/Scripts/CardEffect/LM/Purple/LM_062.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `P_008` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_008.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 6 |
| `P_010` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_010.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 5 |
| `P_012` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_012.cs` | `OnDeclaration, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 2 |
| `P_016` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_016.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 7 |
| `P_021` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_021.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `P_022` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_022.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `P_023` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_023.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `P_024` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_024.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `LM-061#7896@base` | `LM-061` | 7896 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Black/Option/LM_061.asset` |
| `LM-062#7897@base` | `LM-062` | 7897 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Option/LM_062.asset` |
| `P-008#10337@P5` | `P-008` | 10337 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_008_P5.asset` |
| `P-008#6017@base` | `P-008` | 6017 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_008.asset` |
| `P-008#6018@P1` | `P-008` | 6018 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_008_P1.asset` |
| `P-008#6019@P2` | `P-008` | 6019 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_008_P2.asset` |
| `P-008#6020@P3` | `P-008` | 6020 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_008_P3.asset` |
| `P-008#6021@P4` | `P-008` | 6021 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_008_P4.asset` |
| `P-010#2869@base` | `P-010` | 2869 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/Digimon/RB1_007.asset` |
| `P-010#6025@base` | `P-010` | 6025 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_010.asset` |
| `P-010#6026@P1` | `P-010` | 6026 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_010_P1.asset` |
| `P-010#6027@P2` | `P-010` | 6027 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_010_P2.asset` |
| `P-010#6028@P3` | `P-010` | 6028 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_010_P3.asset` |
| `P-012#10339@P1` | `P-012` | 10339 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Tamer/P_012_P1.asset` |
| `P-012#6030@base` | `P-012` | 6030 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Tamer/P_012.asset` |
| `P-016#10341@P5` | `P-016` | 10341 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_016_P5.asset` |
| `P-016#10342@P6` | `P-016` | 10342 | `P6` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_016_P6.asset` |
| `P-016#6036@base` | `P-016` | 6036 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_016.asset` |
| `P-016#6037@P1` | `P-016` | 6037 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_016_P1.asset` |
| `P-016#6038@P2` | `P-016` | 6038 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_016_P2.asset` |
| `P-016#6039@P3` | `P-016` | 6039 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_016_P3.asset` |
| `P-016#6040@P4` | `P-016` | 6040 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_016_P4.asset` |
| `P-021#6046@base` | `P-021` | 6046 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_021.asset` |
| `P-022#6047@base` | `P-022` | 6047 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Option/P_022.asset` |
| `P-023#6048@base` | `P-023` | 6048 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_023.asset` |
| `P-024#6049@base` | `P-024` | 6049 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_024.asset` |

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
