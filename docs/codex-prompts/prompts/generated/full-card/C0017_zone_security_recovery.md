# C0017_zone_security_recovery - zone/security/recovery card porting 11

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0017_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT14_040` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_040.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |
| `BT14_041` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_041.cs` | `OnAddSecurity, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 7 |
| `BT14_042` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_042.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT14_043` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_043.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT14_047` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_047.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT14_050` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_050.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT14_051` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_051.cs` | `OnEndTurn` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT14_061` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_061.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard` | 1 |
| `BT14_065` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_065.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT14_067` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_067.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT14-040#2963@base` | `BT14-040` | 2963 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_040.asset` |
| `BT14-040#4656@P0` | `BT14-040` | 4656 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_040_P0.asset` |
| `BT14-041#2964@base` | `BT14-041` | 2964 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041.asset` |
| `BT14-041#4657@P0` | `BT14-041` | 4657 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P0.asset` |
| `BT14-041#4658@P1` | `BT14-041` | 4658 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P1.asset` |
| `BT14-041#4659@P2` | `BT14-041` | 4659 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P2.asset` |
| `BT14-041#4660@P3` | `BT14-041` | 4660 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P3.asset` |
| `BT14-041#8174@P4` | `BT14-041` | 8174 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P4.asset` |
| `BT14-041#8175@P5` | `BT14-041` | 8175 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P5.asset` |
| `BT14-042#2965@base` | `BT14-042` | 2965 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_042.asset` |
| `BT14-043#2966@base` | `BT14-043` | 2966 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_043.asset` |
| `BT14-047#2971@base` | `BT14-047` | 2971 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_047.asset` |
| `BT14-050#2975@base` | `BT14-050` | 2975 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_050.asset` |
| `BT14-051#2976@base` | `BT14-051` | 2976 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_051.asset` |
| `BT14-061#2987@base` | `BT14-061` | 2987 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_061.asset` |
| `BT14-065#2991@base` | `BT14-065` | 2991 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_065.asset` |
| `BT14-067#2993@base` | `BT14-067` | 2993 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_067.asset` |
| `BT14-067#4679@P0` | `BT14-067` | 4679 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_067_P0.asset` |

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
