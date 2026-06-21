# C0355_special_digivolution_play - special digivolution/play mechanics card porting 120

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0355_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX11_036` | `DCGO/Assets/Scripts/CardEffect/EX11/Green/EX11_036.cs` | `None, OnAllyAttack, OnEndTurn, OnEnterFieldAnyone, WhenLinked` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectAssembly` | 2 |
| `EX11_037` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_037.cs` | `None, OnEnterFieldAnyone, OnMove` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 2 |
| `EX11_039` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_039.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `EX11_040` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_040.cs` | `None, OnEnterFieldAnyone, WhenLinked` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectInteger, SelectJogress` | 2 |
| `EX11_041` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_041.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OnSecurityCheck` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 2 |
| `EX11_042` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_042.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenLinked` | `inherited, linked, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectJogress` | 2 |
| `EX11_046` | `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_046.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectBoolean, SelectJogress, SelectAssembly` | 2 |
| `EX11_047` | `DCGO/Assets/Scripts/CardEffect/EX11/Purple/EX11_047.cs` | `None, OnStartMainPhase` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectHand, SelectJogress` | 2 |
| `EX11_053` | `DCGO/Assets/Scripts/CardEffect/EX11/White/EX11_053.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectInteger, SelectSecurity, SelectJogress` | 2 |
| `EX11_055` | `DCGO/Assets/Scripts/CardEffect/EX11/Red/EX11_055.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX11-036#7731@base` | `EX11-036` | 7731 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_036.asset` |
| `EX11-036#7732@P1` | `EX11-036` | 7732 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Green/Digimon/EX11_036_P1.asset` |
| `EX11-037#7733@base` | `EX11-037` | 7733 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_037.asset` |
| `EX11-037#7734@P1` | `EX11-037` | 7734 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_037_P1.asset` |
| `EX11-039#7737@base` | `EX11-039` | 7737 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_039.asset` |
| `EX11-039#7738@P1` | `EX11-039` | 7738 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_039_P1.asset` |
| `EX11-040#7739@base` | `EX11-040` | 7739 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_040.asset` |
| `EX11-040#7740@P1` | `EX11-040` | 7740 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_040_P1.asset` |
| `EX11-041#7741@base` | `EX11-041` | 7741 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_041.asset` |
| `EX11-041#7742@P1` | `EX11-041` | 7742 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_041_P1.asset` |
| `EX11-042#7743@base` | `EX11-042` | 7743 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_042.asset` |
| `EX11-042#7744@P1` | `EX11-042` | 7744 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_042_P1.asset` |
| `EX11-046#7752@base` | `EX11-046` | 7752 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_046.asset` |
| `EX11-046#7753@P1` | `EX11-046` | 7753 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Black/Digimon/EX11_046_P1.asset` |
| `EX11-047#7754@base` | `EX11-047` | 7754 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_047.asset` |
| `EX11-047#7755@P1` | `EX11-047` | 7755 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Purple/Digimon/EX11_047_P1.asset` |
| `EX11-053#7767@base` | `EX11-053` | 7767 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/White/Digimon/EX11_053.asset` |
| `EX11-053#9102@P1` | `EX11-053` | 9102 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/White/Digimon/EX11_053_P1.asset` |
| `EX11-055#7770@base` | `EX11-055` | 7770 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Tamer/EX11_055.asset` |
| `EX11-055#7771@P1` | `EX11-055` | 7771 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX11/Red/Tamer/EX11_055_P1.asset` |

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
