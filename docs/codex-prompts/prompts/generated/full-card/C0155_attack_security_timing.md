# C0155_attack_security_timing - attack/security timing card porting 28

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0155_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT6_018` | `DCGO/Assets/Scripts/CardEffect/BT6/Red/BT6_018.cs` | `OnAllyAttack, OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |
| `BT6_022` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_022.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT6_024` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_024.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT6_025` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_025.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT6_027` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_027.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT6_030` | `DCGO/Assets/Scripts/CardEffect/BT6/Blue/BT6_030.cs` | `OnAllyAttack` | `max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT6_041` | `DCGO/Assets/Scripts/CardEffect/BT6/Yellow/BT6_041.cs` | `OnAllyAttack` | `max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT6_045` | `DCGO/Assets/Scripts/CardEffect/BT6/Green/BT6_045.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT6_051` | `DCGO/Assets/Scripts/CardEffect/BT6/Green/BT6_051.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT6_054` | `DCGO/Assets/Scripts/CardEffect/BT6/Green/BT6_054.cs` | `OnAllyAttack, OnDestroyedAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT6-018#1131@base` | `BT6-018` | 1131 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_018.asset` |
| `BT6-018#1132@P1` | `BT6-018` | 1132 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_018_P1.asset` |
| `BT6-018#8682@P2` | `BT6-018` | 8682 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Red/Digimon/BT6_018_P2.asset` |
| `BT6-022#1138@base` | `BT6-022` | 1138 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_022.asset` |
| `BT6-024#1140@base` | `BT6-024` | 1140 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_024.asset` |
| `BT6-024#8685@P0` | `BT6-024` | 8685 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_024_P0.asset` |
| `BT6-025#1141@base` | `BT6-025` | 1141 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_025.asset` |
| `BT6-027#1143@base` | `BT6-027` | 1143 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_027.asset` |
| `BT6-027#8686@P0` | `BT6-027` | 8686 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_027_P0.asset` |
| `BT6-030#1146@base` | `BT6-030` | 1146 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_030.asset` |
| `BT6-030#1147@P1` | `BT6-030` | 1147 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Blue/Digimon/BT6_030_P1.asset` |
| `BT6-041#1164@base` | `BT6-041` | 1164 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_041.asset` |
| `BT6-041#8691@P0` | `BT6-041` | 8691 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Yellow/Digimon/BT6_041_P0.asset` |
| `BT6-045#1169@base` | `BT6-045` | 1169 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_045.asset` |
| `BT6-051#1179@base` | `BT6-051` | 1179 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_051.asset` |
| `BT6-051#8695@P0` | `BT6-051` | 8695 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_051_P0.asset` |
| `BT6-054#1182@base` | `BT6-054` | 1182 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_054.asset` |
| `BT6-054#8697@P0` | `BT6-054` | 8697 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT6/Green/Digimon/BT6_054_P0.asset` |

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
