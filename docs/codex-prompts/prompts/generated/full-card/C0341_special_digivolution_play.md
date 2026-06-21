# C0341_special_digivolution_play - special digivolution/play mechanics card porting 106

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0341_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 32
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT8_081` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_081.cs` | `OnDigivolutionCardDiscarded, OnEndAttack, OnEndTurn` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT8_082` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_082.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT8_083` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_083.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT8_084` | `DCGO/Assets/Scripts/CardEffect/BT8/White/BT8_084.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT8_086` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_086.cs` | `OnAllyAttack, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 4 |
| `BT8_091` | `DCGO/Assets/Scripts/CardEffect/BT8/Green/BT8_091.cs` | `BeforePayCost, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 5 |
| `BT8_093` | `DCGO/Assets/Scripts/CardEffect/BT8/Purple/BT8_093.cs` | `OnDestroyedAnyone, OnEndTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 3 |
| `BT8_095` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_095.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 3 |
| `BT8_096` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_096.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT8_097` | `DCGO/Assets/Scripts/CardEffect/BT8/Red/BT8_097.cs` | `None, OptionSkill, SecuritySkill` | `max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 6 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT8-081#1664@base` | `BT8-081` | 1664 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_081.asset` |
| `BT8-081#8895@P0` | `BT8-081` | 8895 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_081_P0.asset` |
| `BT8-081#8896@P1` | `BT8-081` | 8896 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_081_P1.asset` |
| `BT8-082#1665@base` | `BT8-082` | 1665 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_082.asset` |
| `BT8-082#1666@P1` | `BT8-082` | 1666 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_082_P1.asset` |
| `BT8-082#8897@P2` | `BT8-082` | 8897 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_082_P2.asset` |
| `BT8-083#1667@base` | `BT8-083` | 1667 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_083.asset` |
| `BT8-083#1668@P1` | `BT8-083` | 1668 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_083_P1.asset` |
| `BT8-084#1669@base` | `BT8-084` | 1669 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Digimon/BT8_084.asset` |
| `BT8-084#1670@P1` | `BT8-084` | 1670 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/White/Digimon/BT8_084_P1.asset` |
| `BT8-086#1673@base` | `BT8-086` | 1673 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Tamer/BT8_086.asset` |
| `BT8-086#1674@P1` | `BT8-086` | 1674 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Tamer/BT8_086_P1.asset` |
| `BT8-086#8901@P0` | `BT8-086` | 8901 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Tamer/BT8_086_P0.asset` |
| `BT8-086#8902@P2` | `BT8-086` | 8902 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Tamer/BT8_086_P2.asset` |
| `BT8-091#1683@base` | `BT8-091` | 1683 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Tamer/BT8_091.asset` |
| `BT8-091#1684@P1` | `BT8-091` | 1684 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Tamer/BT8_091_P1.asset` |
| `BT8-091#3291@P2` | `BT8-091` | 3291 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Tamer/BT8_091_P2.asset` |
| `BT8-091#8912@P0` | `BT8-091` | 8912 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Tamer/BT8_091_P0.asset` |
| `BT8-091#8913@P02` | `BT8-091` | 8913 | `P02` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Green/Tamer/BT8_091_P02.asset` |
| `BT8-093#1686@base` | `BT8-093` | 1686 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Tamer/BT8_093.asset` |
| `BT8-093#8918@P0` | `BT8-093` | 8918 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Tamer/BT8_093_P0.asset` |
| `BT8-093#8919@P1` | `BT8-093` | 8919 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Tamer/BT8_093_P1.asset` |
| `BT8-095#1688@base` | `BT8-095` | 1688 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Option/BT8_095.asset` |
| `BT8-095#8921@P1` | `BT8-095` | 8921 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Option/BT8_095_P1.asset` |
| `BT8-095#8922@P2` | `BT8-095` | 8922 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Option/BT8_095_P2.asset` |
| `BT8-096#1689@base` | `BT8-096` | 1689 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Option/BT8_096.asset` |
| `BT8-097#1690@base` | `BT8-097` | 1690 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Option/BT8_097.asset` |
| `BT8-097#6805@P0` | `BT8-097` | 6805 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Option/BT8_097_P0.asset` |
| `BT8-097#6806@P1` | `BT8-097` | 6806 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Option/BT8_097_P1.asset` |
| `BT8-097#6807@P2` | `BT8-097` | 6807 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Option/BT8_097_P2.asset` |
| `BT8-097#8923@P3` | `BT8-097` | 8923 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Option/BT8_097_P3.asset` |
| `BT8-097#8924@P4` | `BT8-097` | 8924 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Red/Option/BT8_097_P4.asset` |

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
