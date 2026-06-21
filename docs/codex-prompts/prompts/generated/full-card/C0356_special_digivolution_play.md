# C0356_special_digivolution_play - special digivolution/play mechanics card porting 121

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0356_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX11_056` | `DCGO/Assets/Scripts/CardEffect/EX11/Red/EX11_056.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `EX11_059` | `DCGO/Assets/Scripts/CardEffect/EX11/Yellow/EX11_059.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectSecurity, SelectJogress` | 2 |
| `EX11_066` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_066.cs` | `None, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectOrder, SelectSecurity, SelectJogress` | 2 |
| `EX11_067` | `DCGO/Assets/Scripts/CardEffect/EX11/Purple/EX11_067.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectInteger, SelectJogress` | 2 |
| `EX11_070` | `DCGO/Assets/Scripts/CardEffect/EX11/White/EX11_070.cs` | `None, OnEndTurn, OnStartTurn, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, security, static_or_continuous, zone_movement` | `SelectJogress` | 2 |
| `EX11_071` | `DCGO/Assets/Scripts/CardEffect/EX11/White/EX11_071.cs` | `None, OnDeclaration, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 1 |
| `EX11_072` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_072.cs` | `OnTappedAnyone, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 1 |
| `EX11_073` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_073.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, linked, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 3 |
| `EX11_074` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_074.cs` | `None, OnAllyAttack, OnDetermineDoSecurityCheck, OnEndTurn, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectJogress` | 3 |
| `EX1_001` | `DCGO/Assets/Scripts/CardEffect/EX1/Red/EX1_001.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `SelectCard, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX1-001#1259@base` | `EX1-001` | 1259 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_001.asset` |
| `EX1-001#1260@P1` | `EX1-001` | 1260 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_001_P1.asset` |
| `EX1-001#9087@P2` | `EX1-001` | 9087 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX1/Red/Digimon/EX1_001_P2.asset` |
| `EX11-056#7772@base` | `EX11-056` | 7772 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Tamer/EX11_056.asset` |
| `EX11-059#7776@base` | `EX11-059` | 7776 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Tamer/EX11_059.asset` |
| `EX11-059#7777@P1` | `EX11-059` | 7777 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Yellow/Tamer/EX11_059_P1.asset` |
| `EX11-066#7787@base` | `EX11-066` | 7787 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Tamer/EX11_066.asset` |
| `EX11-066#7788@P1` | `EX11-066` | 7788 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Tamer/EX11_066_P1.asset` |
| `EX11-067#7789@base` | `EX11-067` | 7789 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Tamer/EX11_067.asset` |
| `EX11-067#7790@P1` | `EX11-067` | 7790 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Tamer/EX11_067_P1.asset` |
| `EX11-070#7795@base` | `EX11-070` | 7795 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/White/Tamer/EX11_070.asset` |
| `EX11-070#7796@P1` | `EX11-070` | 7796 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/White/Tamer/EX11_070_P1.asset` |
| `EX11-071#7797@base` | `EX11-071` | 7797 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/White/Tamer/EX11_071.asset` |
| `EX11-072#7798@base` | `EX11-072` | 7798 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Option/EX11_072.asset` |
| `EX11-073#7799@base` | `EX11-073` | 7799 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_073.asset` |
| `EX11-073#7800@P1` | `EX11-073` | 7800 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_073_P1.asset` |
| `EX11-073#7801@P2` | `EX11-073` | 7801 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_073_P2.asset` |
| `EX11-074#7802@base` | `EX11-074` | 7802 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_074.asset` |
| `EX11-074#7803@P1` | `EX11-074` | 7803 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_074_P1.asset` |
| `EX11-074#7804@P2` | `EX11-074` | 7804 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_074_P2.asset` |

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
