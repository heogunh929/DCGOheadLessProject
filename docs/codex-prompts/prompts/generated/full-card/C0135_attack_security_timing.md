# C0135_attack_security_timing - attack/security timing card porting 8

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0135_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT15_064` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_064.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `BT15_071` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_071.cs` | `None, OnAllyAttack, OnEndAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `BT15_075` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_075.cs` | `None, OnAllyAttack, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand` | 1 |
| `BT15_078` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_078.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectBoolean` | 2 |
| `BT15_085` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_085.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |
| `BT16_001` | `DCGO/Assets/Scripts/CardEffect/BT16/Red/BT16_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 3 |
| `BT16_023` | `DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_023.cs` | `None, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT16_034` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_034.cs` | `None, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT16_044` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_044.cs` | `None, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT16_045` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_045.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectBoolean, SelectAttackTarget` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT15-064#3196@base` | `BT15-064` | 3196 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_064.asset` |
| `BT15-064#3197@P1` | `BT15-064` | 3197 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_064_P1.asset` |
| `BT15-071#3204@base` | `BT15-071` | 3204 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_071.asset` |
| `BT15-071#4751@P1` | `BT15-071` | 4751 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_071_P1.asset` |
| `BT15-075#3208@base` | `BT15-075` | 3208 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_075.asset` |
| `BT15-078#3212@base` | `BT15-078` | 3212 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_078.asset` |
| `BT15-078#4753@P0` | `BT15-078` | 4753 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_078_P0.asset` |
| `BT15-085#3223@base` | `BT15-085` | 3223 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Tamer/BT15_085.asset` |
| `BT15-085#3224@P1` | `BT15-085` | 3224 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Tamer/BT15_085_P1.asset` |
| `BT15-085#4761@P0` | `BT15-085` | 4761 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Green/Tamer/BT15_085_P0.asset` |
| `BT16-001#3300@base` | `BT16-001` | 3300 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/DigiEgg/BT16_001.asset` |
| `BT16-001#3301@P1` | `BT16-001` | 3301 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/DigiEgg/BT16_001_P1.asset` |
| `BT16-001#4773@P0` | `BT16-001` | 4773 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Red/DigiEgg/BT16_001_P0.asset` |
| `BT16-023#3331@base` | `BT16-023` | 3331 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_023.asset` |
| `BT16-034#3347@base` | `BT16-034` | 3347 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_034.asset` |
| `BT16-044#3358@base` | `BT16-044` | 3358 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_044.asset` |
| `BT16-044#4797@P0` | `BT16-044` | 4797 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_044_P0.asset` |
| `BT16-045#3359@base` | `BT16-045` | 3359 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_045.asset` |
| `BT16-045#4798@P0` | `BT16-045` | 4798 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_045_P0.asset` |

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
