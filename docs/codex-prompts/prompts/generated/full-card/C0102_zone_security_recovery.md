# C0102_zone_security_recovery - zone/security/recovery card porting 96

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0102_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST7_06` | `DCGO/Assets/Scripts/CardEffect/ST7/Red/ST7_06.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 5 |
| `ST7_07` | `DCGO/Assets/Scripts/CardEffect/ST7/Red/ST7_07.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `ST8_01` | `DCGO/Assets/Scripts/CardEffect/ST8/Blue/ST8_01.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `-` | 2 |
| `ST8_02` | `DCGO/Assets/Scripts/CardEffect/ST8/Blue/ST8_02.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `-` | 1 |
| `ST8_03` | `DCGO/Assets/Scripts/CardEffect/ST8/Blue/ST8_03.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `ST8_06` | `DCGO/Assets/Scripts/CardEffect/ST8/Blue/ST8_06.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `-` | 4 |
| `ST8_08` | `DCGO/Assets/Scripts/CardEffect/ST8/Blue/ST8_08.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `-` | 1 |
| `ST8_09` | `DCGO/Assets/Scripts/CardEffect/ST8/Blue/ST8_09.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 1 |
| `ST9_02` | `DCGO/Assets/Scripts/CardEffect/ST9/Blue/ST9_02.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 3 |
| `ST9_06` | `DCGO/Assets/Scripts/CardEffect/ST9/Blue/ST9_06.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST7-06#574@base` | `ST7-06` | 574 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_06.asset` |
| `ST7-06#575@P1` | `ST7-06` | 575 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_06_P1.asset` |
| `ST7-06#576@P2` | `ST7-06` | 576 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_06_P2.asset` |
| `ST7-06#577@P3` | `ST7-06` | 577 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_06_P3.asset` |
| `ST7-06#578@P4` | `ST7-06` | 578 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_06_P4.asset` |
| `ST7-07#579@base` | `ST7-07` | 579 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST7/Red/Digimon/ST7_07.asset` |
| `ST8-01#1710@base` | `ST8-01` | 1710 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/DigiEgg/ST8_01.asset` |
| `ST8-01#4995@P1` | `ST8-01` | 4995 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/DigiEgg/ST8_01_P1.asset` |
| `ST8-02#1711@base` | `ST8-02` | 1711 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_02.asset` |
| `ST8-03#1712@base` | `ST8-03` | 1712 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_03.asset` |
| `ST8-03#1713@P1` | `ST8-03` | 1713 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_03_P1.asset` |
| `ST8-06#1720@base` | `ST8-06` | 1720 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_06.asset` |
| `ST8-06#1721@P1` | `ST8-06` | 1721 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_06_P1.asset` |
| `ST8-06#1722@P2` | `ST8-06` | 1722 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_06_P2.asset` |
| `ST8-06#1723@P3` | `ST8-06` | 1723 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_06_P3.asset` |
| `ST8-08#1725@base` | `ST8-08` | 1725 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_08.asset` |
| `ST8-09#1726@base` | `ST8-09` | 1726 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_09.asset` |
| `ST9-02#1731@base` | `ST9-02` | 1731 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_02.asset` |
| `ST9-02#1732@P1` | `ST9-02` | 1732 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_02_P1.asset` |
| `ST9-02#4997@P2` | `ST9-02` | 4997 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_02_P2.asset` |
| `ST9-06#1743@base` | `ST9-06` | 1743 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_06.asset` |
| `ST9-06#1744@P1` | `ST9-06` | 1744 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_06_P1.asset` |
| `ST9-06#5000@P2` | `ST9-06` | 5000 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_06_P2.asset` |
| `ST9-06#9085@P3` | `ST9-06` | 9085 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/ST9/Blue/Digimon/ST9_06_P3.asset` |

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
