# C0295_special_digivolution_play - special digivolution/play mechanics card porting 60

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0295_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 17
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT21_005` | `DCGO/Assets/Scripts/CardEffect/BT21/Green/BT21_005.cs` | `WhenLinked` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT21_006` | `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_006.cs` | `None` | `inherited, static_or_continuous` | `SelectJogress` | 1 |
| `BT21_009` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_009.cs` | `None, OnAllyAttack, OnDeclaration, WhenLinked` | `inherited, linked, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 2 |
| `BT21_010` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_010.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectSecurity, SelectJogress` | 3 |
| `BT21_013` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_013.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `BT21_014` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_014.cs` | `None, OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectJogress` | 1 |
| `BT21_016` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_016.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnDetermineDoSecurityCheck` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean, SelectJogress, SelectDigiXros` | 1 |
| `BT21_017` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_017.cs` | `OnEnterFieldAnyone, OnLoseSecurity` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 2 |
| `BT21_018` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_018.cs` | `None, OnAllyAttack, OnDeclaration, WhenLinked` | `inherited, linked, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectAttackTarget` | 1 |
| `BT21_019` | `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_019.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT21-005#5311@base` | `BT21-005` | 5311 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Green/DigiEgg/BT21_005.asset` |
| `BT21-006#5312@base` | `BT21-006` | 5312 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Black/DigiEgg/BT21_006.asset` |
| `BT21-009#5316@base` | `BT21-009` | 5316 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_009.asset` |
| `BT21-009#5317@P1` | `BT21-009` | 5317 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_009_P1.asset` |
| `BT21-010#5318@base` | `BT21-010` | 5318 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_010.asset` |
| `BT21-010#8372@P1` | `BT21-010` | 8372 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_010_P1.asset` |
| `BT21-010#8373@P2` | `BT21-010` | 8373 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_010_P2.asset` |
| `BT21-013#5321@base` | `BT21-013` | 5321 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_013.asset` |
| `BT21-013#8374@P1` | `BT21-013` | 8374 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_013_P1.asset` |
| `BT21-014#5322@base` | `BT21-014` | 5322 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_014.asset` |
| `BT21-016#5324@base` | `BT21-016` | 5324 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_016.asset` |
| `BT21-017#5325@base` | `BT21-017` | 5325 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_017.asset` |
| `BT21-017#8375@P1` | `BT21-017` | 8375 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_017_P1.asset` |
| `BT21-018#5326@base` | `BT21-018` | 5326 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_018.asset` |
| `BT21-019#5327@base` | `BT21-019` | 5327 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_019.asset` |
| `BT21-019#8376@P1` | `BT21-019` | 8376 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_019_P1.asset` |
| `BT21-019#8377@P2` | `BT21-019` | 8377 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_019_P2.asset` |

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
