# C0084_zone_security_recovery - zone/security/recovery card porting 78

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0084_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX8_041` | `DCGO/Assets/Scripts/CardEffect/EX8/Green/EX8_041.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `EX8_042` | `DCGO/Assets/Scripts/CardEffect/EX8/Green/EX8_042.cs` | `None, OnDestroyedAnyone, OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |
| `EX8_043` | `DCGO/Assets/Scripts/CardEffect/EX8/Green/EX8_043.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `EX8_046` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_046.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectHand` | 2 |
| `EX8_047` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_047.cs` | `OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX8_049` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_049.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `EX8_053` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_053.cs` | `None, OnDestroyedAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `EX8_066` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_066.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectSecurity` | 3 |
| `EX8_067` | `DCGO/Assets/Scripts/CardEffect/EX8/Black/EX8_067.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity` | 3 |
| `EX8_068` | `DCGO/Assets/Scripts/CardEffect/EX8/Blue/EX8_068.cs` | `None, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX8-041#4125@base` | `EX8-041` | 4125 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_041.asset` |
| `EX8-041#4126@P1` | `EX8-041` | 4126 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_041_P1.asset` |
| `EX8-042#4127@base` | `EX8-042` | 4127 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_042.asset` |
| `EX8-042#4128@P1` | `EX8-042` | 4128 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_042_P1.asset` |
| `EX8-043#4129@base` | `EX8-043` | 4129 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_043.asset` |
| `EX8-043#4130@P1` | `EX8-043` | 4130 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Green/Digimon/EX8_043_P1.asset` |
| `EX8-046#4138@base` | `EX8-046` | 4138 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_046.asset` |
| `EX8-046#4139@P1` | `EX8-046` | 4139 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_046_P1.asset` |
| `EX8-047#4140@base` | `EX8-047` | 4140 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_047.asset` |
| `EX8-047#4141@P1` | `EX8-047` | 4141 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_047_P1.asset` |
| `EX8-049#4144@base` | `EX8-049` | 4144 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_049.asset` |
| `EX8-049#4145@P1` | `EX8-049` | 4145 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_049_P1.asset` |
| `EX8-053#4151@base` | `EX8-053` | 4151 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Digimon/EX8_053.asset` |
| `EX8-066#4177@base` | `EX8-066` | 4177 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Tamer/EX8_066.asset` |
| `EX8-066#4178@P1` | `EX8-066` | 4178 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Tamer/EX8_066_P1.asset` |
| `EX8-066#9201@P2` | `EX8-066` | 9201 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Tamer/EX8_066_P2.asset` |
| `EX8-067#4179@base` | `EX8-067` | 4179 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Tamer/EX8_067.asset` |
| `EX8-067#4180@P1` | `EX8-067` | 4180 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Tamer/EX8_067_P1.asset` |
| `EX8-067#9202@P2` | `EX8-067` | 9202 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Black/Tamer/EX8_067_P2.asset` |
| `EX8-068#4181@base` | `EX8-068` | 4181 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Option/EX8_068.asset` |
| `EX8-068#4182@P1` | `EX8-068` | 4182 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX8/Blue/Option/EX8_068_P1.asset` |

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
