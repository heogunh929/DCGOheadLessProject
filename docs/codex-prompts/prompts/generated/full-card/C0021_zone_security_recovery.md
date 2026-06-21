# C0021_zone_security_recovery - zone/security/recovery card porting 15

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0021_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 18
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT15_062` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_062.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `BT15_063` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_063.cs` | `None, OnTappedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent` | 2 |
| `BT15_067` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_067.cs` | `None, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 5 |
| `BT15_068` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_068.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT15_069` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_069.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT15_070` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_070.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand` | 2 |
| `BT15_073` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_073.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand` | 1 |
| `BT15_074` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_074.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand` | 2 |
| `BT15_077` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_077.cs` | `OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent` | 1 |
| `BT15_080` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_080.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT15-062#3194@base` | `BT15-062` | 3194 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_062.asset` |
| `BT15-063#3195@base` | `BT15-063` | 3195 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_063.asset` |
| `BT15-063#4744@P0` | `BT15-063` | 4744 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_063_P0.asset` |
| `BT15-067#3200@base` | `BT15-067` | 3200 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_067.asset` |
| `BT15-067#4747@P0` | `BT15-067` | 4747 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_067_P0.asset` |
| `BT15-067#4748@P1` | `BT15-067` | 4748 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_067_P1.asset` |
| `BT15-067#4749@P2` | `BT15-067` | 4749 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_067_P2.asset` |
| `BT15-067#4750@P3` | `BT15-067` | 4750 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_067_P3.asset` |
| `BT15-068#3201@base` | `BT15-068` | 3201 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_068.asset` |
| `BT15-069#3202@base` | `BT15-069` | 3202 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_069.asset` |
| `BT15-070#3203@base` | `BT15-070` | 3203 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_070.asset` |
| `BT15-070#8186@P1` | `BT15-070` | 8186 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_070_P1.asset` |
| `BT15-073#3206@base` | `BT15-073` | 3206 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_073.asset` |
| `BT15-074#3207@base` | `BT15-074` | 3207 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_074.asset` |
| `BT15-074#4752@P0` | `BT15-074` | 4752 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_074_P0.asset` |
| `BT15-077#3211@base` | `BT15-077` | 3211 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_077.asset` |
| `BT15-080#3214@base` | `BT15-080` | 3214 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_080.asset` |
| `BT15-080#4755@P0` | `BT15-080` | 4755 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT15/Purple/Digimon/BT15_080_P0.asset` |

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
