# C0028_zone_security_recovery - zone/security/recovery card porting 22

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0028_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT19_027` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_027.cs` | `OnEndTurn, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `BT19_028` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_028.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT19_039` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_039.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT19_041` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_041.cs` | `OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT19_045` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_045.cs` | `BeforePayCost, None` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `BT19_046` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_046.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT19_052` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_052.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |
| `BT19_055` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_055.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT19_067` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_067.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT19_071` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_071.cs` | `OnDiscardLibrary, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT19-027#3985@base` | `BT19-027` | 3985 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_027.asset` |
| `BT19-027#3986@P1` | `BT19-027` | 3986 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_027_P1.asset` |
| `BT19-027#8278@P2` | `BT19-027` | 8278 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_027_P2.asset` |
| `BT19-028#3987@base` | `BT19-028` | 3987 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_028.asset` |
| `BT19-039#3991@base` | `BT19-039` | 3991 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_039.asset` |
| `BT19-041#3996@base` | `BT19-041` | 3996 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_041.asset` |
| `BT19-045#3995@base` | `BT19-045` | 3995 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_045.asset` |
| `BT19-046#3993@base` | `BT19-046` | 3993 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_046.asset` |
| `BT19-052#4006@base` | `BT19-052` | 4006 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_052.asset` |
| `BT19-052#8282@P1` | `BT19-052` | 8282 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_052_P1.asset` |
| `BT19-052#8283@P2` | `BT19-052` | 8283 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_052_P2.asset` |
| `BT19-055#4002@base` | `BT19-055` | 4002 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_055.asset` |
| `BT19-067#5049@base` | `BT19-067` | 5049 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_067.asset` |
| `BT19-067#8286@P1` | `BT19-067` | 8286 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_067_P1.asset` |
| `BT19-071#5051@base` | `BT19-071` | 5051 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_071.asset` |

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
