# C0029_zone_security_recovery - zone/security/recovery card porting 23

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0029_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT19_077` | `DCGO/Assets/Scripts/CardEffect/BT19/White/BT19_077.cs` | `None, OnDeclaration, OnDestroyedAnyone, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 2 |
| `BT19_084` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_084.cs` | `OnDeclaration, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectSecurity` | 3 |
| `BT19_089` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_089.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT19_092` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_092.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectSecurity` | 1 |
| `BT19_096` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_096.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |
| `BT1_005` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_005.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectSecurity` | 1 |
| `BT1_010` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_010.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 8 |
| `BT1_017` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_017.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT1_018` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_018.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `-` | 1 |
| `BT1_023` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_023.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-005#139@base` | `BT1-005` | 139 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/DigiEgg/BT1_005.asset` |
| `BT1-010#145@base` | `BT1-010` | 145 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_010.asset` |
| `BT1-010#146@P1` | `BT1-010` | 146 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_010_P1.asset` |
| `BT1-010#147@P2` | `BT1-010` | 147 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_010_P2.asset` |
| `BT1-010#148@P3` | `BT1-010` | 148 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_010_P3.asset` |
| `BT1-010#4260@P4` | `BT1-010` | 4260 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_010_P4.asset` |
| `BT1-017#156@base` | `BT1-017` | 156 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_017.asset` |
| `BT1-018#157@base` | `BT1-018` | 157 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_018.asset` |
| `BT1-023#164@base` | `BT1-023` | 164 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_023.asset` |
| `BT1-023#165@P1` | `BT1-023` | 165 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_023_P1.asset` |
| `BT19-077#5056@base` | `BT19-077` | 5056 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/White/Digimon/BT19_077.asset` |
| `BT19-077#8291@P1` | `BT19-077` | 8291 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/White/Digimon/BT19_077_P1.asset` |
| `BT19-084#4021@base` | `BT19-084` | 4021 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Tamer/BT19_084.asset` |
| `BT19-084#4022@P1` | `BT19-084` | 4022 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Tamer/BT19_084_P1.asset` |
| `BT19-084#8296@P2` | `BT19-084` | 8296 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Tamer/BT19_084_P2.asset` |
| `BT19-089#5069@base` | `BT19-089` | 5069 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Red/Option/BT19_089.asset` |
| `BT19-092#4023@base` | `BT19-092` | 4023 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Option/BT19_092.asset` |
| `BT19-096#4024@base` | `BT19-096` | 4024 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT19/Green/Option/BT19_096.asset` |
| `P-042#10359@P1` | `P-042` | 10359 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_042_P1.asset` |
| `P-042#10360@P2` | `P-042` | 10360 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_042_P2.asset` |
| `P-042#6082@base` | `P-042` | 6082 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_042.asset` |

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
