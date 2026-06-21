# C0271_special_digivolution_play - special digivolution/play mechanics card porting 36

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0271_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT16_097` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_097.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `BT16_098` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_098.cs` | `OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT16_099` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_099.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean, SelectOrder, SelectSecurity, SelectJogress` | 3 |
| `BT16_100` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_100.cs` | `BeforePayCost, None, OptionSkill, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `BT17_004` | `DCGO/Assets/Scripts/CardEffect/BT17/Green/BT17_004.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 2 |
| `BT17_007` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_007.cs` | `None, OnEndTurn, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectCard, SelectJogress` | 4 |
| `BT17_008` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_008.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 4 |
| `BT17_011` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_011.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectJogress` | 2 |
| `BT17_012` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_012.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectJogress` | 1 |
| `BT17_013` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_013.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT16-097#3425@base` | `BT16-097` | 3425 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Option/BT16_097.asset` |
| `BT16-097#4830@P0` | `BT16-097` | 4830 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Option/BT16_097_P0.asset` |
| `BT16-098#3426@base` | `BT16-098` | 3426 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Black/Option/BT16_098.asset` |
| `BT16-099#3427@base` | `BT16-099` | 3427 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Option/BT16_099.asset` |
| `BT16-099#4831@P0` | `BT16-099` | 4831 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Option/BT16_099_P0.asset` |
| `BT16-099#4832@P1` | `BT16-099` | 4832 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Option/BT16_099_P1.asset` |
| `BT16-100#3428@base` | `BT16-100` | 3428 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Option/BT16_100.asset` |
| `BT17-004#3544@base` | `BT17-004` | 3544 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/DigiEgg/BT17_004.asset` |
| `BT17-004#4836@P0` | `BT17-004` | 4836 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Green/DigiEgg/BT17_004_P0.asset` |
| `BT17-007#3547@base` | `BT17-007` | 3547 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_007.asset` |
| `BT17-007#4839@P0` | `BT17-007` | 4839 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_007_P0.asset` |
| `BT17-007#8216@P1` | `BT17-007` | 8216 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_007_P1.asset` |
| `BT17-007#8217@P2` | `BT17-007` | 8217 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_007_P2.asset` |
| `BT17-008#3548@base` | `BT17-008` | 3548 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_008.asset` |
| `BT17-008#4840@P0` | `BT17-008` | 4840 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_008_P0.asset` |
| `BT17-008#4841@P1` | `BT17-008` | 4841 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_008_P1.asset` |
| `BT17-008#4842@P2` | `BT17-008` | 4842 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_008_P2.asset` |
| `BT17-011#3551@base` | `BT17-011` | 3551 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_011.asset` |
| `BT17-011#4843@P1` | `BT17-011` | 4843 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_011_P1.asset` |
| `BT17-012#3552@base` | `BT17-012` | 3552 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_012.asset` |
| `BT17-013#3553@base` | `BT17-013` | 3553 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_013.asset` |

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
