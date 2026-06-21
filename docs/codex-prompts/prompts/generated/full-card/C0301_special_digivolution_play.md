# C0301_special_digivolution_play - special digivolution/play mechanics card porting 66

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0301_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 14
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT22_014` | `DCGO/Assets/Scripts/CardEffect/BT22/Red/BT22_014.cs` | `None, OnAllyAttack, OnAttackTargetChanged, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectAttackTarget, SelectJogress` | 1 |
| `BT22_015` | `DCGO/Assets/Scripts/CardEffect/BT22/Red/BT22_015.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `BT22_016` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_016.cs` | `None, OnDeclaration, OnEnterFieldAnyone, WhenLinked` | `linked, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT22_017` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_017.cs` | `None, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 1 |
| `BT22_020` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_020.cs` | `None, OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectJogress` | 1 |
| `BT22_023` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_023.cs` | `None, OnEndTurn, OnEnterFieldAnyone, OnTappedAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 1 |
| `BT22_024` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_024.cs` | `OnDeclaration, OnEndAttack, WhenRemoveField` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT22_026` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_026.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectInteger, SelectJogress` | 2 |
| `BT22_028` | `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_028.cs` | `None, OnAllyAttack, OnEnterFieldAnyone, WhenRemoveField` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 2 |
| `BT22_030` | `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_030.cs` | `None, OnAllyAttack, OnDeclaration, WhenLinked` | `linked, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT22-014#7005@base` | `BT22-014` | 7005 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Digimon/BT22_014.asset` |
| `BT22-015#7006@base` | `BT22-015` | 7006 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Digimon/BT22_015.asset` |
| `BT22-015#7007@P1` | `BT22-015` | 7007 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Red/Digimon/BT22_015_P1.asset` |
| `BT22-016#7008@base` | `BT22-016` | 7008 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_016.asset` |
| `BT22-017#7009@base` | `BT22-017` | 7009 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_017.asset` |
| `BT22-020#7013@base` | `BT22-020` | 7013 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_020.asset` |
| `BT22-023#7017@base` | `BT22-023` | 7017 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_023.asset` |
| `BT22-024#7018@base` | `BT22-024` | 7018 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_024.asset` |
| `BT22-024#8428@P1` | `BT22-024` | 8428 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_024_P1.asset` |
| `BT22-026#7021@base` | `BT22-026` | 7021 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_026.asset` |
| `BT22-026#8429@P1` | `BT22-026` | 8429 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_026_P1.asset` |
| `BT22-028#7023@base` | `BT22-028` | 7023 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_028.asset` |
| `BT22-028#7024@P1` | `BT22-028` | 7024 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Blue/Digimon/BT22_028_P1.asset` |
| `BT22-030#7027@base` | `BT22-030` | 7027 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT22/Yellow/Digimon/BT22_030.asset` |

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
