# C0236_special_digivolution_play - special digivolution/play mechanics card porting 1

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0236_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `AD1_001` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_001.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `AD1_002` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_002.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectJogress` | 2 |
| `AD1_003` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_003.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 1 |
| `AD1_004` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_004.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `AD1_006` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_006.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectDigiXros` | 2 |
| `AD1_008` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_008.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `AD1_009` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_009.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress, SelectAssembly` | 2 |
| `AD1_010` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_010.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `AD1_011` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectAttackTarget, SelectJogress` | 1 |
| `AD1_013` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_013.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectDigiXros` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `AD1-001#7817@base` | `AD1-001` | 7817 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001.asset` |
| `AD1-001#7818@P1` | `AD1-001` | 7818 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001_P1.asset` |
| `AD1-002#7819@base` | `AD1-002` | 7819 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002.asset` |
| `AD1-002#7820@P1` | `AD1-002` | 7820 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002_P1.asset` |
| `AD1-003#7821@base` | `AD1-003` | 7821 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_003.asset` |
| `AD1-004#7822@base` | `AD1-004` | 7822 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004.asset` |
| `AD1-004#7823@P1` | `AD1-004` | 7823 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004_P1.asset` |
| `AD1-006#7826@base` | `AD1-006` | 7826 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006.asset` |
| `AD1-006#7827@P1` | `AD1-006` | 7827 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006_P1.asset` |
| `AD1-008#7830@base` | `AD1-008` | 7830 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008.asset` |
| `AD1-008#7831@P1` | `AD1-008` | 7831 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008_P1.asset` |
| `AD1-009#7832@base` | `AD1-009` | 7832 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009.asset` |
| `AD1-009#7833@P1` | `AD1-009` | 7833 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009_P1.asset` |
| `AD1-010#7834@base` | `AD1-010` | 7834 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_010.asset` |
| `AD1-011#7835@base` | `AD1-011` | 7835 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_011.asset` |
| `AD1-013#7838@base` | `AD1-013` | 7838 | `base` | `None` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_013.asset` |

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
