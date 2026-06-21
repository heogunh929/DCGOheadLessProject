# C0099_zone_security_recovery - zone/security/recovery card porting 93

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0099_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 10
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST23_06` | `DCGO/Assets/Scripts/CardEffect/ST23/Green/ST23_06.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone, OnMove` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |
| `ST23_07` | `DCGO/Assets/Scripts/CardEffect/ST23/Green/ST23_07.cs` | `None, OnDetermineDoSecurityCheck` | `inherited, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 1 |
| `ST23_09` | `DCGO/Assets/Scripts/CardEffect/ST23/Green/ST23_09.cs` | `None, OptionSkill` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `ST23_10` | `DCGO/Assets/Scripts/CardEffect/ST23/Black/ST23_10.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `ST23_11` | `DCGO/Assets/Scripts/CardEffect/ST23/Black/ST23_11.cs` | `BeforePayCost, None` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `ST23_12` | `DCGO/Assets/Scripts/CardEffect/ST23/Purple/ST23_12.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `ST23_13` | `DCGO/Assets/Scripts/CardEffect/ST23/Green/ST23_13.cs` | `OnDigivolutionCardDiscarded, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity` | 1 |
| `ST23_14` | `DCGO/Assets/Scripts/CardEffect/ST23/Black/ST23_14.cs` | `OnDigivolutionCardDiscarded, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectInteger, SelectSecurity` | 1 |
| `ST23_15` | `DCGO/Assets/Scripts/CardEffect/ST23/White/ST23_15.cs` | `None, OnStartMainPhase, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectSecurity` | 1 |
| `ST24_09` | `DCGO/Assets/Scripts/CardEffect/ST24/Green/ST24_09.cs` | `None` | `inherited, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST23-06#7943@base` | `ST23-06` | 7943 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Green/Digimon/ST23_06.asset` |
| `ST23-07#7945@base` | `ST23-07` | 7945 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Green/Digimon/ST23_07.asset` |
| `ST23-09#7949@base` | `ST23-09` | 7949 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Green/Digimon/ST23_09.asset` |
| `ST23-10#7951@base` | `ST23-10` | 7951 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Black/Digimon/ST23_10.asset` |
| `ST23-11#7953@base` | `ST23-11` | 7953 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Black/Digimon/ST23_11.asset` |
| `ST23-12#7955@base` | `ST23-12` | 7955 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Purple/Digimon/ST23_12.asset` |
| `ST23-13#7957@base` | `ST23-13` | 7957 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Green/Tamer/ST23_13.asset` |
| `ST23-14#7959@base` | `ST23-14` | 7959 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/Purple/Tamer/ST23_14.asset` |
| `ST23-15#7961@base` | `ST23-15` | 7961 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST23/White/Option/ST23_15.asset` |
| `ST24-09#7918@base` | `ST24-09` | 7918 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST24/Green/Digimon/ST24_09.asset` |

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
