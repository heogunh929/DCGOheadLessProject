# C0038_zone_security_recovery - zone/security/recovery card porting 32

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0038_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 19
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT22_048` | `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_048.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT22_051` | `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_051.cs` | `None, OnDestroyedAnyone, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT22_054` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_054.cs` | `None, OnAddDigivolutionCards, OnDeclaration` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT22_056` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_056.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT22_065` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_065.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT22_069` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_069.cs` | `None, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT22_077` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_077.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `BT22_079` | `DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_079.cs` | `BeforePayCost, None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT22_092` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_092.cs` | `None, OnDeclaration, OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 2 |
| `BT22_093` | `DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_093.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectSecurity` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT22-048#7049@base` | `BT22-048` | 7049 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_048.asset` |
| `BT22-048#8433@P1` | `BT22-048` | 8433 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_048_P1.asset` |
| `BT22-051#7053@base` | `BT22-051` | 7053 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Green/Digimon/BT22_051.asset` |
| `BT22-054#7057@base` | `BT22-054` | 7057 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_054.asset` |
| `BT22-054#7058@P1` | `BT22-054` | 7058 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_054_P1.asset` |
| `BT22-056#7060@base` | `BT22-056` | 7060 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_056.asset` |
| `BT22-056#8434@P1` | `BT22-056` | 8434 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_056_P1.asset` |
| `BT22-065#7072@base` | `BT22-065` | 7072 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_065.asset` |
| `BT22-069#7076@base` | `BT22-069` | 7076 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_069.asset` |
| `BT22-069#8439@P1` | `BT22-069` | 8439 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_069_P1.asset` |
| `BT22-077#7084@base` | `BT22-077` | 7084 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_077.asset` |
| `BT22-077#7085@P1` | `BT22-077` | 7085 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_077_P1.asset` |
| `BT22-079#7087@base` | `BT22-079` | 7087 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Digimon/BT22_079.asset` |
| `BT22-079#8443@P1` | `BT22-079` | 8443 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Digimon/BT22_079_P1.asset` |
| `BT22-092#7110@base` | `BT22-092` | 7110 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Tamer/BT22_092.asset` |
| `BT22-092#7111@P1` | `BT22-092` | 7111 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Tamer/BT22_092_P1.asset` |
| `BT22-093#7112@base` | `BT22-093` | 7112 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Tamer/BT22_093.asset` |
| `BT22-093#7113@P1` | `BT22-093` | 7113 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Tamer/BT22_093_P1.asset` |
| `BT22-093#7114@P2` | `BT22-093` | 7114 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Tamer/BT22_093_P2.asset` |

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
