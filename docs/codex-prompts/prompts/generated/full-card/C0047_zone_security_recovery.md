# C0047_zone_security_recovery - zone/security/recovery card porting 41

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0047_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT2_017` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_017.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT2_018` | `DCGO/Assets/Scripts/CardEffect/BT2/Red/BT2_018.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `-` | 2 |
| `BT2_021` | `DCGO/Assets/Scripts/CardEffect/BT2/Blue/BT2_021.cs` | `OnUnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `BT2_023` | `DCGO/Assets/Scripts/CardEffect/BT2/Blue/BT2_023.cs` | `None` | `static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `BT2_028` | `DCGO/Assets/Scripts/CardEffect/BT2/Blue/BT2_028.cs` | `OnEnterFieldAnyone, OnUnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |
| `BT2_030` | `DCGO/Assets/Scripts/CardEffect/BT2/Blue/BT2_030.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `BT2_034` | `DCGO/Assets/Scripts/CardEffect/BT2/Yellow/BT2_034.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity` | 3 |
| `BT2_036` | `DCGO/Assets/Scripts/CardEffect/BT2/Yellow/BT2_036.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `BT2_038` | `DCGO/Assets/Scripts/CardEffect/BT2/Yellow/BT2_038.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 4 |
| `BT2_040` | `DCGO/Assets/Scripts/CardEffect/BT2/Yellow/BT2_040.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-017#386@base` | `BT2-017` | 386 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_017.asset` |
| `BT2-017#387@P1` | `BT2-017` | 387 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_017_P1.asset` |
| `BT2-018#388@base` | `BT2-018` | 388 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_018.asset` |
| `BT2-018#389@P1` | `BT2-018` | 389 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Red/Digimon/BT2_018_P1.asset` |
| `BT2-021#395@base` | `BT2-021` | 395 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_021.asset` |
| `BT2-021#396@P1` | `BT2-021` | 396 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_021_P1.asset` |
| `BT2-021#8307@P2` | `BT2-021` | 8307 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_021_P2.asset` |
| `BT2-023#398@base` | `BT2-023` | 398 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_023.asset` |
| `BT2-028#407@base` | `BT2-028` | 407 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_028.asset` |
| `BT2-028#408@P1` | `BT2-028` | 408 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_028_P1.asset` |
| `BT2-028#409@P2` | `BT2-028` | 409 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_028_P2.asset` |
| `BT2-030#411@base` | `BT2-030` | 411 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_030.asset` |
| `BT2-030#412@P1` | `BT2-030` | 412 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Blue/Digimon/BT2_030_P1.asset` |
| `BT2-034#420@base` | `BT2-034` | 420 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_034.asset` |
| `BT2-034#421@P1` | `BT2-034` | 421 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_034_P1.asset` |
| `BT2-034#8309@P2` | `BT2-034` | 8309 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_034_P2.asset` |
| `BT2-036#426@base` | `BT2-036` | 426 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_036.asset` |
| `BT2-036#427@P1` | `BT2-036` | 427 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_036_P1.asset` |
| `BT2-038#430@base` | `BT2-038` | 430 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_038.asset` |
| `BT2-038#431@P1` | `BT2-038` | 431 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_038_P1.asset` |
| `BT2-038#432@P2` | `BT2-038` | 432 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_038_P2.asset` |
| `BT2-038#8310@P3` | `BT2-038` | 8310 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_038_P3.asset` |
| `BT2-040#434@base` | `BT2-040` | 434 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Yellow/Digimon/BT2_040.asset` |

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
