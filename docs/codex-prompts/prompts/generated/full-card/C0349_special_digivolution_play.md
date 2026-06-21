# C0349_special_digivolution_play - special digivolution/play mechanics card porting 114

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0349_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT9_111` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_111.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `EX10_001` | `DCGO/Assets/Scripts/CardEffect/EX10/Green/EX10_001.cs` | `OnLinkCardDiscarded` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `EX10_004` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_004.cs` | `OnMove` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectJogress` | 2 |
| `EX10_006` | `DCGO/Assets/Scripts/CardEffect/EX10/Red/EX10_006.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `EX10_007` | `DCGO/Assets/Scripts/CardEffect/EX10/Red/EX10_007.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `EX10_011` | `DCGO/Assets/Scripts/CardEffect/EX10/Red/EX10_011.cs` | `None, OnAllyAttack, OnDeclaration, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `EX10_012` | `DCGO/Assets/Scripts/CardEffect/EX10/Blue/EX10_012.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEndTurn, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `EX10_013` | `DCGO/Assets/Scripts/CardEffect/EX10/Yellow/EX10_013.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX10_014` | `DCGO/Assets/Scripts/CardEffect/EX10/Yellow/EX10_014.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone, SecuritySkill` | `linked, max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `EX10_015` | `DCGO/Assets/Scripts/CardEffect/EX10/Green/EX10_015.cs` | `None, OnDestroyedAnyone, OnDetermineDoSecurityCheck, OnStartMainPhase` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent, SelectSecurity, SelectDigiXros` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT9-111#1910@base` | `BT9-111` | 1910 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_111.asset` |
| `BT9-111#1911@P1` | `BT9-111` | 1911 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_111_P1.asset` |
| `EX10-001#7132@base` | `EX10-001` | 7132 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/DigiEgg/EX10_001.asset` |
| `EX10-001#7269@P1` | `EX10-001` | 7269 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/DigiEgg/EX10_001_P1.asset` |
| `EX10-004#7138@base` | `EX10-004` | 7138 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/DigiEgg/EX10_004.asset` |
| `EX10-004#7272@P1` | `EX10-004` | 7272 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/DigiEgg/EX10_004_P1.asset` |
| `EX10-006#7142@base` | `EX10-006` | 7142 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_006.asset` |
| `EX10-006#7274@P1` | `EX10-006` | 7274 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_006_P1.asset` |
| `EX10-007#7144@base` | `EX10-007` | 7144 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_007.asset` |
| `EX10-007#7275@P1` | `EX10-007` | 7275 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_007_P1.asset` |
| `EX10-011#7152@base` | `EX10-011` | 7152 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_011.asset` |
| `EX10-011#7279@P1` | `EX10-011` | 7279 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_011_P1.asset` |
| `EX10-012#7154@base` | `EX10-012` | 7154 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Blue/Digimon/EX10_012.asset` |
| `EX10-012#7280@P1` | `EX10-012` | 7280 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Blue/Digimon/EX10_012_P1.asset` |
| `EX10-013#7156@base` | `EX10-013` | 7156 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Yellow/Digimon/EX10_013.asset` |
| `EX10-014#7157@base` | `EX10-014` | 7157 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Yellow/Digimon/EX10_014.asset` |
| `EX10-014#7281@P1` | `EX10-014` | 7281 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Yellow/Digimon/EX10_014_P1.asset` |
| `EX10-015#7159@base` | `EX10-015` | 7159 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_015.asset` |
| `EX10-015#7282@P1` | `EX10-015` | 7282 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Green/Digimon/EX10_015_P1.asset` |

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
