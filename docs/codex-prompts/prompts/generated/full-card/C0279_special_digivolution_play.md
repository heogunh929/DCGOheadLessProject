# C0279_special_digivolution_play - special digivolution/play mechanics card porting 44

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0279_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 15
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT18_034` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_034.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OnStartMainPhase` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT18_037` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_037.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `BT18_041` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_041.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT18_047` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_047.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT18_048` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_048.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT18_049` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_049.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT18_050` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_050.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT18_053` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_053.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 1 |
| `BT18_054` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_054.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectJogress, SelectDigiXros` | 2 |
| `BT18_055` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_055.cs` | `None, OnTappedAnyone, WhenRemoveField` | `max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectBoolean, SelectSecurity, SelectJogress, SelectDigiXros` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT18-034#3884@base` | `BT18-034` | 3884 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_034.asset` |
| `BT18-034#3885@P1` | `BT18-034` | 3885 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_034_P1.asset` |
| `BT18-037#3883@base` | `BT18-037` | 3883 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_037.asset` |
| `BT18-041#3905@base` | `BT18-041` | 3905 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_041.asset` |
| `BT18-047#3892@base` | `BT18-047` | 3892 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_047.asset` |
| `BT18-048#3894@base` | `BT18-048` | 3894 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_048.asset` |
| `BT18-048#3895@P1` | `BT18-048` | 3895 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_048_P1.asset` |
| `BT18-049#3899@base` | `BT18-049` | 3899 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_049.asset` |
| `BT18-049#3900@P1` | `BT18-049` | 3900 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_049_P1.asset` |
| `BT18-050#3914@base` | `BT18-050` | 3914 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_050.asset` |
| `BT18-053#3913@base` | `BT18-053` | 3913 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_053.asset` |
| `BT18-054#3909@base` | `BT18-054` | 3909 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_054.asset` |
| `BT18-054#8257@P1` | `BT18-054` | 8257 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_054_P1.asset` |
| `BT18-055#3910@base` | `BT18-055` | 3910 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_055.asset` |
| `BT18-055#8258@P1` | `BT18-055` | 8258 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_055_P1.asset` |

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
