# C0073_zone_security_recovery - zone/security/recovery card porting 67

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0073_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX2_025` | `DCGO/Assets/Scripts/CardEffect/EX2/Green/EX2_025.cs` | `OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 3 |
| `EX2_027` | `DCGO/Assets/Scripts/CardEffect/EX2/Green/EX2_027.cs` | `OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX2_030` | `DCGO/Assets/Scripts/CardEffect/EX2/Black/EX2_030.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `EX2_031` | `DCGO/Assets/Scripts/CardEffect/EX2/Black/EX2_031.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `EX2_036` | `DCGO/Assets/Scripts/CardEffect/EX2/Black/EX2_036.cs` | `None` | `inherited, modifier_duration, static_or_continuous, zone_movement` | `-` | 1 |
| `EX2_037` | `DCGO/Assets/Scripts/CardEffect/EX2/Black/EX2_037.cs` | `None, OnUnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `EX2_043` | `DCGO/Assets/Scripts/CardEffect/EX2/Purple/EX2_043.cs` | `OnDiscardHand, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `EX2_063` | `DCGO/Assets/Scripts/CardEffect/EX2/Black/EX2_063.cs` | `OnStartMainPhase, OnTappedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 1 |
| `EX2_064` | `DCGO/Assets/Scripts/CardEffect/EX2/Purple/EX2_064.cs` | `BeforePayCost, None, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `EX2_074` | `DCGO/Assets/Scripts/CardEffect/EX2/Purple/EX2_074.cs` | `None, OnDiscardLibrary, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX2-025#1957@base` | `EX2-025` | 1957 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Digimon/EX2_025.asset` |
| `EX2-025#1958@P1` | `EX2-025` | 1958 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Digimon/EX2_025_P1.asset` |
| `EX2-025#1959@P2` | `EX2-025` | 1959 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Digimon/EX2_025_P2.asset` |
| `EX2-027#1961@base` | `EX2-027` | 1961 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Digimon/EX2_027.asset` |
| `EX2-027#1962@P1` | `EX2-027` | 1962 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Green/Digimon/EX2_027_P1.asset` |
| `EX2-030#1966@base` | `EX2-030` | 1966 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Digimon/EX2_030.asset` |
| `EX2-031#1967@base` | `EX2-031` | 1967 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Digimon/EX2_031.asset` |
| `EX2-031#1968@P1` | `EX2-031` | 1968 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Digimon/EX2_031_P1.asset` |
| `EX2-036#1974@base` | `EX2-036` | 1974 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Digimon/EX2_036.asset` |
| `EX2-037#1975@base` | `EX2-037` | 1975 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Digimon/EX2_037.asset` |
| `EX2-043#1985@base` | `EX2-043` | 1985 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_043.asset` |
| `EX2-043#1986@P1` | `EX2-043` | 1986 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_043_P1.asset` |
| `EX2-063#2014@base` | `EX2-063` | 2014 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Black/Tamer/EX2_063.asset` |
| `EX2-064#2015@base` | `EX2-064` | 2015 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Tamer/EX2_064.asset` |
| `EX2-064#9118@P1` | `EX2-064` | 9118 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Tamer/EX2_064_P1.asset` |
| `EX2-074#2029@base` | `EX2-074` | 2029 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_074.asset` |
| `EX2-074#2030@P1` | `EX2-074` | 2030 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_074_P1.asset` |
| `EX2-074#9126@P2` | `EX2-074` | 9126 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Purple/Digimon/EX2_074_P2.asset` |

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
