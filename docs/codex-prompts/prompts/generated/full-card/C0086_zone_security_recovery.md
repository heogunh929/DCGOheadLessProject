# C0086_zone_security_recovery - zone/security/recovery card porting 80

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0086_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 26
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `LM_009` | `DCGO/Assets/Scripts/CardEffect/LM/Green/LM_009.cs` | `BeforePayCost, OnTappedAnyone` | `max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `LM_010` | `DCGO/Assets/Scripts/CardEffect/LM/Green/LM_010.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `LM_011` | `DCGO/Assets/Scripts/CardEffect/LM/Green/LM_011.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `LM_012` | `DCGO/Assets/Scripts/CardEffect/LM/Green/LM_012.cs` | `OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `LM_018` | `DCGO/Assets/Scripts/CardEffect/LM/Purple/LM_018.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `LM_027` | `DCGO/Assets/Scripts/CardEffect/LM/Red/LM_027.cs` | `OnStartTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 4 |
| `LM_028` | `DCGO/Assets/Scripts/CardEffect/LM/Blue/LM_028.cs` | `OnStartTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 4 |
| `LM_029` | `DCGO/Assets/Scripts/CardEffect/LM/Yellow/LM_029.cs` | `OnStartTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 5 |
| `LM_030` | `DCGO/Assets/Scripts/CardEffect/LM/Green/LM_030.cs` | `OnStartTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 4 |
| `LM_031` | `DCGO/Assets/Scripts/CardEffect/LM/Black/LM_031.cs` | `OnStartTurn, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `LM-009#3255@base` | `LM-009` | 3255 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Digimon/LM_009.asset` |
| `LM-010#3256@base` | `LM-010` | 3256 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Digimon/LM_010.asset` |
| `LM-011#3257@base` | `LM-011` | 3257 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Digimon/LM_011.asset` |
| `LM-012#3258@base` | `LM-012` | 3258 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Digimon/LM_012.asset` |
| `LM-018#3264@base` | `LM-018` | 3264 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Purple/Digimon/LM_018.asset` |
| `LM-027#4037@base` | `LM-027` | 4037 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Red/Option/LM_027.asset` |
| `LM-027#7873@P1` | `LM-027` | 7873 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/LM/Red/Option/LM_027_P1.asset` |
| `LM-027#7874@P2` | `LM-027` | 7874 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/LM/Red/Option/LM_027_P2.asset` |
| `LM-027#7875@P3` | `LM-027` | 7875 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/LM/Red/Option/LM_027_P3.asset` |
| `LM-028#4038@base` | `LM-028` | 4038 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Option/LM_028.asset` |
| `LM-028#7876@P1` | `LM-028` | 7876 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Option/LM_028_P1.asset` |
| `LM-028#7877@P2` | `LM-028` | 7877 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Option/LM_028_P2.asset` |
| `LM-028#7878@P3` | `LM-028` | 7878 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Option/LM_028_P3.asset` |
| `LM-029#4039@base` | `LM-029` | 4039 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Yellow/Option/LM_029.asset` |
| `LM-029#7879@P1` | `LM-029` | 7879 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/LM/Yellow/Option/LM_029_P1.asset` |
| `LM-029#7880@P2` | `LM-029` | 7880 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/LM/Yellow/Option/LM_029_P2.asset` |
| `LM-029#7881@P3` | `LM-029` | 7881 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/LM/Yellow/Option/LM_029_P3.asset` |
| `LM-029#7882@P4` | `LM-029` | 7882 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/LM/Yellow/Option/LM_029_P4.asset` |
| `LM-030#4040@base` | `LM-030` | 4040 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Option/LM_030.asset` |
| `LM-030#7883@P1` | `LM-030` | 7883 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Option/LM_030_P1.asset` |
| `LM-030#7884@P2` | `LM-030` | 7884 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Option/LM_030_P2.asset` |
| `LM-030#7885@P3` | `LM-030` | 7885 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Option/LM_030_P3.asset` |
| `LM-031#4041@base` | `LM-031` | 4041 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Black/Option/LM_031.asset` |
| `LM-031#7886@P1` | `LM-031` | 7886 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/LM/Black/Option/LM_031_P1.asset` |
| `LM-031#7887@P2` | `LM-031` | 7887 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/LM/Black/Option/LM_031_P2.asset` |
| `LM-031#7888@P3` | `LM-031` | 7888 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/LM/Black/Option/LM_031_P3.asset` |

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
