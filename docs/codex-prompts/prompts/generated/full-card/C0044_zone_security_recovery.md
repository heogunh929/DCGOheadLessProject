# C0044_zone_security_recovery - zone/security/recovery card porting 38

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0044_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT25_012` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_012.cs` | `None` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT25_019` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_019.cs` | `None, OnEndTurn` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT25_020` | `DCGO/Assets/Scripts/CardEffect/BT25/Red/BT25_020.cs` | `None, OnEndBattle` | `max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT25_022` | `DCGO/Assets/Scripts/CardEffect/BT25/Blue/BT25_022.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT25_040` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_040.cs` | `None, OnDestroyedAnyone, OnDiscardSecurity, OnLoseSecurity` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectInteger, SelectSecurity` | 1 |
| `BT25_042` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_042.cs` | `None, OnLoseSecurity` | `max_count_per_turn, modifier_duration, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectInteger, SelectSecurity` | 1 |
| `BT25_044` | `DCGO/Assets/Scripts/CardEffect/BT25/Yellow/BT25_044.cs` | `None, OnLoseSecurity` | `max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectSecurity` | 2 |
| `BT25_046` | `DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_046.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `BT25_047` | `DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_047.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT25_048` | `DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_048.cs` | `None, OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT25-012#7974@base` | `BT25-012` | 7974 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_012.asset` |
| `BT25-019#7982@base` | `BT25-019` | 7982 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_019.asset` |
| `BT25-020#7983@base` | `BT25-020` | 7983 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_020.asset` |
| `BT25-020#7984@P1` | `BT25-020` | 7984 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Red/Digimon/BT25_020_P1.asset` |
| `BT25-022#7986@base` | `BT25-022` | 7986 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Blue/Digimon/BT25_022.asset` |
| `BT25-040#8006@base` | `BT25-040` | 8006 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_040.asset` |
| `BT25-042#8008@base` | `BT25-042` | 8008 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_042.asset` |
| `BT25-044#8012@base` | `BT25-044` | 8012 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_044.asset` |
| `BT25-044#8013@P1` | `BT25-044` | 8013 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Yellow/Digimon/BT25_044_P1.asset` |
| `BT25-046#8015@base` | `BT25-046` | 8015 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_046.asset` |
| `BT25-047#8016@base` | `BT25-047` | 8016 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_047.asset` |
| `BT25-048#8017@base` | `BT25-048` | 8017 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_048.asset` |

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
