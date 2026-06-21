# C0111_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 7

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0111_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 33
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT1_031` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_031.cs` | `None` | `inherited, static_or_continuous` | `-` | 14 |
| `BT1_033` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_033.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT1_034` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_034.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT1_035` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_035.cs` | `OnDestroyedAnyone` | `max_count_per_turn, static_or_continuous` | `-` | 3 |
| `BT1_068` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_068.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT1_073` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_073.cs` | `None` | `inherited, static_or_continuous` | `-` | 5 |
| `BT1_077` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_077.cs` | `OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `BT1_083` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_083.cs` | `None, OnDetermineDoSecurityCheck` | `inherited, static_or_continuous` | `-` | 2 |
| `BT20_005` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_005.cs` | `OnSecurityCheck` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 2 |
| `BT20_047` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_047.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-031#181@base` | `BT1-031` | 181 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_031.asset` |
| `BT1-033#183@base` | `BT1-033` | 183 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_033.asset` |
| `BT1-034#184@base` | `BT1-034` | 184 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_034.asset` |
| `BT1-035#185@base` | `BT1-035` | 185 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_035.asset` |
| `BT1-035#4266@P1` | `BT1-035` | 4266 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_035_P1.asset` |
| `BT1-035#4267@P2` | `BT1-035` | 4267 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_035_P2.asset` |
| `BT1-068#236@base` | `BT1-068` | 236 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_068.asset` |
| `BT1-073#242@base` | `BT1-073` | 242 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_073.asset` |
| `BT1-073#243@P1` | `BT1-073` | 243 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_073_P1.asset` |
| `BT1-077#250@base` | `BT1-077` | 250 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_077.asset` |
| `BT1-077#251@P1` | `BT1-077` | 251 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_077_P1.asset` |
| `BT1-077#252@P2` | `BT1-077` | 252 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_077_P2.asset` |
| `BT1-083#261@base` | `BT1-083` | 261 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_083.asset` |
| `BT1-083#262@P1` | `BT1-083` | 262 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_083_P1.asset` |
| `BT12-049#2461@base` | `BT12-049` | 2461 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT12/Green/Digimon/BT12_049.asset` |
| `BT13-022#2669@base` | `BT13-022` | 2669 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_022.asset` |
| `BT13-024#2671@base` | `BT13-024` | 2671 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Digimon/BT13_024.asset` |
| `BT14-011#2929@base` | `BT14-011` | 2929 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_011.asset` |
| `BT2-048#446@base` | `BT2-048` | 446 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_048.asset` |
| `BT2-061#472@base` | `BT2-061` | 472 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_061.asset` |
| `BT2-061#473@P1` | `BT2-061` | 473 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Black/Digimon/BT2_061_P1.asset` |
| `BT20-005#5084@base` | `BT20-005` | 5084 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/DigiEgg/BT20_005.asset` |
| `BT20-005#5193@P1` | `BT20-005` | 5193 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/DigiEgg/BT20_005_P1.asset` |
| `BT20-047#5126@base` | `BT20-047` | 5126 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT20/Black/Digimon/BT20_047.asset` |
| `BT3-048#663@base` | `BT3-048` | 663 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_048.asset` |
| `BT3-048#664@P1` | `BT3-048` | 664 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_048_P1.asset` |
| `BT3-052#671@base` | `BT3-052` | 671 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_052.asset` |
| `BT5-061#1019@base` | `BT5-061` | 1019 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT5/Black/Digimon/BT5_061.asset` |
| `ST16-06#2851@base` | `ST16-06` | 2851 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_06.asset` |
| `ST16-06#4949@P0` | `ST16-06` | 4949 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_06_P0.asset` |
| `ST5-03#322@base` | `ST5-03` | 322 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_03.asset` |
| `ST5-03#323@P1` | `ST5-03` | 323 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/ST5/Black/Digimon/ST5_03_P1.asset` |
| `ST8-07#1724@base` | `ST8-07` | 1724 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST8/Blue/Digimon/ST8_07.asset` |

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
