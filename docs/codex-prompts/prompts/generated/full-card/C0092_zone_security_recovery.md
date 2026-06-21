# C0092_zone_security_recovery - zone/security/recovery card porting 86

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0092_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_200` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_200.cs` | `BeforePayCost, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |
| `P_206` | `DCGO/Assets/Scripts/CardEffect/P/White/P_206.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity` | 3 |
| `P_210` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_210.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity` | 1 |
| `P_211` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_211.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `P_212` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_212.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `P_215` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_215.cs` | `None, OnEnterFieldAnyone, OnMove` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean` | 2 |
| `P_216` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_216.cs` | `None, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |
| `P_224` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_224.cs` | `None, OnDeclaration, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity` | 1 |
| `P_225` | `DCGO/Assets/Scripts/CardEffect/P/White/P_225.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `P_235` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_235.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-200#7471@base` | `P-200` | 7471 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Tamer/P_200.asset` |
| `P-206#7482@base` | `P-206` | 7482 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_206.asset` |
| `P-206#7483@P1` | `P-206` | 7483 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_206_P1.asset` |
| `P-206#7484@P2` | `P-206` | 7484 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_206_P2.asset` |
| `P-210#7488@base` | `P-210` | 7488 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Tamer/P_210.asset` |
| `P-211#7489@base` | `P-211` | 7489 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Tamer/P_211.asset` |
| `P-212#7490@base` | `P-212` | 7490 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Tamer/P_212.asset` |
| `P-215#7509@base` | `P-215` | 7509 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_215.asset` |
| `P-215#7510@P1` | `P-215` | 7510 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_215_P1.asset` |
| `P-216#7511@base` | `P-216` | 7511 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_216.asset` |
| `P-216#7512@P1` | `P-216` | 7512 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_216_P1.asset` |
| `P-224#7655@base` | `P-224` | 7655 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Tamer/P_224.asset` |
| `P-225#7656@base` | `P-225` | 7656 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Option/P_225.asset` |
| `P-235#7902@base` | `P-235` | 7902 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_235.asset` |
| `P-235#7903@P1` | `P-235` | 7903 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Option/P_235_P1.asset` |

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
