# C0097_zone_security_recovery - zone/security/recovery card porting 91

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0097_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST19_09` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_09.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 1 |
| `ST19_12` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_12.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `ST19_14` | `DCGO/Assets/Scripts/CardEffect/ST19/Yellow/ST19_14.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `ST1_08` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_08.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `ST1_12` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_12.cs` | `None, SecuritySkill` | `inherited, modifier_duration, security, static_or_continuous, zone_movement` | `-` | 2 |
| `ST20_02` | `DCGO/Assets/Scripts/CardEffect/ST20/Red/ST20_02.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `ST20_03` | `DCGO/Assets/Scripts/CardEffect/ST20/Red/ST20_03.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `ST20_07` | `DCGO/Assets/Scripts/CardEffect/ST20/Green/ST20_07.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 2 |
| `ST20_08` | `DCGO/Assets/Scripts/CardEffect/ST20/Green/ST20_08.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |
| `ST20_12` | `DCGO/Assets/Scripts/CardEffect/ST20/Red/ST20_12.cs` | `BeforePayCost, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST1-08#21@base` | `ST1-08` | 21 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_08.asset` |
| `ST1-08#22@P1` | `ST1-08` | 22 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Digimon/ST1_08_P1.asset` |
| `ST1-12#32@base` | `ST1-12` | 32 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Tamer/ST1_12.asset` |
| `ST1-12#33@P1` | `ST1-12` | 33 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST1/Red/Tamer/ST1_12_P1.asset` |
| `ST19-09#3841@base` | `ST19-09` | 3841 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_09.asset` |
| `ST19-12#3844@base` | `ST19-12` | 3844 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_12.asset` |
| `ST19-12#9049@P1` | `ST19-12` | 9049 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Digimon/ST19_12_P1.asset` |
| `ST19-14#3846@base` | `ST19-14` | 3846 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Tamer/ST19_14.asset` |
| `ST19-14#9050@P1` | `ST19-14` | 9050 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST19/Yellow/Tamer/ST19_14_P1.asset` |
| `ST20-02#5264@base` | `ST20-02` | 5264 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Red/Digimon/ST20_02.asset` |
| `ST20-02#9052@P1` | `ST20-02` | 9052 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Red/Digimon/ST20_02_P1.asset` |
| `ST20-03#5265@base` | `ST20-03` | 5265 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Red/Digimon/ST20_03.asset` |
| `ST20-03#9053@P1` | `ST20-03` | 9053 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Red/Digimon/ST20_03_P1.asset` |
| `ST20-07#5269@base` | `ST20-07` | 5269 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Green/Digimon/ST20_07.asset` |
| `ST20-07#9057@P1` | `ST20-07` | 9057 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Green/Digimon/ST20_07_P1.asset` |
| `ST20-08#5270@base` | `ST20-08` | 5270 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Green/Digimon/ST20_08.asset` |
| `ST20-08#9058@P1` | `ST20-08` | 9058 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Green/Digimon/ST20_08_P1.asset` |
| `ST20-12#5275@base` | `ST20-12` | 5275 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST20/Red/Tamer/ST20_12.asset` |

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
