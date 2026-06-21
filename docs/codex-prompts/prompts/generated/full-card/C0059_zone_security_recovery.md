# C0059_zone_security_recovery - zone/security/recovery card porting 53

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0059_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 30
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT6_072` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_072.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `BT6_077` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_077.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand` | 3 |
| `BT6_080` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_080.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `BT6_081` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_081.cs` | `OnDiscardHand, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 3 |
| `BT6_090` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_090.cs` | `OnDestroyedAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 2 |
| `BT6_112` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_112.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 3 |
| `BT7_002` | `DCGO/Assets/Scripts/CardEffect/BT7/Blue/BT7_002.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 4 |
| `BT7_005` | `DCGO/Assets/Scripts/CardEffect/BT7/Black/BT7_005.cs` | `OnAddDigivolutionCards` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 8 |
| `BT7_010` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_010.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT7_013` | `DCGO/Assets/Scripts/CardEffect/BT7/Red/BT7_013.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT6-072#1206@base` | `BT6-072` | 1206 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_072.asset` |
| `BT6-072#8708@P0` | `BT6-072` | 8708 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_072_P0.asset` |
| `BT6-077#1211@base` | `BT6-077` | 1211 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_077.asset` |
| `BT6-077#1212@P1` | `BT6-077` | 1212 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_077_P1.asset` |
| `BT6-077#8710@P0` | `BT6-077` | 8710 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_077_P0.asset` |
| `BT6-080#1216@base` | `BT6-080` | 1216 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_080.asset` |
| `BT6-080#8711@P0` | `BT6-080` | 8711 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_080_P0.asset` |
| `BT6-081#1217@base` | `BT6-081` | 1217 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_081.asset` |
| `BT6-081#1218@P1` | `BT6-081` | 1218 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_081_P1.asset` |
| `BT6-081#8712@P2` | `BT6-081` | 8712 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_081_P2.asset` |
| `BT6-090#1232@base` | `BT6-090` | 1232 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Tamer/BT6_090.asset` |
| `BT6-090#8724@P0` | `BT6-090` | 8724 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Tamer/BT6_090_P0.asset` |
| `BT6-112#1257@base` | `BT6-112` | 1257 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_112.asset` |
| `BT6-112#1258@P1` | `BT6-112` | 1258 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_112_P1.asset` |
| `BT6-112#8742@P2` | `BT6-112` | 8742 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_112_P2.asset` |
| `BT7-002#1380@base` | `BT7-002` | 1380 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/DigiEgg/BT7_002.asset` |
| `BT7-002#8744@P0` | `BT7-002` | 8744 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/DigiEgg/BT7_002_P0.asset` |
| `BT7-002#8745@P1` | `BT7-002` | 8745 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/DigiEgg/BT7_002_P1.asset` |
| `BT7-002#8746@P2` | `BT7-002` | 8746 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Blue/DigiEgg/BT7_002_P2.asset` |
| `BT7-005#1384@base` | `BT7-005` | 1384 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/DigiEgg/BT7_005.asset` |
| `BT7-005#1385@P1` | `BT7-005` | 1385 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/Digimon/BT7_005_P1.asset` |
| `BT7-005#8750@P0` | `BT7-005` | 8750 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/DigiEgg/BT7_005_P0.asset` |
| `BT7-005#8751@P1` | `BT7-005` | 8751 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/DigiEgg/BT7_005_P1.asset` |
| `BT7-005#8752@P2` | `BT7-005` | 8752 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/DigiEgg/BT7_005_P2.asset` |
| `BT7-005#8753@P3` | `BT7-005` | 8753 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/DigiEgg/BT7_005_P3.asset` |
| `BT7-005#8754@P4` | `BT7-005` | 8754 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Black/DigiEgg/BT7_005_P4.asset` |
| `BT7-010#1391@base` | `BT7-010` | 1391 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_010.asset` |
| `BT7-013#1394@base` | `BT7-013` | 1394 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_013.asset` |
| `BT7-013#1395@P1` | `BT7-013` | 1395 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT7/Red/Digimon/BT7_013_P1.asset` |
| `RB1-001#2862@base` | `RB1-001` | 2862 | `base` | `None` | `DCGO/Assets/CardBaseEntity/RB1/Red/DigiEgg/RB1_001.asset` |

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
