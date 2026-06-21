# C0078_zone_security_recovery - zone/security/recovery card porting 72

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0078_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX5_016` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_016.cs` | `None, OnDeclaration, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `EX5_017` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_017.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `EX5_020` | `DCGO/Assets/Scripts/CardEffect/EX5/Blue/EX5_020.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 4 |
| `EX5_035` | `DCGO/Assets/Scripts/CardEffect/EX5/Green/EX5_035.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `EX5_039` | `DCGO/Assets/Scripts/CardEffect/EX5/Green/EX5_039.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `EX5_042` | `DCGO/Assets/Scripts/CardEffect/EX5/Green/EX5_042.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |
| `EX5_056` | `DCGO/Assets/Scripts/CardEffect/EX5/Purple/EX5_056.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 1 |
| `EX5_057` | `DCGO/Assets/Scripts/CardEffect/EX5/Purple/EX5_057.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 1 |
| `EX5_060` | `DCGO/Assets/Scripts/CardEffect/EX5/Purple/EX5_060.cs` | `OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `EX5_062` | `DCGO/Assets/Scripts/CardEffect/EX5/Purple/EX5_062.cs` | `None, OnDeclaration, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX5-016#3055@base` | `EX5-016` | 3055 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_016.asset` |
| `EX5-016#4211@P1` | `EX5-016` | 4211 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_016_P1.asset` |
| `EX5-017#3056@base` | `EX5-017` | 3056 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_017.asset` |
| `EX5-020#3059@base` | `EX5-020` | 3059 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_020.asset` |
| `EX5-020#4212@P1` | `EX5-020` | 4212 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_020_P1.asset` |
| `EX5-020#4213@P2` | `EX5-020` | 4213 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_020_P2.asset` |
| `EX5-020#4214@P3` | `EX5-020` | 4214 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Blue/Digimon/EX5_020_P3.asset` |
| `EX5-035#3074@base` | `EX5-035` | 3074 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_035.asset` |
| `EX5-039#3078@base` | `EX5-039` | 3078 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_039.asset` |
| `EX5-042#3081@base` | `EX5-042` | 3081 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_042.asset` |
| `EX5-042#4229@P1` | `EX5-042` | 4229 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Green/Digimon/EX5_042_P1.asset` |
| `EX5-056#3095@base` | `EX5-056` | 3095 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/Digimon/EX5_056.asset` |
| `EX5-057#3096@base` | `EX5-057` | 3096 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/Digimon/EX5_057.asset` |
| `EX5-060#3099@base` | `EX5-060` | 3099 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/Digimon/EX5_060.asset` |
| `EX5-062#3101@base` | `EX5-062` | 3101 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/Digimon/EX5_062.asset` |
| `EX5-062#4234@P1` | `EX5-062` | 4234 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX5/Purple/Digimon/EX5_062_P1.asset` |

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
