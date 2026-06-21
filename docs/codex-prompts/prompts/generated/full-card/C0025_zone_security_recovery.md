# C0025_zone_security_recovery - zone/security/recovery card porting 19

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0025_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT18_008` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_008.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT18_009` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_009.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 2 |
| `BT18_010` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_010.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT18_021` | `DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_021.cs` | `BeforePayCost, None` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `BT18_031` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_031.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT18_033` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_033.cs` | `OnDeclaration` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `BT18_038` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_038.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectSecurity` | 1 |
| `BT18_043` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_043.cs` | `BeforePayCost, OnDetermineDoSecurityCheck` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `BT18_044` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_044.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectSecurity` | 1 |
| `BT18_046` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_046.cs` | `None` | `inherited, modifier_duration, static_or_continuous, zone_movement` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT18-008#3854@base` | `BT18-008` | 3854 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_008.asset` |
| `BT18-009#3855@base` | `BT18-009` | 3855 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_009.asset` |
| `BT18-009#8250@P1` | `BT18-009` | 8250 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_009_P1.asset` |
| `BT18-010#3859@base` | `BT18-010` | 3859 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_010.asset` |
| `BT18-021#3879@base` | `BT18-021` | 3879 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_021.asset` |
| `BT18-031#3888@base` | `BT18-031` | 3888 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_031.asset` |
| `BT18-033#3889@base` | `BT18-033` | 3889 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_033.asset` |
| `BT18-033#8256@P1` | `BT18-033` | 8256 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_033_P1.asset` |
| `BT18-038#3881@base` | `BT18-038` | 3881 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_038.asset` |
| `BT18-043#3897@base` | `BT18-043` | 3897 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_043.asset` |
| `BT18-044#3893@base` | `BT18-044` | 3893 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_044.asset` |
| `BT18-046#3898@base` | `BT18-046` | 3898 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_046.asset` |

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
