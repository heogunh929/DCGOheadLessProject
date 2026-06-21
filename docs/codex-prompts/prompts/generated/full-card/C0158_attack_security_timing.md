# C0158_attack_security_timing - attack/security timing card porting 31

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0158_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 31
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT8_024` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_024.cs` | `BeforePayCost, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 2 |
| `BT8_031` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_031.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT8_036` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_036.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent` | 1 |
| `BT8_044` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_044.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectSecurity` | 2 |
| `BT8_067` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_067.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget` | 4 |
| `BT8_085` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_085.cs` | `OnAllyAttack, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 5 |
| `BT8_087` | `DCGO/Assets/Scripts/CardEffect/BT8/Blue/BT8_087.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 4 |
| `BT8_089` | `DCGO/Assets/Scripts/CardEffect/BT8/Yellow/BT8_089.cs` | `OnAllyAttack, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 4 |
| `BT8_092` | `DCGO/Assets/Scripts/CardEffect/BT8/Black/BT8_092.cs` | `OnAllyAttack, OnMove, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity` | 5 |
| `BT8_111` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_111.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT8-024#1586@base` | `BT8-024` | 1586 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_024.asset` |
| `BT8-024#8859@P1` | `BT8-024` | 8859 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_024_P1.asset` |
| `BT8-031#1594@base` | `BT8-031` | 1594 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_031.asset` |
| `BT8-031#8861@P0` | `BT8-031` | 8861 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Digimon/BT8_031_P0.asset` |
| `BT8-036#1602@base` | `BT8-036` | 1602 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_036.asset` |
| `BT8-044#1615@base` | `BT8-044` | 1615 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_044.asset` |
| `BT8-044#8870@P0` | `BT8-044` | 8870 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Digimon/BT8_044_P0.asset` |
| `BT8-067#1646@base` | `BT8-067` | 1646 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_067.asset` |
| `BT8-067#1647@P1` | `BT8-067` | 1647 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_067_P1.asset` |
| `BT8-067#8883@P0` | `BT8-067` | 8883 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_067_P0.asset` |
| `BT8-067#8884@P2` | `BT8-067` | 8884 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Digimon/BT8_067_P2.asset` |
| `BT8-085#1671@base` | `BT8-085` | 1671 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Tamer/BT8_085.asset` |
| `BT8-085#1672@P1` | `BT8-085` | 1672 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Tamer/BT8_085_P1.asset` |
| `BT8-085#8898@P0` | `BT8-085` | 8898 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Tamer/BT8_085_P0.asset` |
| `BT8-085#8899@P2` | `BT8-085` | 8899 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Tamer/BT8_085_P2.asset` |
| `BT8-085#8900@P3` | `BT8-085` | 8900 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Tamer/BT8_085_P3.asset` |
| `BT8-087#1675@base` | `BT8-087` | 1675 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Tamer/BT8_087.asset` |
| `BT8-087#1676@P1` | `BT8-087` | 1676 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Tamer/BT8_087_P1.asset` |
| `BT8-087#8903@P0` | `BT8-087` | 8903 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Tamer/BT8_087_P0.asset` |
| `BT8-087#8904@P2` | `BT8-087` | 8904 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Blue/Tamer/BT8_087_P2.asset` |
| `BT8-089#1679@base` | `BT8-089` | 1679 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Tamer/BT8_089.asset` |
| `BT8-089#1680@P1` | `BT8-089` | 1680 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Tamer/BT8_089_P1.asset` |
| `BT8-089#8908@P0` | `BT8-089` | 8908 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Tamer/BT8_089_P0.asset` |
| `BT8-089#8909@P2` | `BT8-089` | 8909 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Yellow/Tamer/BT8_089_P2.asset` |
| `BT8-092#1685@base` | `BT8-092` | 1685 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Tamer/BT8_092.asset` |
| `BT8-092#8914@P0` | `BT8-092` | 8914 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Tamer/BT8_092_P0.asset` |
| `BT8-092#8915@P1` | `BT8-092` | 8915 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Tamer/BT8_092_P1.asset` |
| `BT8-092#8916@P2` | `BT8-092` | 8916 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Tamer/BT8_092_P2.asset` |
| `BT8-092#8917@P3` | `BT8-092` | 8917 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Black/Tamer/BT8_092_P3.asset` |
| `BT8-111#1704@base` | `BT8-111` | 1704 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_111.asset` |
| `BT8-111#1705@P1` | `BT8-111` | 1705 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_111_P1.asset` |

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
