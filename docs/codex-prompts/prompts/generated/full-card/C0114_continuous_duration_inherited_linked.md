# C0114_continuous_duration_inherited_linked - continuous/duration/inherited/linked card porting 10

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0114_continuous_duration_inherited_linked`
- Kind: `card-porting`
- Category: `continuous-duration-inherited-linked` / continuous/duration/inherited/linked
- Dependencies: `L0104_continuous_duration_inherited_linked`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT2_079` | `DCGO/Assets/Scripts/CardEffect/BT2/Purple/BT2_079.cs` | `None, OnTappedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT3_010` | `DCGO/Assets/Scripts/CardEffect/BT3/Red/BT3_010.cs` | `None` | `inherited, static_or_continuous` | `-` | 2 |
| `BT3_016` | `DCGO/Assets/Scripts/CardEffect/BT3/Red/BT3_016.cs` | `OnDetermineDoSecurityCheck` | `inherited` | `-` | 1 |
| `BT3_040` | `DCGO/Assets/Scripts/CardEffect/BT3/Yellow/BT3_040.cs` | `None` | `inherited, modifier_duration, static_or_continuous` | `-` | 1 |
| `BT3_050` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_050.cs` | `OnEndBattle` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `BT3_055` | `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_055.cs` | `None, OnDetermineDoSecurityCheck` | `inherited, static_or_continuous` | `-` | 1 |
| `BT3_066` | `DCGO/Assets/Scripts/CardEffect/BT3/Black/BT3_066.cs` | `None` | `inherited, static_or_continuous` | `-` | 5 |
| `BT3_074` | `DCGO/Assets/Scripts/CardEffect/BT3/Black/BT3_074.cs` | `None` | `inherited, static_or_continuous` | `-` | 1 |
| `BT3_075` | `DCGO/Assets/Scripts/CardEffect/BT3/Black/BT3_075.cs` | `None` | `inherited, static_or_continuous` | `-` | 4 |
| `BT3_080` | `DCGO/Assets/Scripts/CardEffect/BT3/Purple/BT3_080.cs` | `OnDestroyedAnyone` | `inherited` | `-` | 5 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT2-079#511@base` | `BT2-079` | 511 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT2/Purple/Digimon/BT2_079.asset` |
| `BT3-010#605@base` | `BT3-010` | 605 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_010.asset` |
| `BT3-013#609@base` | `BT3-013` | 609 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_013.asset` |
| `BT3-016#612@base` | `BT3-016` | 612 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Red/Digimon/BT3_016.asset` |
| `BT3-040#652@base` | `BT3-040` | 652 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Yellow/Digimon/BT3_040.asset` |
| `BT3-050#667@base` | `BT3-050` | 667 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_050.asset` |
| `BT3-050#668@P1` | `BT3-050` | 668 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_050_P1.asset` |
| `BT3-050#669@P2` | `BT3-050` | 669 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_050_P2.asset` |
| `BT3-055#675@base` | `BT3-055` | 675 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Green/Digimon/BT3_055.asset` |
| `BT3-066#692@base` | `BT3-066` | 692 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_066.asset` |
| `BT3-068#695@base` | `BT3-068` | 695 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_068.asset` |
| `BT3-074#702@base` | `BT3-074` | 702 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_074.asset` |
| `BT3-075#703@base` | `BT3-075` | 703 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_075.asset` |
| `BT3-075#704@P1` | `BT3-075` | 704 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_075_P1.asset` |
| `BT3-075#705@P2` | `BT3-075` | 705 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_075_P2.asset` |
| `BT3-075#8483@P3` | `BT3-075` | 8483 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Black/Digimon/BT3_075_P3.asset` |
| `BT3-080#712@base` | `BT3-080` | 712 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_080.asset` |
| `BT8-075#1657@base` | `BT8-075` | 1657 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT8/Purple/Digimon/BT8_075.asset` |
| `P-013#10340@P2` | `P-013` | 10340 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_013_P2.asset` |
| `P-013#6031@base` | `P-013` | 6031 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_013.asset` |
| `P-013#6032@P1` | `P-013` | 6032 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_013_P1.asset` |
| `P-019#6044@base` | `P-019` | 6044 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_019.asset` |
| `ST16-04#2849@base` | `ST16-04` | 2849 | `base` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_04.asset` |
| `ST16-04#4947@P0` | `ST16-04` | 4947 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/ST16/Purple/Digimon/ST16_04_P0.asset` |

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
