# C0052_zone_security_recovery - zone/security/recovery card porting 46

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0052_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 16
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT4_009` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_009.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT4_013` | `DCGO/Assets/Scripts/CardEffect/BT4/Red/BT4_013.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `-` | 2 |
| `BT4_023` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_023.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 1 |
| `BT4_025` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_025.cs` | `None` | `static_or_continuous, zone_movement` | `-` | 4 |
| `BT4_035` | `DCGO/Assets/Scripts/CardEffect/BT4/Blue/BT4_035.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 2 |
| `BT4_037` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_037.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT4_039` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_039.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectSecurity` | 1 |
| `BT4_041` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_041.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `BT4_045` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_045.cs` | `None` | `inherited, static_or_continuous, zone_movement` | `SelectSecurity` | 1 |
| `BT4_047` | `DCGO/Assets/Scripts/CardEffect/BT4/Yellow/BT4_047.cs` | `OnEndTurn, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity` | 2 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT4-009#771@base` | `BT4-009` | 771 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_009.asset` |
| `BT4-013#781@base` | `BT4-013` | 781 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_013.asset` |
| `BT4-013#8505@P0` | `BT4-013` | 8505 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Red/Digimon/BT4_013_P0.asset` |
| `BT4-023#795@base` | `BT4-023` | 795 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_023.asset` |
| `BT4-025#6775@P0` | `BT4-025` | 6775 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_025_P0.asset` |
| `BT4-025#6776@P2` | `BT4-025` | 6776 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_025_P2.asset` |
| `BT4-025#797@base` | `BT4-025` | 797 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_025.asset` |
| `BT4-025#798@P1` | `BT4-025` | 798 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_025_P1.asset` |
| `BT4-035#810@base` | `BT4-035` | 810 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_035.asset` |
| `BT4-035#811@P1` | `BT4-035` | 811 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Blue/Digimon/BT4_035_P1.asset` |
| `BT4-037#813@base` | `BT4-037` | 813 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_037.asset` |
| `BT4-039#816@base` | `BT4-039` | 816 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_039.asset` |
| `BT4-041#818@base` | `BT4-041` | 818 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_041.asset` |
| `BT4-045#823@base` | `BT4-045` | 823 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_045.asset` |
| `BT4-047#825@base` | `BT4-047` | 825 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_047.asset` |
| `BT4-047#8519@P0` | `BT4-047` | 8519 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_047_P0.asset` |

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
