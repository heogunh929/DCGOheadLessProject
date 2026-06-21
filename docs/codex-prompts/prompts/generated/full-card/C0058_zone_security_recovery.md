# C0058_zone_security_recovery - zone/security/recovery card porting 52

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0058_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT6_043` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_043.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectSecurity` | 1 |
| `BT6_044` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_044.cs` | `OnEnterFieldAnyone, OnLoseSecurity` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity` | 2 |
| `BT6_049` | `DCGO/Assets/Scripts/CardEffect/BT6/Green/BT6_049.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 2 |
| `BT6_050` | `DCGO/Assets/Scripts/CardEffect/BT6/Green/BT6_050.cs` | `None, OnDetermineDoSecurityCheck` | `inherited, static_or_continuous, zone_movement` | `-` | 1 |
| `BT6_060` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_060.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT6_065` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_065.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT6_066` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_066.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT6_067` | `DCGO/Assets/Scripts/CardEffect/BT6/Black/BT6_067.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `BT6_068` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_068.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 5 |
| `BT6_070` | `DCGO/Assets/Scripts/CardEffect/BT6/Purple/BT6_070.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT6-043#1166@base` | `BT6-043` | 1166 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_043.asset` |
| `BT6-044#1167@base` | `BT6-044` | 1167 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_044.asset` |
| `BT6-044#1168@P1` | `BT6-044` | 1168 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_044_P1.asset` |
| `BT6-049#1177@base` | `BT6-049` | 1177 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_049.asset` |
| `BT6-049#6781@P0` | `BT6-049` | 6781 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_049_P0.asset` |
| `BT6-050#1178@base` | `BT6-050` | 1178 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_050.asset` |
| `BT6-060#1189@base` | `BT6-060` | 1189 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_060.asset` |
| `BT6-065#1196@base` | `BT6-065` | 1196 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_065.asset` |
| `BT6-065#1197@P1` | `BT6-065` | 1197 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_065_P1.asset` |
| `BT6-066#1198@base` | `BT6-066` | 1198 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_066.asset` |
| `BT6-066#8703@P0` | `BT6-066` | 8703 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_066_P0.asset` |
| `BT6-067#1199@base` | `BT6-067` | 1199 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_067.asset` |
| `BT6-067#1200@P1` | `BT6-067` | 1200 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Black/Digimon/BT6_067_P1.asset` |
| `BT6-068#1201@base` | `BT6-068` | 1201 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_068.asset` |
| `BT6-068#1202@P1` | `BT6-068` | 1202 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_068_P1.asset` |
| `BT6-068#8704@P0` | `BT6-068` | 8704 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_068_P0.asset` |
| `BT6-068#8705@P2` | `BT6-068` | 8705 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_068_P2.asset` |
| `BT6-068#8706@P3` | `BT6-068` | 8706 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_068_P3.asset` |
| `BT6-070#1204@base` | `BT6-070` | 1204 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_070.asset` |
| `BT6-070#8707@P1` | `BT6-070` | 8707 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Purple/Digimon/BT6_070_P1.asset` |

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
