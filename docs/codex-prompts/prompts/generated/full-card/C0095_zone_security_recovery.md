# C0095_zone_security_recovery - zone/security/recovery card porting 89

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0095_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 22
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `ST14_11` | `DCGO/Assets/Scripts/CardEffect/ST14/Purple/ST14_11.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 2 |
| `ST15_04` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_04.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `ST15_09` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_09.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 2 |
| `ST15_10` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_10.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `ST15_13` | `DCGO/Assets/Scripts/CardEffect/ST15/Black/ST15_13.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |
| `ST16_02` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_02.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand` | 2 |
| `ST16_09` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_09.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `ST16_13` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_13.cs` | `OnDiscardHand, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 2 |
| `ST16_14` | `DCGO/Assets/Scripts/CardEffect/ST16/Purple/ST16_14.cs` | `OnDiscardHand, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `-` | 3 |
| `ST18_04` | `DCGO/Assets/Scripts/CardEffect/ST18/Green/ST18_04.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `ST14-11#2828@base` | `ST14-11` | 2828 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Tamer/ST14_11.asset` |
| `ST14-11#4924@P0` | `ST14-11` | 4924 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST14/Purple/Tamer/ST14_11_P0.asset` |
| `ST15-04#2833@base` | `ST15-04` | 2833 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_04.asset` |
| `ST15-04#4930@P0` | `ST15-04` | 4930 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_04_P0.asset` |
| `ST15-09#2838@base` | `ST15-09` | 2838 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_09.asset` |
| `ST15-09#4934@P0` | `ST15-09` | 4934 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_09_P0.asset` |
| `ST15-10#2839@base` | `ST15-10` | 2839 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_10.asset` |
| `ST15-10#4935@P0` | `ST15-10` | 4935 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_10_P0.asset` |
| `ST15-13#2842@base` | `ST15-13` | 2842 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_13.asset` |
| `ST15-13#4939@P0` | `ST15-13` | 4939 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST15/Black/Digimon/ST15_13_P0.asset` |
| `ST16-02#2847@base` | `ST16-02` | 2847 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_02.asset` |
| `ST16-02#4944@P0` | `ST16-02` | 4944 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_02_P0.asset` |
| `ST16-09#2854@base` | `ST16-09` | 2854 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_09.asset` |
| `ST16-09#4951@P0` | `ST16-09` | 4951 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_09_P0.asset` |
| `ST16-13#2858@base` | `ST16-13` | 2858 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_13.asset` |
| `ST16-13#4956@P0` | `ST16-13` | 4956 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_13_P0.asset` |
| `ST16-14#2859@base` | `ST16-14` | 2859 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Tamer/ST16_14.asset` |
| `ST16-14#4957@P0` | `ST16-14` | 4957 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Tamer/ST16_14_P0.asset` |
| `ST16-14#9034@P1` | `ST16-14` | 9034 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Tamer/ST16_14_P1.asset` |
| `ST18-04#3821@base` | `ST18-04` | 3821 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_04.asset` |
| `ST18-04#9040@P1` | `ST18-04` | 9040 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_04_P1.asset` |
| `ST18-04#9041@P2` | `ST18-04` | 9041 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/ST18/Green/Digimon/ST18_04_P2.asset` |

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
