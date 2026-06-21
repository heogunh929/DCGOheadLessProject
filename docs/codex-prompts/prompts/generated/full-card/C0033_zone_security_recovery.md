# C0033_zone_security_recovery - zone/security/recovery card porting 27

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0033_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT20_062` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_062.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT20_063` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_063.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 2 |
| `BT20_065` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_065.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT20_067` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_067.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 1 |
| `BT20_069` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_069.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 2 |
| `BT20_072` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_072.cs` | `OnDestroyedAnyone, OnEndTurn` | `inherited, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard` | 4 |
| `BT20_075` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_075.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 4 |
| `BT20_079` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_079.cs` | `None, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 3 |
| `BT20_086` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_086.cs` | `OnStartMainPhase, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity` | 3 |
| `BT20_088` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_088.cs` | `OnDestroyedAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT20-062#5141@base` | `BT20-062` | 5141 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_062.asset` |
| `BT20-063#5142@base` | `BT20-063` | 5142 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_063.asset` |
| `BT20-063#5226@P1` | `BT20-063` | 5226 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_063_P1.asset` |
| `BT20-065#5144@base` | `BT20-065` | 5144 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_065.asset` |
| `BT20-067#5146@base` | `BT20-067` | 5146 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_067.asset` |
| `BT20-069#5148@base` | `BT20-069` | 5148 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_069.asset` |
| `BT20-069#8348@P1` | `BT20-069` | 8348 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_069_P1.asset` |
| `BT20-072#5151@base` | `BT20-072` | 5151 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_072.asset` |
| `BT20-072#8349@P1` | `BT20-072` | 8349 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_072_P1.asset` |
| `BT20-072#8350@P2` | `BT20-072` | 8350 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_072_P2.asset` |
| `BT20-072#8351@P3` | `BT20-072` | 8351 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_072_P3.asset` |
| `BT20-075#5154@base` | `BT20-075` | 5154 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_075.asset` |
| `BT20-075#8353@P1` | `BT20-075` | 8353 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_075_P1.asset` |
| `BT20-075#8354@P2` | `BT20-075` | 8354 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_075_P2.asset` |
| `BT20-075#8355@P3` | `BT20-075` | 8355 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_075_P3.asset` |
| `BT20-079#5158@base` | `BT20-079` | 5158 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_079.asset` |
| `BT20-079#5233@P1` | `BT20-079` | 5233 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_079_P1.asset` |
| `BT20-079#8356@P2` | `BT20-079` | 8356 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_079_P2.asset` |
| `BT20-086#5165@base` | `BT20-086` | 5165 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Tamer/BT20_086.asset` |
| `BT20-086#5244@P1` | `BT20-086` | 5244 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Tamer/BT20_086_P1.asset` |
| `BT20-086#8361@P2` | `BT20-086` | 8361 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Tamer/BT20_086_P2.asset` |
| `BT20-088#5167@base` | `BT20-088` | 5167 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Tamer/BT20_088.asset` |
| `BT20-088#5248@P1` | `BT20-088` | 5248 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Tamer/BT20_088_P1.asset` |
| `BT20-088#8362@P2` | `BT20-088` | 8362 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Purple/Tamer/BT20_088_P2.asset` |

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
