# C0351_special_digivolution_play - special digivolution/play mechanics card porting 116

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0351_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `EX10_032` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_032.cs` | `OnAllyAttack, OnDeclaration, OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectJogress` | 3 |
| `EX10_035` | `DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_035.cs` | `None, OnDeclaration, OnDestroyedAnyone, OnEndTurn, SecuritySkill` | `inherited, max_count_per_turn, modifier_duration, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectCount, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `EX10_038` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_038.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `linked, max_count_per_turn, skippable, static_or_continuous, zone_movement` | `SelectCard` | 1 |
| `EX10_042` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_042.cs` | `None, OnAddDigivolutionCards, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectBoolean, SelectJogress` | 2 |
| `EX10_043` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_043.cs` | `None, OnAllyAttack, OnDeclaration, OnEnterFieldAnyone, OnLinkCardDiscarded` | `linked, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 2 |
| `EX10_044` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_044.cs` | `OnDestroyedAnyone, OnDigivolutionCardDiscarded, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectJogress` | 2 |
| `EX10_047` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_047.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX10_050` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_050.cs` | `None, OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectJogress` | 2 |
| `EX10_051` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_051.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 2 |
| `EX10_053` | `DCGO/Assets/Scripts/CardEffect/EX10/Purple/EX10_053.cs` | `None, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `EX10-032#7192@base` | `EX10-032` | 7192 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_032.asset` |
| `EX10-032#7298@P1` | `EX10-032` | 7298 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_032_P1.asset` |
| `EX10-032#9100@P2` | `EX10-032` | 9100 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_032_P2.asset` |
| `EX10-035#7196@base` | `EX10-035` | 7196 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_035.asset` |
| `EX10-035#7299@P1` | `EX10-035` | 7299 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_035_P1.asset` |
| `EX10-038#7202@base` | `EX10-038` | 7202 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_038.asset` |
| `EX10-042#7209@base` | `EX10-042` | 7209 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_042.asset` |
| `EX10-042#7305@P1` | `EX10-042` | 7305 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_042_P1.asset` |
| `EX10-043#7211@base` | `EX10-043` | 7211 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_043.asset` |
| `EX10-043#7306@P1` | `EX10-043` | 7306 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_043_P1.asset` |
| `EX10-044#7213@base` | `EX10-044` | 7213 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_044.asset` |
| `EX10-044#7307@P1` | `EX10-044` | 7307 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_044_P1.asset` |
| `EX10-047#7219@base` | `EX10-047` | 7219 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_047.asset` |
| `EX10-047#7310@P1` | `EX10-047` | 7310 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_047_P1.asset` |
| `EX10-050#7224@base` | `EX10-050` | 7224 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_050.asset` |
| `EX10-050#7312@P1` | `EX10-050` | 7312 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_050_P1.asset` |
| `EX10-051#7226@base` | `EX10-051` | 7226 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_051.asset` |
| `EX10-051#7313@P1` | `EX10-051` | 7313 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_051_P1.asset` |
| `EX10-053#7229@base` | `EX10-053` | 7229 | `base` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_053.asset` |
| `EX10-053#7314@P1` | `EX10-053` | 7314 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_053_P1.asset` |

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
