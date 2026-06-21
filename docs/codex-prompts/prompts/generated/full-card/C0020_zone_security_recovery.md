# C0020_zone_security_recovery - zone/security/recovery card porting 14

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0020_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT15_028` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_028.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `BT15_030` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_030.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `BT15_036` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_036.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity` | 1 |
| `BT15_043` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_043.cs` | `OnEndBattle, OnStartMainPhase` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT15_044` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_044.cs` | `OnDestroyedAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `BT15_050` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_050.cs` | `OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 1 |
| `BT15_053` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_053.cs` | `None, OnEnterFieldAnyone, OnStartMainPhase` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 5 |
| `BT15_055` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_055.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT15_058` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_058.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT15_061` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_061.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT15-028#3153@base` | `BT15-028` | 3153 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_028.asset` |
| `BT15-030#3155@base` | `BT15-030` | 3155 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_030.asset` |
| `BT15-030#4725@P0` | `BT15-030` | 4725 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_030_P0.asset` |
| `BT15-036#3161@base` | `BT15-036` | 3161 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_036.asset` |
| `BT15-043#3171@base` | `BT15-043` | 3171 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_043.asset` |
| `BT15-043#3172@P1` | `BT15-043` | 3172 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_043_P1.asset` |
| `BT15-044#3173@base` | `BT15-044` | 3173 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_044.asset` |
| `BT15-050#3181@base` | `BT15-050` | 3181 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_050.asset` |
| `BT15-053#3184@base` | `BT15-053` | 3184 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_053.asset` |
| `BT15-053#4735@P0` | `BT15-053` | 4735 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_053_P0.asset` |
| `BT15-053#4736@P1` | `BT15-053` | 4736 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_053_P1.asset` |
| `BT15-053#4737@P2` | `BT15-053` | 4737 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_053_P2.asset` |
| `BT15-053#4738@P3` | `BT15-053` | 4738 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_053_P3.asset` |
| `BT15-055#3186@base` | `BT15-055` | 3186 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_055.asset` |
| `BT15-055#4740@P0` | `BT15-055` | 4740 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_055_P0.asset` |
| `BT15-058#3190@base` | `BT15-058` | 3190 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_058.asset` |
| `BT15-061#3193@base` | `BT15-061` | 3193 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_061.asset` |
| `BT15-061#4743@P0` | `BT15-061` | 4743 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_061_P0.asset` |

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
