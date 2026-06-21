# C0361_special_digivolution_play - special digivolution/play mechanics card porting 126

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0361_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 13
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX2_047` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_047.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `EX2_049` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_049.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `EX2_050` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_050.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 1 |
| `EX2_051` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_051.cs` | `OnDeclaration` | `max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `EX2_052` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_052.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 1 |
| `EX2_053` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_053.cs` | `OnAllyAttack, OnEnterFieldAnyone` | `max_count_per_turn, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectOrder, SelectJogress` | 1 |
| `EX2_055` | `DCGO/Assets/Scripts/CardEffect/EX2/White/EX2_055.cs` | `BeforePayCost, None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectCount, SelectOrder, SelectJogress` | 1 |
| `EX2_056` | `DCGO/Assets/Scripts/CardEffect/EX2/Red/EX2_056.cs` | `BeforePayCost, None, OnDestroyedAnyone, OnEnterFieldAnyone, SecuritySkill` | `inherited, max_count_per_turn, optional, security, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 3 |
| `EX2_057` | `DCGO/Assets/Scripts/CardEffect/EX2/Blue/EX2_057.cs` | `None, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `EX2_058` | `DCGO/Assets/Scripts/CardEffect/EX2/Blue/EX2_058.cs` | `OnAllyAttack, OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX2-047#1993@base` | `EX2-047` | 1993 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_047.asset` |
| `EX2-049#1995@base` | `EX2-049` | 1995 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_049.asset` |
| `EX2-050#1996@base` | `EX2-050` | 1996 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_050.asset` |
| `EX2-051#1997@base` | `EX2-051` | 1997 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_051.asset` |
| `EX2-052#1998@base` | `EX2-052` | 1998 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_052.asset` |
| `EX2-053#1999@base` | `EX2-053` | 1999 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_053.asset` |
| `EX2-055#2001@base` | `EX2-055` | 2001 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/White/Digimon/EX2_055.asset` |
| `EX2-056#2002@base` | `EX2-056` | 2002 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Tamer/EX2_056.asset` |
| `EX2-056#2003@P1` | `EX2-056` | 2003 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Tamer/EX2_056_P1.asset` |
| `EX2-056#9114@P2` | `EX2-056` | 9114 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Red/Tamer/EX2_056_P2.asset` |
| `EX2-057#2004@base` | `EX2-057` | 2004 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Tamer/EX2_057.asset` |
| `EX2-058#2005@base` | `EX2-058` | 2005 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Tamer/EX2_058.asset` |
| `EX2-058#2006@P1` | `EX2-058` | 2006 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX2/Blue/Tamer/EX2_058_P1.asset` |

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
