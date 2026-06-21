# C0031_zone_security_recovery - zone/security/recovery card porting 25

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0031_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 29
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT1_062` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_062.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 3 |
| `BT1_063` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_063.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity` | 2 |
| `BT1_067` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_067.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 3 |
| `BT1_070` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_070.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT1_074` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_074.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |
| `BT1_085` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_085.cs` | `None, OnStartTurn, SecuritySkill` | `inherited, modifier_duration, security, static_or_continuous, zone_movement` | `-` | 5 |
| `BT1_086` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_086.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT1_087` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_087.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 4 |
| `BT1_088` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_088.cs` | `OnDeclaration, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 3 |
| `BT1_089` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_089.cs` | `OnDeclaration, OnStartTurn, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `-` | 4 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-062#222@base` | `BT1-062` | 222 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_062.asset` |
| `BT1-062#223@P1` | `BT1-062` | 223 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_062_P1.asset` |
| `BT1-062#224@P2` | `BT1-062` | 224 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_062_P2.asset` |
| `BT1-063#225@base` | `BT1-063` | 225 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_063.asset` |
| `BT1-063#226@P1` | `BT1-063` | 226 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_063_P1.asset` |
| `BT1-067#234@base` | `BT1-067` | 234 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_067.asset` |
| `BT1-067#235@P1` | `BT1-067` | 235 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_067_P1.asset` |
| `BT1-067#4271@P2` | `BT1-067` | 4271 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_067_P2.asset` |
| `BT1-070#238@base` | `BT1-070` | 238 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_070.asset` |
| `BT1-074#244@base` | `BT1-074` | 244 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_074.asset` |
| `BT1-074#245@P1` | `BT1-074` | 245 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_074_P1.asset` |
| `BT1-085#269@base` | `BT1-085` | 269 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Tamer/BT1_085.asset` |
| `BT1-085#270@P1` | `BT1-085` | 270 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Tamer/BT1_085_P1.asset` |
| `BT1-085#271@P2` | `BT1-085` | 271 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Tamer/BT1_085_P2.asset` |
| `BT1-085#4274@P3` | `BT1-085` | 4274 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Tamer/BT1_085_P3.asset` |
| `BT1-085#8095@P4` | `BT1-085` | 8095 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Tamer/BT1_085_P4.asset` |
| `BT1-086#272@base` | `BT1-086` | 272 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Tamer/BT1_086.asset` |
| `BT1-086#273@P1` | `BT1-086` | 273 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Tamer/BT1_086_P1.asset` |
| `BT1-087#274@base` | `BT1-087` | 274 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Tamer/BT1_087.asset` |
| `BT1-087#275@P1` | `BT1-087` | 275 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Tamer/BT1_087_P1.asset` |
| `BT1-087#276@P2` | `BT1-087` | 276 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Tamer/BT1_087_P2.asset` |
| `BT1-087#4275@P3` | `BT1-087` | 4275 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Tamer/BT1_087_P3.asset` |
| `BT1-088#277@base` | `BT1-088` | 277 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_088.asset` |
| `BT1-088#278@P1` | `BT1-088` | 278 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_088_P1.asset` |
| `BT1-088#279@P2` | `BT1-088` | 279 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_088_P2.asset` |
| `BT1-089#280@base` | `BT1-089` | 280 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_089.asset` |
| `BT1-089#281@P1` | `BT1-089` | 281 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_089_P1.asset` |
| `BT1-089#282@P2` | `BT1-089` | 282 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_089_P2.asset` |
| `BT1-089#4276@P3` | `BT1-089` | 4276 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_089_P3.asset` |

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
