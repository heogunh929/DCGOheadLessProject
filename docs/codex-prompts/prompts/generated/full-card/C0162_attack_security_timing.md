# C0162_attack_security_timing - attack/security timing card porting 35

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0162_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX1_008` | `DCGO/Assets/Scripts/CardEffect/EX1/Red/EX1_008.cs` | `OnAllyAttack, OnDetermineDoSecurityCheck` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |
| `EX1_009` | `DCGO/Assets/Scripts/CardEffect/EX1/Red/EX1_009.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX1_010` | `DCGO/Assets/Scripts/CardEffect/EX1/Red/EX1_010.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `EX1_016` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_016.cs` | `None` | `static_or_continuous` | `SelectAttackTarget` | 1 |
| `EX1_017` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_017.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `EX1_018` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_018.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 2 |
| `EX1_020` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_020.cs` | `None, OnDigivolutionCardDiscarded` | `max_count_per_turn, static_or_continuous` | `SelectAttackTarget` | 1 |
| `EX1_021` | `DCGO/Assets/Scripts/CardEffect/EX1/Blue/EX1_021.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX1_025` | `DCGO/Assets/Scripts/CardEffect/EX1/Yellow/EX1_025.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |
| `EX1_026` | `DCGO/Assets/Scripts/CardEffect/EX1/Yellow/EX1_026.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX1-008#1274@base` | `EX1-008` | 1274 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_008.asset` |
| `EX1-008#1275@P1` | `EX1-008` | 1275 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_008_P1.asset` |
| `EX1-008#1276@P2` | `EX1-008` | 1276 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_008_P2.asset` |
| `EX1-009#1277@base` | `EX1-009` | 1277 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_009.asset` |
| `EX1-009#1278@P1` | `EX1-009` | 1278 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_009_P1.asset` |
| `EX1-010#1279@base` | `EX1-010` | 1279 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_010.asset` |
| `EX1-016#1290@base` | `EX1-016` | 1290 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_016.asset` |
| `EX1-017#1291@base` | `EX1-017` | 1291 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_017.asset` |
| `EX1-017#1292@P1` | `EX1-017` | 1292 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_017_P1.asset` |
| `EX1-018#1293@base` | `EX1-018` | 1293 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_018.asset` |
| `EX1-018#1294@P1` | `EX1-018` | 1294 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_018_P1.asset` |
| `EX1-020#1297@base` | `EX1-020` | 1297 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_020.asset` |
| `EX1-021#1298@base` | `EX1-021` | 1298 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_021.asset` |
| `EX1-021#1299@P1` | `EX1-021` | 1299 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Blue/Digimon/EX1_021_P1.asset` |
| `EX1-025#1306@base` | `EX1-025` | 1306 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_025.asset` |
| `EX1-025#1307@P1` | `EX1-025` | 1307 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_025_P1.asset` |
| `EX1-026#1308@base` | `EX1-026` | 1308 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_026.asset` |
| `EX1-026#1309@P1` | `EX1-026` | 1309 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Yellow/Digimon/EX1_026_P1.asset` |

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
