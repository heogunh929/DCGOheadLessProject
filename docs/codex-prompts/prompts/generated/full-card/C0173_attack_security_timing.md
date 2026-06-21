# C0173_attack_security_timing - attack/security timing card porting 46

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0173_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 27
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_074` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_074.cs` | `BeforePayCost, None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectCount, SelectSecurity` | 2 |
| `P_076` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_076.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `P_077` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_077.cs` | `OnAllyAttack, OnDiscardLibrary` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand` | 5 |
| `P_089` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_089.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `P_091` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_091.cs` | `OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `P_097` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_097.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectPermanent` | 4 |
| `P_101` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_101.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 4 |
| `P_111` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_111.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `P_117` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_117.cs` | `BeforePayCost, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard` | 5 |
| `P_134` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_134.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-074#10392@P1` | `P-074` | 10392 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_074_P1.asset` |
| `P-074#6116@base` | `P-074` | 6116 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_074.asset` |
| `P-076#10380@P1` | `P-076` | 10380 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_076_P1.asset` |
| `P-076#6118@base` | `P-076` | 6118 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_076.asset` |
| `P-077#10387@P0` | `P-077` | 10387 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_077_P0.asset` |
| `P-077#10388@P2` | `P-077` | 10388 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_077_P2.asset` |
| `P-077#10389@P3` | `P-077` | 10389 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_077_P3.asset` |
| `P-077#6119@base` | `P-077` | 6119 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_077.asset` |
| `P-077#6120@P1` | `P-077` | 6120 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_077_P1.asset` |
| `P-089#6135@base` | `P-089` | 6135 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_089.asset` |
| `P-091#6137@base` | `P-091` | 6137 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_091.asset` |
| `P-097#10398@P1` | `P-097` | 10398 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_097_P1.asset` |
| `P-097#10399@P2` | `P-097` | 10399 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_097_P2.asset` |
| `P-097#10400@P3` | `P-097` | 10400 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_097_P3.asset` |
| `P-097#6143@base` | `P-097` | 6143 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_097.asset` |
| `P-101#10401@P1` | `P-101` | 10401 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_101_P1.asset` |
| `P-101#10402@P2` | `P-101` | 10402 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_101_P2.asset` |
| `P-101#10403@P3` | `P-101` | 10403 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_101_P3.asset` |
| `P-101#6147@base` | `P-101` | 6147 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_101.asset` |
| `P-111#6157@base` | `P-111` | 6157 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_111.asset` |
| `P-117#10407@P2` | `P-117` | 10407 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_117_P2.asset` |
| `P-117#10408@P3` | `P-117` | 10408 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_117_P3.asset` |
| `P-117#10409@P4` | `P-117` | 10409 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_117_P4.asset` |
| `P-117#6163@base` | `P-117` | 6163 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_117.asset` |
| `P-117#9242@P5` | `P-117` | 9242 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_117_P5.asset` |
| `P-134#10288@base` | `P-134` | 10288 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_134.asset` |
| `P-134#9253@P2` | `P-134` | 9253 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_134_P2.asset` |

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
