# C0032_zone_security_recovery - zone/security/recovery card porting 26

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0032_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT20_001` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_001.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT20_003` | `DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_003.cs` | `OnEndTurn` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |
| `BT20_004` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_004.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 2 |
| `BT20_006` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_006.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard` | 2 |
| `BT20_009` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_009.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |
| `BT20_034` | `DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_034.cs` | `None, OnAddDigivolutionCards, OnDestroyedAnyone, OnEndBattle` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT20_039` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_039.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT20_048` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_048.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT20_049` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_049.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT20_055` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_055.cs` | `OnEndTurn, OnEnterFieldAnyone, OnSecurityCheck` | `max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT20-001#5080@base` | `BT20-001` | 5080 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/DigiEgg/BT20_001.asset` |
| `BT20-001#5189@P1` | `BT20-001` | 5189 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/DigiEgg/BT20_001_P1.asset` |
| `BT20-003#5082@base` | `BT20-003` | 5082 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/DigiEgg/BT20_003.asset` |
| `BT20-003#5191@P1` | `BT20-003` | 5191 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/DigiEgg/BT20_003_P1.asset` |
| `BT20-003#8325@P2` | `BT20-003` | 8325 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/DigiEgg/BT20_003_P2.asset` |
| `BT20-004#5083@base` | `BT20-004` | 5083 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/DigiEgg/BT20_004.asset` |
| `BT20-004#5192@P1` | `BT20-004` | 5192 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/DigiEgg/BT20_004_P1.asset` |
| `BT20-006#5085@base` | `BT20-006` | 5085 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/DigiEgg/BT20_006.asset` |
| `BT20-006#5194@P1` | `BT20-006` | 5194 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/DigiEgg/BT20_006_P1.asset` |
| `BT20-009#5088@base` | `BT20-009` | 5088 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_009.asset` |
| `BT20-034#5113@base` | `BT20-034` | 5113 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_034.asset` |
| `BT20-039#5118@base` | `BT20-039` | 5118 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_039.asset` |
| `BT20-048#5127@base` | `BT20-048` | 5127 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_048.asset` |
| `BT20-048#8335@P1` | `BT20-048` | 8335 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_048_P1.asset` |
| `BT20-049#5128@base` | `BT20-049` | 5128 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_049.asset` |
| `BT20-055#5134@base` | `BT20-055` | 5134 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_055.asset` |
| `BT20-055#8339@P2` | `BT20-055` | 8339 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_055_P2.asset` |

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
