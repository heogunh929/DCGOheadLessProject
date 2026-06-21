# C0035_zone_security_recovery - zone/security/recovery card porting 29

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0035_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT21_029_token` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_029_token.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity` | 0 |
| `BT21_033` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_033.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT21_048` | `DCGO/Assets/Scripts/CardEffect/BT21/Green/BT21_048.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT21_049` | `DCGO/Assets/Scripts/CardEffect/BT21/Green/BT21_049.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT21_055` | `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_055.cs` | `None, OnDigivolutionCardDiscarded` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT21_063` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_063.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 2 |
| `BT21_065` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_065.cs` | `BeforePayCost, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `BT21_080` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_080.cs` | `OnAddDigivolutionCards, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 4 |
| `BT21_082` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_082.cs` | `OnLoseSecurity, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `BT21_085` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_085.cs` | `OnDeclaration, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT21-033#5344@base` | `BT21-033` | 5344 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_033.asset` |
| `BT21-048#5359@base` | `BT21-048` | 5359 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Green/Digimon/BT21_048.asset` |
| `BT21-049#5360@base` | `BT21-049` | 5360 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Green/Digimon/BT21_049.asset` |
| `BT21-055#5369@base` | `BT21-055` | 5369 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_055.asset` |
| `BT21-055#8399@P1` | `BT21-055` | 8399 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/Digimon/BT21_055_P1.asset` |
| `BT21-063#5380@base` | `BT21-063` | 5380 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_063.asset` |
| `BT21-063#8405@P1` | `BT21-063` | 8405 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_063_P1.asset` |
| `BT21-065#5382@base` | `BT21-065` | 5382 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_065.asset` |
| `BT21-065#8406@P1` | `BT21-065` | 8406 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_065_P1.asset` |
| `BT21-080#5397@base` | `BT21-080` | 5397 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_080.asset` |
| `BT21-080#5398@P1` | `BT21-080` | 5398 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_080_P1.asset` |
| `BT21-080#8411@P2` | `BT21-080` | 8411 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_080_P2.asset` |
| `BT21-080#8412@P3` | `BT21-080` | 8412 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_080_P3.asset` |
| `BT21-082#5401@base` | `BT21-082` | 5401 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_082.asset` |
| `BT21-082#5402@P1` | `BT21-082` | 5402 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Tamer/BT21_082_P1.asset` |
| `BT21-085#5407@base` | `BT21-085` | 5407 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Tamer/BT21_085.asset` |
| `BT21-085#5408@P1` | `BT21-085` | 5408 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Tamer/BT21_085_P1.asset` |

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
