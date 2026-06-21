# C0027_zone_security_recovery - zone/security/recovery card porting 21

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0027_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 12
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT18_087` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_087.cs` | `OnLoseSecurity, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT18_090` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_090.cs` | `OnEndBattle, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT18_093` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_093.cs` | `OnStartMainPhase, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 2 |
| `BT18_098` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_098.cs` | `None, OnDiscardSecurity, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT19_006` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_006.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `BT19_015` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_015.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT19_016` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_016.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT19_021` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_021.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT19_022` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_022.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT19_026` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_026.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT18-087#3952@base` | `BT18-087` | 3952 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Red/Tamer/BT18_087.asset` |
| `BT18-090#3959@base` | `BT18-090` | 3959 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Tamer/BT18_090.asset` |
| `BT18-090#3960@P1` | `BT18-090` | 3960 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Tamer/BT18_090_P1.asset` |
| `BT18-093#3964@base` | `BT18-093` | 3964 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Tamer/BT18_093.asset` |
| `BT18-093#3965@P1` | `BT18-093` | 3965 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Tamer/BT18_093_P1.asset` |
| `BT18-098#3969@base` | `BT18-098` | 3969 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Option/BT18_098.asset` |
| `BT19-006#3979@base` | `BT19-006` | 3979 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Purple/DigiEgg/BT19_006.asset` |
| `BT19-015#5018@base` | `BT19-015` | 5018 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_015.asset` |
| `BT19-016#5019@base` | `BT19-016` | 5019 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_016.asset` |
| `BT19-021#3988@base` | `BT19-021` | 3988 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_021.asset` |
| `BT19-022#5021@base` | `BT19-022` | 5021 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_022.asset` |
| `BT19-026#5023@base` | `BT19-026` | 5023 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_026.asset` |

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
