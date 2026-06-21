# C0080_zone_security_recovery - zone/security/recovery card porting 74

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0080_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX6_046` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_046.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand` | 1 |
| `EX6_047` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_047.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 1 |
| `EX6_049` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_049.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `EX6_055` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_055.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `EX6_063` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_063.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX6_064` | `DCGO/Assets/Scripts/CardEffect/EX6/Yellow/EX6_064.cs` | `OnEnterFieldAnyone, OnTappedAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `EX6_071` | `DCGO/Assets/Scripts/CardEffect/EX6/Purple/EX6_071.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity` | 1 |
| `EX7_008` | `DCGO/Assets/Scripts/CardEffect/EX7/Red/EX7_008.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `EX7_011` | `DCGO/Assets/Scripts/CardEffect/EX7/Red/EX7_011.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity` | 2 |
| `EX7_015` | `DCGO/Assets/Scripts/CardEffect/EX7/Blue/EX7_015.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 6 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX6-046#3499@base` | `EX6-046` | 3499 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_046.asset` |
| `EX6-047#3500@base` | `EX6-047` | 3500 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_047.asset` |
| `EX6-049#3502@base` | `EX6-049` | 3502 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_049.asset` |
| `EX6-055#3509@base` | `EX6-055` | 3509 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Digimon/EX6_055.asset` |
| `EX6-063#3527@base` | `EX6-063` | 3527 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Tamer/EX6_063.asset` |
| `EX6-063#3528@P1` | `EX6-063` | 3528 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Tamer/EX6_063_P1.asset` |
| `EX6-064#3529@base` | `EX6-064` | 3529 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Tamer/EX6_064.asset` |
| `EX6-064#3530@P1` | `EX6-064` | 3530 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Yellow/Tamer/EX6_064_P1.asset` |
| `EX6-071#3537@base` | `EX6-071` | 3537 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX6/Purple/Option/EX6_071.asset` |
| `EX7-008#3690@base` | `EX7-008` | 3690 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_008.asset` |
| `EX7-008#3691@P1` | `EX7-008` | 3691 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_008_P1.asset` |
| `EX7-011#3695@base` | `EX7-011` | 3695 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_011.asset` |
| `EX7-011#3696@P1` | `EX7-011` | 3696 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Red/Digimon/EX7_011_P1.asset` |
| `EX7-015#3703@base` | `EX7-015` | 3703 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_015.asset` |
| `EX7-015#3704@P1` | `EX7-015` | 3704 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_015_P1.asset` |
| `EX7-015#9166@P2` | `EX7-015` | 9166 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_015_P2.asset` |
| `EX7-015#9167@P3` | `EX7-015` | 9167 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_015_P3.asset` |
| `EX7-015#9168@P4` | `EX7-015` | 9168 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_015_P4.asset` |
| `EX7-015#9169@P5` | `EX7-015` | 9169 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/EX7/Blue/Digimon/EX7_015_P5.asset` |

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
