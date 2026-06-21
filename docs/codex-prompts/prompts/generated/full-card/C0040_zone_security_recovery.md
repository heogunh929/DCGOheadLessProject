# C0040_zone_security_recovery - zone/security/recovery card porting 34

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0040_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT23_025` | `DCGO/Assets/Scripts/CardEffect/BT23/Blue/BT23_025.cs` | `None, OnDeclaration, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT23_038` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_038.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT23_042` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_042.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 1 |
| `BT23_049` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_049.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `BT23_061` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_061.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `BT23_064` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_064.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT23_068` | `DCGO/Assets/Scripts/CardEffect/BT23/Purple/BT23_068.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone, OnStartMainPhase` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT23_081` | `DCGO/Assets/Scripts/CardEffect/BT23/Yellow/BT23_081.cs` | `OnEnterFieldAnyone, OnStartMainPhase, OnTappedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 3 |
| `BT23_083` | `DCGO/Assets/Scripts/CardEffect/BT23/Green/BT23_083.cs` | `OnAddSecurity, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |
| `BT23_085` | `DCGO/Assets/Scripts/CardEffect/BT23/Black/BT23_085.cs` | `OnEnterFieldAnyone, OnStartMainPhase, OnTappedAnyone, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT23-025#7357@base` | `BT23-025` | 7357 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Blue/Digimon/BT23_025.asset` |
| `BT23-038#7370@base` | `BT23-038` | 7370 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_038.asset` |
| `BT23-042#7374@base` | `BT23-042` | 7374 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Digimon/BT23_042.asset` |
| `BT23-049#7383@base` | `BT23-049` | 7383 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Digimon/BT23_049.asset` |
| `BT23-061#7395@base` | `BT23-061` | 7395 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_061.asset` |
| `BT23-061#7396@P1` | `BT23-061` | 7396 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_061_P1.asset` |
| `BT23-064#7399@base` | `BT23-064` | 7399 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_064.asset` |
| `BT23-068#7403@base` | `BT23-068` | 7403 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_068.asset` |
| `BT23-068#7404@P1` | `BT23-068` | 7404 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Purple/Digimon/BT23_068_P1.asset` |
| `BT23-081#7421@base` | `BT23-081` | 7421 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Tamer/BT23_081.asset` |
| `BT23-081#7422@P1` | `BT23-081` | 7422 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Tamer/BT23_081_P1.asset` |
| `BT23-081#7423@P2` | `BT23-081` | 7423 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Yellow/Tamer/BT23_081_P2.asset` |
| `BT23-083#7426@base` | `BT23-083` | 7426 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Tamer/BT23_083.asset` |
| `BT23-083#7427@P1` | `BT23-083` | 7427 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Green/Tamer/BT23_083_P1.asset` |
| `BT23-085#7431@base` | `BT23-085` | 7431 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Tamer/BT23_085.asset` |
| `BT23-085#7432@P1` | `BT23-085` | 7432 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Tamer/BT23_085_P1.asset` |
| `BT23-085#7433@P2` | `BT23-085` | 7433 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT23/Black/Tamer/BT23_085_P2.asset` |

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
