# C0081_zone_security_recovery - zone/security/recovery card porting 75

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0081_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX7_018` | `DCGO/Assets/Scripts/CardEffect/EX7/Blue/EX7_018.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `EX7_021` | `DCGO/Assets/Scripts/CardEffect/EX7/Blue/EX7_021.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity` | 2 |
| `EX7_023` | `DCGO/Assets/Scripts/CardEffect/EX7/Blue/EX7_023.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 4 |
| `EX7_024` | `DCGO/Assets/Scripts/CardEffect/EX7/Yellow/EX7_024.cs` | `None` | `inherited, static_or_continuous` | `SelectCard, SelectSecurity` | 2 |
| `EX7_030_token` | `DCGO/Assets/Scripts/CardEffect/EX7/Yellow/EX7_030_token.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 0 |
| `EX7_035` | `DCGO/Assets/Scripts/CardEffect/EX7/Green/EX7_035.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX7_039` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_039.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `EX7_040` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_040.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 2 |
| `EX7_043` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_043.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectOrder` | 2 |
| `EX7_045` | `DCGO/Assets/Scripts/CardEffect/EX7/Black/EX7_045.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX7-018#3709@base` | `EX7-018` | 3709 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_018.asset` |
| `EX7-018#3710@P1` | `EX7-018` | 3710 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_018_P1.asset` |
| `EX7-021#3715@base` | `EX7-021` | 3715 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_021.asset` |
| `EX7-021#9170@P1` | `EX7-021` | 9170 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_021_P1.asset` |
| `EX7-023#3718@base` | `EX7-023` | 3718 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_023.asset` |
| `EX7-023#3719@P1` | `EX7-023` | 3719 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_023_P1.asset` |
| `EX7-023#9171@P2` | `EX7-023` | 9171 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_023_P2.asset` |
| `EX7-023#9172@P3` | `EX7-023` | 9172 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_023_P3.asset` |
| `EX7-024#3720@base` | `EX7-024` | 3720 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_024.asset` |
| `EX7-024#3721@P1` | `EX7-024` | 3721 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Yellow/Digimon/EX7_024_P1.asset` |
| `EX7-035#3741@base` | `EX7-035` | 3741 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_035.asset` |
| `EX7-035#3742@P1` | `EX7-035` | 3742 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Green/Digimon/EX7_035_P1.asset` |
| `EX7-039#3750@base` | `EX7-039` | 3750 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_039.asset` |
| `EX7-040#3751@base` | `EX7-040` | 3751 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_040.asset` |
| `EX7-040#3752@P1` | `EX7-040` | 3752 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_040_P1.asset` |
| `EX7-043#3757@base` | `EX7-043` | 3757 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_043.asset` |
| `EX7-043#3758@P1` | `EX7-043` | 3758 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_043_P1.asset` |
| `EX7-045#3761@base` | `EX7-045` | 3761 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_045.asset` |
| `EX7-045#3762@P1` | `EX7-045` | 3762 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Black/Digimon/EX7_045_P1.asset` |

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
