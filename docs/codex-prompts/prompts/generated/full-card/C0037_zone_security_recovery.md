# C0037_zone_security_recovery - zone/security/recovery card porting 31

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0037_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 14
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT22_005` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_005.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT22_006` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_006.cs` | `OnAddDigivolutionCards` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `BT22_018` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_018.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `BT22_021` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_021.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT22_027` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_027.cs` | `OnAddDigivolutionCards, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT22_040` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_040.cs` | `None, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `BT22_043` | `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_043.cs` | `None, OnAddDigivolutionCards, OnDeclaration` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT22_044` | `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_044.cs` | `None, OnAddDigivolutionCards, OnDeclaration` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT22_046` | `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_046.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT22_047` | `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_047.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT22-005#6993@base` | `BT22-005` | 6993 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/DigiEgg/BT22_005.asset` |
| `BT22-006#6994@base` | `BT22-006` | 6994 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/DigiEgg/BT22_006.asset` |
| `BT22-018#7010@base` | `BT22-018` | 7010 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_018.asset` |
| `BT22-018#7011@P1` | `BT22-018` | 7011 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_018_P1.asset` |
| `BT22-021#7014@base` | `BT22-021` | 7014 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_021.asset` |
| `BT22-027#7022@base` | `BT22-027` | 7022 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_027.asset` |
| `BT22-040#7038@base` | `BT22-040` | 7038 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_040.asset` |
| `BT22-043#7041@base` | `BT22-043` | 7041 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_043.asset` |
| `BT22-043#7042@P1` | `BT22-043` | 7042 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_043_P1.asset` |
| `BT22-044#7043@base` | `BT22-044` | 7043 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_044.asset` |
| `BT22-044#7044@P1` | `BT22-044` | 7044 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_044_P1.asset` |
| `BT22-046#7046@base` | `BT22-046` | 7046 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_046.asset` |
| `BT22-046#8432@P1` | `BT22-046` | 8432 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_046_P1.asset` |
| `BT22-047#7048@base` | `BT22-047` | 7048 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_047.asset` |

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
