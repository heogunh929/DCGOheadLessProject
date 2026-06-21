# C0141_attack_security_timing - attack/security timing card porting 14

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0141_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT1_082` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_082.cs` | `OnAllyAttack` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT1_114` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_114.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `BT1_115` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_115.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 3 |
| `BT20_002` | `DCGO/Assets/Scripts/CardEffect/BT20/Blue/BT20_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT20_017` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_017.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 2 |
| `BT20_033` | `DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_033.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT20_041` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_041.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 1 |
| `BT20_050` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_050.cs` | `None, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity` | 1 |
| `BT20_052` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_052.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OnSecurityCheck` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectAttackTarget` | 4 |
| `BT20_054` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_054.cs` | `None, OnAllyAttack, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-082#259@base` | `BT1-082` | 259 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_082.asset` |
| `BT1-082#260@P1` | `BT1-082` | 260 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_082_P1.asset` |
| `BT1-114#316@base` | `BT1-114` | 316 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_114.asset` |
| `BT1-114#317@P1` | `BT1-114` | 317 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_114_P1.asset` |
| `BT1-114#4278@P2` | `BT1-114` | 4278 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_114_P2.asset` |
| `BT1-115#318@base` | `BT1-115` | 318 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_115.asset` |
| `BT1-115#319@P1` | `BT1-115` | 319 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_115_P1.asset` |
| `BT1-115#4279@P2` | `BT1-115` | 4279 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_115_P2.asset` |
| `BT20-002#5081@base` | `BT20-002` | 5081 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/DigiEgg/BT20_002.asset` |
| `BT20-002#5190@P1` | `BT20-002` | 5190 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Blue/DigiEgg/BT20_002_P1.asset` |
| `BT20-017#5096@base` | `BT20-017` | 5096 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_017.asset` |
| `BT20-017#8329@P1` | `BT20-017` | 8329 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Red/Digimon/BT20_017_P1.asset` |
| `BT20-033#5112@base` | `BT20-033` | 5112 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Yellow/Digimon/BT20_033.asset` |
| `BT20-041#5120@base` | `BT20-041` | 5120 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Green/Digimon/BT20_041.asset` |
| `BT20-050#5129@base` | `BT20-050` | 5129 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_050.asset` |
| `BT20-052#5131@base` | `BT20-052` | 5131 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_052.asset` |
| `BT20-052#8336@P1` | `BT20-052` | 8336 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_052_P1.asset` |
| `BT20-052#8337@P2` | `BT20-052` | 8337 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_052_P2.asset` |
| `BT20-052#8338@P3` | `BT20-052` | 8338 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_052_P3.asset` |
| `BT20-054#5133@base` | `BT20-054` | 5133 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_054.asset` |

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
