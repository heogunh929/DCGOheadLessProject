# C0142_attack_security_timing - attack/security timing card porting 15

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0142_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 23
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT20_090` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_090.cs` | `OnEndTurn, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget` | 3 |
| `BT21_002` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_002.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT21_025` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_025.cs` | `None, OnAttackTargetChanged, OnLoseSecurity` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectAttackTarget` | 2 |
| `BT21_026` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_026.cs` | `None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `BT21_028` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_028.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 3 |
| `BT21_029` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_029.cs` | `None, OnDestroyedAnyone, OnEndAttack, OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 6 |
| `BT21_031` | `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_031.cs` | `None, OnEndAttack` | `inherited, max_count_per_turn, static_or_continuous` | `SelectCard` | 2 |
| `BT21_050` | `DCGO/Assets/Scripts/CardEffect/BT21/Green/BT21_050.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 1 |
| `BT21_072` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_072.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectAttackTarget` | 2 |
| `BT21_078` | `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_078.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT20-090#5169@base` | `BT20-090` | 5169 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Tamer/BT20_090.asset` |
| `BT20-090#5252@P1` | `BT20-090` | 5252 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Tamer/BT20_090_P1.asset` |
| `BT20-090#5253@P2` | `BT20-090` | 5253 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Tamer/BT20_090_P2.asset` |
| `BT21-002#5308@base` | `BT21-002` | 5308 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/DigiEgg/BT21_002.asset` |
| `BT21-002#8369@P1` | `BT21-002` | 8369 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/DigiEgg/BT21_002_P1.asset` |
| `BT21-025#5334@base` | `BT21-025` | 5334 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_025.asset` |
| `BT21-025#8383@P1` | `BT21-025` | 8383 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_025_P1.asset` |
| `BT21-026#5335@base` | `BT21-026` | 5335 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_026.asset` |
| `BT21-028#5337@base` | `BT21-028` | 5337 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_028.asset` |
| `BT21-028#8384@P1` | `BT21-028` | 8384 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_028_P1.asset` |
| `BT21-028#8385@P2` | `BT21-028` | 8385 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_028_P2.asset` |
| `BT21-029#5338@base` | `BT21-029` | 5338 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_029.asset` |
| `BT21-029#5339@P1` | `BT21-029` | 5339 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_029_P1.asset` |
| `BT21-029#5340@P2` | `BT21-029` | 5340 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_029_P2.asset` |
| `BT21-029#8386@P3` | `BT21-029` | 8386 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_029_P3.asset` |
| `BT21-029#8387@P4` | `BT21-029` | 8387 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_029_P4.asset` |
| `BT21-029#8388@P5` | `BT21-029` | 8388 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_029_P5.asset` |
| `BT21-031#5342@base` | `BT21-031` | 5342 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_031.asset` |
| `BT21-031#8389@P1` | `BT21-031` | 8389 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Blue/Digimon/BT21_031_P1.asset` |
| `BT21-050#5361@base` | `BT21-050` | 5361 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Green/Digimon/BT21_050.asset` |
| `BT21-072#5389@base` | `BT21-072` | 5389 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_072.asset` |
| `BT21-072#8409@P1` | `BT21-072` | 8409 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_072_P1.asset` |
| `BT21-078#5395@base` | `BT21-078` | 5395 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Purple/Digimon/BT21_078.asset` |

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
