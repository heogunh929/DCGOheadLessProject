# C0387_special_digivolution_play - special digivolution/play mechanics card porting 152

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0387_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 10
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `LM_051` | `DCGO/Assets/Scripts/CardEffect/LM/Red/LM_051.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `LM_052` | `DCGO/Assets/Scripts/CardEffect/LM/Blue/LM_052.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `LM_053` | `DCGO/Assets/Scripts/CardEffect/LM/Black/LM_053.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, static_or_continuous` | `SelectCard, SelectSecurity, SelectJogress` | 1 |
| `LM_054` | `DCGO/Assets/Scripts/CardEffect/LM/Yellow/LM_054.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `LM_055` | `DCGO/Assets/Scripts/CardEffect/LM/Green/LM_055.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `LM_056` | `DCGO/Assets/Scripts/CardEffect/LM/Purple/LM_056.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `LM_057` | `DCGO/Assets/Scripts/CardEffect/LM/Red/LM_057.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `LM_058` | `DCGO/Assets/Scripts/CardEffect/LM/Blue/LM_058.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `LM_059` | `DCGO/Assets/Scripts/CardEffect/LM/Yellow/LM_059.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |
| `LM_060` | `DCGO/Assets/Scripts/CardEffect/LM/Green/LM_060.cs` | `None, OnDeclaration, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `LM-051#7812@base` | `LM-051` | 7812 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Red/Option/LM_051.asset` |
| `LM-052#7813@base` | `LM-052` | 7813 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Option/LM_052.asset` |
| `LM-053#7814@base` | `LM-053` | 7814 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Black/Option/LM_053.asset` |
| `LM-054#7811@base` | `LM-054` | 7811 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Yellow/Option/LM_054.asset` |
| `LM-055#7815@base` | `LM-055` | 7815 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Option/LM_055.asset` |
| `LM-056#7816@base` | `LM-056` | 7816 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Option/LM_056.asset` |
| `LM-057#7892@base` | `LM-057` | 7892 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Red/Option/LM_057.asset` |
| `LM-058#7893@base` | `LM-058` | 7893 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Blue/Option/LM_058.asset` |
| `LM-059#7894@base` | `LM-059` | 7894 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Yellow/Option/LM_059.asset` |
| `LM-060#7895@base` | `LM-060` | 7895 | `base` | `None` | `DCGO/Assets/CardBaseEntity/LM/Green/Option/LM_060.asset` |

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
