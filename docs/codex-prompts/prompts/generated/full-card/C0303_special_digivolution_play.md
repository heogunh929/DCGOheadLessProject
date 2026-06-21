# C0303_special_digivolution_play - special digivolution/play mechanics card porting 68

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0303_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT22_059` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_059.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT22_063` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_063.cs` | `None, OnTappedAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT22_064` | `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_064.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 4 |
| `BT22_068` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_068.cs` | `None, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT22_070` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_070.cs` | `None, OnAllyAttack, OnEndBattle, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT22_071` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_071.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 1 |
| `BT22_078` | `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_078.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectAssembly` | 1 |
| `BT22_081` | `DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_081.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 1 |
| `BT22_082` | `DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_082.cs` | `None, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 1 |
| `BT22_083` | `DCGO/Assets/Scripts/CardEffect/BT22/Red/BT22_083.cs` | `OnAttackTargetChanged, OnStartMainPhase, SecuritySkill` | `inherited, max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT22-059#7065@base` | `BT22-059` | 7065 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_059.asset` |
| `BT22-063#7069@base` | `BT22-063` | 7069 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_063.asset` |
| `BT22-063#7070@P1` | `BT22-063` | 7070 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_063_P1.asset` |
| `BT22-064#7071@base` | `BT22-064` | 7071 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_064.asset` |
| `BT22-064#8436@P1` | `BT22-064` | 8436 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_064_P1.asset` |
| `BT22-064#8437@P2` | `BT22-064` | 8437 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_064_P2.asset` |
| `BT22-064#8438@P3` | `BT22-064` | 8438 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Black/Digimon/BT22_064_P3.asset` |
| `BT22-068#7075@base` | `BT22-068` | 7075 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_068.asset` |
| `BT22-070#7077@base` | `BT22-070` | 7077 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_070.asset` |
| `BT22-071#7078@base` | `BT22-071` | 7078 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_071.asset` |
| `BT22-078#7086@base` | `BT22-078` | 7086 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Purple/Digimon/BT22_078.asset` |
| `BT22-081#7091@base` | `BT22-081` | 7091 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Digimon/BT22_081.asset` |
| `BT22-082#7092@base` | `BT22-082` | 7092 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/White/Digimon/BT22_082.asset` |
| `BT22-083#7093@base` | `BT22-083` | 7093 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Tamer/BT22_083.asset` |
| `BT22-083#7094@P1` | `BT22-083` | 7094 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Tamer/BT22_083_P1.asset` |
| `BT22-083#7095@P2` | `BT22-083` | 7095 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Tamer/BT22_083_P2.asset` |

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
